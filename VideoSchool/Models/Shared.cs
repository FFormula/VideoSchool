using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoSchool.Models
{
    public enum RunMode
    {
        WebDebug,
        WebRelease,
        UnitTest
    };

    public class Shared
    {
        public Config config { get; private set; }
        public DataBase db { get; private set; }
        public Error error { get; private set; }

        public Shared (RunMode mode)
        {
            config = new Config (mode);
            error = new Error(this);
            db = new DataBase(this);
        }

    }
}