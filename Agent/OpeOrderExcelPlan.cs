using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class OpeOrderExcelPlan
    {
        /// <summary>
        /// Excel にデータを入れて開く。
        /// </summary>
        public static void ExcelOpen(string file_name, List<PatOpeOrder2> order_list)
        {
            if (!System.IO.File.Exists(file_name))
            {
                return;
            }

            Excel.Application exApp = new Excel.Application();
            Excel._Workbook exWorkbook;
            Excel._Worksheet exWorksheet11;
            Excel._Worksheet exWorksheet12;
            Excel._Worksheet exWorksheet21;
            Excel._Worksheet exWorksheet22;

            exApp.Visible = true;

            exWorkbook = (Excel._Workbook)(exApp.Workbooks.Open(file_name,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value));

            exWorksheet11 = (Excel._Worksheet)(exWorkbook.Sheets["中央"]);
            exWorksheet12 = (Excel._Worksheet)(exWorkbook.Sheets["中央2"]);
            exWorksheet21 = (Excel._Worksheet)(exWorkbook.Sheets["南館"]);
            exWorksheet22 = (Excel._Worksheet)(exWorkbook.Sheets["南館2"]);

            try
            {
                int x11 = 1;
                int y11 = 2;

                int x12 = 1;
                int y12 = 3;

                int x21 = 1;
                int y21 = 2;

                int x22 = 1;
                int y22 = 3;

                int date1 = 0;
                int date2 = 0;

                // 初日と最終日
                int d1 = 0;
                int d2 = 0;

                // 開始時間順に並べ替える
                order_list.Sort((x, y) =>
                {

                    int i = x.Date - y.Date;

                    if (i == 0)
                    {
                        i = x.StartTime - y.StartTime;
                    }

                    return i;
                });

                foreach (PatOpeOrder2 order in order_list)
                {
                    if (order.IsCanceled)
                    {
                        continue;
                    }

                    // 初日をチェックする
                    if (d1 == 0 || d1 > order.Date)
                    {
                        d1 = order.Date;
                    }

                    // 最終日をチェックする
                    if (d2 < order.Date)
                    {
                        d2 = order.Date;
                    }

                    if (order.OpePlace.Equals("中央"))
                    {
                        if (!date1.Equals(order.Date))
                        {
                            y11++;
                            date1 = order.Date;

                            exWorksheet11.Range["A" + y11, "A" + y11].Font.Bold = true;
                            exWorksheet11.Range["A" + y11, "A" + y11].Font.Size = 14;
                            exWorksheet11.Range["A" + y11, "A" + y11].WrapText = false;
                            exWorksheet11.Range["A" + y11, "U" + y11].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;
                            exWorksheet11.Cells[y11, 1] = DateTimeAgent.DateFormat(date1, DateTimeAgent.DateFormatKind.WLONG);
                            y11++;
                        }

                        exWorksheet11.Cells[y11, x11++] = order.OpeRoomName;
                        exWorksheet11.Cells[y11, x11++] = order.TimeFreeMark;
                        exWorksheet11.Cells[y11, x11++] = order.TimeOutStringShort;
                        exWorksheet11.Cells[y11, x11++] = order.StartTimeString;
                        exWorksheet11.Cells[y11, x11++] = order.EndTimeString;
                        exWorksheet11.Cells[y11, x11++] = order.Minutes > 0 ? order.Minutes.ToString() : "";
                        exWorksheet11.Cells[y11, x11++] = order.Pat.Id;
                        exWorksheet11.Cells[y11, x11++] = order.Pat.Name;
                        exWorksheet11.Cells[y11, x11++] = order.Pat.SexName;
                        exWorksheet11.Cells[y11, x11++] = order.Pat.AgeCalc(order.Date);

                        exWorksheet11.Cells[y11, x11++] = order.InOutString;
                        exWorksheet11.Cells[y11, x11++] = order.OpeName;
                        exWorksheet11.Cells[y11, x11++] = order.OpeAnes;
                        exWorksheet11.Cells[y11, x11++] = order.OpeDoctor;

                        exWorksheet11.Cells[y11, x11++] = order.DeptName;
                        exWorksheet11.Cells[y11, x11++] = order.InfectionFlg;
                        exWorksheet11.Cells[y11, x11++] = order.Doctor3;
                        exWorksheet11.Cells[y11, x11++] = order.Ns1;
                        exWorksheet11.Cells[y11, x11++] = order.Ns2;
                        exWorksheet11.Cells[y11, x11++] = order.Ns4;
                        exWorksheet11.Cells[y11, x11++] = order.Cont;

                        exWorksheet11.Range["A" + y11, "U" + y11].Borders.LineStyle = Excel.XlLineStyle.xlDot;

                        x11 = 1;
                        y11++;

                        exWorksheet12.Cells[y12, x12++] = DateTimeAgent.DateFormat(order.Date, DateTimeAgent.DateFormatKind.LONG);
                        exWorksheet12.Cells[y12, x12++] = order.OpeRoomName;
                        exWorksheet12.Cells[y12, x12++] = order.TimeFreeMark;
                        exWorksheet12.Cells[y12, x12++] = order.TimeOutStringShort;
                        exWorksheet12.Cells[y12, x12++] = order.StartTimeString;
                        exWorksheet12.Cells[y12, x12++] = order.EndTimeString;
                        exWorksheet12.Cells[y12, x12++] = order.Minutes > 0 ? order.Minutes.ToString() : "";
                        exWorksheet12.Cells[y12, x12++] = order.Pat.Id;
                        exWorksheet12.Cells[y12, x12++] = order.Pat.Name;
                        exWorksheet12.Cells[y12, x12++] = order.Pat.SexName;
                        exWorksheet12.Cells[y12, x12++] = order.Pat.AgeCalc(order.Date);

                        exWorksheet12.Cells[y12, x12++] = order.InOutString;
                        exWorksheet12.Cells[y12, x12++] = order.OpeName;
                        exWorksheet12.Cells[y12, x12++] = order.OpeAnes;
                        exWorksheet12.Cells[y12, x12++] = order.OpeDoctor;

                        exWorksheet12.Cells[y12, x12++] = order.DeptName;
                        exWorksheet12.Cells[y12, x12++] = order.InfectionFlg;
                        exWorksheet12.Cells[y12, x12++] = order.Doctor3;
                        exWorksheet12.Cells[y12, x12++] = order.Ns1;
                        exWorksheet12.Cells[y12, x12++] = order.Ns2;
                        exWorksheet12.Cells[y12, x12++] = order.Ns4;
                        exWorksheet12.Cells[y12, x12++] = order.Cont;

                        exWorksheet12.Range["A" + y12, "V" + y12].Borders.LineStyle = Excel.XlLineStyle.xlDot;

                        x12 = 1;
                        y12++;
                    }
                    else if (order.OpePlace.Equals("南館"))
                    {
                        if (!date2.Equals(order.Date))
                        {
                            y21++;
                            date2 = order.Date;

                            exWorksheet21.Range["A" + y21, "A" + y21].Font.Bold = true;
                            exWorksheet21.Range["A" + y21, "A" + y21].Font.Size = 14;
                            exWorksheet21.Range["A" + y21, "A" + y21].WrapText = false;
                            exWorksheet21.Range["A" + y21, "U" + y21].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;
                            exWorksheet21.Cells[y21, 1] = DateTimeAgent.DateFormat(date2, DateTimeAgent.DateFormatKind.WLONG);
                            y21++;
                        }

                        exWorksheet21.Cells[y21, x21++] = order.OpeRoomName;
                        exWorksheet21.Cells[y21, x21++] = order.TimeFreeMark;
                        exWorksheet21.Cells[y21, x21++] = order.TimeOutStringShort;
                        exWorksheet21.Cells[y21, x21++] = order.StartTimeString;
                        exWorksheet21.Cells[y21, x21++] = order.EndTimeString;
                        exWorksheet21.Cells[y21, x21++] = order.Minutes > 0 ? order.Minutes.ToString() : "";
                        exWorksheet21.Cells[y21, x21++] = order.Pat.Id;
                        exWorksheet21.Cells[y21, x21++] = order.Pat.Name;
                        exWorksheet21.Cells[y21, x21++] = order.Pat.SexName;
                        exWorksheet21.Cells[y21, x21++] = order.Pat.AgeCalc(order.Date);

                        exWorksheet21.Cells[y21, x21++] = order.InOutString;
                        exWorksheet21.Cells[y21, x21++] = order.OpeName;
                        exWorksheet21.Cells[y21, x21++] = order.OpeAnes;
                        exWorksheet21.Cells[y21, x21++] = order.OpeDoctor;

                        exWorksheet21.Cells[y21, x21++] = order.DeptName;
                        exWorksheet21.Cells[y21, x21++] = order.InfectionFlg;
                        exWorksheet21.Cells[y21, x21++] = order.Doctor3;
                        exWorksheet21.Cells[y21, x21++] = order.Ns1;
                        exWorksheet21.Cells[y21, x21++] = order.Ns2;
                        exWorksheet21.Cells[y21, x21++] = order.Ns4;
                        exWorksheet21.Cells[y21, x21++] = order.Cont;

                        exWorksheet21.Range["A" + y21, "U" + y21].Borders.LineStyle = Excel.XlLineStyle.xlDot;

                        x21 = 1;
                        y21++;

                        exWorksheet22.Cells[y22, x22++] = DateTimeAgent.DateFormat(date2, DateTimeAgent.DateFormatKind.LONG);
                        exWorksheet22.Cells[y22, x22++] = order.OpeRoomName;
                        exWorksheet22.Cells[y22, x22++] = order.TimeFreeMark;
                        exWorksheet22.Cells[y22, x22++] = order.TimeOutStringShort;
                        exWorksheet22.Cells[y22, x22++] = order.StartTimeString;
                        exWorksheet22.Cells[y22, x22++] = order.EndTimeString;
                        exWorksheet22.Cells[y22, x22++] = order.Minutes > 0 ? order.Minutes.ToString() : "";
                        exWorksheet22.Cells[y22, x22++] = order.Pat.Id;
                        exWorksheet22.Cells[y22, x22++] = order.Pat.Name;
                        exWorksheet22.Cells[y22, x22++] = order.Pat.SexName;
                        exWorksheet22.Cells[y22, x22++] = order.Pat.AgeCalc(order.Date);

                        exWorksheet22.Cells[y22, x22++] = order.InOutString;
                        exWorksheet22.Cells[y22, x22++] = order.OpeName;
                        exWorksheet22.Cells[y22, x22++] = order.OpeAnes;
                        exWorksheet22.Cells[y22, x22++] = order.OpeDoctor;

                        exWorksheet22.Cells[y22, x22++] = order.DeptName;
                        exWorksheet22.Cells[y22, x22++] = order.InfectionFlg;
                        exWorksheet22.Cells[y22, x22++] = order.Doctor3;
                        exWorksheet22.Cells[y22, x22++] = order.Ns1;
                        exWorksheet22.Cells[y22, x22++] = order.Ns2;
                        exWorksheet22.Cells[y22, x22++] = order.Ns4;
                        exWorksheet22.Cells[y22, x22++] = order.Cont;

                        exWorksheet22.Range["A" + y22, "V" + y22].Borders.LineStyle = Excel.XlLineStyle.xlDot;

                        x22 = 1;
                        y22++;
                    }
                }

                // タイトル
                DateTime dt1 = DateTimeAgent.DateTimeFromInt(d1);
                DateTime dt2 = DateTimeAgent.DateTimeFromInt(d2);

                exWorksheet11.Cells[1, 1] = dt1.ToString("yyyy年MM月dd日") + " ～ " + dt2.ToString("MM月dd日") + " 中央 手術一覧";
                exWorksheet12.Cells[1, 1] = dt1.ToString("yyyy年MM月dd日") + " ～ " + dt2.ToString("MM月dd日") + " 中央 手術一覧";
                exWorksheet21.Cells[1, 1] = dt1.ToString("yyyy年MM月dd日") + " ～ " + dt2.ToString("MM月dd日") + " 南館 手術一覧";
                exWorksheet22.Cells[1, 1] = dt1.ToString("yyyy年MM月dd日") + " ～ " + dt2.ToString("MM月dd日") + " 南館 手術一覧";

                // 罫線
                y11++;
                exWorksheet11.Range["A2", "U2"].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet11.Range["A2", "U2"].Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
                exWorksheet11.Range["A2", "A" + y11].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet11.Range["A2", "A" + y11].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
                exWorksheet11.Range["A" + y11, "U" + y11].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet11.Range["A" + y11, "U" + y11].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;
                exWorksheet11.Range["U2", "U" + y11].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet11.Range["U2", "U" + y11].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;

                y21++;
                exWorksheet21.Range["A2", "U2"].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet21.Range["A2", "U2"].Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
                exWorksheet21.Range["A2", "A" + y21].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet21.Range["A2", "A" + y21].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
                exWorksheet21.Range["A" + y21, "U" + y21].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet21.Range["A" + y21, "U" + y21].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;
                exWorksheet21.Range["U2", "U" + y21].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                exWorksheet21.Range["U2", "U" + y21].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;

                string exFileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file_name.Split('\\')[file_name.Split('\\').Length - 1];

                exWorkbook.SaveAs(exFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
            finally
            {
                Marshal.ReleaseComObject(exWorksheet11);
                Marshal.ReleaseComObject(exWorksheet21);
                Marshal.ReleaseComObject(exWorkbook);
                Marshal.ReleaseComObject(exApp);

                GC.Collect();
            }
        }
    }
}
