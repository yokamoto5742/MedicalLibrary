using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;
using System.Security.Cryptography;

namespace MedicalLibrary.Entity
{
    public class Staff
    {
        public int Code = 0;
        public string Name = "";

        public string Kana = "";
        public int QualCode = 0;

        public int Status = 0;

        public int SectionCode = 0;

        public int DeptCode = 0;

        public int DoctorCode = 0;

        public Staff()
        {
        }

        public Staff(int code, string name, string kana, int sectionCode, int status, int qualCode, int deptCode, int doctorCode)
        {
            Code = code;
            Name = name;
            Kana = kana;
            SectionCode = sectionCode;
            Status = status;
            QualCode = qualCode;
            DeptCode = deptCode;
            DoctorCode = doctorCode;
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// M_USR.PASSWORD（AES 暗号化）を復号し、入力パスワードと照合する。
        /// 旧 InnoUketsukeLib.Entity.M_USR.GetData の移植。
        /// </summary>
        static bool VerifyPassword(int code, string pw)
        {
            string cmd = "select PASSWORD from M_USR where CODE = :CODE";

            List<StdDbColumn> param_list = new List<StdDbColumn>();
            param_list.Add(new StdDbColumn("CODE", StdDbType.NUMBER, code));

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd, param_list);

            if (tmp_list.Count == 0) return false;

            return pw == DecryptPassword(tmp_list[0].DataDict["PASSWORD"].ToString());
        }

        /// <summary>
        /// M_USR.PASSWORD の復号（AES-128 CBC、平文は UTF-16LE）。
        /// </summary>
        static string DecryptPassword(string text)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = Encoding.UTF8.GetBytes("&94YAKHGQS$FFKQ8");
                aes.Key = Encoding.UTF8.GetBytes("JVT5%W#SA$$%%G0W");
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] cipher_bytes = Convert.FromBase64String(text);

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] plain_bytes = decryptor.TransformFinalBlock(cipher_bytes, 0, cipher_bytes.Length);
                    return Encoding.Unicode.GetString(plain_bytes);
                }
            }
        }

        public static Staff Verify(string id, string pw)
        {
            Staff obj = new Staff();

            try
            {
                int i = 0;
                int.TryParse(id, out i);
                if (i == 0) return obj;

                // 認証に失敗したら終了
                if (!VerifyPassword(i, pw)) return obj;
            }
            catch (Exception ex)
            {
                // 照合中の例外（DB 障害・復号失敗など）は認証失敗とする
                LibUtility.Except(ex, false);
                return obj;
            }

            string cmd = "select CODE コード, Trim(NAME) 氏名, SYOZOKU 所属, SHIKAKU 資格, DEPT 科コード, DR 医師コード " +
                " from M_USR " +
                " where CODE = " + id;

            List<StdClass> tmp_list = StdClass.GetList(DB.Db3, cmd);

            foreach (StdClass tmp in tmp_list)
            {
                int.TryParse(tmp.DataDict["コード"].ToString(), out obj.Code);
                obj.Name = tmp.DataDict["氏名"].ToString();
                int.TryParse(tmp.DataDict["所属"].ToString(), out obj.SectionCode);
                int.TryParse(tmp.DataDict["資格"].ToString(), out obj.QualCode);

                if (tmp.DataDict["科コード"].ToString().Length > 0 && tmp.DataDict["科コード"].ToString() != "0")
                {
                    int.TryParse(tmp.DataDict["科コード"].ToString(), out obj.DeptCode);
                }

                if (tmp.DataDict["医師コード"].ToString().Length > 0 && tmp.DataDict["医師コード"].ToString() != "0")
                {
                    int.TryParse(tmp.DataDict["医師コード"].ToString(), out obj.DoctorCode);
                }

                break;
            }

            return obj;
        }
    }
}
