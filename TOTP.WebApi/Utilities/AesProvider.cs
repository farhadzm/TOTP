using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TOTP.WebApi.Utilities
{
    public static class AesProvider
    {
        private static byte[] GetIV()
        {
            //این کد ثابتی است که باید در سمت سرور و موبایل موجود باشد
            return encoding.GetBytes("ThisIsASecretKey");
        }
        public static string Encrypt(string plainText, string key)
        {
            try
            {
                var aes = GetRijndael(key);
                ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] buffer = encoding.GetBytes(plainText);
                string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return encryptedText;
            }
            catch (Exception)
            {
                throw new Exception("an error occurred when encrypting");
            }
        }
        private static RijndaelManaged GetRijndael(string key)
        {
            return new RijndaelManaged
            {
                KeySize = 128,
                BlockSize = 128,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                Key = encoding.GetBytes(key),
                IV = GetIV()
            };
        }
        private static readonly Encoding encoding = Encoding.UTF8;
        public static string Decrypt(string plainText, string key)
        {
            try
            {
                var aes = GetRijndael(key);
                ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] buffer = Convert.FromBase64String(plainText);

                return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception)
            {
                throw new Exception("an error occurred when decrypting");
            }
        }
    }
}
