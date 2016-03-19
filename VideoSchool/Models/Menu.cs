using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSchool.Models
{
    public struct MenuItem
    {
        public string name;
        public string href;
        public string text;
        public bool active;

        public MenuItem (string name, string href, string text)
        {
            this.name = name;
            this.href = href;
            this.text = text;
            active = false;
        }

    }

    public class Menu
    {
        Shared shared;
        public string name { get; private set; }
        public MenuItem [] items;

        public Menu (Shared shared)
        {
            this.shared = shared;
            items = null;
        }

        public void Init (string theme)
        {
            switch (theme)
            {
                case "HOME" :   items = new MenuItem [] { 
                                    new MenuItem ("help_about", "/Help/About", "About"),
                                    new MenuItem ("login_index", "/Login/",     "Login"),
                                    new MenuItem ("login_signup", "/Login/Signup", "Signup")
                                };
                                break;
                case "LOGIN": items = new MenuItem[] { 
                                    new MenuItem ("help_about", "/Help/About", "About")
                                };
                                break;
                case "ADMIN" :  items = new MenuItem [] { 
                                    new MenuItem ("help_about", "/Help/About", "About")
                                };
                                break;
            }
        }

        public void Active (string name)
        {
            this.name = name;
            for (int j = 0; j < items.Length; j ++)
                if (items[j].name == this.name)
                    items[j].active = true;
        }


    }
}