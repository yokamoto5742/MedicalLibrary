using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PatBoard : StdKarte1
    {
        /// <summary>
        /// 掲示連番
        /// </summary>
        public int SEQ1 = 0;

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ2 = 0;

        /// <summary>
        /// 掲示区分
        /// 1: オーダー, 2: 基本指示, 3: 看護指示, 4: 手術指示
        /// </summary>
        public string KindCode1 = "";

        /// <summary>
        /// 掲示区分
        /// 1: オーダー, 2: 基本指示, 3: 看護指示, 4: 手術指示
        /// </summary>
        public string KindName1
        {
            get
            {
                string s = "";

                if (this.KindCode1.Equals("1"))
                {
                    s = "オーダー";
                }
                else if (this.KindCode1.Equals("2"))
                {
                    s = "基本指示";
                }
                else if (this.KindCode1.Equals("3"))
                {
                    s = "看護指示";
                }
                else if (this.KindCode1.Equals("4"))
                {
                    s = "手術指示";
                }

                return s;
            }
        }

        /// <summary>
        /// 掲示区分名から掲示区分コードを取得する
        /// 1: オーダー, 2: 基本指示, 3: 看護指示, 4: 手術指示
        /// </summary>
        /// <param name="kind_name"></param>
        /// <returns></returns>
        public static string GetKindCodeFromName(string kind_name)
        {
            string s = "";

            if (kind_name.Equals("オーダー"))
            {
                s = "1";
            }
            else if (kind_name.Equals("基本指示"))
            {
                s = "2";
            }
            else if (kind_name.Equals("看護指示"))
            {
                s = "3";
            }
            else if (kind_name.Equals("手術指示"))
            {
                s = "4";
            }

            return s;
        }

        /// <summary>
        /// 記事
        /// </summary>
        public string Cont1 = "";

        /// <summary>
        /// 所属
        /// </summary>
        public string RegStaffSectionName
        {
            get
            {
                string s = "";
                
                if (Dict.StaffDict.ContainsKey(this.RegStaffCode))
                {
                    s = Dict.StaffDict[RegStaffCode].SectionFullName;
                }

                return s;
            }
        }

        /// <summary>
        /// 所属
        /// </summary>
        public string UpStaffSectionName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(UpStaffCode))
                {
                    s = Dict.StaffDict[UpStaffCode].SectionFullName;
                }

                return s;
            }
        }

        /// <summary>
        /// 削除フラグ
        /// 0: 非削除, 1: 削除
        /// </summary>
        public int DeleteFlg = 0;

        /// <summary>
        /// 指示受けフラグ
        /// 0: 未確認, 1: 確認, 2: 完了
        /// </summary>
        public string StatusCode = "0";

        /// <summary>
        /// 指示受け
        /// </summary>
        public string StatusName
        {
            get
            {
                string s = "";

                if (this.StatusCode.Equals("0"))
                {
                    s = "未";
                }
                else if (this.StatusCode.Equals("1"))
                {
                    s = "確認";
                }
                else if (this.StatusCode.Equals("2"))
                {
                    s = "完了";
                }

                return s;
            }
        }

        public static Dictionary<string, List<PatBoard>> GetDict(string pt_id)
        {
            Dictionary<string, List<PatBoard>> dict = new Dictionary<string, List<PatBoard>>();

            if (pt_id.Length == 0)
            {
                return dict;
            }

            /*
            string cmd = "select * from macs.ADT_患者掲示板データ t" +
                " where t.患者コード = " + pt_id +
                " order by t.掲示連番 desc, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);
             */

            StdDbClass tmp_db = new StdDbClass(Db);

            tmp_db.Table = "ADT_患者掲示板データ";
            tmp_db.WhereList.Add("患者コード = " + pt_id);
            tmp_db.OrderByList.Add("掲示連番 desc");
            tmp_db.OrderByList.Add("連番");

            List<StdClass> tmp_list = tmp_db.SelectSQL();

            foreach (StdClass tmp in tmp_list)
            {
                PatBoard obj = new PatBoard();

                obj.PtId = tmp.DataDict["患者コード"].ToString();
                int.TryParse(tmp.DataDict["掲示連番"].ToString(), out obj.SEQ1);
                int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ2);
                obj.KindCode1 = tmp.DataDict["掲示区分"].ToString();
                obj.Cont1 = tmp.DataDict["記事"].ToString();
                int.TryParse(tmp.DataDict["削除フラグ"].ToString(), out obj.DeleteFlg);
                int.TryParse(tmp.DataDict["登録日"].ToString(), out obj.RegDate);
                int.TryParse(tmp.DataDict["登録時間"].ToString(), out obj.RegTime);
                obj.RegStaffCode = tmp.DataDict["登録者"].ToString();
                obj.RegStaffCode2 = tmp.DataDict["代行登録者"].ToString();
                int.TryParse(tmp.DataDict["更新日"].ToString(), out obj.UpDate);
                int.TryParse(tmp.DataDict["更新時間"].ToString(), out obj.UpTime);
                obj.UpStaffCode = tmp.DataDict["更新者"].ToString();
                obj.UpStaffCode2 = tmp.DataDict["代行更新者"].ToString();
                obj.StatusCode = tmp.DataDict["指示受けフラグ"].ToString();

                if (dict.ContainsKey(obj.SEQ1.ToString()))
                {
                    dict[obj.SEQ1.ToString()].Add(obj);
                }
                else
                {
                    List<PatBoard> list = new List<PatBoard>();
                    list.Add(obj);
                    dict.Add(obj.SEQ1.ToString(), list);
                }
            }

            return dict;
        }

        /// <summary>
        /// 指定した患者の掲示連番の最大値を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <returns></returns>
        public static int GetMaxSEQ1(string pt_id)
        {
            int n = 0;

            string cmd = "select max(掲示連番) M from macs.ADT_患者掲示板データ " +
                " where 患者コード = " + pt_id;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["M"].ToString(), out n);
            }

            return n;
        }

        /// <summary>
        /// 指定した患者・掲示連番の連番の最大値を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="seq1"></param>
        /// <returns></returns>
        public static int GetMaxSEQ2(string pt_id, int seq1)
        {
            int n = 0;

            string cmd = "select max(連番) M from macs.ADT_患者掲示板データ " +
                " where 患者コード = " + pt_id + " and 掲示連番 = " + seq1;

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["M"].ToString(), out n);
            }

            return n;
        }

        /// <summary>
        /// 指定した患者・掲示連番の記事数を取得する
        /// </summary>
        /// <param name="pt_id"></param>
        /// <param name="seq1"></param>
        /// <returns></returns>
        public static int GetCount(string pt_id, int seq1)
        {
            int n = 0;

            string cmd = "select max(連番) M from macs.ADT_患者掲示板データ " +
                " where 患者コード = " + pt_id + " and 掲示連番 = " + seq1 +
                " and 削除フラグ = 0";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["M"].ToString(), out n);
            }

            return n;
        }

        /// <summary>
        /// 保存する
        /// </summary>
        public StdReturn OrderSave()
        {
            StdReturn sr = new StdReturn();
/*
            StdDbClass obj = StdDbClass.Load(Db, "ADT_患者掲示板データ");

            obj.DataDict["患者コード"].Value = this.PtId;
            obj.DataDict["掲示連番"].Value = this.SEQ1;
            obj.DataDict["連番"].Value = this.SEQ2;
            obj.DataDict["掲示区分"].Value = this.KindCode1;
            obj.DataDict["記事"].Value = this.Cont1;
            obj.DataDict["削除フラグ"].Value = this.DeleteFlg;
            obj.DataDict["更新日"].Value = DateTime.Now.ToString("yyyyMMdd");
            obj.DataDict["更新時間"].Value = DateTime.Now.ToString("HHmmss");
            obj.DataDict["更新者"].Value = LoginUser.Id;
            obj.DataDict["代行更新者"].Value = "";
            obj.DataDict["指示受けフラグ"].Value = this.DoCode;
*/
            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_患者掲示板データ";

            obj.DataList.Add(new StdDbColumn("掲示区分", StdDbType.NUMBER, this.KindCode1));
            obj.DataList.Add(new StdDbColumn("記事", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, this.DeleteFlg));
            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));


            if (this.SEQ1 > 0)
            {
                // 既存指示の変更

                if (!this.StatusCode.Equals("2"))
                {
                    // 完了でない場合は、既存の記事数で決まる

                    if (PatBoard.GetCount(this.PtId, this.SEQ1) > 1)
                    {
                        this.StatusCode = "1";
                    }
                    else
                    {
                        this.StatusCode = "0";
                    }
                }

                obj.DataList.Add(new StdDbColumn("指示受けフラグ", StdDbType.NUMBER, this.StatusCode));

                obj.WhereList.Add("患者コード = " + this.PtId);
                obj.WhereList.Add("掲示連番 = " + this.SEQ1);
                obj.WhereList.Add("連番 = 1");

                sr = obj.UpdateSQL();
            }
            else
            {
                // 新規指示の登録

                // 患者コード・掲示連番・連番を指定する
                this.SEQ1 = PatBoard.GetMaxSEQ1(this.PtId) + 1;
                this.StatusCode = "0";

                obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                obj.DataList.Add(new StdDbColumn("掲示連番", StdDbType.NUMBER, this.SEQ1));
                obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, 1));

                obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));
                obj.DataList.Add(new StdDbColumn("指示受けフラグ", StdDbType.NUMBER, this.StatusCode));

                sr = obj.InsertSQL();
            }

            // 指示受けフラグは、同一の掲示連番の全レコードに対して変更する
            string cmd = "update macs.ADT_患者掲示板データ set 指示受けフラグ = " + this.StatusCode +
                " where 患者コード = " + this.PtId + " and 掲示連番 = " + this.SEQ1;

            Db.ExecuteNonQuery(cmd);
            sr.Msgs.Add(cmd);

            return sr;
        }

        /// <summary>
        /// 指示を削除する
        /// </summary>
        /// <returns></returns>
        public StdReturn OrderDelete()
        {
            StdReturn sr = new StdReturn();

            if (this.SEQ1 == 0)
            {
                sr.Errs.Add("削除する指示が指定されていません");
                return sr;
            }
/*
            // すでに返信が入っている場合は削除できない
            if (PatBoard.GetCount(this.PtId, this.SEQ1) > 1)
            {
                return sr;
            }
*/
            this.DeleteFlg = 1;

            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_患者掲示板データ";

            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, this.DeleteFlg));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("掲示連番 = " + this.SEQ1);
            obj.WhereList.Add("連番 = 1");

            sr = obj.UpdateSQL();

            return sr;
        }

        /// <summary>
        /// 返信を保存する
        /// </summary>
        public StdReturn ReplySave()
        {
            StdReturn sr = new StdReturn();

            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_患者掲示板データ";

            obj.DataList.Add(new StdDbColumn("掲示区分", StdDbType.NUMBER, this.KindCode1));
            obj.DataList.Add(new StdDbColumn("記事", StdDbType.VARCHAR2, this.Cont1));
            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, this.DeleteFlg));
            obj.DataList.Add(new StdDbColumn("更新日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
            obj.DataList.Add(new StdDbColumn("更新時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
            obj.DataList.Add(new StdDbColumn("更新者", StdDbType.NUMBER, LoginUser.Id));
            obj.DataList.Add(new StdDbColumn("代行更新者", StdDbType.NUMBER, LoginUser.Id2));

            if (!this.StatusCode.Equals("2"))
            {
                this.StatusCode = "1";
            }

            obj.DataList.Add(new StdDbColumn("指示受けフラグ", StdDbType.NUMBER, this.StatusCode));


            if (this.SEQ1 > 0)
            {
                if (this.SEQ2 > 1)
                {
                    // 既存返信の変更

                    obj.WhereList.Add("患者コード = " + this.PtId);
                    obj.WhereList.Add("掲示連番 = " + this.SEQ1);
                    obj.WhereList.Add("連番 = " + this.SEQ2);

                    sr = obj.UpdateSQL();
                }
                else
                {
                    // 新規返信の登録

                    // 患者コード・掲示連番・連番を指定する
                    this.SEQ2 = PatBoard.GetMaxSEQ2(this.PtId, this.SEQ1) + 1;

                    obj.DataList.Add(new StdDbColumn("患者コード", StdDbType.NUMBER, this.PtId));
                    obj.DataList.Add(new StdDbColumn("掲示連番", StdDbType.NUMBER, this.SEQ1));
                    obj.DataList.Add(new StdDbColumn("連番", StdDbType.NUMBER, this.SEQ2));

                    obj.DataList.Add(new StdDbColumn("登録日", StdDbType.NUMBER, DateTime.Now.ToString("yyyyMMdd")));
                    obj.DataList.Add(new StdDbColumn("登録時間", StdDbType.NUMBER, DateTime.Now.ToString("HHmmss")));
                    obj.DataList.Add(new StdDbColumn("登録者", StdDbType.NUMBER, LoginUser.Id));
                    obj.DataList.Add(new StdDbColumn("代行登録者", StdDbType.NUMBER, LoginUser.Id2));

                    sr = obj.InsertSQL();
                }
            }

            // 指示受けフラグは、同一の掲示連番の全レコードに対して変更する
            string cmd = "update macs.ADT_患者掲示板データ set 指示受けフラグ = " + this.StatusCode +
                " where 患者コード = " + this.PtId + " and 掲示連番 = " + this.SEQ1;

            Db.ExecuteNonQuery(cmd);

            sr.Msgs.Add(cmd);
            return sr;
        }

        /// <summary>
        /// 返信を削除する
        /// </summary>
        /// <returns></returns>
        public StdReturn ReplyDelete()
        {
            StdReturn sr = new StdReturn();

            if (this.SEQ1 == 0)
            {
                sr.Errs.Add("削除する返信の掲示連番が指定されていません");
            }

            if (this.SEQ2 < 2)
            {
                sr.Errs.Add("返信ではなく指示を削除しようとしています");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            this.DeleteFlg = 1;

            StdDbClass obj = new StdDbClass();

            obj.Table = "ADT_患者掲示板データ";

            obj.DataList.Add(new StdDbColumn("削除フラグ", StdDbType.NUMBER, this.DeleteFlg));

            obj.WhereList.Add("患者コード = " + this.PtId);
            obj.WhereList.Add("掲示連番 = " + this.SEQ1);
            obj.WhereList.Add("連番 = " + this.SEQ2);

            sr = obj.UpdateSQL();

            // 完了でない場合
            // 返信を削除したことによって、返信が無くなった（＝指示のみ）場合は、「指示受けフラグ = 0」とする
            if (!this.StatusCode.Equals("2"))
            {
                if (PatBoard.GetCount(this.PtId, this.SEQ1) < 2)
                {
                    string cmd = "update macs.ADT_患者掲示板データ set 指示受けフラグ = 0 " +
                        " where 患者コード = " + this.PtId + " and 掲示連番 = " + this.SEQ1;

                    Db.ExecuteNonQuery(cmd);

                    sr.Msgs.Add(cmd);
                }
            }

            return sr;
        }
    }
}
