using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSchool.Models.Units
{
    public class BaseUnit
    {
        protected Shared shared;
        public QTable qtable { get; protected set; }
        public string filter { get; set; }

        public BaseUnit () : this (null) { }

        public BaseUnit (Shared shared)
        {
            this.shared = shared;
        }

        /// <summary>
        /// Throw a last error if exists, or throw new Ex error
        /// </summary>
        /// <param name="ex">Last error Exception</param>
        protected void ThrowError(Exception ex)
        {
            if (shared.error.NoErrors())
                shared.error.MarkSystemError(ex);
            throw ex;
        }

    }
}