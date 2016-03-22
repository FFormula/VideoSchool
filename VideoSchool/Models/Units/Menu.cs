using System;
using System.Collections.Generic;
using System.Web;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public struct MenuItem
    {
        public string menu { get; private set; }
        public string href { get; private set; }
        public string name { get; private set; }
        public string info { get; private set; }
        public bool active { get; private set; }

        public MenuItem(string menu, string href, string name, string info) : this ()
        {
            this.menu = menu;
            this.href = href;
            this.name = name;
            this.info = info;
            active = false;
        }

        public void SetActive ()
        {
            active = true;
        }

    }

    public class Menu : BaseUnit
    {
        public string main;
        public string active { get; private set; }
        public MenuItem [] items;

        public Menu () : 
            this (null)
        {
        }

        public Menu(Shared shared)
            : base (shared)
        {
            main = "";
            active = "";
            items = null;
        }

        public void Init (string main)
        {
            try
            {
                this.main = main;
                table = shared.db.Select(
                   @"SELECT menu, href, name, info 
                       FROM menu 
                      WHERE main = '" + shared.db.addslashes(main) + @"'
                      ORDER BY nr");
                items = new MenuItem[table.Rows.Count];
                for (int j = 0; j < table.Rows.Count; j++)
                    items[j] = new MenuItem(
                        ExtractRowValue("menu", j),
                        ExtractRowValue("href", j),
                        ExtractRowValue("name", j),
                        ExtractRowValue("info", j));
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void SetActive (string menu)
        {
            this.active = menu;
            for (int j = 0; j < items.Length; j++)
                if (items[j].menu == this.active)
                    items[j].SetActive();
        }

    }
}