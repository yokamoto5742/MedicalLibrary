using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MedicalLibrary.Utility;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Boundary
{
    public partial class FormLibSettings : Form
    {
        public FormLibSettings()
        {
            InitializeComponent();

            this.OrderXmlExeBox1.Text = LibSettings.Current.OrderXmlExe;
            this.ReceExeBox1.Text = LibSettings.Current.ReceExe;
            this.ReceApiExeBox1.Text = LibSettings.Current.ReceApiExe;

            this.OrderReceApiIntervalBox1.Items.Add("2");
            this.OrderReceApiIntervalBox1.Items.Add("3");
            this.OrderReceApiIntervalBox1.Items.Add("4");
            this.OrderReceApiIntervalBox1.Items.Add("5");
            this.OrderReceApiIntervalBox1.Items.Add("7");
            this.OrderReceApiIntervalBox1.Items.Add("10");
            this.OrderReceApiIntervalBox1.Text = LibSettings.Current.OrderReceApiInterval;
        }

        private void ReceExeButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";

            if (File.Exists(this.ReceExeBox1.Text))
            {
                ofd.FileName = this.ReceExeBox1.Text.Substring(this.ReceExeBox1.Text.LastIndexOf('\\') + 1);
            }

            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //            ofd.InitialDirectory = @"C:\";

            if (Directory.Exists(this.ReceExeBox1.Text.Substring(0, this.ReceExeBox1.Text.LastIndexOf('\\'))))
            {
                ofd.InitialDirectory = this.ReceExeBox1.Text.Substring(0, this.ReceExeBox1.Text.LastIndexOf('\\'));
            }

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "EXEファイル(*.exe)|*.exe|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.ReceExeBox1.Text = ofd.FileName;
            }
        }

        private void OrderXmlExeButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";

            if (File.Exists(this.OrderXmlExeBox1.Text))
            {
                ofd.FileName = this.OrderXmlExeBox1.Text.Substring(this.OrderXmlExeBox1.Text.LastIndexOf('\\') + 1);
            }

            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //            ofd.InitialDirectory = @"C:\";

            if (Directory.Exists(this.OrderXmlExeBox1.Text.Substring(0, this.OrderXmlExeBox1.Text.LastIndexOf('\\'))))
            {
                ofd.InitialDirectory = this.OrderXmlExeBox1.Text.Substring(0, this.OrderXmlExeBox1.Text.LastIndexOf('\\'));
            }

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "EXEファイル(*.exe)|*.exe|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.OrderXmlExeBox1.Text = ofd.FileName;
            }
        }

        private void ReceApiExeButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";

            if (File.Exists(this.OrderXmlExeBox1.Text))
            {
                ofd.FileName = this.ReceApiExeBox1.Text.Substring(this.ReceApiExeBox1.Text.LastIndexOf('\\') + 1);
            }

            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //            ofd.InitialDirectory = @"C:\";

            if (Directory.Exists(this.ReceApiExeBox1.Text.Substring(0, this.ReceApiExeBox1.Text.LastIndexOf('\\'))))
            {
                ofd.InitialDirectory = this.ReceApiExeBox1.Text.Substring(0, this.ReceApiExeBox1.Text.LastIndexOf('\\'));
            }

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "EXEファイル(*.exe)|*.exe|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.ReceApiExeBox1.Text = ofd.FileName;
            }
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            List<string> msgs = new List<string>();

            int i = 0;

            if (!int.TryParse(this.OrderReceApiIntervalBox1.Text, out i))
            {
                msgs.Add("オーダー転送してから医事会計APIを起動するまでの時間（秒）が入力されていません");
            }
            else if (i > 10 || i < 1)
            {
                msgs.Add("オーダー転送してから医事会計APIを起動するまでの時間（秒）は 1～10 で入力してください");
            }

            if (msgs.Count > 0)
            {
                MessageBox.Show(AppString.ConcatList(msgs, Environment.NewLine));
                return;
            }

            LibSettings.Current.OrderXmlExe = this.OrderXmlExeBox1.Text;
            LibSettings.Current.ReceExe = this.ReceExeBox1.Text;
            LibSettings.Current.ReceApiExe = this.ReceApiExeBox1.Text;
            LibSettings.Current.OrderReceApiInterval = this.OrderReceApiIntervalBox1.Text;
        }

        private void CancelButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
