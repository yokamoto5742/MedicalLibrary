using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Entity
{
    public class OrderDetail
    {
        /// <summary>
        /// 種別
        /// 1: Rpの先頭
        /// </summary>
        public int Kind = 0;

        /// <summary>
        /// 施行予定日
        /// </summary>
        public string SekouDate = "";

        /// <summary>
        /// 明細連番
        /// </summary>
        public int SEQ2 = 0;

        /// <summary>
        /// 診療区分コード
        /// </summary>
        public string SDCD = "";

        /// <summary>
        /// 診療区分
        /// </summary>
        public string KouiName = "";

        /// <summary>
        /// オーダーコード
        /// </summary>
        public string OrderCode = "";

        /// <summary>
        /// オーダー項目
        /// </summary>
        public string OrderName = "";

        /// <summary>
        /// 数量
        /// </summary>
        public float Qty = 0;

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit = "";

        /// <summary>
        /// 日/回数
        /// </summary>
        public float Times = 0;

        /// <summary>
        /// 予備フラグ
        /// </summary>
        public int[] EtcFlgs = new int[22];

        public OrderDetail()
        {
        }

        public OrderDetail(PatOrderDetail detail)
        {
            this.SDCD = detail.KouiCode;
            this.KouiName = detail.KouiName;
            this.OrderCode = detail.Code;
            this.OrderName = detail.Name;
            this.Qty = detail.Qty;
            this.Unit = detail.Unit;
            this.Times = detail.Times;
        }


        /// <summary>
        /// オーダーコードが存在し、かつ 8888888% ではなく
        /// SDCD・診療区分・名称・単位が無い場合はオーダーマスターから取得
        /// </summary>
        public void GetFromOrderMaster()
        {
            // オーダーコードが存在し、かつ 8888888% ではなく
            // SDCD・診療区分・名称・単位が無い場合はオーダーマスターから取得
            if (this.OrderCode.Length > 0 && !this.OrderCode.StartsWith("8888888"))
            {
                OrderMaster master = OrderMaster.Load(this.OrderCode);

                if (this.SDCD.Length == 0 || this.SDCD.Equals("0"))
                {
                    this.SDCD = master.KouiCodes[1].ToString();
                }

                if (this.KouiName.Length == 0)
                {
                    this.KouiName = master.KouiNames[1];
                }

                if (this.OrderName.Length == 0)
                {
                    this.OrderName = master.FullName;
                }

                if (this.Unit.Length == 0)
                {
                    this.Unit = master.InputUnit;
                }

                // 予備フラグをセット
                this.EtcFlgs = master.EtcFlgs;
            }
        }

/*
        public void Insert(DataGridView view, int i)
        {
            view.Rows.Insert(i, 1);

            DataGridViewRow r = view.Rows[i];

            r.Cells["Kind"].Value = this.Kind;

            // オーダーコードが存在し、かつ 8888888% ではなく
            // SDCD・診療区分・名称・単位が無い場合はオーダーマスターから取得
            if (this.OrderCode.Length > 0 && !this.OrderCode.StartsWith("8888888"))
            {
                this.GetFromOrderMaster();
            }

            if (this.Kind.Equals(1))
            {
                r.Cells["SekouDate"].Value = this.SekouDate;
                r.Cells["KouiName"].Value = this.KouiName;
            }

            r.Cells["Kind"].Value = this.Kind;
            r.Cells["SEQ2"].Value = this.SEQ2;
            r.Cells["SDCD"].Value = this.SDCD;
            r.Cells["OrderCode"].Value = this.OrderCode;
            r.Cells["OrderName"].Value = this.OrderName;
            r.Cells["Qty"].Value = this.Qty;
            r.Cells["Unit"].Value = this.Unit;

            if (this.Times > 0)
            {
                r.Cells["Times"].Value = this.Times;
                r.DividerHeight = 1;
            }

            r.Cells["RsvCode1"].Value = this.EtcFlgs[7];
            r.Cells["RsvCode2"].Value = this.EtcFlgs[17];
        }
 */
    }
}
