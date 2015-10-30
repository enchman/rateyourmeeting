using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace RateYourMeeting
{
    class Login
    {

        public Login()
        {

        }

        public Login(string username, string password)
        {

        }

        public string BuildPassword(string password)
        {
            Encrypt(password);
        }

        private byte[] Encrypt(string input, string pass)
        {
            byte[] data;
            byte[] raw;
            using (RijndaelManaged rij = new RijndaelManaged())
            {
                SHA256Managed sha = new SHA256Managed();
                rij.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
                rij.Mode = CipherMode.CBC;
                rij.GenerateIV();
                ICryptoTransform process = rij.CreateEncryptor(rij.Key, rij.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, process, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(input);
                        }
                        data = ms.ToArray();
                    }
                }
                byte[] result = new byte[rij.IV.Length + data.Length];
                Buffer.BlockCopy(rij.IV, 0, result, 0, rij.IV.Length);
                Buffer.BlockCopy(data, 0, result, rij.IV.Length, data.Length);
                raw = result;
            }

            return raw;
        }

        private string Decrypt(byte[] input, string pass)
        {
            return Decipher(input, pass);
        }

        private string Decrypt(string input, string pass)
        {
            byte[] data = Convert.FromBase64String(input);
            return Decipher(data, pass);
        }

        private string Decipher(byte[] input, string passphrase)
        {
            string text = null;

            using (RijndaelManaged rij = new RijndaelManaged())
            {
                int realLength = input.Length - 16;
                SHA256Managed sha = new SHA256Managed();
                byte[] key = sha.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
                byte[] iv = new byte[16];
                byte[] data = new byte[realLength];
                Buffer.BlockCopy(input, 0, iv, 0, 16);
                Buffer.BlockCopy(input, 16, data, 0, realLength);
                rij.Mode = CipherMode.CBC;
                rij.Key = key;
                rij.IV = iv;
                ICryptoTransform process = rij.CreateDecryptor(rij.Key, rij.IV);

                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, process, CryptoStreamMode.Read))
                    {
                        using (StreamReader sw = new StreamReader(cs))
                        {
                            text = sw.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }
    }
}
