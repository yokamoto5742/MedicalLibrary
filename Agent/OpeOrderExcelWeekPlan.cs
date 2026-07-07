using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class OpeOrderExcelWeekPlan
    {
        /// <summary>
        /// Excel に週間手術予定データを入れて開く。
        /// </summary>
        public static void ExcelOpen(string file_name, string crit_date_time, DataGridView view)
        {
            if (!System.IO.File.Exists(file_name))
            {
                return;
            }

            Excel.Application exApp = new Excel.Application();
            Excel._Workbook exWorkbook;
            Excel._Worksheet exWorksheet1;

            exApp.Visible = true;

            exWorkbook = (Excel._Workbook)(exApp.Workbooks.Open(file_name,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value));

            exWorksheet1 = (Excel._Worksheet)(exWorkbook.Sheets["週間手術予定表"]);

            try
            {
                int x = 1;
                int y = 1;

                foreach (DataGridViewColumn c in view.Columns)
                {
                    exWorksheet1.Cells[y, x++] = c.HeaderText;
                }

                y = 2;

                foreach (DataGridViewRow r in view.Rows)
                {
                    x = 1;

                    exWorksheet1.Cells[y, x++] = r.Cells["館"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["室"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["0"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["1"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["2"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["3"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["4"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["5"].Value.ToString();
                    exWorksheet1.Cells[y, x++] = r.Cells["6"].Value.ToString();

                    y++;
                }

                exWorksheet1.get_Range("A1", "I" + (y - 1)).Borders.LineStyle = Excel.XlLineStyle.xlDot;

                // 原本エクセル上書きを避けるためコードを修正 2011/04/07
                string exFileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + crit_date_time + "_" + file_name.Split('\\')[file_name.Split('\\').Length - 1];

                exWorkbook.SaveAs(exFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
            finally
            {
                Marshal.ReleaseComObject(exWorksheet1);
                Marshal.ReleaseComObject(exWorkbook);
                Marshal.ReleaseComObject(exApp);

                GC.Collect();
            }
        }
    }
}
