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


        public DataBase (Shared shared)
        {
            this.shared = shared;
            connected = false;
        }

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
                shared.error.MarkDBaseError(ex, "Error connection to mysql", query);
                con = null;
                throw ex;
            }
        }

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
                shared.error.MarkDBaseError(ex, "Error on Select query", query);
                throw ex;
            }
        }

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
                shared.error.MarkDBaseError(ex, "Error on Scalar query", query);
                throw ex;
            }
        }

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
                shared.error.MarkDBaseError(ex, "Error on Insert query", query);
                throw ex;
            }
        }

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
                shared.error.MarkDBaseError(ex, "Error on Update query", query);
                throw ex;
            }
        }

        public string addslashes (string text)
        {
            if (text == null) return "";
            return text.Replace ("\'", "\\\'");
        }

    }
}