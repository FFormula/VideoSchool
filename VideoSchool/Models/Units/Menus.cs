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
        public string main_id { get; set; }
        public string menu { get; set; }
        public string href { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string status { get; set; }
        public string nr { get; set; }
        
        public QTable selectMenuMain { get; set; }
        public string SelectedMenuId { get; private set; }
        public string menuMainId { get; set; }

        public Menus () : 
            this (null)
        {
        }

        public Menus(Shared shared)
            : base (shared)
        {
            SetDefaults();
        }

        public void SetDefaults()
        {
            id = "";
            main_id = "";
            menu = "";
            href = "";
            name = "";
            info = "";
        }

        public void SelectMenuInMenuMain(string menuMainId)
        {
            try
            {
                this.menuMainId = menuMainId;
                qtable = new QTable(shared);
                string where = " main_id = '" + shared.db.addslashes(menuMainId) + "'";
                qtable.Init(
                        "SELECT COUNT(*) FROM menu WHERE " + where,
                       @"SELECT id, main_id, menu, href, name, info, status, nr
                           FROM menu 
                          WHERE " + where + @"
                       ORDER BY nr");
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
                SELECT id, main_id, menu, href, name, info, status, nr
                  FROM menu
                 WHERE id = '" + shared.db.addslashes(id) + "'";
                 table = shared.db.Select(query);
               // this.SelectedMenuId = id;
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("Action " + id + " not found");
                    return;
                }
                this.id = ExtractRowValue("id", 0);
                main_id = ExtractRowValue("main_id", 0);
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
            this.main_id = post.main_id;
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
                    DELETE FROM menu
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
		               SET main_id = '" + shared.db.addslashes(this.main_id) + @"',
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
		               SET main_id = '" + shared.db.addslashes(this.main_id) + @"',
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

        protected void SwapNR (string prevID)
        {
            if (prevID == "") return;
            Menus prev = new Menus(shared);
            prev.Select(prevID);
            string prevNR = prev.nr;
            prev.nr = this.nr;
            prev.Update();
            this.nr = prevNR;
            this.Update();
        }

        public void MoveUp(string id)
        {
            try
            {
                Select(id);
                if (this.id == "" || this.main_id == "")
                    return;
                string prevID = shared.db.Scalar (
                    @"SELECT id
                        FROM menu
                       WHERE main_id = '" + shared.db.addslashes(this.main_id) + @"'
                         AND nr < '" + shared.db.addslashes(this.nr) + @"'
                       ORDER BY nr DESC
                       LIMIT 1");
                SwapNR(prevID);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void MoveDn(string id)
        {
            try
            {
                Select(id);
                if (this.id == "" || this.main_id == "" || this.nr == "")
                    return;
                string nextID = shared.db.Scalar(
                    @"SELECT id
                        FROM menu
                       WHERE main_id = '" + shared.db.addslashes(this.main_id) + @"'
                         AND nr > '" + shared.db.addslashes(this.nr) + @"'
                       ORDER BY nr ASC
                       LIMIT 1");
                SwapNR(nextID);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
        /// Select QTable MenuMain from Select Filters
        /// </summary>
        public void SelectMenuMain()
        {
            try
            {
                selectMenuMain = new QTable(shared);                
                selectMenuMain.Init(
                    "SELECT 1 ",
                   @"SELECT id, main, name
                       FROM menu_main
                   ORDER BY main;");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

    }
}