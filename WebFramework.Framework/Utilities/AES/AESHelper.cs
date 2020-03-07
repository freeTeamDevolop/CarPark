using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.Utilities.AES
{
    public class AESHelper
    {
        public AESHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private string inKey = string.Empty;

        public AESHelper(string key)
        {
            int length = key.Length;
            if (length > 16)
                key = key.Remove(16);
            else if (length < 16)
            {
                for (int i = 1; i <= 16 - length; i++)
                {
                    key += "0";
                }
            }
            inKey = key;
        }


        /// <summary>
        /// 获取密钥
        /// </summary>
        private static string Key
        {
            get
            {
                return "JuBao?@.$Encrypt";    ////必须是16位
            }
        }


        /// <summary>
        /// 获取密钥
        /// </summary>
        private static string Key256
        {
            get
            {
                return "JuBao?@.$EncryptoOpK~*&^%)([];D,";    ////必须是16位
            }
        }

        public static string Key256PreFix
        {
            get
            {
                return "IO%Jb?@.$EJy*(()JI^)";    ////必须是32位
            }
        }


        public static string Key128PreFix
        {
            get
            {
                return "IO%Jb?@.$E";    ////必须是32位
            }
        }

        /// <summary>
        /// 解密结算加密期号的密钥
        /// </summary>
        public static string Key128
        {
            get
            {
                return "IO%Jb?@.$EJy*(()";    ////必须是32位
            }
        }



        /// <summary>
        /// 获取密钥向量
        /// </summary>
        private static string Iv
        {
            get
            {

                return "HaiYuSoftOrder18";    ////必须是16位
            }
        }



        #region 成员变量
        /// <summary>
        /// 密钥(32位,不足在后面补0)
        /// </summary>
        private const string _passwd = "ihlih*0037JOHT*)(PIJY*(()JI^)IO%";
        /// <summary>
        /// 运算模式
        /// </summary>
        private static CipherMode _cipherMode = CipherMode.ECB;
        /// <summary>  PKCS7
        /// 填充模式
        /// </summary>
        private static PaddingMode _paddingMode = PaddingMode.PKCS7;
        /// <summary>
        /// 字符串采用的编码
        /// </summary>
        private static Encoding _encoding = Encoding.UTF8;
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取32byte密钥数据
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private static byte[] GetKeyArray(string password)
        {
            if (password == null)
            {
                password = string.Empty;
            }

            if (password.Length < 32)
            {
                password = password.PadRight(32, '0');
            }
            else if (password.Length > 32)
            {
                password = password.Substring(0, 32);
            }

            return _encoding.GetBytes(password);
        }

        /// <summary>
        /// 将字符数组转换成字符串
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private static string ConvertByteToString(byte[] inputData)
        {
            StringBuilder sb = new StringBuilder(inputData.Length * 2);
            foreach (var b in inputData)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串转换成字符数组
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static byte[] ConvertStringToByte(string inputString)
        {
            if (inputString == null || inputString.Length < 2)
            {
                throw new ArgumentException();
            }
            int l = inputString.Length / 2;
            byte[] result = new byte[l];
            for (int i = 0; i < l; ++i)
            {
                result[i] = Convert.ToByte(inputString.Substring(2 * i, 2), 16);
            }

            return result;
        }
        #endregion

        #region 加密

        /// <summary>
        /// 加密字节数据
        /// </summary>
        /// <param name="inputData">要加密的字节数据</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] inputData, string password)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = GetKeyArray(password);
            aes.Mode = _cipherMode;
            aes.Padding = _paddingMode;
            ICryptoTransform transform = aes.CreateEncryptor();
            byte[] data = transform.TransformFinalBlock(inputData, 0, inputData.Length);
            aes.Clear();
            return data;
        }

        /// <summary>
        /// 加密字符串(加密为16进制字符串)
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string Encrypt_android(string inputString, string passWord)
        {
            if (inputString == null)
                return null;
            try
            {
                byte[] toEncryptArray = _encoding.GetBytes(inputString);
                byte[] result = Encrypt(toEncryptArray, string.IsNullOrWhiteSpace(passWord) ? Key : passWord);
                return ConvertByteToString(result);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串加密(加密为16进制字符串)
        /// </summary>
        /// <param name="inputString">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(string inputString, string passWord = null)
        {
            if (inputString == null)
                return null;
            if (string.IsNullOrEmpty(passWord) || passWord.Trim().Length < 16)
            {
                passWord = Key;
            }
            //return Encrypt_android(inputString, passWord);
            return AESEncrypt(inputString, passWord);
        }


        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string password, string iv = "")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = _cipherMode;
            rijndaelCipher.Padding = _paddingMode;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            if (!string.IsNullOrEmpty(iv))
            {
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                rijndaelCipher.IV = new byte[16];
            }
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }


        /// <summary>
        /// 字符串加密(加密为32进制字符串)
        /// </summary>
        /// <param name="inputString">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString256(string inputString, string passWord = null)
        {
            if (inputString == null)
                return null;
            if (string.IsNullOrEmpty(passWord) || passWord.Trim().Length < 32)
            {
                passWord = Key256;
            }
            //return Encrypt_android(inputString, passWord);
            return AESEncrypt256(inputString, passWord);
        }

        /// <summary>
        /// AES加密 32位密钥
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt256(string text, string password, string iv = "")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = _cipherMode;
            rijndaelCipher.Padding = _paddingMode;
            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 256;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[32];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            if (!string.IsNullOrEmpty(iv))
            {
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                rijndaelCipher.IV = ivBytes;
            }
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        #endregion

        #region 解密

        /// <summary>
        /// 解密字节数组
        /// </summary>
        /// <param name="inputData">要解密的字节数据</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] inputData, string password)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = GetKeyArray(password);
            aes.Mode = _cipherMode;
            aes.Padding = _paddingMode;
            ICryptoTransform transform = aes.CreateDecryptor();
            byte[] data = null;
            try
            {
                data = transform.TransformFinalBlock(inputData, 0, inputData.Length);
            }
            catch
            {
                return null;
            }
            aes.Clear();
            return data;
        }

        /// <summary>
        /// 解密16进制的字符串为字符串
        /// </summary>
        /// <param name="inputString">要解密的字符串</param>
        /// <param name="password">密码</param>
        /// <returns>字符串</returns>
        public static string Decrypt_android(string inputString, string passWord)
        {
            if (inputString == null)
                return null;
            byte[] toDecryptArray = ConvertStringToByte(inputString);
            string decryptString = _encoding.GetString(Decrypt(toDecryptArray, string.IsNullOrWhiteSpace(passWord) ? Key : passWord));
            return decryptString;
        }

        /// <summary>
        /// 解密16进制的字符串为字符串
        /// </summary>
        /// <param name="inputString">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptString(string inputString, string passWord = null)
        {
            if (inputString == null)
                return null;
            if (string.IsNullOrEmpty(passWord) || passWord.Trim().Length < 16)
            {
                passWord = Key;
            }
            //return Encrypt_android(inputString, passWord);
            return AESDecrypt(inputString, passWord);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string password, string iv = "")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = _cipherMode;
            rijndaelCipher.Padding = _paddingMode;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            if (!string.IsNullOrEmpty(iv))
            {
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                rijndaelCipher.IV = ivBytes;
            }
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }



        /// <summary>
        /// 解密16进制的字符串为字符串
        /// </summary>
        /// <param name="inputString">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptString256(string inputString, string passWord = null)
        {
            if (inputString == null)
                return null;
            if (string.IsNullOrEmpty(passWord) || passWord.Trim().Length < 32)
            {
                passWord = Key256;
            }
            //return Encrypt_android(inputString, passWord);
            return AESDecrypt256(inputString, passWord);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt256(string text, string password, string iv = "")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = _cipherMode;
            rijndaelCipher.Padding = _paddingMode;
            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 256;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[32];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            if (!string.IsNullOrEmpty(iv))
            {
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                rijndaelCipher.IV = ivBytes;
            }
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        #endregion
    }
}
