using System;
using System.Collections.Generic;
using System.Text;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class FCROrder : PatOrder
    {
        public static List<FCROrder> GetListFCROrder(string date)
        {
            List<FCROrder> list = new List<FCROrder>();

            if (!DateTimeAgent.IsDate(date))
            {
                return list;
            }

            List<string> order_id_list = new List<string>();
#if INNO
            string cmd = "select th.* " +
                " , P_KANA, P_NAME, P_SEX, TEL P_TEL, P_BIRTHDAY_AD " +
                " from D_ORDER_HEADER th " +
                " inner join M_PATIENT tm on th.P_ID = tm.P_ID " +
                " where th.SEKOU_CODE in (101, 112) " +
                " and (th.ORDER_DATE = " + date + " or th.ORDER_DATE = 99999999) " +
                " order by SEKOU_TIME desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);
#else
            string cmd = "select th.* " +
                " , Trim(IM01RC_F03) as P_KANA, Trim(IM01RC_F04) as P_NAME, IM01RC_F05 as P_SEX, IM01RC_F08 as P_TEL, IM01RC_F10 as P_BIRTHDAY_AD " +
                " from ＮＴオーダーヘッダー th " + 
                " inner join IM01RC tm on th.患者コード = tm.IM01RC_F01 " +
                " where th.施行部署１ in (101, 112) " +
                " and (th.施行予定日 = " + date + " or th.施行予定日 = 99999999) " +
                " order by 装置番号 desc";

            List<StdClass> tmp_list = StdClass.GetList(DB.Db1, cmd);
#endif
            foreach (StdClass tmp in tmp_list)
            {
                FCROrder obj = GetFromStdClass(tmp);
                list.Add(obj);
                order_id_list.Add(obj.OrderId);
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

        static FCROrder GetFromStdClass(StdClass tmp)
        {
            FCROrder obj = new FCROrder();

            obj.Pat.Kana = tmp.GetDataString("P_KANA").Trim();
            obj.Pat.Name = tmp.GetDataString("P_NAME").Trim();
            obj.Pat.Sex = tmp.GetDataString("P_SEX");
            obj.Pat.Birth = tmp.GetDataString("P_BIRTHDAY_AD");
            obj.Pat.Tel = tmp.GetDataString("P_TEL");

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
