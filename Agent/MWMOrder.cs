using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class MWMOrder : PatOrder
    {
        public string MwmId = "";
        public string Status = "";

        public static List<MWMOrder> Load(string sekou_date, List<string> code_list, MWMKind kind)
        {
            List<MWMOrder> list = new List<MWMOrder>();

            if (sekou_date.Length != 8 || AppString.ConcatList(code_list, ",").Length == 0)
            {
                return list;
            }

            List<string> order_id_list = new List<string>();
            string cmd = "";

            if (kind == MWMKind.SekouCode)
            {
#if INNO
                cmd = "select ID, STATUS, P_KANA, P_NAME, P_SEX, P_BIRTHDAY_AD, th.* " +
                    " from D_ORDER_HEADER" + Env.DB_LINK + " th " +
                    " inner join M_PATIENT" + Env.DB_LINK + " tm on th.P_ID = tm.P_ID " +
                    " left join MWM_SENT on ORDER_NO = ORDER_ID " +
                    " where ORDER_DATE in (" + sekou_date + ", 99999999) and SEKOU_CODE in (" + AppString.ConcatList(code_list, ",") + ")";
#else
                cmd = "select ID, STATUS, Trim(IM01RC_F03) as P_KANA, Trim(IM01RC_F04) as P_NAME, IM01RC_F05 as P_SEX, IM01RC_F10 as P_BIRTHDAY_AD, th.* " +
                    " from ＮＴオーダーヘッダー" + Env.DB_LINK + " th " +
                    " inner join IM01RC" + Env.DB_LINK + " tm on th.患者コード = tm.IM01RC_F01 " +
                    " left join MWM_SENT on オーダー番号 = ORDER_ID " +
                    " where 施行予定日 in (" + sekou_date + ", 99999999) and 施行部署１ in (" + AppString.ConcatList(code_list, ",") + ")";
#endif
                List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                foreach (StdClass tmp in tmp_list)
                {
                    MWMOrder obj = GetFromStdClass(tmp);
                    list.Add(obj);
                    order_id_list.Add(obj.OrderId);
                }
            }
            else if (kind == MWMKind.OrderCode)
            {
#if INNO
                cmd = "select ORDER_NO " +
                    "  from D_ORDER_DETAIL" +
                    "  where ORDER_DATE in (" + sekou_date + ", 99999999) and ORDER_CODE in (" + AppString.ConcatList(code_list, ",", "'") + ")";

                List<StdClass> tmp_list2 = StdClass.GetList(DB.Db3, cmd);
#else
                cmd = "select オーダー番号 ORDER_NO " +
                    "  from ＮＴオーダーディティール" +
                    "  where 施行予定日 in (" + sekou_date + ", 99999999) and オーダーコード in (" + AppString.ConcatList(code_list, ",", "'") + ")";

                List<StdClass> tmp_list2 = StdClass.GetList(DB.Db1, cmd);
#endif
                foreach (StdClass tmp in tmp_list2)
                {
                    if (tmp.GetDataString("ORDER_NO").Length == 0)
                    {
                        continue;
                    }

                    if (order_id_list.Contains(tmp.GetDataString("ORDER_NO")))
                    {
                        continue;
                    }

                    order_id_list.Add(tmp.GetDataString("ORDER_NO"));
                }

                if (order_id_list.Count > 0)
                {
#if INNO
                    cmd = "select ID, STATUS, P_KANA, P_NAME, P_SEX, P_BIRTHDAY_AD, th.* " +
                        " from D_ORDER_HEADER" + Env.DB_LINK + " th " +
                        " inner join M_PATIENT" + Env.DB_LINK + " tm on th.P_ID = tm.P_ID " +
                        " left join MWM_SENT on ORDER_NO = ORDER_ID " +
                        " where ORDER_NO in (" + AppString.ConcatList(order_id_list, ",") + ")";
#else
                    cmd = "select ID, STATUS, Trim(IM01RC_F03) as P_KANA, Trim(IM01RC_F04) as P_NAME, IM01RC_F05 as P_SEX, IM01RC_F10 as P_BIRTHDAY_AD, th.* " +
                        " from ＮＴオーダーヘッダー" + Env.DB_LINK + " th " +
                        " inner join IM01RC" + Env.DB_LINK + " tm on th.患者コード = tm.IM01RC_F01 " +
                        " left join MWM_SENT on オーダー番号 = ORDER_ID " +
                        " where オーダー番号 in (" + AppString.ConcatList(order_id_list, ",") + ")";
#endif
                    List<StdClass> tmp_list = StdClass.GetList(DB.Db2, cmd);

                    foreach (StdClass tmp in tmp_list)
                    {
                        list.Add(GetFromStdClass(tmp));
                    }
                }
            }

            // オーダーディティールを取得する
            List<PatOrderDetail> list2 = PatOrderDetail.Load(order_id_list);

            foreach (PatOrderDetail d in list2)
            {
                foreach (PatOrder p in list)
                {
                    if (p.OrderId.Equals(d.OrderId))
                    {
                        p.DetailList.Add(d);
                        break;
                    }
                }
            }

            return list;
        }

        static MWMOrder GetFromStdClass(StdClass tmp)
        {
            MWMOrder obj = new MWMOrder();

            obj.MwmId = tmp.GetDataString("ID");
            obj.Status = tmp.GetDataString("STATUS");

            obj.Pat.Kana = tmp.GetDataString("P_KANA").Trim();
            obj.Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Pat.Sex = tmp.GetDataString("P_SEX");
            obj.Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");

#if INNO
            obj.OrderId = tmp.DataDict["ORDER_NO"].ToString();
            obj.UkeId = tmp.DataDict["RP_NO"].ToString();
            int.TryParse(tmp.DataDict["ORDER_SEQ"].ToString(), out obj.UkeSEQ);
            obj.Pat.Id = tmp.DataDict["P_ID"].ToString();
            obj.SekouDate = tmp.DataDict["ORDER_DATE"].ToString();
            obj.SekouTime = tmp.DataDict["SEKOU_TIME"].ToString();
            obj.Pat.Ins = tmp.DataDict["P_HOKEN"].ToString();
            obj.Shinku = tmp.DataDict["SHINKU"].ToString();
            obj.InOut = tmp.DataDict["INOUT"].ToString();
            obj.Dept = tmp.DataDict["DEPT"].ToString();
            obj.Doctor = tmp.DataDict["DR"].ToString();
            obj.OrderDate = tmp.DataDict["DIRECTION_DATE"].ToString();
            obj.StartDate = tmp.DataDict["DATE_S"].ToString();
            obj.EndDate = tmp.DataDict["DATE_E"].ToString();
            //            obj.SOAP = tmp.DataDict["ＳＯＡＰ表示名称"].ToString();
            obj.SekouFlg = tmp.DataDict["SEKOU_FLG"].ToString();
            obj.KaikeiFlg = tmp.DataDict["BILL_FLG"].ToString();
            obj.PaperFlg = tmp.DataDict["PRINT_FLG"].ToString();
            obj.InnaiFlg = tmp.DataDict["IN_TYPE"].ToString();
            obj.Sekou1 = tmp.DataDict["SEKOU_CODE"].ToString();
            //            obj.Sekou2 = tmp.DataDict["施行部署２"].ToString();
            obj.SekouStaff = tmp.DataDict["SEKOU_USR"].ToString();
#else
            obj.OrderId = tmp.DataDict["オーダー番号"].ToString();
            obj.UkeId = tmp.DataDict["受付番号"].ToString();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.UkeSEQ);
            obj.Pat.Id = tmp.DataDict["患者コード"].ToString();
            obj.SekouDate = tmp.DataDict["施行予定日"].ToString();
            obj.SekouTime = tmp.DataDict["装置番号"].ToString();
            obj.Pat.Ins = tmp.DataDict["保険ビット"].ToString();
            obj.Shinku = tmp.DataDict["診療区分"].ToString();
            obj.InOut = tmp.DataDict["入外区分"].ToString();
            obj.Dept = tmp.DataDict["科コード"].ToString();
            obj.Doctor = tmp.DataDict["指示医コード"].ToString();
            obj.OrderDate = tmp.DataDict["指示日"].ToString();
            obj.StartDate = tmp.DataDict["開始日付"].ToString();
            obj.EndDate = tmp.DataDict["終了日付"].ToString();
            obj.SOAP = tmp.DataDict["ＳＯＡＰ表示名称"].ToString();
            obj.SekouFlg = tmp.DataDict["施行フラグ"].ToString();
            obj.KaikeiFlg = tmp.DataDict["会計フラグ"].ToString();
            obj.PaperFlg = tmp.DataDict["指示箋フラグ"].ToString();
            obj.InnaiFlg = tmp.DataDict["院内区分"].ToString();
            obj.Sekou1 = tmp.DataDict["施行部署１"].ToString();
            obj.Sekou2 = tmp.DataDict["施行部署２"].ToString();
            obj.SekouStaff = tmp.DataDict["施行者コード"].ToString();
#endif

            return obj;
        }
    }
}
