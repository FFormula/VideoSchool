using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoSchool.Models
{
    public enum RunMode
    {
        WebDebug,    // Run Project from programmer's desktop
        WebRelease, // Run Project on the web-server
        UnitTest   // Run Unit test
    };

    /// <summary>
    /// This class contains all stuff required for the others Models work
    /// </summary>
    public class Shared
    {
        public Config config { get; private set; } // all configs
        public DataBase db { get; private set; }   // sql functions
        public Error error { get; private set; }   // errors manager

        /// <summary>
        /// Init all stuffs for required Run Mode
        /// </summary>
        /// <param name="mode">A place, where it started</param>
        public Shared (RunMode mode)
        {
            config = new Config (mode);
            error = new Error(this);
            db = new DataBase(this);
        }

    }
}