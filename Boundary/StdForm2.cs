using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public partial class StdForm2 : Form
    {
        public StdForm2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// エラーメッセージがあれば表示する。なければ標準結果を表示する。
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public StdReturn Msg1(StdReturn sr)
        {
            if (sr.ErrExist)
            {
                MessageBox.Show(sr.Err, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (sr.MsgExist)
            {
                MessageBox.Show(sr.Msg);
            }

            return sr;
        }

        public virtual void FontSet(AppFont f)
        {
//            this.Fnt = f;
            this.Font = f.Ft;

            foreach (Control c in this.Controls)
            {
                c.Font = f.Ft;
                /*
                                if (c.GetType().Name.StartsWith("Label") ||
                                    c.GetType().Name.StartsWith("TextBox") ||
                                    c.GetType().Name.StartsWith("ComboBox") ||
                                    c.GetType().Name.StartsWith("CheckBox") ||
                                    c.GetType().Name.StartsWith("Button") ||
                                    c.GetType().Name.StartsWith("DataGridView"))
                                {
                                    c.Font = f.Ft;
                                }
                 */
            }
        }

        public virtual void PatSet(PatBase p)
        {
//            this.Pat = p;
        }
    }
}
