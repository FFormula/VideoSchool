using System;
using System.Data;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class RoleAction : BaseUnit
    {
        public Role role { get; private set; }

        public QTable selectActionsInRole { get; set; } // список действий для выбранной роли
        public QTable selectNewActionsForRole { get; set; } // список новых действий для роли (ещё не добавленных)

        public RoleAction()
            : this(null)
        {
        }

        public RoleAction(Shared shared)
            : base(shared)
        {
            selectActionsInRole = new QTable(shared);
            selectNewActionsForRole = new QTable(shared);
        }

        /// <summary>
        /// Select all action in current role
        /// </summary>
        public void SelectActionsInRole(string RoleID)
        {
            try
            {
                role = new Role(shared);
                role.Select(RoleID);
                if (shared.error.AnyError())
                    return;
                selectActionsInRole = new QTable(shared);
                string where =
                    " (role_id = '" + shared.db.addslashes(RoleID) + @"')";

                selectActionsInRole.Init(
                       @"SELECT COUNT(*) 
                           FROM role_action 
                          WHERE " + where,
                       @"SELECT ra.role_id,
                                ra.action_id,
                                r.name role_name, 
                                a.name action_name,
                                a.info action_info,
		                        a.status action_status
                           FROM role_action ra
                           JOIN role r
                             ON ra.role_id = r.id
                           JOIN action a 
  			                 ON ra.action_id = a.id
                          WHERE " + where + @"
                       ORDER BY a.name ASC");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
        /// Prepare a list of new (not yet added) Actions for specified Role
        /// </summary>
        /// <param name="RoleId"></param>
        public void SelectNewActionsForRole(string RoleId)
        {
            try
            {
                selectNewActionsForRole = new QTable(shared);
                selectNewActionsForRole.Init(
                    @"SELECT 1 ",
                    @"SELECT id, name
                        FROM action 
                    WHERE id NOT IN 
                             (SELECT action_id 
                                FROM role_action
                               WHERE role_id = '" + shared.db.addslashes(RoleId) + @"')
                    ORDER BY name ASC");
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
        /// Check, is there record in role_action table
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        protected bool ExistsRoleAction(string RoleId, string ActionId)
        {
            string count = shared.db.Scalar(
                @"SELECT COUNT(*) 
                    FROM role_action
                   WHERE role_id='" + shared.db.addslashes(RoleId) + @"' 
                     AND action_id='" + shared.db.addslashes(ActionId) + @"'");
            return (count != "0");
        }

        /// <summary>
        /// Add Relationship between Role and Action
        /// </summary>
        /// <param name="AddRoleId"></param>
        /// <param name="AddActionId"></param>
        public void InsertRoleAction(string AddRoleId = "", string AddActionId = "")
        {
            try
            {
                if (ExistsRoleAction(AddRoleId, AddActionId))
                {
                    shared.error.MarkUserError("This Action for this Role already exists ");
                    return;
                }
                string query = @"
                    INSERT INTO role_action
		               SET role_id = '" + shared.db.addslashes(AddRoleId) + @"',
		                   action_id = '" + shared.db.addslashes(AddActionId) + @"'";
                shared.db.Insert(query);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }
        }

        /// <summary>
        /// Delete relationship between specified role and action
        /// </summary>
        /// <param name="DelRoleId"></param>
        /// <param name="DelActionId"></param>
        public void DeleteRoleAction(string DelRoleId, string DelActionId)
        {
            try
            {
                if (!ExistsRoleAction(DelRoleId, DelActionId))
                {
                    shared.error.MarkUserError("Action for this Role does not exists already");
                    return;
                }
                string query = @"
                    DELETE FROM role_action
                     WHERE role_id='" + shared.db.addslashes(DelRoleId) + @"' 
                       AND action_id='" + shared.db.addslashes(DelActionId) + @"'";
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