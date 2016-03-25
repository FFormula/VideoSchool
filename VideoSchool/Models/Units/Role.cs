using System;
using System.Data;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class Role : BaseUnit
    {
        public string id { get; private set; }
        public string name { get; set; }
        public string info { get; set; }

        public Role()
            : this (null)
        { 
        }

        public Role(Shared shared)
            : base (shared)
        {
            SetDefaults();
        }

        /// <summary>
        /// New Role
        /// </summary>
        public void SetDefaults()
        {
            this.id = "";
            this.name = "";
            this.info = "";       
        }

        public void Select(string id)
        {
            try
            {
                string query = @"
                SELECT id, name, info
                  FROM role
                 WHERE id = '" + shared.db.addslashes(id) + "'";
                DataTable table = shared.db.Select(query);
                if (table.Rows.Count == 0)
                {
                    shared.error.MarkUserError("Role " + id + " not found");
                    return;
                }
                ExtractRow(table, 0);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
        /// Extract data from table row to model
        /// </summary>
        /// <param name="table"></param>
        /// <param name="row"></param>
        private void ExtractRow(DataTable table, int row)
        {
            id = table.Rows[row]["id"].ToString();
            name = table.Rows[row]["name"].ToString();
            info = table.Rows[row]["info"].ToString();          
        }

        public void SelectRoles()
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

                qtable.Init(
                        "SELECT COUNT(*) FROM role WHERE " + where,
                       @"SELECT id, name, info 
                           FROM role 
                          WHERE " + where + @"
                          ORDER BY name ASC");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void Copy(Role post)
        {
            this.name = post.name;
            this.info = post.info;
        }

        /// <summary>
        /// update role or roles data
        /// </summary>
        public void Update()		
        {
            // role_Update
            try
            {
                string query = @"
                    UPDATE role
		               SET name = '" + shared.db.addslashes(this.name) + @"',
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

        /// <summary>
        /// Adding new Role
        /// </summary>
        public void Insert()
        {            
            try
            {
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM role
                       WHERE name = '" + shared.db.addslashes(this.name) + @"'");
                if (count != "0")
                {
                    shared.error.MarkUserError("Role with this name already exists");
                    return;
                }
                string query = @"
                    INSERT INTO role
		               SET name = '" + shared.db.addslashes(this.name) + @"',
		                   info = '" + shared.db.addslashes(this.info) + @"'";
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