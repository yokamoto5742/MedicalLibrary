using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading;
using System.Windows.Forms;
using MedicalLibrary.Boundary;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class MC500Form : StdForm1
    {
        List<MC500> MC500List = new List<MC500>();

        DataSet dSet = new DataSet();

        string XMLPath1 = "";
        string XMLPath2 = "";
        string XMLPath3 = "";

        public MC500Form()
        {
            InitializeComponent();
        }

        private void MC500Form_Load(object sender, EventArgs e)
        {
            try
            {
                LibSettings.Init();

                this.InitShow(Environment.GetCommandLineArgs());

                if (LoginUser.Status == LoginUser.STATUS.NONE)
                {
                    this.Dispose();
                }

                string xml_file = AppFile.FilePath("EyeMachine.xml");

                StreamReader reader = new StreamReader(xml_file, Encoding.GetEncoding("shift-jis"));
                dSet.ReadXml(reader);
                reader.Close();

                XMLPath1 = dSet.Tables["Config"].Rows[0]["XMLPath1"].ToString();
                XMLPath2 = dSet.Tables["Config"].Rows[0]["XMLPath2"].ToString();
                XMLPath3 = dSet.Tables["Config"].Rows[0]["XMLPath3"].ToString();

                DataTable table = dSet.Tables.Add("Files");

                table.Columns.Add("ID");
                table.Columns.Add("氏名");
                table.Columns.Add("日付");
                table.Columns.Add("左右");
                table.Columns.Add("ファイル");
                table.Columns.Add("取込", typeof(bool));

                LoginUserBox.Text = LoginUser.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Dispose();
            }
        }

        void InitShow(string[] args = null)
        {
            LoginUser.Init(true, args);
        }

        /// <summary>
        /// XMLファイルを読み込み、取り込んだデータを返す
        /// </summary>
        public static List<MC500> ReadFile(string xml_path)
        {
            List<MC500> list = new List<MC500>();

            // フォルダにあるXMLファイルを読み込む
            string[] files = Directory.GetFiles(xml_path, "*.xml");

            // XML読み込み設定
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            foreach (string file in files)
            {
                int i = file.LastIndexOf('\\');

                if (i < 0 || i + 1 >= file.Length)
                {
                    continue;
                }

                MC500 data = new MC500();

                data.XMLPath = xml_path;

                string f = file.Substring(file.LastIndexOf('\\') + 1);

                data.XMLFile = f;
                data.PtId = f.Substring(6, 14).Trim();
                data.LaserDate = f.Substring(21, 8).Trim();
                data.LaserTime = f.Substring(30, 6).Trim();
                data.Eye = f.Substring(37, 1).Trim();
//                data.SEQ = (int.Parse(f.Substring(39, 2)) + 1);

                StreamReader stream = new StreamReader(file, Encoding.GetEncoding("utf-16"));
                XmlReader reader = XmlReader.Create(stream);

                while (reader.Read())
                {
                    reader.ReadToFollowing("LaserShot");

                    if (reader.GetAttribute("type") == null || reader.GetAttribute("type").Length < 1)
                    {
                        continue;
                    }

                    MC500Element m = new MC500Element();

                    m.LaserShot = reader.GetAttribute("type");

                    // Power
                    reader.ReadToFollowing("Power");

                    reader.ReadToFollowing("Min");
                    m.PowerMin = reader.GetAttribute("unit");
                    m.PowerMin = reader.ReadString() + m.PowerMin;

                    reader.ReadToFollowing("Max");
                    m.PowerMax = reader.GetAttribute("unit");
                    m.PowerMax = reader.ReadString() + m.PowerMax;

                    reader.ReadToFollowing("Ave");
                    m.PowerAve = reader.GetAttribute("unit");
                    m.PowerAve = reader.ReadString() + m.PowerAve;

                    // Time
                    reader.ReadToFollowing("Time");

                    reader.ReadToFollowing("Min");
                    m.TimeMin = reader.GetAttribute("unit");
                    m.TimeMin = reader.ReadString() + m.TimeMin;

                    reader.ReadToFollowing("Max");
                    m.TimeMax = reader.GetAttribute("unit");
                    m.TimeMax = reader.ReadString() + m.TimeMax;

                    reader.ReadToFollowing("Ave");
                    m.TimeAve = reader.GetAttribute("unit");
                    m.TimeAve = reader.ReadString() + m.TimeAve;

                    reader.ReadToFollowing("ActAve");
                    m.TimeActAve = reader.GetAttribute("unit");
                    m.TimeActAve = reader.ReadString() + m.TimeActAve;

                    // SpotSize
                    reader.ReadToFollowing("SpotSize");

                    reader.ReadToFollowing("Min");
                    m.SpotSizeMin = reader.GetAttribute("unit");
                    m.SpotSizeMin = reader.ReadString() + m.SpotSizeMin;

                    reader.ReadToFollowing("Max");
                    m.SpotSizeMax = reader.GetAttribute("unit");
                    m.SpotSizeMax = reader.ReadString() + m.SpotSizeMax;

                    reader.ReadToFollowing("Ave");
                    m.SpotSizeAve = reader.GetAttribute("unit");
                    m.SpotSizeAve = reader.ReadString() + m.SpotSizeAve;

                    // ShotNo
                    reader.ReadToFollowing("ShotNo");
                    m.ShotNo = reader.ReadString() + "shot";

                    // ActEnergy
                    reader.ReadToFollowing("ActEnergy");
                    m.ActEnergy = reader.GetAttribute("unit");
                    m.ActEnergy = reader.ReadString() + m.ActEnergy;

                    // WaveLength
                    reader.ReadToFollowing("WaveLength");
                    m.WaveLength = reader.GetAttribute("unit");
                    m.WaveLength = reader.ReadString() + m.WaveLength;

                    data.DataList.Add(m);
                }

                reader.Close();
                stream.Dispose();

                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// リストをクリアする
        /// </summary>
        void ListClear()
        {
            MC500List.Clear();

            dSet.Tables["Files"].Clear();
        }

        /// <summary>
        /// リストの患者の氏名を取得する
        /// </summary>
        void ListName()
        {
            List<string> pt_list = new List<string>();

            DataTable table = dSet.Tables["Files"];

            foreach (DataRow r in table.Rows)
            {
                if (r["ID"].ToString().Length == 0)
                {
                    continue;
                }

                pt_list.Add(r["ID"].ToString());
            }

            if (pt_list.Count == 0)
            {
                return;
            }

            // 氏名をセットする
            List<PatBase> tmp_list = PatBase.GetList(pt_list);

            foreach (DataRow r in table.Rows)
            {
                foreach (PatBase tmp in tmp_list)
                {
                    if (r["ID"].ToString().Equals(tmp.Id))
                    {
                        r["氏名"] = tmp.Name;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// DB保存
        /// </summary>
        /// <returns></returns>
        bool Save()
        {
            bool result = false;

            // 保存した後、ファイルを移動させる
            foreach (DataGridViewRow r in ReadFileView.Rows)
            {
                if ((bool)(r.Cells["取込"].Value))
                {
                    foreach (MC500 m in MC500List)
                    {
                        if (r.Cells["ファイル"].Value.ToString().Equals(m.XMLFile))
                        {
                            m.Save(LoginUser.Id);

                            File.Move(XMLPath1 + m.XMLFile, XMLPath2 + m.XMLFile);
                            Thread.Sleep(100);

                            continue;
                        }
                    }
                }
            }

            return result;
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            ReadFiles();
        }

        /// <summary>
        /// データファイルを読み込む
        /// </summary>
        void ReadFiles()
        {
            ListClear();

            MC500List = ReadFile(XMLPath1);

            DataTable table = dSet.Tables["Files"];

            foreach (MC500 m in MC500List)
            {
                DataRow r = table.NewRow();

                r["ID"] = m.PtId;
                r["氏名"] = "";
                r["日付"] = DateTimeAgent.DateFormat(m.LaserDate, DateTimeAgent.DateFormatKind.LONG);
                r["左右"] = m.EyeJ;
                r["ファイル"] = m.XMLFile;
                r["取込"] = true;

                table.Rows.Add(r);
            }

            ListName();

            ReadFileView.DataSource = new DataView(table);
            FormatView();
        }

        void FormatView()
        {
            ReadFileView.Columns["ID"].Width = 60;
            ReadFileView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ReadFileView.Columns["ID"].ReadOnly = true;

            ReadFileView.Columns["氏名"].Width = 90;
            ReadFileView.Columns["氏名"].ReadOnly = true;

            ReadFileView.Columns["日付"].Width = 70;
            ReadFileView.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ReadFileView.Columns["日付"].ReadOnly = true;

            ReadFileView.Columns["左右"].Width = 30;
            ReadFileView.Columns["左右"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ReadFileView.Columns["左右"].ReadOnly = true;

            ReadFileView.Columns["ファイル"].Visible = false;

            ReadFileView.Columns["取込"].Width = 30;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ListClear();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("保存します。よろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Save();
                ReadFiles();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Dispose();
            }
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            MC500DataListForm f = new MC500DataListForm();
            f.Show();
        }

        private void ReadFileStripMenu1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("データファイルを削除しますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string f = ReadFileView.CurrentRow.Cells["ファイル"].Value.ToString();
                File.Move(XMLPath1 + f, XMLPath3 + f);

                ReadFiles();
            }
        }
    }
}
