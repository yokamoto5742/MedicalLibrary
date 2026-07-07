using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MedicalLibrary.Utility
{
    public class AppDataGridView
    {
        /// <summary>
        /// 女性の場合は文字色を赤くする
        /// </summary>
        /// <param name="view"></param>
        /// <param name="sex_column">このカラムの値が 2, F, 女, 女性 のいずれかなら女性とみなす</param>
        /// <param name="red_column_list">文字を赤くするカラムのリスト。空の場合は氏名のみ。</param>
        /// <returns></returns>
        public static void SexColor(DataGridView view, string sex_column = "", List<string> red_column_list = null)
        {
            try
            {
                string sex_col = "";

                if (sex_column.Length > 0 && view.Columns.Contains(sex_column))
                {
                    sex_col = sex_column;
                }
                else if (view.Columns.Contains("性別"))
                {
                    sex_col = "性別";
                }

                // 性別を判断するカラムが無ければ終了
                if (sex_col.Length == 0)
                {
                    return;
                }

                List<string> red_col_list = new List<string>();

                if (red_column_list != null)
                {
                    foreach (string s in red_column_list)
                    {
                        if (view.Columns.Contains(s))
                        {
                            red_col_list.Add(s);
                        }
                    }
                }

                if (red_col_list.Count == 0)
                {
                    if (view.Columns.Contains("カナ")) red_col_list.Add("カナ");
                    if (view.Columns.Contains("氏名")) red_col_list.Add("氏名");
                }

                // 文字を赤くするカラムが無ければ終了
                if (red_col_list.Count == 0)
                {
                    return;
                }

                foreach (DataGridViewRow r in view.Rows)
                {
                    if (r.Cells[sex_col].Value.ToString().Equals("2") ||
                        r.Cells[sex_col].Value.ToString().Equals("F") ||
                        r.Cells[sex_col].Value.ToString().Equals("女") ||
                        r.Cells[sex_col].Value.ToString().Equals("女性"))
                    {
                        foreach (string column in red_col_list)
                        {
                            r.Cells[column].Style.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LibUtility.Except(ex);
            }
        }
    }
}
