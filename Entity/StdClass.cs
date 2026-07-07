using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class StdClass
    {
        public Dictionary<string, Object> DataDict = new Dictionary<string, Object>();

        public static List<StdClass> GetList(DB db, string sql_command, List<StdDbColumn> param_list = null)
        {
            List<StdClass> list = new List<StdClass>();

            db.Open();

            db.Command.CommandText = sql_command;

            if (param_list != null)
            {
                foreach (StdDbColumn obj in param_list)
                {
                    if (obj.Value.ToString().Length > 0)
                    {
                        if (obj.DataType == StdDbType.NUMBER)
                        {
                            if (obj.Value != null && obj.Value.ToString().Length > 0)
                            {
                                db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                            }
                            else
                            {
                                db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                            }
                        }
                        else if (obj.DataType == StdDbType.VARCHAR2)
                        {
                            db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.CHAR)
                        {
                            db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.DATE)
                        {
                            db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                        }
                    }
                }
            }

            OracleDataReader reader = db.Command.ExecuteReader();

            while (reader.Read())
            {
                StdClass obj = new StdClass();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetOracleValue(i).ToString() != "null")
                    {
                        obj.DataDict[reader.GetName(i)] = reader.GetOracleValue(i).ToString();
                    }
                    else
                    {
                        obj.DataDict[reader.GetName(i)] = "";
                    }
                }

                list.Add(obj);
            }

            reader.Close();
            db.Close();

            return list;
        }


        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public Object GetData(string key)
        {
            Object obj = new object();

            if (this.DataDict.ContainsKey(key))
            {
                obj = this.DataDict[key];
            }

            return obj;
        }


        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを指定された型に変換して取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="t">変換先の型</param>
        /// <returns></returns>
        public Object GetData(string key, Type t)
        {
            Object obj = new object();

            try
            {
                if (this.DataDict.ContainsKey(key))
                {
                    obj = Convert.ChangeType(this.DataDict[key], t);
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }

            return obj;
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


        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを long 型に変換して取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="default_val">デフォルト値</param>
        /// <returns></returns>
        public long GetDataLong(string key, long default_val = -1)
        {
            long i = default_val;

            if (this.DataDict.ContainsKey(key) && this.DataDict[key].ToString().Length > 0)
            {
                long.TryParse(this.DataDict[key].ToString(), out i);
            }

            return i;
        }


        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを double 型に変換して取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="default_val">デフォルト値</param>
        /// <returns></returns>
        public double GetDataDouble(string key, double default_val = -1)
        {
            double i = default_val;

            if (this.DataDict.ContainsKey(key) && this.DataDict[key].ToString().Length > 0)
            {
                double.TryParse(this.DataDict[key].ToString(), out i);
            }

            return i;
        }


        /// <summary>
        /// DataDict から指定されたキーに該当するオブジェクトを float 型に変換して取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="default_val">デフォルト値</param>
        /// <returns></returns>
        public float GetDataFloat(string key, float default_val = -1)
        {
            float i = default_val;

            if (this.DataDict.ContainsKey(key) && this.DataDict[key].ToString().Length > 0)
            {
                float.TryParse(this.DataDict[key].ToString(), out i);
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
        /// Select 句。Select では必須。
        /// </summary>
        public string SelectState
        {
            get
            {
                string s = "";

                foreach (string ss in SelectList)
                {
                    if (s.Length > 0)
                    {
                        s += ", ";
                    }

                    s += ss;
                }

                if (s.Length > 0)
                {
                    s = "select " + s + " ";
                }
                else
                {
                    s = "select * ";
                }

                return s;
            }
        }

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
        /// Order By 句。Select の時のみ。
        /// </summary>
        public string OrderByState
        {
            get
            {
                string s = "";

                foreach (string ss in OrderByList)
                {
                    if (s.Length > 0)
                    {
                        s += ", ";
                    }

                    s += ss;
                }

                if (s.Length > 0)
                {
                    s = " order by " + s;
                }

                return s;
            }
        }

        /// <summary>
        /// Group By 句で指定する個々の条件。Select の時のみ。
        /// </summary>
        public List<string> GroupByList = new List<string>();

        /// <summary>
        /// Group By 句。Select の時のみ。
        /// </summary>
        public string GroupByState
        {
            get
            {
                string s = "";

                foreach (string ss in GroupByList)
                {
                    if (s.Length > 0)
                    {
                        s += ", ";
                    }

                    s += ss;
                }

                if (s.Length > 0)
                {
                    s = " group by " + s;
                }

                return s;
            }
        }

        public StdDbClass()
        {
        }

        public StdDbClass(DB db)
        {
            this.Db = db;
        }


        /// <summary>
        /// テーブルの構造を取得してクラスを生成
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static StdDbClass Load(DB db, string table_name)
        {
            StdDbClass obj = new StdDbClass();
            obj.Table = table_name;

            List<StdDbColumn> tmp_list = StdDbColumn.GetList(db, table_name);

            foreach (StdDbColumn tmp in tmp_list)
            {
//                obj.DataDict.Add(tmp.Name, tmp);
                obj.DataList.Add(tmp);
            }

            return obj;
        }

        /// <summary>
        /// Select SQL
        /// </summary>
        /// <param name="execute">true: 実行する, false: 実行しない</param>
        /// <param name="max">Where 指定が無い場合に取得するデータの最大値</param>
        /// <param name="close">true: close 実行, false: close しない</param>
        /// <returns></returns>
        public List<StdClass> SelectSQL(bool execute = true, int max = 1000, bool close = true)
        {
            List<StdClass> list = new List<StdClass>();

            StdReturn sr = new StdReturn();

            if (this.Table.Length == 0)
            {
                sr.Errs.Add("テーブルの指定がありません");
            }

            if (sr.ErrExist)
            {
                return list;
            }

            string where_state = this.WhereState;

            if (this.WhereState.Length == 0)
            {
                if (max > 0)
                {
                    where_state = " where rownum <= " + max;
                }
            }

            string cmd = this.SelectState + " from " + this.Table + where_state + this.OrderByState + this.GroupByState;

            Db.Command.CommandText = cmd;

            foreach (StdDbColumn obj in this.ParamList)
            {
                if (obj.Value.ToString().Length > 0)
                {
                    if (obj.DataType == StdDbType.NUMBER)
                    {
                        if (obj.Value != null && obj.Value.ToString().Length > 0)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                        }
                        else
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                        }
                    }
                    else if (obj.DataType == StdDbType.VARCHAR2)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.CHAR)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.DATE)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                    }
                }
            }

            if (execute)
            {
                Db.Open();
                OracleDataReader reader = Db.Command.ExecuteReader();
                
                while (reader.Read())
                {
                    StdClass obj = new StdClass();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetOracleValue(i).ToString() != "null")
                        {
                            obj.DataDict[reader.GetName(i)] = reader.GetOracleValue(i).ToString();
                        }
                        else
                        {
                            obj.DataDict[reader.GetName(i)] = "";
                        }
                    }

                    list.Add(obj);
                }

                reader.Close();

                if (close)
                {
                    Db.Close();
                }
            }

            sr.Msgs.Add(cmd);
            return list;
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
                if (mode == 0 || obj.Value.ToString().Length > 0)
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

                        if (obj.DataType == StdDbType.NUMBER)
                        {
                            if (obj.Value != null && obj.Value.ToString().Length > 0)
                            {
                                Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                            }
                            else
                            {
                                Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                            }
                        }
                        else if (obj.DataType == StdDbType.VARCHAR2)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.CHAR)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.DATE)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                        }
                    }
                }
            }

            foreach (StdDbColumn obj in this.ParamList)
            {
                if (obj.Value.ToString().Length > 0)
                {
                    if (obj.DataType == StdDbType.NUMBER)
                    {
                        if (obj.Value != null && obj.Value.ToString().Length > 0)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                        }
                        else
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                        }
                    }
                    else if (obj.DataType == StdDbType.VARCHAR2)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.CHAR)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.DATE)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
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

            if (execute)
            {
                Db.Open();
                sr.IntValue = Db.Command.ExecuteNonQuery();

                if (close)
                {
                    Db.Close();
                }
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
                if (mode == 0 || obj.Value.ToString().Length > 0)
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

                        if (obj.DataType == StdDbType.NUMBER)
                        {
                            if (obj.Value != null && obj.Value.ToString().Length > 0)
                            {
                                Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                            }
                            else
                            {
                                Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                            }
                        }
                        else if (obj.DataType == StdDbType.VARCHAR2)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.CHAR)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.DATE)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                        }
                    }
                }
            }

            foreach (StdDbColumn obj in this.ParamList)
            {
                if (obj.Value.ToString().Length > 0)
                {
                    if (obj.DataType == StdDbType.NUMBER)
                    {
                        if (obj.Value != null && obj.Value.ToString().Length > 0)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                        }
                        else
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                        }
                    }
                    else if (obj.DataType == StdDbType.VARCHAR2)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.CHAR)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.DATE)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
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

            if (execute)
            {
                Db.Open();
                sr.IntValue = Db.Command.ExecuteNonQuery();

                if (close)
                {
                    Db.Close();
                }
            }

            sr.Msgs.Add(cmd);
            return sr;
        }

        /// <summary>
        /// Delete SQL
        /// </summary>
        /// <param name="execute">true: 実行する, false: 実行しない</param>
        /// <param name="close">true: close 実行, false: close しない</param>
        /// <returns></returns>
        public StdReturn DeleteSQL(bool execute = true, bool close = true)
        {
            StdReturn sr = new StdReturn();
            string cmd = "";

            if (this.Table.Length == 0)
            {
                sr.Errs.Add("対象のテーブルがありません");
            }

            if (this.WhereState.Length == 0)
            {
                sr.Errs.Add("削除レコードを指定する Where 文がありません");
            }

            if (sr.ErrExist)
            {
                return sr;
            }

            foreach (StdDbColumn obj in this.ParamList)
            {
                if (obj.Value.ToString().Length > 0)
                {
                    if (obj.DataType == StdDbType.NUMBER)
                    {
                        if (obj.Value != null && obj.Value.ToString().Length > 0)
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                        }
                        else
                        {
                            Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                        }
                    }
                    else if (obj.DataType == StdDbType.VARCHAR2)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.CHAR)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                    }
                    else if (obj.DataType == StdDbType.DATE)
                    {
                        Db.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                    }
                }
            }

            cmd = "delete from " + this.Table + this.WhereState;

            Db.Command.CommandText = cmd;

            if (execute)
            {
                Db.Open();
                sr.IntValue = Db.Command.ExecuteNonQuery();

                if (close)
                {
                    Db.Close();
                }
            }

            sr.Msgs.Add(cmd);
            return sr;
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

        /// <summary>
        /// テーブルの構造を取得する
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static List<StdDbColumn> GetList(DB db, string table_name)
        {
            List<StdDbColumn> list = new List<StdDbColumn>();

            db.Open();

            db.Command.CommandText = "select a.COLUMN_NAME, a.DATA_TYPE" +
                " ,nvl(a.DATA_PRECISION, a.CHAR_COL_DECL_LENGTH) as length " +
                " ,a.DATA_SCALE " +
                " ,a.NULLABLE " +
                " from user_tab_columns a " +
                " where a.TABLE_NAME = '" + table_name + "'" +
                " order by a.COLUMN_ID ";

            OracleDataReader reader = db.Command.ExecuteReader();

            while (reader.Read())
            {
                StdDbColumn obj = new StdDbColumn();

                obj.Name = reader["COLUMN_NAME"].ToString();

                string c = reader["DATA_TYPE"].ToString();

                if (c.Equals("NUMBER", StringComparison.CurrentCultureIgnoreCase))
                {
                    obj.DataType = StdDbType.NUMBER;
                }
                else if (c.Equals("VARCHAR2", StringComparison.CurrentCultureIgnoreCase))
                {
                    obj.DataType = StdDbType.VARCHAR2;
                }
                else if (c.Equals("CHAR", StringComparison.CurrentCultureIgnoreCase))
                {
                    obj.DataType = StdDbType.CHAR;
                }
                else if (c.Equals("DATE", StringComparison.CurrentCultureIgnoreCase))
                {
                    obj.DataType = StdDbType.DATE;
                }

                int.TryParse(reader["LENGTH"].ToString(), out obj.Length);
                int.TryParse(reader["DATA_SCALE"].ToString(), out obj.Scale);

                c = reader["NULLABLE"].ToString();

                if (c.Equals("N", StringComparison.CurrentCultureIgnoreCase))
                {
                    obj.Nullable = false;
                }
                else
                {
                    obj.Nullable = true;
                }

                list.Add(obj);
            }

            reader.Close();
            db.Close();

            return list;
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
        public Object Value;

        /// <summary>
        /// エラーのリスト
        /// </summary>
        public List<string> Errs = new List<string>();

        /// <summary>
        /// エラー文（改行で接続）
        /// </summary>
        public string Err
        {
            get
            {
                string s = "";

                foreach (string ss in this.Errs)
                {
                    if (s.Length > 0)
                    {
                        s += Environment.NewLine;
                    }

                    s += ss;
                }

                return s;
            }
        }

        /// <summary>
        /// エラーの有無
        /// </summary>
        public bool ErrExist
        {
            get
            {
                return this.Errs.Count() > 0 ? true : false;
            }
        }

        /// <summary>
        /// メッセージのリスト
        /// </summary>
        public List<string> Msgs = new List<string>();

        /// <summary>
        /// メッセージ文（改行で接続）
        /// </summary>
        public string Msg
        {
            get
            {
                string s = "";

                foreach (string ss in this.Msgs)
                {
                    if (s.Length > 0)
                    {
                        s += Environment.NewLine;
                    }

                    s += ss;
                }

                return s;
            }
        }

        /// <summary>
        /// メッセージの有無
        /// </summary>
        public bool MsgExist
        {
            get
            {
                return this.Msgs.Count() > 0 ? true : false;
            }
        }

        /// <summary>
        /// 戻り値。整数型
        /// </summary>
        public int IntValue = 0;

        /// <summary>
        /// 戻り値。倍精度浮動小数点数
        /// </summary>
        public double DoubleValue = 0.0;

        /// <summary>
        /// 終了値。
        /// 0: 正常終了
        /// </summary>
        public int Exit = 0;
    }
}
