using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class LoginPrompt : Form
    {
        enum Mode : int
        {
            Single = 1,
            Double = 2
        }

        Mode _Mode = Mode.Single;

        public LoginPrompt()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (idBox1.Text.Length == 0)
            {
                MessageBox.Show("IDが入力されていません");
                return;
            }
            else if (this._Mode == Mode.Double && idBox2.Text.Length == 0)
            {
                MessageBox.Show("代行者のIDが入力されていません");
                return;
            }

            this.Hide();

            string id1 = "";
            string id2 = "";

            bool suc1 = false;
            bool suc2 = true;

            Staff st1 = Staff.Verify(idBox1.Text, pwdBox1.Text);

            if (st1.Name.Length > 0)
            {
                id1 = st1.Code.ToString();
                suc1 = true;
            }

            if (this._Mode == Mode.Double)
            {
                suc2 = false;
                Staff st2 = Staff.Verify(idBox2.Text, pwdBox2.Text);

                if (st2.Name.Length > 0)
                {
                    id2 = st2.Code.ToString();
                    suc2 = true;
                }
            }

            if (suc1 && suc2)
            {
                // ログインに成功したら終了
                LoginUser.SetUser(id1, id2);
                this.Dispose();
            }
            else
            {
                // ログインに失敗した場合
                this.Show();
                this.errLabel.Text = "ログインに失敗しました";

                if (!suc1)
                {
                    this.pwdBox1.Clear();
                    this.pwdBox1.Focus();
                }
                else if (!suc2)
                {
                    this.pwdBox2.Clear();
                    this.pwdBox2.Focus();
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void idBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pwdBox1.Focus();
            }
        }

        private void pwdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton.Focus();
            }
        }

        void ModeChange(Mode mode)
        {
            this._Mode = mode;

            if (mode == Mode.Single)
            {
                SecondLabel1.Visible = false;
                idBox2.Visible = false;
                pwdBox2.Visible = false;
                this.Width = 250;
            }
            else
            {
                SecondLabel1.Visible = true;
                idBox2.Visible = true;
                pwdBox2.Visible = true;
                this.Width = 350;
            }
        }

        private void IDLabel1_DoubleClick(object sender, EventArgs e)
        {
            if (this._Mode == Mode.Single)
            {
                this.ModeChange(Mode.Double);
            }
            else
            {
                this.ModeChange(Mode.Single);
            }
        }
    }
}