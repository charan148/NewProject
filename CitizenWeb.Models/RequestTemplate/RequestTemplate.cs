using System;
using System.Collections.Generic;
using System.Text;

namespace CitizenWeb.Models
{
    /// <summary>Request Template.</summary>

    public class RequestTemplate
    {
        public Int64 RequestTemplateId { get; set; }
        public string RequestName { get; set; }
        public string RequestDescription { get; set; }
        public Int64 DisplayType { get; set; }
        public string DisplayTypeText { get; set; }
        public int RequestCategoryId { get; set; }
        public Int64 CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<Int64> ModifiedUser { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public string RowStatus { get; set; }
        public Int64 Visibility { get; set; }
        public string VisibilityText { get; set; }
        public Int64 Priority { get; set; }

        public string PriorityText { get; set; }
        public string CategoryText { get; set; }
    }

    public class DeletedRequestTemplateWithAdminUser
    {        
            /// <summary>Gets or sets the LookupDetailsIds.</summary>
            /// <value>The string object.</value>
            public string RequestTemplateIds { get; set; }
            /// <summary>Gets or sets the Admin UserId.</summary>
            /// <value>The Ineger Object.</value>
            public Int64 AdminUserID { get; set; }

    }
    //public class RequestTemplate
    //{
    //    public long RequestTemplateId { get; set; }
    //    public string RequestName { get; set; }
    //    public string RequestDescription { get; set; }
    //    public int DisplayType { get; set; }
    //    public int RequestCategoryId { get; set; }
    //    public long UserId { get; set; }
    //    public int Visibility { get; set; }
    //    public int Priority { get; set; }
    //    public string RowStatus { get; set; }
    //}



}
