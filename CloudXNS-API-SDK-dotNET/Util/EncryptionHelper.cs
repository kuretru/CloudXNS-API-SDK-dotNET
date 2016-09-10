using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kuretru.CloudXNSAPI.Util
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public static class EncryptionHelper
    {
        /// <summary>
        /// 获取字符串的MD5值
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <returns>加密后的MD5值</returns>
        public static string GetMD5Hash(string text)
        {
            byte[] result = Encoding.Default.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string encryptResult = BitConverter.ToString(output).Replace("-", "");
            return encryptResult.ToLower();
        }
    }
}
