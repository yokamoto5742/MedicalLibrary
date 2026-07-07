using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormDPCIdChange : Form
    {
        public FormDPCIdChange()
        {
            InitializeComponent();

            this.CodeBox1.Text = "161910769";
            this.CharBox1.Text = "9";
        }

        private void FileOpenButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();

            f.Description = "フォルダを選択してください";

            f.RootFolder = Environment.SpecialFolder.Desktop;
            f.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            f.ShowNewFolderButton = false;

            if (f.ShowDialog() == DialogResult.OK)
            {
                this.FileOpenBox1.Text = f.SelectedPath;
            }
        }

        private void FileSaveButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();

            f.Description = "フォルダを選択してください";

            f.RootFolder = Environment.SpecialFolder.Desktop;
            f.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            f.ShowNewFolderButton = true;

            if (f.ShowDialog() == DialogResult.OK)
            {
                this.FileSaveBox1.Text = f.SelectedPath;
            }
        }

        private void ExecButton1_Click(object sender, EventArgs e)
        {
            if (this.FileOpenBox1.Text.Equals(this.FileSaveBox1.Text))
            {
                MessageBox.Show("元フォルダと新フォルダは別の場所にしてください");
                return;
            }

            if (this.CodeBox1.Text.Length != 9)
            {
                MessageBox.Show("病院コード（9桁）を指定してください");
                return;
            }

            if (this.CharBox1.Text.Length != 1)
            {
                MessageBox.Show("識別番号先頭文字（1文字）を指定してください");
                return;
            }

            string[] files = Directory.GetFiles(this.FileOpenBox1.Text, "*_" + this.CodeBox1.Text + "_*.txt");

            foreach (string f1 in files)
            {
                this.Convert(f1, this.FileSaveBox1.Text + "\\" + f1.Substring(f1.LastIndexOf('\\') + 1));
            }
        }

        /// <summary>
        /// ファイルの中身を変換する
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        void Convert(string file1, string file2)
        {
            int counter = 0;
            string line;

            StreamReader fr = new StreamReader(file1, Encoding.GetEncoding("shift_jis"));
            StreamWriter fw = new StreamWriter(file2, false, Encoding.GetEncoding("shift_jis"));

            // ファイル名
            string fn = file1.Substring(file1.LastIndexOf('\\') + 1);

            while ((line = fr.ReadLine()) != null)
            {
                if (fn.StartsWith("Hn_"))
                {
                    // Hn_ で始まるファイルの場合は 病院コード（9桁）+ Tab + 病棟コード + Tab + 患者コード ... となる
                    // 2016/09/30 by sakane
                    fw.WriteLine(line.Substring(0, 12) + this.CharBox1.Text + line.Substring(13));
                }
                else
                {
                    // それ以外のファイルの場合は 病院コード（9桁）+ Tab + 患者コード ... となる
                    fw.WriteLine(line.Substring(0, 10) + this.CharBox1.Text + line.Substring(11));
                }

                counter++;
            }

            fr.Close();
            fw.Close();

            this.Log(fn + " " + counter + "行を変換しました");
        }


        void Log(string s)
        {
            this.LogBox1.Text += DateTime.Now.ToString("HH:mm:ss") + " " + s + Environment.NewLine;
        }
    }
}
