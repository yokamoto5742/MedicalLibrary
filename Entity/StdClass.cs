using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class StdClass
    {
        public Dictionary<string, Object> DataDict = new Dictionary<string, Object>();

        /// <param name="progress">進捗の通知先（省略可）。検索開始時と1000件ごとに通知する</param>
        public static List<StdClass> GetList(DB db, string sql_command, List<StdDbColumn> param_list = null, Action<string> progress = null)
        {
            List<StdClass> list = new List<StdClass>();

            // 途中で失敗しても Close（パラメータのクリアを含む）を必ず実行し、
            // 共有の Command にパラメータが残って次の SQL まで失敗するのを防ぐ
            try
            {
                db.Open();

                db.Command.CommandText = sql_command;

                if (param_list != null)
                {
                    db.AddParameters(param_list, true);
                }

                if (progress != null)
                {
                    progress("DBを検索しています...");
                }

                using (OracleDataReader reader = db.Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StdClass obj = new StdClass();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // NULL 判定は IsDBNull で行う。ToString() が "null" になるかはドライバの実装依存で、
                            // 値そのものが "null" という文字列の列を空文字に潰してしまう危険もある。
                            if (reader.IsDBNull(i))
                            {
                                obj.DataDict[reader.GetName(i)] = "";
                                continue;
                            }

                            // GetOracleValue は呼ぶたびに新しいオブジェクトを返す（CLOB/BLOB はネイティブの
                            // LOBロケータを持つ）ため、1列につき1回だけ呼び、使い終わったら解放する。
                            // 2回呼んでいると大量件数の取得でネイティブ資源が倍のペースで枯渇する。
                            object value = reader.GetOracleValue(i);

                            obj.DataDict[reader.GetName(i)] = value.ToString();

                            IDisposable disposable = value as IDisposable;

                            if (disposable != null)
                            {
                                disposable.Dispose();
                            }
                        }

                        list.Add(obj);

                        if (progress != null && list.Count % 1000 == 0)
                        {
                            progress("検索結果を取得中 " + list.Count.ToString("#,0") + "件");
                        }
                    }
                }
            }
            finally
            {
                db.Close();
            }

            return list;
        }




        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを string 型に変換して取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public string GetDataString(string key)
        {
            string s = "";

            if (this.DataDict.ContainsKey(key))
            {
                s = this.DataDict[key].ToString();
            }

            return s;
        }


        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを int 型に変換して取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="default_val">デフォルト値</param>
        /// <returns></returns>
        public int GetDataInt(string key, int default_val = -1)
        {
            int i = default_val;

            if (this.DataDict.ContainsKey(key) && this.DataDict[key].ToString().Length > 0)
            {
                int.TryParse(this.DataDict[key].ToString(), out i);
            }

            return i;
        }



    }

    public class StdDbClass
    {
        public DB Db = DB.Db1;

        /// <summary>
        /// 対象のテーブル名。
        /// </summary>
        public string Table = "";

        /// <summary>
        /// Select 句で指定する個々のカラム。
        /// </summary>
        public List<string> SelectList = new List<string>();

        /// <summary>
        /// Update/Insert で登録するデータのリスト。
        /// </summary>
        public List<StdDbColumn> DataList = new List<StdDbColumn>();

        /// <summary>
        /// Where 句で指定する個々の条件。Update/Delete では必須。
        /// </summary>
        public List<string> WhereList = new List<string>();

        /// <summary>
        /// パラメータ登録するデータのリスト。
        /// </summary>
        public List<StdDbColumn> ParamList = new List<StdDbColumn>();

        /// <summary>
        /// Where 句。Update/Delete では必須。
        /// </summary>
        public string WhereState
        {
            get
            {
                string s = "";

                foreach (string ss in WhereList)
                {
                    if (s.Length > 0)
                    {
                        s += " and ";
                    }

                    s += ss;
                }

                if (s.Length > 0)
                {
                    s = " where " + s;
                }

                return s;
            }
        }

        /// <summary>
        /// Order By 句で指定する個々の条件。Select の時のみ。
        /// </summary>
        public List<string> OrderByList = new List<string>();

        /// <summary>
        /// Group By 句で指定する個々の条件。Select の時のみ。
        /// </summary>
        public List<string> GroupByList = new List<string>();

        public StdDbClass()
        {
        }

        public StdDbClass(DB db)
        {
            this.Db = db;
        }


        /// <summary>
        /// Update SQL
        /// </summary>
        /// <param name="execute">true: 実行する, false: 実行しない</param>
        /// <param name="mode">0: null も登録, 1: null なら無視</param>
        /// <param name="close">true: close 実行, false: close しない</param>
        /// <returns></returns>
        public StdReturn UpdateSQL(bool execute = true, int mode = 0, bool close = true)
        {
            StdReturn sr = new StdReturn();

            if (this.Table.Length == 0)
            {
                sr.Errs.Add("テーブルの指定がありません");
            }

            if (this.WhereState.Length == 0)
            {
                sr.Errs.Add("更新対象レコードを指定する Where 文がありません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            string cmd = "";
            string ups = "";

            foreach (StdDbColumn obj in this.DataList)
            {
                if (mode == 0 || !obj.IsEmpty)
                {
                    if (ups.Length > 0)
                    {
                        ups += ",";
                    }

                    if (obj.DataType == StdDbType.TEXT)
                    {
                        // TEXT のときはパラメータ化せず、そのまま追加
                        ups += obj.Name + " = " + obj.Value;
                    }
                    else
                    {
                        // TEXT 以外のときはパラメータ化する
                        ups += obj.Name + " = :" + obj.Name;
                    }
                }
            }

            if (ups.Length == 0)
            {
                sr.Errs.Add("更新対象のカラムがありません");
                return sr;
            }

            cmd = "update " + this.Table + " set " + ups + this.WhereState;

            Db.Command.CommandText = cmd;
            Db.AddParameters(this.DataList, mode != 0);
            Db.AddParameters(this.ParamList, true);

            if (execute)
            {
                sr.IntValue = Db.ExecuteCommand(close);
            }

            sr.Msgs.Add(cmd);
            return sr;
        }

        /// <summary>
        /// Insert SQL
        /// </summary>
        /// <param name="execute">true: 実行する, false: 実行しない</param>
        /// <param name="mode">0: null も登録, 1: null なら無視</param>
        /// <param name="close">true: close 実行, false: close しない</param>
        /// <returns></returns>
        public StdReturn InsertSQL(bool execute = true, int mode = 0, bool close = true)
        {
            StdReturn sr = new StdReturn();

            if (this.Table.Length == 0)
            {
                sr.Errs.Add("テーブルの指定がありません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            string cmd = "";
            string cols = "";
            string vals = "";

            foreach (StdDbColumn obj in this.DataList)
            {
                if (mode == 0 || !obj.IsEmpty)
                {
                    if (cols.Length > 0)
                    {
                        cols += ",";
                        vals += ",";
                    }

                    cols += obj.Name;

                    if (obj.DataType == StdDbType.TEXT)
                    {
                        // TEXT のときはパラメータ化せず、そのまま追加
                        vals += obj.Value;
                    }
                    else
                    {
                        // TEXT 以外のときはパラメータ化する
                        vals += ":" + obj.Name;
                    }
                }
            }

            if (cols.Length == 0 || vals.Length == 0)
            {
                sr.Errs.Add("挿入対象のカラムがありません");
                return sr;
            }

            cmd = "insert into " + this.Table + " (" + cols + ") values (" + vals + ")";

            Db.Command.CommandText = cmd;
            Db.AddParameters(this.DataList, mode != 0);
            Db.AddParameters(this.ParamList, true);

            if (execute)
            {
                sr.IntValue = Db.ExecuteCommand(close);
            }

            sr.Msgs.Add(cmd);
            return sr;
        }

        /// <summary>
        /// key_list を条件に Update し、対象が無ければ key_list を加えて Insert する。
        /// key_list は数値の列のみ（Where 句に値をそのまま埋め込むため）。
        /// </summary>
        /// <param name="key_list">レコードを特定するキー</param>
        /// <returns></returns>
        internal StdReturn UpdateOrInsert(List<StdDbColumn> key_list)
        {
            foreach (StdDbColumn key in key_list)
            {
                this.WhereList.Add(key.Name + " = " + key.Value);
            }

            StdReturn sr = this.UpdateSQL();

            if (sr.ErrExist || sr.IntValue > 0)
            {
                return sr;
            }

            // update 対象が無ければ新規登録
            this.DataList.AddRange(key_list);
            return this.InsertSQL();
        }

    }


    /// <summary>
    /// SQL文に含まれるカラム名とオブジェクト値のセット
    /// </summary>
    public class StdDbColumn
    {
        /// <summary>
        /// カラム名
        /// </summary>
        public string Name = "";

        /// <summary>
        /// データ型
        /// </summary>
        public StdDbType DataType = StdDbType.VARCHAR2;

        /// <summary>
        /// 長さ
        /// </summary>
        public int Length = 1;

        /// <summary>
        /// 小数点以下
        /// </summary>
        public int Scale = 0;

        /// <summary>
        /// Nullable
        /// </summary>
        public bool Nullable = true;

        /// <summary>
        /// 値
        /// </summary>
        public Object Value = new object();

        /// <summary>
        /// 値が null・DBNull・空文字のいずれかであれば true
        /// </summary>
        internal bool IsEmpty
        {
            get
            {
                return Value == null || Value == DBNull.Value || Value.ToString().Length == 0;
            }
        }


        public StdDbColumn()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data_type"></param>
        /// <param name="value"></param>
        public StdDbColumn(string name, StdDbType data_type, Object value)
        {
            this.Name = name;
            this.DataType = data_type;
            this.Value = value;
        }

        public StdDbColumn(string name, StdDbType data_type, int length, int scale, bool nullable)
        {
            this.Name = name;
            this.DataType = data_type;
            this.Length = length;
            this.Scale = scale;
            this.Nullable = nullable;
        }
    }

    /// <summary>
    /// データ型
    /// </summary>
    public enum StdDbType : int
    {
        TEXT = 0,
        NUMBER = 1,
        CHAR = 2,
        VARCHAR2 = 3,
        DATE = 4
    }


    /// <summary>
    /// メソッドの戻り値として使う標準クラス。
    /// </summary>
    public class StdReturn
    {
        /// <summary>
        /// エラーのリスト
        /// </summary>
        public List<string> Errs = new List<string>();

        /// <summary>
        /// エラーの有無
        /// </summary>
        public bool ErrExist
        {
            get
            {
                return this.Errs.Count > 0;
            }
        }

        /// <summary>
        /// メッセージのリスト
        /// </summary>
        public List<string> Msgs = new List<string>();

        /// <summary>
        /// 戻り値。整数型
        /// </summary>
        public int IntValue = 0;

    }
}
