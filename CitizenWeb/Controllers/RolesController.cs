// <copyright file=RolesController company="citizen">
// ©2020 City of Orlando;
//
// The copyright to the computer program(s) and source code herein is
// City of Orlando.
//  The program(s) and source code may be used and/or copied only with the
// written permission of the City of Orlando or
// in accordance with the terms and conditions stipulated in the
// agreement/contract under which the program(s) and source code have
// been supplied.
// </copyright>
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenWeb.BL;
using CitizenWeb.Models;
using CitizenWeb.DAL;
using Newtonsoft.Json;

namespace CitizenWeb.Controllers
{
    [Route("api/RolesController")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        /// <summary>Gets the list of all admin roles.</summary>
        /// <returns>List of all admin Roles.</returns>
        [Route("GetAllRoles")]
        [HttpGet]
        public List<AdminRoles> GetAllRoles()
        {
            Logging.LogDebugMessage("Method: GetAllRoles, MethodType: Get, Layer: RolesController, Parameters: No Input Parameters");
            using (RolesBL allRoles = new RolesBL())
            {
                return allRoles.GetAllRoles();
            }
        }

        /// <summary>Get the roles by identifier.</summary>
        /// <param name="roleId">The Integer Object.</param>
        /// <returns>Roles Object.</returns>
        [HttpGet]
        [Route("GetRolesById/{roleId}")]
        public AdminRoles GetRolesById(int roleId)
        {
            Logging.LogDebugMessage("Method: GetRolesById ,MethodType: Get, Layer: RolesController, Parameters: " + "roleId = " + roleId.ToString());
            using (RolesBL roleById = new RolesBL())
            {
                return roleById.GetRolesById(roleId);
            }
        }

        /// <summary>Get the roles by name.</summary>
        /// <param name="roleName">The String Object.</param>
        /// <returns>Roles Object.</returns>       
        [Route("GetRolesByName/{roleName}")]
        [HttpGet]
        public AdminRoles GetRolesByName(string roleName)
        {
            Logging.LogDebugMessage("Method: GetRolesByName ,MethodType: Get, Layer: RolesController, Parameters: " + "roleName = " + roleName);
            using (RolesBL roleByName = new RolesBL())
            {
                return roleByName.GetRolesByName(roleName);
            }
        }

        /// <summary>Gets the list of all active roles.</summary>
        /// <returns>List of all active roles.</returns>
        [Route("GetAllActiveRoles")]
        [HttpGet]
        public List<AdminRoles> GetAllActiveRoles()
        {
            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: RolesController, Parameters: No Input Parameters");
            using (RolesBL activeRoles = new RolesBL())
            {
                return activeRoles.GetAllActiveRoles();
            }
        }

        /// <summary>Inserts the role details.</summary>
        /// <param name="roles">The AdminRole Object.</param>
        /// <returns>The Integer Object.</returns>       
        [Route("InsertRole")]
        [HttpPost]
        public int InsertRole(AdminRoles roles)
        {
            Logging.LogDebugMessage("Method: InsertRole ,MethodType: Post, Layer: RolesController, Parameters: roles = " + JsonConvert.SerializeObject(roles));
            using (RolesBL insertRole = new RolesBL())
            {
                return insertRole.InsertRole(roles);
            }
        }

        /// <summary>Upadte The Role Details.</summary>
        /// <param name="role">The AdminRole Object.</param>
        /// <returns>The Boolean Value.</returns>       
        [Route("UpdateRole")]
        [HttpPost]
        public bool UpdateRole(AdminRoles role)
        {
            Logging.LogDebugMessage("Method: UpdateRole ,MethodType: Post, Layer: RolesController, Parameters:role = " + JsonConvert.SerializeObject(role));
            using (RolesBL updateRole = new RolesBL())
            {
                return updateRole.UpdateRole(role);
            }
        }

        /// <summary>Insert The Role and User.</summary>
        /// <param name="RoleList">The InserRoleUser Object.</param>
        /// <param name="CreatedByUserID">The Integer Object.</param>
        /// <returns>The Boolean Value.</returns>    
        [Route("UserRoleInsertBulk")]
        [HttpPost]
        public bool UserRoleInsertBulk(RoleObj roleObj)
        {
            Logging.LogDebugMessage("Method: UserRoleInsertBulk ,MethodType: Post, Layer: RolesController, Parameters: roleObj = " + JsonConvert.SerializeObject(roleObj));
            using (RolesBL userRoleInsert = new RolesBL())
            {
                List<InserRoleUser> insertroleuser = new List<InserRoleUser>();
                int CreatedByUserID = 0;
                insertroleuser = roleObj.Insertroleuser;
                CreatedByUserID = roleObj.CreateUserId;
                return userRoleInsert.UserRoleInsertBulk(insertroleuser, CreatedByUserID);
            }
        }

        /// <summary>Get the role exist by role name.</summary>
        /// <param name="roleName">The String Object.</param>
        /// <returns>Roles Object.</returns>
        [Route("GetRoleExistsByRoleName/{RoleName}")]
        [HttpGet]
        public bool GetRoleExistsByRoleName(string roleName)
        {
            Logging.LogDebugMessage("Method: GetRoleExistsByRoleName ,MethodType: Get, Layer: RolesController, Parameters: " + "roleName = " + roleName);
            using (RolesBL roleExistByRoleName = new RolesBL())
            {
                return roleExistByRoleName.GetRoleExistsByRoleName(roleName);
            }
        }
        [Route("DeleteRoles")]
        [HttpPost]
        /// <summary>This method deletes the selected roles.</summary>
        /// <param name="deletedRolesWithAdminUser">The DeletedRolesWithAdminUser Object.</param>
        /// <returns>List of Roles Object.</returns>
        public List<AdminRoles> DeleteRoles(DeletedRolesWithAdminUser deletedRolesWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteRoles, MethodType: Post, Layer: RolesController, Parameters: DeletedUsersWithAdminUser = " + JsonConvert.SerializeObject(deletedRolesWithAdminUser));
            using (RolesBL roleBL = new RolesBL())
            {
                return roleBL.DeleteRoles(deletedRolesWithAdminUser);
            }
        }
    }
}
