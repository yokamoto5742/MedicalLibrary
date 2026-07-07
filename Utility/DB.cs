using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Oracle.DataAccess.Client;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Utility
{
    public class DB
    {
        public static DB Db1 = new DB();
        public static DB Db2 = new DB();
        public static DB Db3 = new DB();
        public OracleConnection Connection = new OracleConnection();
        public OracleCommand Command = new OracleCommand();


        /// <summary>
        /// Close() 実行時に接続を閉じるかどうか。
        /// </summary>
        bool ToClose = false;


        public DB()
        {
            Command.Connection = Connection;
            Command.BindByName = true;
        }


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="con_str"></param>
        public void Init(string connection_string)
        {
            Connection.ConnectionString = connection_string;
        }


        /// <summary>
        /// 接続を開く
        /// </summary>
        public void Open()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                ToClose = true;
            }
            else
            {
                ToClose = false;
            }
        }

        /// <summary>
        /// 接続を閉じる
        /// </summary>
        public void Close()
        {
            if (ToClose)
            {
                Connection.Close();
                ToClose = false;
            }

            Command.Parameters.Clear();
        }

        /// <summary>
        /// Select SQL を直接実行して結果を文字列で取得する。
        /// Open/Close も含めて実行されるので、直接SQLを書くだけでよい。
        /// </summary>
        /// <param name="command_text"></param>
        /// <returns></returns>
        public string ExecuteSelect(string command_text)
        {
            string result = "";

            Open();

            this.Command.CommandText = command_text;
            OracleDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                result += Environment.NewLine;

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result += reader[i] + ",";
                }
            }

            reader.Close();

            Close();

            return result;
        }

        /// <summary>
        /// Update/Delete SQL を直接実行する。
        /// Open/Close も含めて実行されるので、直接SQLを書くだけでよい。
        /// </summary>
        /// <param name="command_text">SQL</param>
        /// <param name="param_list">パラメータリスト</param>
        /// <param name="execute">true: 実行する, false: 実行しない</param>
        /// <param name="mode">0: null も登録, 1: null なら無視</param>
        /// <param name="close">true: close 実行, false: close しない</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string command_text, List<StdDbColumn> param_list = null, bool execute = true, int mode = 0, bool close = true)
        {
            int result = -1;

            this.Command.CommandText = command_text;

            if (param_list != null)
            {
                foreach (StdDbColumn obj in param_list)
                {
                    if (mode == 0 || obj.Value.ToString().Length > 0)
                    {
                        if (obj.DataType == StdDbType.NUMBER)
                        {
                            if (obj.Value != null && obj.Value.ToString().Length > 0)
                            {
                                this.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = obj.Value;
                            }
                            else
                            {
                                this.Command.Parameters.Add(":" + obj.Name, OracleDbType.Decimal).Value = DBNull.Value;
                            }
                        }
                        else if (obj.DataType == StdDbType.VARCHAR2)
                        {
                            this.Command.Parameters.Add(":" + obj.Name, OracleDbType.Varchar2).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.CHAR)
                        {
                            this.Command.Parameters.Add(":" + obj.Name, OracleDbType.Char).Value = obj.Value;
                        }
                        else if (obj.DataType == StdDbType.DATE)
                        {
                            this.Command.Parameters.Add(":" + obj.Name, OracleDbType.Date).Value = obj.Value;
                        }
                    }
                }
            }

            if (execute)
            {
                this.Open();
                result = this.Command.ExecuteNonQuery();

                if (close)
                {
                    this.Close();
                }
            }

            return result;
        }

        public int NextVal(string sequence)
        {
            int i = -1;

            if (sequence.Length == 0)
            {
                return i;
            }

            this.Open();

            this.Command.CommandText = "select " + sequence + ".nextval SEQ from DUAL"; ;
            OracleDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                int.TryParse(reader["SEQ"].ToString(), out i);
                break;
            }

            reader.Close();
            this.Close();

            return i;
        }

        public int CurrVal(string sequence)
        {
            int i = -1;

            if (sequence.Length == 0)
            {
                return i;
            }

            this.Open();

            this.Command.CommandText = "select " + sequence + ".currval SEQ from DUAL"; ;
            OracleDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                int.TryParse(reader["SEQ"].ToString(), out i);
                break;
            }

            reader.Close();
            this.Close();

            return i;
        }
    }
}
