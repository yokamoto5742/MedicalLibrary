using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    /// <summary>
    /// 患者基本情報のマスター
    /// </summary>
    public class BaseInfoFixedMaster
    {
        public enum InputKind : int
        {
            TextBox = 1,
            ComboBox = 2,
            CheckBox = 3,
            DateTime = 4
        }

        /// <summary>
        /// コード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// ラベル名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 入力種別1
        /// 1: テキスト, 2: コンボボックス, 3: チェックボックス, 4: 日付
        /// </summary>
        public InputKind Kind1 = InputKind.CheckBox;

        /// <summary>
        /// チェックボックスの場合、付けるテキスト
        /// </summary>
        public string KindText1 = "";

        /// <summary>
        /// コンボボックスの場合、選択肢
        /// </summary>
        public List<string> KindItemList1 = new List<string>();

        /// <summary>
        /// カラム名
        /// </summary>
        public string DbColumnName1 = "";

        /// <summary>
        /// カラム型
        /// </summary>
        public StdDbType DbColumnType1 = StdDbType.VARCHAR2;

        /// <summary>
        /// 入力種別
        /// 1: テキスト, 2: コンボボックス（値は A～D）, 3: チェックボックス, 4: 日付
        /// </summary>
        public InputKind Kind2 = InputKind.TextBox;

        /// <summary>
        /// チェックボックスに付けるテキスト
        /// </summary>
        public string KindText2 = "";

        public List<string> KindItemList2 = new List<string>();

        /// <summary>
        /// カラム名
        /// </summary>
        public string DbColumnName2 = "";

        /// <summary>
        /// カラム型
        /// </summary>
        public StdDbType DbColumnType2 = StdDbType.VARCHAR2;


        static List<BaseInfoFixedMaster> list = new List<BaseInfoFixedMaster>();

        public static List<BaseInfoFixedMaster> List
        {
            get
            {
                if (list.Count == 0)
                {
                    BaseInfoFixedMaster m = new BaseInfoFixedMaster();
                    m.Code = "fm1";
                    m.Name = "トラブル";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "TROUBLE";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "TROUBLE_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm2";
                    m.Name = "視覚障害";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "VISUAL";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "VISUAL_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm3";
                    m.Name = "聴覚障害";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "HEARING";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "HEARING_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm4";
                    m.Name = "言語障害";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "SPEECH";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "SPEECH_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm5";
                    m.Name = "運動障害";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "MOTION";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "MOTION_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm6";
                    m.Name = "緊急度";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "EMERGENCY";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "EMERGENCY_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
//                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm7";
                    m.Name = "病名告知";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "INFORM";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "INFORM_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm8";
                    m.Name = "妊娠・授乳中";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "MATERNITY";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    m.Kind2 = InputKind.TextBox;
                    m.DbColumnName2 = "MATERNITY_TXT";
                    m.DbColumnType2 = StdDbType.VARCHAR2;
                    list.Add(m);
/*
                    m = new BaseInfoFixedMaster();
                    m.Code = "fm9";
                    m.Name = "死亡フラグ";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "死亡";
                    m.DbColumnName1 = "死亡フラグ";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm10";
                    m.Name = "死亡日";
                    m.Kind1 = InputKind.TextBox;
                    m.DbColumnName1 = "死亡日";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);
*/
                    m = new BaseInfoFixedMaster();
                    m.Code = "fm11";
                    m.Name = "独居老人";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_1";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm12";
                    m.Name = "老老介護";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_2";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm13";
                    m.Name = "透析中";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_3";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm14";
                    m.Name = "糖尿病";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_4";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm15";
                    m.Name = "抗凝血薬";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_5";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm16";
                    m.Name = "病名非告知";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_6";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm17";
                    m.Name = "かかりつけ";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_7";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);

                    m = new BaseInfoFixedMaster();
                    m.Code = "fm18";
                    m.Name = "アレルギー無し";
                    m.Kind1 = InputKind.CheckBox;
                    m.KindText1 = "有り";
                    m.DbColumnName1 = "STATUS_8";
                    m.DbColumnType1 = StdDbType.NUMBER;
                    list.Add(m);
                }

                return list;
            }
        }
    }

    /// <summary>
    /// 患者基本情報　タブマスター
    /// AMB_汎用ＴＡＢ名称マスター
    /// </summary>
    public class BaseInfoTabMaster : StdEntity
    {
        /// <summary>
        /// TabNo
        /// </summary>
        public string Code = "";

        /// <summary>
        /// Tab名称
        /// </summary>
        public string Name = "";

        static List<BaseInfoTabMaster> list = new List<BaseInfoTabMaster>();

        public static List<BaseInfoTabMaster> List
        {
            get
            {
                if (list.Count == 0)
                {
                    // マスターの取得
                    string cmd = "select * from D_GENERAL_TABMASTER " +
                        " where DISPNO = 150 " +
                        " order by TABNO";

                    List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        BaseInfoTabMaster obj = new BaseInfoTabMaster();

                        obj.Code = tmp.GetDataString("TABNO");
                        obj.Name = tmp.GetDataString("TABNAME");

                        list.Add(obj);
                    }
                }

                return list;
            }
        }
    }

    /// <summary>
    /// 患者基本情報　項目マスター
    /// AMC_汎用入力画面ＣＴＬで「画面ＮＯ=150」のデータ
    /// </summary>
    public class BaseInfoItemMaster : StdEntity
    {
        public string TabCode = "";

        public string GroupCode = "";

        /// <summary>
        /// 行表示順
        /// </summary>
        public int RowSEQ = 1;

        /// <summary>
        /// 列表示順
        /// </summary>
        public int ColSEQ = 1;

        /// <summary>
        /// パターン（現状ではすべて1）
        /// </summary>
        public int Pattern = 1;

        /// <summary>
        /// 項目名タイトル１
        /// </summary>
        public string Name1 = "";

        /// <summary>
        /// 項目名タイトル２
        /// </summary>
        public string Name2 = "";

        /// <summary>
        /// 入力タイプ
        /// 1 テキスト, 2 不明, 3 選択項目, 4 チェックボックス
        /// </summary>
        public int InputType = 1;

        /// <summary>
        /// 入力欄の幅
        /// </summary>
        public int InputWidth = 1;

        /// <summary>
        /// 入力欄の高さ
        /// </summary>
        public int InputHeight = 1;

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit = "";

        /// <summary>
        /// 選択項目のコード
        /// </summary>
        public string SelectMasterCode = "";

        /// <summary>
        /// 項目ユニークキー
        /// </summary>
        public string ItemKey = "";

        static Dictionary<string, List<BaseInfoItemMaster>> dict = new Dictionary<string, List<BaseInfoItemMaster>>();

        public static Dictionary<string, List<BaseInfoItemMaster>> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    // マスターの取得
                    string cmd = "select * from D_GENERAL_INPUTCTL " +
                        " where DISPNO = 150 " +
                        " order by TABNO, ROW_ORDER, COLUMN_ORDER";

                    List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        BaseInfoItemMaster obj = new BaseInfoItemMaster();

                        obj.TabCode = tmp.GetDataString("TABNO");
                        obj.RowSEQ = tmp.GetDataInt("ROW_ORDER");
                        obj.ColSEQ = tmp.GetDataInt("COLUMN_ORDER");
                        obj.Name1 = tmp.GetDataString("ITEM_TITLE1");
                        obj.Name2 = tmp.GetDataString("ITEM_TITLE2");
                        obj.InputType = tmp.GetDataInt("INPUT_TYPE");
                        obj.InputWidth = tmp.GetDataInt("INPUT_WIDTH");
                        obj.InputHeight = tmp.GetDataInt("INPUT_HEIGHT");
                        obj.Unit = tmp.GetDataString("UNIT");
                        obj.SelectMasterCode = tmp.GetDataString("SELECT_KEY");
                        obj.ItemKey = tmp.GetDataString("ITEM_KEY");

                        // 「入力行幅」が 0 になっているものもあるので最低 1 にする
                        if (obj.InputWidth == 0) obj.InputWidth = 1;

                        // 「入力行高」が 0 になっているものもあるので最低 1 にする
                        if (obj.InputHeight == 0) obj.InputHeight = 1;

                        if (dict.ContainsKey(obj.TabCode))
                        {
                            // 辞書にキーが存在する場合
                            List<BaseInfoItemMaster> list = dict[obj.TabCode];
                            list.Add(obj);
                        }
                        else
                        {
                            // 辞書にキーが存在しない場合
                            List<BaseInfoItemMaster> list = new List<BaseInfoItemMaster>();
                            list.Add(obj);
                            dict.Add(obj.TabCode, list);
                        }
                    }
                }

                return dict;
            }
        }

        /// <summary>
        /// 項目ユニークキーに基づいて BaseInfoItemMaster を探す
        /// </summary>
        /// <param name="item_key"></param>
        /// <returns></returns>
        public static BaseInfoItemMaster GetByItemKey(string item_key)
        {
            BaseInfoItemMaster m = new BaseInfoItemMaster();

            foreach (List<BaseInfoItemMaster> list in BaseInfoItemMaster.Dict.Values)
            {
                foreach (BaseInfoItemMaster mm in list)
                {
                    if (mm.ItemKey.Equals(item_key))
                    {
                        m = mm;
                        break;
                    }
                }

                if (m.ItemKey.Length > 0)
                {
                    break;
                }
            }

            return m;
        }
    }

    /// <summary>
    /// 患者基本情報　選択項目マスター
    /// AMB_汎用入力選択項目マスター
    /// </summary>
    public class BaseInfoSelectMaster : StdEntity
    {
        /// <summary>
        /// 選択項目キー
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 表示順
        /// </summary>
        public int ShowSEQ = 1;

        /// <summary>
        /// 選択値
        /// </summary>
        public int IntValue = 1;

        /// <summary>
        /// 選択名
        /// </summary>
        public string StringValue = "";


        public override string ToString()
        {
            return this.StringValue;
        }


        static Dictionary<string, List<BaseInfoSelectMaster>> dict = new Dictionary<string, List<BaseInfoSelectMaster>>();

        /// <summary>
        /// データベースから取得して辞書を返す
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<BaseInfoSelectMaster>> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    // マスターの取得
                    string cmd = "select * from D_GENERAL_SELECTMASTER " +
                        " order by SELECT_ITEMKEY, DISP_ORDER";

                    List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        BaseInfoSelectMaster obj = new BaseInfoSelectMaster();

                        obj.Name = tmp.GetDataString("SELECT_ITEMKEY");
                        obj.IntValue = tmp.GetDataInt("ITEM_VALUE");
                        obj.StringValue = tmp.GetDataString("ITEM_NAME");
                        obj.ShowSEQ = tmp.GetDataInt("DISP_ORDER");

                        if (dict.ContainsKey(obj.Name))
                        {
                            // 辞書にキーが存在する場合
                            List<BaseInfoSelectMaster> list = dict[obj.Name];
                            list.Add(obj);
                        }
                        else
                        {
                            // 辞書にキーが存在しない場合
                            List<BaseInfoSelectMaster> list = new List<BaseInfoSelectMaster>();
                            list.Add(obj);
                            dict.Add(obj.Name, list);
                        }
                    }
                }

                return dict;
            }
        }
    }
}
