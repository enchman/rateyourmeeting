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
using System.Windows.Shapes;

namespace RateYourMeeting
{
    /// <summary>
    /// Interaction logic for AcceptPopup.xaml
    /// </summary>
    public partial class AcceptPopup : Window
    {
        private int EmployeeId = 0;

        public AcceptPopup()
        {
            InitializeComponent();
        }

        public AcceptPopup(int id)
        {
            InitializeComponent();
            this.EmployeeId = id;
            comboBoxMinute.SelectedIndex = 0;
            comboBoxHour.SelectedIndex = 3;
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = dateMeeting.SelectedDate;
            if(date != null)
            {
                DateTime time = Convert.ToDateTime(date);
                int hour = Convert.ToInt32(comboBoxHour.SelectedValue);
                int minute = Convert.ToInt32(comboBoxMinute.SelectedValue);
                time.AddHours(hour);
                time.AddMinutes(minute);
                string timestamp = time.ToString("yyyy-MM-dd HH:mm:ss");
                labelTitle.Content = timestamp;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
