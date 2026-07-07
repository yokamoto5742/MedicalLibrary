using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Media;
using System.IO;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormBillPayCheckIn : Form
    {
        enum Mode : int
        {
            Waiting = 0,
            Success = 1,
            Error = 2
        }

        string PtId = "";

        PatOut _patOut = new PatOut();

        PatOut _PatOut
        {
            get
            {
                if (!this._patOut.Id.Equals(this.PtId))
                {
                    List<PatOut> list = PatOut.GetOnedayLast(this.PtId, DateTime.Now.ToString("yyyyMMdd"));

                    foreach (PatOut obj in list)
                    {
                        this._patOut = obj;
                        break;
                    }
                }

                return this._patOut;
            }
        }

        SoundPlayer _Player1;

        Mode _mode = Mode.Waiting;

        Mode _Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;

                if (this._mode == Mode.Waiting)
                {
                    this.NumLabel1.Visible = false;
                    this.NumLabel2.Visible = false;
                    this.NumLabel2.Text = "";

                    this.MsgLabel.Location = new Point(0, 200);
                    this.MsgLabel.Text = "受付票を\r\nバーコードリーダーに\r\n通してください";
                    this.MsgLabel.ForeColor = Color.Black;
                    this.MsgLabel.BackColor = Color.White;

                    this.BackColor = Color.White;
                }
                else if (this._mode == Mode.Success)
                {
                    this.NumLabel1.Visible = true;
                    this.NumLabel2.Visible = true;
                    this.NumLabel2.Text = this._PatOut.Seq1;

                    this.MsgLabel.Location = new Point(0, 320);
                    this.MsgLabel.Text = "受付票を持って\r\nお待ちください";
                    this.MsgLabel.ForeColor = Color.Black;
                    this.MsgLabel.BackColor = Color.FromArgb(255, 224, 224);

                    this.BackColor = Color.FromArgb(255, 224, 224);

                    this._Timer.Interval = 3 * 1000;
                    this._Timer.Start();
                }
                else if (this._mode == Mode.Error)
                {
                    this.NumLabel1.Visible = false;
                    this.NumLabel2.Visible = false;
                    this.NumLabel2.Text = "";

                    this.MsgLabel.Location = new Point(0, 200);
                    this.MsgLabel.Text = "受付できませんでした\r\n恐れ入りますが\r\n係にお渡しください";
                    this.MsgLabel.ForeColor = Color.Red;
                    this.MsgLabel.BackColor = Color.Yellow;

                    this.BackColor = Color.Yellow;

                    this._Player1.Play();

                    this._Timer.Interval = 4 * 1000;
                    this._Timer.Start();
                }

                this.PatClear();
            }
        }

        Timer _Timer = new Timer();


        public FormBillPayCheckIn()
        {
            InitializeComponent();
        }

        private void FormBillPayCheckIn_Load(object sender, EventArgs e)
        {
            this.NumLabel1.ForeColor = Color.Black;
            this.NumLabel1.BackColor = Color.FromArgb(255, 224, 224);

            this.NumLabel2.ForeColor = Color.Blue;
            this.NumLabel2.BackColor = Color.FromArgb(255, 224, 224);

            this.NumLabel1.Visible = false;
            this.NumLabel2.Visible = false;
            this.NumLabel2.Text = "";

            // デバッグ用
/*
            this.NumLabel1.Visible = true;
            this.NumLabel2.Visible = true;
            this.NumLabel2.Text = "9999";
*/
            this.PatSeqNeedBox.Checked = true;

            foreach (BillPayPlace obj in BillPayPlace.Dict.Values)
            {
                this.PlaceBox.Items.Add(obj);
            }

            if (BillPayPlace.Dict.ContainsKey(LibSettings.Current.PC.BillPayPlace))
            {
                this.PlaceBox.SelectedItem = BillPayPlace.Dict[LibSettings.Current.PC.BillPayPlace];
            }
            else
            {
                this.PlaceBox.SelectedItem = BillPayPlace.Dict["1"];
            }

            string sound_file = AppFile.FilePath("KaikeiErrorMessage.wav");

            if (File.Exists(sound_file))
            {
                this._Player1 = new SoundPlayer(sound_file);
            }
            else
            {
                this._Player1 = new SoundPlayer();
            }

            this._Timer.Interval = 3 * 1000;
            this._Timer.Tick += new EventHandler(_Timer_Tick);

            this._Mode = Mode.Waiting;

            this.WindowState = FormWindowState.Maximized;
        }

        void _Timer_Tick(object sender, EventArgs e)
        {
            if (this._Mode == Mode.Success)
            {
                this._Mode = Mode.Waiting;
            }
            else if (this._Mode == Mode.Error)
            {
                this._Mode = Mode.Waiting;
            }

            this._Timer.Stop();
        }

        private void FormBillPayCheckIn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // 患者IDが空の場合は弾く
                    if (this.PtId.Length == 0)
                    {
                        this._Mode = Mode.Error;
                        return;
                    }

                    /*
                    // 患者IDが不正な値ならば弾く
                    int pt_id = 0;

                    if (!int.TryParse(this.PtId, out pt_id))
                    {
                        this._Mode = Mode.Error;
                        return;
                    }

                    if (pt_id > 999999999)
                    {
                        this._Mode = Mode.Error;
                        return;
                    }
                     */

                    // 受付していない方は弾く
                    if (this.PatSeqNeedBox.Checked)
                    {
                        if (this._PatOut.Id.Length == 0)
                        {
                            this._Mode = Mode.Error;
                            return;
                        }
                    }

                    BillPay obj = new BillPay();
                    obj.PtId = this.PtId;
                    obj.ArDate = DateTime.Now.ToString("yyyyMMdd");
                    obj.ArTime = DateTime.Now.ToString("HHmmss");
                    obj.ArPlace = ((BillPayPlace)this.PlaceBox.SelectedItem).Code;
                    obj.Status = 0;

                    StdReturn sr = obj.Save();

                    if (sr.ErrExist)
                    {
                        this._Mode = Mode.Error;
                        return;
                    }
                    else
                    {
                        this._Mode = Mode.Success;
                        return;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this._Mode = Mode.Waiting;
                }
            }
            catch (Exception ex)
            {
                this._Mode = Mode.Waiting;
                LibUtility.Except(ex, false);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 患者データをクリアする
        /// </summary>
        void PatClear()
        {
            this.PtId = "";
            this._patOut = new PatOut();
        }

        private void FormBillPayCheckIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar >= '0' && e.KeyChar <= '9')
                {
                    this.PtId += e.KeyChar.ToString();
                }
            }
            catch (Exception ex)
            {
                this.PatClear();
                LibUtility.Except(ex, false);
            }
            finally
            {
            }
        }

        private void PlaceLabel_DoubleClick(object sender, EventArgs e)
        {
            this.PatClear();

            // PlaceBox の選択可否モードを変更する
            this.PlaceBox.Enabled = !this.PlaceBox.Enabled;
        }

        private void FormBillPayCheckIn_Resize(object sender, EventArgs e)
        {
            int w = this.Width;

            this.NumLabel1.Width = this.Width / 2 - 100;

            this.NumLabel2.Location = new Point(this.Width / 2 - 100, this.NumLabel2.Location.Y);
            this.NumLabel2.Width = this.Width / 2 + 100;
        }

        private void FormBillPayCheckIn_DoubleClick(object sender, EventArgs e)
        {
            this.WindowModeChange();
        }

        private void MsgLabel_DoubleClick(object sender, EventArgs e)
        {
            this.WindowModeChange();
        }

        void WindowModeChange()
        {
            this._Mode = Mode.Waiting;

            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.PatSeqNeedBox.Visible = true;
                this.PlaceLabel.Visible = true;
                this.PlaceBox.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                this.PatSeqNeedBox.Visible = false;
                this.PlaceLabel.Visible = false;
                this.PlaceBox.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }
    }
}
