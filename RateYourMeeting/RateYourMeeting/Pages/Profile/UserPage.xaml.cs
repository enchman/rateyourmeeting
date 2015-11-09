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
        private ReviewMode ViewType = ReviewMode.Latest;

        public UserPage()
        {
            InitializeComponent();
            LoadItems();
            LoadProfile();
            GetEmployees();
            GetArrangement();
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

        private void GetArrangement()
        {
            List<User> user = User.GetQueues();

            if (user.Count > 0)
            {
                for (int i = 0; i < user.Count; i++)
                {
                    GenerateQueue(user[i]);
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

        private void GenerateQueue(User user)
        {
            // Border
            Border bord = new Border();
            bord.BorderBrush = Brushes.Black;
            bord.BorderThickness = new Thickness(0, 0, 0, 1);

            // Border > Stackpanel
            StackPanel panel = new StackPanel();
            panel.Height = 20;
            panel.Orientation = Orientation.Horizontal;
            panel.MouseEnter += list_Hovering;
            panel.MouseLeave += list_Leaving;

            // Border > Stackpanel > Stackpanel
            StackPanel p1 = new StackPanel();
            p1.Width = 300;

            // Border > Stackpanel > Stackpanel > TextBlock
            TextBlock tb1 = new TextBlock();
            tb1.Padding = new Thickness(15, 0, 0, 0);
            tb1.HorizontalAlignment = HorizontalAlignment.Left;
            tb1.Cursor = Cursors.Hand;
            tb1.PreviewMouseDown += MeetingArrange_Click;
            tb1.Text = user.Fullname;
            tb1.Uid = user.Id.ToString();

            // Border > Stackpanel > Stackpanel
            StackPanel p2 = new StackPanel();
            p2.Width = 170;

            // Border > Stackpanel > Stackpanel > TextBlock
            TextBlock tb2 = new TextBlock();
            tb2.TextAlignment = TextAlignment.Right;
            tb2.Padding = new Thickness(0, 0, 10, 0);
            tb2.Text = user.Total.ToString();

            // Assembly UI
            p1.Children.Add(tb1);
            p2.Children.Add(tb2);
            panel.Children.Add(p1);
            panel.Children.Add(p2);
            bord.Child = panel;
            panelAction.Children.Add(bord);
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

        private void MeetingArrange_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock text = sender as TextBlock;
            AcceptPopup page = new AcceptPopup(int.Parse(text.Uid));
            page.Show();
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
            if (e.Key == Key.Left)
            {
                if(0 == this.tabControl.SelectedIndex)
                {
                    this.tabControl.SelectedIndex = (tabControl.Items.Count - 1);
                }
                else
                {
                    this.tabControl.SelectedIndex--;
                }
            }
            else if(e.Key == Key.Right)
            {
                if ((tabControl.Items.Count - 1) == this.tabControl.SelectedIndex)
                {
                    this.tabControl.SelectedIndex = 0;
                }
                else
                {
                    this.tabControl.SelectedIndex++;
                }
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
        private void ShowOwner_Hover(object sender, MouseEventArgs e)
        {
            TextBlock main = sender as TextBlock;
            StackPanel panel = main.Parent as StackPanel;
            TextBlock text = panel.Children[1] as TextBlock;
            text.Visibility = Visibility.Visible;
        }

        private void ShowOwner_Leave(object sender, MouseEventArgs e)
        {
            TextBlock main = sender as TextBlock;
            StackPanel panel = main.Parent as StackPanel;
            TextBlock text = panel.Children[1] as TextBlock;
            text.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
