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
using MySql.Data.MySqlClient;

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
            if(MainControl.Session != null)
            {
                PageSwitch.Forward(new UserPage());
            }
        }
        public LoginPage(string username)
        {
            InitializeComponent();
            this.boxUsername.Text = username;
        }
        public LoginPage(string username, string password)
        {
            InitializeComponent();
            this.boxUsername.Text = username;
            this.boxPassword.Password = password;
        }

        /// <summary>
        /// Do Login process
        /// when event is trigged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            User data = new User();

            if (data.Login(this.boxUsername.Text, this.boxPassword.Password))
            {
                MainControl.Session = data;
                SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                labelUsername.Foreground = color;
                labelPassword.Foreground = color;
                PageSwitch.Forward(new UserPage());
            }
            else
            {
                SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EE5555"));
                labelUsername.Foreground = color;
                labelPassword.Foreground = color;
            }
        }

        private void buttonSignup_Click(object sender, RoutedEventArgs e)
        {
            PageSwitch.Forward(new SignupPage());
        }

        private void box_KeyUp(object sender, KeyEventArgs e)
        {
            boxFilter();
        }

        /// <summary>
        /// TextBox filter
        /// Prevent Client to login the fields are empty
        /// </summary>
        private void boxFilter()
        {
            if(this.boxUsername.Text.Length > 2 && this.boxPassword.Password.Length > 0)
            {
                this.buttonLogin.IsEnabled = true;
            }
            else
            {
                this.buttonLogin.IsEnabled = false;
            }
        }
    }
}
