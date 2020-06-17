using System;
using System.Data.SqlClient;
using CitizenWeb.DAL;
using CitizenWeb.Models;
using Newtonsoft.Json;

namespace CitizenWeb.BL
{
    /// <summary>RequestFormBL.This business layer contains all the methods which are related to Setting and Getting Request Form Template.</summary>
    public class RequestFormBL : IDisposable
    {
        private bool disposed = false;
        /// <summary>
        /// Get the Request Form Template by RequestCategoryID and RequestTemplateId
        /// </summary>
        /// <param name="RequestCategoryID">The Integer Object for RequestCategoryID</param>
        /// <param name="RequestTemplateID">The Integer Object for RequestTemplateId</param>
        /// <returns>RequestFormTemplate Object</returns>
        public RequestFormTemplate GetRequestFormTemplateByCategoryIDAndTemplateID(int RequestCategoryID, int RequestTemplateID)
        {
            Logging.LogDebugMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, MethodType: Get, Layer: RequestFormBL, Parameters: RequestCategoryID = " + RequestCategoryID.ToString() + ", RequestTemplateID =" + RequestTemplateID.ToString());
            try
            {
                using (RequestFormDAL requestDL = new RequestFormDAL())
                {
                    return requestDL.GetRequestFormTemplateByCategoryIDAndTemplateID(RequestCategoryID, RequestTemplateID);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, Layer: RequestTransactionBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, Layer: RequestTransactionBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Save or Update RequestFormTemplate </summary>
        /// <param name="requestTemplate">RequestTemplate Object</param>
        /// <returns>Integer Object</returns>
        public Int64 SaveRequestFormTemplate(RequestTemplateDetails requestTemplate)
        {
            Logging.LogDebugMessage("Method: SaveRequestFormTemplate, MethodType: Post, Layer: RequestFormBL, Parameters:  requestTemplate = " + JsonConvert.SerializeObject(requestTemplate));
            try
            {
                using (RequestFormDAL requestDL = new RequestFormDAL())
                {
                    if (requestTemplate.RequestTemplateId == 0)
                    {
                        RequestTemplateSection rs1 = new RequestTemplateSection();
                        rs1.RequestTemplateSectionId = 0;
                        rs1.RequestTemplateId = 0;
                        rs1.TaskStepName = "Location";
                        rs1.SeqNo = 1;
                        rs1.SectionName = "Location";
                        rs1.SectionTitle = "Location";
                        rs1.IsRequired = true;
                        rs1.RowStatus = "A";
                        requestTemplate.requestTemplateSection.Add(rs1);
                        RequestTemplateSection rs2 = new RequestTemplateSection();
                        rs2.RequestTemplateSectionId = 0;
                        rs2.RequestTemplateId = 0;
                        rs2.TaskStepName = "Photo";
                        rs2.SeqNo = 2;
                        rs2.SectionName = "Photo";
                        rs2.SectionTitle = "Photo";
                        rs2.IsRequired = true;
                        rs2.RowStatus = "A";
                        requestTemplate.requestTemplateSection.Add(rs2);
                        RequestTemplateSection rs3 = new RequestTemplateSection();
                        rs3.RequestTemplateSectionId = 0;
                        rs3.RequestTemplateId = 0;
                        rs3.TaskStepName = "Details";
                        rs3.SeqNo = 3;
                        rs3.SectionName = "Details";
                        rs3.SectionTitle = "Details";
                        rs3.IsRequired = true;
                        rs3.RowStatus = "A";
                        requestTemplate.requestTemplateSection.Add(rs3);
                        RequestTemplateSection rs4 = new RequestTemplateSection();
                        rs4.RequestTemplateSectionId = 0;
                        rs4.RequestTemplateId = 0;
                        rs4.TaskStepName = "Submit";
                        rs4.SeqNo = 4;
                        rs4.SectionName = "Submit";
                        rs4.SectionTitle = "Submit";
                        rs4.IsRequired = true;
                        rs4.RowStatus = "A";
                        requestTemplate.requestTemplateSection.Add(rs4);
                        RequestTemplateSection rs5 = new RequestTemplateSection();
                        rs5.RequestTemplateSectionId = 0;
                        rs5.RequestTemplateId = 0;
                        rs5.TaskStepName = "Register or Sign In";
                        rs5.SeqNo = 5;
                        rs5.SectionName = "Register or Sign In";
                        rs5.SectionTitle = "Register or Sign In";
                        rs5.IsRequired = true;
                        rs5.RowStatus = "A";
                        requestTemplate.requestTemplateSection.Add(rs5);
                        RequestTemplateSection rs6 = new RequestTemplateSection();
                        rs6.RequestTemplateSectionId = 0;
                        rs6.RequestTemplateId = 0;
                        rs6.TaskStepName = "Submit Request";
                        rs6.SeqNo = 4;
                        rs6.SectionName = "Submit Request";
                        rs6.SectionTitle = "Submit Request";
                        rs6.IsRequired = true;
                        rs6.RowStatus = "A";
                        requestTemplate.requestTemplateSection.Add(rs6);

                    }
                    return requestDL.SaveRequestFormTemplate(requestTemplate);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveRequestFormTemplate, Layer: RequestFormBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveRequestFormTemplate, Layer: RequestFormBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }
        /// <summary>
        /// This method disposing unmanaged code like Stream/File etc.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.marshalbyvaluecomponent.dispose?view=netframework-4.8#System_ComponentModel_MarshalByValueComponent_Dispose.
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