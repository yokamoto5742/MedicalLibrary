using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class MWMSent
    {
        public string MwmId = "";
        public string OrderId = "";
        /*
        public string SekouDate = "";
        public string SekouCode = "";
        public string SekouFlg = "";
        public string SekouTime = "";
        public string PtId = "";
        public string Kana = "";
        public string Name = "";
        public string Sex = "";
        public string Birth = "";
        public string Age = "";
        public string InOut = "";
        public string Dept = "";
        public string Doctor = "";
        public string OrderDate = "";
        public string Soap = "";
        */
        public string Status = "0";     // 0 êVãK, 1 çÌèú

        public void Save()
        {
            if (OrderId.Length == 0)
            {
                return;
            }

            string cmd = "select * from MWM_SENT where ID = " + MwmId;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            bool new_flg = true;

            foreach (StdClass tmp in tmp_list)
            {
                new_flg = false;
                break;
            }

            if (new_flg)
            {
                cmd = "insert into MWM_SENT (ID, ORDER_ID, STATUS) values (" + MwmId + ", " + OrderId + ", " + Status + ")";
                DB.Db2.ExecuteNonQuery(cmd);
            }
            else
            {
                cmd = "update MWM_SENT set STATUS = " + Status + " where ID = " + MwmId;
                DB.Db2.ExecuteNonQuery(cmd);
            }
        }

        public static string GetMwmId(string order_id)
        {
            string id = "";

            if (order_id.Length == 0)
            {
                return id;
            }

            string cmd = "select ID from MWM_SENT where ORDER_ID = " + order_id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

            bool new_flg = true;

            foreach (StdClass tmp in tmp_list)
            {
                id = tmp.GetDataString("ID");
                new_flg = false;
                break;
            }

            if (new_flg)
            {
                cmd = "select MWM_SEQ.nextval N from DUAL";

                tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    id = tmp.GetDataString("N");
                    break;
                }
            }

            return id;
        }
    }
}
