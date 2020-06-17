using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace CitizenWeb.Models
{
    /// <summary>AdminRole Model Class.</summary>
    public partial class AdminRoles
    {
        /// <summary>Gets or sets the role identifier.</summary>
        /// <value>The role identifier.</value>
        [DataMember]
        public int RoleId { get; set; }

        /// <summary>Gets or sets the name of the role.</summary>
        /// <value>The name of the role.</value>
        [DataMember]
        public String RoleName { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is active.</summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the create date.</summary>
        /// <value>The create date.</value>
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>Gets or sets the created by.</summary>
        /// <value>The created by.</value>
        [DataMember]
        public String CreatedBy { get; set; }

        /// <summary>Gets or sets the update date.</summary>
        /// <value>The update date.</value>
        [DataMember]
        public DateTime UpdateDate { get; set; }

        /// <summary>Gets or sets the updated by.</summary>
        /// <value>The updated by.</value>
        [DataMember]
        public String UpdatedBy { get; set; }

        /// <summary>Gets or sets the privilege identifier.</summary>
        /// <value>The privilege identifier.</value>
        //[DataMember]
        //public Int32 PrivilegeId { get; set; }

        /// <summary>Gets or sets the name of the privilege.</summary>
        /// <value>The name of the privilege.</value>
        //[DataMember]
        //public string PrivilegeName { get; set; }

        /// <summary>Gets or sets the privilege code.</summary>
        /// <value>The privilege code.</value>
        //[DataMember]
        //public string PrivilegeCode { get; set; }

        /// <summary>Gets or sets the create user identifier.</summary>
        /// <value>The create user identifier.</value>
        [DataMember]
        public Int32 CreateUserId { get; set; }

        /// <summary>Gets or sets the update user identifier.</summary>
        /// <value>The update user identifier.</value>
        [DataMember]
        public Int32 UpdateUserId { get; set; }


    }
    [DataContract]
    public class RoleObj
    {
        [DataMember]
        public Int32 CreateUserId { get; set; }
        [DataMember]
        public List<InserRoleUser> Insertroleuser { get; set; }
    }
    public class InserRoleUser
    {
        [DataMember]
        public Int32 IntegerColumn1 { get; set; } //UserID
        [DataMember]
        public Int32 IntegerColumn2 { get; set; } //RoleID
    }
    public class DeletedRolesWithAdminUser
    {
        /// <summary>Gets or sets the RoleIds.</summary>
        /// <value>The string object.</value>
        public string RoleIds { get; set; }
        /// <summary>Gets or sets the Admin UserId.</summary>
        /// <value>The Ineger Object.</value>
        public Int64 AdminUserId { get; set; }
    }
}
