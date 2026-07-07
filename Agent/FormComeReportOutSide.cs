using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public partial class FormComeReportOutSide : Form
    {
        FormComeReportList F1;

        DataSet DSet = new DataSet();

        public FormComeReportOutSide(FormComeReportList f1)
        {
            InitializeComponent();

            this.F1 = f1;
        }

        private void FormComeReportOutSide_Load(object sender, EventArgs e)
        {
            DataTable tmpTable = DSet.Tables.Add("オーダー");
            tmpTable.Columns.Add("オーダー番号");
            tmpTable.Columns.Add("施行部署１");
            tmpTable.Columns.Add("種別");
            tmpTable.Columns.Add("施行日");
            tmpTable.Columns.Add("ID");
            tmpTable.Columns.Add("氏名");
            tmpTable.Columns.Add("年齢");
            tmpTable.Columns.Add("性別");
            tmpTable.Columns.Add("入外区分");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("内容");
            tmpTable.Columns.Add("科コード");
            tmpTable.Columns.Add("診療科");
            tmpTable.Columns.Add("指示医コード");
            tmpTable.Columns.Add("指示医");
            tmpTable.Columns.Add("施行");
            tmpTable.Columns.Add("部位");
            tmpTable.Columns.Add("依頼", typeof(bool));

            tmpTable = DSet.Tables.Add("所見");
            tmpTable.Columns.Add("オーダー番号");
            tmpTable.Columns.Add("施行部署１");
            tmpTable.Columns.Add("種別");
            tmpTable.Columns.Add("施行日");
            tmpTable.Columns.Add("ID");
            tmpTable.Columns.Add("氏名");
            tmpTable.Columns.Add("年齢");
            tmpTable.Columns.Add("性別");
            tmpTable.Columns.Add("入外区分");
            tmpTable.Columns.Add("入外");
            tmpTable.Columns.Add("内容");
            tmpTable.Columns.Add("科コード");
            tmpTable.Columns.Add("診療科");
            tmpTable.Columns.Add("指示医コード");
            tmpTable.Columns.Add("指示医");
            tmpTable.Columns.Add("施行");
            tmpTable.Columns.Add("部位");
            tmpTable.Columns.Add("依頼", typeof(bool));

            DateTime21.Value = DateTime21.Value.AddDays(-10);

            this.ShowView1();
        }

        private void ShowView1()
        {
            string startDate = DateTime11.Value.ToString("yyyyMMdd");
            string endDate = DateTime12.Value.ToString("yyyyMMdd");

            if (ComeReportSettings.Current.OutSideList.Count > 0)
            {
                DataTable tmpTable = DSet.Tables["オーダー"];
                tmpTable.Clear();

                List<ComeReportOrder> list = ComeReportOrder.GetList3(startDate, endDate, ComeReportSettings.Current.OutSideList.ToArray(), true);

                foreach (ComeReportOrder obj in list)
                {
                    DataRow r = tmpTable.NewRow();

                    r["オーダー番号"] = obj.OrderId;
                    r["施行部署１"] = obj.Sekou1;
                    r["種別"] = obj.SekouName1;
                    r["施行日"] = DateTimeAgent.DateFormat(obj.SekouDate, DateTimeAgent.DateFormatKind.LONG);
                    r["ID"] = obj.Pat.Id;
                    r["氏名"] = obj.Pat.Name;
                    r["年齢"] = obj.Pat.Age;
                    r["性別"] = obj.Pat.Sex;
                    r["入外区分"] = obj.InOut;
                    r["入外"] = obj.InOutName;
                    r["内容"] = obj.SOAP;
                    r["科コード"] = obj.Dept;
                    r["診療科"] = obj.DeptName;
                    r["指示医コード"] = obj.Doctor;
                    r["指示医"] = obj.DoctorName;
                    r["施行"] = obj.SekouFlg.Equals("1") ? "○" : "";
                    r["部位"] = obj.ReportListString;
                    r["依頼"] = obj.OutSideDone.Equals("1");

                    tmpTable.Rows.Add(r);
                }
            }

            this.filterView1();
        }

        private void ShowButton1_Click(object sender, EventArgs e)
        {
            if (DateTime12.Value.Subtract(DateTime11.Value).Days >= 5)
            {
                MessageBox.Show("検索が重くなりますので５日間以上の指定はできません。");
            }
            else
            {
                this.ShowView1();
            }
        }

        private void ShowView2()
        {
            string startDate = DateTime21.Value.ToString("yyyyMMdd");
            string endDate = DateTime22.Value.ToString("yyyyMMdd");

            List<ComeReportOrder> list = ComeReportOrder.GetList4(startDate, endDate, ComeReportSettings.Current.OutSideList.ToArray(), true);

            DataTable tmpTable = DSet.Tables["所見"];
            tmpTable.Clear();

            foreach (ComeReportOrder obj in list)
            {
                DataRow r = tmpTable.NewRow();

                r["オーダー番号"] = obj.OrderId;
                r["施行部署１"] = obj.Sekou1;
                r["種別"] = obj.SekouName1;
                r["施行日"] = DateTimeAgent.DateFormat(obj.SekouDate, DateTimeAgent.DateFormatKind.LONG);
                r["ID"] = obj.Pat.Id;
                r["氏名"] = obj.Pat.Name;
                r["年齢"] = obj.Pat.AgeCalc(obj.SekouDate);
                r["性別"] = obj.Pat.Sex;
                r["入外区分"] = obj.InOut;
                r["入外"] = obj.InOutName;
                r["内容"] = obj.SOAP;
                r["科コード"] = obj.Dept;
                r["診療科"] = obj.DeptName;
                r["指示医コード"] = obj.Doctor;
                r["指示医"] = obj.DoctorName;
                r["施行"] = obj.SekouFlg.Equals("1") ? "○" : "";
                r["部位"] = obj.ReportListString;
                r["依頼"] = obj.OutSideDone.Equals("1");

                tmpTable.Rows.Add(r);
            }

            this.filterView2();
        }

        private void ShowButton2_Click(object sender, EventArgs e)
        {
            if (DateTime22.Value.Subtract(DateTime21.Value).Days >= 15)
            {
                MessageBox.Show("検索が重くなりますので１５日間以上の指定はできません。");
            }
            else
            {
                this.ShowView2();
            }
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void GridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridView1.Columns[e.ColumnIndex].Name.Equals("依頼"))
            {
                DataGridViewRow tmpRow = GridView1.Rows[e.RowIndex];

                Thread.Sleep(200);

                if (tmpRow.Cells["依頼"].Value.Equals(false))
                {
                    ComeReportOrder.DoneOutSide(tmpRow.Cells["オーダー番号"].Value.ToString(), "1");
                }
                else
                {
                    ComeReportOrder.DoneOutSide(tmpRow.Cells["オーダー番号"].Value.ToString(), "0");
                }

                MessageBox.Show("変更しました");
            }
        }

        private void GridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridView2.Columns[e.ColumnIndex].Name.Equals("依頼"))
            {
                DataGridViewRow tmpRow = GridView2.Rows[e.RowIndex];

                Thread.Sleep(200);

                if (tmpRow.Cells["依頼"].Value.Equals(false))
                {
                    ComeReportOrder.DoneOutSide(tmpRow.Cells["オーダー番号"].Value.ToString(), "1");
                }
                else
                {
                    ComeReportOrder.DoneOutSide(tmpRow.Cells["オーダー番号"].Value.ToString(), "0");
                }

                MessageBox.Show("変更しました");
            }
        }

        private void GridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 末尾カラム以外
            if (e.ColumnIndex < GridView1.Columns.Count - 1)
            {
                FormComeReportPat.FormShow(GridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString(), GridView1.Rows[e.RowIndex].Cells["オーダー番号"].Value.ToString());
                /*
                FormPat fp1 = new FormPat(GridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString(), GridView1.Rows[e.RowIndex].Cells["オーダー番号"].Value.ToString());
                fp1.Show();
                 */
            }
        }

        private void GridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 末尾カラム以外
            if (e.ColumnIndex < GridView2.Columns.Count - 1)
            {
                FormComeReportPat.FormShow(GridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString(), GridView2.Rows[e.RowIndex].Cells["オーダー番号"].Value.ToString());
                /*
                FormPat fp1 = new FormPat(GridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString(), GridView2.Rows[e.RowIndex].Cells["オーダー番号"].Value.ToString());
                fp1.Show();
                 */
            }
        }

        private void UndoneOutBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.filterView1();
        }

        private void filterView1()
        {
            DataView tmpView = new DataView(DSet.Tables["オーダー"]);

            if (UndoneOutBox1.Checked)
            {
                tmpView.RowFilter = "依頼 = false";
            }
            else
            {
                tmpView.RowFilter = "";
            }

            GridView1.DataSource = tmpView;

            GridView1.Columns["オーダー番号"].Visible = false;

            GridView1.Columns["施行部署１"].Visible = false;

            GridView1.Columns["種別"].Width = 65;
            GridView1.Columns["種別"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);
            GridView1.Columns["種別"].ReadOnly = true;

            GridView1.Columns["施行日"].Width = 70;
            GridView1.Columns["施行日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView1.Columns["施行日"].ReadOnly = true;

            GridView1.Columns["ID"].Width = 55;
            GridView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GridView1.Columns["ID"].ReadOnly = true;

            GridView1.Columns["氏名"].Width = 80;
            GridView1.Columns["氏名"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);
            GridView1.Columns["氏名"].ReadOnly = true;

            GridView1.Columns["年齢"].Width = 35;
            GridView1.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView1.Columns["年齢"].ReadOnly = true;

            GridView1.Columns["性別"].Visible = false;

            GridView1.Columns["入外区分"].Visible = false;

            GridView1.Columns["入外"].Width = 35;
            GridView1.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView1.Columns["入外"].ReadOnly = true;

            GridView1.Columns["内容"].Width = 270;
            GridView1.Columns["内容"].ReadOnly = true;

            GridView1.Columns["科コード"].Visible = false;
            GridView1.Columns["診療科"].Width = 65;
            GridView1.Columns["診療科"].ReadOnly = true;

            GridView1.Columns["指示医コード"].Visible = false;
            GridView1.Columns["指示医"].Width = 80;
            GridView1.Columns["指示医"].ReadOnly = true;

            GridView1.Columns["施行"].Width = 35;
            GridView1.Columns["施行"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView1.Columns["施行"].ReadOnly = true;

            GridView1.Columns["部位"].Width = 130;

            GridView1.Columns["依頼"].Width = 35;

            foreach (DataGridViewRow r in GridView1.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("2"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void UndoneOutBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.filterView2();
        }

        private void filterView2()
        {
            DataView tmpView = new DataView(DSet.Tables["所見"]);

            if (UndoneOutBox2.Checked)
            {
                tmpView.RowFilter = "依頼 = false";
            }
            else
            {
                tmpView.RowFilter = "";
            }

            GridView2.DataSource = tmpView;

            GridView2.Columns["オーダー番号"].Visible = false;

            GridView2.Columns["施行部署１"].Visible = false;

            GridView2.Columns["種別"].Width = 65;
            GridView2.Columns["種別"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);
            GridView2.Columns["種別"].ReadOnly = true;

            GridView2.Columns["施行日"].Width = 70;
            GridView2.Columns["施行日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView2.Columns["施行日"].ReadOnly = true;

            GridView2.Columns["ID"].Width = 55;
            GridView2.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GridView2.Columns["ID"].ReadOnly = true;

            GridView2.Columns["氏名"].Width = 80;
            GridView2.Columns["氏名"].DefaultCellStyle.Font = new Font("MS UI Gothic", 9);
            GridView2.Columns["氏名"].ReadOnly = true;

            GridView2.Columns["年齢"].Width = 35;
            GridView2.Columns["年齢"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView2.Columns["年齢"].ReadOnly = true;

            GridView2.Columns["性別"].Visible = false;

            GridView2.Columns["入外区分"].Visible = false;

            GridView2.Columns["入外"].Width = 35;
            GridView2.Columns["入外"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView2.Columns["入外"].ReadOnly = true;

            GridView2.Columns["内容"].Width = 270;
            GridView2.Columns["内容"].ReadOnly = true;

            GridView2.Columns["科コード"].Visible = false;
            GridView2.Columns["診療科"].Width = 65;
            GridView2.Columns["診療科"].ReadOnly = true;

            GridView2.Columns["指示医コード"].Visible = false;
            GridView2.Columns["指示医"].Width = 80;
            GridView2.Columns["指示医"].ReadOnly = true;

            GridView2.Columns["施行"].Width = 35;
            GridView2.Columns["施行"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridView2.Columns["施行"].ReadOnly = true;

            GridView2.Columns["部位"].Width = 130;

            GridView2.Columns["依頼"].Width = 35;

            foreach (DataGridViewRow r in GridView2.Rows)
            {
                if (r.Cells["性別"].Value.ToString().Equals("2"))
                {
                    r.Cells["氏名"].Style.ForeColor = Color.Red;
                }
            }
        }
    }
}