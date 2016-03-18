using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSchool.Models
{
    public struct MenuItem
    {
        public string href;
        public string text;
        public string style;

        public MenuItem (string href, string text, string style = "navbar-brand")
        {
            this.href = href;
            this.text = text;
            this.style = style;
        }

    }

    public class Menu
    {
        Shared shared;
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
                case "home" :   items = new MenuItem [] { 
                                    new MenuItem ("/Help/About", "About"),
                                    new MenuItem ("/Login/",     "Login", "navbar-brand bg-success"),
                                    new MenuItem ("/Login/Signup", "Signup")
                                };
                                break;
                case "login": items = new MenuItem[] { 
                                    new MenuItem ("/Help/About", "About")
                                };
                                break;
                case "admin" :  items = new MenuItem [] { 
                                    new MenuItem ("/Help/About", "About")
                                };
                                break;
            }
        }


    }
}