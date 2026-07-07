using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlOrderSheetGridView1 : DataGridView
    {
        public CtrlOrderSheetGridView1()
        {
        }

        public void Init()
        {
            this.Columns.Add("Kind", "種別");
            this.Columns.Add("SekouDate", "実施日");
            this.Columns.Add("SEQ2", "明細連番");
            this.Columns.Add("SDCD", "SD");
            this.Columns.Add("KouiName", "診");
            this.Columns.Add("OrderCode", "オーダーコード");
            this.Columns.Add("OrderName", "オーダー項目");
            this.Columns.Add("Qty", "数量");
            this.Columns.Add("Unit", "単位");
            this.Columns.Add("Times", "日/回数");
            this.Columns.Add("RsvCode1", "予約種別");
            this.Columns.Add("RsvCode2", "予約詳細");

            this.Columns["Kind"].Width = 20;
            this.Columns["Kind"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["SekouDate"].Width = 80;
            this.Columns["SekouDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Columns["SekouDate"].ReadOnly = true;

            this.Columns["SEQ2"].Width = 20;
            this.Columns["SEQ2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["SDCD"].Width = 30;
            this.Columns["SDCD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["KouiName"].Width = 40;
            this.Columns["KouiName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["OrderCode"].Width = 70;
            this.Columns["OrderName"].Width = 140;

            this.Columns["Qty"].Width = 30;
            this.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.Columns["Unit"].Width = 30;
            this.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.Columns["Times"].Width = 30;
            this.Columns["Times"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.Columns["RsvCode1"].Width = 30;
            this.Columns["RsvCode1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.Columns["RsvCode2"].Width = 30;
            this.Columns["RsvCode2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(CtrlOrderSheetView1_EditingControlShowing);
            this.CellEndEdit += new DataGridViewCellEventHandler(CtrlOrderSheetView1_CellEndEdit);
            this.CellClick += new DataGridViewCellEventHandler(CtrlOrderSheetView1_CellClick);
            this.KeyDown += new KeyEventHandler(CtrlOrderSheetGridView1_KeyDown);
        }

        void CtrlOrderSheetGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.CurrentRow != null)
                {
                    this.RemoveCurrentRow();
                }
            }
        }

        public void Insert(int i, int num = 1)
        {
            if (i < 0 || i > this.RowCount - 1)
            {
                return;
            }

            this.Rows.Insert(i, num);
        }

        public void InsertCurrentRow(int num = 1)
        {
            if (this.CurrentRow != null)
            {
                this.Rows.Insert(this.CurrentRow.Index, num);
            }
        }

        public void RemoveAt(int i)
        {
            if (i < 0 || i > this.RowCount - 1)
            {
                return;
            }

            this.Rows.RemoveAt(i);
        }

        public void RemoveCurrentRow()
        {
            if (this.CurrentRow != null && !this.CurrentRow.IsNewRow)
            {
                this.Rows.RemoveAt(this.CurrentRow.Index);
            }
        }

        public void OrderHeaderInsert(OrderHeader order, bool insert_last = true, bool delimiter = true)
        {
            // デフォルトは最下行に挿入
            int i = this.Rows.Count - 1;

            if (!insert_last && this.CurrentRow == null)
            {
                // 現在の位置に挿入
                i = this.CurrentRow.Index;
            }

            /*
            // 新規行または区切りの先頭行でない場合はエラーを出す
            DataGridViewRow rr = this.Rows[i];

            if (!rr.IsNewRow && rr.Cells["Kind"].Value != null && !rr.Cells["Kind"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("ここにオーダーを入れることは出来ません");
                return;
            }
             */

            int j = 0;

            foreach (OrderDetail detail in order.DetailList)
            {
                this.Rows.Insert(i, 1);

                DataGridViewRow r = this.Rows[i];

                if (detail.Kind.Equals(1))
                {
                    if (detail.SekouDate.Length == 10)
                    {
                        r.Cells["SekouDate"].Value = detail.SekouDate;
                    }
                    else
                    {
                        // 施行予定日が空の場合は、本日の日付を入れる
                        r.Cells["SekouDate"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                    }

                    r.Cells["KouiName"].Value = detail.KouiName;
                }

                r.Cells["Kind"].Value = detail.Kind;
                r.Cells["SEQ2"].Value = detail.SEQ2;
                r.Cells["SDCD"].Value = detail.SDCD;
                r.Cells["OrderCode"].Value = detail.OrderCode;
                r.Cells["OrderName"].Value = detail.OrderName;
                r.Cells["Qty"].Value = detail.Qty;
                r.Cells["Unit"].Value = detail.Unit;

                if (detail.Times > 0)
                {
                    // 回数が入っている場合は、必ず区切り線を入れる。
                    r.Cells["Times"].Value = detail.Times;
                    r.DividerHeight = 1;
                }
                else if (j >= order.DetailList.Count - 1 && delimiter)
                {
                    // 回数が入っていない場合は、DetailList の最後の行に区切り線を入れる。
                    // ただし delimiter == false のとき（薬ボタンなど）は入れない。
                    r.Cells["Times"].Value = "1";
                    r.DividerHeight = 1;
                }

                r.Cells["RsvCode1"].Value = detail.EtcFlgs[7];
                r.Cells["RsvCode2"].Value = detail.EtcFlgs[17];

                i++;
                j++;
            }

            this.CurrentCell = this.Rows[i].Cells[0];
        }

        public void OrderDetailInsert(List<OrderDetail> detail_list, bool insert_last = true, bool delimiter = true)
        {
            // デフォルトは最下行に挿入
            int i = this.Rows.Count - 1;

            if (!insert_last && this.CurrentRow == null)
            {
                // 現在の位置に挿入
                i = this.CurrentRow.Index;
            }

            /*
            // 新規行または区切りの先頭行でない場合はエラーを出す
            DataGridViewRow rr = this.Rows[i];

            if (!rr.IsNewRow && rr.Cells["Kind"].Value != null && !rr.Cells["Kind"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("ここにオーダーを入れることは出来ません");
                return;
            }
             */

            int j = 0;

            foreach (OrderDetail detail in detail_list)
            {
                this.Rows.Insert(i, 1);

                DataGridViewRow r = this.Rows[i];

                // オーダーコードが存在し、かつ 8888888% ではなく
                // SDCD・診療区分・名称・単位が無い場合はオーダーマスターから取得
                if (detail.OrderCode.Length > 0 && !detail.OrderCode.StartsWith("8888888"))
                {
                    detail.GetFromOrderMaster();
                }

                if (detail.Kind.Equals(1))
                {
                    if (detail.SekouDate.Length == 10)
                    {
                        r.Cells["SekouDate"].Value = detail.SekouDate;
                    }
                    else
                    {
                        // 施行予定日が空の場合は、本日の日付を入れる
                        r.Cells["SekouDate"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                    }

                    r.Cells["KouiName"].Value = detail.KouiName;
                }

                r.Cells["Kind"].Value = detail.Kind;
                r.Cells["SEQ2"].Value = detail.SEQ2;
                r.Cells["SDCD"].Value = detail.SDCD;
                r.Cells["OrderCode"].Value = detail.OrderCode;
                r.Cells["OrderName"].Value = detail.OrderName;
                r.Cells["Qty"].Value = detail.Qty;
                r.Cells["Unit"].Value = detail.Unit;

                if (detail.Times > 0)
                {
                    // 回数が入っている場合は、必ず区切り線を入れる。
                    r.Cells["Times"].Value = detail.Times;
                    r.DividerHeight = 1;
                }
                else if (j >= detail_list.Count - 1 && delimiter)
                {
                    // 回数が入っていない場合は、DetailList の最後の行に区切り線を入れる。
                    // ただし delimiter == false のとき（薬ボタンなど）は入れない。
                    r.Cells["Times"].Value = "1";
                    r.DividerHeight = 1;
                }

                r.Cells["RsvCode1"].Value = detail.EtcFlgs[7];
                r.Cells["RsvCode2"].Value = detail.EtcFlgs[17];

                i++;
                j++;
            }

            this.CurrentCell = this.Rows[i].Cells[0];
        }

        void CtrlOrderSheetView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                string s = this.CurrentCell.OwningColumn.Name;

                if (s.Equals("SekouDate"))
                {
                    FormDateSelector f = new FormDateSelector(tb.Text);

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        tb.Text = DateTimeAgent.DateFormat(f.Date, DateTimeAgent.DateFormatKind.LONG);
                    }
                }
                else if (s.Equals("OrderName"))
                {
                    tb.ImeMode = ImeMode.Hiragana;
                }
                else if (s.Equals("Times") || s.Equals("Qty"))
                {
                    tb.ImeMode = ImeMode.Disable;
                }
            }
        }

        void CtrlOrderSheetView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string s = "";

            if (this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                s = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            if (e.ColumnIndex == 6)
            {
                if (s.Length >= 3)
                {
                    List<FormSelectorColumn> cols = new List<FormSelectorColumn>();

                    FormSelectorColumn col = new FormSelectorColumn();
                    col.ColumnName = "コード";
                    col.Width = 60;
                    col.Visible = true;
                    cols.Add(col);

                    col = new FormSelectorColumn();
                    col.ColumnName = "名称";
                    col.Width = 200;
                    col.Visible = true;
                    cols.Add(col);

                    col = new FormSelectorColumn();
                    col.ColumnName = "単位";
                    col.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.Width = 50;
                    col.Visible = true;
                    cols.Add(col);

                    col = new FormSelectorColumn();
                    col.ColumnName = "診療区分";
                    col.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.Width = 80;
                    col.Visible = true;
                    cols.Add(col);

                    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

                    List<string> name_list = new List<string>();
                    name_list.Add(s);
                    name_list.Add(AppString.ZenToHan(s));
                    name_list.Add(AppString.HiraToHankana(s));

                    List<OrderMaster> master_list = OrderMaster.FindByNames(name_list);

                    foreach (OrderMaster master in master_list)
                    {
                        foreach (int i in master.KouiCodes)
                        {
                            if (i > 0)
                            {
                                Dictionary<string, string> d = new Dictionary<string, string>();
                                d.Add("診療区分", i.ToString());
                                d.Add("コード", master.OrderCode);
                                d.Add("名称", master.FullName);
                                d.Add("単位", master.Unit);
                                list.Add(d);
                            }
                        }
                    }

                    Dictionary<string, string> data = new Dictionary<string, string>();

                    FormSelector f = new FormSelector(data);
                    f.Set(cols, list);
                    f.ShowDialog();

                    if (data.ContainsKey("名称"))
                    {
                        this.Rows[e.RowIndex].Cells["SekouDate"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                        this.Rows[e.RowIndex].Cells["SDCD"].Value = data["診療区分"];

                        if (Dict.KouiDict.ContainsKey(data["診療区分"]))
                        {
                            this.Rows[e.RowIndex].Cells["KouiName"].Value = Dict.KouiDict[data["診療区分"]];
                        }

                        this.Rows[e.RowIndex].Cells["OrderCode"].Value = data["コード"];
                        this.Rows[e.RowIndex].Cells["OrderName"].Value = data["名称"];
                        this.Rows[e.RowIndex].Cells["Unit"].Value = data["単位"];
                    }
                }
            }
            else if (e.ColumnIndex == 9)
            {
                if (s.Length > 0)
                {
                    if (s.Equals("0"))
                    {
                        this.Rows[e.RowIndex].DividerHeight = 0;
                        this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }
                    else
                    {
                        this.Rows[e.RowIndex].DividerHeight = 1;
                    }
                }
                else
                {
                    this.Rows[e.RowIndex].DividerHeight = 0;
                }
            }
        }

        void CtrlOrderSheetView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewRow r = this.Rows[e.RowIndex];

            // 日付選択に使う

            if (e.ColumnIndex == 1)
            {
                FormDateSelector f = new FormDateSelector(r.Cells[e.ColumnIndex].Value.ToString());

                if (f.ShowDialog() == DialogResult.OK)
                {
                    r.Cells[e.ColumnIndex].Value = DateTimeAgent.DateFormat(f.Date, DateTimeAgent.DateFormatKind.LONG);
                }
            }
        }

        public List<OrderHeader> GetOrderList()
        {
            List<OrderHeader> list = new List<OrderHeader>();

            bool first_flg = true;
            OrderHeader header = new OrderHeader();
            int seq2 = 1;

            foreach (DataGridViewRow r in this.Rows)
            {
                if (r.IsNewRow)
                {
                    break;
                }

                if (first_flg)
                {
                    if (header.DetailList.Count > 0)
                    {
                        list.Add(header);
                    }

                    header = new OrderHeader();
                    header.SekouDateString = r.Cells["SekouDate"].Value != null ? r.Cells["SekouDate"].Value.ToString() : DateTime.Now.ToString("yyyy/MM/dd");
                    seq2 = 1;
                }

                OrderDetail detail = new OrderDetail();

                detail.SekouDate = header.SekouDate;
                detail.SEQ2 = seq2++;
                detail.SDCD = r.Cells["SDCD"].Value != null ? r.Cells["SDCD"].Value.ToString().Trim() : "";
                detail.OrderCode = r.Cells["OrderCode"].Value != null ? r.Cells["OrderCode"].Value.ToString().Trim() : "";
                detail.OrderName = r.Cells["OrderName"].Value != null ? r.Cells["OrderName"].Value.ToString().Trim() : "";
                float.TryParse(r.Cells["Qty"].Value != null ? r.Cells["Qty"].Value.ToString().Trim() : "", out detail.Qty);
                detail.Unit = r.Cells["Unit"].Value != null ? r.Cells["Unit"].Value.ToString().Trim() : "";
                float.TryParse(r.Cells["Times"].Value != null ? r.Cells["Times"].Value.ToString().Trim() : "", out detail.Times);

                header.DetailList.Add(detail);

                if (detail.Times > 0)
                {
                    first_flg = true;
                }
                else
                {
                    first_flg = false;
                }
            }

            if (header.DetailList.Count > 0)
            {
                list.Add(header);
            }

            return list;
        }
    }
}
