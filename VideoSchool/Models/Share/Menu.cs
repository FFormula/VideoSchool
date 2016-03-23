using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace VideoSchool.Models.Share
{
    public struct MenuItem
    {
        public string menu { get; private set; }
        public string href { get; private set; }
        public string name { get; private set; }
        public string info { get; private set; }
        public bool active { get; private set; }

        public MenuItem(string menu, string href, string name, string info)
            : this()
        {
            this.menu = menu;
            this.href = href;
            this.name = name;
            this.info = info;
            active = false;
        }

        public void SetActive()
        {
            active = true;
        }

    }

    public class Menu
    {
        protected Shared shared;
        public string main_id;
        public string active { get; private set; }
        public MenuItem[] items;

        public Menu() :
            this(null)
        {
        }

        public Menu(Shared shared)
        {
            this.shared = shared;
            main_id = "";
            active = "";
            items = new MenuItem[0];
        }

        public void Init(string main)
        {
            try
            {
                this.main_id = shared.db.Scalar(
                    @"SELECT id 
                        FROM menu_main 
                       WHERE main = '" + shared.db.addslashes(main) + 
                    "' LIMIT 1");
                DataTable table = shared.db.Select(
                   @"SELECT menu, href, name, info 
                       FROM menu 
                      WHERE main_id = '" + shared.db.addslashes(this.main_id) + @"'
                        AND status > 0
                      ORDER BY nr");
                items = new MenuItem[table.Rows.Count];
                for (int j = 0; j < table.Rows.Count; j++)
                    items[j] = new MenuItem(
                        table.Rows[j]["menu"].ToString(),
                        table.Rows[j]["href"].ToString(),
                        table.Rows[j]["name"].ToString(),
                        table.Rows[j]["info"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetActive(string menu)
        {
            this.active = menu;
            for (int j = 0; j < items.Length; j++)
                if (items[j].menu == this.active)
                    items[j].SetActive();
        }

    }
}