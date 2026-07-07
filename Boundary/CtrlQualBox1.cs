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
    public class CtrlQualBox1 : ComboBox
    {
        bool CodeShow = true;
        bool FullName = false;
        bool Qual0 = false;
        bool Qual9 = false;

        public CtrlQualBox1()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init(bool code_show = true, bool full_name = false, bool qual0 = true, bool qual9 = false)
        {
            this.CodeShow = code_show;
            this.FullName = full_name;
            this.Qual0 = qual0;
            this.Qual9 = qual9;

            this.Items.Add("");

            /*
            if (this.Qual0)
            {
                this.Items.Add("0 すべての資格");
            }
            else
            {
                this.Items.Add("");
            }
             */

            foreach (string key in Dict.QualDict.Keys)
            {
                // 0 は飛ばす
                if (key.Equals("0"))
                {
                    continue;
                }

                if (!this.Qual9)
                {
                    // 非表示のものは飛ばす
                    if (Dict.QualDict[key].Kind1 == 9)
                    {
                        continue;
                    }
                }

                if (this.CodeShow)
                {
                    if (this.FullName)
                    {
                        this.Items.Add(key + " " + Dict.QualDict[key].FullName);
                    }
                    else
                    {
                        this.Items.Add(key + " " + Dict.QualDict[key].ShortName);
                    }
                }
                else
                {
                    if (this.FullName)
                    {
                        this.Items.Add(Dict.QualDict[key].FullName);
                    }
                    else
                    {
                        this.Items.Add(Dict.QualDict[key].ShortName);
                    }
                }
            }

            this.SelectedIndex = 0;
        }

        public Qual GetQual()
        {
            Qual obj = new Qual();

            if (this.Text.Length > 0 && this.Text.Contains(" "))
            {
                obj = Qual.Load(this.Text.Split(' ')[0]);
            }

            return obj;
        }

        public void SetQual(string qual_code)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            Qual obj = Qual.Load(qual_code);

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

        public void SetQual(Qual qual)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            if (this.CodeShow)
            {
                if (this.FullName)
                {
                    this.Text = qual.Code + " " + qual.FullName;
                }
                else
                {
                    this.Text = qual.Code + " " + qual.ShortName;
                }
            }
            else
            {
                if (this.FullName)
                {
                    this.Text = qual.FullName;
                }
                else
                {
                    this.Text = qual.ShortName;
                }
            }
        }
    }
}
