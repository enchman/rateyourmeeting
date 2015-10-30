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

namespace RateYourMeeting
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();

            
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            //string key = "Hello world";
            byte[] raw = User.Encrypt(boxUsername.Text, boxPassword.Password);

            try
            {
                string result = User.Decrypt(raw, boxPassword.Password);
                this.labelTitle.Content = result;
            }
            catch (Exception exc)
            {
                this.labelTitle.Content = exc.Message;
            }
        }
    }
}
