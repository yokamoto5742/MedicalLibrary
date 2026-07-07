using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class SoapOrderHeader : StdEntity
    {
        public string PtId = "";

        /// <summary>
        /// 施行予定日
        /// </summary>
        public int Date = 0;

        public int InOut = 1;

        /// <summary>
        /// 診療区分
        /// </summary>
        public string Koui = "";

        public string KouiName
        {
            get
            {
                string s = "";

                if (Dict.KouiDict.ContainsKey(Koui))
                {
                    s = Dict.KouiDict[Koui];
                }

                return s;
            }
        }

        public string Dept = "";

        public string DeptName
        {
            get
            {
                string s = "";

                if (Dict.DeptDict.ContainsKey(Dept))
                {
                    s = Dict.DeptDict[Dept].FullName;
                }

                return s;
            }
        }

        public string Staff = "";

        public string StaffName
        {
            get
            {
                string s = "";

                if (Dict.StaffDict.ContainsKey(Staff))
                {
                    s = Dict.StaffDict[Staff].Name;
                }

                return s;
            }
        }

        /// <summary>
        /// オーダー番号
        /// </summary>
        public string OrderCode = "";

        public string Soap = "";

        public string SoapShow(float font_size, int width)
        {
            return AppString.Wrap(this.Soap, font_size, width);
        }


        public static Dictionary<string, Dictionary<string, SoapOrderHeader>> GetDict(string pt_id, List<string> date_list)
        {
            Dictionary<string, Dictionary<string, SoapOrderHeader>> dict = new Dictionary<string, Dictionary<string, SoapOrderHeader>>();

            if (date_list.Count == 0)
            {
                return dict;
            }

            string date_str = "";

            foreach (string date in date_list)
            {
                if (date_str.Length > 0)
                {
                    date_str += ",";
                }

                date_str += date;
            }

            List<PatOrder> tmp_list = PatOrder.GetListByPatCond(pt_id, new List<string> { "ORDER_DATE in (" + date_str + ")" });

            foreach (PatOrder tmp in tmp_list)
            {
                SoapOrderHeader obj = new SoapOrderHeader();

                obj.PtId = pt_id;
                int.TryParse(tmp.InOut, out obj.InOut);
                int.TryParse(tmp.SekouDate, out obj.Date);
                obj.Koui = tmp.Shinku;
                obj.Dept = tmp.Dept;
                obj.Staff = tmp.Staff;
                obj.OrderCode = tmp.OrderId;
                obj.Soap = tmp.DetailString;

                if (dict.ContainsKey(obj.Date.ToString()))
                {
                    dict[obj.Date.ToString()].Add(obj.OrderCode, obj);
                }
                else
                {
                    Dictionary<string, SoapOrderHeader> order_dict = new Dictionary<string, SoapOrderHeader>();
                    order_dict.Add(obj.OrderCode, obj);
                    dict.Add(obj.Date.ToString(), order_dict);
                }
            }

            return dict;
        }
    }
}
