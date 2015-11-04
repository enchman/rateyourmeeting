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
        private MySqlConnection DB;
        private MySqlDataReader Data;
        public int Rows = 0;


        public Database(string query)
        {
            Connect();
            Query(query);
        }

        public Database(string query, Dictionary<string, object> param)
        {
            Connect();
            Query(query, param);
        }

        /// <summary>
        /// Done with SQL action
        /// Use this after finished the use of the object
        /// </summary>
        public void Done()
        {
            // Free result
            Free();
            // Close the connection
            Disconnect();
        }

        /// <summary>
        /// Success Query
        /// Cannot use Fetch after this...
        /// </summary>
        public bool Success{ get; set; }

        /// <summary>
        /// Database Connection
        /// Make connection from client to SQL server
        /// </summary>
        private void Connect()
        {
            MySqlConnection connection = new MySqlConnection(MySqlInfo.Dns());
            connection.Open();
            DB = connection;
        }

        /// <summary>
        /// SQL Query
        /// </summary>
        /// <param name="sql"></param>
        private void Query(string sql)
        {
            try
            {
                MySqlCommand com = new MySqlCommand();
                com.Connection = DB;
                com.CommandText = sql;
                com.Prepare();
                Data = com.ExecuteReader();
                Success = true;
            }
            catch
            {
                Success = false;
            }
        }

        /// <summary>
        /// SQL Query Prepared statement
        /// Improved security and prevent SQL Injection
        /// </summary>
        /// <param name="sql">Query string</param>
        /// <param name="param">Bind parameters</param>
        private void Query(string sql, Dictionary<string, object> param)
        {
            try
            {
                MySqlCommand com = new MySqlCommand();
                com.Connection = DB;
                com.CommandText = sql;
                List<string> keys = new List<string>(param.Keys);
                foreach (string key in keys)
                {
                    com.Parameters.AddWithValue("@" + key, param[key]);
                }
                com.Prepare();
                Data = com.ExecuteReader();
                Success = true;
            }
            catch
            {
                Success = false;
            }
        }

        /// <summary>
        /// Fetch 2d object array
        /// Index ONLY
        /// </summary>
        /// <returns>2d object array of SQL result</returns>
        public object[][] FetchNumbers()
        {
            object[][] items = new object[][] { };
            for(int i = 0; Data.Read(); i++)
            {
                Data.GetValues(items[i]);
            }
            Done();
            return items;
        }

        /// <summary>
        /// Fetch object array
        /// Index ONLY
        /// </summary>
        /// <returns>object array of SQL result</returns>
        public object[] FetchSingle()
        {
            object[] items = new object[] { };
            if(Data.Read())
            {
                Data.GetValues(items);
            }
            Done();
            return items;
        }

        /// <summary>
        /// Fetch all 
        /// Combine List and associative array
        /// </summary>
        /// <returns>Multidimensional Dictionary array of SQL result</returns>
        public List<Dictionary<string, object>> FetchAll()
        {
            List<Dictionary<string, object>> items = new List<Dictionary<string, object>> { };
            while (Data.Read())
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                for (int i = 0; i < Data.FieldCount; i++)
                {
                    item.Add(Data.GetName(i), Data.GetValue(i));
                }
                items.Add(item);
            }
            Done();
            return items;
        }

        /// <summary>
        /// Just Fetch 
        /// Combine index and associative array
        /// </summary>
        /// <returns>Dictionary array of SQL result</returns>
        public Dictionary<string, object> Fetch()
        {
            Dictionary<string, object> item = new Dictionary<string, object>();
            if(Data.Read())
            {
                for (int i = 0; i < Data.FieldCount; i++)
                {
                    item.Add(Data.GetName(i), Data.GetValue(i));
                }
            }
            Done();

            return item;
        }

        /// <summary>
        /// Free results
        /// </summary>
        private void Free()
        {
            // Check if Result is exist and still open
            if (this.Data != null && !this.Data.IsClosed)
            {
                this.Data.Close();
            }
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        private void Disconnect()
        {
            if(this.DB != null)
            {
                this.DB.Close();
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
        protected static string Port = "3306";

        protected static string Dns()
        {
            if(Port != null)
            {
                return String.Format("server={0};port={1};uid={2};pwd={3};database={4};charset={5}", Host, Port, Username, Password, Database, Charset);
            }
            else
            {
                return String.Format("server={0};uid={1};pwd={2};database={3};charset={4}", Host, Username, Password, Database, Charset);
            }
        }
    }
}
