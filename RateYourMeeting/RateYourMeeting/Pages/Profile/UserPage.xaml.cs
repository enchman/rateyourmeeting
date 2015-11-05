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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : UserControl
    {
        public enum ReviewMode { Latest, ByEmployee}
        

        public UserPage()
        {
            InitializeComponent();
            LoadItems();
            LoadProfile();
            GetEmployees();
        }

        private void GetEmployees()
        {
            List<User> users = User.GetEmployees();
            
            if(users.Count != 0)
            {
                foreach(User item in users)
                {
                    GenerateEmployeeStackPanel(item);
                }
            }
        }

        private void GenerateEmployeeStackPanel(User user)
        {
            // Parent Stackpanel
            Border bord = new Border();
            bord.BorderBrush = Brushes.Black;
            bord.BorderThickness = new Thickness(0, 0, 0, 1);

            // Parent Stackpanel
            StackPanel panel = new StackPanel();
            panel.Height = 20;
            panel.Orientation = Orientation.Horizontal;
            panel.MouseEnter += list_Hovering;
            panel.MouseLeave += list_Leaving;

            // 1. Child @ 1. Stackpanel
            StackPanel p1 = new StackPanel();
            p1.Width = 250;
            // 2. Child @ 1. TextBlock
            TextBlock txtName = new TextBlock();
            txtName.HorizontalAlignment = HorizontalAlignment.Left;
            txtName.Padding = new Thickness(15, 0, 0, 0);
            txtName.Cursor = Cursors.Hand;
            txtName.Text = user.Fullname;
            txtName.Uid = user.Id.ToString();

            // 1. Child @ 2. Stackpanel
            StackPanel p2 = new StackPanel();
            p2.Width = 130;
            // 2. Child @ 2. TextBlock
            TextBlock txtDate = new TextBlock();
            txtDate.HorizontalAlignment = HorizontalAlignment.Center;
            txtDate.Text = user.GetDate;

            //  Text="1" TextAlignment="Right" Padding="0,0,10,0" 
            StackPanel p3 = new StackPanel();
            p3.Width = 90;
            // 2. Child @ 2. TextBlock
            TextBlock txtTotal = new TextBlock();
            txtTotal.TextAlignment = TextAlignment.Right;
            txtTotal.Padding = new Thickness(0, 0, 10, 0);
            txtTotal.Text = user.Total.ToString();

            // Attaching Elements
            p1.Children.Add(txtName);
            p2.Children.Add(txtDate);
            p3.Children.Add(txtTotal);
            panel.Children.Add(p1);
            panel.Children.Add(p2);
            panel.Children.Add(p3);
            bord.Child = panel;
            panelEmployee.Children.Add(bord);
        }

        private void GetMeetings()
        {

        }


        private void LoadEmployeeReviews()
        {

        }

        #region Data loader

        private void LoadItems()
        {
            if (MainControl.Session.Type == User.Status.Customer)
            {
                tabAction.Header = "Arrange a meeting";
                // Change UI
                
            }
            

            
        }

        private void LoadMeeting()
        {
            //MainControl.SyncMeetingList();

            //List<Meeting> meetings =MainControl.GetMeetingList();
            //listBoxMeeting.Items.Clear();

            //for (int i = 0; i < meetings.Count; i++)
            //{
            //    Meeting data = meetings.ElementAt(i);
            //    ListBoxItem item = new ListBoxItem();
            //    item.Content = String.Format("{0} - {1}", data.MeetDate.ToString("dd-MM-yyyy HH:mm"), data.Fullname); ;
            //    listBoxMeeting.Items.Add(item);
            //}
        }

        private void LoadProfile()
        {
            labelFullname.Content = MainControl.FulllName;
        }
        #endregion

        #region User Event
        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            MainControl.Session = null;
            PageSwitch.Forward(new LoginPage());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ViewComment com = new ViewComment();
            com.Show();
        }

        private void list_Hovering(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            panel.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#B9DCFF"));
        }

        private void list_Leaving(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            panel.Background = Brushes.Transparent;
        }
        private void Employee_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock text = sender as TextBlock;
            
        }

        private void MeetingReview_Click(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                TextBlock text = sender as TextBlock;
                WriteReview page = new WriteReview();
                page.Show();
            }
        }
        private void buttonViewComment_Click(object sender, RoutedEventArgs e)
        {
            ViewComment page = new ViewComment();
            page.Show();
        }
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {

            }
        }
        private void Star_Hover(object sender, MouseEventArgs e)
        {
            TextBlock star = sender as TextBlock;
            star.Foreground = Brushes.Cyan;
        }
        private void Star_Leave(object sender, MouseEventArgs e)
        {
            TextBlock star = sender as TextBlock;
            star.Foreground = Brushes.Gray;
        }
        #endregion
    }
}
