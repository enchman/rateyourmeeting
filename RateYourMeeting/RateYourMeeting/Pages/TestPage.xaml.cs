using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace RateYourMeeting
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : UserControl
    {

        public TestPage()
        {
            InitializeComponent();
        }

        public static string BytetoHex(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void boxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                User item = new User();
                HMACSHA256 sha = new HMACSHA256(User.Salt);
                byte[] data = sha.ComputeHash(Encoding.Unicode.GetBytes(boxInput.Text));
                string raw = Encoding.Unicode.GetString(data);
                
                byte[] encrypt = item.Encrypt(raw, User.Key);
                string hash = BytetoHex(encrypt);
                string base64 = "CuItK4CJi7LN+7Lzq2kbef9440vYg6g/7wXchLdPc37dRi8jS9Nk2uXLrhGufubKIj6vAyekO5GQTI+bTpv7JN6UwzIzUlgzrSBoI7eoV3o=";
                byte[] block = Convert.FromBase64String(base64);
                string decrypt = item.Decrypt(block, User.Key);
                //string compare = item.ComparePassword(boxInput.Text, encrypt) ? "TRUE" : "FALSE";

                TextBlock_1.Text = raw;
                TextBlock_2.Text = decrypt;
                TextBlock_3.Text = BytetoHex(data);
                TextBlock_4.Text = BytetoHex(Encoding.Unicode.GetBytes(decrypt));
                TextBlock_5.Text = decrypt.Length.ToString();
                TextBlock_6.Text = raw.Length.ToString();

                //TextBlock_4.Text = compare;

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PageSwitch.Backward();
        }
    }
}
