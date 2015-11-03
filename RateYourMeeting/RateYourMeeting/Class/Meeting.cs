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

        public int Id;
        public string Fullname;
        public DateTime MeetDate;

        public void MeetingList(ref ListBox box)
        {
            //box.Items.Clear();
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("username", MainControl.Session.Id);

            //Database DB = new Database("CALL `getCustomerMeeting`(@username)");
            //Dictionary<string, object>[] data = DB.FetchAll();

            //if(data.Length > 0)
            //{
            //    for(int i = 0; i < data.Length; i++)
            //    {
            //        data[i][""]
            //        box.Items.Add();
            //    }
            //}

            //for (int i = 0; i < games.Count; i++)
            //{
            //    Game data = games.ElementAt(i);
            //    ListBoxItem item = new ListBoxItem();
            //    item.Content = data.Name;
            //    block.Items.Add(item);
            //}
        }

        public void WaitingList(ref ListBox box)
        {
            // Checking Privilege
            if(MainControl.Session.Type == User.Status.Employee)
            {

            }
            else
            {
                throw new Exception("User doesn't have right to access this method");
            }
        }
    }
}
