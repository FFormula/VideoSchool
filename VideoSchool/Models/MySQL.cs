using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.Configuration;

namespace VideoSchool.Models
{
    public class MySQL
    {
        private MySqlConnection con;
        public string error { get; private set; }
        public string query { get; private set; }

        public MySQL ()
        {
            try
            {
                error = "";
                query = "OPEN CONNECTION TO MYSQL";
                con = new MySqlConnection(
                    WebConfigurationManager.ConnectionStrings["conn"].ConnectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                con = null;
            }
        }

        ~MySQL ()
        {
            try
            {
                con.Close();
            }
            catch 
            {

            }
        }

        public DataTable Select (string myquery)
        {
            if (IsError ()) return null;
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
                error = ex.Message;
                return null;
            }
        }

        public string Scalar (string myquery)
        {
            if (IsError()) return null;
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
                error = ex.Message;
                return "";
            }
        }

        public long Insert(string myquery)
        {
            if (IsError()) return -1;
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }

        public int Update(string myquery)
        {
            if (IsError()) return -1;
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }

        public bool IsError()
        {
            return error != "";
        }

        public string addslashes (string text)
        {
            return text.Replace ("\'", "\\\'");
        }
    }
}