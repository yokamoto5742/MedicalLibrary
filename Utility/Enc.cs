using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace MedicalLibrary.Utility
{
    /*
    public static class Enc
    {
        static char[] keys = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '-', '+', '*', '/', '\\', '=', '^', '~', '|', '`', '@', '[', ']', '{', '}', ';', ':', '_', '?', ',', '.', '<', '>' };
        static char[] vals = new char[] { '?', '.', 'u', ',', '[', '<', '8', 'z', ';', 'y', '+', 'J', 'Q', 'K', 'C', 'H', 'T', 'B', 'R', 'F', 'U', 'S', 'D', 'O', 'Y', 'E', 'I', 'X', 'N', 'L', 'A', 'W', 'Z', 'V', 'P', 'M', 'G', '%', '1', '|', '6', '/', 'c', '\"', '{', 'f', '0', 'g', 'h', '*', 'i', 'x', 'k', 'l', '7', 'n', 'j', '~', 'r', 's', '>', '5', 't', 'a', 'v', 'w', '3', '&', '9', '!', 'p', '#', '$', 'd', '\'', '(', ')', '-', '\\', 'q', '=', '^', 'm', '`', '4', '@', 'b', ']', 'o', '}', 'e', '2', ':', '_' };

        static Dictionary<char, char> encDict = new Dictionary<char, char>();
        static Dictionary<char, char> decDict = new Dictionary<char, char>();

        private static void init()
        {
            encDict.Clear();
            decDict.Clear();

            for (int i = 0; i < keys.Length; i++)
            {
                encDict.Add(keys[i], vals[i]);
                decDict.Add(vals[i], keys[i]);
            }
        }

        public static string Encrypt(string s)
        {
            string ret = "";

            init();

            for (int i = 0; i < s.Length; i++)
            {
                if (encDict.ContainsKey(s[i]))
                {
                    ret += encDict[s[i]].ToString();
                }
                else
                {
                    ret += s[i].ToString();
                }
            }

            return ret;
        }

        public static string Decrypt(string s)
        {
            string ret = "";

            init();

            for (int i = 0; i < s.Length; i++)
            {
                if (decDict.ContainsKey(s[i]))
                {
                    ret += decDict[s[i]].ToString();
                }
                else
                {
                    ret += s[i].ToString();
                }
            }

            return ret;
        }
    }
     */

    public class AES
    {
        // 128bit(16byte)のIV（初期ベクタ）とKey（暗号キー）
        private const string AesIV = @"!QAZ2WSX#EDC4RFV";
        private const string AesKey = @"5TGB&YHN7UJM(IK<";

        /// <summary>
        /// 文字列をAESで暗号化
        /// </summary>
        public static string Encrypt(string text)
        {
            // AES暗号化サービスプロバイダ
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(AesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // 文字列をバイト型配列に変換
            byte[] src = Encoding.Unicode.GetBytes(text);

            // 暗号化する
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                // バイト型配列からBase64形式の文字列に変換
                return Convert.ToBase64String(dest);
            }
        }

        /// <summary>
        /// 文字列をAESで復号化
        /// </summary>
        public static string Decrypt(string text)
        {
            // AES暗号化サービスプロバイダ
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(AesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Base64形式の文字列からバイト型配列に変換
            byte[] src = System.Convert.FromBase64String(text);

            // 複号化する
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }
    }
}
