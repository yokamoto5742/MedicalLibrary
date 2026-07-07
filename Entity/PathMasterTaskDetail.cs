using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class PathMasterTaskDetail : StdEntity
    {
        /// <summary>
        /// タスクコード
        /// </summary>
        public string TaskCode = "";

        /// <summary>
        /// 連番
        /// </summary>
        public int SEQ = 0;

        /// <summary>
        /// 診療区分
        /// </summary>
        public int KouiCode = 0;

        /// <summary>
        /// オーダーコード
        /// </summary>
        public string OrderCode = "";

        /// <summary>
        /// 数量
        /// </summary>
        public float Qty = 0;

        public string QtyString
        {
            get
            {
                string s = "";

                if (this.Qty != 0)
                {
                    s = this.Qty.ToString();
                }

                return s;
            }
        }

        /// <summary>
        /// 回数
        /// </summary>
        public float Times = 0;

        public string TimesString
        {
            get
            {
                string s = "";

                if (this.Times != 0)
                {
                    s = this.Times.ToString();
                }

                return s;
            }
        }

        /// <summary>
        /// コメント
        /// </summary>
        public string Cont1 = "";


        public static List<PathMasterTaskDetail> GetList(string task_code)
        {
            List<PathMasterTaskDetail> list = new List<PathMasterTaskDetail>();

            if (task_code.Length == 0)
            {
                return list;
            }

            string cmd = "select * from macs.PATH_タスクマスタ_オーダー t " +
                " where t.タスクコード = '" + task_code + "'" +
                " order by t.タスクコード, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        public static Panel GetPanel(string task_code, int width, int height)
        {
            int w = width;
            int h = height;

            if (w < 200)
            {
                w = 200;
            }

            if (h < 200)
            {
                h = 200;
            }

            Panel p = new Panel();
            p.Width = w;
            p.Height = h;
            p.AutoScroll = true;

            List<PathMasterTaskDetail> detail_list = PathMasterTaskDetail.GetList(task_code);
            int hh = 0;

            foreach (PathMasterTaskDetail detail in detail_list)
            {
                Label lb = new Label();
                lb.Location = new Point(5, hh);
                lb.Size = new System.Drawing.Size(119, 18);
                lb.AutoEllipsis = true;
                lb.Text = detail.Cont1;
                lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                lb.BackColor = Color.White;

                p.Controls.Add(lb);

                lb = new Label();
                lb.Location = new Point(125, hh);
                lb.Size = new System.Drawing.Size(24, 18);
                lb.AutoEllipsis = true;
                lb.Text = detail.QtyString;
                lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lb.BackColor = Color.White;

                p.Controls.Add(lb);

                lb = new Label();
                lb.Location = new Point(150, hh);
                lb.Size = new System.Drawing.Size(24, 18);
                lb.AutoEllipsis = true;
                lb.Text = detail.TimesString;
                lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lb.BackColor = Color.White;

                p.Controls.Add(lb);

                hh += 20;
            }

            return p;
        }


        public static List<PathMasterTaskDetail> GetList(List<string> task_code_list)
        {
            List<PathMasterTaskDetail> list = new List<PathMasterTaskDetail>();

            if (task_code_list.Count == 0)
            {
                return list;
            }

            string task_code_str = AppString.ConcatList(task_code_list, ",");

            string cmd = "select * from macs.PATH_タスクマスタ_オーダー t " +
                " where t.タスクコード in (" + task_code_str + ") " +
                " order by t.タスクコード, t.連番";

            List<StdClass> tmp_list = StdClass.GetList(Db, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                list.Add(GetFromStdClass(tmp));
            }

            return list;
        }


        static PathMasterTaskDetail GetFromStdClass(StdClass tmp)
        {
            PathMasterTaskDetail obj = new PathMasterTaskDetail();

            obj.TaskCode = tmp.DataDict["タスクコード"].ToString();
            int.TryParse(tmp.DataDict["連番"].ToString(), out obj.SEQ);
            int.TryParse(tmp.DataDict["診療区分"].ToString(), out obj.KouiCode);
            obj.OrderCode = tmp.DataDict["オーダーコード"].ToString();
            float.TryParse(tmp.DataDict["数量"].ToString(), out obj.Qty);
            float.TryParse(tmp.DataDict["回数"].ToString(), out obj.Times);
            obj.Cont1 = tmp.DataDict["コメント"].ToString();

            return obj;
        }
    }
}
