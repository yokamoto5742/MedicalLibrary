using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace MedicalLibrary.Utility
{
    /// <summary>
    /// ExcelやCSVへの出力データ
    /// </summary>
    public class TableData
    {
        /// <summary>
        /// カラム名のリスト
        /// </summary>
        public List<string> Title = new List<string>();

        /// <summary>
        /// カラム名のリスト2
        /// </summary>
        public List<string> Title2 = new List<string>();

        /// <summary>
        /// 出力レコードのリスト
        /// </summary>
        public List<TableDataRecord> RecordList = new List<TableDataRecord>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TableData()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">ExcelやCSV出力したい DataGridView </param>
        /// <param name="hide_column_print">非表示カラムを印字するかどうか</param>
        public TableData(DataGridView view, bool hide_column_print = false)
        {
            foreach (DataGridViewColumn col in view.Columns)
            {
                // 非表示カラムを印字しない場合、そのカラムが非表示ならば飛ばす
                if (!hide_column_print && !col.Visible) continue;

                Title.Add(col.HeaderText);
            }

            foreach (DataGridViewRow row in view.Rows)
            {
                TableDataRecord record = new TableDataRecord();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 非表示カラムを印字しない場合、そのカラムが非表示ならば飛ばす
                    if (!hide_column_print && !cell.Visible) continue;

                    record.DataList.Add(cell.Value.ToString());
                }

                this.RecordList.Add(record);
            }
        }

        /// <summary>
        /// CSVファイルに保存する
        /// </summary>
        /// <param name="file">保存ファイル名</param>
        /// <param name="append">true 追記, false 上書き</param>
        /// <param name="title_print">カラム名を印字するかどうか</param>
        /// <param name="dialog_show">SaveFileDialogを表示するかどうか</param>
        /// <returns></returns>
        public bool CSVSave(string file, bool append, bool title_print = true, bool dialog_show = true)
        {
            string save_file = file;

            if (dialog_show || file.Length == 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                if (file.Length > 0)
                {
                    saveFileDialog1.FileName = file;
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    save_file = saveFileDialog1.FileName;
                }
                else
                {
                    return false;
                }
            }

            try
            {
                StreamWriter writer = new StreamWriter(save_file, append, Encoding.GetEncoding("shift-jis"));

                if (title_print)
                {
                    foreach (string s in this.Title)
                    {
                        writer.Write("\"" + s.Replace("\"", "\"\"") + "\",");
                    }

                    writer.WriteLine();

                    // ２番目のカラム名リストが存在する場合
                    if (this.Title2.Count > 0)
                    {
                        foreach (string s in this.Title2)
                        {
                            writer.Write("\"" + s.Replace("\"", "\"\"") + "\",");
                        }

                        writer.WriteLine();
                    }
                }

                foreach (TableDataRecord record in this.RecordList)
                {
                    foreach (string r in record.DataList)
                    {
                        writer.Write("\"" + r.Replace("\"", "\"\"") + "\",");
                    }

                    writer.WriteLine();
                }

                writer.Close();

                return true;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                return false;
            }
        }

        public bool ExcelOpen(bool title_print = true)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = false;

            Excel.Workbook work = (Excel.Workbook)(app.Workbooks.Add(Type.Missing));
            Excel.Worksheet sheet = (Excel.Worksheet)(work.ActiveSheet);

            try
            {
                int x = 1;
                int y = 1;

                if (title_print)
                {
                    // 先頭行にはカラム名を入れていく。
                    foreach (string s in this.Title)
                    {
                        sheet.Cells[y, x] = s;
                        x++;
                    }

                    y++;

                    // ２番目のカラム名リストが存在する場合
                    if (this.Title2.Count > 0)
                    {
                        foreach (string s in this.Title2)
                        {
                            sheet.Cells[y, x] = s;
                            x++;
                        }

                        y++;
                    }
                }

                // セル単位の代入は1回ごとにExcelとのプロセス間通信が発生して遅いため、
                // 1000行ずつ2次元配列にまとめて一括代入する。
                const int chunk_rows = 1000;

                for (int start = 0; start < this.RecordList.Count; start += chunk_rows)
                {
                    int rows = Math.Min(chunk_rows, this.RecordList.Count - start);
                    int cols = 0;

                    for (int i = 0; i < rows; i++)
                    {
                        cols = Math.Max(cols, this.RecordList[start + i].DataList.Count);
                    }

                    if (cols > 0)
                    {
                        object[,] values = new object[rows, cols];

                        for (int i = 0; i < rows; i++)
                        {
                            List<string> list = this.RecordList[start + i].DataList;

                            for (int j = 0; j < list.Count; j++)
                            {
                                values[i, j] = list[j];
                            }
                        }

                        Excel.Range from = (Excel.Range)(sheet.Cells[y, 1]);
                        Excel.Range to = (Excel.Range)(sheet.Cells[y + rows - 1, cols]);
                        Excel.Range range = sheet.get_Range(from, to);

                        range.Value2 = values;

                        Marshal.ReleaseComObject(range);
                        Marshal.ReleaseComObject(to);
                        Marshal.ReleaseComObject(from);
                    }

                    y += rows;
                }

                app.Visible = true;
                return true;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                return false;
            }
            finally
            {
                Marshal.ReleaseComObject(sheet);
                Marshal.ReleaseComObject(work);
                Marshal.ReleaseComObject(app);

                GC.Collect();
            }
        }

        public bool ExcelSave(string file, bool title_print = true, bool dialog_show = true)
        {
            string save_file = file;

            if (dialog_show)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                if (file.Length > 0)
                {
                    saveFileDialog1.FileName = file;
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    save_file = saveFileDialog1.FileName;
                }
                else
                {
                    return false;
                }
            }

            Excel.Application app = new Excel.Application();
            app.Visible = false;

            Excel.Workbook work = (Excel.Workbook)(app.Workbooks.Add(Type.Missing));
            Excel.Worksheet sheet = (Excel.Worksheet)(work.ActiveSheet);

            try
            {
                int x = 1;
                int y = 1;

                if (title_print)
                {
                    // 先頭行にはカラム名を入れていく。
                    foreach (string s in this.Title)
                    {
                        sheet.Cells[y, x] = s;
                        x++;
                    }

                    y++;

                    // ２番目のカラム名リストが存在する場合
                    if (this.Title2.Count > 0)
                    {
                        foreach (string s in this.Title2)
                        {
                            sheet.Cells[y, x] = s;
                            x++;
                        }

                        y++;
                    }
                }

                foreach (TableDataRecord record in this.RecordList)
                {
                    x = 1;

                    foreach (string r in record.DataList)
                    {
                        sheet.Cells[y, x] = r;
                        x++;
                    }

                    y++;
                }

                if (save_file.Length > 0)
                {
                    work.SaveAs(save_file, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                return true;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                return false;
            }
            finally
            {
//                work.Close();
//                app.Quit();

                Marshal.ReleaseComObject(sheet);
                Marshal.ReleaseComObject(work);
                Marshal.ReleaseComObject(app);

//                GC.Collect();
            }
        }
/*
        /// <summary>
        /// CSV保存時にカラム名を出力するかどうか
        /// </summary>
        public enum TitlePrint : int
        {
            No = 0,
            Yes = 1
        }

        /// <summary>
        /// SaveFileDialogを表示するかどうか
        /// </summary>
        public enum FileDialogShow : int
        {
            No = 0,
            Yes = 1
        }
 */
    }

    /// <summary>
    /// 出力レコード
    /// </summary>
    public class TableDataRecord
    {
        public List<string> DataList = new List<string>();
    }
}
