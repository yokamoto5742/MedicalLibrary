using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormOpeNursingPat : StdForm1
    {
        FormOpeNursingList f1;
        FormOpeNursingAs fa1;
        public DataSet mSet;
        DataSet dSet;
        string mode;
        string ptId;

        OpeNursingData _OpeNursingData = new OpeNursingData();

        public Dictionary<string, string> asTitleDict;

        string[] in_out_array = { "", "外", "入" };
        string[] time_out_array = { "", "緊内", "緊外" };
        string[] rec_kind_array = { "", "術前", "術中", "術後" };
        string[] status_array = { "", "●", "" };

        // 以下はシェーマ描画用
        Graphics g;
        int gMode = 0;   // 0 選択, 1 直線, 2 楕円, 3 四角, 4 文字, 5 フリー

        // (gStart, gEnd)
        // (0, 0) 非描画状態
        // (1, 0) 描画中（始点のみ確定されている）
        // (0, 1) 描画中（終点のみ確定されている）
        // (1, 1) 始点・終点が確定された状態（すぐに非描画状態に戻す。実際は存在しないはず）
        int gStart = 0;
        int gEnd = 0;

        int gStartX;
        int gStartY;
        int gEndX;
        int gEndY;
        Pen gPen;
        Color gColor;
        string gText = "";  // 選択されているテキストボックスの文字列
        int gSchema = -1;   // 選択されているシェーマアイテムの番号
        int gFree = -1;     // 選択されているフリーアイテムの番号
        FreeItem gFreeItem;    // テンポラリのフリーアイテム

        int gMarginX = -1;  // 楕円選択時、クリック点と始点・終点の差
        int gMarginY = -1;

        /// <summary>
        /// SchemaBox 上に記述されたアイテムのクラス。
        /// </summary>
        public class SchemaItem
        {
            public string Kind;
            public Color C;
            public int StartX;
            public int StartY;
            public int EndX;
            public int EndY;
            public string Text;
            public Point[] Points;

            public int GetByteCount()
            {
                string str = Kind + "," + C.Name + "," + StartX + "," + StartY + "," + EndX + "," + EndY;

                if (Text != null)
                {
                    str += "," + Text.Replace("\r\n", "<CR+LF>") + "\r\n";
                }

                return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str);
            }
        }

        public class FreeItem
        {
            public string Kind;
            public Color C;
            public List<Point> PointList = new List<Point>();

            public FreeItem()
            {
                Kind = "フリー";
                C = Color.White;
            }

            public void Clear()
            {
                Kind = "フリー";
                C = Color.White;
                PointList.Clear();
            }

            public int GetByteCount()
            {
                string str = Kind + "," + C.Name;

                foreach (Point p in PointList)
                {
                    str += "," + p.X + "," + p.Y;
                }

                str += "\r\n";

                return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str);
            }
        }

        List<SchemaItem> schemaItemList = new List<SchemaItem>();
        List<FreeItem> freeItemList = new List<FreeItem>();
        Dictionary<PictureBox, Color> schemaColorDict = new Dictionary<PictureBox, Color>();

        public struct SchemaTag
        {
            public string Id;
            public string Bg;
            public string Item;
        }

        List<SchemaTag> SchemaTagList = new List<SchemaTag>();

        private int GetSchemaByteCount()
        {
            string s = "";

            foreach (SchemaItem si in schemaItemList)
            {
                s += si.Kind + "," + si.C.Name + "," + si.StartX + "," + si.StartY + "," + si.EndX + "," + si.EndY;

                if (si.Kind.Equals("文字") && si.Text != null)
                {
                    s += si.Text.Replace("\r\n", "<CR+LF>");
                }

                s += "\r\n";
            }

            foreach (FreeItem fi in freeItemList)
            {
                s += fi.Kind + "," + fi.C.Name;

                foreach (Point p in fi.PointList)
                {
                    s += "," + p.X + "," + p.Y;
                }

                s += "\r\n";
            }

            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(s);
        }

        public FormOpeNursingPat()
        {
            InitializeComponent();
        }

        public FormOpeNursingPat(FormOpeNursingList F1, string PtId)
        {
            InitializeComponent();

            this.f1 = F1;
            this.ptId = PtId;
            this.ptIdBox.Text = PtId;
            this.asTitleDict = F1.asTitleDict;
        }

        private void FormOpeNursingPat_Load(object sender, EventArgs e)
        {
            recKindBox.Items.Add("");

            for (int k = 1; k < rec_kind_array.Length; k++ )
            {
                recKindBox.Items.Add(k + " " + rec_kind_array[k]);
            }

            mSet = f1.mSet;
            dSet = new DataSet();

            DataTable table = mSet.Tables["Dept"];

            deptBox.Items.Add("");

            foreach (DataRow tmpRow in table.Rows)
            {
                if (!this.deptBox.Items.Contains(tmpRow.ItemArray[0].ToString()))
                {
                    this.deptBox.Items.Add(tmpRow.ItemArray[0].ToString());
                }
            }

            table = mSet.Tables["Ns"];

            string[] ns1 = table.Rows[0].ItemArray[0].ToString().Split(',');
            string[] ns2 = table.Rows[0].ItemArray[1].ToString().Split(',');
            string[] ns3 = table.Rows[0].ItemArray[2].ToString().Split(',');

            foreach (string s1 in ns1)
            {
                if (!this.nsBox1.Items.Contains(s1))
                {
                    this.nsBox1.Items.Add(s1);
                }
            }

            foreach (string s2 in ns2)
            {
                if (!this.nsBox2.Items.Contains(s2))
                {
                    this.nsBox2.Items.Add(s2);
                }
            }

            foreach (string s3 in ns3)
            {
                if (!this.nsBox3.Items.Contains(s3))
                {
                    this.nsBox3.Items.Add(s3);
                }
            }

            table = mSet.Tables["OpeRoom"];

            roomBox.Items.Add("");

            foreach (DataRow tmpRow in table.Rows)
            {
                if (!this.roomBox.Items.Contains(tmpRow.ItemArray[1].ToString()))
                {
                    this.roomBox.Items.Add(tmpRow.ItemArray[1].ToString());
                }
            }

            table = dSet.Tables.Add("手術歴");
            table.Columns.Add("ID");
            table.Columns.Add("INS");
            table.Columns.Add("保険");
            table.Columns.Add("OPE_DATE");
            table.Columns.Add("IN_OUT");
            table.Columns.Add("入外");
            table.Columns.Add("TIME_OUT");
            table.Columns.Add("緊急");
            table.Columns.Add("REC_KIND");
            table.Columns.Add("種別");
            table.Columns.Add("DEPT");
            table.Columns.Add("診療科");
            table.Columns.Add("OPE");
            table.Columns.Add("PART");
            table.Columns.Add("ANES");
            table.Columns.Add("ANES_TIME1");
            table.Columns.Add("ANES_TIME2");
            table.Columns.Add("OPE_TIME1");
            table.Columns.Add("OPE_TIME2");
            table.Columns.Add("ROOM_TIME1");
            table.Columns.Add("ROOM_TIME2");
            table.Columns.Add("DOCTOR1");
            table.Columns.Add("DOCTOR2");
            table.Columns.Add("DOCTOR3");
            table.Columns.Add("NS1");
            table.Columns.Add("NS2");
            table.Columns.Add("NS3");
            table.Columns.Add("ROOM");
            table.Columns.Add("NS_REC");
            table.Columns.Add("INFECTION");

            table.Columns.Add("REC_HIST");

            table.Columns.Add("STAFF");
            table.Columns.Add("看護師");
            table.Columns.Add("STATUS");
            table.Columns.Add("完成");
            table.Columns.Add("PDF_SAVE");

            table.Columns.Add("Obj", typeof(OpeNursingData));

            schemaColorDict.Add(this.colorBox1, this.colorBox1.BackColor);
            schemaColorDict.Add(this.colorBox2, this.colorBox2.BackColor);
            schemaColorDict.Add(this.colorBox3, this.colorBox3.BackColor);
            schemaColorDict.Add(this.colorBox4, this.colorBox4.BackColor);

            this.schemaBox.Image = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.g = Graphics.FromImage(schemaBox.Image);

            this.drawButton0.Checked = true;
            this.gMode = 0;
            this.gPen = new Pen(this.colorBox1.BackColor, 2);
            this.gColor = this.colorBox1.BackColor;
            this.colorBox1.BorderStyle = BorderStyle.Fixed3D;

            this.changeMode("新規");

            this.makePtData();
        }

        private void clearData()
        {
            this._OpeNursingData = new OpeNursingData();

            this.recordIdBox.Clear();
            this.opeDate.Value = DateTime.Now;
            this.infectionBox.Clear();
            this.inOutButton1.Checked = true;
            this.inOutButton2.Checked = false;
            this.timeOutButton0.Checked = true;
            this.timeOutButton1.Checked = false;
            this.timeOutButton2.Checked = false;
            this.recKindBox.Text = "";
            this.deptBox.Text = "";
            this.opeBox.Text = "";
            this.partBox.Text = "";
            this.anesBox.Text = "";
            this.anesTimeBox1.Clear();
            this.anesTimeBox2.Clear();
            this.opeTimeBox1.Clear();
            this.opeTimeBox2.Clear();
            this.roomTimeBox1.Clear();
            this.roomTimeBox2.Clear();
            this.doctorBox1.Text = "";
            this.doctorBox2.Text = "";
            this.doctorBox3.Text = "";
            this.nsBox1.Text = "";
            this.nsBox2.Text = "";
            this.nsBox3.Text = "";
            this.roomBox.Text = "";
            this.insBox.Text = "";
            this.recText.Clear();
            this.schemaIdBox.Clear();
            this.schemaBgBox.Clear();
            this.schemaItemList.Clear();
            this.freeItemList.Clear();
            this.SchemaTagList.Clear();
            this.schemaPanel.Controls.Clear();
            this.redrawSchema();
            this.staffBox.Clear();
            this.saveDateTimeBox.Clear();
            this.completeBox.Checked = false;
            this.completeBox.Enabled = true;
            this.pdfBox.Checked = false;

            this.recHistBox.Items.Clear();

            this.asTabControl.Controls.Clear();

            // 患者IDが入っていれば感染症データを表示する
            if (this.ptIdBox.Text.Length > 0)
            {
                this.infectionBox.Text = InfectionData.GetInfectionData(this.ptIdBox.Text).ResultString;
            }
        }

        private void clearSchemas()
        {
            this.schemaIdBox.Clear();
            this.schemaBgBox.Clear();
            this.schemaItemList.Clear();
            this.freeItemList.Clear();

            gSchema = -1;
            gFree = -1;
            gMode = 0;
            drawButton0.Checked = true;
            gText = "";
            gMarginX = -1;
            gMarginY = -1;

            this.redrawSchema();

            foreach (PictureBox p in schemaPanel.Controls)
            {
                p.BorderStyle = BorderStyle.None;
            }
        }

        private void deptBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.opeBox.Items.Clear();
            this.partBox.Items.Clear();
            this.anesBox.Items.Clear();

            DataTable table = mSet.Tables["Dept"];

            foreach (DataRow tmpRow in table.Rows)
            {
                if (tmpRow.ItemArray[0].ToString() == this.deptBox.Text)
                {
                    string[] s1 = tmpRow.ItemArray[4].ToString().Split('\n');

                    foreach (string s in s1)
                    {
                        if (s.Contains("="))
                        {
                            this.opeBox.Items.Add(s.Split('=')[0]);
                        }
                    }

                    break;
                }
            }
        }

        private void opeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.partBox.Items.Clear();
            this.partBox.Text = "";
            this.anesBox.Items.Clear();
            this.anesBox.Text = "";
            this.doctorBox1.Items.Clear();
            this.doctorBox1.Text = "";
            this.doctorBox2.Items.Clear();
            this.doctorBox2.Text = "";
            this.doctorBox3.Items.Clear();
            this.doctorBox3.Text = "";
            this.nsBox1.Text = "";
            this.nsBox2.Text = "";
            this.nsBox3.Text = "";

            DataTable table = mSet.Tables["Dept"];

            foreach (DataRow tmpRow in table.Rows)
            {
                if (tmpRow.ItemArray[0].ToString() == this.deptBox.Text)
                {
                    string[] s4 = tmpRow.ItemArray[4].ToString().Split('\n');

                    foreach (string s in s4)
                    {
                        if (s.Split('=')[0].Equals(this.opeBox.Text))
                        {
                            string[] ss = s.Split('=')[1].Trim('\r').Split(',');

                            // 部位リスト
                            if (ss[0].Contains("|"))
                            {
                                foreach (string sss in ss[0].Split('|'))
                                {
                                    this.partBox.Items.Add(sss);
                                }

                                this.partBox.SelectedIndex = 0;
                            }
                            else
                            {
                                this.partBox.Text = ss[0];
                            }

                            // 麻酔リスト
                            if (ss[1].Contains("|"))
                            {
                                foreach (string sss in ss[1].Split('|'))
                                {
                                    this.anesBox.Items.Add(sss);
                                }

                                this.anesBox.SelectedIndex = 0;
                            }
                            else
                            {
                                this.anesBox.Text = ss[1];
                            }

                            // 執刀医
                            if (ss[2].Contains("|"))
                            {
                                foreach (string sss in ss[2].Split('|'))
                                {
                                    this.doctorBox1.Items.Add(sss);
                                }

                                this.doctorBox1.SelectedIndex = 0;
                            }
                            else
                            {
                                this.doctorBox1.Text = ss[2];
                            }

                            // 助手医
                            if (ss[3].Contains("|"))
                            {
                                foreach (string sss in ss[3].Split('|'))
                                {
                                    this.doctorBox2.Items.Add(sss);
                                }

                                this.doctorBox2.SelectedIndex = 0;
                            }
                            else
                            {
                                this.doctorBox2.Text = ss[3];
                            }

                            // 麻酔医
                            if (ss[4].Contains("|"))
                            {
                                foreach (string sss in ss[4].Split('|'))
                                {
                                    this.doctorBox3.Items.Add(sss);
                                }

                                this.doctorBox3.SelectedIndex = 0;
                            }
                            else
                            {
                                this.doctorBox3.Text = ss[4];
                            }

                            break;
                        }
                    }

                    break;
                }
            }
        }

        private void ptIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ptId = this.ptIdBox.Text;
                this.changeMode("新規");
                this.makePtData();
            }
        }

        private void makePtData()
        {
            this.clearData();

            if (this.ptIdBox.Text.Length == 0)
            {
                return;
            }

            PatBase p = PatBase.Load(ptIdBox.Text);

            this.Text = p.Name;

            // 患者情報
            ptInfoBox.Text = p.Info1;

            // 保険ComboBox と現在使用保険
            insBox.Items.Clear();
            insBox.Items.Add("");

            foreach (PatIns ins in PatIns.GetDict(p.Id).Values)
            {
                insBox.Items.Add(ins.SEQ + " " + ins.KindNameShort);

                if (p.Ins == ins.SEQ.ToString())
                {
                    insBox.Text = ins.SEQ + " " + ins.KindNameShort;
                }
            }

            // 感染症
            infectionBox.Text = InfectionData.GetInfectionData(ptIdBox.Text).ResultString;

            this.cutOverData();
            this.makeHistory();

            // ログ
            LibUtility.Log("手術看護カルテ参照", this.Pat.Id);
        }

        private void makeHistory()
        {
            DataTable table = this.dSet.Tables["手術歴"];
            table.Clear();

            List<OpeNursingData> list = OpeNursingData.Find(this.ptId);

            string ope_nursing_ids = "";

//            Dictionary<int, PatIns> ins_dict = PatIns.GetDict(this.ptId);

            foreach (OpeNursingData p in list)
            {
                DataRow r = table.NewRow();

                r["ID"] = p.Id;
                r["INS"] = p.Ins;
                r["保険"] = p.InsName;
/*
                if (ins_dict.ContainsKey(int.Parse(p.Ins)))
                {
                    r["保険"] = ins_dict[int.Parse(p.Ins)].KindNameShort;
                }
*/
                r["OPE_DATE"] = p.OpeDateStringShort;
                r["IN_OUT"] = p.InOut;
                r["入外"] = p.InOutStringShort;
                r["TIME_OUT"] = p.TimeOut;
                r["緊急"] = p.TimeOutString;
                r["REC_KIND"] = p.RecKind;
                r["種別"] = p.RecKindString;
                r["DEPT"] = p.Dept;
                r["診療科"] = p.DeptShort;
                r["OPE"] = p.Ope;
                r["PART"] = p.Part;
                r["ANES"] = p.Anes;
                r["ANES_TIME1"] = p.AnesTimeString1;
                r["ANES_TIME2"] = p.AnesTimeString2;
                r["OPE_TIME1"] = p.OpeTimeString1;
                r["OPE_TIME2"] = p.OpeTimeString2;
                r["ROOM_TIME1"] = p.RoomTimeString1;
                r["ROOM_TIME2"] = p.RoomTimeString2;
                r["DOCTOR1"] = p.Doctor1;
                r["DOCTOR2"] = p.Doctor2;
                r["DOCTOR3"] = p.Doctor3;
                r["NS1"] = p.Ns1;
                r["NS2"] = p.Ns2;
                r["NS3"] = p.Ns3;
                r["ROOM"] = p.Room;
                r["NS_REC"] = p.NsRec;
                r["INFECTION"] = p.Infection;
                r["STAFF"] = p.Staff;
                r["看護師"] = p.StaffName;
                r["STATUS"] = p.Status;
                r["完成"] = p.StatusFlg;
                r["PDF_SAVE"] = p.PDFSave;

                r["REC_HIST"] = p.RecHist;

                r["Obj"] = p;

                table.Rows.Add(r);

                if (ope_nursing_ids.Length > 0)
                {
                    ope_nursing_ids = ope_nursing_ids + "," + p.Id;
                }
                else
                {
                    ope_nursing_ids = p.Id.ToString();
                }
            }

            this.filterHistory();

            if (historyView.SelectedRows.Count > 0)
            {
                this.changeMode("参照");
            }
            else
            {
                this.changeMode("新規");
            }
        }

        private void filterHistory()
        {
            DataView tmpView = new DataView(this.dSet.Tables["手術歴"]);

            tmpView.RowFilter = "";

            if (!this.inActiveBox.Checked)
            {
                tmpView.RowFilter = "STATUS <> 0";
            }

            this.historyView.DataSource = tmpView;

            historyView.Columns["ID"].Visible = false;

            historyView.Columns["INS"].Visible = false;
            historyView.Columns["保険"].Visible = false;

            historyView.Columns["OPE_DATE"].HeaderText = "手術日";
            historyView.Columns["OPE_DATE"].Width = 55;
            historyView.Columns["OPE_DATE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            historyView.Columns["IN_OUT"].Visible = false;

            historyView.Columns["入外"].Width = 35;
            historyView.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            historyView.Columns["REC_KIND"].Visible = false;

            historyView.Columns["種別"].Width = 30;
            historyView.Columns["種別"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            historyView.Columns["TIME_OUT"].Visible = false;

            historyView.Columns["緊急"].Visible = false;

            historyView.Columns["DEPT"].Visible = false;

            historyView.Columns["診療科"].HeaderText = "科";
            historyView.Columns["診療科"].Width = 45;
            historyView.Columns["診療科"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            historyView.Columns["OPE"].HeaderText = "術式";
            historyView.Columns["OPE"].Width = 75;

            historyView.Columns["PART"].HeaderText = "部位";
            historyView.Columns["PART"].Width = 45;

            historyView.Columns["ANES"].Visible = false;

            historyView.Columns["ANES_TIME1"].Visible = false;
            historyView.Columns["ANES_TIME2"].Visible = false;
            historyView.Columns["OPE_TIME1"].Visible = false;
            historyView.Columns["OPE_TIME2"].Visible = false;

            historyView.Columns["ROOM_TIME1"].Visible = false;
            historyView.Columns["ROOM_TIME2"].Visible = false;

            historyView.Columns["DOCTOR1"].HeaderText = "執刀医";
            historyView.Columns["DOCTOR1"].Width = 55;

            historyView.Columns["DOCTOR2"].Visible = false;
            historyView.Columns["DOCTOR3"].Visible = false;
            historyView.Columns["NS1"].Visible = false;
            historyView.Columns["NS2"].Visible = false;
            historyView.Columns["NS3"].Visible = false;
            historyView.Columns["ROOM"].Visible = false;
            historyView.Columns["NS_REC"].Visible = false;
            historyView.Columns["INFECTION"].Visible = false;

            historyView.Columns["REC_HIST"].Visible = false;

            historyView.Columns["STAFF"].Visible = false;
            historyView.Columns["看護師"].Visible = false;

            historyView.Columns["STATUS"].Visible = false;
            historyView.Columns["完成"].Width = 35;
            historyView.Columns["完成"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            historyView.Columns["PDF_SAVE"].Visible = false;

            historyView.Columns["Obj"].Visible = false;

            // 削除された記録は背景グレー・削除線を引く
            Font inActiveFont = new Font("MS UI Gothic", 8, FontStyle.Strikeout);

            for (int i = 0; i < historyView.RowCount; i++)
            {
                if (historyView.Rows[i].Cells["STATUS"].Value.ToString() == "0")
                {
                    historyView.Rows[i].DefaultCellStyle.Font = inActiveFont;
                    historyView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        // 手術記録の内容を表示する
        private void showData()
        {
            if (historyView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = historyView.SelectedRows[0];

                OpeNursingData obj = (OpeNursingData)tmpRow.Cells["Obj"].Value;
                this._OpeNursingData = obj;

                this.recordIdBox.Text = obj.Id.ToString();
                this.opeDate.Value = DateTime.Parse(obj.OpeDateString);

                if (obj.RecKind.Equals("1"))
                {
                    recKindBox.Text = "1 術前";
                }
                else if (obj.RecKind.Equals("2"))
                {
                    recKindBox.Text = "2 術中";
                }
                else if (obj.RecKind.Equals("3"))
                {
                    recKindBox.Text = "3 術後";
                }

                if (obj.InOut.Equals("1"))
                {
                    this.inOutButton1.Checked = true;
                }
                else if (obj.InOut.Equals("2"))
                {
                    this.inOutButton2.Checked = true;
                }

                if (obj.TimeOut.Equals("0"))
                {
                    this.timeOutButton0.Checked = true;
                }
                else if (obj.TimeOut.Equals("1"))
                {
                    this.timeOutButton1.Checked = true;
                }
                else if (obj.TimeOut.Equals("2"))
                {
                    this.timeOutButton2.Checked = true;
                }

                this.deptBox.SelectedItem = obj.Dept + " " + obj.DeptShort;

                this.opeBox.Text = obj.Ope;
                this.partBox.Text = obj.Part;
                this.anesBox.Text = obj.Anes;
                this.doctorBox1.Text = obj.Doctor1;
                this.doctorBox2.Text = obj.Doctor2;
                this.doctorBox3.Text = obj.Doctor3;
                this.nsBox1.Text = obj.Ns1;
                this.nsBox2.Text = obj.Ns2;
                this.nsBox3.Text = obj.Ns3;
                this.roomBox.Text = obj.Room;

                insBox.Text = obj.Ins + " " + obj.InsName;

                anesTimeBox1.Text = obj.AnesTimeString1;
                anesTimeBox2.Text = obj.AnesTimeString2;
                opeTimeBox1.Text = obj.OpeTimeString1;
                opeTimeBox2.Text = obj.OpeTimeString2;

                roomTimeBox1.Text = obj.RoomTimeString1;
                roomTimeBox2.Text = obj.RoomTimeString2;

                recHistBox.MakeList(obj.RecHist);

                this.recText.Text = obj.NsRec;

                SchemaTagList.Clear();

                foreach (OpeNursingSchema p in OpeNursingSchema.GetDict(obj.Id).Values)
                {
                    SchemaTag tmpTag = new SchemaTag();
                    tmpTag.Id = p.Id.ToString();
                    tmpTag.Bg = p.Bg.ToString();
                    tmpTag.Item = p.Item;

                    SchemaTagList.Add(tmpTag);
                }

                this.redrawSchemas();

                this.staffBox.Text = obj.StaffName;
                this.saveDateTimeBox.Text = DateTimeAgent.DateFormat(obj.SaveDate, DateTimeAgent.DateFormatKind.LONG) + " " + DateTimeAgent.TimeFormat6(obj.SaveTime, 4, true);
                this.infectionBox.Text = obj.Infection;

                /*
                if (obj.PDFSave.Equals("1"))
                {
                    this.completeBox.Checked = true;
                    this.completeBox.Enabled = false;

                    this.pdfBox.Checked = true;
                    this.regButton.Enabled = false;
                }
                else
                {
                    this.completeBox.Checked = false;
                    this.completeBox.Enabled = true;

                    this.pdfBox.Checked = false;
                }
                 */

                this.completeBox.Checked = obj.Status.Equals("1");
                this.pdfBox.Checked = obj.PDFSave.Equals("1");

                this.asTabControl.Controls.Clear();

                foreach (OpeNursingAs p in OpeNursingAs.GetDict(obj.Id).Values)
                {
                    TextBox tmpAsTextBox = new TextBox();
                    TabPage tmpAsTabPage = new TabPage();

                    tmpAsTextBox.Multiline = true;
                    tmpAsTextBox.Location = new Point(10, 10);
                    tmpAsTextBox.Size = new Size(315, 450);
                    tmpAsTextBox.ScrollBars = ScrollBars.Vertical;
                    tmpAsTextBox.Text = p.Text.ToString();

                    tmpAsTabPage.Controls.Add(tmpAsTextBox);
                    tmpAsTabPage.Text = p.Title.ToString();

                    this.asTabControl.Controls.Add(tmpAsTabPage);
                }
            }
        }

        // 文字数制限をオーバーするデータをカットする
        public void cutOverData()
        {
            // 術式は100字以上であれば削る
            if (this.opeBox.Text.Length >= 100)
            {
                this.opeBox.Text = this.opeBox.Text.Substring(0, 99);
            }

            // 麻酔・部位・感染症は50字以上であれば削る
            if (this.anesBox.Text.Length >= 50)
            {
                this.anesBox.Text = this.anesBox.Text.Substring(0, 49);
            }

            if (this.partBox.Text.Length >= 50)
            {
                this.partBox.Text = this.partBox.Text.Substring(0, 49);
            }

            if (this.infectionBox.Text.Length >= 50)
            {
                this.infectionBox.Text = this.infectionBox.Text.Substring(0, 49);
            }

            // 医師・看護師は25字以上であれば削る
            if (this.doctorBox1.Text.Length >= 25)
            {
                this.doctorBox1.Text = this.doctorBox1.Text.Substring(0, 24);
            }

            if (this.doctorBox2.Text.Length >= 25)
            {
                this.doctorBox2.Text = this.doctorBox2.Text.Substring(0, 24);
            }

            if (this.doctorBox3.Text.Length >= 25)
            {
                this.doctorBox3.Text = this.doctorBox3.Text.Substring(0, 24);
            }

            if (this.nsBox1.Text.Length >= 25)
            {
                this.nsBox1.Text = this.nsBox1.Text.Substring(0, 24);
            }

            if (this.nsBox2.Text.Length >= 25)
            {
                this.nsBox2.Text = this.nsBox2.Text.Substring(0, 24);
            }

            if (this.nsBox3.Text.Length >= 25)
            {
                this.nsBox3.Text = this.nsBox3.Text.Substring(0, 24);
            }

            // 看護診断は1000字以上であれば削る
            foreach (TabPage tp in this.asTabControl.Controls)
            {
                if (tp.Controls[0].Text.Length >= 1000)
                {
                    tp.Controls[0].Text = tp.Controls[0].Text.Substring(0, 999);
                }
            }

            // 看護記録は1000字以上であれば削る
            if (this.recText.Text.Length >= 2000)
            {
                this.recText.Text = this.recText.Text.Substring(0, 1999);
            }
        }

        private void openAsButton_Click(object sender, EventArgs e)
        {
            if (fa1 == null || !fa1.Created)
            {
                fa1 = new FormOpeNursingAs(this);
            }

            fa1.Show();
            fa1.Activate();

            if (fa1.WindowState == FormWindowState.Minimized)
            {
                fa1.WindowState = FormWindowState.Normal;
            }
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            /*
            if (this.pdfBox.Checked)
            {
                MessageBox.Show("既にPDF化されている記録は保存できません");
                return;
            }
            */

            if (!LoginUser.IsNurse)
            {
                if (MessageBox.Show("ログインユーザーが看護部ではありません。登録しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }

            if (this.mode.Equals("修正") && this._OpeNursingData.PDFSave.Equals("1"))
            {
                // すでにPDF保存されている記録を修正する場合は、いったん PDFファイルを削除する
                PdfDoc.Delete(this._OpeNursingData.PDFFile, OpeNursingData.PDFFolder);
            }

            string in_out = "1";

            if (inOutButton2.Checked)
            {
                in_out = "2";
            }

            string time_out = "0";

            if (timeOutButton1.Checked)
            {
                time_out = "1";
            }
            else if (timeOutButton2.Checked)
            {
                time_out = "2";
            }

            string deptId = "0";

            if (deptBox.Text.Contains(" "))
            {
                deptId = deptBox.Text.Split(' ')[0].Trim();
            }

            string ins = "0";

            if (insBox.Text.Contains(" "))
            {
                ins = insBox.Text.Split(' ')[0].Trim();
            }

            string status = "1";

            if (!completeBox.Checked)
            {
                status = "2";
            }

            this.cutOverData();

            if (schemaItemList.Count > 0 && MessageBox.Show("記載中の図があります。保存しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.regSchema();
            }

            OpeNursingData obj = new OpeNursingData();

            int.TryParse(this.recordIdBox.Text, out obj.Id);
            obj.Pat.Id = this.ptId;
            obj.Infection = this.infectionBox.Text;
            obj.Ins = ins;
            obj.OpeDate = opeDate.Value.ToString("yyyyMMdd");
            obj.InOut = in_out;
            obj.TimeOut = time_out;
            obj.Dept = deptId;
            obj.Ope = this.opeBox.Text;
            obj.Part = this.partBox.Text;
            obj.Anes = this.anesBox.Text;
            obj.Doctor1 = this.doctorBox1.Text;
            obj.Doctor2 = this.doctorBox2.Text;
            obj.Doctor3 = this.doctorBox3.Text;
            obj.Ns1 = this.nsBox1.Text;
            obj.Ns2 = this.nsBox2.Text;
            obj.Ns3 = this.nsBox3.Text;
            obj.Room = this.roomBox.Text;
            obj.NsRec = this.recText.Text;
            obj.AnesTime1 = this.anesTimeBox1.Text.Length > 0 ? this.anesTimeBox1.ToInt() : -1;
            obj.AnesTime2 = this.anesTimeBox2.Text.Length > 0 ? this.anesTimeBox2.ToInt() : -1;
            obj.OpeTime1 = this.opeTimeBox1.Text.Length > 0 ? this.opeTimeBox1.ToInt() : -1;
            obj.OpeTime2 = this.opeTimeBox2.Text.Length > 0 ? this.opeTimeBox2.ToInt() : -1;
            obj.RoomTime1 = this.roomTimeBox1.Text.Length > 0 ? this.roomTimeBox1.ToInt() : -1;
            obj.RoomTime2 = this.roomTimeBox2.Text.Length > 0 ? this.roomTimeBox2.ToInt() : -1;
            obj.RecHist = this.recHistBox.HistString;
            obj.RecKind = this.recKindBox.Text.Contains(" ") ? recKindBox.Text.Split(' ')[0] : "";
            obj.Status = status;


            // 看護診断を登録する
            int as_id = 1;

            foreach (TabPage tp in this.asTabControl.Controls)
            {
                OpeNursingAs a = new OpeNursingAs();

                a.Id = obj.Id;
                a.Title = tp.Text;
                a.Text = tp.Controls[0].Text;

                obj.AsDict.Add(as_id.ToString(), a);
                as_id++;
            }


            // シェーマを登録する
            int schema_id = 0;

            foreach (SchemaTag sTag in SchemaTagList)
            {
                OpeNursingSchema s = new OpeNursingSchema();

                s.Id = obj.Id;
                int.TryParse(sTag.Bg, out s.Bg);
                s.Item = sTag.Item;

                obj.SchemaDict.Add(schema_id.ToString(), s);
                schema_id++;
            }

            StdReturn sr = obj.Save();

            if (sr.ErrExist)
            {
                MessageBox.Show(sr.Err);
            }
            else
            {
                MessageBox.Show("登録しました");

                // 選択行
                int rowNum = 0;

                if (historyView.SelectedRows.Count > 0)
                {
                    rowNum = historyView.SelectedRows[0].Index;
                }

                this.clearData();
                this.makeHistory();

                // 前の選択行があれば選ぶ
                if (this.historyView.Rows.Count > rowNum)
                {
                    this.historyView.Rows[rowNum].Selected = true;
                    showData();
                }
            }
        }

        private void inActiveBox_CheckedChanged(object sender, EventArgs e)
        {
            this.filterHistory();
        }

        private void changeMode(string Mode)
        {
            if (Mode == "新規")
            {
                this.mode = Mode;
                this.modeLabel.Text = Mode;
                this.modeLabel.BackColor = Color.White;

                this.recordIdBox.Text = "";
                this.clearData();

                this.regButton.Enabled = true;
            }
            else if (Mode == "修正")
            {
                this.mode = Mode;
                this.modeLabel.Text = Mode;
                this.modeLabel.BackColor = Color.LightPink;

                this.recordIdBox.Text = "";
                this.clearData();

                this.regButton.Enabled = true;

                this.showData();
            }
            else if (Mode == "参照")
            {
                this.mode = Mode;
                this.modeLabel.Text = Mode;
                this.modeLabel.BackColor = Color.Yellow;

                this.recordIdBox.Text = "";
                this.clearData();

                this.regButton.Enabled = false;

                this.showData();
            }
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeMode("新規");
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void exitFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void historyView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.changeMode("参照");
        }

        private void historyContextMenu_Opening(object sender, CancelEventArgs e)
        {
            /*
            if (historyView.SelectedRows.Count > 0)
            {
                DataGridViewRow tmpRow = historyView.SelectedRows[0];

                if (tmpRow.Cells["PDF_SAVE"].Value.ToString() == "1")
                {
                    historyContextMenu.Items[1].Enabled = false;
                    historyContextMenu.Items[2].Enabled = false;
                }
                else
                {
                    historyContextMenu.Items[1].Enabled = true;
                    historyContextMenu.Items[2].Enabled = true;
                }
            }
             */
        }

        private void newContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeMode("新規");
        }

        private void modifyContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeMode("修正");
        }

        private void delContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("削除しますか？", "削除", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    OpeNursingData obj = (OpeNursingData)historyView.SelectedRows[0].Cells["Obj"].Value;

                    // データを削除
                    OpeNursingData.Delete(obj.Id);

                    // PDF保存されている場合
                    if (obj.PDFSave.Equals("1"))
                    {
                        // PDFファイルを削除
                        PdfDoc.Delete(obj.PDFFile, OpeNursingData.PDFFolder);
                    }

                    MessageBox.Show("削除しました");

                    this.clearData();
                    this.makeHistory();
                }
            }
        }

        private void modeLabel_Click(object sender, EventArgs e)
        {
            if (this.modeLabel.Text == "参照")
            {
                if (!this.pdfBox.Checked)
                {
                    this.mode = "修正";
                    this.modeLabel.Text = "修正";
                    this.modeLabel.BackColor = Color.LightPink;
                    this.regButton.Enabled = true;
                }
            }
            else if (this.modeLabel.Text == "修正")
            {
                this.mode = "参照";
                this.modeLabel.Text = "参照";
                this.modeLabel.BackColor = Color.Yellow;
                this.regButton.Enabled = false;
            }
        }

        private void redrawSchemas()
        {
            schemaPanel.Controls.Clear();

            foreach (SchemaTag tmpTag in SchemaTagList)
            {
                PictureBox sBox = new PictureBox();

                sBox.Tag = tmpTag;
                sBox.Size = new Size(100, 100);
                sBox.Image = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                g = Graphics.FromImage(sBox.Image);

                if (tmpTag.Bg.Length > 0)
                {
                    DataTable table = mSet.Tables["Schema"];

                    foreach (DataRow tmpRow in table.Rows)
                    {
                        if (tmpRow.ItemArray[0].ToString().Equals(tmpTag.Bg))
                        {
                            string f = AppFile.FilePath(tmpRow.ItemArray[2].ToString());

                            if (f.Length > 0)
                            {
                                sBox.BackgroundImage = Image.FromFile(f);
                                sBox.BackgroundImageLayout = ImageLayout.Zoom;
                            }

                            break;
                        }
                    }
                }
                else
                {
                    sBox.BackgroundImage = null;
                    sBox.BackgroundImageLayout = ImageLayout.None;
                }

                string[] s = (tmpTag.Item + "\n").Split('\n');

                if (s.Length > 0)
                {
                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        string[] ss = s[i].Trim('\r').Split(',');

                        if (ss[0].Trim() == "直線" || ss[0].Trim() == "楕円" || ss[0].Trim() == "四角" || ss[0].Trim() == "文字")
                        {
                            SchemaItem d = new SchemaItem();
                            d.Kind = ss[0].Trim();
                            d.C = Color.FromName(ss[1].Trim());
                            d.StartX = int.Parse(ss[2].Trim());
                            d.StartY = int.Parse(ss[3].Trim());
                            d.EndX = int.Parse(ss[4].Trim());
                            d.EndY = int.Parse(ss[5].Trim());

                            if (ss.Length >= 7)
                            {
                                for (int j = 6; j < ss.Length; j++)
                                {
                                    d.Text += ss[j].Trim().Replace("<CR+LF>", "\r\n");
                                }
                            }

                            Pen p1 = new Pen(d.C, 1);

                            if (d.Kind == "直線")
                            {
                                g.DrawLine(p1, d.StartX / 4, d.StartY / 4, d.EndX / 4, d.EndY / 4);
                            }
                            else if (d.Kind == "楕円")
                            {
                                g.DrawEllipse(p1, d.StartX / 4, d.StartY / 4, (d.EndX - d.StartX) / 4, (d.EndY - d.StartY) / 4);
                            }
                            else if (d.Kind == "四角")
                            {
                                g.DrawRectangle(p1, d.StartX / 4, d.StartY / 4, (d.EndX - d.StartX) / 4, (d.EndY - d.StartY) / 4);
                            }
                            else if (d.Kind == "文字")
                            {
                                Label tmp = new Label();
                                tmp.Location = new Point(d.StartX / 4, d.StartY / 4);
                                tmp.Size = new Size((d.EndX - d.StartX) / 4, (d.EndY - d.StartY) / 4);
                                tmp.Font = new Font("", 4);
                                tmp.BorderStyle = BorderStyle.None;
                                tmp.Text = d.Text;
                                tmp.ForeColor = d.C;
                                tmp.BackColor = Color.LightYellow;

                                sBox.Controls.Add(tmp);
                            }
                        }
                        else if (ss[0].Trim() == "フリー")
                        {
                            FreeItem d = new FreeItem();
                            d.Kind = "フリー";
                            d.C = Color.FromName(ss[1].Trim());
                            d.PointList = new List<Point>();

                            Pen p1 = new Pen(d.C, 1);

                            for (int j = 2; j < ss.Length; j = j + 2)
                            {
                                d.PointList.Add(new Point(int.Parse(ss[j].Trim()), int.Parse(ss[j + 1].Trim())));
                            }

                            for (int j = 1; j < d.PointList.Count; j++)
                            {
                                g.DrawLine(p1, d.PointList[j - 1].X / 4, d.PointList[j - 1].Y / 4, d.PointList[j].X / 4, d.PointList[j].Y / 4);
                            }
                        }
                    }
                }

                sBox.Click += new EventHandler(sBox_Click);

                schemaPanel.Controls.Add(sBox);
            }
        }

        void sBox_Click(object sender, EventArgs e)
        {
            PictureBox tmpBox = (PictureBox)(sender);

            foreach (PictureBox p in schemaPanel.Controls)
            {
                if (p.Equals(tmpBox))
                {
                    p.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    p.BorderStyle = BorderStyle.None;
                }
            }

            SchemaTag tmpTag = (SchemaTag)(tmpBox.Tag);

            this.schemaIdBox.Text = tmpTag.Id;
            this.schemaBgBox.Text = tmpTag.Bg;
            this.schemaItemList.Clear();
            this.freeItemList.Clear();

            gStart = 0;
            gEnd = 0;

            gMode = 0;
            drawButton0.Checked = true;
            gText = "";
            gSchema = -1;
            gFree = -1;
            gFreeItem = new FreeItem();

            string[] s = (tmpTag.Item + "\n").Split('\n');

            for (int i = 0; i < s.Length - 1; i++)
            {
                string[] ss = s[i].Trim('\r').Split(',');

                if (ss[0].Trim() == "直線" || ss[0].Trim() == "楕円" || ss[0].Trim() == "四角" || ss[0].Trim() == "文字")
                {
                    SchemaItem tmpItem = new SchemaItem();
                    tmpItem.Kind = ss[0].Trim();
                    tmpItem.C = Color.FromName(ss[1].Trim());
                    tmpItem.StartX = int.Parse(ss[2].Trim());
                    tmpItem.StartY = int.Parse(ss[3].Trim());
                    tmpItem.EndX = int.Parse(ss[4].Trim());
                    tmpItem.EndY = int.Parse(ss[5].Trim());

                    if (ss.Length >= 7)
                    {
                        for (int j = 6; j < ss.Length; j++)
                        {
                            tmpItem.Text += ss[j].Trim().Replace("<CR+LF>", "\r\n");
                        }
                    }

                    if (tmpItem.Kind == "直線")
                    {
                        tmpItem.Points = new Point[17];
                        tmpItem.Points[0].X = tmpItem.StartX;
                        tmpItem.Points[0].Y = tmpItem.StartY;
                        tmpItem.Points[1].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 16;
                        tmpItem.Points[1].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 1 / 16;
                        tmpItem.Points[2].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 16;
                        tmpItem.Points[2].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 2 / 16;
                        tmpItem.Points[3].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 16;
                        tmpItem.Points[3].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 16;
                        tmpItem.Points[4].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 4 / 16;
                        tmpItem.Points[4].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 4 / 16;
                        tmpItem.Points[5].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 5 / 16;
                        tmpItem.Points[5].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 5 / 16;
                        tmpItem.Points[6].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 6 / 16;
                        tmpItem.Points[6].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 6 / 16;
                        tmpItem.Points[7].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 7 / 16;
                        tmpItem.Points[7].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 7 / 16;
                        tmpItem.Points[8].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 8 / 16;
                        tmpItem.Points[8].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 8 / 16;
                        tmpItem.Points[9].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 9 / 16;
                        tmpItem.Points[9].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 9 / 16;
                        tmpItem.Points[10].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 10 / 16;
                        tmpItem.Points[10].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 10 / 16;
                        tmpItem.Points[11].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 11 / 16;
                        tmpItem.Points[11].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 11 / 16;
                        tmpItem.Points[12].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 12 / 16;
                        tmpItem.Points[12].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 12 / 16;
                        tmpItem.Points[13].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 13 / 16;
                        tmpItem.Points[13].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 13 / 16;
                        tmpItem.Points[14].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 14 / 16;
                        tmpItem.Points[14].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 14 / 16;
                        tmpItem.Points[15].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 15 / 16;
                        tmpItem.Points[15].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 15 / 16;
                        tmpItem.Points[16].X = tmpItem.EndX;
                        tmpItem.Points[16].Y = tmpItem.EndY;
                    }
                    else if (tmpItem.Kind == "楕円")
                    {
                        tmpItem.Points = new Point[16];
                        tmpItem.Points[0].X = tmpItem.StartX;
                        tmpItem.Points[0].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 2;
                        tmpItem.Points[1].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 8;
                        tmpItem.Points[1].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 4;
                        tmpItem.Points[2].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 8;
                        tmpItem.Points[2].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 8;
                        tmpItem.Points[3].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 8;
                        tmpItem.Points[3].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 16;
                        tmpItem.Points[4].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 4 / 8;
                        tmpItem.Points[4].Y = tmpItem.StartY;
                        tmpItem.Points[5].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 5 / 8;
                        tmpItem.Points[5].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 16;
                        tmpItem.Points[6].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 6 / 8;
                        tmpItem.Points[6].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 8;
                        tmpItem.Points[7].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 7 / 8;
                        tmpItem.Points[7].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 4;
                        tmpItem.Points[8].X = tmpItem.EndX;
                        tmpItem.Points[8].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 2;
                        tmpItem.Points[9].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 7 / 8;
                        tmpItem.Points[9].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                        tmpItem.Points[10].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 6 / 8;
                        tmpItem.Points[10].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 7 / 8;
                        tmpItem.Points[11].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 5 / 8;
                        tmpItem.Points[11].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 15 / 16;
                        tmpItem.Points[12].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 4 / 8;
                        tmpItem.Points[12].Y = tmpItem.EndY;
                        tmpItem.Points[13].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 8;
                        tmpItem.Points[13].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 15 / 16;
                        tmpItem.Points[14].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 8;
                        tmpItem.Points[14].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 7 / 8;
                        tmpItem.Points[15].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 8;
                        tmpItem.Points[15].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                    }
                    else if (tmpItem.Kind == "四角")
                    {
                        tmpItem.Points = new Point[16];
                        tmpItem.Points[0].X = tmpItem.StartX;
                        tmpItem.Points[0].Y = tmpItem.StartY;
                        tmpItem.Points[1].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 4;
                        tmpItem.Points[1].Y = tmpItem.StartY;
                        tmpItem.Points[2].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 4;
                        tmpItem.Points[2].Y = tmpItem.StartY;
                        tmpItem.Points[3].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 4;
                        tmpItem.Points[3].Y = tmpItem.StartY;
                        tmpItem.Points[4].X = tmpItem.EndX;
                        tmpItem.Points[4].Y = tmpItem.StartY;
                        tmpItem.Points[5].X = tmpItem.EndX;
                        tmpItem.Points[5].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 1 / 4;
                        tmpItem.Points[6].X = tmpItem.EndX;
                        tmpItem.Points[6].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 2 / 4;
                        tmpItem.Points[7].X = tmpItem.EndX;
                        tmpItem.Points[7].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                        tmpItem.Points[8].X = tmpItem.EndX;
                        tmpItem.Points[8].Y = tmpItem.EndY;
                        tmpItem.Points[9].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 4;
                        tmpItem.Points[9].Y = tmpItem.EndY;
                        tmpItem.Points[10].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 4;
                        tmpItem.Points[10].Y = tmpItem.EndY;
                        tmpItem.Points[11].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 4;
                        tmpItem.Points[11].Y = tmpItem.EndY;
                        tmpItem.Points[12].X = tmpItem.StartX;
                        tmpItem.Points[12].Y = tmpItem.EndY;
                        tmpItem.Points[13].X = tmpItem.StartX;
                        tmpItem.Points[13].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                        tmpItem.Points[14].X = tmpItem.StartX;
                        tmpItem.Points[14].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 2 / 4;
                        tmpItem.Points[15].X = tmpItem.StartX;
                        tmpItem.Points[15].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 1 / 4;
                    }

                    if (this.GetSchemaByteCount() + tmpItem.GetByteCount() < 3990)
                    {
                        schemaItemList.Add(tmpItem);
                    }
                }
                else if (ss[0].Trim() == "フリー")
                {
                    FreeItem tmpItem = new FreeItem();
                    tmpItem.Kind = "フリー";
                    tmpItem.C = Color.FromName(ss[1].Trim());
                    tmpItem.PointList = new List<Point>();

                    for (int j = 2; j < ss.Length; j = j + 2)
                    {
                        tmpItem.PointList.Add(new Point(int.Parse(ss[j].Trim()), int.Parse(ss[j + 1].Trim())));
                    }

                    if (this.GetSchemaByteCount() + tmpItem.GetByteCount() < 3990)
                    {
                        freeItemList.Add(tmpItem);
                    }
                }
            }

            this.redrawSchema();
        }

        private void openSchemaBgButton_Click(object sender, EventArgs e)
        {
            FormOpeNursingSchema f = new FormOpeNursingSchema();
            DialogResult dr = f.ShowDialog(this);

            if (dr == DialogResult.Yes)
            {
                PictureBox box = f.SelectedPictureBox;

                if (box.Tag != null)
                {
                    OpeNursingSettings.PictureTag tmpTag = (OpeNursingSettings.PictureTag)(box.Tag);

                    this.schemaBox.BackgroundImage = Image.FromFile(tmpTag.Path);
                    this.schemaBgBox.Text = tmpTag.Id;
                }
            }
            else if (dr == DialogResult.No)
            {
                this.schemaBox.BackgroundImage = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.schemaBgBox.Text = "";
            }

            f.Dispose();
        }

        private void schemaBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                schemaBox.Focus();

                if (e.Button == MouseButtons.Left)
                {
                    if (gMode != 0)
                    {
                        gSchema = -1;
                        gFree = -1;

                        gStart = 1;
                        gEnd = 0;

                        gStartX = e.X;
                        gStartY = e.Y;

                        if (gMode == 5)
                        {
                            gFreeItem = new FreeItem();
                            gFreeItem.C = gColor;
                        }
                    }
                    else
                    {
                        gSchema = -1;
                        gFree = -1;
                        bool loop = true;

                        for (int x = e.X - 5; x <= e.X + 5 && loop; x++)
                        {
                            for (int y = e.Y - 5; y <= e.Y + 5 && loop; y++)
                            {
                                for (int i = 0; i < schemaItemList.Count && loop; i++)
                                {
                                    SchemaItem si = schemaItemList[i];

                                    // 直線・楕円・四角であればポイントと重なるかチェックする。
                                    // 重なれば選択状態となる。
                                    if (si.Kind == "直線" || si.Kind == "楕円" || si.Kind == "四角")
                                    {
                                        for (int p = 0; p < si.Points.Length; p++)
                                        {
                                            Point pi = si.Points[p];

                                            if (x == pi.X && y == pi.Y)
                                            {
                                                gSchema = i;

                                                if (si.Kind == "直線")
                                                {
                                                    if (p == 0 || p == 1 || p == 2)
                                                    {
                                                        gMode = 11;
                                                        gStart = 0;
                                                        gEnd = 1;
                                                        gStartX = si.StartX;
                                                        gStartY = si.StartY;
                                                        gEndX = si.EndX;
                                                        gEndY = si.EndY;
                                                    }
                                                    else if (p == 14 || p == 15 || p == 16)
                                                    {
                                                        gMode = 11;
                                                        gStart = 1;
                                                        gEnd = 0;
                                                        gStartX = si.StartX;
                                                        gStartY = si.StartY;
                                                        gEndX = si.EndX;
                                                        gEndY = si.EndY;
                                                    }
                                                }
                                                else if (si.Kind == "楕円")
                                                {
                                                    if (p >= 0 && p <= 7)
                                                    {
                                                        gMode = 12;
                                                        gStart = 0;
                                                        gEnd = 1;
                                                        gStartX = si.StartX;
                                                        gStartY = si.StartY;
                                                        gEndX = si.EndX;
                                                        gEndY = si.EndY;
                                                        gMarginX = si.StartX - e.X;
                                                        gMarginY = si.StartY - e.Y;
                                                    }
                                                    else if (p >= 8 && p <= 15)
                                                    {
                                                        gMode = 12;
                                                        gStart = 1;
                                                        gEnd = 0;
                                                        gStartX = si.StartX;
                                                        gStartY = si.StartY;
                                                        gEndX = si.EndX;
                                                        gEndY = si.EndY;
                                                        gMarginX = si.EndX - e.X;
                                                        gMarginY = si.EndY - e.Y;
                                                    }
                                                }
                                                else if (si.Kind == "四角")
                                                {
                                                    if (p == 0)
                                                    {
                                                        gMode = 13;
                                                        gStart = 0;
                                                        gEnd = 1;
                                                        gStartX = si.StartX;
                                                        gStartY = si.StartY;
                                                        gEndX = si.EndX;
                                                        gEndY = si.EndY;
                                                        gMarginX = si.StartX - e.X;
                                                        gMarginY = si.StartY - e.Y;
                                                    }
                                                    else if (p == 4 || p == 8 || p == 12)
                                                    {
                                                        gMode = 13;
                                                        gStart = 1;
                                                        gEnd = 0;
                                                        gStartX = si.StartX;
                                                        gStartY = si.StartY;
                                                        gEndX = si.EndX;
                                                        gEndY = si.EndY;
                                                        gMarginX = si.EndX - e.X;
                                                        gMarginY = si.EndY - e.Y;
                                                    }
                                                }

                                                loop = false;
                                                break;
                                            }
                                        }
                                    }
                                    // 文字であれば下隅と重なるかチェックする。
                                    // 重なれば大きさ変更モードとなる。
                                    else if (si.Kind == "文字")
                                    {
                                        if (x == si.StartX && y == si.StartY)
                                        {
                                            gSchema = i;
                                            gMode = 14;
                                            gStart = 0;
                                            gEnd = 1;
                                            gStartX = si.StartX;
                                            gStartY = si.StartY;
                                            gEndX = si.EndX;
                                            gEndY = si.EndY;
                                            gMarginX = si.StartX - e.X;
                                            gMarginY = si.StartY - e.Y;
                                            gText = si.Text;

                                            loop = false;
                                            break;
                                        }
                                        else if ((x == si.EndX && y == si.EndY) || (x == si.StartX && y == si.EndY)  || (x == si.EndX && y == si.StartY))
                                        {
                                            gSchema = i;
                                            gMode = 14;
                                            gStart = 1;
                                            gEnd = 0;
                                            gStartX = si.StartX;
                                            gStartY = si.StartY;
                                            gEndX = si.EndX;
                                            gEndY = si.EndY;
                                            gMarginX = si.EndX - e.X;
                                            gMarginY = si.EndY - e.Y;
                                            gText = si.Text;

                                            loop = false;
                                            break;
                                        }
                                    }
                                }

                                for (int i = 0; i < freeItemList.Count && loop; i++)
                                {
                                    FreeItem fi = freeItemList[i];

                                    // フリー曲線がポイントと重なるかチェックする。
                                    // 重なれば選択状態となる。
                                    for (int p = 0; p < fi.PointList.Count; p++)
                                    {
                                        Point pi = fi.PointList[p];

                                        if (x == pi.X && y == pi.Y)
                                        {
                                            gFree = i;
                                            gMode = 15;
                                            gStart = 0;
                                            gEnd = 0;

                                            loop = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        this.redrawSchema();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "エラー");
            }
        }

        private void schemaBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (gStart == 0 && gEnd == 0) return;

            if (gStart == 0)
            {
                if (gMode == 12 || gMode == 13 || gMode == 14)
                {
                    gStartX = e.X + gMarginX;
                    gStartY = e.Y + gMarginY;
                }
                else
                {
                    gStartX = e.X;
                    gStartY = e.Y;
                }
            }
            else if (gEnd == 0)
            {
                if (gMode == 12 || gMode == 13 || gMode == 14)
                {
                    gEndX = e.X + gMarginX;
                    gEndY = e.Y + gMarginY;
                }
                else if (gMode == 5)
                {
                    gFreeItem.PointList.Add(new Point(e.X, e.Y));
                    g.DrawLine(gPen, gStartX, gStartY, e.X, e.Y);
                    gStartX = e.X;
                    gStartY = e.Y;
                }
                else
                {
                    gEndX = e.X;
                    gEndY = e.Y;
                }
            }

            schemaBox.Refresh();
        }

        private void schemaBox_Paint(object sender, PaintEventArgs e)
        {
            if (gStart == 0 && gEnd == 0) return;

            if (gMode % 10 == 1)
            {
                e.Graphics.DrawLine(gPen, gStartX, gStartY, gEndX, gEndY);
            }
            else if (gMode % 10 == 2)
            {
                e.Graphics.DrawEllipse(gPen, gStartX, gStartY, gEndX - gStartX, gEndY - gStartY);
            }
            else if (gMode % 10 == 3)
            {
                e.Graphics.DrawRectangle(gPen, gStartX, gStartY, gEndX - gStartX, gEndY - gStartY);
            }
            else if (gMode % 10 == 4)
            {
                e.Graphics.DrawRectangle(gPen, gStartX, gStartY, gEndX - gStartX, gEndY - gStartY);
            }
        }

        private void schemaBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                // 非描画状態であれば無効
                if (gStart == 0 && gEnd == 0) return;

                // 始点・終点状態をリセットする（非描画状態にする）
                gStart = 0;
                gEnd = 0;

                // 始点と終点が同じであれば無効
                if (gStartX == gEndX && gStartY == gEndY) return;

                // 始点と終点がシェーマ枠からはみ出ていれば枠内に収める
                if (gStartX < 0) gStartX = 0;
                if (gStartX > schemaBox.Width) gStartX = schemaBox.Width;
                if (gStartY < 0) gStartY = 0;
                if (gStartY > schemaBox.Height) gStartY = schemaBox.Height;

                if (gEndX < 0) gEndX = 0;
                if (gEndX > schemaBox.Width) gEndX = schemaBox.Width;
                if (gEndY < 0) gEndY = 0;
                if (gEndY > schemaBox.Height) gEndY = schemaBox.Height;

                if (gMode % 10 == 1)
                {
                    if (gSchema >= 0 && gSchema < schemaItemList.Count)
                    {
                        schemaItemList.RemoveAt(gSchema);
                    }

                    SchemaItem tmpItem = new SchemaItem();
                    tmpItem.Kind = "直線";
                    tmpItem.C = gColor;
                    tmpItem.StartX = gStartX;
                    tmpItem.StartY = gStartY;
                    tmpItem.EndX = gEndX;
                    tmpItem.EndY = gEndY;
                    tmpItem.Text = "";

                    tmpItem.Points = new Point[17];
                    tmpItem.Points[0].X = tmpItem.StartX;
                    tmpItem.Points[0].Y = tmpItem.StartY;
                    tmpItem.Points[1].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 16;
                    tmpItem.Points[1].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 1 / 16;
                    tmpItem.Points[2].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 16;
                    tmpItem.Points[2].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 2 / 16;
                    tmpItem.Points[3].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 16;
                    tmpItem.Points[3].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 16;
                    tmpItem.Points[4].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 4 / 16;
                    tmpItem.Points[4].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 4 / 16;
                    tmpItem.Points[5].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 5 / 16;
                    tmpItem.Points[5].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 5 / 16;
                    tmpItem.Points[6].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 6 / 16;
                    tmpItem.Points[6].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 6 / 16;
                    tmpItem.Points[7].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 7 / 16;
                    tmpItem.Points[7].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 7 / 16;
                    tmpItem.Points[8].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 8 / 16;
                    tmpItem.Points[8].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 8 / 16;
                    tmpItem.Points[9].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 9 / 16;
                    tmpItem.Points[9].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 9 / 16;
                    tmpItem.Points[10].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 10 / 16;
                    tmpItem.Points[10].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 10 / 16;
                    tmpItem.Points[11].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 11 / 16;
                    tmpItem.Points[11].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 11 / 16;
                    tmpItem.Points[12].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 12 / 16;
                    tmpItem.Points[12].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 12 / 16;
                    tmpItem.Points[13].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 13 / 16;
                    tmpItem.Points[13].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 13 / 16;
                    tmpItem.Points[14].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 14 / 16;
                    tmpItem.Points[14].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 14 / 16;
                    tmpItem.Points[15].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 15 / 16;
                    tmpItem.Points[15].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 15 / 16;
                    tmpItem.Points[16].X = tmpItem.EndX;
                    tmpItem.Points[16].Y = tmpItem.EndY;

                    if (this.GetSchemaByteCount() + tmpItem.GetByteCount() < 3990)
                    {
                        schemaItemList.Add(tmpItem);
                    }

                    if (gMode == 11)
                    {
                        gSchema = schemaItemList.Count - 1;
                    }
                    else
                    {
                        gSchema = -1;
                    }

                    this.redrawSchema();

                    gMode = 0;
                    drawButton0.Checked = true;
                    gText = "";
                    gMarginX = -1;
                    gMarginY = -1;
                }
                else if (gMode % 10 == 2)
                {
                    if (gSchema >= 0 && gSchema < schemaItemList.Count)
                    {
                        schemaItemList.RemoveAt(gSchema);
                    }

                    SchemaItem tmpItem = new SchemaItem();
                    tmpItem.Kind = "楕円";
                    tmpItem.C = gColor;
                    tmpItem.StartX = gStartX;
                    tmpItem.StartY = gStartY;
                    tmpItem.EndX = gEndX;
                    tmpItem.EndY = gEndY;
                    tmpItem.Text = "";

                    tmpItem.Points = new Point[16];
                    tmpItem.Points[0].X = tmpItem.StartX;
                    tmpItem.Points[0].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 2;
                    tmpItem.Points[1].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 8;
                    tmpItem.Points[1].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 4;
                    tmpItem.Points[2].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 8;
                    tmpItem.Points[2].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 8;
                    tmpItem.Points[3].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 8;
                    tmpItem.Points[3].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 16;
                    tmpItem.Points[4].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 4 / 8;
                    tmpItem.Points[4].Y = tmpItem.StartY;
                    tmpItem.Points[5].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 5 / 8;
                    tmpItem.Points[5].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 16;
                    tmpItem.Points[6].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 6 / 8;
                    tmpItem.Points[6].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 8;
                    tmpItem.Points[7].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 7 / 8;
                    tmpItem.Points[7].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 4;
                    tmpItem.Points[8].X = tmpItem.EndX;
                    tmpItem.Points[8].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) / 2;
                    tmpItem.Points[9].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 7 / 8;
                    tmpItem.Points[9].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                    tmpItem.Points[10].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 6 / 8;
                    tmpItem.Points[10].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 7 / 8;
                    tmpItem.Points[11].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 5 / 8;
                    tmpItem.Points[11].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 15 / 16;
                    tmpItem.Points[12].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 4 / 8;
                    tmpItem.Points[12].Y = tmpItem.EndY;
                    tmpItem.Points[13].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 8;
                    tmpItem.Points[13].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 15 / 16;
                    tmpItem.Points[14].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 8;
                    tmpItem.Points[14].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 7 / 8;
                    tmpItem.Points[15].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 8;
                    tmpItem.Points[15].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;

                    if (this.GetSchemaByteCount() + tmpItem.GetByteCount() < 3990)
                    {
                        schemaItemList.Add(tmpItem);
                    }

                    if (gMode == 12)
                    {
                        gSchema = schemaItemList.Count - 1;
                    }
                    else
                    {
                        gSchema = -1;
                    }

                    this.redrawSchema();

                    gMode = 0;
                    drawButton0.Checked = true;
                    gText = "";
                    gMarginX = -1;
                    gMarginY = -1;
                }
                else if (gMode % 10 == 3)
                {
                    if (gSchema >= 0 && gSchema < schemaItemList.Count)
                    {
                        schemaItemList.RemoveAt(gSchema);
                    }

                    SchemaItem tmpItem = new SchemaItem();
                    tmpItem.Kind = "四角";
                    tmpItem.C = gColor;
                    tmpItem.StartX = gStartX;
                    tmpItem.StartY = gStartY;
                    tmpItem.EndX = gEndX;
                    tmpItem.EndY = gEndY;
                    tmpItem.Text = "";

                    tmpItem.Points = new Point[16];
                    tmpItem.Points[0].X = tmpItem.StartX;
                    tmpItem.Points[0].Y = tmpItem.StartY;
                    tmpItem.Points[1].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 4;
                    tmpItem.Points[1].Y = tmpItem.StartY;
                    tmpItem.Points[2].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 4;
                    tmpItem.Points[2].Y = tmpItem.StartY;
                    tmpItem.Points[3].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 4;
                    tmpItem.Points[3].Y = tmpItem.StartY;
                    tmpItem.Points[4].X = tmpItem.EndX;
                    tmpItem.Points[4].Y = tmpItem.StartY;
                    tmpItem.Points[5].X = tmpItem.EndX;
                    tmpItem.Points[5].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 1 / 4;
                    tmpItem.Points[6].X = tmpItem.EndX;
                    tmpItem.Points[6].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 2 / 4;
                    tmpItem.Points[7].X = tmpItem.EndX;
                    tmpItem.Points[7].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                    tmpItem.Points[8].X = tmpItem.EndX;
                    tmpItem.Points[8].Y = tmpItem.EndY;
                    tmpItem.Points[9].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 3 / 4;
                    tmpItem.Points[9].Y = tmpItem.EndY;
                    tmpItem.Points[10].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 2 / 4;
                    tmpItem.Points[10].Y = tmpItem.EndY;
                    tmpItem.Points[11].X = tmpItem.StartX + (tmpItem.EndX - tmpItem.StartX) * 1 / 4;
                    tmpItem.Points[11].Y = tmpItem.EndY;
                    tmpItem.Points[12].X = tmpItem.StartX;
                    tmpItem.Points[12].Y = tmpItem.EndY;
                    tmpItem.Points[13].X = tmpItem.StartX;
                    tmpItem.Points[13].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 3 / 4;
                    tmpItem.Points[14].X = tmpItem.StartX;
                    tmpItem.Points[14].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 2 / 4;
                    tmpItem.Points[15].X = tmpItem.StartX;
                    tmpItem.Points[15].Y = tmpItem.StartY + (tmpItem.EndY - tmpItem.StartY) * 1 / 4;

                    if (this.GetSchemaByteCount() + tmpItem.GetByteCount() < 3990)
                    {
                        schemaItemList.Add(tmpItem);
                    }

                    if (gMode == 13)
                    {
                        gSchema = schemaItemList.Count - 1;
                    }
                    else
                    {
                        gSchema = -1;
                    }

                    this.redrawSchema();

                    gMode = 0;
                    drawButton0.Checked = true;
                    gText = "";
                    gMarginX = -1;
                    gMarginY = -1;
                }
                else if (gMode % 10 == 4)
                {
                    if (gSchema >= 0 && gSchema < schemaItemList.Count)
                    {
                        schemaItemList.RemoveAt(gSchema);
                    }

                    if (gEndX <= gStartX)
                    {
                        gEndX = gStartX + 10;
                    }

                    if (gEndY <= gStartY)
                    {
                        gEndY = gStartY + 10;
                    }

                    TextBox tmp = new TextBox();
                    tmp.Location = new Point(gStartX, gStartY);
                    tmp.Size = new Size(gEndX - gStartX, gEndY - gStartY);
                    tmp.BorderStyle = BorderStyle.FixedSingle;
                    tmp.ImeMode = ImeMode.Hiragana;
                    tmp.Text = gText;
                    tmp.Multiline = true;
                    tmp.WordWrap = true;
                    tmp.ForeColor = gColor;
                    tmp.BackColor = Color.LightYellow;

                    tmp.Enter += new System.EventHandler(enterTextBox);
                    tmp.Leave += new System.EventHandler(leaveTextBox);
                    tmp.ContextMenuStrip = itemContextMenu;
                    schemaBox.Controls.Add(tmp);

                    tmp.Focus();

                    if (gMode == 14)
                    {
                        this.redrawSchema();
                    }
                }
                else if (gMode % 10 == 5)
                {
                    if (gMode == 15)
                    {
                        gFree = freeItemList.Count - 1;
                    }
                    else
                    {
                        if (this.GetSchemaByteCount() + gFreeItem.GetByteCount() < 3990)
                        {
                            freeItemList.Add(gFreeItem);
                        }

                        gFree = -1;
                    }

                    this.redrawSchema();

                    gMode = 0;
                    drawButton0.Checked = true;
                    gText = "";
                    gMarginX = -1;
                    gMarginY = -1;
                }

                schemaBox.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "エラー");
            }
        }

        private void enterTextBox(object sender, EventArgs e)
        {
            TextBox tmp = (TextBox)(sender);
            this.gText = tmp.Text;
            
            for (int i = 0; i < schemaItemList.Count; i++)
            {
                SchemaItem tmpItem = schemaItemList[i];

                if (tmpItem.Kind == "文字" && tmpItem.StartX == tmp.Location.X && tmpItem.StartY == tmp.Location.Y && tmpItem.EndX == tmp.Location.X + tmp.Width && tmpItem.EndY == tmp.Location.Y + tmp.Height)
                {
                    this.gSchema = i;
                    break;
                }
            }
        }

        private void leaveTextBox(object sender, EventArgs e)
        {
            TextBox tmp = (TextBox)(sender);
            tmp.BorderStyle = BorderStyle.None;

            if (tmp.Text.Length > 0)
            {
                if (gMode % 10 == 4)
                {
                    SchemaItem tmpItem = new SchemaItem();
                    tmpItem.Kind = "文字";
                    tmpItem.C = gColor;
                    tmpItem.StartX = gStartX;
                    tmpItem.StartY = gStartY;
                    tmpItem.EndX = gEndX;
                    tmpItem.EndY = gEndY;
                    tmpItem.Text = tmp.Text;

                    if (this.GetSchemaByteCount() + tmpItem.GetByteCount() < 3990)
                    {
                        schemaItemList.Add(tmpItem);
                    }
                }
                else if (gText.Length > 0)
                {
                    for (int i = 0; i < schemaItemList.Count; i++ )
                    {
                        SchemaItem item = schemaItemList[i];

                        if (item.StartX == tmp.Location.X && item.StartY == tmp.Location.Y && item.Text.Equals(gText))
                        {
                            if (this.GetSchemaByteCount() + System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(tmp.Text) - Encoding.GetEncoding("Shift_JIS").GetByteCount(gText) < 3990)
                            {
                                item.Text = tmp.Text;
                                schemaItemList[i] = item;
                            }

                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < schemaItemList.Count; i++)
                {
                    SchemaItem item = schemaItemList[i];

                    if (item.StartX == tmp.Location.X && item.StartY == tmp.Location.Y && item.Text.Equals(gText))
                    {
                        schemaItemList.RemoveAt(i);
                        break;
                    }
                }

                tmp.Dispose();
            }

            gMode = 0;
            drawButton0.Checked = true;
            gText = "";
            gSchema = -1;

            gMarginX = -1;
            gMarginY = -1;

            this.redrawSchema();
        }

        private void redrawSchema()
        {
            schemaBox.Image = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            g = Graphics.FromImage(schemaBox.Image);

            if (schemaBgBox.Text.Length > 0)
            {
                DataTable table = mSet.Tables["Schema"];

                foreach (DataRow tmpRow in table.Rows)
                {
                    if (tmpRow.ItemArray[0].ToString() == schemaBgBox.Text.Trim())
                    {
                        string f = AppFile.FilePath(tmpRow.ItemArray[2].ToString());

                        if (f.Length > 0)
                        {
                            schemaBox.BackgroundImage = Image.FromFile(f);
                        }

                        break;
                    }
                }
            }
            else
            {
                schemaBox.BackgroundImage = null;
            }

            schemaBox.Controls.Clear();

            foreach (SchemaItem d in schemaItemList)
            {
                if (d.Kind == "直線")
                {
                    g.DrawLine(new Pen(d.C, 2), d.StartX, d.StartY, d.EndX, d.EndY);
                }
                else if (d.Kind == "楕円")
                {
                    g.DrawEllipse(new Pen(d.C, 2), d.StartX, d.StartY, d.EndX - d.StartX, d.EndY - d.StartY);
                }
                else if (d.Kind == "四角")
                {
                    g.DrawRectangle(new Pen(d.C, 2), d.StartX, d.StartY, d.EndX - d.StartX, d.EndY - d.StartY);
                }
                else if (d.Kind == "文字")
                {
                    TextBox tmp = new TextBox();
                    tmp.Location = new Point(d.StartX, d.StartY);
                    tmp.Size = new Size(d.EndX - d.StartX, d.EndY - d.StartY);
                    tmp.BorderStyle = BorderStyle.None;
                    tmp.ImeMode = ImeMode.Hiragana;
                    tmp.Text = d.Text;
                    tmp.Multiline = true;
                    tmp.WordWrap = true;
                    tmp.ForeColor = d.C;
                    tmp.BackColor = Color.LightYellow;

                    tmp.Enter += new System.EventHandler(enterTextBox);
                    tmp.Leave += new System.EventHandler(leaveTextBox);
                    tmp.ContextMenuStrip = itemContextMenu;
                    schemaBox.Controls.Add(tmp);
                }
            }

            // フリー描画
            for (int i = 0; i < freeItemList.Count; i++)
            {
                Pen pen2 = new Pen(freeItemList[i].C, 2);

                for (int j = 1; j < freeItemList[i].PointList.Count; j++)
                {
                    g.DrawLine(pen2, freeItemList[i].PointList[j - 1], freeItemList[i].PointList[j]);
                }
            }

            // 選択状態になっているものがあれば赤く描画
            if (gSchema >= 0 && gSchema < schemaItemList.Count)
            {
                SchemaItem tmpItem = schemaItemList[gSchema];

                int sx = tmpItem.StartX;
                int sy = tmpItem.StartY;
                int ex = tmpItem.EndX;
                int ey = tmpItem.EndY;

                Pen p3 = new Pen(Color.Red, 3);
                Pen p4 = new Pen(Color.Red, 4);

                if (tmpItem.Kind.Contains("直線"))
                {
                    g.DrawLine(p3, sx, sy, ex, ey);
                }
                else if (tmpItem.Kind.Contains("楕円"))
                {
                    g.DrawEllipse(p3, sx, sy, ex - sx, ey - sy);
                }
                else if (tmpItem.Kind.Contains("四角"))
                {
                    g.DrawRectangle(p3, sx, sy, ex - sx, ey - sy);
                }
                else if (tmpItem.Kind.Contains("文字"))
                {
                    g.DrawRectangle(p4, sx, sy, ex - sx, ey - sy);
                }
            }
            else if (gFree >= 0 && gFree < freeItemList.Count)
            {
                FreeItem tmpItem = freeItemList[gFree];

                for (int j = 1; j < tmpItem.PointList.Count; j++)
                {
                    Pen p3 = new Pen(Color.Red, 3);

                    g.DrawLine(p3, tmpItem.PointList[j - 1], tmpItem.PointList[j]);
                }
            }

            schemaBox.Refresh();
        }

        private void clearItem_Click(object sender, EventArgs e)
        {
            gStart = 0;
            gEnd = 0;

            gMode = 0;
            drawButton0.Checked = true;
            gText = "";
            gSchema = -1;
            gFree = -1;
            gFreeItem = new FreeItem();

            this.redrawSchema();
        }

        private void delItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((gSchema >= 0 || gFree >= 0) && MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (gSchema >= 0 && gSchema < schemaItemList.Count)
                    {
                        schemaItemList.RemoveAt(gSchema);
                        gSchema = -1;
                        this.redrawSchema();
                    }
                    else if (gFree >= 0 && gFree < freeItemList.Count)
                    {
                        freeItemList.RemoveAt(gFree);
                        gFree = -1;
                        this.redrawSchema();
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        private void drawButton0_CheckedChanged(object sender, EventArgs e)
        {
            if (drawButton0.Checked)
            {
                gMode = 0;
            }
        }

        private void drawButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (drawButton1.Checked)
            {
                gSchema = -1;
                gFree = -1;
                gMode = 1;
            }
        }

        private void drawButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (drawButton2.Checked)
            {
                gSchema = -1;
                gFree = -1;
                gMode = 2;
            }
        }

        private void drawButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (drawButton3.Checked)
            {
                gSchema = -1;
                gFree = -1;
                gMode = 3;
            }
        }

        private void drawButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (drawButton4.Checked)
            {
                gSchema = -1;
                gFree = -1;
                gMode = 4;
            }
        }

        private void drawButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (drawButton5.Checked)
            {
                gSchema = -1;
                gFree = -1;
                gMode = 5;

                gFreeItem = new FreeItem();
            }
        }

        private void colorBox1_Click(object sender, EventArgs e)
        {
            foreach (PictureBox p in schemaColorDict.Keys)
            {
                if (p.Equals(colorBox1))
                {
                    p.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    p.BorderStyle = BorderStyle.None;
                }
            }

            gColor = colorBox1.BackColor;
            gPen.Color = colorBox1.BackColor;
        }

        private void colorBox2_Click(object sender, EventArgs e)
        {
            foreach (PictureBox p in schemaColorDict.Keys)
            {
                if (p.Equals(colorBox2))
                {
                    p.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    p.BorderStyle = BorderStyle.None;
                }
            }

            gColor = colorBox2.BackColor;
            gPen.Color = colorBox2.BackColor;
        }

        private void colorBox3_Click(object sender, EventArgs e)
        {
            foreach (PictureBox p in schemaColorDict.Keys)
            {
                if (p.Equals(colorBox3))
                {
                    p.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    p.BorderStyle = BorderStyle.None;
                }
            }

            gColor = colorBox3.BackColor;
            gPen.Color = colorBox3.BackColor;
        }

        private void colorBox4_Click(object sender, EventArgs e)
        {
            foreach (PictureBox p in schemaColorDict.Keys)
            {
                if (p.Equals(colorBox4))
                {
                    p.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    p.BorderStyle = BorderStyle.None;
                }
            }

            gColor = colorBox4.BackColor;
            gPen.Color = colorBox4.BackColor;
        }

        private void clearSchemaButton_Click(object sender, EventArgs e)
        {
            if (schemaItemList.Count > 0)
            {
                DialogResult tmpResult = MessageBox.Show("記載中の図があります。保存しますか？", "確認", MessageBoxButtons.YesNoCancel);

                if (tmpResult == DialogResult.Yes)
                {
                    this.regSchema();
                }
                else if (tmpResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            this.clearSchemas();
        }

        private void regSchemaButton_Click(object sender, EventArgs e)
        {
            this.regSchema();
        }

        private void regSchema()
        {
            int schema_id = -1;

            if (this.schemaIdBox.Text.Length > 0)
            {
                for (int i = 0; i < SchemaTagList.Count; i++)
                {
                    SchemaTag sTag = SchemaTagList[i];

                    if (this.schemaIdBox.Text == sTag.Id)
                    {
                        schema_id = i;
                        break;
                    }
                }
            }

            if (schema_id >= 0)
            {
                SchemaTag tmpTag = SchemaTagList[schema_id];

                tmpTag.Bg = this.schemaBgBox.Text;

                string tmp_item = "";

                foreach (SchemaItem si in schemaItemList)
                {
                    tmp_item += si.Kind + "," + si.C.Name + "," + si.StartX + "," + si.StartY + "," + si.EndX + "," + si.EndY;

                    if (si.Kind == "文字")
                    {
                        tmp_item += "," + si.Text.Replace("\r\n", "<CR+LF>");
                    }

                    tmp_item += "\r\n";
                }

                foreach (FreeItem fi in freeItemList)
                {
                    tmp_item += "フリー," + fi.C.Name;

                    foreach (Point pi in fi.PointList)
                    {
                        tmp_item += "," + pi.X + "," + pi.Y;
                    }

                    tmp_item += "\r\n";
                }

                tmpTag.Item = tmp_item;

                SchemaTagList[schema_id] = tmpTag;
            }
            else
            {
                int max_id = 1;

                foreach (SchemaTag sTag in SchemaTagList)
                {
                    if (int.Parse(sTag.Id) > max_id)
                    {
                        max_id = int.Parse(sTag.Id);
                    }
                }

                max_id++;

                SchemaTag tmpTag = new SchemaTag();

                tmpTag.Id = max_id.ToString();
                tmpTag.Bg = this.schemaBgBox.Text;

                string tmp_item = "";

                foreach (SchemaItem si in schemaItemList)
                {
                    tmp_item += si.Kind + "," + si.C.Name + "," + si.StartX + "," + si.StartY + "," + si.EndX + "," + si.EndY;

                    if (si.Kind == "文字")
                    {
                        tmp_item += "," + si.Text.Replace("\r\n", "<CR+LF>");
                    }

                    tmp_item += "\r\n";
                }

                foreach (FreeItem fi in freeItemList)
                {
                    tmp_item += "フリー," + fi.C.Name;

                    foreach (Point pi in fi.PointList)
                    {
                        tmp_item += "," + pi.X + "," + pi.Y;
                    }

                    tmp_item += "\r\n";
                }

                tmpTag.Item = tmp_item;

                SchemaTagList.Add(tmpTag);
            }

            this.clearSchemas();
            this.redrawSchemas();
        }

        private void delSchemaButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この図を削除しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                int schema_id = -1;

                if (this.schemaIdBox.Text.Length > 0)
                {
                    for (int i = 0; i < SchemaTagList.Count; i++)
                    {
                        SchemaTag sTag = SchemaTagList[i];

                        if (this.schemaIdBox.Text == sTag.Id)
                        {
                            schema_id = i;
                            break;
                        }
                    }
                }

                if (schema_id >= 0)
                {
                    SchemaTagList.RemoveAt(schema_id);
                }

                this.clearSchemas();
                this.redrawSchemas();
            }
        }

        private void wordContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.wordContextMenu.Items.Clear();

            ToolStripMenuItem tmpItem = new ToolStripMenuItem();
            tmpItem.Text = "器械出Ns交代 : ";
            tmpItem.Click += new EventHandler(InsertWord);
            this.wordContextMenu.Items.Add(tmpItem);

            tmpItem = new ToolStripMenuItem();
            tmpItem.Text = "外回Ns交代 : ";
            tmpItem.Click += new EventHandler(InsertWord);
            this.wordContextMenu.Items.Add(tmpItem);

            tmpItem = new ToolStripMenuItem();
            tmpItem.Text = "記録Ns交代 : ";
            tmpItem.Click += new EventHandler(InsertWord);
            this.wordContextMenu.Items.Add(tmpItem);

            this.wordContextMenu.Items.Add(new ToolStripSeparator());

            DataTable table = mSet.Tables["Dict"];

            foreach (DataRow r in table.Rows)
            {
                tmpItem = new ToolStripMenuItem();

                tmpItem.Text = r.ItemArray[0].ToString();
                tmpItem.Click += new EventHandler(InsertWord);

                this.wordContextMenu.Items.Add(tmpItem);
            }
        }

        private void InsertNs(object sender, EventArgs e)
        {
            int pos = recText.SelectionStart;
            ToolStripMenuItem tmpItem = (ToolStripMenuItem)(sender);

            recText.Text = recText.Text.Insert(recText.SelectionStart, tmpItem.Text + LoginUser.Name);
            recText.SelectionStart = pos + tmpItem.Text.Length + LoginUser.Name.Length;
        }

        private void InsertWord(object sender, EventArgs e)
        {
            int pos = recText.SelectionStart;
            ToolStripMenuItem tmpItem = (ToolStripMenuItem)(sender);

            recText.Text = recText.Text.Insert(recText.SelectionStart, tmpItem.Text);
            recText.SelectionStart = pos + tmpItem.Text.Length;
        }

        private void pdfMenuItem_Click(object sender, EventArgs e)
        {
            PatBase p = PatBase.Load(ptIdBox.Text);
            p.WritePatCSV();
            Launcher.PdfViewer();
        }

        private void recHistAddButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("診断記録者を追加しますか？", "確認", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                recHistBox.ListAdd(DateTime.Now.ToString("HH:mm").PadLeft(5, ' ') + " 診断 " + LoginUser.Name);
            }
        }

        private void recHistAddButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("評価記録者を追加しますか？", "確認", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                recHistBox.ListAdd(DateTime.Now.ToString("HH:mm").PadLeft(5, ' ') + " 評価 " + LoginUser.Name);
            }
        }

        private void recHistAddButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("記録者を追加しますか？", "確認", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                recHistBox.ListAdd(DateTime.Now.ToString("HH:mm").PadLeft(5, ' ') + " 記録 " + LoginUser.Name);
            }
        }
    }

    public class ListBoxEx : ListBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.SelectedIndex < 0)
                {
                    return;
                }

                if (MessageBox.Show("選択されているものを削除しますか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }

                this.Items.RemoveAt(this.SelectedIndex);
            }
        }

        public bool ListAdd(string s)
        {
            bool result = false;

            string[] ss = s.Split(' ');

            if (ss.Length < 3)
            {
                return result;
            }

            // 直前のスタッフと行為が同じならば直前の Time のみ変更して false を返す
            if (Items.Count > 0)
            {
                string[] s0 = Items[Items.Count - 1].ToString().Split(' ');

                if (s0.Length == ss.Length)
                {
                    if (s0[1].Equals(ss[1]) && s0[2].Equals(ss[2]))
                    {
                        Items[Items.Count - 1] = s;
                    }
                    else
                    {
                        Items.Add(s);
                    }
                }
            }
            else
            {
                Items.Add(s);
            }

            result = true;

            return result;
        }

        public void MakeList(string hist)
        {
            this.Items.Clear();

            foreach (string s in hist.Split('\r', '\n'))
            {
                if (s.Length > 0)
                {
                    Items.Add(s);
                }
            }
        }

        public string HistString
        {
            get
            {
                string result = "";

                foreach (string s in this.Items)
                {
                    if (result.Length > 0)
                    {
                        result += "\r\n";
                    }

                    result += s;
                }

                return result;
            }
        }
    }
}