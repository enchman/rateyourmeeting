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
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : UserControl
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void buttonSignup_Click(object sender, RoutedEventArgs e)
        {
            User data = new User();
            bool[] status = {false, false, false, false};

            if (boxUsername.Text.Length > 2)
            {
                data.Username = boxUsername.Text;
                labelUsername.Foreground = Brushes.Black;
                status[0] = true;
            }
            else
            {
                labelUsername.Foreground = Brushes.OrangeRed;
                status[0] = false;
            }

            if (boxPassword.Password.Length > 0 && boxPassword.Password == boxRepassword.Password)
            {
                data.Password = boxPassword.Password;
                labelPassword.Foreground = Brushes.Black;
                labelRepassword.Foreground = Brushes.Black;
                status[1] = true;
            }
            else
            {
                labelPassword.Foreground = Brushes.OrangeRed;
                labelRepassword.Foreground = Brushes.OrangeRed;
                status[1] = false;
            }

            if (boxFirstname.Text.Length > 1)
            {
                data.Firstname = boxFirstname.Text;
                labelFirstname.Foreground = Brushes.Black;
                status[2] = true;
            }
            else
            {
                labelFirstname.Foreground = Brushes.OrangeRed;
                status[2] = false;
            }

            if (boxLastname.Text.Length > 1)
            {
                data.Lastname = boxLastname.Text;
                labelLastname.Foreground = Brushes.Black;
                status[3] = true;
            }
            else
            {
                labelLastname.Foreground = Brushes.OrangeRed;
                status[3] = false;
            }

            if(radioCustomer.IsChecked == true)
            {
                data.Type = User.Status.Customer;
            }
            else
            {
                data.Type = User.Status.Employee;
            }

            // Final check before process user register
            if(status.All(x => x == true))
            {
                // Check if user is already exist
                if(data.CreateUser())
                {
                    labelUsername.Foreground = Brushes.Black;
                    PageSwitch.Forward(new LoginPage(boxUsername.Text));
                }
                else
                {
                    labelUsername.Foreground = Brushes.OrangeRed;
                }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            PageSwitch.Backward();
        }
    }
}
