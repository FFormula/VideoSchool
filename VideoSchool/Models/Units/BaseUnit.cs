using System;
using System.Data;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class BaseUnit
    {
        protected DataTable table;
        public Shared shared { get; private set; }
        public QTable qtable { get; protected set; }
        public string filter { get; set; }

        public BaseUnit () : this (null) { }

        public BaseUnit (Shared shared)
        {
            this.shared = shared;
            qtable = new QTable(shared);
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

        protected string ExtractRowValue(string name, int row = 0)
        {
            return table.Rows[row][name].ToString();
        }

    }
}