using System;
using System.Collections.Generic;
using System.Text;

namespace CitizenWeb.Models
{
	public class Lookups
	{
		public int LookupId { get; set; }
		public string LookupName { get; set; }
		public string LookupDescription { get; set; }
		public string LookupLongDescription { get; set; }
		public string LookupShortDescription { get; set; }
		public string LookupDisplayDescription { get; set; }
		public bool IsUsedInDisplay { get; set; }
		public DateTime? EffectiveFrom { get; set; }
		public DateTime? EffectiveTo { get; set; }
		public string LookupStatus { get; set; }
		public long CreateUserId { get; set; }
		public DateTime CreateDate { get; set; }
		public long? UpdateUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string RowStatus { get; set; }
	}

	public class LookupDetails
	{
		public int LookupDetailsId { get; set; }
		public int LookupId { get; set; }
		public string LookupDetailsDescription { get; set; }
		public string LookupDetailsValue { get; set; }
		public string LookupDetailsLongDescription { get; set; }
		public int LookupDetailsSequenceOrder { get; set; }
		public string LookupDetailsCrossWalkValue { get; set; }
		public string LookupDetailsCategory { get; set; }
		public string LookupDetailsType { get; set; }
		public int LookupDetailsSubSequenceOrder { get; set; }
		public string LookupDetailsSubCategory { get; set; }
		public string LookupDetailsSubType { get; set; }
		public string LookupDetailsShortDescription { get; set; }
		public string LookupDetailsDisplayDescription { get; set; }
		public bool IsUsedInDisplay { get; set; }
		public DateTime? LookupDetailsEffectiveFrom { get; set; }
		public DateTime? LookupDetailsEffectiveTo { get; set; }
		public string LookupDetailsStatus { get; set; }
		public long CreateUserId { get; set; }
		public DateTime CreateDate { get; set; }
		public long? UpdateUserId { get; set; }
		public DateTime UpdateDate { get; set; }
		public string RowStatus { get; set; }
	}

	public class LookupObj
	{
		public int LookupId { get; set; }
		public string LookupName { get; set; }
		public string LookupDescription { get; set; }
		public string LookupLongDescription { get; set; }
		public string LookupShortDescription { get; set; }
		public string LookupDisplayDescription { get; set; }
		public bool IsUsedInDisplay { get; set; }
		public DateTime? EffectiveFrom { get; set; }
		public DateTime? EffectiveTo { get; set; }
		public string LookupStatus { get; set; }
		public long CreateUserId { get; set; }
		public DateTime CreateDate { get; set; }
		public long? UpdateUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string RowStatus { get; set; }

		public List<LookupDetails> lookupDetails { get; set; }
	}

	public class LookupDetailsWithLookupName
	{
		public int LookupDetailsId { get; set; }
		public int LookupId { get; set; }
		public string LookupDetailsDescription { get; set; }

		public string LookupName { get; set; }
		public string LookupDetailsValue { get; set; }
		public string LookupDetailsLongDescription { get; set; }
		public int LookupDetailsSequenceOrder { get; set; }
		public string LookupDetailsCrossWalkValue { get; set; }
		public string LookupDetailsCategory { get; set; }
		public string LookupDetailsType { get; set; }
		public int LookupDetailsSubSequenceOrder { get; set; }
		public string LookupDetailsSubCategory { get; set; }
		public string LookupDetailsSubType { get; set; }
		public string LookupDetailsShortDescription { get; set; }
		public string LookupDetailsDisplayDescription { get; set; }
		public bool IsUsedInDisplay { get; set; }
		public DateTime LookupDetailsEffectiveFrom { get; set; }
		public DateTime LookupDetailsEffectiveTo { get; set; }
		public string LookupDetailsStatus { get; set; }
		public long CreateUserId { get; set; }
		public DateTime CreateDate { get; set; }
		public long? UpdateUserId { get; set; }
		public DateTime UpdateDate { get; set; }
		public string RowStatus { get; set; }
	}

	public class DeletedLookupsWithAdminUser
	{
		/// <summary>Gets or sets the LookupIds.</summary>
		/// <value>The string object.</value>
		public string LookupIds { get; set; }
		/// <summary>Gets or sets the Admin UserId.</summary>
		/// <value>The Ineger Object.</value>
		public Int64 AdminUserID { get; set; }
	}

	public class DeletedLookupDetailsWithAdminUser
	{
		/// <summary>Gets or sets the LookupDetailsIds.</summary>
		/// <value>The string object.</value>
		public string LookupDetailsIds { get; set; }
		/// <summary>Gets or sets the Admin UserId.</summary>
		/// <value>The Ineger Object.</value>
		public Int64 AdminUserID { get; set; }
	}

	public class DesignerLookup
	{
		/// <summary>Gets or sets the LookupName.</summary>
		/// <value>The string object.</value>
		public string LookupName { get; set; }
		/// <summary>Gets or sets the LookupDescription.</summary>
		/// <value>The string Object.</value>
		public string LookupDescription { get; set; }
		/// <summary>Gets or sets the LookupShortDescription.</summary>
		/// <value>The string Object.</value>
		public string LookupShortDescription { get; set; }

		/// <summary>Gets or sets the CreateUserId.</summary>
		/// <value>The Integer Object.</value>
		public Int64 CreateUserId { get; set; }
		/// <summary>Gets or sets the designerLookupDetails.</summary>
		/// <value>The list of DesignerLookupDetails Object.</value>
		public List<DesignerLookupDetails> designerLookupDetails { get; set; }
	}

	public class DesignerLookupDetails
	{
		/// <summary>Gets or sets the LookupDetailsValue.</summary>
		/// <value>The string object.</value>
		public string LookupDetailsValue { get; set; }
		/// <summary>Gets or sets the LookupDetailsDescription.</summary>
		/// <value>The string Object.</value>
		public string LookupDetailsDescription { get; set; }

		/// <summary>Gets or sets the LookupDetailsSequenceOrder.</summary>
		/// <value>The Integer Object.</value>
		public int LookupDetailsSequenceOrder { get; set; }
	}

	public class DesignerCategory
	{
		/// <summary>Gets or sets the CategoryId.</summary>
		/// <value>The Integer object.</value>
		public int RequestCategoryId { get; set; }
		/// <summary>Gets or sets the RequestCategory.</summary>
		/// <value>The string Object.</value>
		public string RequestCategory { get; set; }
	}

	public class DesignerLookupsWithCategories
	{
		/// <summary>Gets or sets the lookupDetailsWithLookupNames.</summary>
		/// <value>The list of LookupDetailsWithLookupName object.</value>
		public List<LookupDetailsWithLookupName> lookupDetailsWithLookupNames { get; set; }

		/// <summary>Gets or sets the designerCategories.</summary>
		/// <value>The list of DesignerCategory Object.</value>
		public List<DesignerCategory> designerCategories { get; set; }
	}
}

