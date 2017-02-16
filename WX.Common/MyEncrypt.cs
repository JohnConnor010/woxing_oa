using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace WX
{
    class MyEncrypt
    {
        public MyEncrypt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="CryptText">要加密的字符串</param>
        /// <param name="CryptKey">密匙 八位 字符</param>
        /// <param name="CryptIV">对称算法初始化向量8位字符</param>
        /// <returns></returns>
        public static string DESEncrypt(string CryptText, string CryptKey, string CryptIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] textOut = Encoding.Default.GetBytes(CryptText);
            byte[] DESKey = ASCIIEncoding.ASCII.GetBytes(CryptKey);
            byte[] DESIV = ASCIIEncoding.ASCII.GetBytes(CryptIV);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, des.CreateEncryptor(DESKey, DESIV), CryptoStreamMode.Write);
            CStream.Write(textOut, 0, textOut.Length);
            CStream.FlushFinalBlock();
            StringBuilder StrRes = new StringBuilder();
            foreach (byte Byte in MStream.ToArray())
            {
                StrRes.AppendFormat("{0:x2}", Byte);
            }
            return StrRes.ToString();
        }

        /// <summary>
        /// 解密  DES
        /// </summary>
        /// <param name="CryptText">加密后的字符串</param>
        /// <param name="CryptKey">密匙8位字符</param>
        /// <param name="CryptIV">对称算法初始化向量8位字符</param>
        /// <returns>解密后字符串</returns>
        public static string DESDecrypt(string CryptText, string CryptKey, string CryptIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] textOut = new byte[CryptText.Length / 2];
            for (int Count = 0; Count < CryptText.Length; Count += 2)
            {
                textOut[Count / 2] = (byte)(Convert.ToInt32(CryptText.Substring(Count, 2), 16));
            }
            byte[] DESKey = ASCIIEncoding.ASCII.GetBytes(CryptKey);
            byte[] DESIV = ASCIIEncoding.ASCII.GetBytes(CryptIV);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, des.CreateDecryptor(DESKey, DESIV), CryptoStreamMode.Write);
            CStream.Write(textOut, 0, textOut.Length);
            CStream.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(MStream.ToArray());
        }
    }
}
