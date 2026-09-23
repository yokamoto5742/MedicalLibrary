using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalLibrary.Utility;
using System.Runtime.CompilerServices;

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
		/// InnoUketsukeLib による認証。
		/// DLL が無い環境では呼び出し時点で FileNotFoundException が発生するため、
		/// 呼び出し元の try/catch で捕捉できるよう別メソッドに分離（インライン化禁止）。
		/// </summary>
		[MethodImpl(MethodImplOptions.NoInlining)]
		static bool VerifyInnoUketsukeLib(int i, string pw)
		{
			return InnoUketsukeLib.Entity.M_USR.g_Usr1.GetData(i, pw);
		}

        public static Staff Verify(string id, string pw)
        {
            Staff obj = new Staff();

			try
			{
				// InnoUketsukeLib で認証
				int i = 0;
				int.TryParse(id, out i);

				// 認証に失敗したら終了
				if (!VerifyInnoUketsukeLib(i, pw)) return obj;
			}
			catch (Exception ex)
			{
				// InnoUketsukeLib の例外が生じたらスルー
				LibUtility.Except(ex, false);
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
