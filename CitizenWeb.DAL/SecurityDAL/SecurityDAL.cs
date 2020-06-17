// <copyright file=SecurityDAL company="citizen">
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
    using CitizenWeb.Models;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Newtonsoft.Json;
    /// <summary>SecurityDAL. this access layer contains methods to get security related data.</summary>
    public class SecurityDAL : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        string connectionString { get; set; }


        public SecurityDAL()
        {
            this.connectionString = AppSettings.ConnectionString.ToString();

        }

        /// <summary>Gets the list of roles.</summary>
        /// <returns>List of AdminRole.</returns>
        public List<SecurityRole> GetRoles()
        {
            Logging.LogDebugMessage("Method: GetRoles, MethodType: Get, Layer: SecurityDAL, Parameters: No Input Parameters");
            //int roleId = 2;
            var dataSet = new DataSet();
            var adminRolesList = new List<SecurityRole>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RoleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID" });
                    // command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = 2 });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            adminRolesList = EntityCollectionHelper.ConvertTo<SecurityRole>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRoles, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRoles, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return adminRolesList;

        }

        /// <summary>Gets the list of all role privileges.</summary>
        /// <param name="role">The RoleID Object.</param>
        /// <returns>List of SecurityRole.</returns>
        public List<SecurityRole> GetAllRolePrivileges(RoleID role)
        {
            Logging.LogDebugMessage("Method: GetAllRolePrivileges, MethodType: Post, Layer: SecurityDAL, Parameters: roleId = " + JsonConvert.SerializeObject(role));
            var dataSet = new DataSet();
            var adminRolesList = new List<SecurityRole>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RolePrivilegeSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = role.RoleId });
                    // command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = 2 });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {

                            adminRolesList = EntityCollectionHelper.ConvertTo<SecurityRole>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllRolePrivileges, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllRolePrivileges, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return adminRolesList;
        }

        /// <summary>Gets the list of all role privileges by role identifier.</summary>
        /// <param name="roleId">The Integer Object.</param>
        /// <returns>List of SecurityRole.</returns>
        public List<SecurityRole> GetAllRolePrivilegesByRoleID(int roleId)
        {
            Logging.LogDebugMessage("Method: GetAllRolePrivilegesByRoleID, MethodType: Get, Layer: SecurityDAL, Parameters: roleIds = " + roleId.ToString());
            var dataSet = new DataSet();
            var adminRolesList = new List<SecurityRole>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RolePrivilegeSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = roleId });
                    // command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = 2 });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {

                            adminRolesList = EntityCollectionHelper.ConvertTo<SecurityRole>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllRolePrivilegesByRoleID, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllRolePrivilegesByRoleID, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return adminRolesList;
        }

        /// <summary>Gets the list of all privileges.</summary>
        /// <returns>List of AdminModules.</returns>
        public List<CSTMModule> GetAllPrivileges()
        {
            Logging.LogDebugMessage("Method: GetAllPrivileges, MethodType: Get, Layer: SecurityDAL, Parameters: No Input Parameters");
            DataSet ds_result_modules = new DataSet();
            DataSet ds_result_privilege = new DataSet();

            DataSet ds_result_page = new DataSet();

            var Modules = new List<CSTMModule>();
            var Pages = new List<CSTMpage>();
            var PagePrivileges = new List<CSTMPrivilege>();

            try
            {
                using (var command = new SqlCommand())
            {

                try
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_ModuleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds_result_modules);
                        if (ds_result_modules != null && ds_result_modules.Tables.Count > 0)
                        {

                            Modules = EntityCollectionHelper.ConvertTo<CSTMModule>(ds_result_modules.Tables[0]).ToList();

                        }
                    }


                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Logging Error Message for GetAllPrivileges of Modules " + ex.Message);
                    throw ex;
                }
            }
            using (var command = new SqlCommand())
            {

                try
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_PageSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@PageID" });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds_result_page);
                        if (ds_result_page.Tables.Count > 0 && ds_result_page.Tables[0] != null)
                        {

                            Pages = EntityCollectionHelper.ConvertTo<CSTMpage>(ds_result_page.Tables[0]).ToList();

                        }
                    }


                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Logging Error Message for Get AllPrivileges of Pages " + ex.Message);
                    throw ex;
                }
            }

            using (var command = new SqlCommand())
            {

                try
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_PrivilegeSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds_result_privilege);
                        if (ds_result_privilege.Tables.Count > 0 && ds_result_privilege.Tables[0] != null)
                        {

                            PagePrivileges = EntityCollectionHelper.ConvertTo<CSTMPrivilege>(ds_result_privilege.Tables[0]).ToList();

                        }
                    }


                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Logging Error Message for Get AllPrivileges for Privilege Select " + ex.Message);
                    throw ex;
                }
            }

            foreach (CSTMModule mod in Modules)
            {
                List<CSTMpage> ModPages = Pages.Where(p => p.ModuleId == mod.ModuleID).ToList();
                foreach (CSTMpage pag in ModPages)
                {
                    List<CSTMPrivilege> ModPagePrivileges = PagePrivileges.Where(p => p.PageId == pag.PageID).ToList();
                    if (ModPagePrivileges != null && ModPagePrivileges.Count > 0)
                    {
                        pag.Privileges = ModPagePrivileges;
                    }
                }
                if (ModPages != null && ModPages.Count > 0)
                {
                    mod.Pages = ModPages;
                }
            }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllPrivileges, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllPrivileges, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return Modules;
        }

        /// <summary>Gets the List of all user roles by roleId.</summary>
        /// <param name="userid">The Int64 Object.</param>
        /// <returns>List of UserRoles.</returns>
        public List<UserRoles> GetAllUserRoles(Int64 roleId)
        {
            Logging.LogDebugMessage("Method: GetAllUserRoles, MethodType: Get, Layer: SecurityDAL, Parameters: roleId = " + roleId.ToString());
            // int userid = 2;
            var dataSet = new DataSet();
            var userRoles = new List<UserRoles>();

            try
            {
                using (var command = new SqlCommand())
                {
                    Int64? userid = null;
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_UserRoleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = userid });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = roleId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            userRoles = EntityCollectionHelper.ConvertTo<UserRoles>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllUserRoles, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllUserRoles, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return userRoles;

        }

        /// <summary>Insert or Update RolesPrivilege.</summary>
        /// <param name="privilegeList">The List of IntegerColumn Object.</param>
        /// <param name="role">The RolePrivilege Object.</param>
        /// <returns>Boolean Object.</returns>
        public bool RolePrivilegeUpsert(List<IntegerColumn> privilegeList, RolePrivilege role)
        {
            Logging.LogDebugMessage("Method: RolePrivilegeUpsert, MethodType: Post, Layer: SecurityDAL, Parameters: privilegeList = " + JsonConvert.SerializeObject(privilegeList) + ",role = " + JsonConvert.SerializeObject(role));
            bool checkRolePrivilege = false;
            DataSet dataSet = new DataSet();
            var res = new List<IntegerColumn>();
            DataTable dataTable = new DataTable();
            dataTable = EntityCollectionHelper.ConvertTo<IntegerColumn>(privilegeList);

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_RolePrivilegeUpsert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = role.RoleId });
                    // command.Parameters.Add(new SqlParameter() { ParameterName = "@PrivilegeTable", Value = dataTable });
                    SqlParameter structuredParam = new SqlParameter("@PrivilegeTable", SqlDbType.Structured);
                    structuredParam.Value = dataTable;
                    command.Parameters.Add(structuredParam);
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UpdatedByUserID", Value = role.UpdateUserId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            checkRolePrivilege = true;
                        }
                        else
                        {
                            checkRolePrivilege = false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: RolePrivilegeUpsert, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: RolePrivilegeUpsert, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return checkRolePrivilege;
        }

        /// <summary>Insert or Update UserRole.</summary>
        /// <param name="privilegeList">The List of AddRoleUser Object.</param>
        /// <param name="role">The RoleUserlist Object.</param>
        /// <returns>Boolean Object.</returns>
        public bool UserRoleUpsert(List<AddRoleUser> privilegeList, RoleUserlist role)
        {
            Logging.LogDebugMessage("Method: UserRoleUpsert, MethodType: Post, Layer: SecurityDAL, Parameters: privilegeList = " + JsonConvert.SerializeObject(privilegeList) + ",role = " + JsonConvert.SerializeObject(role));
            bool checkUserRole = false;
            DataSet dataSet = new DataSet();
            var res = new List<AddRoleUser>();
            DataTable dataTable = new DataTable();
            dataTable = EntityCollectionHelper.ConvertTo<AddRoleUser>(privilegeList);

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_UserRoleUpsert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = role.RoleId });
                    //command.Parameters.Add(new SqlParameter() { ParameterName = "@PrivilegeTable", Value = dataTable });
                    SqlParameter structuredParam = new SqlParameter("@UserTable", SqlDbType.Structured);
                    structuredParam.Value = dataTable;
                    command.Parameters.Add(structuredParam);
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UpdatedByUserID", Value = role.UpdateUserId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            checkUserRole = true;
                        }
                        else
                        {
                            checkUserRole = false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: UserRoleUpsert, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: UserRoleUpsert, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return checkUserRole;
        }

        /// <summary>Gets the list of all active roles.</summary>
        /// <returns>List of AdminRole.</returns>
        public List<ActiveRoles> GetAllActiveRoles()
        {
            Logging.LogDebugMessage("Method: GetAllActiveRoles, MethodType: Get, Layer: SecurityDAL, Parameters: No Input Parameters");
            // int userid = 2;
            var dataSet = new DataSet();
            var adminRole = new List<ActiveRoles>();

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

                            adminRole = EntityCollectionHelper.ConvertTo<ActiveRoles>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllActiveRoles, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return adminRole;

        }

        /// <summary>Gets the list of all modules.</summary>
        /// <returns>List of AdminModule.</returns>
        public List<Module> GetAllmodules()
        {
            Logging.LogDebugMessage("Method: GetAllModules, MethodType: Get, Layer: SecurityDAL, Parameters: No Input Parameters");
            var dataset = new DataSet();
            var adminModule = new List<Module>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_ModuleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add(new SqlParameter() { ParameterName = "@ExcludeInactive", Value = 1 });
                    // command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = 2 });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null && dataset.Tables.Count > 0)
                        {

                            adminModule = EntityCollectionHelper.ConvertTo<Module>(dataset.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllModules, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllModules, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return adminModule;

        }

        /// <summary>Gets list of all pages.</summary>
        /// <returns>List of AdminPages.</returns>
        public List<Page> GetAllPages()
        {
            Logging.LogDebugMessage("Method: GetAllPages, MethodType: Get, Layer: SecurityDAL, Parameters: No Input Parameters");
            var dataSet = new DataSet();
            var adminPages = new List<Page>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_PageSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add(new SqlParameter() { ParameterName = "@ExcludeInactive", Value = 1 });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@PageID" });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            adminPages = EntityCollectionHelper.ConvertTo<Page>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllPages, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllPages, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return adminPages;

        }

        /// <summary>Insert the UserRole in bulk.</summary>
        /// <param name="privilegeList">The List of InsertRoleUser Objects.</param>
        /// <param name="createdByUserID">The Integer Object for CreatedByUserId.</param>
        /// <param name="updatedByUserID">The Integer Object for UpdatedByUserId.</param>
        /// <returns>Boolean Object.</returns>
        public bool UserRoleInsertBulk(List<InsertRoleUser> privilegeList, Int32 createdByUserID, int updatedByUserID)
        {
            Logging.LogDebugMessage("Method: UserRoleInsertBulk, MethodType: Post, Layer: SecurityDAL, Parameters: privilegeList = " + JsonConvert.SerializeObject(privilegeList) + ",createdByUserID = " + createdByUserID.ToString() + ",updatedByUserID = " + updatedByUserID.ToString());
            bool checkUserRole = false;
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataTable = EntityCollectionHelper.ConvertTo<InsertRoleUser>(privilegeList);

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_UserRoleInsertBulk";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = createdByUserID });
                    //  command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = UpdatedByUserID });
                    SqlParameter structuredParam = new SqlParameter("@UserRoleTable", SqlDbType.Structured);
                    structuredParam.Value = dataTable;
                    command.Parameters.Add(structuredParam);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            checkUserRole = true;
                        }
                        else
                        {
                            checkUserRole = false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: UserRoleInsertBulk, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return checkUserRole;
        }

        /// <summary>Gets the list of user roles by UserId.</summary>
        /// <param name="userId">The Long object.</param>
        /// <returns>List of UserRoles.</returns>
        public List<UserRoles> GetUserRoles(Int64 userId)
        {
            Logging.LogDebugMessage("Method: GetUserRoles, MethodType: Get, Layer: SecurityDAL, Parameters: userId = " + userId.ToString());
            // int userid = 2;
            var dataSet = new DataSet();
            var userRoles = new List<UserRoles>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_UserRoleSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = userId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            userRoles = EntityCollectionHelper.ConvertTo<UserRoles>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetUserRoles, Layer: SecurityDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetUserRoles, Layer: SecurityDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return userRoles;

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
