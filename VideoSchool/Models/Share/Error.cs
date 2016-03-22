using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoSchool.Models.Share
{
    /// <summary>
    /// Type of errors
    /// </summary>
    public enum ErrorMode
    {
        NoErrors, 
        UserError, // Those errors processed by project
        DBaseError, 
        SystemError // Any other uncatched errors
    }

    /// <summary>
    /// An Error Manager
    /// </summary>
    public class Error
    {
        // can be used for reading config data, where to put error logs
        public Shared shared { get; private set; }

        public Exception exception { get; set; }
        public string query { get; set; }
        public string text { get; set; }
        

        public ErrorMode mode { get; set; }
        
        /// <summary>
        /// Init Error Manager
        /// </summary>
        /// <param name="shared">Shared object</param>
        public Error (Shared shared)
        {
            this.shared = shared;
            Clear();
        }

        /// <summary>
        /// Cancel any errors
        /// </summary>
        public void Clear ()
        {
            mode = ErrorMode.NoErrors;
            exception = null;
            text = "";
        }

        /// <summary>
        /// Mark an User Error :)
        /// </summary>
        /// <param name="text">A message to the user</param>
        public void MarkUserError(string text)
        {
            this.mode = ErrorMode.UserError;
            this.exception = new Exception("no exception");
            this.text = text;
        }

        /// <summary>
        /// Mark a DataBase error with a last query
        /// </summary>
        /// <param name="exception">Catched Exception</param>
        /// <param name="query">Error's query</param>
        public void MarkDBaseError(Exception exception, string query)
        {
            this.mode = ErrorMode.DBaseError;
            this.query = query;
            this.exception = exception;
            this.text = "DataBase error";
        }
        
        /// <summary>
        /// Any other uncatched error
        /// </summary>
        /// <param name="exception">Catched Exception</param>
        public void MarkSystemError(Exception exception)
        {
            this.mode = ErrorMode.SystemError;
            this.query = "";
            this.exception = exception;
            this.text = "System error";
        }

        /// <summary>
        /// Check, if there is any error
        /// </summary>
        /// <returns>True - is an error, False - No errors</returns>
        public bool AnyError()
        {
            return mode != ErrorMode.NoErrors;
        }

        /// <summary>
        /// Check, if there are no errors
        /// </summary>
        /// <returns>True - No errors, False - Any error</returns>
        public bool NoErrors()
        {
            return !AnyError(); 
        }

        /// <summary>
        /// Check, if there are any User error
        /// </summary>
        /// <returns>True - Is User Error, False - No User errors</returns>
        public bool UserError()
        {
            return mode == ErrorMode.UserError;
        }
    }
}