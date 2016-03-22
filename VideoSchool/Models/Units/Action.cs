using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class Action : BaseUnit
    {
        public string id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string status { get; set; }

        public Action () : this (null) { }

        public Action (Shared shared)
            : base (shared)
        {
            SelectNew();
        }

        public void SelectNew()
        {
            this.id = "New";
            this.name = "";
            this.info = "-";
            this.status = "0";
        }

        public void Select(string id)
        {
            try
            {
                string query = @"
                SELECT id, name, info, status
                  FROM action
                 WHERE id = '" + shared.db.addslashes(id) + "'";
                DataTable table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("Action " + id + " not found");
                    return;
                }
                ExtractRow(table, 0);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        private void ExtractRow(DataTable table, int row)
        {
            id = table.Rows[row]["id"].ToString();
            name = table.Rows[row]["name"].ToString();
            info = table.Rows[row]["info"].ToString();
            status = table.Rows[row]["status"].ToString();
        }

        public void SelectActions()
        {
            try
            {
                qtable = new QTable(shared);
                string filterSlashes = shared.db.addslashes(filter);
                
                string where = " 1 ";
                if (filter != "")
                    where = 
                    " (id = '" + filterSlashes + @"'
                       OR name LIKE '%" + filterSlashes + @"%'
                       OR info LIKE '%" + filterSlashes + @"%'
                       OR status = '" + filterSlashes + "')";

                qtable.Init (
                        "SELECT COUNT(*) FROM action WHERE " + where,
                       @"SELECT id, name, info, status
                           FROM action 
                          WHERE " + where + @"
                          ORDER BY name ASC");
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
            UPDATE action
		       SET name = '" + shared.db.addslashes(this.name) + @"',
		           info = '" + shared.db.addslashes(this.info) + @"',
		           status = '" + shared.db.addslashes(this.status) + @"'
		     WHERE id = '" + shared.db.addslashes(this.id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

	    public void Insert ()		 			//добавить действие
	    {
		    // action_Update
            try
            {
                string count = shared.db.Scalar (
                    @"SELECT COUNT(*) 
                        FROM action 
                       WHERE name = '" + shared.db.addslashes(this.name) + @"'");
	    	    if (count != "0") {
                    shared.error.MarkUserError ("Action with this name already exists");
                    return;
                }
                string query = @"
            INSERT INTO action
		       SET name = '" + shared.db.addslashes(this.name) + @"',
		           info = '" + shared.db.addslashes(this.info) + @"',
		           status = '" + shared.db.addslashes(this.status) + @"'";
                long id = shared.db.Insert(query);
                this.id = id.ToString();
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
	    }

        public void Copy (Action post)
        {
            this.name = post.name;
            this.info = post.info;
            this.status = post.status;
        }

/*
         public int GetIdByAction (string action)
	    {
		    // no action
		    return 
			    SELECT id FROM action 
			     WHERE name = 'action';
	    }

	    public string GetActionById (string id)
	    {
		    // no action
		    return 
			    SELECT name FROM action 
			     WHERE id = 'id';	
	    }
*/

        public void Delete(string id)
        {
            try
            {
                string query = @"
            DELETE FROM action
		     WHERE id = '" + shared.db.addslashes(id) + @"'
		     LIMIT 1";
                shared.db.Update(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }
    }
}