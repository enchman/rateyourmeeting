using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RateYourMeeting
{
    class Meeting
    {

        public enum ReviewStatus { None, Wrote}
        public int Id;
        public User Person;
        public DateTime MeetDate;
        public ReviewStatus Status;

        public static List<Meeting> GetMeeting()
        {
            Database DB;
            if(MainControl.Session.Type == User.Status.Customer)
            {
                DB = new Database("CALL `getCustomerMeeting`(@user)");
            }
            else
            {
                DB = new Database("CALL `getEmployeeMeeting`(@user)");
            }

            List<Dictionary<string, object>> data = DB.FetchAll();
            List<Meeting> items = new List<Meeting> { };
            if(data.Count != 0)
            {
                for(int i = 0; i < data.Count; i++)
                {
                    Meeting item = new Meeting();
                    item.Id = Convert.ToInt32(data[i]["id"]);
                    item.MeetDate = Convert.ToDateTime(data[i]["meetingdate"]);
                    item.Status = data[i]["reviewstatus"].ToString() != "1" ? ReviewStatus.None : ReviewStatus.Wrote;
                    item.Person.Firstname = data[i]["firstname"].ToString();
                    item.Person.Lastname = data[i]["lastname"].ToString();
                    items.Add(item);
                }
            }
            return items;
        }

    }
}
