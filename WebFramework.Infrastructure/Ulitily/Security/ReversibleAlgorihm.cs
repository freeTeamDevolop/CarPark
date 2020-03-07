using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Infrastructure.Ulitily.Security
{
    public static class ReversibleAlgorihm
    {
        private static readonly byte[] Key = new byte[]
        {
            33, 217, 57, 138, 102, 232, 206, 176, 7, 27, 117, 171, 123, 33, 91, 99, 67, 21, 40, 175, 184, 253, 61, 213,
            172, 225, 203, 11, 64, 177, 248, 194
        };
        private static readonly byte[] Iv = new byte[] { 246, 159, 208, 188, 228, 53, 29, 209, 10, 61, 123, 30, 175, 191, 66, 178 };
        public static string EncryptStringAes(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            string outStr;
            RijndaelManaged aesAlg = null;

            try
            {
                aesAlg = new RijndaelManaged();
                aesAlg.IV = Iv;
                aesAlg.Key = Key;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }
            return outStr;
        }

        public static string DecryptStringAes(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;
            RijndaelManaged aesAlg = null;
            string plaintext;

            try
            {
                //Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Secret, Salt);
                byte[] bytes = Convert.FromBase64String(cipherText);
                aesAlg = new RijndaelManaged();
                plaintext = InnerDecrypt(aesAlg, bytes);
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        private static string InnerDecrypt(RijndaelManaged aesAlg, byte[] bytes)
        {
            string plaintext;
            using (var msDecrypt = new MemoryStream(bytes))
            {
                aesAlg.IV = Iv;
                aesAlg.Key = Key;
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                        plaintext = srDecrypt.ReadToEnd();
                }
            }
            return plaintext;
        }
    }
}
