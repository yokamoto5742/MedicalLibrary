using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class CanonRKF1Form : Form
    {
        SerialPort port;
        string file = "c:\\transfile\\data\\ref.dat";

        SerialStatus status = SerialStatus.Close;

        public CanonRKF1Form()
        {
            InitializeComponent();
        }

        private void CanonRFK1Form_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                return;
            }

            foreach (string s in ports)
            {
                PortBox.Items.Add(s);
            }

            if (ports.Length == 1)
            {
                PortBox.Text = ports[0];
            }

            if (File.Exists(AppFile.FilePath("CanonRKF1.xml")))
            {
                DataSet d_set = new DataSet();

                d_set.ReadXml(AppFile.FilePath("CanonRKF1.xml"));

                foreach (DataRow r in d_set.Tables["PC"].Rows)
                {
                    if (r["Name"].ToString().Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        file = r["File"].ToString();
                        PortBox.Text = r["Com"].ToString();
                        break;
                    }
                }
            }

            if (!Directory.Exists(AppFile.PathName(file)))
            {
                Directory.CreateDirectory(AppFile.PathName(file));
            }

            ConnectButton.Select();

            timer1.Start();
        }

        void PortConnect()
        {
            if (port != null)
            {
                port.Dispose();
            }

            port = new SerialPort(PortBox.Text);

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            port.DtrEnable = true;
            port.RtsEnable = true;

            port.Open();

            StatusLabel.Text = "接続中";
            StatusLabel.BackColor = Color.LightPink;
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // スレッドプールで実行されるため、ここで例外を漏らすと EyeData ごと終了する
            try
            {
                if (IsDisposed)
                {
                    return;
                }

                string rsv = ReadData();

                RsvBox.Invoke(new Action(() => RsvBox.Text += rsv));

                if (status == SerialStatus.Close)
                {
                    SaveFile();
                }
            }
            catch (Exception)
            {
                // 画面を閉じた直後の受信・ファイル書き込み失敗は無視する
            }
        }

        /// <summary>
        /// 受信データを読み取る。受信エラー時はエラーメッセージを返す。
        /// </summary>
        string ReadData()
        {
            try
            {
                if (status == SerialStatus.Close)
                {
                    RsvBox.Invoke(new Action(() => RsvBox.Clear()));

                    status = SerialStatus.Open;
                }

                string rsv = port.ReadExisting();

                if (rsv[rsv.Length - 1] == (char)0x17 || rsv[rsv.Length - 1] == (char)0x03)
                {
                    byte[] s_data = new byte[1];
                    s_data[0] = (byte)0x06;

                    port.Write(s_data, 0, 1);

                    rsv = rsv.TrimEnd((char)0x03).TrimEnd((char)0x17);
                }
                else if (rsv[rsv.Length - 1] == (char)0x04)
                {
                    status = SerialStatus.Close;

                    rsv = rsv.TrimEnd((char)0x04);
                }

                return rsv.TrimStart((char)0x02);
            }
            catch (Exception ex)
            {
                status = SerialStatus.Close;

                return ex.Message;
            }
        }

        void SaveFile()
        {
            File.WriteAllText(file, RsvBox.Text, Encoding.Default);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PortConnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("接続できませんでした。\n" + ex.Message, "Canon RKF");
            }
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            SaveFile();

            MessageBox.Show("ファイル出力されました");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (port != null)
            {
                port.Dispose(); // Close も兼ねる
            }

            timer1.Stop();

            base.OnFormClosed(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                StatusLabel.Text = "接続中";
                StatusLabel.BackColor = Color.LightPink;
            }
            else
            {
                StatusLabel.Text = "切断";
                StatusLabel.BackColor = Color.White;
            }
        }
    }

    enum SerialStatus : int
    {
        Close = 0,
        Open = 1
    }
}