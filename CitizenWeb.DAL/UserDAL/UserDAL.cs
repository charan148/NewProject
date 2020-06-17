// <copyright file=UserDAL company="citizen">
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
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using CitizenWeb.Models;
    using Newtonsoft.Json;
   

    /// <summary>UserDAL.This data access layer contains all the methods which are related to Setting and Getting Alerts.</summary>
    public class UserDAL : IDisposable
    {
        private bool disposed = false;
        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        private string ConnectionString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDAL"/> class.Gets the users List.</summary>
        /// <returns>returns UsersList Object.<returns>
        public UserDAL()
        {
            this.ConnectionString = AppSettings.ConnectionString.ToString();
        }


        /// <summary>Authenticates the specified user.</summary>
        /// <param name="user">The user object.</param>
        /// <returns>Object of User.</returns>
        public Users Authenticate(Users user)
        {
            Logging.LogDebugMessage("Method: Authenticate, MethodType: Post, Layer: UserDAL, Parameters: user = " + JsonConvert.SerializeObject(user) );
            DataSet dataSet = new DataSet();
            Users users = new Users();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserAuthenticate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@EmailAddress", Value = user.EmailAddress });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Password", Value = user.Password });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsAuthenticated", Value = 1 });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            users = EntityCollectionHelper.ConvertTo<Users>(dataSet.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: Authenticate, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: Authenticate, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return users;
        }


        /// <summary> Saves New User </summary>
        /// <param name="user">Users Object.</param>
        /// <returns>Users Object.</returns>
        public Users userInsert(Users user)
        {
            var ds = new DataSet();
            Users res = new Users();
            Logging.LogDebugMessage("Method: userInsert, MethodType: Post, Layer: UserDAL, Parameters:  user = " + JsonConvert.SerializeObject(user));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserInsert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AccessFailedCount", Value = user.AccessFailedCount });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserName", Value = user.UserName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Password", Value = user.Password });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@FirstName", Value = user.FirstName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LastName", Value = user.LastName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@EmailAddress", Value = user.EmailAddress });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@PhoneNumber", Value = user.PhoneNumber });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsEmailConfirmed", Value = user.IsEmailConfirmed });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = user.UserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = user.UserId });

                    // command.Parameters["@UserID"].Direction = ParameterDirection.Output;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            res = EntityCollectionHelper.ConvertTo<Users>(ds.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Logging Error Message for user Insert " + ex.Message);
                throw ex;
            }
            return res;
        }


        /// <summary>Gets the users List.</summary>
        /// <returns>returns UsersList Object.<returns>
        public List<Users> FetchAllUsers()
        {
            Logging.LogDebugMessage("Method: FetchAllUsers, MethodType: Get, Layer: UserDAL, Parameters: No Input Parameters");
            var dataSet = new DataSet();
            var users = new List<Users>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserSelect";
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            users = EntityCollectionHelper.ConvertTo<Users>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: FetchAllUsers, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: FetchAllUsers, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return users;

        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="userId">The Integer Object.</param>
        /// <returns>User Object.</returns>
        public List<Users> FetchUserById(int userId)
        {
            Logging.LogDebugMessage("Method: FetchUserById, MethodType: Get, Layer: UserDAL, Parameters: userId = " + userId.ToString());
            var dataSet = new DataSet();
            var users = new List<Users>();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = userId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            users = EntityCollectionHelper.ConvertTo<Users>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: FetchUserById, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: FetchUserById, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return users;

        }

        
        /// <summary>Update User.</summary>
        /// <param name="user">The AdminUser Object.</param>
        /// <returns>AdminUser Object.</returns>
        public bool UpdateUser(Users user)
        {
            Logging.LogDebugMessage("Method: UpdateUser, MethodType: Post, Layer: UserDAL, Parameters: user = " + JsonConvert.SerializeObject(user));
            var dataSet = new DataSet();
            // var retList = new List<Users>();
            Boolean checkUpdateValue = false;

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = user.UserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AccessFailedCount", Value = user.AccessFailedCount });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserName", Value = user.UserName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Password", Value = user.Password });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@FirstName", Value = user.FirstName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LastName", Value = user.LastName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@EmailAddress", Value = user.EmailAddress });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@PhoneNumber", Value = user.PhoneNumber });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsEmailConfirmed", Value = user.IsEmailConfirmed });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UpdatedByUserID", Value = user.UserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserImage", Value = user.UserImage });


                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            checkUpdateValue = true;
                            //Audits audit = new Audits
                            //{
                            //    MainPageName = "Users",
                            //    PageTabName = "Users",
                            //    ActionName = "Save",
                            //    UserID = user.UpdateUserId,
                            //    RecordID = user.UserId
                            //};
                            //AuditDAL auditobj = new AuditDAL();
                            //auditobj.SaveAuditInfo(audit);

                        }
                        else
                        {
                            checkUpdateValue = false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: UpdateUser, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: UpdateUser, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return checkUpdateValue;
        }
        /// <summary>Gets the User by User name.</summary>
        /// <param name="userName">The String Object.</param>
        /// <returns>User Object.</returns>
        public Users FetchUserByName(String userName)
        {
            Logging.LogDebugMessage("Method: FetchUserByName, MethodType: Get, Layer: UserDAL, Parameters: userName = " + userName);
            var dataSet = new DataSet();
            Users users = new Users();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserName", Value = userName });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            users = EntityCollectionHelper.ConvertTo<Users>(dataSet.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: FetchUserByName, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: FetchUsersByName, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return users;
        }

        /// <summary>Delete the user and roles.</summary>
        /// <param name="userRoleDelete"> UsesRoleDelete Object.</param>
        /// <returns>List of Users and roles List.</returns>
        public bool DeleteUserRole(UsesRoleDelete userRole)
        {
            Logging.LogDebugMessage("Method: DeleteUserRole, MethodType: Get, Layer: UserDAL, Parameters: userRole = " + JsonConvert.SerializeObject(userRole));
            var dataset = new DataSet();
            // var retList = new List<Users>();
            Boolean checkdeleteValue = false;

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserRoleDelete";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RoleID", Value = userRole.RoleID });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = userRole.UserID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null && dataset.Tables.Count > 0)
                        {
                            checkdeleteValue = true;
                            //retList = EntityCollectionHelper.ConvertTo<Users>(ds.Tables[0]).ToList();


                        }
                        else
                        {
                            checkdeleteValue = false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteUserRole, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteUserRole, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return checkdeleteValue;
        }

        /// <summary>Gets the search user by name List.</summary>
        /// <param name="userName">The String Object.</param>
        /// <returns>User Object.</returns>
        public List<UsersByName> SearchUserByNameList(String userName)
        {
            Logging.LogDebugMessage("Method: SearchUserByNameList, MethodType: Get, Layer: UserDAL, Parameters: userName = " + userName);
            var dataSet = new DataSet();
            var user = new List<UsersByName>();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_SearchUserByNameList";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserName", Value = userName });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            user = EntityCollectionHelper.ConvertTo<UsersByName>(dataSet.Tables[0]).ToList();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SearchUserByNameList, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SearchUserByNameList, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return user;
        }

        /// <summary>This method check Email exists or not</summary>
        /// <param name="usersEmail">The UsersEmail Object.</param>
        /// <returns>boolean Object.</returns>
        public bool CheckEmailExistsOrNot(UsersEmail usersEmail)
        {
            Logging.LogDebugMessage("Method: CheckEmailExistsOrNot, MethodType: Post, Layer: UserDAL, Parameters: UsersEmail = " + JsonConvert.SerializeObject(usersEmail));
            var dataSet = new DataSet();
            bool isEmailExistsOrNot = false;

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_CheckEmailExistsOrNot";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Email", Value = usersEmail.EmailAddress });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            isEmailExistsOrNot = Convert.ToBoolean(dataSet.Tables[0].Rows[0][0]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: CheckEmailExistsOrNot, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: CheckEmailExistsOrNot, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return isEmailExistsOrNot;
        }

        /// <summary>This method deletes the selected users.</summary>
        /// <param name="deletedUsersWithAdminUser">The DeletedUsersWithAdminUser Object.</param>
        /// <returns>List of Users Object.</returns>
        public List<Users> DeleteUsers(DeletedUsersWithAdminUser deletedUsersWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteUsers, MethodType: Post, Layer: UserDAL, Parameters: DeletedUsersWithAdminUser = " + JsonConvert.SerializeObject(deletedUsersWithAdminUser));
            try
            {
                using (var command = new SqlCommand())
                {
                    var dataSet = new DataSet();
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_UserDelete";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserIDs", Value = deletedUsersWithAdminUser.UserIds });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AdminUserID", Value = deletedUsersWithAdminUser.AdminUserId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteUsers, Layer: UserDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteUsers, Layer: UserDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return this.FetchAllUsers();
        }

        /// <summary>
        /// This method disposing unmanaged code like Stream/File etc.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.marshalbyvaluecomponent.dispose?view=netframework-4.8#System_ComponentModel_MarshalByValueComponent_Dispose
        /// </summary>
        /// <param name="disposing">Dispose Parameter is going to be only used in case to free up the unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            // Dispose of any unmanaged resources not wrapped in safe handles.
            this.disposed = true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
        }

    }
}
