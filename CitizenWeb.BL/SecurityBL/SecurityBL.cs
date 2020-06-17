// <copyright file=SecurityBL company="citizen">
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

namespace CitizenWeb.BL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CitizenWeb.Models;
    using CitizenWeb.DAL;
    using Newtonsoft.Json;
    using System.Data.SqlClient;
    public class SecurityBL : IDisposable
    {
        private bool disposed = false;
        /// <summary>Gets the list of roles.</summary>
        /// <returns>List of AdminRole.</returns>
        public List<SecurityRole> GetRoles()
        {
            Logging.LogDebugMessage("Method: GetRoles, MethodType: Get, Layer: SecurityBL, Parameters: No Input Parameters");
            using (SecurityDAL roles = new SecurityDAL())
            {
                try
                {
                    return roles.GetRoles();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetRoles, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetRoles, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of all role privileges.</summary>
        /// <param name="roleId">The RoleID Object.</param>
        /// <returns>List of SecurityRole.</returns>
        public List<SecurityRole> GetAllRolePrivileges(RoleID roleId)
        {
            Logging.LogDebugMessage("Method: GetAllRolePrivileges, MethodType: Post, Layer: SecurityBL, Parameters: roleId = " + JsonConvert.SerializeObject(roleId));
            using (SecurityDAL rolePrivileges = new SecurityDAL())
            {
                try
                {
                    return rolePrivileges.GetAllRolePrivileges(roleId);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllRolePrivileges, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllRolePrivileges, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of all role privileges by role identifier.</summary>
        /// <param name="roleId">The Integer Object.</param>
        /// <returns>List of SecurityRole.</returns>
        public List<SecurityRole> GetAllRolePrivilegesByRoleID(int roleId)
        {
            Logging.LogDebugMessage("Method: GetAllRolePrivilegesByRoleID, MethodType: Get, Layer: SecurityBL, Parameters: roleId = " + roleId);
            using (SecurityDAL rolePrivilegesById = new SecurityDAL())
            {
                try
                {
                    return rolePrivilegesById.GetAllRolePrivilegesByRoleID(roleId);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllRolePrivilegesByRoleID, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllRolePrivilegesByRoleID, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of all privileges.</summary>
        /// <returns>List of AdminModules.</returns>
        public List<CSTMModule> GetAllPrivileges()
        {
            Logging.LogDebugMessage("Method: GetAllPrivileges, MethodType: Get, Layer: SecurityBL, Parameters: No Input Parameters");
            using (SecurityDAL allPrivileges = new SecurityDAL())
            {
                try
                {
                    return allPrivileges.GetAllPrivileges();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllPrivileges, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllPrivileges, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the List of all user roles by roleId.</summary>
        /// <param name="userid">The Int64 Object.</param>
        /// <returns>List of UserRoles.</returns>
        public List<UserRoles> GetAllUserRoles(Int64 userid)
        {
            Logging.LogDebugMessage("Method: GetAllUserRoles, MethodType: Get, Layer: SecurityBL, Parameters: userid = " + userid.ToString());
            using (SecurityDAL allUserRoles = new SecurityDAL())
            {
                try
                {
                    return allUserRoles.GetAllUserRoles(userid);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllUserRoles, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllUserRoles, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Insert or Update RolesPrivilege.</summary>
        /// <param name="privilegeList">The List of IntegerColumn Object.</param>
        /// <param name="role">The RolePrivilege Object.</param>
        /// <returns>Boolean Object.</returns>
        public bool RolePrivilegeUpsert(List<IntegerColumn> privilegeList, RolePrivilege role)
        {
            Logging.LogDebugMessage("Method: RolePrivilegeUpsert, MethodType: Post, Layer: SecurityBL, Parameters: privilegeList = " + JsonConvert.SerializeObject(privilegeList) + ",role = " + JsonConvert.SerializeObject(role));
            using (SecurityDAL rolePrivilegeUpsert = new SecurityDAL())
            {
                try
                {
                    return rolePrivilegeUpsert.RolePrivilegeUpsert(privilegeList, role);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: RolePrivilegeUpsert, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: RolePrivilegeUpsert, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Insert or Update UserRole.</summary>
        /// <param name="privilegeList">The List of AddRoleUser Object.</param>
        /// <param name="rolePrivilege">The RoleUserlist Object.</param>
        /// <returns>Boolean Object.</returns>
        public bool UserRoleUpsert(List<AddRoleUser> privilegeList, RoleUserlist rolePrivilege)
        {
            Logging.LogDebugMessage("Method: UserRoleUpsert, MethodType: Post, Layer: SecurityBL, Parameters: privilegelist = " + JsonConvert.SerializeObject(privilegeList) + ",rolePrivilege = " + JsonConvert.SerializeObject(rolePrivilege));
            using (SecurityDAL useRoleUpsert = new SecurityDAL())
            {
                try
                {
                    return useRoleUpsert.UserRoleUpsert(privilegeList, rolePrivilege);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: UserRoleUpsert, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: UserRoleUpsert, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of all active roles.</summary>
        /// <returns>List of AdminRole.</returns>
        public List<ActiveRoles> GetAllActiveRoles()
        {
            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: SecurityBL, Parameters: No Input Parameters");
            using (SecurityDAL activeRoles = new SecurityDAL())
            {
                try
                {
                    return activeRoles.GetAllActiveRoles();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of all modules.</summary>
        /// <returns>List of AdminModule.</returns>
        public List<Module> GetAllmodules()
        {
            Logging.LogDebugMessage("Method: GetAllModules, MethodType: Get, Layer: SecurityBL, Parameters: No Input Parameters");
            using (SecurityDAL allModules = new SecurityDAL())
            {
                try
                {
                    return allModules.GetAllmodules();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllModules, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllModules, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets list of all pages.</summary>
        /// <returns>List of AdminPages.</returns>
        public List<Page> GetAllPages()
        {
            Logging.LogDebugMessage("Method: GetAllPages, MethodType: Get, Layer: SecurityBL, Parameters: No Input Parameters");
            using (SecurityDAL allPages = new SecurityDAL())
            {
                try
                {
                    return allPages.GetAllPages();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllPages, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllPages, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Insert the UserRole in bulk.</summary>
        /// <param name="privilegeList">The List of InsertRoleUser Objects.</param>
        /// <param name="createdByUserID">The Integer Object for CreatedByUserId.</param>
        /// <param name="updatedByUserID">The Integer Object for UpdatedByUserId.</param>
        /// <returns>Boolean Object.</returns>
        public bool UserRoleInsertBulk(List<InsertRoleUser> privilegeList, int createdByUserID, int updatedByUserID)
        {
            Logging.LogDebugMessage("Method: UserRoleInsertBulk, MethodType: Post, Layer: SecurityBL, Parameters: privilegeList = " + JsonConvert.SerializeObject(privilegeList) + ",createdByUserID = " + createdByUserID.ToString() + ",updatedByUserID = " + updatedByUserID.ToString());
            using (SecurityDAL userRoleInsert = new SecurityDAL())
            {
                try
                {
                    return userRoleInsert.UserRoleInsertBulk(privilegeList, createdByUserID, updatedByUserID);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of user roles by UserId.</summary>
        /// <param name="userId">The Long object.</param>
        /// <returns>List of UserRoles.</returns>
        public List<UserRoles> GetUserRoles(Int64 userId)
        {
            Logging.LogDebugMessage("Method: GetUserRoles, MethodType: Get, Layer: SecurityBL, Parameters: userId = " + userId.ToString());
            using (SecurityDAL userRoles = new SecurityDAL())
            {
                try
                {
                    return userRoles.GetUserRoles(userId);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetUserRoles, Layer: SecurityBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetUserRoles, Layer: SecurityBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>
        /// This method disposing unmanaged code like Stream/File etc.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.marshalbyvaluecomponent.dispose?view=netframework-4.8#System_ComponentModel_MarshalByValueComponent_Dispose.
        /// </summary>
        /// <param name="disposing">Dispose Parameter is going to be only used in case to free up the unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
