using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CitizenWeb.Models
{
    class Security
    {
    }
    public class SecurityRole
    {
        [DataMember]
        public int RoleID { get; set; }

        [DataMember]
        public String RoleName { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public String UpdatedBy { get; set; }

        [DataMember]
        public Int32 PrivilegeID { get; set; }

        [DataMember]
        public string PrivilegeName { get; set; }

        [DataMember]
        public string PrivilegeCode { get; set; }
        [DataMember]
        public int PageID { get; set; }

        [DataMember]
        public String PageName { get; set; }

    }
    [DataContract]
    public class RoleID
    {
        [DataMember]
        public int RoleId { get; set; }

    }

    [DataContract]
    public class CSTMPrivilege
    {
        [DataMember]
        public int PrivilegeId { get; set; }

        [DataMember]
        public string PrivilegeName { get; set; }

        [DataMember]
        public string PrivilegeCode { get; set; }

        [DataMember]
        public int PageId { get; set; }
    }

    [DataContract]
    public class CSTMModule
    {
        [DataMember]
        public int ModuleID { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public List<CSTMpage> Pages { get; set; }
    }

    [DataContract]
    public class CSTMpage
    {
        [DataMember]
        public int PageID { get; set; }

        [DataMember]
        public string PageName { get; set; }

        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public List<CSTMPrivilege> Privileges { get; set; }
    }
    [DataContract]
    public class UserRoles
    {
        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public String RoleName { get; set; }

        [DataMember]
        public DateTime CreatedDateTime { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        [DataMember]
        public DateTime UpdatedDateTime { get; set; }

        [DataMember]
        public String UpdatedBy { get; set; }
        [DataMember]
        public String UserName { get; set; }

        [DataMember]
        public Int64? UserId { get; set; }
    }

    [DataContract]
    public class IntegerColumn
    {
        [DataMember]
        public int PrivilegeId { get; set; }
    }

    public class RolePrivilege
    {
        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public String RoleName { get; set; }

        [DataMember]
        public DateTime CreatedDateTime { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        [DataMember]
        public Int32 PrivilegeId { get; set; }

        [DataMember]
        public string PrivilegeName { get; set; }

        [DataMember]
        public string PrivilegeCode { get; set; }

        [DataMember]
        public Int32 UpdateUserId { get; set; }

    }

    [DataContract]

    public class PrivilegeObj

    {
        [DataMember]

        public List<IntegerColumn> privilegelist { get; set; }
        [DataMember]

        public RolePrivilege roleslist { get; set; }

    }

    [DataContract]
    public class ActiveRoles
    {
        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public String RoleName { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public String UpdatedBy { get; set; }


    }
    [DataContract]
    public class Module
    {
        [DataMember]
        public String ModuleName { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int ModuleID { get; set; }


        [DataMember]
        public DateTime CreatedDateTime { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        [DataMember]
        public DateTime UpdatedDateTime { get; set; }

        [DataMember]
        public String UpdatedBy { get; set; }

    }

    [DataContract]
    public class Page
    {
        [DataMember]
        public String PageName { get; set; }

        [DataMember]
        public int PageID { get; set; }

        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        [DataMember]
        public DateTime UpdatdDate { get; set; }

        [DataMember]
        public String UpdatedBy { get; set; }
    }
    public class InsertRoleUser
    {
        [DataMember]
        public Int32 IntegerColumn1 { get; set; } //UserID
        [DataMember]
        public Int32 IntegerColumn2 { get; set; } //RoleID
    }
    [DataContract]
    public class AddUserRoleObj
    {
        [DataMember]
        public RoleUserlist roleuserlist { get; set; }

        [DataMember]
        public List<AddRoleUser> Addroleuser { get; set; }

    }
    public class AddRoleUser
    {
        [DataMember]
        public Int32 IntegerColumn { get; set; } //UserID

    }
    public class RoleUserlist
    {

        [DataMember]
        public Int32 RoleId { get; set; }

        [DataMember]
        public int UpdateUserId { get; set; }
    }
    [DataContract]
    public class SecurityRoleObj
    {
        [DataMember]
        public Int32 CreatedByUserID { get; set; }
        [DataMember]
        public Int32 UpdatedByUserID { get; set; }
        [DataMember]
        public List<InsertRoleUser> insertroleuser { get; set; }
    }
}
