using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace VideoSchool.Models.Units
{
    public class UserAddress : User
    {
        public string zip     { get; set; }
        public string area    { get; set; }
        public string city    { get; set; }
        public string street  { get; set; }
        public string country { get; set; }
        public string personal { get; set; }

        public UserAddress(Shared shared)
            : base(shared)
        {
            zip = "";
            area = "";
            city = "";
            street = "";
            country = "";
            personal = "";
        }

        public void Select (string id)
        {
            try
            {
                string query = @"
                SELECT zip, area, city, street, country, personal
                  FROM user_address
                 WHERE user_id = '" + shared.db.addslashes(id) + "'";
                DataTable table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {

                    shared.error.MarkUserError("Address for user " + id + " not found");
                    return;
                }
                zip = ExtractRowValue("zip");
                area = ExtractRowValue("area");
                city  = ExtractRowValue("city");
                street = ExtractRowValue("street");
                country = ExtractRowValue("country");
                personal = ExtractRowValue("personal");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }

        }

        public void Update ()
        {
            try
            {
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM user_address
                       WHERE user_id = '" + shared.db.addslashes(this.id) + "'");
                if (count == "0")
                {
                    Insert();
                }
                else
                {
                    string query = @"
            UPDATE user_address
		       SET zip = '" + shared.db.addslashes(this.zip) + @"',
		           area = '" + shared.db.addslashes(this.area) + @"',
		           city  = '" + shared.db.addslashes(this.city) + @"',
		           street = '" + shared.db.addslashes(this.street) + @"',
		           country = '" + shared.db.addslashes(this.country) + @"',
		           personal = '" + shared.db.addslashes(this.personal) + @"'
		     WHERE user_id = '" + shared.db.addslashes(this.id) + @"'
		     LIMIT 1";
                    shared.db.Update(query);
                }
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        protected void Insert ()
        {
            try
            {
                if (!Exists ())
                {
                    shared.error.MarkUserError("User not exists");
                    return;
                }
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM user_address
                       WHERE user_id = '" + shared.db.addslashes(this.id) + "'");
                if (count == "1")
                {
                    shared.error.MarkUserError("User address already exists");
                    return;
                }
                shared.db.Insert(
           @"INSERT INTO user_address
		       SET user_id = '" + shared.db.addslashes(this.id) + @"',
                   zip = '" + shared.db.addslashes(this.zip) + @"',
		           area = '" + shared.db.addslashes(this.area) + @"',
		           city  = '" + shared.db.addslashes(this.city) + @"',
		           street = '" + shared.db.addslashes(this.street) + @"',
		           country = '" + shared.db.addslashes(this.country) + @"',
		           personal = '" + shared.db.addslashes(this.personal) + @"'");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void Delete ()
        {
            try
            {
                string query = @"
            DELETE FROM user_address
		     WHERE user_id = '" + shared.db.addslashes(this.id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }
    }
}