using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 手術指示データ
    /// </summary>
    public class PatOpeOrder
    {
        PatIn _Pat = new PatIn();

        public PatIn Pat
        {
            get
            {
                return this._Pat;
            }
        }

        /// <summary>
        /// 日付
        /// </summary>
        public int Date = 0;

        /// <summary>
        /// データ辞書
        /// </summary>
        public Dictionary<string, PatOpeOrderData> DictData = new Dictionary<string, PatOpeOrderData>();

        public string DictDataString
        {
            get
            {
                string s = "";

                foreach (PatOpeOrderData data in this.DictData.Values)
                {
                    if (data.Title.Length > 0)
                    {
                        s += "\r\n【" + data.Title + "】　" + data.Text + "\r\n";
                    }
                    else
                    {
                        s += "　　　" + data.Text + "\r\n";
                    }
                }

                s = s.TrimStart('\r', '\n');

                return s;
            }
        }

        /// <summary>
        /// 場所（中央・南館）
        /// </summary>
        public string OpePlace
        {
            get
            {
                string result = "";

                if (DictData.ContainsKey("12-35"))
                {
                    result = DictData["12-35"].Text;

                    // 旧来にならって「中央」「南館」いずれかに変更
                    if (result.Contains("本館"))
                    {
                        result = "中央";
                    }
                    else if (result.Contains("アイセンター"))
                    {
                        result = "南館";
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 入外
        /// </summary>
        public string InOut
        {
            get
            {
                string result = "";

                if (this.InOutString.Equals("外来"))
                {
                    result = "1";
                }
                else if (this.InOutString.Equals("入院"))
                {
                    result = "2";
                }

                return result;
            }
        }

        /// <summary>
        /// 入外
        /// </summary>
        public string InOutString
        {
            get
            {
                string result = "";

                if (DictData.ContainsKey("12-36"))
                {
                    result = DictData["12-36"].Text;
                }

                return result;
            }
        }

        /// <summary>
        /// 入外
        /// </summary>
        public string InOutStringShort
        {
            get
            {
                string result = "";

                if (this.InOut.Equals("1"))
                {
                    result = "外";
                }
                else if (this.InOut.Equals("2"))
                {
                    result = "入";
                }

                return result;
            }
        }

        /// <summary>
        /// 入外
        /// </summary>
        public string InOutMark
        {
            get
            {
                string result = "";

                if (this.InOut.Equals("1"))
                {
                    result = "★";
                }

                return result;
            }
        }

        /// <summary>
        /// 術者
        /// </summary>
        public string OpeDoctor
        {
            get
            {
                string result = "";

                if (DictData.ContainsKey("12-19"))
                {
                    result = DictData["12-19"].Text;
                }

                return result;
            }
        }

        /// <summary>
        /// 術式
        /// </summary>
        public string OpeName
        {
            get
            {
                string result = "";

                if (DictData.ContainsKey("12-1"))
                {
                    result = DictData["12-1"].Text;
                }

                return result;
            }
        }

        /// <summary>
        /// 麻酔
        /// </summary>
        public string OpeAnes
        {
            get
            {
                string result = "";

                if (DictData.ContainsKey("12-21"))
                {
                    result = DictData["12-21"].Text.Replace("（↓部位）", "");
                }

                return result;
            }
        }

        public PatOpeOrder()
        {
        }

        public PatOpeOrder(StdClass tmp)
        {
            this.Pat.Id = tmp.GetDataString("P_ID");
            this.Pat.Name = tmp.GetDataString("P_NAME").Trim();
            this.Pat.Sex = tmp.GetDataString("P_SEX");
            this.Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
            this.Date = tmp.GetDataInt("DIRECTION_DATE", 0);
        }

        /// <summary>
        /// 当該患者の当該日の手術指示データを取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static PatOpeOrder Load(string pt_id, int date)
        {
            PatOpeOrder ope = new PatOpeOrder();

            if (pt_id.Length == 0 || date.ToString().Length != 8)
            {
                return ope;
            }

            ope.Date = date;

            string cmd = "select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td, M_PATIENT tm " +
                " where td.P_ID = " + pt_id + " and DIRECTION_DATE = " + date +
                " and CATEGORY_TYPE = 12 " +
                " and td.P_ID = tm.P_ID " +
                " order by td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                if (!ope.DictData.ContainsKey(data.KeyCode))
                {
                    ope.DictData.Add(data.KeyCode, data);
                }
            }

            return ope;
        }

        /// <summary>
        /// 当該日の手術指示データ一覧を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatOpeOrder> Load(int date)
        {
            List<PatOpeOrder> list = new List<PatOpeOrder>();

            if (date.ToString().Length != 8)
            {
                return list;
            }
            string cmd = "Select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td inner join M_PATIENT tm on td.P_ID = tm.P_ID " +
                " where DIRECTION_DATE = " + date +
                " and CATEGORY_TYPE = 12 " +
                " order by td.P_ID, td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                bool flg = false;

                PatOpeOrder ope = new PatOpeOrder(tmp);
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                foreach (PatOpeOrder p in list)
                {
                    if (p.Pat.Id.Equals(ope.Pat.Id) && p.Date.Equals(ope.Date))
                    {
                        if (!p.DictData.ContainsKey(data.KeyCode))
                        {
                            p.DictData.Add(data.KeyCode, data);
                        }

                        flg = true;
                        break;
                    }
                }

                if (!flg)
                {
                    ope.DictData.Add(data.KeyCode, data);
                    list.Add(ope);
                }
            }

            return list;
        }

        /// <summary>
        /// 当該日の手術指示データ一覧を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pt_list"></param>
        /// <returns></returns>
        public static List<PatOpeOrder> Load(int date, List<string> pt_list)
        {
            List<PatOpeOrder> list = new List<PatOpeOrder>();

            if (date.ToString().Length != 8 || pt_list.Count == 0 || AppString.ConcatList(pt_list, ",").Length == 0)
            {
                return list;
            }
            string cmd = "Select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td inner join M_PATIENT tm on td.P_ID = tm.P_ID " +
                " where DIRECTION_DATE = " + date +
                " and CATEGORY_TYPE = 12 " +
                " and td.P_ID in (" + AppString.ConcatList(pt_list, ",") + ") " +
                " order by td.P_ID, td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                bool flg = false;

                PatOpeOrder ope = new PatOpeOrder(tmp);
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                foreach (PatOpeOrder p in list)
                {
                    if (p.Pat.Id.Equals(ope.Pat.Id) && p.Date.Equals(ope.Date))
                    {
                        if (!p.DictData.ContainsKey(data.KeyCode))
                        {
                            p.DictData.Add(data.KeyCode, data);
                        }

                        flg = true;
                        break;
                    }
                }

                if (!flg)
                {
                    ope.DictData.Add(data.KeyCode, data);
                    list.Add(ope);

                    pt_list.Add(ope.Pat.Id);
                }
            }

            return list;
        }

        /// <summary>
        /// 当該期間の手術指示データ一覧を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<PatOpeOrder> Load(int start_date, int end_date)
        {
            List<PatOpeOrder> list = new List<PatOpeOrder>();

            if (start_date.ToString().Length != 8 || end_date.ToString().Length != 8)
            {
                return list;
            }
            string cmd = "Select td.*, tm.P_NAME, tm.P_SEX, tm.P_BIRTHDAY_AD " +
                " from D_DIRECTION td inner join M_PATIENT tm on td.P_ID = tm.P_ID " +
                " where DIRECTION_DATE >= " + start_date + " and DIRECTION_DATE <= " + end_date +
                " and CATEGORY_TYPE = 12 " +
                " order by td.DIRECTION_DATE, td.P_ID, td.SEQ";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
            foreach (StdClass tmp in tmp_list)
            {
                bool flg = false;

                PatOpeOrder ope = new PatOpeOrder(tmp);
                PatOpeOrderData data = new PatOpeOrderData(tmp);

                foreach (PatOpeOrder p in list)
                {
                    if (p.Pat.Id.Equals(ope.Pat.Id) && p.Date.Equals(ope.Date))
                    {
                        if (!p.DictData.ContainsKey(data.KeyCode))
                        {
                            p.DictData.Add(data.KeyCode, data);
                        }

                        flg = true;
                        break;
                    }
                }

                if (!flg)
                {
                    ope.DictData.Add(data.KeyCode, data);
                    list.Add(ope);
                }
            }

            return list;
        }
    }

    /// <summary>
    /// 個々の手術指示データ
    /// </summary>
    public class PatOpeOrderData
    {
        /// <summary>
        /// 指示コード - 連番
        /// </summary>
        public string KeyCode = "";

        /// <summary>
        /// マスターから取得した項目名
        /// </summary>
        public string Title = "";

        /// <summary>
        /// マスターから取得した値 + 入力されているデータ
        /// </summary>
        public string Text = "";

        public PatOpeOrderData()
        {
        }

        public PatOpeOrderData(StdClass tmp)
        {
            this.KeyCode = tmp.GetDataString("CATEGORY_TYPE") + "-" + tmp.GetDataString("SEQ");
            this.Title = tmp.GetDataString("DIRECTION_TITLE");
            this.Text = tmp.GetDataString("DIRECTION_DISP").Replace(Environment.NewLine, " ").Trim();
        }
    }
}
