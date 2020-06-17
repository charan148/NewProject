// <copyright file=RolesBL company="citizen">
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
    using Newtonsoft.Json;
    using CitizenWeb.DAL;
    using CitizenWeb.Models;
    using System.Data.SqlClient;
    public class RolesBL : IDisposable
    {
        private bool disposed = false;
        /// <summary>Gets the list of all admin roles.</summary>
        /// <returns>List of all admin Roles.</returns>
        public List<AdminRoles> GetAllRoles()
        {
            Logging.LogDebugMessage("Method: GetAllRoles, MethodType: Get, Layer: RolesBL, Parameters: No Input Parameters");
            using (RolesDAL allRoles = new RolesDAL())
            {
                try
                {
                    return allRoles.GetAllRoles();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllRoles, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllRoles, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>Gets the list of all active roles.</summary>
        /// <returns>List of all active roles.</returns>
        public List<AdminRoles> GetAllActiveRoles()
        {
            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: RolesBL, Parameters: No Input Parameters");
            using (RolesDAL activeRoles = new RolesDAL())
            {
                try
                {
                    return activeRoles.GetAllActiveRoles();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>Get the roles by identifier.</summary>
        /// <param name="roleId">The Integer Object.</param>
        /// <returns>Roles Object.</returns>
        public AdminRoles GetRolesById(int roleId)
        {
            Logging.LogDebugMessage("Method: GetRolesById ,MethodType: Get, Layer: RolesBL, Parameters: roleId = " + roleId.ToString());
            using (RolesDAL roleById = new RolesDAL())
            {
                try
                {
                    return roleById.GetRolesById(roleId);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetRolesById, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetRolesById, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>Get the roles by name.</summary>
        /// <param name="roleName">The String Object.</param>
        /// <returns>Roles Object.</returns>
        public AdminRoles GetRolesByName(string roleName)
        {
            Logging.LogDebugMessage("Method: GetRolesByName, MethodType: Get, Layer: RolesBL, Parameters: roleName = " + roleName);
            using (RolesDAL roleByName = new RolesDAL())
            {
                try
                {
                    return roleByName.GetRolesByName(roleName);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetRolesByName, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetRolesByName, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>Inserts the role details.</summary>
        /// <param name="roles">The AdminRole Object.</param>
        /// <returns>The Integer Object.</returns>
        public int InsertRole(AdminRoles roles)
        {
            Logging.LogDebugMessage("Method: InsertRole ,MethodType: Post, Layer: RolesBL, Parameters: roles = " + JsonConvert.SerializeObject(roles));
            using (RolesDAL insertRole = new RolesDAL())
            {
                try
                {
                    return insertRole.InsertRole(roles);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: InsertRole, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: InsertRole, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>Upadte The Role Details.</summary>
        /// <param name="role">The AdminRole Object.</param>
        /// <returns>The Boolean Value.</returns>
        public bool UpdateRole(AdminRoles role)
        {
            Logging.LogDebugMessage("Method: UpdateRole, MethodType: Post, Layer: RolesBL, Parameters: role = " + JsonConvert.SerializeObject(role));
            using (RolesDAL updateRole = new RolesDAL())
            {
                try
                {
                    return updateRole.UpdateRole(role);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: UpdateRole, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: UpdateRole, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Insert The Role and User.</summary>
        /// <param name="RoleList">The InserRoleUser Object.</param>
        /// <param name="CreatedByUserID">The Integer Object.</param>
        /// <returns>The Boolean Value.</returns>
        public bool UserRoleInsertBulk(List<InserRoleUser> Insertroleuser, int CreatedByUserID)
        {
            Logging.LogDebugMessage("Method: UserRoleInsertBulk ,MethodType: Post, Layer: RolesBL, Parameters: Insertroleuser = " + JsonConvert.SerializeObject(Insertroleuser));
            using (RolesDAL userRoleInsert = new RolesDAL())
            {
                try
                {
                    return userRoleInsert.UserRoleInsertBulk(Insertroleuser, CreatedByUserID);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: RolesBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>This method deletes the selected roles.</summary>
        /// <param name="deletedRolesWithAdminUser">The DeletedRolesWithAdminUser Object.</param>
        /// <returns>List of Roles Object.</returns>
        public List<AdminRoles> DeleteRoles(DeletedRolesWithAdminUser deletedRolesWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteRoles, MethodType: Post, Layer: RolesBL, Parameters: DeletedRolesWithAdminUser = " + JsonConvert.SerializeObject(deletedRolesWithAdminUser));
            using (RolesDAL rolesDAL = new RolesDAL())
            {
                try
                {
                    return rolesDAL.DeleteRoles(deletedRolesWithAdminUser);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: DeleteRoles, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: DeleteRoles, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Get the role exist by role name.</summary>
        /// <param name="roleName">The String Object.</param>
        /// <returns>Roles Object.</returns>
        public bool GetRoleExistsByRoleName(string roleName)
        {
            Logging.LogDebugMessage("Method: GetRoleExistsByRoleName, MethodType: Get, Layer: RolesBL, Parameters: roleName = " + roleName);
            using (RolesDAL roleExistByRoleName = new RolesDAL())
            {
                try
                {
                    return roleExistByRoleName.GetRoleExistsByRoleName(roleName);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: GetRoleExistsByRoleName, Layer: RolesBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: GetRoleExistsByRoleName, Layer: RolesBL, Stack Trace: " + ex.ToString());
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
