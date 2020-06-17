using System;
using System.Data;
using System.Data.SqlClient;
using CitizenWeb.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CitizenWeb.DAL
{
    /// <summary>RequestFormDAL.This data access layer contains all the methods which are related to Setting and Getting Request Form.</summary>
    public class RequestFormDAL : IDisposable
    {
        private string connectionString { get; set; }
        AppConstants appConstants = new AppConstants();
        private bool disposed = false;
        public RequestFormDAL()
        {
            this.connectionString = AppSettings.ConnectionString.ToString();
        }

        /// <summary>
        /// Get the Request Form Template by RequestCategoryID and RequestTemplateId
        /// </summary>
        /// <param name="RequestCategoryID">The Integer Object for RequestCategoryID</param>
        /// <param name="RequestTemplateID">The Integer Object for RequestTemplateId</param>
        /// <returns>RequestFormTemplate Object</returns>
        public RequestFormTemplate GetRequestFormTemplateByCategoryIDAndTemplateID(int RequestCategoryID, int RequestTemplateID)
        {
            Logging.LogDebugMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, MethodType: Get, Layer: RequestFormDAL, Parameters: RequestCategoryID = " + RequestCategoryID.ToString() + ", RequestTemplateID =" + RequestTemplateID.ToString());
            RequestFormTemplate requestFormTemplate = new RequestFormTemplate();
            List<RequestFormTemplateDetails> TemplateDetails = new List<RequestFormTemplateDetails>();
            List<RequestTemplateSectionControls> TemplateSectionControls = new List<RequestTemplateSectionControls>();
            List<TemplateSectionControls> TemplateControls = new List<TemplateSectionControls>();
            PreviewTemplate PreviewTemplateDetails = new PreviewTemplate();
            List<PreviewTemplateControl> PreviewTemplateList = new List<PreviewTemplateControl>();
            try
            {
                using (var command = new SqlCommand())
                {

                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_GetRequestFormTemplateByCategoryIdAndRequestTemplateId";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestCategoryId", Value = RequestCategoryID });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateId", Value = RequestTemplateID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            requestFormTemplate = EntityCollectionHelper.ConvertTo<RequestFormTemplate>(ds.Tables[0]).FirstOrDefault();
                        }
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            TemplateDetails = EntityCollectionHelper.ConvertTo<RequestFormTemplateDetails>(ds.Tables[1]).ToList();
                        }
                        if (ds != null && ds.Tables.Count > 2)
                        {
                            TemplateSectionControls = EntityCollectionHelper.ConvertTo<RequestTemplateSectionControls>(ds.Tables[2]).ToList();
                        }
                        List<int> controlGroupList = (from obj in TemplateSectionControls select obj.SeqNo).Distinct().ToList();
                        int incrCount = 1;
                        for (int count = 0; count < controlGroupList.Count; count++)
                        {
                            List<RequestTemplateSectionControls> GroupSectionControls = TemplateSectionControls.Where(sectionControl => sectionControl.SeqNo == controlGroupList[count]).ToList();
                            if (GroupSectionControls.Count == 1)
                            {


                                TemplateSectionControls templateControl = new TemplateSectionControls();
                                templateControl.RequestTemplateSectionId = GroupSectionControls[0].RequestTemplateSectionId;
                                templateControl.Type = GroupSectionControls[0].ControlName.ToLower();
                                templateControl.Name = GroupSectionControls[0].ControlName.ToLower() + incrCount.ToString();
                                templateControl.ControlType = GroupSectionControls[0].ControlType;
                                templateControl.RequestTemplateSectionControlId = GroupSectionControls[0].RequestTemplateSectionControlId;
                                templateControl.Label = GroupSectionControls[0].ControlLabel;
                                templateControl.Value = "";
                                templateControl.Required = GroupSectionControls[0].IsRequired;
                                if (GroupSectionControls[0].ControlName.ToLower() == appConstants.TextArea)
                                {
                                    templateControl.Input = "this.inputChange.bind(this)";
                                }
                                else if (GroupSectionControls[0].ControlName.ToLower() == appConstants.CheckBox)
                                {
                                    templateControl.Input = "this.checkValue.bind(this)";
                                }
                                else if (GroupSectionControls[0].ControlName.ToLower() == appConstants.TextBox)
                                {
                                    templateControl.Input = "this.inputChange.bind(this)";
                                }
                                else
                                {
                                    templateControl.Input = "";
                                }
                                templateControl.SeqNO = GroupSectionControls[0].SeqNo;
                                templateControl.MaxLen = GroupSectionControls[0].MaxLen;
                                templateControl.RowLength = GroupSectionControls[0].RowLength;
                                templateControl.DisplayField = GroupSectionControls[0].DisplayField;
                                templateControl.ValueField = GroupSectionControls[0].ValueField;
                                templateControl.SourceName = GroupSectionControls[0].SourceName;
                                templateControl.IsChecked = GroupSectionControls[0].IsChecked;
                                templateControl.TemplateControlOptions = new List<TemplateSectionControlOptions>();
                                TemplateControls.Add(templateControl);

                                PreviewTemplateControl PreviewTempDetails = new PreviewTemplateControl();
                                if (GroupSectionControls[0].ControlName.ToLower() == appConstants.TextArea)
                                {
                                    PreviewTempDetails.Type = appConstants.Label;
                                }
                                else if (GroupSectionControls[0].ControlName.ToLower() == appConstants.CheckBox)
                                {
                                    PreviewTempDetails.Type = "ul";
                                }
                                else if (GroupSectionControls[0].ControlName.ToLower() == appConstants.TextBox)
                                {
                                    PreviewTempDetails.Type = appConstants.Label;
                                }
                                else
                                {
                                    PreviewTempDetails.Type = "";
                                }

                                PreviewTempDetails.Name = templateControl.Name;
                                PreviewTempDetails.Label = templateControl.Label.Split('(')[0].Split("Example")[0];
                                if (GroupSectionControls[0].ControlName.ToLower() != appConstants.Label)
                                {
                                    PreviewTemplateList.Add(PreviewTempDetails);
                                }
                                incrCount++;
                            }
                            else if (GroupSectionControls.Count > 1)
                            {
                                TemplateSectionControls templateControl = new TemplateSectionControls();
                                List<TemplateSectionControlOptions> templateControlOptions = new List<TemplateSectionControlOptions>();
                                RequestTemplateSectionControls requestTemplateSectionControl = GroupSectionControls.Where(sectionControl => sectionControl.ControlName.ToLower() == appConstants.Label).FirstOrDefault();

                                templateControl.RequestTemplateSectionId = requestTemplateSectionControl.RequestTemplateSectionId;
                                templateControl.RequestTemplateSectionControlId = requestTemplateSectionControl.RequestTemplateSectionControlId;
                                templateControl.Label = requestTemplateSectionControl.ControlLabel;
                                //templateControl.ControlType = requestTemplateSectionControl.ControlType;
                                templateControl.Value = "";
                                templateControl.Required = requestTemplateSectionControl.IsRequired;
                                templateControl.Input = "";
                                templateControl.SeqNO = requestTemplateSectionControl.SeqNo;
                                templateControl.MaxLen = requestTemplateSectionControl.MaxLen;
                                templateControl.RowLength = requestTemplateSectionControl.RowLength;
                                templateControl.DisplayField = requestTemplateSectionControl.DisplayField;
                                templateControl.ValueField = requestTemplateSectionControl.ValueField;
                                templateControl.SourceName = requestTemplateSectionControl.SourceName;
                                templateControl.IsChecked = requestTemplateSectionControl.IsChecked;
                                incrCount = incrCount + 1;

                                PreviewTemplateControl PreviewTempDetails = new PreviewTemplateControl();

                                foreach (RequestTemplateSectionControls item in GroupSectionControls)
                                {
                                    if (item.ControlName.ToLower() != appConstants.Label)
                                    {
                                        if (templateControl.Type == null)
                                        {
                                            templateControl.Type = item.ControlName.ToLower();
                                            templateControl.Name = item.ControlName.ToLower() + incrCount.ToString();
                                            templateControl.ControlType = item.ControlType;

                                            if (item.ControlName.ToLower() == appConstants.TextArea)
                                            {
                                                PreviewTempDetails.Type = appConstants.Label;
                                            }
                                            else if (item.ControlName.ToLower() == appConstants.CheckBox)
                                            {
                                                PreviewTempDetails.Type = "ul";
                                            }
                                            else if (item.ControlName.ToLower() == appConstants.TextBox)
                                            {
                                                PreviewTempDetails.Type = appConstants.Label;
                                            }
                                            else
                                            {
                                                PreviewTempDetails.Type = "";
                                            }

                                            PreviewTempDetails.Name = templateControl.Name;
                                            PreviewTempDetails.Label = templateControl.Label.Split('(')[0].Split("Example")[0];


                                            incrCount = incrCount + 1;
                                        }
                                        TemplateSectionControlOptions templateSectionControlOptions = new TemplateSectionControlOptions();
                                        templateSectionControlOptions.RequestTemplateSectionControlId = item.RequestTemplateSectionControlId;
                                        templateSectionControlOptions.RequestTemplateSectionId = item.RequestTemplateSectionId;
                                        templateSectionControlOptions.Value = false;
                                        templateSectionControlOptions.Key = string.Join("", item.ControlLabel.ToLower().Split(" "));
                                        templateSectionControlOptions.Label = item.ControlLabel;
                                        templateSectionControlOptions.Name = item.ControlName.ToLower() + incrCount.ToString();
                                        incrCount = incrCount + 1;
                                        if (item.ControlName.ToLower() == appConstants.CheckBox)
                                        {
                                            templateControl.Input = "this.checkValue.bind(this)";
                                        }
                                        else
                                        {
                                            templateControl.Input = "";
                                        }

                                        templateControlOptions.Add(templateSectionControlOptions);
                                    }
                                }

                                PreviewTemplateList.Add(PreviewTempDetails);
                                templateControl.TemplateControlOptions = templateControlOptions;
                                TemplateControls.Add(templateControl);
                            }
                        }
                        foreach (RequestFormTemplateDetails item in TemplateDetails)
                        {
                            List<TemplateSectionControls> templateControls = TemplateControls.Where(control => control.RequestTemplateSectionId == item.RequestTemplateSectionId).ToList();
                            if (templateControls == null || templateControls.Count == 0)
                            {
                                templateControls = new List<TemplateSectionControls>();
                            }
                            item.TemplateControls = templateControls;
                        }
                        if (TemplateDetails == null || TemplateDetails.Count == 0)
                        {
                            TemplateDetails = new List<RequestFormTemplateDetails>();
                        }
                        requestFormTemplate.TemplateDetailsList = TemplateDetails;
                        PreviewTemplateDetails.PreviewTemplateControlsList = PreviewTemplateList;
                        requestFormTemplate.PreviewDetails = PreviewTemplateDetails;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, Layer: RequestFormDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, Layer: RequestFormDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            using (LookupsDAL lookuDAL = new LookupsDAL())
            {
                requestFormTemplate.LookupsList = lookuDAL.GetLookupDetailsByLookupNames("Submit,Track,Request Status");
            }
            return requestFormTemplate;
        }


        /// <summary> Save or Update RequestFormTemplate </summary>
        /// <param name="requestTemplate">RequestTemplate Object</param>
        /// <returns>Integer Object</returns>
        public Int64 SaveRequestFormTemplate(RequestTemplateDetails requestTemplate)
        {
            var dataset = new DataSet();
            Int64 templateId = 0;

            Logging.LogDebugMessage("Method: SaveRequestFormTemplate, MethodType: Post, Layer: RequestFormDAL, Parameters:  requestTemplate = " + JsonConvert.SerializeObject(requestTemplate));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_SaveRequestFormTemplate_V1";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateId", Value = requestTemplate.RequestTemplateId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestName", Value = requestTemplate.RequestName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestDescription", Value = requestTemplate.RequestDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@DisplayType", Value = requestTemplate.DisplayType });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestCategoryId", Value = requestTemplate.RequestCategoryId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = requestTemplate.UserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Visibility", Value = requestTemplate.Visibility });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Priority", Value = requestTemplate.Priority });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RowStatus", Value = requestTemplate.RowStatus });
                    DataTable dataTableOne = new DataTable();
                    dataTableOne = EntityCollectionHelper.ConvertTo<RequestTemplateSection>(requestTemplate.requestTemplateSection);
                    DataTable dataTableTwo = new DataTable();
                    dataTableTwo = EntityCollectionHelper.ConvertTo<RequestTemplateSectionControl>(requestTemplate.requestTemplateSectionControl);
                    DataTable dataTableThree = new DataTable();
                    SqlParameter structuredParamOne = new SqlParameter("@RequestTemplateSection", SqlDbType.Structured);
                    structuredParamOne.Value = dataTableOne;
                    command.Parameters.Add(structuredParamOne);
                    SqlParameter structuredParamTwo = new SqlParameter("@RequestTemplateSectionControl", SqlDbType.Structured);
                    structuredParamTwo.Value = dataTableTwo;
                    command.Parameters.Add(structuredParamTwo);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            templateId = Convert.ToInt64(dataset.Tables[0].Rows[0][0]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveRequestFormTemplate, Layer: RequestFormDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveRequestFormTemplate, Layer: RequestFormDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return templateId;
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
