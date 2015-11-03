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
        public bool Success
        {
            get
            {
                if(Data != null && Data.Read())
                {
                    Data.Close();
                    return true;
                }
                else
                {
                    return false;
                }
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
            Data.Close();
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
            Data.Close();
            return items;
        }

        /// <summary>
        /// Fetch all 
        /// Combine index and associative array
        /// </summary>
        /// <returns>Multidimensional Dictionary array of SQL result</returns>
        public Dictionary<string,object>[] FetchAll()
        {
            Dictionary<string, object>[] items = new Dictionary<string, object>[] { };
            int n = 0;
            while(Data.Read())
            {
                for(int i = 0; i < Data.FieldCount; i++)
                {
                    items[n].Add(Data.GetName(i), Data.GetValue(i));
                }
                n++;
            }

            Data.Close();
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
            Data.Close();

            return item;
        }

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
            }
            catch (Exception)
            {
                // SQL Error
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
            }
            catch(Exception)
            {
                // SQL Error
            }
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

    #region Old Database
    //class Database: MySqlInfo
    //{
    //    // Database Connection
    //    private static MySqlConnection DB;
    //    // Database Result
    //    private static MySqlDataReader Result;
    //    // Number of results
    //    public static int Num = 0;

    //    /// <summary>
    //    /// Database Connection
    //    /// </summary>
    //    public static void Connect()
    //    {
    //        try
    //        {
    //            MySqlConnection connection = new MySqlConnection(MySqlInfo.Dns());
    //            connection.Open();
    //            DB = connection;
    //        }
    //        catch (MySqlException)
    //        {
    //            // Error
    //        }
    //    }

    //    public static void Reconnect()
    //    {
    //        try
    //        {
    //            if (DB == null || DB.State == System.Data.ConnectionState.Closed)
    //            {
    //                MySqlConnection connection = new MySqlConnection(MySqlInfo.Dns());
    //                connection.Open();
    //                DB = connection;
    //            }
    //        }
    //        catch (MySqlException)
    //        {
    //            // Error
    //        }
    //    }

    //    /// <summary>
    //    /// Disconnect current database sconnection
    //    /// </summary>
    //    public static void Disconnect()
    //    {
    //        if(DB != null)
    //        {
    //            DB.Close();
    //        }
    //    }

    //    /// <summary>
    //    /// Database query
    //    /// </summary>
    //    /// <param name="sql">SQL command string</param>
    //    public static void Query(string sql)
    //    {
    //        try
    //        {
    //            // Free result if it 's not close
    //            Free();

    //            // Prepare statement & execute query
    //            MySqlCommand cmd = new MySqlCommand();
    //            cmd.Connection = DB;
    //            cmd.CommandText = sql;
    //            cmd.Prepare();
    //            Result = cmd.ExecuteReader();
    //        }
    //        catch(Exception)
    //        {
    //            // No connection error
    //        }
    //        finally
    //        {
    //            Reconnect();
    //            // Free result if it 's not close
    //            Free();

    //            // Prepare statement & execute query
    //            MySqlCommand cmd = new MySqlCommand();
    //            cmd.Connection = DB;
    //            cmd.CommandText = sql;
    //            cmd.Prepare();
    //            Result = cmd.ExecuteReader();
    //        }
    //    }

    //    /// <summary>
    //    /// Database query
    //    /// </summary>
    //    /// <param name="sql">SQL command string</param>
    //    /// <param name="param">Bind Parameters</param>
    //    public static void Query(string sql, Dictionary<string,object> param)
    //    {
    //        try
    //        {

    //            MySqlCommand cmd = new MySqlCommand();
    //            cmd.Connection = DB;
    //            cmd.CommandText = sql;
    //            List<string> keys = new List<string>(param.Keys);
    //            foreach(string key in keys)
    //            {
    //                cmd.Parameters.AddWithValue("@" + key, param[key]);
    //            }
    //            cmd.Prepare();
    //            Result = cmd.ExecuteReader();

    //        }
    //        catch(Exception)
    //        {
    //            // Something when wrong...
    //        }
    //    }

    //    public static void Free()
    //    {
    //        if (Result != null && !Result.IsClosed)
    //        {
    //            Result.Close();
    //        }
    //    }



    //    //public static DatabaseResult[] Fetch()
    //    //{
    //    //    try
    //    //    {
    //    //        DatabaseResult[] data = new DatabaseResult[] { };
    //    //        for(int i = 0; Result.Read(); i++)
    //    //        {
    //    //            for(int n = 0; n < Result.FieldCount; n++)
    //    //            {
    //    //                data[i].Add(Result.GetName(n), Result.GetValue(n));
    //    //            }
    //    //        }

    //    //        Result.Close();
    //    //        return data;
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        return null;
    //    //    }
    //    //}

    //    public static MySqlDataReader Data()
    //    {
    //        try
    //        {
    //            return GetResult();
    //        }
    //        catch(Exception)
    //        {
    //            // Error
    //            return null;
    //        }
    //    }

    //    private static MySqlDataReader GetResult()
    //    {
    //        if(Result != null)
    //        {
    //            return Result;
    //        }
    //        else
    //        {
    //            throw new Exception("Error: There hadn't been");
    //        }
    //    }

    //    private static MySqlConnection GetConnection(string sql)
    //    {
    //        if(DB != null)
    //        {
    //            return DB;
    //        }
    //        else
    //        {
    //            throw new Exception("Error: There is no connection yet!");
    //        }
    //    }
    //}

    #endregion

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
