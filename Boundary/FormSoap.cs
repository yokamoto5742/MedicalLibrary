using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormSoap : StdForm1
    {
        bool _ReadOnly = true;

        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;

                if (value)
                {
                    this.Width = 850;
                    this.stdControlPat11.ReadOnly = true;
                    this.SoapWrite1.Visible = false;
                    this.KarteTemplateButton1.Visible = false;
                    this.ClearButton1.Visible = false;
                    this.SaveButton1.Visible = false;
                    this.SoapDatePanel.Location = new Point(5, this.SoapDatePanel.Location.Y);
                    this.SoapDataPanel.Location = new Point(230, this.SoapDataPanel.Location.Y);
                    this.SoapDataPanel.Width = 600;
                }
                else
                {
                    this.Width = 1200;
                    this.stdControlPat11.ReadOnly = false;
                    this.SoapWrite1.Visible = true;
                    this.KarteTemplateButton1.Visible = true;
                    this.ClearButton1.Visible = true;
                    this.SaveButton1.Visible = true;
                    this.SoapDatePanel.Location = new Point(455, this.SoapDatePanel.Location.Y);
                    this.SoapDataPanel.Location = new Point(680, this.SoapDataPanel.Location.Y);
                    this.SoapDataPanel.Width = 500;
                }
            }
        }

        ToolTip tp1 = new ToolTip();

        Dictionary<string, Panel> soapPanelDict = new Dictionary<string, Panel>();

        FormKarteTemplate1 formKarteTemplate1;
