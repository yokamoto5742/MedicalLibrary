using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class EyeDoc
    {
        public string FileName = "";

        public string PtId = "";

        PatBase _Pat = new PatBase();

        public PatBase Pat
        {
            get
            {
                if (!this._Pat.Id.Equals(this.PtId))
                {
                    this._Pat = PatBase.Load(this.PtId);
                }

                return this._Pat;
            }
        }

        public string UserId = "";
        public string UserName = "";

        public string SaveDate = "";
        public string SaveTime = "";

        public class Item
        {
            public string Kind = "";
            public string Name = "";
            public string Value = "";
        }

        /// <summary>
        /// 印刷情報（手術または検査データ）
        /// </summary>
        public List<Item> ItemList = new List<Item>();

        /// <summary>
        /// 家族連絡先
        /// </summary>
        public List<Item> ContactList = new List<Item>();

        /// <summary>
        /// 患者情報（短期入院）
        /// </summary>
        public List<Item> PatInfoList = new List<Item>();

        /// <summary>
        /// サマリ
        /// </summary>
        public List<Item> SumList = new List<Item>();

        /// <summary>
        /// 禁忌・アレルギー
        /// </summary>
        public List<Item> AllergyList = new List<Item>();

        /// <summary>
        /// 患者IDをパラメータで渡せば、患者基本情報・作成者・作成日時・連絡先・短期入院情報は自動的に取得する。
        /// あとはファイル名 FileName と手術・検査データを ItemList, サマリデータを SumList に入れて ExcelOpen すればよい。
        /// </summary>
        /// <param name="pt_id"></param>
        public EyeDoc(string pt_id)
        {
            int i = 0;

            if (pt_id.Length == 0 || !int.TryParse(pt_id, out i))
            {
                return;
            }

            this.PtId = pt_id;
            this._Pat = PatBase.Load(pt_id);

            this.UserId = LoginUser.Id;
            this.UserName = LoginUser.Name;

            this.SaveDate = DateTime.Now.ToString("yyyyMMdd");
            this.SaveTime = DateTime.Now.ToString("HHmmss");

            // 家族連絡先リスト作成

            List<PatContact> contact_list = PatContact.GetList(pt_id);

            foreach (PatContact c in contact_list)
            {
                Item tmpItem = new Item();

                tmpItem.Value = c.ShowSEQ + " " + c.Name;

                if (c.RelationVal.Length > 0)
                {
                    tmpItem.Value += ", " + c.RelationVal;

                    if (c.RelationComment.Length > 0)
                    {
                        tmpItem.Value += "（" + c.RelationComment + "）";
                    }
                }

                if (c.Tel1.Length > 0)
                {
                    tmpItem.Value += ", " + c.Tel1;

                    if (c.KindVal1.Length > 0)
                    {
                        tmpItem.Value += "（" + c.KindVal1 + "）";
                    }
                }

                if (c.Tel2.Length > 0)
                {
                    tmpItem.Value += ", " + c.Tel2;

                    if (c.KindVal2.Length > 0)
                    {
                        tmpItem.Value += "（" + c.KindVal2 + "）";
                    }
                }

                if (c.Tel3.Length > 0)
                {
                    tmpItem.Value += ", " + c.Tel3;

                    if (c.KindVal3.Length > 0)
                    {
                        tmpItem.Value += "（" + c.KindVal3 + "）";
                    }
                }

                if (c.Cont.Length > 0)
                {
                    tmpItem.Value += ", " + c.Cont;
                }

                tmpItem.Kind = "家族";
                tmpItem.Name = "連絡先";

                ContactList.Add(tmpItem);
            }

            // 患者基本情報
            Dictionary<string, List<BaseInfo>> dict = BaseInfo.GetDict(pt_id);

            // 患者情報（短期入院）作成（名称, 患者基本情報の項目コード）
            string[,] pat_infos =
            {
                { "既往歴", LibSettings.Current.BaseInfoCodes.Diag },
                { "内服・外用", LibSettings.Current.BaseInfoCodes.Drug },
                { "アレルギー", LibSettings.Current.BaseInfoCodes.Allergy }
            };

            for (int k = 0; k < pat_infos.GetLength(0); k++)
            {
                Item item = new Item();
                item.Kind = "患者情報";
                item.Name = pat_infos[k, 0];

                if (dict.ContainsKey(pat_infos[k, 1]))
                {
                    item.Value = dict[pat_infos[k, 1]][0].Value;
                }

                PatInfoList.Add(item);
            }

            // 禁忌・アレルギー　リスト作成（GroupCode 1: 食物, 2: 薬剤, 3: その他）
            string[] allergy_names = { "食物", "薬剤", "その他" };
            Dictionary<string, Item> allergy_dict = new Dictionary<string, Item>();

            for (int k = 0; k < allergy_names.Length; k++)
            {
                Item item = new Item();
                item.Kind = "禁忌アレルギー";
                item.Name = allergy_names[k];

                allergy_dict.Add((k + 1).ToString(), item);
                AllergyList.Add(item);
            }

            foreach (AllergyData allergy in AllergyData.GetList(PtId))
            {
                if (!allergy_dict.ContainsKey(allergy.GroupCode))
                {
                    continue;
                }

                if (allergy.Cont.Length > 0)
                {
                    allergy_dict[allergy.GroupCode].Value += "\r\n" + allergy.Name + " " + allergy.Cont;
                }
                else
                {
                    allergy_dict[allergy.GroupCode].Value += "\r\n" + allergy.Name;
                }
            }
        }

        /// <summary>
        /// Excel にデータを入れて開く。
        /// </summary>
        public void ExcelOpen()
        {
            if (!System.IO.File.Exists(FileName))
            {
                return;
            }

            Excel.Application exApp = new Excel.Application();
            Excel._Workbook exWorkbook;
            Excel._Worksheet exWorksheet;

            exApp.Visible = true;

            exWorkbook = (Excel._Workbook)(exApp.Workbooks.Open(FileName,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value));

            exWorksheet = (Excel._Worksheet)(exWorkbook.Sheets["共通情報"]);

            try
            {
                // 患者基本情報
                exWorksheet.Cells[3, 2] = this.Pat.Id;
                exWorksheet.Cells[3, 3] = this.Pat.Kana;
                exWorksheet.Cells[3, 4] = this.Pat.Name;
                exWorksheet.Cells[3, 5] = this.Pat.BirthString;
                exWorksheet.Cells[3, 6] = this.Pat.Age;
                exWorksheet.Cells[3, 7] = this.Pat.SexNameShort;
                exWorksheet.Cells[3, 8] = this.Pat.Addr;
                exWorksheet.Cells[3, 9] = "'" + this.Pat.Tel;

                // 作成者
                exWorksheet.Cells[7, 2] = UserId.PadLeft(5, '0');
                exWorksheet.Cells[7, 3] = UserName;

                // 作成日時
                exWorksheet.Cells[8, 2] = SaveDate;
                exWorksheet.Cells[9, 2] = SaveTime;

                // ファイル名
                Excel.Range doc1Range = (Excel.Range)(exWorksheet.Cells[4, 2]);
                Excel.Range doc2Range = (Excel.Range)(exWorksheet.Cells[22, 2]);
                Excel.Range deptRange = (Excel.Range)(exWorksheet.Cells[5, 2]);
                Excel.Range dateRange = (Excel.Range)(exWorksheet.Cells[8, 2]);
                Excel.Range timeRange = (Excel.Range)(exWorksheet.Cells[9, 2]);
                exWorksheet.Cells[11, 2] = PtId.PadLeft(9, '0') + doc1Range.Value2.ToString() + deptRange.Value2.ToString().PadLeft(3, '0') + UserId.PadLeft(5, '0') + dateRange.Value2.ToString() + timeRange.Value2.ToString();
                exWorksheet.Cells[23, 2] = PtId.PadLeft(9, '0') + doc2Range.Value2.ToString() + deptRange.Value2.ToString().PadLeft(3, '0') + UserId.PadLeft(5, '0') + dateRange.Value2.ToString() + timeRange.Value2.ToString();

                int i = 27;

                foreach (Item item in ItemList)
                {
                    exWorksheet.Cells[i, 1] = item.Kind;
                    exWorksheet.Cells[i, 2] = item.Name;
                    exWorksheet.Cells[i, 3] = item.Value;

                    i++;
                }

                i = 27;

                foreach (Item item in ContactList)
                {
                    exWorksheet.Cells[i, 5] = item.Kind;
                    exWorksheet.Cells[i, 6] = item.Name;
                    exWorksheet.Cells[i, 7] = item.Value;

                    i++;
                }

                i = 27;

                foreach (Item item in PatInfoList)
                {
                    exWorksheet.Cells[i, 9] = item.Kind;
                    exWorksheet.Cells[i, 10] = item.Name;
                    exWorksheet.Cells[i, 11] = item.Value;

                    i++;
                }

                i = 27;

                foreach (Item item in SumList)
                {
                    exWorksheet.Cells[i, 13] = item.Kind;
                    exWorksheet.Cells[i, 14] = item.Name;
                    exWorksheet.Cells[i, 15] = item.Value;

                    i++;
                }

                i = 27;

                foreach (Item item in AllergyList)
                {
                    exWorksheet.Cells[i, 17] = item.Kind;
                    exWorksheet.Cells[i, 18] = item.Name;
                    exWorksheet.Cells[i, 19] = item.Value;

                    i++;
                }

                string exFileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + PtId + "_" + SaveDate + SaveTime + "_" + System.IO.Path.GetFileName(FileName);

                exWorkbook.SaveAs(exFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
            finally
            {
                Marshal.ReleaseComObject(exWorksheet);
                Marshal.ReleaseComObject(exWorkbook);
                Marshal.ReleaseComObject(exApp);

                GC.Collect();
            }
        }
    }
}
