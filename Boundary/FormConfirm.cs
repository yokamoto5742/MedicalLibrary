using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MedicalLibrary.Boundary
{
    public partial class FormConfirm : Form
    {
        public enum FormButton : int
        {
            Button1 = 1,
            Button2 = 2,
            Button3 = 3
        }

        public FormConfirm()
        {
            InitializeComponent();
        }

        public static DialogResult Show(string Msg, string Title, string Button1, string Button2, string Button3, FormButton DefaultButton)
        {
            FormConfirm fc = new FormConfirm();

            fc.msgLabel.Text = Msg;
            fc.button1.Text = Button1;
            fc.button2.Text = Button2;
            fc.button3.Text = Button3;

            if (DefaultButton == FormButton.Button1)
            {
                fc.button1.Select();
            }
            else if (DefaultButton == FormButton.Button2)
            {
                fc.button2.Select();
            }
            else if (DefaultButton == FormButton.Button3)
            {
                fc.button3.Select();
            }

            return fc.ShowDialog();
        }

        public static DialogResult Show(string Msg, string Title, string Button1, string Button2, FormButton DefaultButton)
        {
            FormConfirm fc = new FormConfirm();

            fc.msgLabel.Text = Msg;
            fc.button1.Text = Button1;
            fc.button1.Location = new Point(60, 68);
            fc.button2.Text = Button2;
            fc.button2.Location = new Point(180, 68);
            fc.button3.Visible = false;

            if (DefaultButton == FormButton.Button1)
            {
                fc.button1.Select();
            }
            else if (DefaultButton == FormButton.Button2)
            {
                fc.button2.Select();
            }

            return fc.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}