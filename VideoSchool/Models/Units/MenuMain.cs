using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class MenuMain : BaseUnit
    {
        public string id;
        public string main { get; set; }
        public string name { get; set; }
        public string info { get; set; }

        public MenuMain() : 
            this (null)
        {
        }

        public MenuMain(Shared shared)
            : base (shared)
        {
            SelectNew();
        }

        internal void SelectNew()
        {
            id = "";
            main = "";
            name = "";
            info = "";
        }


        /// <summary>
        /// выбор пунктов menu
        /// </summary>
        public void SelectMenuMain()
        {
            try
            {
                qtable = new QTable(shared);
                string filterSlashes = shared.db.addslashes(filter);

                string where = " 1 ";
                if (filter != "")
                    where +=
                    " AND(main LIKE '%" + filterSlashes + @"%'                      
                       OR name LIKE '%" + filterSlashes + @"%'
                       OR info LIKE '%" + filterSlashes + @"%'
                       OR id =       '" + filterSlashes + @"')";

                qtable.Init(
                        "SELECT COUNT(*) FROM menu_main WHERE " + where,
                       @"SELECT id, main,  name, info
                           FROM menu_main 
                          WHERE " + where + @"
                          ORDER BY main");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
        /// select by id - 1 row
        /// </summary>
        /// <param name="id"></param>
        public void Select(string id)
        {
            try
            {
                string query = @"
                SELECT id, main, name, info
                  FROM menu_main
                 WHERE id = '" + shared.db.addslashes(id) + "'";
                table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("Action " + id + " not found");
                    return;
                }
                this.id = ExtractRowValue("id", 0);
                this.main = ExtractRowValue("main", 0);
                this.name = ExtractRowValue("name", 0);
                this.info = ExtractRowValue("info", 0);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void Copy(MenuMain post)
        {
            this.main = post.main;
            this.name = post.name;
            this.info = post.info;            
        }

        public void Delete(string id)
        {
            try
            {
                string query = @"
            DELETE FROM menu_main
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
            UPDATE menu_main
		       SET main = '" + shared.db.addslashes(this.main) + @"',		       
		           name = '" + shared.db.addslashes(this.name) + @"',
		           info = '" + shared.db.addslashes(this.info) + @"'
		     WHERE id = '" + shared.db.addslashes(this.id) + @"'
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
                        FROM menu_main
                       WHERE main = '" + shared.db.addslashes(this.main) + @"'");
                if (count != "0")
                {
                    shared.error.MarkUserError("Main menu with this code already exists");
                    return;
                }
                string query = @"
            INSERT INTO menu_main
		       SET main = '" + shared.db.addslashes(this.main) + @"',		         
		           name = '" + shared.db.addslashes(this.name) + @"',
		           info = '" + shared.db.addslashes(this.info) + @"'
		           ";
                long id = shared.db.Insert(query);
                this.id = id.ToString();
              
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }
    }
}