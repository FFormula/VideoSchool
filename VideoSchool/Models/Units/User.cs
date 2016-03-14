using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace VideoSchool.Models.Units
{
    public class User : BaseUnit
    {
        public string id { get; private set; }
        public string status { get; private set; }
        
        public string name  { get; set; }
        public string email { get; set; }
        public string passw { get; set; }

        public User () : this (null) { }
        public User (Shared shared) : base (shared) 
        {
            name = "";
            email = "";
            passw = "";
        }

        /// <summary>
        /// Create User model and load User by id
        /// </summary>
        /// <param name="id">user_id - an unique user's number in database</param>
        public void Select (string id)
        {
            try
            {
                string query = @"
                SELECT id, name, email, status
                  FROM user
                 WHERE id = '" + shared.db.addslashes (id) + "'";
                DataTable table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("User " + id + " not found");
                    return;
                }
                ExtractRow(table, 0);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        private void ExtractRow(DataTable table, int row)
        {
            id = table.Rows[row]["id"].ToString();
            name = table.Rows[row]["name"].ToString();
            email = table.Rows[row]["email"].ToString();
            status = table.Rows[row]["status"].ToString();
        }

        
        /// <summary>
        /// Create an User List by filter
        /// </summary>
        public void SelectUsers()
        {
            try
            {
                qtable = new QTable(shared);
                string filterSlashes = shared.db.addslashes(filter);
                
                string where = " 1 ";
                if (filter != "")
                    where = 
                    " (id = '" + filterSlashes + @"'
                       OR name LIKE '%" + filterSlashes + @"%'
                       OR email LIKE '%" + filterSlashes + @"%')";

                qtable.Init (
                        "SELECT COUNT(*) FROM user WHERE " + where,
                       @"SELECT id, name, email, status
                           FROM user 
                          WHERE " + where + @"
                          ORDER BY id DESC");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
	    /// Join new user - add his to the user table.
	    /// </summary>
        public void Insert()		
	    {
            try
            {
                // no action checking
                string query;

                query = @"
		    SELECT COUNT(*)
		        FROM user
		        WHERE email = '" + shared.db.addslashes(this.email) + @"'";

                if (shared.db.Scalar(query) != "0")
                {
                    shared.error.MarkUserError("This email already taken");
                    return;
                }

                query = @"
		    INSERT INTO user
		        SET name = '" + shared.db.addslashes(this.name) + @"',
		            email = '" + shared.db.addslashes(this.email) + @"',
		            passw = password('" + shared.db.addslashes(this.passw) + @"'),
		            status = '1'";
                shared.db.Insert(query);
            } 
            catch (Exception ex)
            {
                ThrowError(ex);
            }
	    }

        /// <summary>
        /// Check this.email and this.passw by user table.
        /// </summary>
        /// <returns>True - email and password correct, False - invalid authorisation</returns>
	    public void Login()		
	    {
		    // no action checking
            try
            {
                string query = @"
		    SELECT COALESCE(min(id),-1) AS UserID
		      FROM user
		     WHERE email = '" + shared.db.addslashes(this.email) + @"'  
		       AND passw = password('" + shared.db.addslashes(this.passw) + @"')
		       AND status != '0'";
                string id = shared.db.Scalar(query);
                if (id == "-1")
                {
                    shared.error.MarkUserError("Email or password incorrect");
                    return;
                }
                this.id = id;
            } 
            catch (Exception ex)
            {
                ThrowError(ex);
            }
	    }

        /// <summary>
        /// Change user status to 0 (blocked) or to 1 (active).
        /// </summary>
        /// <param name="status">0 - to block, 1 - to active user</param>
	    void UpdateStatus(int status)	//	изменить статус пользователя
	    {
		    // user_Update
            try
            {
                if ((id ?? "") == "")
                {
                    shared.error.MarkUserError("User ID not specified");
                    return;
                }
                string query = @"
            UPDATE user
		       SET status = '1'
		     WHERE id = '" + shared.db.addslashes(id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            } 
            catch (Exception ex)
            {
                ThrowError(ex);
            }
	    }

	    /// <summary>
	    /// Change user name or/and email. Can be done by admin only.
	    /// </summary>
        public void Update ()		// редактирование данных пользователя: name & email
	    {
		    // user_Update
            try { 
		        string query = @"
            UPDATE user
		       SET name = '" + shared.db.addslashes(this.name) + @"',
		           email = '" + shared.db.addslashes(this.email) + @"'
		     WHERE id = '" + shared.db.addslashes(this.id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
	    }

        /// <summary>
        /// Change current user password to this.passw
        /// </summary>
        void ChangePassword ()	// генерация нового пароля 
        {
    	    // no action checking
    	    this.passw = GeneratePassword (9);
            string query = @"
		    UPDATE user
		       SET passw = password('this.passw')
		     WHERE id = '1'
		     LIMIT 1";
        }

        /// <summary>
        /// Generate an Random password of length L
        /// </summary>
        /// <param name="L">A length of password</param>
        /// <returns>A random password</returns>
        private string GeneratePassword(int L)
        {
 	        return "RandomPassword$";
        }

        /// <summary>
        /// Change user password manually, with old and new one.
        /// </summary>
        /// <param name="old_passw">Old user password</param>
        /// <param name="new_passw">New user password</param>
        /// <returns>True - changed successfully, False - old password is wrong</returns>
	    bool ChangePassword (string old_passw, string new_passw)		// смена старого пароля
	    {
		    // no action checking
            string query = @"
		    UPDATE user
		       SET passw = password('this.passw')
		     WHERE id = '1'
		       AND passw = password('old_passw')
		     LIMIT 1";
            return true;
	    }

        /// <summary>
        /// Check a permission to do this action
        /// </summary>
        /// <param name="action">An action to do (from action table)</param>
        /// <returns>True - can be done, False - Forbidden</returns>
	    bool CanDoAction (string action)	
	    {
		    // no action checking
            string query = @"
                SELECT id FROM action 
			     WHERE action = 'action'";
		    //string action_id = "";

            query = @"
            SELECT COUNT(*) 
		      FROM role_action
		     WHERE action_id = 'action_id'
		       AND role_id IN (SELECT role_id
		                         FROM role_user 
		                        WHERE user_id = 'this.id')";
            return false;
	    }
    }
}