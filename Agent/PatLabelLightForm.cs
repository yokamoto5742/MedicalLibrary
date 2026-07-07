using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class PatLabelLightForm : Form
    {
        PatLabelSettings Settings = new PatLabelSettings();

        PatBase Pat = new PatBase();

        Font f10 = new Font("", 10);
        Font f9 = new Font("", 9);
        Font f8 = new Font("", 8);

        int print_total = 0;
        int print_counter = 0;
//        int order_counter = -1;

        string order_date = DateTime.Now.ToString("yyyyMMdd");
        List<PatOrder> order_list = new List<PatOrder>();

        /// <summary>
        /// オーダー情報をプリントするかどうか。デフォルトは true。
        /// </summary>
        bool print_order = true;

        public PatLabelLightForm()
        {
            InitializeComponent();
        }

        private void PatLabelLightForm_Load(object sender, EventArgs e)
        {
            try
            {
                // プログラムで一度も実行されていなければ実行する
                LibSettings.Init();

                this.Settings.Init();

                string[] args = Environment.GetCommandLineArgs();
                int p = 0;

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals("-p", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (i < args.Length - 1 && args[i + 1].Length <= 9 && int.TryParse(args[i + 1], out p))
                        {
                            this.Pat = PatBase.Load(args[i + 1]);
                        }
                    }

                    if (args[i].Equals("-d", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (i < args.Length - 1 && args[i + 1].Length == 8)
                        {
                            order_date = args[i + 1];
                        }
                    }

                    if (args[i].Equals("-n", StringComparison.CurrentCultureIgnoreCase))
                    {
                        print_order = false;
                    }
                }

                if (this.Pat.Id.Length > 0)
                {
                    LabelPrint();
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }

        /// <summary>
        /// ラベルを印刷する。
        /// </summary>
        void LabelPrint()
        {
            if (this.Pat.Id.Length == 0)
            {
                return;
            }

            if (print_order)
            {
                // 診療区分が存在することが必須
                if (this.Settings.PcShinkuList.Count > 0)
                {
                    string in_out = "";

                    if (this.Settings.PcInOutList.Count == 1)
                    {
                        in_out = this.Settings.PcInOutList[0];
                    }

                    List<string> empty_list = new List<string>();
#if INNO
                    order_list = PatOrder.GetListByPatSekouDates(this.Pat.Id, order_date, order_date, in_out, this.Settings.PcShinkuList, this.Settings.PcDeptList, empty_list, false, true);
#else
                    order_list = PatOrder.GetListByPatSekouDates(this.Pat.Id, order_date, order_date, in_out, this.Settings.PcShinkuList, this.Settings.PcDeptList, empty_list, false, false);
#endif

                    print_total = order_list.Count;
                }
            }

            print_counter = 0;
//            order_counter = -1;

            // XML内に該当PCのプリンタ情報があればそれを使用する。
            if (print_total > 0 && this.Settings.Printer.Length > 0)
            {
                printDocument1.PrinterSettings.PrinterName = this.Settings.Printer;
            }
            else if (print_total == 0 && this.Settings.PrinterSmall.Length > 0)
            {
                printDocument1.PrinterSettings.PrinterName = this.Settings.PrinterSmall;
            }
            else
            {
                printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                }
            }

            if (this.Settings.PrintView)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                try
                {
                    printDocument1.DocumentName = this.Pat.Id + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    printDocument1.Print();
                }
                catch (System.Drawing.Printing.InvalidPrinterException ex)
                {
                    MessageBox.Show(ex.GetBaseException().Message, "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
//            order_counter++;

            e.Graphics.DrawString(this.Pat.Id, f10, Brushes.Black, 10, 10);

            Barcode128 bar128 = new Barcode128();
            bar128.CodeType = Barcode.CODE128;
            bar128.CodeSet = Barcode128.Barcode128CodeSet.B;
            bar128.Code = this.Pat.Id;

            float bar_width = this.Pat.Id.Length * 35;

            if (bar_width < 100)
            {
                bar_width = 100;
            }
            else if (bar_width > 150)
            {
                bar_width = 150;
            }

            System.Drawing.Image img128 = bar128.CreateDrawingImage(Color.Black, Color.White);
            e.Graphics.DrawImage(img128, 85, 5, bar_width, 25);

            e.Graphics.DrawString(Pat.Name + "　様　　" + Pat.SexNameShort, f10, Brushes.Black, 10, 35);
            e.Graphics.DrawString(Pat.BirthStringJ + "生　" + Pat.AgeCalc(order_date) + "歳", f8, Brushes.Black, 50, 55);

            /*
            if (order_counter >= 0 && order_counter < print_total)
            {
                PatOrder order = order_list[order_counter];

                e.Graphics.DrawString(order.SekouDateStringShort + "　[" + order.InOutName + "]　" + order.DeptName + "　" + order.DoctorName, f8, Brushes.Black, new RectangleF(5, 80, 225, 33));
                e.Graphics.DrawString(order.SOAP, f8, Brushes.Black, new RectangleF(5, 115, 225, 115));
            }
             */
            if (print_counter >= 0 && print_counter < order_list.Count)
            {
                PatOrder order = order_list[print_counter];

                e.Graphics.DrawString(order.SekouDateStringShort + "　[" + order.InOutName + "]　" + order.DeptName + "　" + order.DoctorName, f8, Brushes.Black, new RectangleF(5, 80, 225, 33));
                e.Graphics.DrawString(order.SOAP, f8, Brushes.Black, new RectangleF(5, 115, 225, 115));
            }
            else
            {
                if (print_order)
                {
                    e.Graphics.DrawString("※該当オーダーはありません", f9, Brushes.Black, new RectangleF(25, 80, 205, 33));
                }
                else
                {
                    e.Graphics.DrawString("※オーダー印刷なし", f9, Brushes.Black, new RectangleF(25, 80, 205, 33));
                }
            }

            print_counter++;

            if (print_counter < print_total)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                print_counter = 0;
//                order_counter = -1;
            }
         }

         private void PatLabelLightForm_Shown(object sender, EventArgs e)
         {
             this.Dispose();
         }
     }
}