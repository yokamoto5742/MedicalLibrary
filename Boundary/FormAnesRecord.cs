using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class FormAnesRecord : StdForm1
    {
        DataSet DSet = new DataSet();

        string IMG_PATH = LibSettings.Current.AnesImageFolder;

        public FormAnesRecord()
        {
            InitializeComponent();
        }

        private void FormAnesRecord_Load(object sender, EventArgs e)
        {
            DataTable table = DSet.Tables.Add("PtList");
            table.Columns.Add("日付");
            table.Columns.Add("ID");
            table.Columns.Add("氏名");

            table = DSet.Tables.Add("AnesList");
            table.Columns.Add("日付");
            table.Columns.Add("連番");
            table.Columns.Add("ファイル");
            table.Columns.Add("パス");
            table.Columns.Add("氏名");
        }

        private void FormAnesRecord_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public override void PatSet(PatBase p)
        {
            base.PatSet(p);

            this.stdControlPat11.PatSet(p);

            this.AnesListShow();
        }

        private void PtListShowButton_Click(object sender, EventArgs e)
        {
            PtListShow();
        }

        void AnesClear()
        {
            if (!DSet.Tables.Contains("AnesList")) return;

            DataTable table = DSet.Tables["AnesList"];
            table.Clear();

            DataView view = new DataView(table);
            AnesListView.DataSource = view;

            AnesImgBox.Image = null;
        }

        /// <summary>
        /// 患者リストを検索して表示する。
        /// </summary>
        void PtListShow()
        {
            // 待機画像を表示する
            this.WaitBox1.Visible = true;
            this.WaitLabel1.Visible = true;

            string date = PtListDate.Value.ToString("yyMMdd");

            DataTable table = DSet.Tables["PtList"];
            table.Clear();

            List<AnesData> list = FindList(date, FindKind.DATE);
            bool add_flg = true;

            foreach (AnesData f in list)
            {
                add_flg = true;

                foreach (DataRow r in table.Rows)
                {
                    if (r["日付"].ToString().Equals(f.Date) && r["ID"].ToString().Equals(f.Id) && r["氏名"].ToString().Equals(f.Name))
                    {
                        add_flg = false;
                        break;
                    }
                }

                if (add_flg)
                {
                    DataRow r = table.NewRow();

                    r["日付"] = f.Date;
                    r["ID"] = f.Id;
                    r["氏名"] = f.Name;

                    table.Rows.Add(r);
                }
            }

            PtListFormat();

            // 待機画像を消す
            this.WaitBox1.Visible = false;
            this.WaitLabel1.Visible = false;
        }

        void PtListFormat()
        {
            DataView view = new DataView(DSet.Tables["PtList"]);
            view.Sort = "ID";

            PtListView.DataSource = view;

            PtListView.Columns["日付"].Width = 75;
            PtListView.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PtListView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PtListView.Columns["ID"].Width = 50;
            PtListView.Columns["氏名"].Width = 70;
        }

        public void AnesListShow()
        {
            if (!DSet.Tables.Contains("AnesList")) return;

            if (this.Pat.Id.Length == 0) return;

            // 待機画像を表示する
            this.WaitBox1.Visible = true;
            this.WaitLabel1.Visible = true;

            DataTable table = DSet.Tables["AnesList"];
            table.Clear();

            List<AnesData> list = FindList(this.Pat.Id, FindKind.ID);

            foreach (AnesData f in list)
            {
                DataRow r = table.NewRow();

                r["日付"] = f.Date;
                //                r["連番"] = f.File.Split('.')[0].Substring(f.File.Split('.')[0].Length - 1);
                r["ファイル"] = f.FileName;
                r["パス"] = f.File;
                r["氏名"] = f.Name;

                table.Rows.Add(r);
            }

            AnesListFormat();

            // 待機画像を消す
            this.WaitBox1.Visible = false;
            this.WaitLabel1.Visible = false;
        }

        void AnesListFormat()
        {
            DataView view = new DataView(DSet.Tables["AnesList"]);
            view.Sort = "日付 desc, 連番";

            AnesListView.DataSource = view;

            AnesListView.Columns["日付"].Width = 80;
            AnesListView.Columns["日付"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AnesListView.Columns["連番"].Width = 30;
            AnesListView.Columns["連番"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AnesListView.Columns["連番"].Visible = false;
            AnesListView.Columns["ファイル"].Width = 120;
            AnesListView.Columns["ファイル"].Visible = false;
            AnesListView.Columns["パス"].Visible = false;
            AnesListView.Columns["氏名"].Width = 90;
        }

        void AnesShow(AnesData data)
        {
            string file = data.File;

            if (File.Exists(file))
            {
                AnesImgBox.Image = Image.FromFile(file);
                AnesImgBox.Tag = file;
            }
        }

        private void PtListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AnesClear();
                DataGridViewRow r = PtListView.Rows[e.RowIndex];
                this.PatSet(PatBase.Load(r.Cells["ID"].Value.ToString()));
                AnesListShow();
            }
        }

        private void AnesListView_SelectionChanged(object sender, EventArgs e)
        {
            if (AnesListView.SelectedRows.Count > 0)
            {
                DataGridViewRow r = AnesListView.SelectedRows[0];

                AnesData data = new AnesData();
                data.Id = this.Pat.Id;
                data.Date = "20" + r.Cells["日付"].Value.ToString();
                data.File = r.Cells["パス"].Value.ToString();

                AnesShow(data);
            }
        }

        /// <summary>
        /// フォルダの中から条件に合致するファイルを検索
        /// </summary>
        /// <param name="key">日付 yyMMdd または患者ID</param>
        /// <param name="kind"></param>
        /// <returns></returns>
        List<AnesData> FindList(string key, FindKind kind)
        {
            List<AnesData> list = new List<AnesData>();
            int id = 0;

            if (kind == FindKind.DATE && key.Length == 6)
            {
                // 日付で検索
                string year = key.Substring(0, 2);
                string date = "20" + key.Insert(2, "/").Insert(5, "/");

                // 年月日が yyMMdd のもの
                // まず直下を検索
                string[] files = Directory.GetFiles(IMG_PATH, key + "*.jpg", SearchOption.TopDirectoryOnly);
                string f = "";

                foreach (string s in files)
                {
                    if (s.Split('\\').Length > 0)
                    {
                        f = s.Split('\\')[s.Split('\\').Length - 1].Split('.')[0].Replace("＿", "_");

                        if (f.Substring(6).Split('_').Length > 1 && int.TryParse(f.Split('_')[1], out id))
                        {
                            AnesData data = new AnesData();
                            data.Date = date;
                            data.Id = id.ToString();
                            data.Name = f.Substring(6).Split('_')[0];
                            data.File = s;

                            list.Add(data);
                        }
                    }
                }

                // 年月日が yyMMdd のもの
                // 20yy フォルダを検索
                if (Directory.Exists(IMG_PATH + "\\20" + year))
                {
                    files = Directory.GetFiles(IMG_PATH + "\\20" + year, key + "*.jpg", SearchOption.TopDirectoryOnly);
                    f = "";

                    foreach (string s in files)
                    {
                        if (s.Split('\\').Length > 0)
                        {
                            f = s.Split('\\')[s.Split('\\').Length - 1].Split('.')[0].Replace("＿", "_");

                            if (f.Substring(6).Split('_').Length > 1 && int.TryParse(f.Split('_')[1], out id))
                            {
                                AnesData data = new AnesData();
                                data.Date = date;
                                data.Id = id.ToString();
                                data.Name = f.Substring(6).Split('_')[0];
                                data.File = s;

                                list.Add(data);
                            }
                        }
                    }
                }

                // 年月日が yyyyMMdd のもの
                // まず直下を検索
                files = Directory.GetFiles(IMG_PATH, "*_20" + key + "_*.jpg", SearchOption.TopDirectoryOnly);
                f = "";

                foreach (string s in files)
                {
                    if (s.Split('\\').Length > 0)
                    {
                        f = s.Split('\\')[s.Split('\\').Length - 1].Split('.')[0].Replace("＿", "_");

                        if (f.Split('_').Length > 3 && int.TryParse(f.Split('_')[2], out id))
                        {
                            AnesData data = new AnesData();
                            data.Date = date;
                            data.Id = id.ToString();
                            data.Name = f.Split('_')[3];
                            data.File = s;

                            list.Add(data);
                        }
                    }
                }

                // 年月日が yyyyMMdd のもの
                // 20yy フォルダを検索
                if (Directory.Exists(IMG_PATH + "\\20" + year))
                {
                    files = Directory.GetFiles(IMG_PATH + "\\20" + year, "*_20" + key + "_*.jpg", SearchOption.TopDirectoryOnly);
                    f = "";

                    foreach (string s in files)
                    {
                        if (s.Split('\\').Length > 0)
                        {
                            f = s.Split('\\')[s.Split('\\').Length - 1].Split('.')[0].Replace("＿", "_");

                            if (f.Split('_').Length > 3 && int.TryParse(f.Split('_')[2], out id))
                            {
                                AnesData data = new AnesData();
                                data.Date = date;
                                data.Id = id.ToString();
                                data.Name = f.Split('_')[3];
                                data.File = s;

                                list.Add(data);
                            }
                        }
                    }
                }
            }
            else if (kind == FindKind.ID)
            {
                // 患者IDで検索
                DateTime dt = new DateTime();
                string[] files = Directory.GetFiles(IMG_PATH, "*_" + key + "_*.jpg", SearchOption.AllDirectories);
                string f = "";

                foreach (string s in files)
                {
                    if (s.Split('\\').Length > 0)
                    {
                        f = s.Split('\\')[s.Split('\\').Length - 1].Split('.')[0].Replace("＿", "_");

                        if (f.Length > 6)
                        {
                            if (DateTime.TryParse(f.Substring(0, 6).Insert(2, "/").Insert(5, "/"), out dt))
                            {
                                // 先頭6文字が日付の場合
                                AnesData data = new AnesData();

                                data.Id = key;
                                data.Date = dt.ToString("yyyy/MM/dd");
                                data.Name = f.Substring(6).Split('_')[0];
                                data.File = s;

                                list.Add(data);
                            }
                            else if (f.Split('_').Length > 3)
                            {
                                string[] ss = f.Split('_');

                                if (ss.Length > 3 && ss[1].Length == 8 && DateTime.TryParse(ss[1].Insert(4, "/").Insert(7, "/"), out dt))
                                {
                                    // _ で区切って３番目が日付の場合
                                    AnesData data = new AnesData();

                                    data.Id = key;
                                    data.Date = dt.ToString("yyyy/MM/dd");
                                    data.Name = ss[3];
                                    data.File = s;

                                    list.Add(data);
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }

        enum FindKind : int
        {
            DATE = 1,
            ID = 2
        }

        private void AnesImgBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            Launcher.ImageViewer(box.Tag.ToString());
        }
    }

    class AnesData
    {
        public string Date = "";
        public string Id = "";
        public string Name = "";
        public string File = "";

        public string FileName
        {
            get
            {
                string s = "";

                if (this.File.Split('\\').Length > 0)
                {
                    s = this.File.Split('\\')[this.File.Split('\\').Length - 1].Split('.')[0].Replace("＿", "_");
                }

                return s;
            }
        }
    }
}