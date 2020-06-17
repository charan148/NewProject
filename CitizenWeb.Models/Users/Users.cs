using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CitizenWeb.Models
{
    /// <summary>Users.</summary>
    public class Users
    {
        /// <summary>Gets or sets the User ID.</summary>
        /// <value>The User ID.</value>
        public long UserId { get; set; }
        public int AccessFailedCount { get; set; }
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }
        public string PasswordResetCode { get; set; }
        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the email address.</summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }
        public string EmailConfirmationCode { get; set; }

        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>Gets or sets the is email confirmed.</summary>
        /// <value>The is email confirmed.</value>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is admin.</summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
        public bool IsAdmin { get; set; }

        /// <summary>Gets or sets the role identifier.</summary>
        /// <value>The role identifier.</value>
        public int roleId { get; set; }

        /// <summary>Gets or sets the create date.</summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }

        /// <summary>Gets or sets the created by.</summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>Gets or sets the update date.</summary>
        /// <value>The update date.</value>
        public DateTime UpdateDate { get; set; }

        /// <summary>Gets or sets the updated by.</summary>
        /// <value>The updated by.</value>
        public string UpdatedBy { get; set; }

        /// <summary>Gets or sets the user image.</summary>
        /// <value>The user image.</value>
        public string UserImage { get; set; }

    }

    public class UsesRoleDelete
    {

        /// <summary>Gets or sets the role identifier.</summary>
        /// <value>The role identifier.</value>
        [DataMember]
        public int RoleID { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        [DataMember]
        public int UserID { get; set; }
    }

    public class UsersByName
    {
        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public String UserName { get; set; }

        public String FirmName { get; set; }

        /// <summary>Gets or sets the email address.</summary>
        /// <value>The email address.</value>
        public String EmailAddress { get; set; }
    }

    public class UsersEmail
    {
        /// <summary>Gets or sets the email address.</summary>
        /// <value>The email address.</value>
        public String EmailAddress { get; set; }
    }
    public class DeletedUsersWithAdminUser
    {
        /// <summary>Gets or sets the UserIds.</summary>
        /// <value>The string object.</value>
        public string UserIds { get; set; }
        /// <summary>Gets or sets the Admin UserId.</summary>
        /// <value>The Ineger Object.</value>
        public Int64 AdminUserId { get; set; }
    }
}
