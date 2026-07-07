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
    public class CtrlDoctorBox1 : ComboBox
    {
        bool CodeShow = true;
        bool Doctor0 = false;

        public CtrlDoctorBox1()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init(bool code_show = true, bool doctor0 = true)
        {
            this.CodeShow = code_show;
            this.Doctor0 = doctor0;

            this.Items.Add("");

            /*
            if (this.Doctor0)
            {
                this.Items.Add("0 (無選択)");
            }
            else
            {
                this.Items.Add("");
            }
             */

            foreach (string key in Dict.DoctorDict.Keys)
            {
                // 0 は飛ばす
                if (key.Equals("0"))
                {
                    continue;
                }

                if (this.CodeShow)
                {
                    this.Items.Add(key + " " + Dict.DoctorDict[key].Name);
                }
                else
                {
                    this.Items.Add(Dict.DoctorDict[key].Name);
                }
            }

//            this.SelectedIndex = 0;
        }

        public Doctor GetDoctor()
        {
            Doctor obj = new Doctor();

            if (this.Text.Length > 0 && this.Text.Contains(" "))
            {
                obj = Doctor.Load(this.Text.Split(' ')[0]);
            }

            return obj;
        }

        public void SetDoctor(string doctor_code)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            Doctor obj = Doctor.Load(doctor_code);

            if (this.CodeShow)
            {
                this.Text = obj.Code + " " + obj.Name;
            }
            else
            {
                this.Text = obj.Name;
            }
        }

        public void SetDoctor(Doctor doctor)
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }

            if (this.CodeShow)
            {
                this.Text = doctor.Code + " " + doctor.Name;
            }
            else
            {
                this.Text = doctor.Name;
            }
        }
    }
}
