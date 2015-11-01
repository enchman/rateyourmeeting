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
        private static string saltWord = "ap21kdGzc6wV1L0xdaA1";
        private static byte[] saltByte;
        private static string keyWord = "dK2d0k0oFmc57oDmlLmM";
        private static byte[] keyByte;

        public static byte[] Salt
        {
            get
            {
                if(saltByte == null)
                {
                    SHA256Managed sha = new SHA256Managed();
                    saltByte = sha.ComputeHash(Encoding.ASCII.GetBytes(saltWord));
                }
                return saltByte;
            }
            set
            {
                SHA256Managed sha = new SHA256Managed();
                saltByte = sha.ComputeHash(value);
            }
        }

        public static byte[] Key
        {
            get
            {
                if (keyByte == null)
                {
                    SHA256Managed sha = new SHA256Managed();
                    keyByte = sha.ComputeHash(Encoding.ASCII.GetBytes(keyWord));
                }
                return keyByte;
            }
            set
            {
                SHA256Managed sha = new SHA256Managed();
                keyByte = sha.ComputeHash(value);
            }
        }

        public Login()
        {
            
        }

        public Login(string username, string password)
        {

        }

        public string BuildPassword(string passphrase)
        {
            HMACSHA256 sha = new HMACSHA256(Salt);
            byte[] password  = sha.ComputeHash(Encoding.UTF8.GetBytes(passphrase));

            return Convert.ToBase64String(Encrypt(password, Key));
        }

        public bool ComparePassword(string pass, string base64)
        {
            HMACSHA256 sha = new HMACSHA256(Salt);
            byte[] passByte = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));

            byte[] raw = Convert.FromBase64String(base64);
            string data = Decipher(raw, Key);
            string password = Encoding.UTF8.GetString(passByte);

            return data == password;
        }

        private string Decrypt(byte[] input, string pass)
        {
            return Decipher(input, GenerateKey(pass));
        }
        private string Decrypt(string input, string pass)
        {
            byte[] data = Convert.FromBase64String(input);
            return Decipher(data, GenerateKey(pass));
        }
        private string Decrypt(byte[] input, byte[] pass)
        {
            return Decipher(input, pass);
        }
        private string Decrypt(string input, byte[] pass)
        {
            byte[] data = Convert.FromBase64String(input);
            return Decipher(data, pass);
        }

        private byte[] Encrypt(string input, string pass)
        {
            return Encipher(Encoding.UTF8.GetBytes(input), GenerateKey(pass));
        }
        private byte[] Encrypt(byte[] input, string pass)
        {
            return Encipher(input, GenerateKey(pass));
        }
        private byte[] Encrypt(string input, byte[] pass)
        {
            return Encipher(Encoding.UTF8.GetBytes(input), pass);
        }
        private byte[] Encrypt(byte[] input, byte[] pass)
        {
            return Encipher(input, pass);
        }

        private byte[] GenerateKey(string pass)
        {
            SHA256Managed sha = new SHA256Managed();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
        }

        private byte[] Encipher(byte[] input, byte[] key)
        {
            byte[] data;
            byte[] raw;
            using (RijndaelManaged rij = new RijndaelManaged())
            {
                rij.Key = key;
                rij.Mode = CipherMode.CBC;
                rij.GenerateIV();
                ICryptoTransform process = rij.CreateEncryptor(rij.Key, rij.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, process, CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                        cs.FlushFinalBlock();
                        data = ms.ToArray();
                    }
                }
                raw = new byte[rij.IV.Length + data.Length];
                Buffer.BlockCopy(rij.IV, 0, raw, 0, rij.IV.Length);
                Buffer.BlockCopy(data, 0, raw, rij.IV.Length, data.Length);
            }

            return raw;
        }

        private string Decipher(byte[] input, byte[] key)
        {
            string text = null;

            using (RijndaelManaged rij = new RijndaelManaged())
            {
                int realLength = input.Length - 16;
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
