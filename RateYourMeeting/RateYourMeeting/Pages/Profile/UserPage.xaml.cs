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
        public UserPage()
        {
            InitializeComponent();
        }

        private void LoadMeeting()
        {
            MainControl.SyncMeetingList();

            List<Meeting> meetings =MainControl.GetMeetingList();
            listBoxMeeting.Items.Clear();

            for (int i = 0; i < meetings.Count; i++)
            {
                Meeting data = meetings.ElementAt(i);
                ListBoxItem item = new ListBoxItem();
                item.Content = String.Format("{0} - {1}", data.MeetDate.ToString("dd-MM-yyyy HH:mm"), data.Fullname); ;
                listBoxMeeting.Items.Add(item);
            }
        }
    }
}
