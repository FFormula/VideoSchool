﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSchool.Models
{
    public class User
    {
        string id;
        string status;
        
        string name  { get; set; }
        string email { get; set; }
        string passw { get; set; }

        /// <summary>
        /// Create an empty instance of User model
        /// </summary>
        public User ()
        {
            id = "";
            status = "0";
            name = "";
            email = "";
            passw = "";
        }

        /// <summary>
        /// Create User model and load User by id
        /// </summary>
        /// <param name="id">user_id - an unique user's number in database</param>
        public User (string id)
        {
            this.id = id;
            string query = @"
                SELECT name, email, status
                  FROM user
                 WHERE id = 'this.id'";
        }

	    /// <summary>
	    /// Join new user - add his to the user table.
	    /// </summary>
        void Insert()		
	    {
		    // no action checking
            string query = @"
		    INSERT INTO user
		       SET name = 'Jevgenij',
		           email = 'formulist@gmail.com',
		           passw = password('qwas'),
		           status = '1'";
	    }

        /// <summary>
        /// Check this.email and this.passw by user table.
        /// </summary>
        /// <returns>True - email and password correct, False - invalid authorisation</returns>
	    bool Login()		
	    {
		    // no action checking
            string query = @"
		    SELECT COUNT(*) 
		      FROM user
		     WHERE email = 'formulist@gmail.com'  
		       AND passw = password('qwas')
		       AND status != '0'";
		    int result = 0;
		    return result == 1;
	    }

        /// <summary>
        /// Change user status to 0 (blocked) or to 1 (active).
        /// </summary>
        /// <param name="status">0 - to block, 1 - to active user</param>
	    void UpdateStatus(int status)	//	изменить статус пользователя
	    {
		    // user_Update
		    string query = @"
            UPDATE user
		       SET status = '1'
		     WHERE id = '1'
		     LIMIT 1";
	    }

	    /// <summary>
	    /// Change user name or/and email. Can be done by admin only.
	    /// </summary>
        void Update ()		// редактирование данных пользователя: name & email
	    {
		    // user_Update
		    string query = @"
            UPDATE user
		       SET name = 'John',
		           email = 'jevgesha@mail.ru'
		     WHERE id = '1'
		     LIMIT 1";
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
		    string action_id = "";

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