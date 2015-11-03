using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;

namespace RateYourMeeting
{
    class User
    {
        public int Id = 0;
        public string Username;
        public string Password;
        public string Firstname;
        public string Lastname;
        public Status Type;
        public enum Status { Customer, Employee}

        private static string saltWord = "ap21kdGzc6wV1L0xdaA1";
        private static byte[] saltByte;
        private static string keyWord = "dK2d0k0oFmc57oDmlLmM";
        private static byte[] keyByte;

        /// <summary>
        /// Salt security
        /// </summary>
        public static byte[] Salt
        {
            get
            {
                // Check if saltByte is initialize
                if (saltByte == null)
                {
                    // Computing Hash function convert saltWord to saltByte
                    // For easier use for AES encryption
                    SHA256Managed sha = new SHA256Managed();
                    saltByte = sha.ComputeHash(Encoding.ASCII.GetBytes(saltWord));
                }
                return saltByte;
            }
            set
            {
                // Convert input to SHA256 byte array (32 byte)
                SHA256Managed sha = new SHA256Managed();
                saltByte = sha.ComputeHash(value);
            }
        }

        /// <summary>
        /// Key for encrypt and decrypt process
        /// </summary>
        public static byte[] Key
        {
            get
            {
                if (keyByte == null)
                {
                    // Computing Hash function convert keyWord to keyWord
                    // For easier use for AES encryption
                    SHA256Managed sha = new SHA256Managed();
                    keyByte = sha.ComputeHash(Encoding.ASCII.GetBytes(keyWord));
                }
                return keyByte;
            }
            set
            {
                // Convert input to SHA256 byte array (32 byte)
                SHA256Managed sha = new SHA256Managed();
                keyByte = sha.ComputeHash(value);
            }
        }

        public User()
        {
            
        }

        public User(string username, string password, string firstname, string lastname)
        {
            CheckUsername(username);
            CheckPassword(password);
            CheckFirstname(firstname);
            CheckLastname(lastname);
        }

        public User(int id, string username, string password, string firstname, string lastname)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Firstname = firstname;
            this.Lastname = lastname;
        }

