using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    class MWMData
    {
        public string OrderId = "";
        public string PtId = "";      /* 0埋め8桁 */
        public string Name = "";
        public string Kana = "";
        public string Rome = "";    /* 不明は空白 */
        public string Sex = "";     /* 1 男性, 2 女性, 不明は空白 */
        public string Birth = "";   /* YYYYMMDD 不明は空白 */
        public string Comment = ""; /* 内視鏡モダリティ番号 */
        public string KensaDate = "";   /* 検査日 YYYYMMDD */
        public string Modality = "";    /* CT, MR, ES, US */
        public string MwmId = "";     /* 最大16桁 */
        public string Status = "0";      /* 0 新規, 1 削除, 2 変更 */
        public string Handle = "1";      /* 1 予約受付, 2 受付完了 */

        public static void MakeCSV(List<MWMData> dataList, string path)
        {
            string file = "";

            if (dataList.Count > 0)
            {
                file = dataList[0].OrderId;
            }
            else
            {
                return;
            }

            // フルパスに変換する。拡張子はつけない。
            file = path.TrimEnd('\\') + "\\" + file;

            // 既に同一オーダー番号の csv ファイルが存在すれば削除する
            if (File.Exists(file + ".csv"))
            {
                File.Delete(file + ".csv");
            }

            StreamWriter writer = new StreamWriter(new FileStream(file + ".tmp", FileMode.Create), Encoding.Default);

            foreach (MWMData tmpData in dataList)
            {
                writer.Write(tmpData.PtId.PadLeft(8, '0') + ",");
                writer.Write(tmpData.Name + ",");
                writer.Write(tmpData.Kana + ",");
                writer.Write(tmpData.Rome + ",");
                writer.Write(tmpData.Sex + ",");
                writer.Write(tmpData.Birth + ",");
                writer.Write(tmpData.Comment + ",");
                writer.Write(tmpData.KensaDate + ",");
                writer.Write(tmpData.Modality + ",");
                writer.Write(tmpData.MwmId + ",");
                writer.Write(tmpData.MwmId + ",");
                writer.Write(tmpData.Status + ",");
                writer.Write(tmpData.Handle);

                writer.Write("\r\n");
            }

            writer.Close();

            File.Move(file + ".tmp", file + ".csv");
        }
    }
}
