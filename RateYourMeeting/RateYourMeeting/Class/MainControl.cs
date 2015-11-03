using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RateYourMeeting
{
    class MainControl
    {
        // User session
        public static User Session;

        // Meeting list
        private static List<Meeting> MeetlingList = new List<Meeting>() { };

        // Waiting list
        private static List<Waiting> WaitingList = new List<Waiting>() { };

        // Review list
        private static List<Review> ReviewList = new List<Review>() { };

        // Comment list
        private static List<Comment> CommentList = new List<Comment>() { };

        // Employee list
        private static List<User> UserList = new List<User>() { };

        public static List<Meeting> GetMeetingList()
        {
            return MeetlingList;
        }

        public static List<Waiting> GetWaitlingList()
        {
            if(Session.Type == User.Status.Employee)
            {
                return WaitingList;
            }
            else
            {
                throw new Exception("Access Denied, User must be employee before access this method");
            }
        }

        public static List<User> GetEmployeeList()
        {
            return UserList;
        }

        public static List<Review> GetReviewList()
        {
            return ReviewList;
        }

        public static List<Comment> GetCommentList()
        {
            return CommentList;
        }



        public static void SyncMeetingList()
        {
            // Determinate resources
            MeetlingList.Clear();

            // Do SQL process
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", Session.Id);
            Database DB = new Database("CALL `getCustomerMeeting` (@id)");

            // Fetching data
            Dictionary<string, object>[] data = DB.FetchAll();
            if(data.Length > 0)
            {
                for(int i = 0; i < data.Length; i++)
                {
                    // Extract data to meeting list
                    Meeting meet = new Meeting();
                    meet.Id = Convert.ToInt32(data[i]["id"]);
                    meet.Fullname = data[i]["fullname"].ToString();
                    meet.MeetDate = Convert.ToDateTime(data[i]["meetingdate"]);
                    MeetlingList.Add(meet);
                }
            }
        }
        public static void SyncWaitingList()
        {

        }
        public static void SyncEmployeeList()
        {

        }

        public static void SyncReviewList()
        {

        }

        public static void SyncCommentList()
        {

        }

    }
}
