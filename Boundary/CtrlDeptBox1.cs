using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlDeptBox1 : ComboBox
    {
        bool CodeShow = true;
        bool FullName = false;
        bool Dept0 = false;

        public CtrlDeptBox1()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init(bool code_show = true, bool full_name = false, bool dept0 = true)
        {
            this.CodeShow = code_show;
            this.FullName = full_name;
            this.Dept0 = dept0;

            this.Items.Add("");
/*
            if (this.Dept0)
            {
                this.Items.Add("0 (無選択)");
            }
            else
            {
                this.Items.Add("");
            }
*/
            foreach (string key in Dict.DeptDict.Keys)
            {
                // 0 は飛ばす
                if (key.Equals("0"))
                {
                    continue;
                }

                if (this.CodeShow)
                {
                    if (this.FullName)
                    {
                        this.Items.Add(key + " " + Dict.DeptDict[key].FullName);
                    }
                    else
                    {
                        this.Items.Add(key + " " + Dict.DeptDict[key].ShortName);
                    }
                }
                else
                {
                    if (this.FullName)
                    {
                        this.Items.Add(Dict.DeptDict[key].FullName);
                    }
                    else
                    {
                        this.Items.Add(Dict.DeptDict[key].ShortName);
                    }
                }
            }

//            this.SelectedIndex = 0;
        }

        public Dept GetDept()
        {
            Dept obj = new Dept();

            if (this.Text.Length > 0 && this.Text.Contains(" "))
            {
                obj = Dept.Load(this.Text.Split(' ')[0]);
            }

            return obj;
        }

        public void SetDept(string dept_code)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            Dept obj = Dept.Load(dept_code);

            if (this.CodeShow)
            {
                if (this.FullName)
                {
                    this.Text = obj.Code + " " + obj.FullName;
                }
                else
                {
                    this.Text = obj.Code + " " + obj.ShortName;
                }
            }
            else
            {
                if (this.FullName)
                {
                    this.Text = obj.FullName;
                }
                else
                {
                    this.Text = obj.ShortName;
                }
            }
        }

        public void SetDept(Dept dept)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            if (this.CodeShow)
            {
                if (this.FullName)
                {
                    this.Text = dept.Code + " " + dept.FullName;
                }
                else
                {
                    this.Text = dept.Code + " " + dept.ShortName;
                }
            }
            else
            {
                if (this.FullName)
                {
                    this.Text = dept.FullName;
                }
                else
                {
                    this.Text = dept.ShortName;
                }
            }
        }
    }
}
