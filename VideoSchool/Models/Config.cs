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

        public string filename = "config.txt";

        /// <summary>
        /// Init config for required Run Mode
        /// </summary>
        /// <param name="mode">Where it will be run now</param>
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

        /// <summary>
        /// Init for web browser, using WebConfigurationManager
        /// </summary>
        public void InitFromWeb ()
        {
            mysql_connection = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }

        /// <summary>
        /// Init for unit test
        /// </summary>
        public void InitFromTest ()
        {
            mysql_connection = System.IO.File.ReadAllText(filename);
        }

        public void SaveFromTest ()
        {
            System.IO.File.WriteAllText(filename, "server=localhost;UserId=root;Password=qwas;database=SCHOOL;CharSet=utf8");
        }
    }
}