using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace VideoSchool.Models
{
    public class Config
    {
        public string mysql_connection { get; set; }

        public Config(RunMode mode)
        {
            switch (mode)
            {
                case RunMode.WebDebug: 
                case RunMode.WebRelease:
                                InitFromWeb();
                                break;

                case RunMode.UnitTest:
                                InitFromTest();
                                break;
            }
        }

        public void InitFromWeb ()
        {
            mysql_connection = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }

        public void InitFromTest ()
        {
            mysql_connection = "server=localhost;UserId=root;Password=qwas;database=SCHOOL;CharSet=utf8";
        }
    }
}