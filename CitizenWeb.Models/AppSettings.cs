using System;
using System.Collections.Generic;
using System.Text;

namespace CitizenWeb.Models
{
    public static class AppSettings
    {
        /// <summary>Gets or sets the connection string.</summary>
        /// <value>The connection string.</value>
        public static string ConnectionString { get; set; }

        /// <summary>Gets or sets the Images Upload Path.</summary>
        /// <value>The Images Upload Path string.</value>
        public static string ImagesUploadPath { get; set; }

        /// <summary>Gets or sets the Host.</summary>
        /// <value>The string.</value>
        public static string Host { get; set; }

        /// <summary>Gets or sets the AdminEmailID.</summary>
        /// <value>The string.</value>
        public static string AdminEmailID { get; set; }

        /// <summary>Gets or sets the AdminEmailPassword.</summary>
        /// <value>The string.</value>
        public static string AdminEmailPassword { get; set; }

        /// <summary>Gets or sets the Subject.</summary>
        /// <value>The string.</value>
        public static string Subject { get; set; }
    }
}