//        FormPdfBrowser1 formPdfBrowser1;

        public FormSoap(bool read_only = true)
        {
            InitializeComponent();

            this.ReadOnly = read_only;
        }

        private void FormSoap_Load(object sender, EventArgs e)
        {
            this.SoapWrite1.Init();
            this.SoapWrite1.InOut = "1";
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.DataShow(1, 10);
            this.SoapWrite1.PatSet(p.Id);
        }

        void DataShow(int start, int end)
        {
            string pt_id = this.Pat.Id;

            // 一覧をクリアする
            soapPanelDict.Clear();
            SoapDatePanel.Controls.Clear();
            SoapDataPanel.Controls.Clear();
            SoapWrite1.Clear();

//            this.PdfBrowserInit();

            // 日付一覧を取得する
            Dictionary<string, List<SoapDate>> date_dict = SoapDate.GetDict(pt_id, this.CritDate1.Value.ToString("yyyyMMdd"));

            int c = 0;
            List<string> date_list = new List<string>();

            // SOAP一覧のチェックボックスを生成する
            foreach (string d in date_dict.Keys)
            {
                c++;

                if (c < start)
                {
                    continue;
                }

                if (c > end)
                {
                    break;
                }

                date_list.Add(d);
            }

            c = 0;
            int h = 0;
            string tmp_year = "";

            // SOAP一覧のチェックボックスを生成する
            foreach (string d in date_list)
            {
                // SoapDatePanel に登録日を生成

                // 年ラベルを作成する
                if (!tmp_year.Equals(d.Substring(0, 4)))
                {
                    h += 10;
                    tmp_year = d.Substring(0, 4);
                    Label lb = new Label();
                    lb.AutoSize = true;
                    lb.Text = tmp_year + " 年";
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.Location = new Point(10, h);
                    SoapDatePanel.Controls.Add(lb);
                    h += 15;
                }

                bool inout_flg1 = false;
                bool inout_flg2 = false;
                List<string> dept_list = new List<string>();

                string dept_text = "";

                foreach (SoapDate obj in date_dict[d])
                {
                    if (obj.InOut.Equals(1)) inout_flg1 = true;
                    if (obj.InOut.Equals(2)) inout_flg2 = true;

                    if (!dept_list.Contains(obj.Dept))
                    {
                        dept_list.Add(obj.Dept);

                        if (obj.DeptName.Length > 0)
                        {
                            dept_text += obj.DeptName[0] + " ";
                        }
                    }
                }

                // SOAP日のチェックボックスを生成
                CheckBox cb = new CheckBox();
                cb.Name = AppDateTime.DateStringFromString(d);
                cb.Text = AppDateTime.DateWeekdayStringFromString(d);
                cb.Location = new Point(20, h);
                cb.Width = 120;
                cb.Tag = d;

                // 入外
                Label l1 = new Label();
                l1.Size = new Size(38, 19);
                l1.TextAlign = ContentAlignment.MiddleCenter;
                l1.Location = new Point(140, h);
//                l1.BorderStyle = BorderStyle.FixedSingle;

                if (inout_flg1 && inout_flg2)
                {
                    l1.Text = "入外";
                    l1.BackColor = Color.Red;
                }
                else if (inout_flg1)
                {
                    l1.Text = "外";
                    l1.BackColor = Color.LightGreen;
                }
                else if (inout_flg2)
                {
                    l1.Text = "入";
                    l1.BackColor = Color.LightPink;
                }
                else
                {
                    l1.Text = "他";
                    l1.BackColor = Color.White;
                }

                // 診療科
                Label l2 = new Label();
                l2.Size = new Size(100, 20);
                l2.Text = dept_text;
                l2.TextAlign = ContentAlignment.MiddleLeft;
                l2.AutoEllipsis = true;
                l2.Location = new Point(180, h);


                // 初期状態で直近３回分まで表示されている
                if (c < 4)
                {
                    cb.Checked = true;
                }

                cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
                SoapDatePanel.Controls.Add(cb);
                SoapDatePanel.Controls.Add(l1);
                SoapDatePanel.Controls.Add(l2);
                h += 20;
                c++;

            }

            this.DateShow();
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            this.DateShow();
        }

        /// <summary>
        /// 選択された日を表示する
        /// </summary>
        void DateShow()
        {
            string pt_id = this.Pat.Id;

            List<string> date_list = new List<string>();

            foreach (Control c in SoapDatePanel.Controls)
            {
                if (c.GetType().Name.Equals("CheckBox"))
                {
                    CheckBox cb = (CheckBox)c;

                    if (!cb.Checked) continue;

                    date_list.Add(cb.Tag.ToString());
                }
            }

            this.SoapDataPanel.Controls.Clear();

            // SOAPヘッダ一覧を取得する
            Dictionary<string, List<SoapHeader>> soap_header_dict = SoapHeader.GetDict(pt_id, date_list);

#if INNO
#else
            // SOAPプロブレム一覧を取得する
            Dictionary<string, List<SoapProblem>> problem_dict = SoapProblem.GetDict(pt_id, date_list);
#endif

            // Orderデータ一覧を取得する
            Dictionary<string, Dictionary<string, SoapOrderHeader>> order_dict = SoapOrderHeader.GetDict(pt_id, date_list);

            // Pdfデータ一覧を取得する
            Dictionary<string, Dictionary<string, PdfDoc>> pdf_dict = PdfDoc.GetDict(pt_id, date_list);

            int ph = 0;
            int lh = 0;

            foreach (string date in date_list)
            {
                if (!soapPanelDict.ContainsKey(date))
                {
                    Panel tmp_p = new Panel();
                    tmp_p.Name = date;
                    tmp_p.BackColor = Color.White;
                    tmp_p.Width = SoapDataPanel.Width - 30;

                    soapPanelDict.Add(date, tmp_p);
                }

                Panel p = soapPanelDict[date];
                lh = 0;

                // SOAPを表示する
                if (soap_header_dict.ContainsKey(date))
                {
                    foreach (SoapHeader header in soap_header_dict[date])
                    {
                        Label lb_title = new Label();
                        lb_title.Tag = header;
                        lb_title.BackColor = Color.LightYellow;
                        lb_title.BorderStyle = BorderStyle.None;
                        lb_title.TextAlign = ContentAlignment.MiddleLeft;
                        lb_title.Font = AppFont.FN10.Ft;
                        lb_title.Location = new Point(0, lh);

                        if (lb_title.PreferredWidth < p.Width)
                        {
                            lb_title.Width = p.Width;
                        }
                        else
                        {
                            lb_title.Width = lb_title.PreferredWidth;
                        }

                        lh += lb_title.Height;

                        // プロブレムがあれば表示する
#if INNO
                        if (header.Problem.Length > 0)
                        {
                            Label lb = new Label();
                            lb.BackColor = Color.White;
                            lb.BorderStyle = BorderStyle.None;
                            lb.Padding = new Padding(3, 3, 3, 3);
                            lb.Font = AppFont.FN10.Ft;
                            lb.Location = new Point(40, lh);
                            lb.Width = p.Width - 40;

                            lb.Text = AppString.Wrap(header.Problem, lb.Font.Size, lb.Width);
                            lb.Height = lb.PreferredHeight + 5;

                            lh += lb.Height;

                            lb.MouseClick += new MouseEventHandler(lb_MouseClick);

                            p.Controls.Add(lb);
                        }
#else
                        if (problem_dict.ContainsKey(header.Key))
                        {
                            foreach (SoapProblem problem in problem_dict[header.Key])
                            {
                                Label lb = new Label();
                                lb.BackColor = Color.White;
                                lb.BorderStyle = BorderStyle.None;
                                lb.Padding = new Padding(3, 3, 3, 3);
                                lb.Font = AppFont.FN10.Ft;
                                lb.Location = new Point(40, lh);
                                lb.Width = p.Width - 40;

                                lb.Text = problem.ContShow(lb.Font.Size, lb.Width);
                                lb.Height = lb.PreferredHeight + 5;

                                Label lb_kind = new Label();
                                lb_kind.BackColor = Color.LightYellow;
                                lb_kind.BorderStyle = BorderStyle.None;
                                lb_kind.Text = "#" + problem.ProblemCode;
                                lb_kind.TextAlign = ContentAlignment.TopCenter;
                                lb_kind.Padding = new Padding(3, 3, 3, 3);
                                lb_kind.Font = AppFont.FN10.Ft;
                                lb_kind.Location = new Point(0, lh);

                                lb_kind.Width = 40;
                                lb_kind.Height = lb.Height;

                                lh += lb.Height;

                                lb_kind.MouseClick += new MouseEventHandler(lb_MouseClick);
                                lb.MouseClick += new MouseEventHandler(lb_MouseClick);

                                p.Controls.Add(lb_kind);
                                p.Controls.Add(lb);
                            }
                        }
#endif
                        // 背景色
                        if (header.InOut.Equals(1))
                        {
                            lb_title.BackColor = Color.LightGreen;
                        }
                        else if (header.InOut.Equals(2))
                        {
                            lb_title.BackColor = Color.LightPink;
                        }
                        else
                        {
                            lb_title.BackColor = Color.LightYellow;
                        }

                        // 科・更新者・更新日時
                        lb_title.Text = AppDateTime.DateWeekdayStringFromLong(header.SoapDate) + " 経過記録 " +
                            header.DeptName + " " + header.RegStaffName + " " + AppDateTime.TimeStringFromInt6(header.RegTime);

                        // 修正があった場合
                        if (header.UpStaffName.Length > 0)
                        {
                            lb_title.Text += "（" + header.UpStaffName + " " + AppDateTime.TimeStringFromInt6(header.UpTime) + "）";
                        }

                        // 個々のSOAPを表示する
                        foreach (SoapDetail soap_data in header.DetailList)
                        {
                            // Secret は記載者以外には表示しない
                            if (soap_data.Kind.Equals("11"))
                            {
                                if (!soap_data.RegStaff.Equals(LoginUser.Id))
                                {
                                    continue;
                                }
                            }

                            Label lb_kind = new Label();
                            lb_kind.BackColor = Color.LightYellow;
                            lb_kind.BorderStyle = BorderStyle.None;
                            lb_kind.Text = soap_data.KindName;
                            lb_kind.TextAlign = ContentAlignment.TopCenter;
                            lb_kind.Padding = new Padding(3, 3, 3, 3);
                            lb_kind.Font = AppFont.FN10.Ft;
                            lb_kind.Location = new Point(0, lh);

                            lb_kind.Width = 40;

                            int tmp_lh = lh;

                            Label lb = new Label();
                            lb.BackColor = Color.White;
                            lb.BorderStyle = BorderStyle.None;
                            lb.Padding = new Padding(3, 3, 3, 3);
                            lb.Font = AppFont.FN10.Ft;
                            lb.Location = new Point(40, lh);
                            lb.Width = p.Width - 40;
                           
                            // Hide は初期表示しない
                            if (soap_data.Kind.Equals("10"))
                            {
                                tp1.SetToolTip(lb, soap_data.Cont);
                            }
                            else
                            {
                                lb.Text = soap_data.ContShow(lb.Font.Size, lb.Width);
                                lb.Height = lb.PreferredHeight + 5;
                            }

                            lh += lb.Height;

                            int img_x = 0;

                            // 図があれば表示する
                            if (soap_data.ImgExist)
                            {
                                foreach (SoapImg img in soap_data.ImgDict.Values)
                                {
                                    if (img.ImgExist)
                                    {
                                        PictureBox pb = new PictureBox();
                                        pb.Image = Image.FromFile(img.ImgPath);
                                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                                        pb.Location = new Point(40 + img_x, lh);
                                        pb.Height = 80;
                                        pb.Tag = img.ImgPath;
                                        pb.MouseClick += new MouseEventHandler(pb_MouseClick);
                                        pb.MouseDoubleClick += new MouseEventHandler(pb_MouseDoubleClick);
                                        img_x += pb.Width + 5;

                                        if (img_x > p.Width - 40)
                                        {
                                            img_x = 0;
                                            lh += 85;
                                        }

                                        p.Controls.Add(pb);
                                    }
                                }

                                lh += 85;
                            }

                            lb_kind.Height = lh - tmp_lh;

                            //                            lh += lb_kind.Height;

                            lb_kind.MouseClick += new MouseEventHandler(lb_MouseClick);
                            lb.MouseClick += new MouseEventHandler(lb_MouseClick);

                            p.Controls.Add(lb_kind);
                            p.Controls.Add(lb);
                        }

                        lb_title.MouseClick += new MouseEventHandler(lb_MouseClick);
                        lb_title.ContextMenuStrip = this.SoapMenuStrip1;

                        p.Controls.Add(lb_title);
                    }
                }

                // オーダーを表示する
                if (order_dict.ContainsKey(date))
                {
                    foreach (string order_key in order_dict[date].Keys)
                    {
                        SoapOrderHeader order = order_dict[date][order_key];

                        Label lb_title = new Label();
                        lb_title.BackColor = Color.LightYellow;
                        lb_title.BorderStyle = BorderStyle.None;
                        lb_title.TextAlign = ContentAlignment.MiddleLeft;
                        lb_title.Font = AppFont.FN10.Ft;
                        lb_title.Location = new Point(0, lh);

                        if (lb_title.PreferredWidth < p.Width)
                        {
                            lb_title.Width = p.Width;
                        }
                        else
                        {
                            lb_title.Width = lb_title.PreferredWidth;
                        }

                        if (order.InOut.Equals(1))
                        {
                            lb_title.BackColor = Color.LightGreen;
                        }
                        else if (order.InOut.Equals(2))
                        {
                            lb_title.BackColor = Color.LightPink;
                        }
                        else
                        {
                            lb_title.BackColor = Color.LightYellow;
                        }

                        lb_title.Text = AppDateTime.DateWeekdayStringFromLong(order.Date) + " オーダー " +
                            order.KouiName + " " + order.DeptName + " " + order.StaffName;

                        lh += lb_title.Height;

                        Label lb = new Label();
                        lb.AutoSize = false;
                        lb.BackColor = Color.LightCyan;
                        lb.BorderStyle = BorderStyle.None;
                        lb.Padding = new Padding(3, 3, 3, 3);
                        lb.Font = AppFont.FN10.Ft;
                        lb.Location = new Point(40, lh);
                        lb.Width = p.Width - 40;

                        lb.Text = order.SoapShow(lb.Font.Size, lb.Width);
                        lb.Height = lb.PreferredHeight + 5;

                        Label lb_kind = new Label();
                        lb_kind.BackColor = Color.LightYellow;
                        lb_kind.BorderStyle = BorderStyle.None;
                        lb_kind.Text = order.KouiName;
                        lb_kind.TextAlign = ContentAlignment.TopCenter;
                        lb_kind.Padding = new Padding(3, 3, 3, 3);
                        lb_kind.Font = AppFont.FN10.Ft;
                        lb_kind.Location = new Point(0, lh);

                        lb_kind.Width = 40;
                        lb_kind.Height = lb.Height;

                        lh += lb.Height;

                        lb_title.MouseClick += new MouseEventHandler(lb_MouseClick);
                        lb_kind.MouseClick += new MouseEventHandler(lb_MouseClick);
                        lb.MouseClick += new MouseEventHandler(lb_MouseClick);

                        p.Controls.Add(lb_title);
                        p.Controls.Add(lb_kind);
                        p.Controls.Add(lb);
                    }
                }

                // PDFを表示する
                if (pdf_dict.ContainsKey(date))
                {
                    foreach (string pdf_key in pdf_dict[date].Keys)
                    {
                        PdfDoc pdf = pdf_dict[date][pdf_key];

                        Label lb_title = new Label();
                        lb_title.BackColor = Color.LightYellow;
                        lb_title.BorderStyle = BorderStyle.None;
                        lb_title.TextAlign = ContentAlignment.MiddleLeft;
                        lb_title.Font = AppFont.FN10.Ft;
                        lb_title.Location = new Point(0, lh);

                        if (lb_title.PreferredWidth < p.Width)
                        {
                            lb_title.Width = p.Width;
                        }
                        else
                        {
                            lb_title.Width = lb_title.PreferredWidth;
                        }

                        if (pdf.PdfFileExist)
                        {
                            lb_title.BackColor = Color.Yellow;
                        }
                        else
                        {
                            lb_title.BackColor = Color.Gray;
                        }

                        lb_title.Text = AppDateTime.DateWeekdayStringFromLong(pdf.PdfDate) + " PDF " +
                            pdf.PdfName + " " + pdf.DeptName + " " + pdf.StaffName;

                        if (pdf.PdfFileExist)
                        {
                            lb_title.Tag = pdf.PdfFilePath;
                            lb_title.MouseDoubleClick += new MouseEventHandler(lb_title_MouseDoubleClick);
                        }

                        lh += lb_title.Height;

                        lb_title.MouseClick += new MouseEventHandler(lb_MouseClick);

                        p.Controls.Add(lb_title);
                    }
                }

                p.Height = lh;
                p.Location = new Point(0, ph);
                ph += p.Height;

                this.SoapDataPanel.Controls.Add(p);
            }
        }

        void pb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 図ファイルを表示する
            PictureBox pb = (PictureBox)sender;

            if (pb.Tag.ToString().Length > 0)
            {
                this.PdfBrowserNavigate(pb.Tag.ToString());
            }
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            // マウスホイールで上下スクロールできるようにする
            SoapDataPanel.Focus();
        }

        void lb_MouseClick(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;

            // マウスホイールで上下スクロールできるようにする
            SoapDataPanel.Focus();
        }

        void lb_title_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // PDFファイルを表示する
            Label lb = (Label)sender;

            if (lb.Tag.ToString().Length > 0)
            {
                this.PdfBrowserNavigate(lb.Tag.ToString());
            }
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            this.DataShow(1, 10);
        }

        private void SoapDataPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // マウスホイールで上下スクロールできるようにする
            SoapDataPanel.Focus();
        }

        private void KarteTemplateButton1_Click(object sender, EventArgs e)
        {
            if (formKarteTemplate1 == null || !formKarteTemplate1.Created)
            {
                formKarteTemplate1 = new FormKarteTemplate1();
                formKarteTemplate1.Init(this.SoapWrite1);
            }

            LibUtility.FormShow(formKarteTemplate1);
        }
/*
        void PdfBrowserInit()
        {
            if (formPdfBrowser1 == null || !formPdfBrowser1.Created)
            {
                formPdfBrowser1 = new FormPdfBrowser1();
            }

            formPdfBrowser1.Init();
        }
*/
        void PdfBrowserNavigate(string url)
        {
            /*
            if (formPdfBrowser1 == null || !formPdfBrowser1.Created)
            {
                formPdfBrowser1 = new FormPdfBrowser1();
            }

            formPdfBrowser1.Navigate(url);
             */

            FormPDFViewer1 f = new FormPDFViewer1(FormPDFViewer1.PageOrientation.Portrait);
            f.Navigate(url);
            f.Show();
        }

        private void ModifyMenuItem1_Click(object sender, EventArgs e)
        {
            Label lb = (Label)this.SoapMenuStrip1.SourceControl;

            this.SoapWrite1.DataShow((SoapHeader)lb.Tag, false);
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            this.SoapWrite1.Save();
            this.DataShow(1, 10);
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            this.SoapWrite1.Clear();
        }
    }
}
