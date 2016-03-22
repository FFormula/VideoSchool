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
            SelectNew();
        }

        internal void SelectNew()
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
                this.id = ExtractRowValue("id", 0);
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

        public void Copy(Menus post)
        {
            this.main = post.main;
            this.menu = post.menu;
            this.name = post.name;
            this.href = post.href;
            this.info = post.info;
            this.status = post.status;
            this.nr = post.nr;
        }

        public void Delete(string id)
        {
            try
            {
                string query = @"
            DELETE FROM menus
		     WHERE id = '" + shared.db.addslashes(id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void Update()
        {
            // action_Update
            try
            {
                string query = @"
            UPDATE menu
		       SET main = '" + shared.db.addslashes(this.main) + @"',
		           menu = '" + shared.db.addslashes(this.menu) + @"',
		           href = '" + shared.db.addslashes(this.href) + @"',
		           name = '" + shared.db.addslashes(this.name) + @"',
		           info = '" + shared.db.addslashes(this.info) + @"',
		           status = '"+shared.db.addslashes(this.status) + @"',
		           nr   = '" + shared.db.addslashes(this.nr) + @"'
		     WHERE id = '"   + shared.db.addslashes(this.id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void Insert()		 			//добавить действие
        {
            // action_Update
            try
            {
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM menu
                       WHERE menu = '" + shared.db.addslashes(this.menu) + @"'");
                if (count != "0")
                {
                    shared.error.MarkUserError("Menu with this menu-code already exists");
                    return;
                }
                string query = @"
            INSERT INTO menu
		       SET main = '" + shared.db.addslashes(this.main) + @"',
		           menu = '" + shared.db.addslashes(this.menu) + @"',
		           href = '" + shared.db.addslashes(this.href) + @"',
		           name = '" + shared.db.addslashes(this.name) + @"',
		           info = '" + shared.db.addslashes(this.info) + @"',
		           status = '"+shared.db.addslashes(this.status) + @"'";
                long id = shared.db.Insert(query);
                this.id = id.ToString();
                shared.db.Update (@"
            UPDATE menu
		       SET nr   = '" + shared.db.addslashes((10 * id).ToString()) + @"'
		     WHERE id = '"   + shared.db.addslashes (this.id) + @"'
		     LIMIT 1");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

    }
}