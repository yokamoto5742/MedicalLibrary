using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;

namespace MedicalLibrary.Boundary
{
    public partial class LoginChange : Form
    {
        string login = "";

        public LoginChange()
        {
            InitializeComponent();

            this.UserLabel.Text = "現在のユーザーは " + LoginUser.Id + " " + LoginUser.Name + " です";
        }

        private void LoginChange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!LoginCheck())
                {
                    MessageBox.Show("入力データが正しくありません");
                }
                else
                {
                    LoginUser.SetUser(login.Substring(2).TrimStart('0'));
                }

                this.Dispose();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void LoginChange_KeyPress(object sender, KeyPressEventArgs e)
        {
            login += e.KeyChar.ToString();
        }

        /// <summary>
        /// 入力文字列が正しいかチェックする
        /// </summary>
        /// <returns></returns>
        private bool LoginCheck()
        {
            if (!login.StartsWith("MA"))
            {
                return false;
            }

            if (login.Length != 9)
            {
                return false;
            }

            return true;
        }
    }
}