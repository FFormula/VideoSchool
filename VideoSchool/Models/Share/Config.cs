using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.IO;

namespace VideoSchool.Models.Share
{
    public class Config
    {
        private string filename;

        public string hostname;

        public string mysqlConnection { get; set; }
        
        public string smtpHost;
        public string smtpPort;
        public string smtpUser;
        public string smtpPass;
        public string mailFrom;
        public string mailName;
        public string mailReplyTo;
        public string mailCc;

        private string[] lines;
        private Dictionary<string, string> items = null;

        /// <summary>
        /// Init config for required Run Mode
        /// </summary>
        /// <param name="mode">Where it will be run now</param>
        public Config(string filename = "")
        {
            this.filename = filename;
            if (this.filename == "") 
            {
                InitFromWebConfig();
                return;
            }
            InitFromFile();
        }

        /// <summary>
        /// Init for web browser, using WebConfigurationManager
        /// </summary>
        public void InitFromWebConfig ()
        {
            mysqlConnection = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            hostname = WebConfigurationManager.AppSettings.Get("hostname");
            smtpHost = WebConfigurationManager.AppSettings.Get("smtpHost");
            smtpPort = WebConfigurationManager.AppSettings.Get("smtpPort");
            smtpUser = WebConfigurationManager.AppSettings.Get("smtpUser");
            smtpPass = WebConfigurationManager.AppSettings.Get("smtpPass");
            mailFrom = WebConfigurationManager.AppSettings.Get("mailFrom");
            mailName = WebConfigurationManager.AppSettings.Get("mailName");
            mailReplyTo = WebConfigurationManager.AppSettings.Get("mailReplyTo");
            mailCc = WebConfigurationManager.AppSettings.Get("mailCc");
        }

        /// <summary>
        /// Init for unit test
        /// </summary>
        public void InitFromFile ()
        {
            Parse();
        }

        public void Parse ()
        {
            lines = File.ReadAllLines(filename);
            items = new Dictionary<string, string>();
            for (int j = 0; j < lines.Length; j++)
                ParseLine(lines[j]);
            ExtractValues();
        }

        private string ExtractOptional(string key)
        {
            string value;
            if (!items.TryGetValue(key, out value))
                value = "";
            return value;
        }

        private string ExtractNeeded(string key)
        {
            string value;
            if (!items.TryGetValue(key, out value))
                value = ""; //  1; //value = items.Count.ToString(); //// throw new Exception("Missing config value for " + key);
            return value;
        }

        private void ParseLine (string line)
        {
            if ((line ?? "") == "") return;
            if (line[0] == ';') return;
            int p = line.IndexOf("=");
            if (p <= 0) return;
            string key = line.Substring(0, p);
            if (items.ContainsKey("key"))
                throw new Exception("Duplicate key in config file");
            string val = line.Substring(p + 1);
            items.Add(key, val);
        }

        private void ExtractValues()
        {
            mysqlConnection = ExtractNeeded("mysqlConnection");
            hostname = ExtractNeeded("hostname");
            smtpHost = ExtractNeeded("smtpHost");
            smtpPort = ExtractNeeded("smtpPort");
            smtpUser = ExtractNeeded("smtpUser");
            smtpPass = ExtractNeeded("smtpPass");
            mailFrom = ExtractNeeded("mailFrom");
            mailName = ExtractNeeded("mailName");
            mailReplyTo = ExtractOptional("mailReplyTo");
            mailCc = ExtractOptional("mailCc");
        }

    }
}