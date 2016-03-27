using System;
using VideoSchool.Models.Share;


namespace VideoSchool.Models.Units
{
    public class UserPassw : User
    {
        Random rand = new Random();
        
        
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string newPassword1 { get; set; }



        public UserPassw() : this(null)
        {
        }

        public UserPassw(Shared shared) : base (shared)
        {
        }

        /// <summary>
        /// Request new password for user by this.email
        /// </summary>
        public string RequestPassword()	// генерация нового пароля 
        {
            // no action checking
            try
            {
                string myid = getIdByEmail();
                if (myid == "-1")
                {
                    shared.error.MarkUserError("User with this email does not registered");
                    return "";
                }
                if ((this.passw ?? "").Length < 5)
                {
                    shared.error.MarkUserError("Password must be at least 5 symbols length");
                    return "";
                }

                shared.db.Delete(
                    @"DELETE FROM user_passw 
                       WHERE user_id = '" + shared.db.addslashes(myid) +
                    "' LIMIT 1");
                string code = getRandomCode();
                shared.db.Insert(
              @"INSERT INTO user_passw
		           SET user_id = '" + shared.db.addslashes(myid) + @"',
                       passw = password('" + shared.db.addslashes(this.passw) + @"'),
                       code = '" + code + @"',
                       request_date = NOW()");

                return myid + "." + code;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return "";
            }
        }


        /// <summary>
        /// Change user password manually, with old and new one.
        /// </summary>
        /// <param name="old_passw">Old user password</param>
        /// <param name="new_passw">New user password</param>
        /// <returns>True - changed successfully, False - old password is wrong</returns>

        public void ChangePassword()
        {
            try
            {
                if (this.newPassword != this.newPassword1)
                {
                    shared.error.MarkUserError("Passwords mismatch");
                    return;
                }
                if (this.newPassword.Length < 4)
                {
                    shared.error.MarkUserError("New password too short");
                    return;
                }
                string match = shared.db.Scalar(@"
                     SELECT COUNT(*) FROM user 
		              WHERE id = '" + shared.db.addslashes(this.id) + @"'
		                AND passw = password('" + shared.db.addslashes(this.oldPassword) + @"')");
                if (match != "1")
                {
                    shared.error.MarkUserError("Incorrect password. Operation aborted.");
                    return;
                }
                string query = @"
		             UPDATE user
		                SET passw = password('" + shared.db.addslashes(this.newPassword) + @"')
		              WHERE id = '" + shared.db.addslashes (this.id) + @"'
		                AND passw = password('" + shared.db.addslashes(this.oldPassword) + @"')
		              LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        internal void Copy(UserPassw post)
        {
            this.passw = post.passw;
            this.name = post.name;
            this.oldPassword = post.oldPassword;
            this.newPassword = post.newPassword;
            this.newPassword1 = post.newPassword1;
           
        }

        public string getRandomCode (int length = 16)
        {
            string code = "";
            for (int j = 0; j < length; j++)
                code += rand.Next(0, 10);
            return code;
        }

        /// <summary>
        /// Parse code from email-link and check it
        /// Activate new password once
        /// </summary>
        /// <param name="code"></param>
        public string ActivatePassword(string idcode)
        {
            try
            {
                int p = idcode.IndexOf(".");
                if (p == -1)
                {
                    shared.error.MarkUserError("Wrong activation code format");
                    return "";
                }
                string id = idcode.Substring(0, p);
                string code = idcode.Substring(p + 1);

                string passw = shared.db.Scalar(
                    @"SELECT passw
                        FROM user_passw
                       WHERE user_id = '" + shared.db.addslashes(id) + @"'
                         AND code    = '" + shared.db.addslashes(code) + @"' 
                         AND ADDDATE(request_date, INTERVAL 48 HOUR) > NOW() LIMIT 1");
                // cleaner by request_date can be placed in crontabs

                if (passw == "")
                {
                    shared.error.MarkUserError("Invalid activation code");
                    return "";
                }
                shared.db.Update(
                    @"UPDATE user
                         SET passw = '" + shared.db.addslashes(passw) + @"'
                       WHERE id    = '" + shared.db.addslashes(id) + @"'");
                shared.db.Delete(
                    @"DELETE FROM user_passw
                       WHERE user_id = '" + shared.db.addslashes(id) + @"' LIMIT 1");
                return id;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return "";
            }
        }

   
      



    }
}