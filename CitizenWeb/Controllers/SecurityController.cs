// <copyright file=SecurityController company="citizen">
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CitizenWeb.BL;
using CitizenWeb.Models;
using CitizenWeb.DAL;
using Newtonsoft.Json;

namespace CitizenWeb.Controllers
{
    /// <summary>SecurityController.This controller contains all the methods which are related to Setting and Getting security based on roles for users.</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/SecurityController")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        /// <summary>Gets the list of roles.</summary>
        /// <returns>List of AdminRole.</returns>
        [Route("GetRoles")]
        [HttpGet]
        public List<SecurityRole> GetRoles()
        {
            Logging.LogDebugMessage("Method: GetRoles, MethodType: Get, Layer: SecurityController, Parameters: No Input Parameters");
            using (SecurityBL roles = new SecurityBL())
            {
                return roles.GetRoles();
            }
        }

        /// <summary>Gets the list of all role privileges.</summary>
        /// <param name="roleId">The RoleID Object.</param>
        /// <returns>List of SecurityRole.</returns>
        [Route("GetAllRolePrivileges")]
        [HttpPost]
        public List<SecurityRole> GetAllRolePrivileges(RoleID roleId)
        {
            Logging.LogDebugMessage("Method: GetAllRolePrivileges, MethodType: Post, Layer: SecurityController, Parameters: roleId = " + JsonConvert.SerializeObject(roleId));
            using (SecurityBL rolePrivileges = new SecurityBL())
            {
                return rolePrivileges.GetAllRolePrivileges(roleId);
            }
        }

        /// <summary>Gets the list of all role privileges by role identifier.</summary>
        /// <param name="roleId">The Integer Object.</param>
        /// <returns>List of SecurityRole.</returns>
        [Route("GetAllRolePrivilegesByRoleID/{roleId}")]
        [HttpGet]
        public List<SecurityRole> GetAllRolePrivilegesByRoleID(int roleId)
        {
            Logging.LogDebugMessage("Method: GetAllRolePrivilegesByRoleID, MethodType: Get, Layer: SecurityController, Parameters: roleId = " + roleId);
            using (SecurityBL rolePrivilegesById = new SecurityBL())
            {
                return rolePrivilegesById.GetAllRolePrivilegesByRoleID(roleId);
            }
        }

        /// <summary>Gets the list of all privileges.</summary>
        /// <returns>List of AdminModules.</returns>
        [Route("GetAllPrivileges")]
        [HttpGet]
        public List<CSTMModule> GetAllPrivileges()
        {
            Logging.LogDebugMessage("Method: GetAllPrivileges, MethodType: Get, Layer: SecurityController, Parameters: No Input Parameters");
            using (SecurityBL allPrivileges = new SecurityBL())
            {
                return allPrivileges.GetAllPrivileges();
            }
        }

        /// <summary>Gets the List of all user roles by roleId.</summary>
        /// <param name="userid">The Integer Object.</param>
        /// <returns>List of UserRoles.</returns>
        [Route("GetAllUserRoles/{userid}")]
        [HttpGet]
        public List<UserRoles> GetAllUserRoles(int userid)
        {
            Logging.LogDebugMessage("Method: GetAllUserRoles, MethodType: Get, Layer: SecurityController, Parameters: userid = " + userid.ToString());
            using (SecurityBL allUserRoles = new SecurityBL())
            {
                return allUserRoles.GetAllUserRoles(userid);
            }
        }

        /// <summary>Insert or Update RolesPrivilege.</summary>
        /// <param name="privilegeObj">The PrivilegeObj Object.</param>
        /// <returns>Boolean Object.</returns>
        [Route("RolePrivilegeUpsert")]
        [HttpPost]
        public bool RolePrivilegeUpsert(PrivilegeObj privilegeObj)
        {
            Logging.LogDebugMessage("Method: RolePrivilegeUpsert, MethodType: Post, Layer: SecurityController, Parameters: privilegeObj = " + JsonConvert.SerializeObject(privilegeObj));
            using (SecurityBL rolePrivilegeUpsert = new SecurityBL())
            {
                RolePrivilege roleobj = new RolePrivilege();
                roleobj = privilegeObj.roleslist;
                List<IntegerColumn> privilegelist = new List<IntegerColumn>();
                privilegelist = privilegeObj.privilegelist;
                return rolePrivilegeUpsert.RolePrivilegeUpsert(privilegelist, roleobj);
            }
        }

