using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RateYourMeeting
{
    class Review
    {
        public enum Point {None, Approve, Disagree};
        public int Id;
        public string Content;
        public Point Rate = Point.None;
        public DateTime Publish;
        public User Owner;
        public User Employee;
        public List<Comment> Comments = new List<Comment>() { };

        public static List<Review> GetReviewByEmployee(int id)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);
            Database DB = new Database("CALL `getReviewOnEmployee`(@id)", param);
            List<Dictionary<string, object>> data = DB.FetchAll();
            return ReviewConvert(data);
        }

        public static List<Review> GetReviews()
        {
            Database DB = new Database("CALL `getReviewList`()");
            List<Dictionary<string, object>> data = DB.FetchAll();
            return ReviewConvert(data);
        }

        private static List<Review> ReviewConvert(List<Dictionary<string, object>> input)
        {
            List<Review> items = new List<Review> { };
            if(input.Count != 0)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    Review item = new Review();
                    item.Owner = new User();
                    item.Employee = new User();
                    item.Id = Convert.ToInt32(input[i]["id"]);
                    item.Content = input[i]["content"].ToString();
                    item.Rate = Point.None;
                    item.Owner.Id = Convert.ToInt32(input[i]["customerID"]);
                    item.Owner.Firstname = input[i]["customerFirstname"].ToString();
                    item.Owner.Lastname = input[i]["customerLastname"].ToString();
                    item.Employee.Id = Convert.ToInt32(input[i]["employeeID"]);
                    item.Employee.Firstname = input[i]["employeeFirstname"].ToString();
                    item.Employee.Lastname = input[i]["employeeLastname"].ToString();
                    if (input[i]["wrotedate"] != DBNull.Value)
                    {
                        item.Publish = Convert.ToDateTime(input[i]["wrotedate"]);
                    }
                }
            }
            return items;
        }
    }
}
