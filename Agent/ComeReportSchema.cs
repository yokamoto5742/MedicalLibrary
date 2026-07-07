using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class ComeReportSchema
    {
        public PictureBox SchemaBox;
        public double Zoom;
        public string BgId;
        private string bgImagePath;

        public string BgImagePath
        {
            get
            {
                if (ComeReportSettings.Current.SchemaBgDict.ContainsKey(BgId))
                {
                    return ComeReportSettings.Current.SchemaBgDict[BgId].Path;
                }
                else
                {
                    return bgImagePath;
                }
            }
            set
            {
                bgImagePath = value;
            }
        }

        public List<ComeReportSchemaItem> SchemaItemList;

        public ComeReportSchema()
        {
            this.SchemaBox = new PictureBox();
            this.Zoom = 1.0;
            this.BgId = "";
            this.bgImagePath = "";
            this.SchemaItemList = new List<ComeReportSchemaItem>();
        }

        public ComeReportSchema(PictureBox schemaBox, double zoom)
        {
            this.SchemaBox = schemaBox;

            if (zoom.Equals(0.0))
            {
                this.Zoom = 1.0;
            }
            else
            {
                this.Zoom = zoom;
            }

            this.BgId = "";
            this.bgImagePath = "";
            this.SchemaItemList = new List<ComeReportSchemaItem>();
        }

        public ComeReportSchema(PictureBox schemaBox, double zoom, string bgId)
        {
            this.SchemaBox = schemaBox;

            if (zoom.Equals(0.0))
            {
                this.Zoom = 1.0;
            }
            else
            {
                this.Zoom = zoom;
            }

            this.BgId = bgId;
            this.bgImagePath = "";
            this.SchemaItemList = new List<ComeReportSchemaItem>();
        }

        /// <summary>
        /// DB保存形式のテキストからシェーマアイテムを作ります。
        /// </summary>
        /// <param name="text"></param>
        public void MakeSchemaItemListFromText(string text)
        {
            this.SchemaItemList.Clear();

            if (text != null && text.Length > 0)
            {
                string[] s = (text + "\n").Split('\n');

                if (s.Length > 0)
                {
                    foreach (string ss in s)
                    {
                        ComeReportSchemaItem tmpItem = new ComeReportSchemaItem(ss);
                        string kind = tmpItem.GetKind();

                        if (kind != null && (kind.Equals("直線") || kind.Equals("楕円") || kind.Equals("四角") || kind.Equals("文字") || kind.Equals("フリー")))
                        {
                            this.SchemaItemList.Add(tmpItem);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// シェーマアイテムからDB保存形式のテキストを作る。
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            string text = "";

            foreach (ComeReportSchemaItem si in SchemaItemList)
            {
                text += si.MakeText() + "\r\n";
            }

            return text;
        }

        /// <summary>
        /// シェーマアイテムのバイト数を計算する。
        /// </summary>
        /// <returns></returns>
        public int GetByteCount()
        {
            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(this.GetText());
        }

        /// <summary>
        /// schemaItem をリストに追加する。
        /// </summary>
        /// <param name="schemaItem"></param>
        public void AddSchemaItem(ComeReportSchemaItem schemaItem)
        {
            if (this.GetByteCount() + schemaItem.GetByteCount() < 4000)
            {
                this.SchemaItemList.Add(schemaItem);
            }
            else
            {
                MessageBox.Show("容量オーバーのためシェーマアイテムを作成できません");
            }
        }

        /// <summary>
        /// schemaItem と一致するシェーマアイテムを削除する。
        /// </summary>
        /// <param name="schemaItem"></param>
        public void RemoveSchemaItem(ComeReportSchemaItem schemaItem)
        {
            foreach (ComeReportSchemaItem tmpItem in SchemaItemList)
            {
                if (tmpItem.Equals(schemaItem))
                {
                    this.SchemaItemList.Remove(tmpItem);
                    break;
                }
            }
        }

        /// <summary>
        /// schemaItem と一致するシェーマアイテムを返す。
        /// なければ null を返す。
        /// </summary>
        /// <param name="schemaItem"></param>
        /// <returns></returns>
        public ComeReportSchemaItem FindSchemaItem(ComeReportSchemaItem schemaItem)
        {
            foreach(ComeReportSchemaItem tmpItem in SchemaItemList)
            {
                if (tmpItem.Equals(schemaItem))
                {
                    return tmpItem;
                }
            }

            return null;
        }

        /// <summary>
        /// schemaItem と一致するアイテムのインデックス番号を返す。
        /// なければ -1 を返す。
        /// </summary>
        /// <param name="schemaItem"></param>
        /// <returns></returns>
        public int FindSchemaItemAt(ComeReportSchemaItem schemaItem)
        {
            foreach (ComeReportSchemaItem tmpItem in SchemaItemList)
            {
                if (tmpItem.Equals(schemaItem))
                {
                    return SchemaItemList.IndexOf(tmpItem);
                }
            }

            return -1;
        }

        /// <summary>
        /// schemaItem を i 番目の要素にセットする。
        /// </summary>
        /// <param name="i"></param>
        /// <param name="schemaItem"></param>
        public void SetSchemaItemAt(int i, ComeReportSchemaItem schemaItem)
        {
            if (i >= 0 && i < SchemaItemList.Count)
            {
                if (this.GetByteCount() + schemaItem.GetByteCount() < 4000)
                {
                    SchemaItemList[i] = schemaItem;
                }
                else
                {
                    MessageBox.Show("容量オーバーのためシェーマアイテムを作成できません");
                }
            }
        }

        /// <summary>
        /// Point p の近くを通るシェーマアイテムがあれば返す。
        /// なければ null を返す。
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ComeReportSchemaItem PointSchemaItem(Point p)
        {
            foreach (ComeReportSchemaItem tmpItem in SchemaItemList)
            {
                if (tmpItem.PassPoint(p))
                {
                    return tmpItem;
                }
            }

            return null;
        }

        /// <summary>
        /// SchemaBox に背景とシェーマアイテムを描画する。
        /// </summary>
        public void Draw()
        {
            SchemaBox.Image = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            Graphics g = Graphics.FromImage(SchemaBox.Image);

            if (this.BgImagePath.Length > 0 && System.IO.File.Exists(this.BgImagePath))
            {
                this.SchemaBox.BackgroundImage = Image.FromFile(this.BgImagePath);
                this.SchemaBox.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                this.SchemaBox.BackgroundImage = null;
            }

            SchemaBox.Controls.Clear();

            foreach (ComeReportSchemaItem si in SchemaItemList)
            {
                si.Draw(SchemaBox, this.Zoom);
            }
        }

        /// <summary>
        /// クローンを返す。
        /// </summary>
        /// <returns></returns>
        public ComeReportSchema Clone()
        {
            ComeReportSchema s = new ComeReportSchema();

            s.SchemaBox = this.SchemaBox;
            s.Zoom = this.Zoom;
            s.BgId = this.BgId;

            if (this.BgId.Length == 0 && this.BgImagePath.Length > 0)
            {
                s.BgImagePath = this.BgImagePath;
            }

            for (int i = 0; i < this.SchemaItemList.Count; i++)
            {
                ComeReportSchemaItem si = this.SchemaItemList[i].Clone();
                s.SchemaItemList.Add(si);
            }

            return s;
        }
    }
}
