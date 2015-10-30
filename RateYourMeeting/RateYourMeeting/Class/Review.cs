using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Comment> Comments = new List<Comment>() { };
    }
}
