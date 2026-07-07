using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormDiagDPC : StdForm1
    {
        public FormDiagDPC()
        {
            InitializeComponent();
        }

        private void FormDiagDPC_Load(object sender, EventArgs e)
        {
            this.DiagCheckBox1.Checked = true;
            this.DiagCheckBox2.Checked = true;
            this.SaveButton1.Visible = true;
        }

        private void FormDiagDPC_Shown(object sender, EventArgs e)
        {
            if (LoginUser.QualId.Equals("1"))
            {
                this.FontSet(AppFont.F10);
            }
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.InHistoryBox1.Init(p.Id);

            if (this.InHistoryBox1.Items.Count > 0)
            {
                this.InHistoryBox1.SelectedIndex = 0;
            }

            this.ListShow();
        }

        public override void FontSet(AppFont f)
        {
            base.FontSet(f);

            this.stdControlFont11.FontSet(f);

            this.ListFormat();
        }

        void ListShow()
        {
            this.DiagDPCGridView1.ListClear();
            this.SaveLabel.Text = "";

            if (this.InHistoryBox1.SelectedItem == null)
            {
                return;
            }

            PatIn pin = (PatIn)this.InHistoryBox1.SelectedItem;

            this.DiagDPCGridView1.ListShow(this.Pat.Id, pin.SEQ, "開始日 desc, 連番 desc", "");

            List<DiagDPC> list = DiagDPC.GetList(this.Pat.Id, pin.SEQ);

            foreach (DiagDPC diag in list)
            {
                if (DateTimeAgent.IsDate(diag.UpDate))
                {
                    this.SaveLabel.Text = diag.UpDateTime + "  " + diag.UpStaffName;
                }
                else
                {
                    this.SaveLabel.Text = diag.RegDateTime + "  " + diag.RegStaffName;
                }

                break;
            }

            if (this.SaveLabel.PreferredWidth + 10 > 180)
            {
                this.SaveLabel.Width = this.SaveLabel.PreferredWidth + 10;
            }

            this.ListFormat();
        }

        void ListFormat()
        {
            List<string> filters = new List<string>();

            if (this.DiagCheckBox1.Checked)
            {
                filters.Add("転帰区分 = ''");
            }

            if (this.DiagCheckBox2.Checked)
            {
                filters.Add("転帰区分 <> ''");
            }

            if (filters.Count == 0)
            {
                filters.Add("転帰区分 = 'A'");
            }

            this.DiagDPCGridView1.ListFormat("開始日 desc, 連番 desc", AppString.ConcatList(filters, " or "));

            this.DiagDPCGridView1.Columns["病名"].Width = 180;

            if (this.Font.Size > 9)
            {
                foreach (DataGridViewColumn c in DiagDPCGridView1.Columns)
                {
                    c.Width = (int)(c.Width * (float)(this.Font.Size / 9));
                }
            }
        }

        private void DiagCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void DiagCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.ListFormat();
        }

        private void InHistoryBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListShow();
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            List<string> msgs = new List<string>();

            if (this.Pat.Id.Length == 0)
            {
                msgs.Add("患者IDが入力されていません");
            }

            if (this.InHistoryBox1.Text.Length == 0)
            {
                msgs.Add("入院期間が選択されていません");
            }

            if (msgs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                return;
            }

            PatIn pin = (PatIn)this.InHistoryBox1.SelectedItem;

            List<DiagDPC> list = new List<DiagDPC>();

            // チェックされたデータを挿入する
            foreach (DataGridViewRow r in this.DiagDPCGridView1.Rows)
            {
                DiagDPC obj = new DiagDPC();
                Diag diag = (Diag)r.Cells["Obj"].Value;

                obj.PtId = this.Pat.Id;
                obj.SEQ = diag.SEQ;

                obj.MainFlg = (r.Cells["主傷病"].Value.ToString().Equals("True"));
                obj.TriggerFlg = (r.Cells["契機傷病"].Value.ToString().Equals("True"));
                obj.ResourceFlg1 = (r.Cells["資源1"].Value.ToString().Equals("True"));
                obj.ResourceFlg2 = (r.Cells["資源2"].Value.ToString().Equals("True"));
                obj.SubFlg = (r.Cells["併存症"].Value.ToString().Equals("True"));
                obj.AfterFlg = (r.Cells["入院後発症"].Value.ToString().Equals("True"));
                obj.DeleteFlg = false;

                // いずれかのフラグが立っていれば登録する
                if (obj.MainFlg || obj.TriggerFlg || obj.ResourceFlg1 || obj.ResourceFlg2 ||
                    obj.SubFlg || obj.AfterFlg)
                {
                    list.Add(obj);

                    if (r.Cells["留意病名1"].Value.ToString().Length > 0 || r.Cells["留意病名2"].Value.ToString().Length > 0)
                    {
                        msgs.Add(diag.DiagName);
                    }
                }
            }

            if (msgs.Count > 0 && this.DiagDPCGridView1.ICDNotice)
            {
                string s = AppString.ConcatList(msgs, ", ");

                if (msgs.Count == 1)
                {
                    s += " は留意すべきICD分類名称です。このまま登録しますか？";
                }
                else
                {
                    s += Environment.NewLine + "これらは留意すべきICD分類名称です。このまま登録しますか？";
                }

                if (MessageBox.Show(s, "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }

            DiagDPC.Save(pin.Id, pin.SEQ, list);

            MessageBox.Show("登録しました");
            this.ListShow();
        }
    }
}
