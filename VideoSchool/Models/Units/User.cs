using System;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class User : BaseUnit
    {
        public string id    { get; set; }
        public string name  { get; set; }
        public string email { get; set; }
        public string passw { get; set; }
        public string status { get; private set; }

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
                table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("User " + id + " not found");
                    return;
                }
                id = ExtractRowValue("id");
                name = ExtractRowValue("name");
                email = ExtractRowValue("email");
                status = ExtractRowValue("status");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
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
                if (getIdByEmail () != "-1")
                {
                    shared.error.MarkUserError("This email already taken");
                    return;
                }
                shared.db.Insert(
           @"INSERT INTO user
		        SET name = '" + shared.db.addslashes(this.name) + @"',
		            email = '" + shared.db.addslashes(this.email) + @"',
		            passw = password('" + shared.db.addslashes(this.passw) + @"'),
		            status = '1'");
            } 
            catch (Exception ex)
            {
                ThrowError(ex);
            }
	    }

        /// <summary>
        /// Check user table for email
        /// </summary>
        /// <returns>-1: email not found, N: user id</returns>
        protected string getIdByEmail ()
        {
            if ((this.email ?? "").Length < 5) 
                return "-1";
            return shared.db.Scalar (
                @"SELECT COALESCE(min(id),-1)
		            FROM user
		           WHERE email = '" + shared.db.addslashes(this.email) + @"'");
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

        public bool Exists ()
        {
            try
            {
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM user
                       WHERE id = '" + shared.db.addslashes(this.id) + "'");
                return (count == "1");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return false;
            }
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