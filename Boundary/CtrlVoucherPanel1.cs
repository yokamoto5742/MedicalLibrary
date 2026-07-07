using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlVoucherPanel1 : Panel
    {
        public string Code = "";

        List<VoucherComp> CompList = new List<VoucherComp>();

        public CtrlVoucherPanel1(string code)
        {
            this.AutoSize = true;
            this.BackColor = Color.White;

            this.Code = code;
            this.CompList = VoucherComp.GetList(this.Code);
        }

        public void MakePanel()
        {
            VoucherCell[,] cells = VoucherCell.Get(this.CompList);

            this.Controls.Clear();

            foreach (VoucherComp obj in this.CompList)
            {
                if (obj.Attr1.Kind.Equals("1"))
                {
                    // 数値
                    TextBox cb = new TextBox();
                    cb.Name = obj.Pos;
                    cb.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    cb.TextAlign = HorizontalAlignment.Right;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = obj.Attr1.BackColorValue;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    this.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("2"))
                {
                    if (obj.Attr1.Embed.Equals("1") || obj.Cont1.Name.Length > 0)
                    {
                        // 「埋め込み」または最初から文字列が入っている場合

                        // ラベル
                        Label cb = new Label();
                        cb.Name = obj.Pos;
                        cb.AutoEllipsis = true;
                        cb.Padding = new Padding(3, 3, 3, 3);
                        cb.TextAlign = ContentAlignment.MiddleLeft;

                        if (obj.Cont1.Name.Length > 0)
                        {
                            cb.Text = obj.Cont1.Name;
//                            cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                        }

                        cb.Width = (int)obj.Width;
                        cb.Height = (int)obj.Height;
                        cb.ForeColor = obj.Attr1.ForeColorValue;
                        cb.BackColor = obj.Attr1.BackColorValue;
//                        cb.Font = obj.Attr1.FontValue;
                        cb.Tag = obj;
                        cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                        this.Controls.Add(cb);
                    }
                    else
                    {
                        // テキストボックス
                        TextBox cb = new TextBox();
                        cb.Name = obj.Pos;
//                        cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                        cb.Text = obj.Cont1.Name;
                        cb.Width = (int)obj.Width;
                        cb.Height = (int)obj.Height;
                        cb.ForeColor = obj.Attr1.ForeColorValue;
                        cb.BackColor = obj.Attr1.BackColorValue;
//                        cb.Font = obj.Attr1.FontValue;
                        cb.Tag = obj;
                        cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                        this.Controls.Add(cb);
                    }
                }
                else if (obj.Attr1.Kind.Equals("3"))
                {
                    // チェックボックス
                    CheckBox cb = new CheckBox();
                    cb.Name = obj.Pos;
                    cb.AutoEllipsis = true;
                    cb.Padding = new Padding(3, 3, 3, 3);
                    cb.Text = obj.Cont1.Name;
//                    cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = obj.Attr1.BackColorValue;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    cb.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);

                    this.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("4"))
                {
                    // コンボボックス
                    ComboBox cb = new ComboBox();
                    cb.Name = obj.Pos;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = obj.Attr1.BackColorValue;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    foreach (VoucherCont c in obj.ContList1)
                    {
                        if (c.Name.Length > 0)
                        {
                            cb.Items.Add(c.Name);
                        }
                    }

                    this.Controls.Add(cb);
                }
                else if (obj.Attr1.Kind.Equals("5"))
                {
                    // トグルボタン
                    CheckBox cb = new CheckBox();
                    cb.Name = obj.Pos;
                    cb.Text = obj.Cont1.Name + " [" + obj.Row + "," + obj.Col + "]";
                    cb.Appearance = Appearance.Button;
                    cb.Width = (int)obj.Width;
                    cb.Height = (int)obj.Height;
                    cb.ForeColor = obj.Attr1.ForeColorValue;
                    cb.BackColor = Color.LightGray;
//                    cb.Font = obj.Attr1.FontValue;
                    cb.Tag = obj;
                    cb.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X), (int)(cells[obj.Row - 1, obj.Col - 1].Y));

                    cb.Checked = obj.Checked;

                    cb.CheckedChanged += new EventHandler(Toggle_CheckedChanged);

                    this.Controls.Add(cb);
                }
            }

            this.MovePanel();
        }

        void MovePanel()
        {
            VoucherCell[,] cells = VoucherCell.Get(this.CompList);

            int x = this.HorizontalScroll.Value;
            int y = this.VerticalScroll.Value;

            foreach (VoucherComp obj in this.CompList)
            {
                foreach (Control c in this.Controls)
                {
                    VoucherComp v = (VoucherComp)(c.Tag);

                    if (obj == v)
                    {
                        c.Location = new Point((int)(cells[obj.Row - 1, obj.Col - 1].X) - x, (int)(cells[obj.Row - 1, obj.Col - 1].Y) - y);

                        if (!cells[obj.Row - 1, obj.Col - 1].Visible)
                        {
                            c.Visible = false;
                        }
                        else
                        {
                            c.Visible = true;
                        }

                        break;
                    }
                }
            }

            this.Refresh();
        }

        void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // グループ化設定されている場合、これが ON になれば、同一グループのチェックボックス・トグルボタンはすべて OFF になる。
            CheckBox cb = (CheckBox)sender;
            VoucherComp v = (VoucherComp)(cb.Tag);
            Panel panel = (Panel)(cb.Parent);

            v.Checked = cb.Checked;

            if (cb.Checked)
            {
                // 同一グループは OFF にする
                foreach (List<VoucherCompGroup> group_list in v.GroupList)
                {
                    foreach (VoucherCompGroup obj in group_list)
                    {
                        if (obj.Pos.Equals(v.Pos))
                        {
                            // 自分自身の場合は飛ばす
                            continue;
                        }

                        if (panel.Controls.ContainsKey(obj.Pos))
                        {
                            if (panel.Controls[obj.Pos] is CheckBox)
                            {
                                ((CheckBox)(panel.Controls[obj.Pos])).Checked = false;
                            }
                        }
                    }
                }
            }
        }

        void Toggle_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox tg = (CheckBox)sender;
            VoucherComp v = (VoucherComp)(tg.Tag);
            Panel panel = (Panel)(tg.Parent);

            v.Checked = tg.Checked;

            if (tg.Checked)
            {
                tg.ForeColor = v.Attr1.BackColorValue;

                // 連動対象も ON にする
                foreach (VoucherLink obj in v.ChildList)
                {
                    if (panel.Controls.ContainsKey(obj.ChildPos))
                    {
                        if (panel.Controls[obj.ChildPos].GetType().Name.Equals("CheckBox"))
                        {
                            CheckBox cb = (CheckBox)(panel.Controls[obj.ChildPos]);
                            cb.Checked = true;
                        }
                    }
                }

                // グループ化設定されている場合、これが ON になれば、同一グループのチェックボックス・トグルボタンはすべて OFF になる。
                foreach (List<VoucherCompGroup> group_list in v.GroupList)
                {
                    foreach (VoucherCompGroup obj in group_list)
                    {
                        if (obj.Pos.Equals(v.Pos))
                        {
                            // 自分自身の場合は飛ばす
                            continue;
                        }

                        if (panel.Controls.ContainsKey(obj.Pos))
                        {
                            if (panel.Controls[obj.Pos] is CheckBox)
                            {
                                ((CheckBox)(panel.Controls[obj.Pos])).Checked = false;
                            }
                        }
                    }
                }
            }
            else
            {
                tg.ForeColor = v.Attr1.ForeColorValue;

                // 連動対象のうち、他の連動元がすべて OFF ならばチェックを外す
                foreach (VoucherLink obj in v.ChildList)
                {
                    if (panel.Controls.ContainsKey(obj.ChildPos))
                    {
                        if (panel.Controls[obj.ChildPos].GetType().Name.Equals("CheckBox"))
                        {
                            CheckBox cb = (CheckBox)(panel.Controls[obj.ChildPos]);
                            VoucherComp vc = (VoucherComp)(cb.Tag);

                            bool other_on = false;

                            foreach (VoucherLink vp in vc.ParentList)
                            {
                                if (panel.Controls.ContainsKey(vp.Pos))
                                {
                                    if (panel.Controls[vp.Pos].GetType().Name.Equals("CheckBox"))
                                    {
                                        CheckBox cp = (CheckBox)(panel.Controls[vp.Pos]);

                                        if (cp.Checked)
                                        {
                                            other_on = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!other_on)
                            {
                                cb.Checked = false;
                            }
                        }
                    }
                }
            }

            List<VoucherComp> list = (List<VoucherComp>)(panel.Tag);

            this.MovePanel();
        }


        public List<OrderHeader> GetOrderHeaderList()
        {
            List<VoucherIji> iji_list = VoucherIji.GetList(this.Code);


            // OrderPanel に追加するオーダーのリスト
            List<OrderHeader> order_list = new List<OrderHeader>();
            List<OrderDetail> detail_list = new List<OrderDetail>();

            // 区切り連番
            int rp_num = 0;

            // 区分の先頭
            bool rp_first = true;

            foreach (VoucherIji iji in iji_list)
            {
                if (this.Controls.ContainsKey(iji.Pos))
                {
                    // トリガーがあれば、それが ON かどうか確認する。OFF なら continue。
                    if (iji.TriggerList.Count > 0)
                    {
                        bool trigger_on = false;

                        foreach (VoucherIji tr_iji in iji.TriggerList)
                        {
                            Control tr_c = this.Controls[tr_iji.Pos];

                            if (tr_c is Label || tr_c is TextBox || tr_c is ComboBox)
                            {
                                if (tr_c.Text.Length > 0)
                                {
                                    trigger_on = true;
                                    break;
                                }
                            }
                            else if (tr_c is CheckBox)
                            {
                                if (((CheckBox)tr_c).Checked)
                                {
                                    trigger_on = true;
                                    break;
                                }
                            }
                        }

                        if (!trigger_on)
                        {
                            continue;
                        }
                    }

                    Control c = this.Controls[iji.Pos];

                    if (c is Label || c is TextBox || c is ComboBox)
                    {
                        if (c.Text.Length > 0)
                        {
                            VoucherComp comp = (VoucherComp)(c.Tag);
                            OrderDetail order = new OrderDetail();

                            // 区切り連番が前と異なれば、区分の先頭。
                            // →　と思ったが、外科伝票などはそうなっていない。
                            if (!rp_num.Equals(iji.SEQ1))
                            {
                                rp_num = iji.SEQ1;
//                                rp_first = true;
                            }

                            // 区切りの先頭の場合
                            if (rp_first)
                            {
                                // 前のオーダーディティールに回数が入っていなければ、detail_list が残っているので
                                // 新しい OrderHeader を作ってリストに追加した上で detail_list を空にする。
                                if (detail_list.Count > 0)
                                {
                                    OrderHeader tmp_order = new OrderHeader();
                                    tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");
                                    tmp_order.Times = order.Times;

                                    foreach (OrderDetail tmp2 in detail_list)
                                    {
                                        tmp_order.DetailList.Add(tmp2);
                                    }

                                    order_list.Add(tmp_order);

                                    detail_list.Clear();
                                }

                                order.Kind = 1;
                                order.SekouDate = DateTime.Now.ToString("yyyy/MM/dd");
                            }

                            if (comp.Cont1.Code.Length > 0)
                            {
                                order.OrderCode = comp.Cont1.Code;
                                order.OrderName = comp.Cont1.Name;
                            }
                            else
                            {
                                order.OrderCode = "88888888";
                                order.OrderName = c.Text;
                            }

                            // 数量
                            if (iji.QtyRow > 0 || iji.QtyCol > 0)
                            {
                                Control cc = this.Controls[iji.QtyPos];

                                if (cc is Label || cc is TextBox || cc is ComboBox)
                                {
                                    if (cc.Text.Length > 0)
                                    {
                                        float.TryParse(cc.Text, out order.Qty);
                                    }
                                }
                            }
                            else if (iji.QtyValue > 0)
                            {
                                order.Qty = iji.QtyValue;
                            }

                            // 日/回数
                            if (iji.TimesRow > 0 || iji.TimesCol > 0)
                            {
                                Control cc = this.Controls[iji.TimesPos];

                                if (cc is Label || cc is TextBox || cc is ComboBox)
                                {
                                    if (cc.Text.Length > 0)
                                    {
                                        float.TryParse(cc.Text, out order.Times);
                                    }
                                }
                            }
                            else if (iji.TimesValue > 0)
                            {
                                order.Times = iji.TimesValue;
                            }

                            // オーダーコードが存在し、かつ 8888 ではなく
                            // SDCD・診療区分・単位が無い場合はオーダーマスターから取得
                            order.GetFromOrderMaster();

                            // 日/回数が入っていれば、次は区切りの先頭になる。
                            if (order.Times > 0)
                            {
                                rp_first = true;
                            }
                            else
                            {
                                rp_first = false;
                            }


                            // OrderPanel に追加するための処理
                            detail_list.Add(order);

                            if (order.Times > 0)
                            {
                                OrderHeader tmp_order = new OrderHeader();
                                tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");
                                tmp_order.KouiCode = order.SDCD;
                                tmp_order.Times = order.Times;

                                foreach (OrderDetail tmp2 in detail_list)
                                {
                                    tmp_order.DetailList.Add(tmp2);
                                }

                                order_list.Add(tmp_order);

                                detail_list.Clear();
                            }
                        }
                    }
                    else if (c is CheckBox)
                    {
                        CheckBox cb = (CheckBox)c;

                        if (cb.Checked)
                        {
                            VoucherComp comp = (VoucherComp)(c.Tag);
                            OrderDetail order = new OrderDetail();

                            // 区切り連番が前と異なれば、区分の先頭。
                            // →　と思ったが、外科伝票などはそうなっていない。
                            if (!rp_num.Equals(iji.SEQ1))
                            {
                                rp_num = iji.SEQ1;
//                                rp_first = true;
                            }

                            // 区切りの先頭の場合
                            if (rp_first)
                            {
                                // 前のオーダーディティールに回数が入っていなければ、detail_list が残っているので
                                // 新しい OrderHeader を作ってリストに追加した上で detail_list を空にする。
                                if (detail_list.Count > 0)
                                {
                                    OrderHeader tmp_order = new OrderHeader();
                                    tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");
                                    tmp_order.Times = order.Times;

                                    foreach (OrderDetail tmp2 in detail_list)
                                    {
                                        tmp_order.DetailList.Add(tmp2);
                                    }

                                    order_list.Add(tmp_order);

                                    detail_list.Clear();
                                }

                                order.Kind = 1;
                                order.SekouDate = DateTime.Now.ToString("yyyy/MM/dd");
                            }

                            order.OrderCode = comp.Cont1.Code;
                            order.OrderName = comp.Cont1.Name;

                            // 数量
                            if (iji.QtyRow > 0 || iji.QtyCol > 0)
                            {
                                Control cc = this.Controls[iji.QtyPos];

                                if (cc is Label || cc is TextBox || cc is ComboBox)
                                {
                                    if (cc.Text.Length > 0)
                                    {
                                        float.TryParse(cc.Text, out order.Qty);
                                    }
                                }
                            }
                            else if (iji.QtyValue > 0)
                            {
                                order.Qty = iji.QtyValue;
                            }

                            // 日/回数
                            if (iji.TimesRow > 0 || iji.TimesCol > 0)
                            {
                                Control cc = this.Controls[iji.TimesPos];

                                if (cc is Label || cc is TextBox || cc is ComboBox)
                                {
                                    if (cc.Text.Length > 0)
                                    {
                                        float.TryParse(cc.Text, out order.Times);
                                    }
                                }
                            }
                            else if (iji.TimesValue > 0)
                            {
                                order.Times = iji.TimesValue;
                            }

                            // オーダーコードが存在し、かつ 8888 ではなく
                            // SDCD・診療区分・単位が無い場合はオーダーマスターから取得
                            order.GetFromOrderMaster();

                            // 日/回数が入っていれば、次は区切りの先頭になる。
                            if (order.Times > 0)
                            {
                                rp_first = true;
                            }
                            else
                            {
                                rp_first = false;
                            }


                            // OrderPanel に追加するための処理
                            detail_list.Add(order);

                            if (order.Times > 0)
                            {
                                OrderHeader tmp_order = new OrderHeader();
                                tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");
                                tmp_order.Times = order.Times;

                                foreach (OrderDetail tmp2 in detail_list)
                                {
                                    tmp_order.DetailList.Add(tmp2);
                                }

                                order_list.Add(tmp_order);

                                detail_list.Clear();
                            }
                        }
                    }
                }

                // 医事データ設定で、回数が設定されていれば、次は区切りの先頭になる。
                if (iji.TimesCol > 0 || iji.TimesRow > 0 || iji.TimesValue > 0)
                {
                    rp_first = true;
                }
            }

            // detail_list に残っているオーダーをリストに追加
            if (detail_list.Count > 0)
            {
                OrderHeader tmp_order = new OrderHeader();
                tmp_order.SekouDate = DateTime.Now.ToString("yyyyMMdd");

                foreach (OrderDetail tmp2 in detail_list)
                {
                    tmp_order.DetailList.Add(tmp2);
                }

                if (tmp_order.Times == 0)
                {
                    tmp_order.Times = 1;
                }

                order_list.Add(tmp_order);

                detail_list.Clear();
            }

            // 内容クリア

            foreach (VoucherIji iji in iji_list)
            {
                if (this.Controls.ContainsKey(iji.Pos))
                {
                    Control c = this.Controls[iji.Pos];

                    if (c is TextBox || c is ComboBox)
                    {
                        c.Text = "";
                    }
                    else if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }

                    // 数量
                    if (iji.QtyRow > 0 || iji.QtyCol > 0)
                    {
                        Control cc = this.Controls[iji.QtyPos];

                        if (cc is Label || cc is TextBox || cc is ComboBox)
                        {
                            cc.Text = "";
                        }
                    }

                    // 日/回数
                    if (iji.TimesRow > 0 || iji.TimesCol > 0)
                    {
                        Control cc = this.Controls[iji.TimesPos];

                        if (cc is Label || cc is TextBox || cc is ComboBox)
                        {
                            cc.Text = "";
                        }
                    }
                }
            }


            return order_list;
        }
    }
}
