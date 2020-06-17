using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CitizenWeb.DAL;
using CitizenWeb.Models;
using Newtonsoft.Json;

namespace CitizenWeb.BL
{

    /// <summary>RequestTemplateBL.This business layer contains all the methods which are related to Setting and Getting RequestTemplate.</summary>
    public class RequestTemplateBL : IDisposable
    {
        private bool disposed = false;

        /// <summary>Gets the RequestTemplate List.</summary>
        /// <returns>returns RequestTemplateList Object.<returns>
        public List<RequestTemplate> GetAllRequestTemplates()
        {
            Logging.LogDebugMessage("Method: GetAllRequestTemplates, MethodType: Get, Layer: RequestTemplateBL, Parameters: No Input Parameters");
            try
            {
                using (RequestTemplateDAL requesttemplates = new RequestTemplateDAL())
                {
                    return requesttemplates.GetAllRequestTemplate();
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllRequestTemplates, Layer: RequestTemplateBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllRequestTemplates, Layer: RequestTemplateBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Get RequestTemplate By RequestTemplateId </summary>
        /// <param name="RequestTemplateId">The Integer object</param>
        /// <returns>RequestTemplateObj object</returns>
        public RequestTemplateDetail GetRequestTemplateId(int RequestTemplateId)
        {
            Logging.LogDebugMessage("Method: GetRequestTemplateId, MethodType: Get, Layer: RequestTemplateBL, Parameters: RequestTemplateId =" + RequestTemplateId.ToString());
            try
            {
                using (RequestTemplateDAL requesttemplate = new RequestTemplateDAL())
                {
                    return requesttemplate.GetRequestTemplateById(RequestTemplateId);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRequestTemplateId, Layer: RequestTemplateBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRequestTemplateId, Layer: RequestTemplateBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary>Delete Requesttemplate </summary>
        /// <param name="RequestTemplateIds">The Integer object</param>
        /// <param name="AdminUserID">THe long object</param>
        /// <returns>boolean value</returns>
        public bool DeleteRequestTemplate(DeletedRequestTemplateWithAdminUser deletedRequestTemplateWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteRequestTemplate, MethodType: Get, Layer: RequestTemplateBL, Parameters:  deletedRequestTemplateWithAdminUser = " + JsonConvert.SerializeObject(deletedRequestTemplateWithAdminUser));
            try
            {
                using (RequestTemplateDAL RequesttemplateDAL = new RequestTemplateDAL())
                {
                    return RequesttemplateDAL.DeleteRequestTemplate(deletedRequestTemplateWithAdminUser);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteRequestTemplate, Layer: RequestTemplateBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteRequestTemplate, Layer: RequestTemplateBL, Stack Trace: " + ex.ToString());
                throw;
            }
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
