using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RateYourMeeting
{
    class User
    {
        public int Id = 0;
        public string Username;
        public string Password;
        public string Firstname;
        public string Lastname;
        public Status Type;
        public enum Status { Customer, Employee}
        public DateTime Register = DateTime.Now;
        private string PassText;

        public User()
        {

        }

        public User(string username, string password, string firstname, string lastname)
        {
            
        }

        public User(int id, string username, string password, string firstname, string lastname, string date)
        {

        }

        public User(int id, string username, string password, string firstname, string lastname, DateTime date)
        {

        }

        public User(MySqlDataReader data)
        {
            while(data.Read())
            {
                
            }
        }


        private void CheckUsername(string username)
        {
            if (username.Length >= 2 && username.Length <= 255)
            {
                this.Username = username;
            }
            else
            {
                throw new ArgumentException("Username is out of length");
            }
        }

        private void CheckPassword(string password)
        {
            if(password.Length >= 4)
            {
                this.Password = password;
            }
            else
            {
                throw new ArgumentException("Password is too short");
            }
        }

        private void CheckFirstname(string firstname)
        {
            if(firstname.Length > 1 && firstname.Length < 256)
            {
                this.Firstname = firstname;
            }
            else
            {
                throw new ArgumentException("First name is out of length");
            }
        }

        private void CheckLastname(string lastname)
        {
            if (lastname.Length > 1 && lastname.Length < 256)
            {
                this.Firstname = lastname;
            }
            else
            {
                throw new ArgumentException("First name is out of length");
            }
        }

        private void CheckDate(string date)
        {
            if(date.IndexOf(' ') != -1)
            {
                int year, month, day, hour, minute, second;
                string[] part = date.Split(' ');

                string[] dates = part[0].Split('-');
                string[] timer = part[1].Split(':');

                year = int.Parse(dates[0]);
                month = int.Parse(dates[1]);
                day = int.Parse(dates[2]);
                hour = int.Parse(timer[0]);
                minute = int.Parse(timer[1]);
                second = int.Parse(timer[2]);

                DateTime time = new DateTime(year, month, day, hour, minute, second);
            }
            else
            {
                int year, month = 1, day = 1;
                string[] atom = date.Split('-');

                year = int.Parse(atom[0]);
                month = int.Parse(atom[1]);
                day = int.Parse(atom[2]);

                DateTime time = new DateTime(year, month, day);
            }
        }
    }
}
