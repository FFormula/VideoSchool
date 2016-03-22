using System;
using System.Collections.Generic;
using System.Web;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class Menus : BaseUnit
    {
        public string id;
        public string main { get; set; }
        public string menu { get; set; }
        public string href { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string status { get; set; }
        public string nr     { get; set; }


        public Menus () : 
            this (null)
        {
        }

        public Menus(Shared shared)
            : base (shared)
        {
            id = "";
            main = "";
            menu = "";
            href = "";
            name = "";
            info = "";
        }

        public void SelectMenus ()
        {
            try
            {
                qtable = new QTable(shared);
                string filterSlashes = shared.db.addslashes(filter);

                string where = " 1 ";
                if (filter != "")
                    where =
                    " (main = '" + filterSlashes + @"'
                       OR menu LIKE '%" + filterSlashes + @"%'
                       OR href LIKE '%" + filterSlashes + @"%'
                       OR name LIKE '%" + filterSlashes + @"%'
                       OR info LIKE '%" + filterSlashes + @"%')";

                qtable.Init(
                        "SELECT COUNT(*) FROM menu WHERE " + where,
                       @"SELECT main, menu, href, name, info, status, nr
                           FROM user 
                          WHERE " + where + @"
                          ORDER BY main, nr DESC");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

    }
}