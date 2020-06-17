using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using CitizenWeb.BL;
using CitizenWeb.Models;
using CitizenWeb.DAL;

namespace CitizenWeb.Controllers
{
    /// <summary>RequestTemplateController.This controller contains all the methods which are related to RequestTemplate</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/RequestTemplateController")]
    [ApiController]
    public class RequestTemplateController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public RequestTemplateController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>Gets the RequestTemplate List.</summary>
        /// <returns>returns RequestTemplate Object.<returns>
        [Route("GetAllRequestTemplate")]
        [HttpGet]
        public List<RequestTemplate> GetAllRequestTemplates()
        {
            Logging.LogDebugMessage("Method: GetAllRequestTemplates, MethodType: Get, Layer: RequestTemplateController, Parameters: No Input Parameters");
            using (RequestTemplateBL requestTemplate = new RequestTemplateBL())
            {
                return requestTemplate.GetAllRequestTemplates();
            }
        }

        /// <summary> Get RequestTemplate By RequestTemplateId </summary>
        /// <param name="RequestTemplateId">The Integer object</param>
        /// <returns>RequestTemplateObj object</returns>
        [Route("GetRequestTemplateId/{RequestTemplateId}")]
        [HttpGet]
        public RequestTemplateDetail GetRequestTemplateId(int RequestTemplateId)
        {
            Logging.LogDebugMessage("Method: GetRequestTemplateId, MethodType: Get, Layer: RequestTemplateController, Parameters: RequestTemplateId =" + RequestTemplateId.ToString());
            using (RequestTemplateBL requestTemplate = new RequestTemplateBL())
            {
                return requestTemplate.GetRequestTemplateId(RequestTemplateId);
            }
        }

        /// <summary>Delete Requesttemplate </summary>
        /// <param name="RequestTemplateIds">The Integer object</param>
        /// <param name="AdminUserID">The long object</param>
        /// <returns>boolean value</returns>
        [Route("DeleteRequestTemplate")]
        [HttpPost]
        public bool DeleteRequestTemplate(DeletedRequestTemplateWithAdminUser deletedRequestTemplateWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteRequestTemplate, MethodType: Post, Layer: RequestTemplateController, Parameters:   deletedRequestTemplateWithAdminUser = " + JsonConvert.SerializeObject(deletedRequestTemplateWithAdminUser));

            using (RequestTemplateBL requestTemplate = new RequestTemplateBL())
            {
                return requestTemplate.DeleteRequestTemplate(deletedRequestTemplateWithAdminUser);
            }

        }


    }
}
