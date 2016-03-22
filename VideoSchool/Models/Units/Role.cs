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

        public QTable currentRoleAction { get; set; } //текущие действия для роли
        public QTable Action4Select { get; set; }

        public Role()
            : this (null)
        { 
        }

        public Role(Shared shared)
            : base (shared)
        {
            id = "";
            name = "";
            info = "";
        }

        /// <summary>
        /// New Role
        /// </summary>
        public void SelectNew()
        {
            this.id = "New";
            this.name = "";
            this.info = "-";
       
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
        /// add role
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

        /// <summary>
        /// select all action in current role
        /// </summary>
        public void SelectActionByRoleID()
        {
            try {

                currentRoleAction  = new QTable(shared);
                string filterSlashes = shared.db.addslashes(this.id);

                string where = " 1 ";
                if (filter != "")
                    where =
                    " (role_id = '" + filterSlashes + @"')";

                currentRoleAction.Init(
                        "SELECT COUNT(*) FROM role_action WHERE " + where,
                       @"SELECT 
                                a.id as id,
                                a.name as name,
                                a.info as info,
		                        a.status as status
                                     FROM action a JOIN role_action r 
			                         ON a.id=r.action_id 
           
                                      WHERE " + where + @"
                                      ORDER BY a.name ASC");


           
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }


        }

        /// <summary>
        /// Secel action 
        /// </summary>
        /// <param name="role_id"></param>
        public void SelectActionForAddRole(string role_id)
        {
            try
            {

                Action4Select = new QTable(shared);
                string filterSlashes = shared.db.addslashes(this.id);

                string where = " 1 ";
              

                Action4Select.Init(
                        "SELECT 1 " ,
                         @"SELECT  id, name FROM action WHERE id NOT IN 
                              (SELECT action_id FROM role_action
                               WHERE role_id = '"+role_id+"' ) ORDER BY name ASC;");



            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        public void InsertActionToRole(string RoleId = "", string AddActionId = "")
        {

            try
            {
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM role_action
                       WHERE  role_id='" + shared.db.addslashes(RoleId) + @"' AND  action_id='" 
                                         + shared.db.addslashes(AddActionId) + @"'");
                if (count != "0")
                {
                    shared.error.MarkUserError("This Action for this Role already exists ");
                    return;
                }
                string query = @"
            INSERT INTO role_action
		       SET role_id = '" + shared.db.addslashes(RoleId) + @"',
		           action_id = '" + shared.db.addslashes(AddActionId) + @"'";
                long id = shared.db.Insert(query);
                this.id = id.ToString();
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        public void DeleteFromRole(string RoleId, string DelActionId)
        {

            try
            {
                string count = shared.db.Scalar(
                    @"SELECT COUNT(*) 
                        FROM role_action
                       WHERE  role_id='" + shared.db.addslashes(RoleId) + @"' AND  action_id='"
                                         + shared.db.addslashes(DelActionId) + @"'");
                if (count == "0")
                {
                    shared.error.MarkUserError("This Action for this Role already not exists ");
                    return;
                }
                string query = @"
            DELETE FROM role_action
		       WHERE role_id = '" + shared.db.addslashes(RoleId) + @"' AND
		             action_id = '" + shared.db.addslashes(DelActionId) + @"'";
                int del = shared.db.Delete(query);
            
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }
        //@Html.Raw(@Html.ActionLink("[replacetext]", "DelActionInRole", "Cabinet", new { DelActionId = x["id"] }, new { @class = "btn btn-default", @title = "delete this action from role "+ Model.name }).ToHtmlString().Replace("[replacetext]", "<span class='glyphicon glyphicon-minus' style='color:red'>  </span>"))

    }
}