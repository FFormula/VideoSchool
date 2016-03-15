using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace VideoSchool.Models
{
    public class DataBase
    {
        Shared shared;
        private MySqlConnection con;
        public string query { get; private set; }
        public bool connected;

        /// <summary>
        /// Init the DataBase
        /// </summary>
        /// <param name="shared"></param>
        public DataBase (Shared shared)
        {
            this.shared = shared;
            connected = false;
        }

        /// <summary>
        /// Open connection to database.
        /// </summary>
        public void Open()
        {
            if (connected) return;
            try
            {
                connected = true;
                query = "OPEN CONNECTION TO MYSQL";
                con = new MySqlConnection(shared.config.mysql_connection);
                con.Open();
            }
            catch (Exception ex)
            {
                shared.error.MarkDBaseError(ex, query);
                con = null;
                throw ex;
            }
        }

        /// <summary>
        /// Close connection if it was used
        /// </summary>
        ~DataBase ()
        {
            if (!connected) return;
            try
            {
                con.Close();
            }
            catch 
            {

            }
        }

        /// <summary>
        /// Complete the SELECT query and get the DataTable of rows
        /// </summary>
        /// <param name="myquery">SQL query</param>
        /// <returns>All fetched rows</returns>
        public DataTable Select (string myquery)
        {
            if (shared.error.AnyError()) return null;
            if (!connected) Open();
            try
            {
                query = myquery;
                DataTable table = new DataTable();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
                return table;
            }
            catch (Exception ex)
            {
                shared.error.MarkDBaseError(ex, query);
                throw ex;
            }
        }

        /// <summary>
        /// Return a single string-value
        /// </summary>
        /// <param name="myquery">SQL query</param>
        /// <returns>Fetched scalar value</returns>
        public string Scalar (string myquery)
        {
            if (shared.error.AnyError()) return null;
            if (!connected) Open();
            try
            {
                query = myquery;
                DataTable table = new DataTable();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
                if (table.Rows.Count == 0)
                    return "";
                return table.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                shared.error.MarkDBaseError(ex, query);
                throw ex;
            }
        }

        /// <summary>
        /// Return a single string-value
        /// </summary>
        /// <param name="myquery">SQL query</param>
        /// <returns>Fetched scalar value</returns>
        public int ScalarInt(string myquery)
        {
            try
            {
                return Convert.ToInt32(Scalar(myquery));
            }
            catch (Exception ex)
            {
                if (shared.error.NoErrors())
                    shared.error.MarkSystemError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Complete an Insert query
        /// </summary>
        /// <param name="myquery">SQL query</param>
        /// <returns>Inserted record ID</returns>
        public long Insert(string myquery)
        {
            if (shared.error.AnyError()) return -1;
            if (!connected) Open();
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                shared.error.MarkDBaseError(ex, query);
                throw ex;
            }
        }

        /// <summary>
        /// Complete an Update query
        /// </summary>
        /// <param name="myquery">SQL query</param>
        /// <returns>?</returns>
        public int Update(string myquery)
        {
            if (shared.error.AnyError()) return -1;
            if (!connected) Open();
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                shared.error.MarkDBaseError(ex, query);
                throw ex;
            }
        }

        public int Delete(string myquery)
        {
            return Update(myquery);
        }

        /// <summary>
        /// Screening user-string slashes
        /// </summary>
        /// <param name="text">Any string text</param>
        /// <returns>Same text with the slashes before '</returns>
        public string addslashes (string text)
        {
            if (text == null) return "";
            return text.Replace ("\'", "\\\'");
        }

    }
}