// <copyright file=UserController company="citizen">
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
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using CitizenWeb.BL;
using CitizenWeb.Models;
using CitizenWeb.DAL;

namespace CitizenWeb.Controllers
{
    /// <summary>UserController.This controller contains all the methods which are related to Setting and Getting Users</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/UserController")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public UserController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private string DefaultPassword { get; set; }

        /// <summary>Authenticates the specified user.</summary>
        /// <param name="user">The user object.</param>
        /// <returns>Object of User.</returns>
        [Route("Authenticate")]
        [HttpPost]
        public Users Authenticate(Users userDto)
        {
            Logging.LogDebugMessage("Method: Authenticate, MethodType: Post, Layer: UserController, Parameters: user = " + JsonConvert.SerializeObject(userDto));
            using (UserBL authenticate = new UserBL())
            {
                Users result = new Users();
                userDto.Password = Crypto.Encrypt(EncryptDecrypt.DecodeText(HttpUtility.UrlDecode(userDto.Password)));
                result = authenticate.Authenticate(userDto);

            //if (result != null)
            //{
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    // var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //       new Claim("userId", result.UserId.ToString())
            //        }),
            //        Expires = DateTime.UtcNow.AddDays(7),
            //        ///   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    var token = tokenHandler.CreateToken(tokenDescriptor);
            //    var tokenString = tokenHandler.WriteToken(token);

            //    // return basic user info (without password) and token to store client side
            //    result.Token = tokenString;
            //}

                return result;
            }

        }
        /// <summary>Insert User Details.</summary>
        /// <param name="user">The UserInsertObj Object.</param>
        /// <returns>Users Object.</returns>
        [Route("userInsert")]
        [HttpPost]
        public Users userInsert([FromBody]Users user)
        {
            Logging.LogDebugMessage("Method: UserInsert, MethodType: Post, Layer: UserController, Parameters: user = " + JsonConvert.SerializeObject(user));
            using (UserBL insertUser = new UserBL())
            {
                user.Password = Crypto.Encrypt(EncryptDecrypt.DecodeText(user.Password));
                return insertUser.userInsert(user);
            }
        }

        /// <summary>Gets the list of users.</summary>
        /// <returns>List of AdminUsers.</returns>
        [Route("fetchAllUsers")]
        [HttpGet]
        public List<Users> FetchAllUsers()
        {
            Logging.LogDebugMessage("Method: FetchAllUsers, MethodType: Get, Layer: UserController, Parameters:No Parameter Values");
            using (UserBL allUsers = new UserBL())
            {
                List<Users> users = new List<Users>();
                users = allUsers.FetchAllUsers();
                return users;
            }
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="userId">The Integer Object.</param>
        /// <returns>Object of AdminUser.</returns>
        [Route("fetchUserById/{userId}")]
        [HttpGet]
        public List<Users> FetchUserById(int userId)
        {
            Logging.LogDebugMessage("Method: FetchUserById, MethodType: Get, Layer: UserController, Parameters: userId = " + userId.ToString());
            using (UserBL userByid = new UserBL())
            {
                List<Users> users = new List<Users>();
                users = userByid.FetchUserById(userId); ;
                //  re[0].Password = Crypto.Decrypt(re[0].Password);
                return users;
            }

        }

        /// <summary>Update the User.</summary>
        /// <param name="user">The UserInsertObj object.</param>
        /// <returns>boolean object.</returns>
        [Route("UpdateUser")]
        [HttpPost]
        public bool UpdateUser(Users user)
        {
            Logging.LogDebugMessage("Method: UpdateUser, MethodType: Post, Layer: UserController, Parameters: user = " + JsonConvert.SerializeObject(user));
            using (UserBL updateUser = new UserBL())
            {
                user.Password = Crypto.Encrypt(EncryptDecrypt.DecodeText(user.Password));
                return updateUser.UpdateUser(user);
            }
        }

        /// <summary>Gets the list of Users.</summary>
        /// <param name="userName">The String Object.</param>
        /// <returns>List of AdnminUser.</returns>
        [Route("FetchUserByName/{UserName}")]
        [HttpGet]
        public Users FetchUserByName(string userName)
        {
            Logging.LogDebugMessage("Method: FetchUserByName, MethodType: Get, Layer: UserController, Parameters: userName = " + userName);
            using (UserBL userByName = new UserBL())
            {
                return userByName.FetchUserByName(userName);
            }
        }

        /// <summary>Delete the user and roles.</summary>
        /// <param name="userRoleDelete"> UsesRoleDelete Object.</param>
        /// <returns>List of Users and roles List.</returns>
        [Route("DeleteUserRole")]
        [HttpPost]
        public bool DeleteUserRole(UsesRoleDelete userRole)
        {
            Logging.LogDebugMessage("Method: UserRoleDelete, MethodType: Get, Layer: UserController, Parameters: userRole = " + JsonConvert.SerializeObject(userRole));
            using (UserBL deleteUserRole = new UserBL())
            {
                return deleteUserRole.DeleteUserRole(userRole);
            }
        }

        [Route("CheckEmailExistsOrNot")]
        [HttpPost]
        /// <summary>This method check Email exists or not</summary>
        /// <param name="usersEmail">The UsersEmail Object.</param>
        /// <returns>boolean Object.</returns>
        public bool CheckEmailExistsOrNot(UsersEmail usersEmail)
        {
            Logging.LogDebugMessage("Method: CheckEmailExistsOrNot, MethodType: Post, Layer: UserController, Parameters: UsersEmail = " + JsonConvert.SerializeObject(usersEmail));
            using (UserBL userBL = new UserBL())
            {
                return userBL.CheckEmailExistsOrNot(usersEmail);
            }
        }
        [Route("DeleteUsers")]
        [HttpPost]
        /// <summary>This method deletes the selected users.</summary>
        /// <param name="deletedUsersWithAdminUser">The DeletedUsersWithAdminUser Object.</param>
        /// <returns>List of Users Object.</returns>
        public List<Users> DeleteUsers(DeletedUsersWithAdminUser deletedUsersWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteUsers, MethodType: Post, Layer: UserController, Parameters: DeletedUsersWithAdminUser = " + JsonConvert.SerializeObject(deletedUsersWithAdminUser));
            using (UserBL userBL = new UserBL())
            {
                return userBL.DeleteUsers(deletedUsersWithAdminUser);
            }
        }
    }
}