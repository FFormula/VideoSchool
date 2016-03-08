using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoSchool.Models
{
    public enum ErrorMode
    {
        NoErrors,
        UserError,
        DBaseError,
        SystemError
    }

    public class Error
    {
        public Exception exception { get; set; }
        public string query { get; set; }
        public string text { get; set; }

        public ErrorMode mode { get; set; }
        
        public Error ()
        {
            Clear();
        }

        public void Clear ()
        {
            mode = ErrorMode.NoErrors;
            exception = null;
            text = "";
        }

        public void MarkUserError(string text)
        {
            this.mode = ErrorMode.UserError;
            this.exception = new Exception("no exception");
            this.text = text;
        }

        public void MarkDBaseError(Exception exception, string text, string query)
        {
            this.mode = ErrorMode.DBaseError;
            this.query = query;
            this.exception = exception;
            this.text = text;
        }
        
        public void MarkSystemError(Exception exception)
        {
            this.mode = ErrorMode.SystemError;
            this.query = "";
            this.exception = exception;
            this.text = "System error";
        }

        public bool AnyError()
        {
            return mode != ErrorMode.NoErrors;
        }

        public bool NoErrors()
        {
            return mode == ErrorMode.NoErrors;
        }

        public bool UserErrors()
        {
            return mode == ErrorMode.UserError;
        }
    }
}