        /// <summary>Insert or Update UserRole.</summary>
        /// <param name="userRoleList">The AddUserRoleObj Object.</param>
        /// <returns>Boolean Object.</returns>
        [Route("UserRoleUpsert")]
        [HttpPost]
        public bool UserRoleUpsert(AddUserRoleObj UserRoleList)
        {
            Logging.LogDebugMessage("Method: UserRoleUpsert, MethodType: Post, Layer: SecurityController, Parameters: userRoleList = " + JsonConvert.SerializeObject(UserRoleList));
            using (SecurityBL userRoleUpsert = new SecurityBL())
            {
                RoleUserlist roleobj = new RoleUserlist();
                roleobj = UserRoleList.roleuserlist;
                List<AddRoleUser> userRoleList = new List<AddRoleUser>();
                userRoleList = UserRoleList.Addroleuser;
                return userRoleUpsert.UserRoleUpsert(userRoleList, roleobj);
            }
        }

        /// <summary>Gets the list of all active roles.</summary>
        /// <returns>List of AdminRole.</returns>
        [Route("GetAllActiveRoles")]
        [HttpGet]
        public List<ActiveRoles> GetAllActiveRoles()
        {
            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: SecurityController, Parameters: No Input Parameters");
            using (SecurityBL activeRoles = new SecurityBL())
            {
                return activeRoles.GetAllActiveRoles();
            }
        }

        /// <summary>Gets the list of all modules.</summary>
        /// <returns>List of AdminModule.</returns>
        [Route("GetAllmodules")]
        [HttpGet]
        public List<Module> GetAllmodules()
        {
            Logging.LogDebugMessage("Method: GetAllModules, MethodType: Get, Layer: SecurityController, Parameters: No Input Parameters");
            using (SecurityBL allModules = new SecurityBL())
            {
                return allModules.GetAllmodules();
            }
        }

        /// <summary>Gets list of all pages.</summary>
        /// <returns>List of AdminPages.</returns>
        [Route("GetAllPages")]
        [HttpGet]
        public List<Page> GetAllPages()
        {
            Logging.LogDebugMessage("Method: GetAllPages, MethodType: Get, Layer: SecurityController, Parameters: No Input Parameters");
            using (SecurityBL allPages = new SecurityBL())
            {
                return allPages.GetAllPages();
            }
        }

        /// <summary>Gets the list of user roles by UserId.</summary>
        /// <param name="userId">The Integer object.</param>
        /// <returns>List of UserRoles.</returns>
        [Route("GetUserRoles/{userId}")]
        [HttpGet]
        public List<UserRoles> GetUserRoles(int userId)
        {
            Logging.LogDebugMessage("Method: GetUserRoles, MethodType: Get, Layer: SecurityController, Parameters: userId = " + userId.ToString());
            using (SecurityBL userRoles = new SecurityBL())
            {
                return userRoles.GetUserRoles(userId);
            }
        }

        /// <summary>Insert the UserRole in bulk.</summary>
        /// <param name="securityRoleObj">The RoleObj Object.</param>
        /// <returns>Boolean Object.</returns>
        [Route("UserRoleInsertBulk")]
        [HttpPost]
        public bool UserRoleInsertBulk(SecurityRoleObj securityRoleObj)
        {
            Logging.LogDebugMessage("Method: UserRoleInsertBulk, MethodType: Post, Layer: SecurityController, Parameters: securityRoleObj = " + JsonConvert.SerializeObject(securityRoleObj));
            using (SecurityBL userRoleInsert = new SecurityBL())
            {
                List<InsertRoleUser> insertroleuser = new List<InsertRoleUser>();
                int CreatedByUserID = 0;
                int UpdatedByUserID = 0;
                insertroleuser = securityRoleObj.insertroleuser;
                CreatedByUserID = securityRoleObj.CreatedByUserID;
                UpdatedByUserID = securityRoleObj.UpdatedByUserID;
                return userRoleInsert.UserRoleInsertBulk(insertroleuser, CreatedByUserID, UpdatedByUserID);
            }
        }
    }
}
