
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace CitizenWeb.Models
{
    /// <summary>RequestFormTemplate.</summary>
    public class RequestFormTemplate
    {
        public int RequestCategoryId { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }

        public Int64 RequestTemplateId { get; set; }

        public string RequestName { get; set; }

        public string RequestDescription { get; set; }

        public string DisplayType { get; set; }

        public string Visibility { get; set; }

        public List<RequestFormTemplateDetails> TemplateDetailsList { get; set; }

        public PreviewTemplate PreviewDetails { get; set; }

        public List<LookupDetailsWithLookupName> LookupsList { get; set; }

    }

    /// <summary>RequestFormTemplateDetails.</summary>
    public class RequestFormTemplateDetails
    {
        public int RequestTemplateSectionId { get; set; }
        public Int64 RequestTemplateId { get; set; }
        public string TaskStepName { get; set; }

        public int SeqNo { get; set; }

        public string SectionName { get; set; }

        public string SectionTitle { get; set; }

        public bool IsRequired { get; set; }

        public List<TemplateSectionControls> TemplateControls { get; set; }
    }

    /// <summary>RequestTemplateSectionControls.</summary>
    public class RequestTemplateSectionControls
    {

        public int RequestTemplateSectionControlId { get; set; }
        public int RequestTemplateSectionId { get; set; }

        public string ControlName { get; set; }
        public int ControlType { get; set; }

        public string ControlLabel { get; set; }

        public int SeqNo { get; set; }

        public int MaxLen { get; set; }
        public bool IsChecked { get; set; }
        public bool IsRequired { get; set; }

        public string DisplayField { get; set; }
        public string ValueField { get; set; }

        public string SourceName { get; set; }

        public int RowLength { get; set; }
    }


    /// <summary>TemplateSectionControls.</summary>
    public class TemplateSectionControls
    {
        public int RequestTemplateSectionId { get; set; }
        public int RequestTemplateSectionControlId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public int ControlType { get; set; }

        public string Label { get; set; }
        public string Value { get; set; }
        public bool Required { get; set; }
        public string Input { get; set; }
        public int SeqNO { get; set; }
        public int MaxLen { get; set; }
        public int RowLength { get; set; }
        public string DisplayField { get; set; }
        public string ValueField { get; set; }
        public string SourceName { get; set; }
        public bool IsChecked { get; set; }
        public List<TemplateSectionControlOptions> TemplateControlOptions { get; set; }
    }

    /// <summary>TemplateSectionControlOptions.</summary>
    public class TemplateSectionControlOptions
    {
        public int RequestTemplateSectionId { get; set; }
        public int RequestTemplateSectionControlId { get; set; }
        public bool Value { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public string Change { get; set; }
        public string Name { get; set; }
    }

    public class PreviewTemplate
    {
        public List<PreviewTemplateControl> PreviewTemplateControlsList { get; set; }
    }

    /// <summary>PreviewTemplate.</summary>
    public class PreviewTemplateControl
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
    }
    public class RequestTemplateDetails
    {
        public long RequestTemplateId { get; set; }
        public string RequestName { get; set; }
        public string RequestDescription { get; set; }
        public int DisplayType { get; set; }
        public int RequestCategoryId { get; set; }
        public long UserId { get; set; }
        public int Visibility { get; set; }
        public int Priority { get; set; }
        public string RowStatus { get; set; }

        public List<RequestTemplateSection> requestTemplateSection { get; set; }

        public List<RequestTemplateSectionControl> requestTemplateSectionControl { get; set; }
    }
    /// <summary> Request Template Detail </summary>
    public class RequestTemplateDetail
    {
        public long RequestTemplateId { get; set; }
        public string RequestName { get; set; }
        public string RequestDescription { get; set; }
        public int DisplayType { get; set; }
        public string DisplayTypeText { get; set; }
        public int RequestCategoryId { get; set; }
        public Int64 CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string RowStatus { get; set; }
        public int Visibility { get; set; }
        public string VisibilityText { get; set; }
        public int Priority { get; set; }
        public string CategoryText { get; set; }
        public string PriorityText { get; set; }

        public List<RequestFormTemplateDetails> TemplateDetailsList { get; set; }
    }

    public class RequestTemplateSection
    {
        public int RequestTemplateSectionId { get; set; }
        public Int64 RequestTemplateId { get; set; }
        public string TaskStepName { get; set; }
        public int SeqNo { get; set; }
        public string SectionName { get; set; }
        public string SectionTitle { get; set; }
        public bool IsRequired { get; set; }
        public string RowStatus { get; set; }
    }

    public class RequestTemplateSectionControl
    {
        public int RequestTemplateSectionControlId { get; set; }
        public int RequestTemplateSectionId { get; set; }
        public int ControlType { get; set; }
        public string ControlLabel { get; set; }
        public int SeqNo { get; set; }
        public int? MaxLen { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsRequired { get; set; }
        public string? DisplayField { get; set; }
        public string? ValueField { get; set; }
        public string? SourceName { get; set; }
        public int? RowLength { get; set; }
        public string RowStatus { get; set; }
    }
}
