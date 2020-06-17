using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using CitizenWeb.Models;
using Newtonsoft.Json;

namespace CitizenWeb.DAL
{

    /// <summary>RequestTemplateDAL.This data access layer contains all the methods which are related to Setting and Getting RequestTemplates.</summary>
    public class RequestTemplateDAL : IDisposable
    {
        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        private string ConnectionString { get; set; }
        AppConstants appConstants = new AppConstants();
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTemplateDAL"/> class.Gets the RequestTemplate List.</summary>
        /// <returns>returns UsersList Object.<returns>
        public RequestTemplateDAL()
        {
            this.ConnectionString = AppSettings.ConnectionString.ToString();
        }

        /// <summary>Gets the RequestTemplate List.</summary>
        /// <returns>returns RequestTemplate Object.<returns>
        public List<RequestTemplate> GetAllRequestTemplate()
        {
            Logging.LogDebugMessage("Method: GetAllRequestTemplate, MethodType: Get, Layer: RequestTemplateDAL, Parameters: No Input Parameters");
            var dataSet = new DataSet();
            var requesttemplates = new List<RequestTemplate>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetAllRequestTemplates";
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            requesttemplates = EntityCollectionHelper.ConvertTo<RequestTemplate>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllRequestTemplate, Layer: RequestTemplateDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllRequestTemplate, Layer: RequestTemplateDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return requesttemplates;

        }

        /// <summary> Get RequestTemplate By RequestTemplateById </summary>
        /// <param name="RequestTemplateId">The Integer object</param>
        /// <returns>requestTemplateObj object</returns>
        public RequestTemplateDetail GetRequestTemplateById(int RequestTemplateId)
        {
            Logging.LogDebugMessage("Method: GetRequestTemplateById, MethodType: Get, Layer: RequestTemplateDAL, Parameters: RequestTemplateId = " + RequestTemplateId.ToString());
            RequestTemplateDetail requestFormTemplate = new RequestTemplateDetail();
            List<RequestFormTemplateDetails> TemplateDetails = new List<RequestFormTemplateDetails>();
            List<RequestTemplateSectionControls> TemplateSectionControls = new List<RequestTemplateSectionControls>();
            List<TemplateSectionControls> TemplateControls = new List<TemplateSectionControls>();
            try
            {
                using (var command = new SqlCommand())
                {

                    command.CommandText = "USP_GetRequestTemplateByTemplateId";
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateId", Value = RequestTemplateId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            requestFormTemplate = EntityCollectionHelper.ConvertTo<RequestTemplateDetail>(ds.Tables[0]).FirstOrDefault();
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
                                templateControl.Label = GroupSectionControls[0].ControlLabel.Split("(")[0];
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

                                incrCount++;
                            }
                            else if (GroupSectionControls.Count > 1)
                            {
                                TemplateSectionControls templateControl = new TemplateSectionControls();
                                List<TemplateSectionControlOptions> templateControlOptions = new List<TemplateSectionControlOptions>();
                                RequestTemplateSectionControls requestTemplateSectionControl = GroupSectionControls.Where(sectionControl => sectionControl.ControlName.ToLower() == appConstants.Label).FirstOrDefault();

                                templateControl.RequestTemplateSectionId = requestTemplateSectionControl.RequestTemplateSectionId;
                                templateControl.RequestTemplateSectionControlId = requestTemplateSectionControl.RequestTemplateSectionControlId;
                                templateControl.Label = requestTemplateSectionControl.ControlLabel.Split("(")[0];
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

                                foreach (RequestTemplateSectionControls item in GroupSectionControls)
                                {
                                    if (item.ControlName.ToLower() != appConstants.Label)
                                    {
                                        if (templateControl.Type == null)
                                        {
                                            templateControl.Type = item.ControlName.ToLower();
                                            templateControl.Name = item.ControlName.ToLower() + incrCount.ToString();
                                            templateControl.ControlType = item.ControlType;

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
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRequestTemplateById, Layer: RequestTemplateDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRequestTemplateById, Layer: RequestTemplateDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return requestFormTemplate;
        }

        /// <summary>Delete DeleteRequestTemplate </summary>
        /// <param name="RequestTemplateIds">The Integer object</param>
        /// <param name="AdminUserID">THe long object</param>
        /// <returns>boolean value</returns>
        public bool DeleteRequestTemplate(DeletedRequestTemplateWithAdminUser deletedRequestTemplateWithAdminUser)
        {
            var dataset = new DataSet();
            var response = false;
            Logging.LogDebugMessage("Method: DeleteRequestTemplate, MethodType: Get, Layer: RequestTemplateDAL, Parameters:  deletedRequestTemplateWithAdminUser = " + JsonConvert.SerializeObject(deletedRequestTemplateWithAdminUser));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_DeleteRequestTemplate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateIds", Value = deletedRequestTemplateWithAdminUser.RequestTemplateIds });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AdminUserID", Value = deletedRequestTemplateWithAdminUser.AdminUserID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            response = true;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteRequestTemplate, Layer: RequestTemplateDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteRequestTemplate, Layer: RequestTemplateDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return response;
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
