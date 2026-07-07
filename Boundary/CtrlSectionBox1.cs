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
    public class CtrlSectionBox1 : ComboBox
    {
        bool CodeShow = true;
        bool FullName = false;
        bool Section0 = false;
        bool Section9 = false;

        public CtrlSectionBox1()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init(bool code_show = true, bool full_name = false, bool section0 = true, bool section9 = false)
        {
            this.CodeShow = code_show;
            this.FullName = full_name;
            this.Section0 = section0;
            this.Section9 = section9;

            this.Items.Add("");

            /*
            if (this.Section0)
            {
                this.Items.Add("0 すべての所属");
            }
            else
            {
                this.Items.Add("");
            }
             */

            foreach (string key in Dict.SectionDict.Keys)
            {
                // 0 は飛ばす
                if (key.Equals("0"))
                {
                    continue;
                }

                if (!this.Section9)
                {
                    // 非表示のものは飛ばす
                    if (Dict.SectionDict[key].Kind1 == 9)
                    {
                        continue;
                    }
                }

                if (this.CodeShow)
                {
                    if (this.FullName)
                    {
                        this.Items.Add(key + " " + Dict.SectionDict[key].FullName);
                    }
                    else
                    {
                        this.Items.Add(key + " " + Dict.SectionDict[key].ShortName);
                    }
                }
                else
                {
                    if (this.FullName)
                    {
                        this.Items.Add(Dict.SectionDict[key].FullName);
                    }
                    else
                    {
                        this.Items.Add(Dict.SectionDict[key].ShortName);
                    }
                }
            }

            this.SelectedIndex = 0;
        }

        public Section GetSection()
        {
            Section obj = new Section();

            if (this.Text.Length > 0 && this.Text.Contains(" "))
            {
                obj = Section.Load(this.Text.Split(' ')[0]);
            }

            return obj;
        }

        public void SetSection(string section_code)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            Section obj = Section.Load(section_code);

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

        public void SetSection(Section section)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            if (this.CodeShow)
            {
                if (this.FullName)
                {
                    this.Text = section.Code + " " + section.FullName;
                }
                else
                {
                    this.Text = section.Code + " " + section.ShortName;
                }
            }
            else
            {
                if (this.FullName)
                {
                    this.Text = section.FullName;
                }
                else
                {
                    this.Text = section.ShortName;
                }
            }
        }
    }
}
