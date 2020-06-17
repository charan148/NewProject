// <copyright file=UserBL company="citizen">
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
    using System.Data.SqlClient;
    using CitizenWeb.DAL;
    using CitizenWeb.Models;
    using Newtonsoft.Json;

    /// <summary>UserBL.This business layer contains all the methods which are related to Setting and Getting Alerts.</summary>
    public class UserBL : IDisposable
    {
        private bool disposed = false;
        /// <summary>Authenticates the specified user.</summary>
        /// <param name="user">The user object.</param>
        /// <returns>Object of User.</returns>
        public Users Authenticate(Users user)
        {
            Logging.LogDebugMessage("Method: Authenticate, MethodType: Post, Layer: UserBL, Parameters: user = " + JsonConvert.SerializeObject(user));
            using (UserDAL authenticate = new UserDAL())
            {
                try
                {
                    return authenticate.Authenticate(user);

                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: Authenticate, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: Authenticate, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw ;
                }
            }
        }

        /// <summary>Insert User Details.</summary>
        /// <param name="user">The UserInsertObj Object.</param>
        /// <returns>Users Object.</returns>
        public Users userInsert(Users user)
        {
            Logging.LogDebugMessage("Method: UserInsert, MethodType: Post, Layer: UserBL, Parameters: user = " + JsonConvert.SerializeObject(user));
            using (UserDAL insertUser = new UserDAL())
            {
                try
                {
                    return insertUser.userInsert(user);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: UserInsert, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: UserInsert, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the users List.</summary>
        /// <returns>returns UsersList Object.<returns>
        public List<Users> FetchAllUsers()
        {
            Logging.LogDebugMessage("Method: FetchAllUsers, MethodType: Get, Layer: UserBL, Parameters: No Input Parameters");
            using (UserDAL allusers = new UserDAL())
            {
                try
                {
                    return allusers.FetchAllUsers();
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: FetchAllUsers, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: FetchAllUsers, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="userId">The Integer Object.</param>
        /// <returns>Object of AdminUser.</returns>
        public List<Users> FetchUserById(int userId)
        {
            Logging.LogDebugMessage("Method: FetchUserById, MethodType: Get, Layer: UserBL, Parameters: userId = " + userId.ToString());
            using (UserDAL userById = new UserDAL())
            {
                try
                {
                    return userById.FetchUserById(userId);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: FetchUserById, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: FetchUserById, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        

        /// <summary>Update the User.</summary>
        /// <param name="user">The UserInsertObj object.</param>
        /// <returns>boolean object.</returns>
        public bool UpdateUser(Users user)
        {
            Logging.LogDebugMessage("Method: UpdateUser, MethodType: Post, Layer: UserBL, Parameters: user = " + JsonConvert.SerializeObject(user));
            using (UserDAL updateuser = new UserDAL())
            {
                try
                {
                    return updateuser.UpdateUser(user);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: UpdateUser, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: UpdateUser, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the list of Users.</summary>
        /// <param name="userName">The String Object.</param>
        /// <returns>List of AdnminUser.</returns>
        public Users FetchUserByName(string userName)
        {
            Logging.LogDebugMessage("Method: FetchUserByName, MethodType: Get, Layer: UserBL, Parameters: userName = " + userName);
            using (UserDAL userByName = new UserDAL())
            {
                try
                {
                    return userByName.FetchUserByName(userName);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: FetchUserByName, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: FetchUserByName, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Delete the user and roles.</summary>
        /// <param name="userRoleDelete"> UsesRoleDelete Object.</param>
        /// <returns>List of Users and roles List.</returns>
        public bool DeleteUserRole(UsesRoleDelete userRole)
        {
            Logging.LogDebugMessage("Method: DeleteUserRole, MethodType: Get, Layer: UserBL, Parameters: userRole = " + JsonConvert.SerializeObject(userRole));
            using (UserDAL deleteUserrole = new UserDAL())
            {
                try
                {
                    return deleteUserrole.DeleteUserRole(userRole);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: DeleteUserRole, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: DeleteUserRole, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>Gets the search user by name List.</summary>
        /// <param name="userName">The String Object.</param>
        /// <returns>User Object.</returns>
        public List<UsersByName> SearchUserByNameList(string userName)
        {
            Logging.LogDebugMessage("Method: SearchUserByNameList, MethodType: Get, Layer: UserBL, Parameters: userName = " + userName);
            using (UserDAL searchUserByName = new UserDAL())
            {
                try
                {
                    return searchUserByName.SearchUserByNameList(userName);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: SearchUserByNameList, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: SearchUserByNameList, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>This method deletes the selected users.</summary>
        /// <param name="deletedUsersWithAdminUser">The DeletedUsersWithAdminUser Object.</param>
        /// <returns>List of Users Object.</returns>
        public List<Users> DeleteUsers(DeletedUsersWithAdminUser deletedUsersWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteUsers, MethodType: Post, Layer: UserBL, Parameters: DeletedUsersWithAdminUser = " + JsonConvert.SerializeObject(deletedUsersWithAdminUser));
            using (UserDAL deleteUserrole = new UserDAL())
            {
                try
                {
                    return deleteUserrole.DeleteUsers(deletedUsersWithAdminUser);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: DeleteUsers, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: DeleteUsers, Layer: UserBL, Stack Trace: " + ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>This method check Email exists or not</summary>
        /// <param name="usersEmail">The UsersEmail Object.</param>
        /// <returns>boolean Object.</returns>
        public bool CheckEmailExistsOrNot(UsersEmail usersEmail)
        {
            Logging.LogDebugMessage("Method: CheckEmailExistsOrNot, MethodType: Post, Layer: UserBL, Parameters: UsersEmail = " + JsonConvert.SerializeObject(usersEmail));
            using (UserDAL deleteUserrole = new UserDAL())
            {
                try
                {
                    return deleteUserrole.CheckEmailExistsOrNot(usersEmail);
                }
                catch (SqlException sqlEx)
                {
                    Logging.LogErrorMessage("Method: CheckEmailExistsOrNot, Layer: UserBL, Stack Trace: " + sqlEx.ToString());
                    throw;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Method: CheckEmailExistsOrNot, Layer: UserBL, Stack Trace: " + ex.ToString());
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
