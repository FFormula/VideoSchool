using System;
using System.Collections.Generic;
using System.Data;
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

        internal void SelectNew()
        {
            throw new NotImplementedException();
        }

        public void SelectMenus ()
        {
            try
            {
                qtable = new QTable(shared);
                string filterSlashes = shared.db.addslashes(filter);

                string where = " 1 ";
                if (filter != "")
                    where +=
                    " AND (main = '" + filterSlashes + @"'
                       OR menu LIKE '%" + filterSlashes + @"%'
                       OR href LIKE '%" + filterSlashes + @"%'
                       OR name LIKE '%" + filterSlashes + @"%'
                       OR info LIKE '%" + filterSlashes + @"%'
                       OR id =       '" + filterSlashes + @"')";

                qtable.Init(
                        "SELECT COUNT(*) FROM menu WHERE " + where,
                       @"SELECT id, main, menu, href, name, info, status, nr
                           FROM menu 
                          WHERE " + where + @"
                          ORDER BY main, nr DESC");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }
        public void Select(string id)
        {
            try
            {
                string query = @"
                SELECT id, main, menu, href, name, info, status, nr
                           FROM menu
                 WHERE id = '" + shared.db.addslashes(id) + "'";
                 table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("Action " + id + " not found");
                    return;
                }
                id=  ExtractRowValue("id", 0);
                main = ExtractRowValue("main", 0);
                menu = ExtractRowValue("menu", 0);
                href = ExtractRowValue("href", 0);
                name = ExtractRowValue("name", 0);
                info = ExtractRowValue("info", 0);
                status = ExtractRowValue("status", 0);
                nr = ExtractRowValue("nr", 0);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }


   

    }
}