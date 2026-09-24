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
                // AllowUserToAddRows の新規行は飛ばす
                if (row.IsNewRow) continue;

                TableDataRecord record = new TableDataRecord();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 非表示カラムを印字しない場合、そのカラムが非表示ならば飛ばす
                    if (!hide_column_print && !cell.Visible) continue;

                    record.DataList.Add(Convert.ToString(cell.Value));
                }

                this.RecordList.Add(record);
            }
        }

        /// <summary>
        /// 保存先を選ぶダイアログを表示する。
        /// </summary>
        /// <param name="file">初期ファイル名</param>
        /// <param name="filter">ファイルの種類のフィルタ（省略可）</param>
        /// <returns>選ばれたファイル名（キャンセル時は空文字）</returns>
        public static string SelectSaveFile(string file, string filter = "")
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (file.Length > 0)
            {
                saveFileDialog1.FileName = file;
            }

            if (filter.Length > 0)
            {
                saveFileDialog1.Filter = filter;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }

            return "";
        }

        /// <summary>
        /// CSVファイルに書き込む（ダイアログは表示しない）。
        /// progress が OperationCanceledException を投げた場合は、書きかけのファイルを削除して再送出する。
        /// </summary>
        /// <param name="save_file">保存ファイル名</param>
        /// <param name="append">true 追記, false 上書き</param>
        /// <param name="title_print">カラム名を印字するかどうか</param>
        /// <param name="progress">進捗の通知先（省略可）。1000行ごとに通知する</param>
        /// <returns></returns>
        public bool CSVWrite(string save_file, bool append, bool title_print = true, Action<string> progress = null)
        {
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(save_file, append, Encoding.GetEncoding("shift-jis"));

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

                int count = 0;

                foreach (TableDataRecord record in this.RecordList)
                {
                    foreach (string r in record.DataList)
                    {
                        writer.Write("\"" + r.Replace("\"", "\"\"") + "\",");
                    }

                    writer.WriteLine();

                    count++;

                    if (progress != null && count % 1000 == 0)
                    {
                        progress("CSVに書き込み中 " + count.ToString("#,0") + " / " + this.RecordList.Count.ToString("#,0") + "行");
                    }
                }

                writer.Close();

                return true;
            }
            catch (OperationCanceledException)
            {
                // 中止時は書きかけのファイルを残さない（追記時は既存の内容ごと消えるため残す）
                writer.Close();

                if (!append)
                {
                    File.Delete(save_file);
                }

                throw;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                return false;
            }
        }

        /// <summary>
        /// Excelを表示せずに出力し、xlsx 形式で保存して閉じる。
        /// progress が OperationCanceledException を投げた場合は、保存せずに閉じて再送出する。
        /// </summary>
        /// <param name="save_file">保存先のファイル名</param>
        /// <param name="title_print">カラム名を印字するかどうか</param>
        /// <param name="progress">進捗の通知先（省略可）。1000行ごとに通知する</param>
        /// <returns></returns>
        public bool ExcelWrite(string save_file, bool title_print = true, Action<string> progress = null)
        {
            try
            {
                return excelWrite(save_file, title_print, progress);
            }
            finally
            {
                // Cells 等の一時的な COM 参照が残ると Quit 後も Excel が終了しないため、
                // excelWrite を抜けて参照が不要になった時点で回収する
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private bool excelWrite(string save_file, bool title_print, Action<string> progress)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            // 上書きの確認は保存ダイアログで済んでいるため、Excel 側の確認は出さない
            app.DisplayAlerts = false;

            Excel.Workbook work = (Excel.Workbook)(app.Workbooks.Add(Type.Missing));
            Excel.Worksheet sheet = (Excel.Worksheet)(work.ActiveSheet);
            sheet.Name = "一覧";

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

                    if (progress != null)
                    {
                        progress("Excelに書き込み中 " + (start + rows).ToString("#,0") + " / " + this.RecordList.Count.ToString("#,0") + "行");
                    }
                }

                // 51 = xlOpenXMLWorkbook (.xlsx)
                work.SaveAs(save_file, 51, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
                return false;
            }
            finally
            {
                // 保存済み・中止・エラーのいずれでも、非表示の Excel プロセスを残さない
                try
                {
                    work.Close(false, Type.Missing, Type.Missing);
                }
                catch
                {
                }

                try
                {
                    app.Quit();
                }
                catch
                {
                }

                Marshal.ReleaseComObject(sheet);
                Marshal.ReleaseComObject(work);
                Marshal.ReleaseComObject(app);
            }
        }
    }

    /// <summary>
    /// 出力レコード
    /// </summary>
    public class TableDataRecord
    {
        public List<string> DataList = new List<string>();
    }
}