        public User(MySqlDataReader data)
        {
            if(data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    FillData(data.GetName(i), data.GetValue(i));
                }
            }
        }

        #region User action

        public bool Login(string username, string password)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("username", username);
                Database DB = new Database("CALL `getUser`(@username)", param);

                Dictionary<string, object> result = DB.Fetch();
                DB.Done();

                if(result.Count != 0 && ComparePassword(password, result["password"].ToString()))
                {
                    FillData(result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public void Logout()
        {
            this.Id = 0;
        }

        public bool CreateUser()
        {
            // Check if password is compatible
            string base64 = BuildPassword(this.Password);

            if (ComparePassword(this.Password, base64) == true)
            {
                if (Type == Status.Employee)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("username", this.Username);
                    param.Add("password", base64);
                    param.Add("firstname", this.Firstname);
                    param.Add("lastname", this.Lastname);

                    new Database("CALL `addEmployee`(@username, @password, @firstname, @lastname)", param).Done();
                    return true;
                }
                else
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("username", this.Username);
                    param.Add("password", base64);
                    param.Add("firstname", this.Firstname);
                    param.Add("lastname", this.Lastname);

                    new Database("CALL `addCustomer`(@username, @password, @firstname, @lastname)", param).Done();
                    return true;
                }
            }
            else
            {
                return false;
            }

            
        }

        public void EditUser()
        {

        }

        public void RemoveUser()
        {

        }

        private void FillData(Dictionary<string, object> items)
        {
            object temp = null;
            // Set ID
            items.TryGetValue("id", out temp);
            this.Id = Convert.ToInt32(temp);
            // Set Username
            items.TryGetValue("username", out temp);
            this.Username = temp.ToString();
            // Set Username
            items.TryGetValue("password", out temp);
            this.Password = temp.ToString();
            // Set Username
            items.TryGetValue("firstname", out temp);
            this.Firstname = temp.ToString();
            // Set Username
            items.TryGetValue("lastname", out temp);
            this.Lastname = temp.ToString();
            // Set Username
            items.TryGetValue("state", out temp);
            this.Type = temp.ToString() == "0" ? Status.Customer : Status.Employee;
        }

        private void FillData(string prop, object value)
        {
            if (prop == "username")
            {
                this.Id = int.Parse(value.ToString());
            }
            else if (prop == "username")
            {
                this.Username = value.ToString();
            }
            else if (prop == "password")
            {
                this.Password = value.ToString();
            }
            else if (prop == "fistname")
            {
                this.Firstname = value.ToString();
            }
            else if (prop == "lastname")
            {
                this.Lastname = value.ToString();
            }
            else if (prop == "state")
            {
                this.Type = value.ToString() == "0" ? Status.Customer : Status.Employee;
            }
        }
        #endregion

        #region User input filter

        private void CheckUsername(string username)
        {
            if (username.Length >= 2 && username.Length <= 255)
            {
                this.Username = username;
            }
            else
            {
                throw new ArgumentException("Username is out of length");
            }
        }

        private void CheckPassword(string password)
        {
            if(password.Length >= 4)
            {
                this.Password = password;
            }
            else
            {
                throw new ArgumentException("Password is too short");
            }
        }

        private void CheckFirstname(string firstname)
        {
            if(firstname.Length > 1 && firstname.Length < 256)
            {
                this.Firstname = firstname;
            }
            else
            {
                throw new ArgumentException("First name is out of length");
            }
        }

        private void CheckLastname(string lastname)
        {
            if (lastname.Length > 1 && lastname.Length < 256)
            {
                this.Firstname = lastname;
            }
            else
            {
                throw new ArgumentException("Last name is out of length");
            }
        }
        #endregion

        #region Encryption utility
        /// <summary>
        /// Create Encrypted Password
        /// </summary>
        /// <param name="passphrase">Plaintext Password</param>
        /// <returns>Password string as Base64</returns>
        public string BuildPassword(string passphrase)
        {
            HMACSHA256 sha = new HMACSHA256(Salt);
            byte[] password = sha.ComputeHash(Encoding.Unicode.GetBytes(passphrase));
            string raw = Encoding.Unicode.GetString(password);

            return Convert.ToBase64String(Encipher(raw, Key));
        }

        /// <summary>
        /// Compare Client Password and Internal Password
        /// </summary>
        /// <param name="pass">Plaintext Password</param>
        /// <param name="base64">Encrypted Password</param>
        /// <returns>Matched result</returns>
        public bool ComparePassword(string pass, string base64)
        {
            // Computing hash with salt
            HMACSHA256 sha = new HMACSHA256(Salt);
            byte[] passByte = sha.ComputeHash(Encoding.Unicode.GetBytes(pass));

            // Convert passByte to string (Readable data)
            string password = Encoding.Unicode.GetString(passByte);

            // Convert Base64 to raw data (byte array)
            byte[] raw = Convert.FromBase64String(base64);
            // Decrypting password
            string data = Decipher(raw, Key);

            // Compare two string
            return data == password;
        }
        #endregion

        #region Encryption Section
        /// <summary>
        /// Decrypting data
        /// </summary>
        /// <param name="input">RAW password (Byte array)</param>
        /// <param name="pass">Plaintext Key word</param>
        /// <returns>Plaintext data</returns>
        private string Decrypt(byte[] input, string pass)
        {
            return Decipher(input, GenerateKey(pass));
        }

        /// <summary>
        /// Decrypting data
        /// </summary>
        /// <param name="input">Base64 password</param>
        /// <param name="pass">Plaintext Key word</param>
        /// <returns>Plaintext data</returns>
        private string Decrypt(string input, string pass)
        {
            byte[] data = Convert.FromBase64String(input);
            return Decipher(data, GenerateKey(pass));
        }

        /// <summary>
        /// Decrypting data
        /// </summary>
        /// <param name="input">RAW password (Byte array)</param>
        /// <param name="pass">Plaintext Key word</param>
        /// <returns>Plaintext data</returns>
        public string Decrypt(byte[] input, byte[] pass)
        {
            return Decipher(input, pass);
        }

        /// <summary>
        /// Decrypting data
        /// </summary>
        /// <param name="input">Base64 password</param>
        /// <param name="pass">Plaintext Key word</param>
        /// <returns>Plaintext data</returns>
        public string Decrypt(string input, byte[] pass)
        {
            byte[] data = Convert.FromBase64String(input);
            return Decipher(data, pass);
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="input">Plaintext</param>
        /// <param name="pass">Plaintext Key word</param>
        /// <returns>Encrypted data (Byte array)</returns>
        private byte[] Encrypt(string input, string pass)
        {
            return Encipher(input, GenerateKey(pass));
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="input">Plaintext</param>
        /// <param name="pass">Plaintext Key word</param>
        /// <returns>Encrypted data (Byte array)</returns>
        public byte[] Encrypt(string input, byte[] pass)
        {
            return Encipher(input, pass);
        }

        /// <summary>
        /// Generate Key
        /// </summary>
        /// <param name="pass">Key word</param>
        /// <returns>Key (byte array)</returns>
        private byte[] GenerateKey(string pass)
        {
            SHA256Managed sha = new SHA256Managed();
            return sha.ComputeHash(Encoding.Unicode.GetBytes(pass));
        }

        /// <summary>
        /// Encryption Process
        /// </summary>
        /// <param name="input">Data (Byte array)</param>
        /// <param name="key">Key (Byte array)</param>
        /// <returns>Encrypted data (Byte array)</returns>
        private byte[] Encipher(string input, byte[] key)
        {
            // Prepare data
            byte[] data;
            byte[] raw;

            // Encryption RijndaelManaged (AES 128)
            using (RijndaelManaged rij = new RijndaelManaged())
            {
                // Prepare Rijndael data
                rij.Key = key;
                rij.Mode = CipherMode.CBC;
                rij.GenerateIV();

                ICryptoTransform process = rij.CreateEncryptor(rij.Key, rij.IV);

                // Create Memory Stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create Cryptography Stream
                    using (CryptoStream cs = new CryptoStream(ms, process, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            //Write all data to the stream.
                            sw.Write(input);
                        }
                        data = ms.ToArray();
                    }
                }
                raw = new byte[rij.IV.Length + data.Length];
                Buffer.BlockCopy(rij.IV, 0, raw, 0, rij.IV.Length);
                Buffer.BlockCopy(data, 0, raw, rij.IV.Length, data.Length);
            }

            return raw;
        }

        /// <summary>
        /// Decryption Process
        /// </summary>
        /// <param name="input">Encrypted data (Byte array)</param>
        /// <param name="key">Key (Byte array)</param>
        /// <returns>Original data</returns>
        private string Decipher(byte[] input, byte[] key)
        {
            // Prepare data
            string text = null;

            // Rijndael Process
            using (RijndaelManaged rij = new RijndaelManaged())
            {
                // Extract Data and Initialized Vector
                int realLength = input.Length - 16;
                byte[] iv = new byte[16];
                byte[] data = new byte[realLength];
                Buffer.BlockCopy(input, 0, iv, 0, 16);
                Buffer.BlockCopy(input, 16, data, 0, realLength);

                // Prepare data
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
        #endregion
    }
}
