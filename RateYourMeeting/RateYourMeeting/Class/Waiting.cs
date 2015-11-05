using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateYourMeeting
{
    class Waiting
    {
        public int Id;
        public int Queue;
        public User Employee;

        public static List<Waiting> GetEmployeeQueue()
        {
            Database DB = new Database("CALL `getEmployeeWait`()");
            List<Dictionary<string, object>> data = DB.FetchAll();
            List<Waiting> items = new List<Waiting>();
            if(data.Count > 0)
            {
                for(int i = 0; i < data.Count; i++)
                {

                }
            }
            return items;
        }
    }
}
