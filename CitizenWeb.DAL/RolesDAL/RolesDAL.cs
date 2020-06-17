// <copyright file=RoleDAL company="citizen">
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
namespace CitizenWeb.DAL
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using CitizenWeb.Models;
    using Newtonsoft.Json;
    /// <summary>RolesDAL.This data access layer contains all the methods which are related to Setting and Getting Alerts.</summary>
    public class RolesDAL : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        string connectionString { get; set; }


        public RolesDAL()
        {
            this.connectionString = AppSettings.ConnectionString.ToString();

        }
        /// <summary>Gets the list of all admin roles.</summary>
        /// <returns>List of all admin Roles.</returns>
        public List<AdminRoles> GetAllRoles()
        {
            Logging.LogDebugMessage("Method: GetAllRoles, MethodType: Get, Layer: RolesDAL, Parameters: No Input Parameters");
            var dataSet = new DataSet();
            var allRoles = new List<AdminRoles>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleSelect";
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            allRoles = EntityCollectionHelper.ConvertTo<AdminRoles>(dataSet.Tables[0]).ToList();
                            Logging.LogDebugMessage("Method: GetAllRoles, MethodType: Get, Layer: RolesDAL, returnRolesList:" + JsonConvert.SerializeObject(allRoles));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllRoles, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllRoles, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return allRoles;
        }
        /// <summary>Gets the list of all active roles.</summary>
        /// <returns>List of all active roles.</returns>
        public List<AdminRoles> GetAllActiveRoles()
        {
            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: RolesDAL, Parameters: No Input Parameters");
            var dataSet = new DataSet();
            var allActiveRoles = new List<AdminRoles>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@ExcludeInactive", Value = 1 });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            allActiveRoles = EntityCollectionHelper.ConvertTo<AdminRoles>(dataSet.Tables[0]).ToList();
                            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: RolesDAL, returnRolesList:" + JsonConvert.SerializeObject(allActiveRoles));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return allActiveRoles;

        }
        /// <summary>Get the roles by identifier.</summary>
        /// <param name="roleId">The Integer Object.</param>
        /// <returns>Roles Object.</returns>
        public AdminRoles GetRolesById(int roleId)
        {
            Logging.LogDebugMessage("Method: GetRolesById, MethodType: Get, Layer: RolesDAL, Parameters: roleId = " + roleId.ToString());
            var dataSet = new DataSet();
            AdminRoles roles = new AdminRoles();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = roleId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            roles = EntityCollectionHelper.ConvertTo<AdminRoles>(dataSet.Tables[0]).FirstOrDefault();
                            Logging.LogDebugMessage("Method: GetRolesById, MethodType: Get, Layer: RolesDAL, returnRolesById:" + JsonConvert.SerializeObject(roles));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRolesById, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRolesById, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return roles;
        }
        /// <summary>Get the roles by name.</summary>
        /// <param name="roleName">The String Object.</param>
        /// <returns>Roles Object.</returns>
        public AdminRoles GetRolesByName(string roleName)
        {
            Logging.LogDebugMessage("Method: GetRolesByName, MethodType: Get, Layer: RolesDAL, Parameters: roleName = " + roleName);
            var dataSet = new DataSet();
            AdminRoles roles = new AdminRoles();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleName", Value = roleName });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            roles = EntityCollectionHelper.ConvertTo<AdminRoles>(dataSet.Tables[0]).FirstOrDefault();
                            Logging.LogDebugMessage("Method: GetRolesByName, MethodType: Get, Layer: RolesDAL, returnRolesList:" + JsonConvert.SerializeObject(roles));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRolesByName, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRolesByName, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return roles;
        }
        /// <summary>Inserts the role details.</summary>
        /// <param name="roles">The AdminRole Object.</param>
        /// <returns>The Integer Object.</returns>
        public int InsertRole(AdminRoles roles)
        {
            Logging.LogDebugMessage("Method: InsertRole, MethodType: Post, Layer: RolesDAL, Parameters: roles = " + JsonConvert.SerializeObject(roles));
            var dataset = new DataSet();
            int insertValue = 0;

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleInsert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleName", Value = roles.RoleName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsActive", Value = roles.IsActive });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = roles.CreateUserId });

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = roles.RoleId });
                    command.Parameters["@RoleID"].Direction = ParameterDirection.Output;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            insertValue = (int)command.Parameters["@RoleID"].Value;
                            //Audits audit = new Audits
                            //{
                            //    MainPageName = "Roles",
                            //    PageTabName = "Roles",
                            //    ActionName = "Save",
                            //    UserID = role.CreateUserId,
                            //    RecordID = val
                            //};
                            //AuditDAL auditobj = new AuditDAL();
                            //auditobj.SaveAuditInfo(audit);

                        }
                        Logging.LogDebugMessage("Method: InsertRole, MethodType: Post, Layer: RolesDAL, returnRoleId:" + roles.RoleId.ToString());
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: InsertRole, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: InsertRole, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return insertValue;
        }
        /// <summary>Upadte The Role Details.</summary>
        /// <param name="role">The AdminRole Object.</param>
        /// <returns>The Boolean Value.</returns>
        public bool UpdateRole(AdminRoles role)
        {
            Logging.LogDebugMessage("Method: UpdateRole ,MethodType: Post, Layer: RolesDAL, Parameters: role = " + JsonConvert.SerializeObject(role));
            var dataSet = new DataSet();
            bool checkUpdateValue = false;

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleName", Value = role.RoleName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsActive", Value = role.IsActive });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UpdatedByUserID", Value = role.UpdateUserId });

                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = role.RoleId });
                    //command.Parameters["@RoleID"].Direction = ParameterDirection.Output;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            checkUpdateValue = true;
                            //Audits audit = new Audits
                            //{
                            //    MainPageName = "Roles",
                            //    PageTabName = "Roles",
                            //    ActionName = "Save",
                            //    UserID = role.UpdateUserId,
                            //    RecordID = role.RoleId
                            //};
                            //AuditDAL auditobj = new AuditDAL();
                            //auditobj.SaveAuditInfo(audit);

                        }
                        Logging.LogDebugMessage("Method: UpdateRole, MethodType: Post, Layer: RolesDAL, returnStatus:" + checkUpdateValue.ToString());
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: UpdateRole, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: UpdateRole, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return checkUpdateValue;
        }
        /// <summary>This method deletes the selected roles.</summary>
        /// <param name="deletedRolesWithAdminUser">The DeletedRolesWithAdminUser Object.</param>
        /// <returns>List of Roles Object.</returns>
        public List<AdminRoles> DeleteRoles(DeletedRolesWithAdminUser deletedRolesWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteUsers, MethodType: Post, Layer: RolesDAL, Parameters: DeletedRolesWithAdminUser = " + JsonConvert.SerializeObject(deletedRolesWithAdminUser));
            try
            {
                using (var command = new SqlCommand())
                {
                    var dataSet = new DataSet();
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleDelete";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleIDs", Value = deletedRolesWithAdminUser.RoleIds });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AdminUserID", Value = deletedRolesWithAdminUser.AdminUserId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteRoles, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteRoles, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return this.GetAllRoles();
        }

        /// <summary>Insert The Role and User.</summary>
        /// <param name="RoleList">The InserRoleUser Object.</param>
        /// <param name="CreatedByUserID">The Integer Object.</param>
        /// <returns>The Boolean Value.</returns>
        public bool UserRoleInsertBulk(List<InserRoleUser> RoleList, int CreatedByUserID)
        {
            Logging.LogDebugMessage("Method: UserRoleInsertBulk ,MethodType: Post, Layer: RolesDAL, Parameters: RoleList = " + JsonConvert.SerializeObject(RoleList));
            var checkInsertValue = false;
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataTable = EntityCollectionHelper.ConvertTo<InserRoleUser>(RoleList);

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_UserRoleInsertBulk";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = CreatedByUserID });
                    SqlParameter structuredParam = new SqlParameter("@UserRoleTable", SqlDbType.Structured);
                    structuredParam.Value = dataTable;
                    command.Parameters.Add(structuredParam);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            checkInsertValue = true;
                        }
                        else
                        {
                            checkInsertValue = false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return checkInsertValue;
        }

        /// <summary>Get the role exist by role name.</summary>
        /// <param name="roleName">The String Object.</param>
        /// <returns>Roles Object.</returns>
        public bool GetRoleExistsByRoleName(string roleName)
        {
            Logging.LogDebugMessage("Method: GetRoleExistsByRoleName, MethodType: Get, Layer: RolesDAL, Parameters: roleName = " + roleName);
            var checkroleexist = false;
            var dataset = new DataSet();
            var dataTable = new DataTable();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_GetRoleExistsByRoleName";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleName", Value = roleName });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            checkroleexist = (Boolean)dataset.Tables[0].Rows[0].ItemArray[0];
                            dataTable = dataset.Tables[0];

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRoleExistsByRoleName, Layer: RolesDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRoleExistsByRoleName, Layer: RolesDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return checkroleexist;
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
