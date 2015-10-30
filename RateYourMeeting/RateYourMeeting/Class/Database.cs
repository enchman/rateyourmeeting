using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RateYourMeeting
{
    class Database: MySqlInfo
    {
        private static MySqlConnection DB;
        private static MySqlDataReader Result;
        public static void Connect()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(MySqlInfo.Dns());
                DB = connection;
            }
            catch (MySqlException)
            {
                // Error
            }
        }

        public static void Query(string sql)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandText = sql;
                cmd.Prepare();
                Result = cmd.ExecuteReader();
            }
            catch(Exception)
            {
                // No connection error
            }
        }

        public static void Query(string sql, Dictionary<string,object> param)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandText = sql;
                List<string> keys = new List<string>(param.Keys);
                foreach(string key in keys)
                {
                    cmd.Parameters.AddWithValue("@" + key, param[key]);
                }
                cmd.Prepare();
                Result = cmd.ExecuteReader();
            }
            catch(Exception)
            {
                // Something when wrong...
            }
        }

        public static DatabaseResult[] Fetch()
        {
            try
            {
                DatabaseResult[] data = new DatabaseResult[] { };
                for(int i = 0; Result.Read(); i++)
                {
                    for(int n = 0; n < Result.FieldCount; n++)
                    {
                        data[i].Add(Result.GetName(n), Result.GetValue(n));
                    }
                }

                Result.Close();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MySqlDataReader Data()
        {
            try
            {
                return GetResult();
            }
            catch(Exception)
            {
                // Error
                return null;
            }
        }

        private static MySqlDataReader GetResult()
        {
            if(Result != null )
            {
                return Result;
            }
            else
            {
                throw new Exception("Error: There hadn't been");
            }
        }

        private static MySqlConnection GetConnection(string sql)
        {
            if(DB != null)
            {
                return DB;
            }
            else
            {
                throw new Exception("Error: There is no connection yet!");
            }
        }

        
    }

    class DatabaseResult
    {
        private string[] Columns = new string[1];
        private object[] Values;
        private int ID = 0;

        public void Add(string column, object value)
        {
            Columns[ID] = column;
            Values[ID] = value;
            ID++;
        }

        public object Result(string column)
        {
            int index = Array.IndexOf(Columns, column);
            if (index != -1)
            {
                return Values[index];
            }
            else
            {
                return null;
            }
        }
    }

    abstract class MySqlInfo
    {
        protected static string Host = "localhost";
        protected static string Username = "meeter";
        protected static string Password = "1234567890";
        protected static string Database = "rateyourmeeting";
        protected static string Charset = "utf8";

        protected static string Dns()
        {
            return String.Format("server={0};uid={1};pwd={2};database={3};charset={4}", Host, Username, Password, Database, Charset);
        }
    }
}
