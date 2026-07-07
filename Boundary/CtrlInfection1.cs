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
    public partial class CtrlInfection1 : UserControl
    {
        PatBase Pat = new PatBase();

        DataSet dSet = new DataSet();

        public CtrlInfection1()
        {
            InitializeComponent();

            // 検査結果テーブル
            dSet.Tables.Add("KensaResult");

            // 異常結果テーブル
            dSet.Tables.Add("KensaMod");

            KensaDataView1.CellMouseEnter += new DataGridViewCellEventHandler(KensaDataView1_CellMouseEnter);
            KensaDataView1.CellMouseLeave += new DataGridViewCellEventHandler(KensaDataView1_CellMouseLeave);
        }

        private void CtrlInfection1_Load(object sender, EventArgs e)
        {
        }

        public void PatSet(PatBase p)
        {
            this.Pat = p;

            this.DataShow();
        }

        void DataShow()
        {
            if (!dSet.Tables.Contains("KensaResult") || !dSet.Tables.Contains("KensaMod"))
            {
                return;
            }

            string pt_id = this.Pat.Id;

            // 検査結果一覧をクリアする
            DataTable table1 = dSet.Tables["KensaResult"];
            table1.Columns.Clear();
            table1.Rows.Clear();

            // 異常結果一覧をクリアする
            DataTable table2 = dSet.Tables["KensaMod"];
            table2.Columns.Clear();
            table2.Rows.Clear();


            // 検査項目一覧を取得する
            Dictionary<string, KensaMaster> master_dict = KensaMaster.GetInfectionDict(pt_id);

            // 検査結果一覧を取得する
            Dictionary<string, Dictionary<string, KensaData>> data_dict = KensaData.GetInfectionDict(pt_id);


            table1.Columns.Add("項目");
            table1.Columns.Add("基準値");
            table1.Columns.Add("単位");

            table2.Columns.Add("項目");
            table2.Columns.Add("基準値");
            table2.Columns.Add("単位");


            // 検査結果の受付番号カラムを生成する
            foreach (string d in data_dict.Keys)
            {
                // KensaDataView に検査日のカラムを生成
                table1.Columns.Add(d);
                table2.Columns.Add(d);
            }


            // KensaDataView に検査項目ごとの結果行を生成
            foreach (string m_key in master_dict.Keys)
            {
                DataRow r1 = table1.NewRow();
                DataRow r2 = table2.NewRow();

                // 先頭のカラム
                r1["項目"] = master_dict[m_key].Name;
                //                r2["項目"] = master_dict[m_key].Name;

                bool normal_unit_flg = false;

                // 受付番号ごとのデータをカラムにセットする
                foreach (string d in data_dict.Keys)
                {
                    Dictionary<string, KensaData> dd = data_dict[d];

                    if (dd.ContainsKey(m_key))
                    {
                        if (!normal_unit_flg)
                        {
                            r1["基準値"] = dd[m_key].Normal;
                            r1["単位"] = dd[m_key].Unit;
                            normal_unit_flg = true;
                        }

                        r1[d] = dd[m_key].Result;
                        r2[d] = dd[m_key].ModFlg.ToString();
                    }
                }

                table1.Rows.Add(r1);
                table2.Rows.Add(r2);
            }

            this.DataFormat();
        }

        public void DataFormat()
        {
            if (!dSet.Tables.Contains("KensaResult") || !dSet.Tables.Contains("KensaMod"))
            {
                return;
            }

            DataTable table1 = dSet.Tables["KensaResult"];
            DataTable table2 = dSet.Tables["KensaMod"];

            KensaDataView1.DataSource = new DataView(table1);

            KensaDataView1.Columns["項目"].Width = 120;
            KensaDataView1.Columns["項目"].DefaultCellStyle.BackColor = Color.LightYellow;

            KensaDataView1.Columns["基準値"].Width = 80;
            KensaDataView1.Columns["基準値"].DefaultCellStyle.BackColor = Color.LightYellow;

            KensaDataView1.Columns["単位"].Width = 60;
            KensaDataView1.Columns["単位"].DefaultCellStyle.BackColor = Color.LightYellow;
            KensaDataView1.Columns["単位"].Frozen = true;

            for (int i = 3; i < KensaDataView1.Columns.Count; i++)
            {
                // DataFormat が複数回実施されると、日付がおかしくなるので、それを防ぐ
                if (KensaDataView1.Columns[i].HeaderText.Length > 10)
                {
                    KensaDataView1.Columns[i].HeaderText = DateTimeAgent.DateFormat(KensaDataView1.Columns[i].HeaderText.Substring(0, 8), DateTimeAgent.DateFormatKind.LONG);
                }
            }

            // 異常値ならば色を変更する
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                for (int j = 0; j < table2.Rows[i].ItemArray.Length; j++)
                {
                    if (table2.Rows[i].ItemArray[j].ToString().Equals("1"))
                    {
                        KensaDataView1.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                    }
                    else if (table2.Rows[i].ItemArray[j].ToString().Equals("2"))
                    {
                        KensaDataView1.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        void KensaDataView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex >= 0)
            {
                KensaDataView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightPink;
                KensaDataView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightPink;
            }
        }

        void KensaDataView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex >= 0)
            {
                KensaDataView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightYellow;
                KensaDataView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
        }
    }
}
