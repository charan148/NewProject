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
using CitizenWeb.DAL;
using CitizenWeb.Models;

namespace CitizenWeb.Controllers
{
    /// <summary>RequestFormController.This controller contains all the methods which are related to Setting and Getting Request Form Templates</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/RequestFormController")]
    [ApiController]
    public class RequestFormController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public RequestFormController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Get the Request Form Template by RequestCategoryID and RequestTemplateId
        /// </summary>
        /// <param name="RequestCategoryID">The Integer Object for RequestCategoryID</param>
        /// <param name="RequestTemplateID">The Integer Object for RequestTemplateId</param>
        /// <returns>RequestFormTemplate Object</returns>
        [Route("GetRequestFormTemplateByCategoryIDAndTemplateID/{RequestCategoryID}/{RequestTemplateID}")]
        [HttpGet]
        public RequestFormTemplate GetRequestFormTemplateByCategoryIDAndTemplateID(int RequestCategoryID, int RequestTemplateID)
        {
            Logging.LogDebugMessage("Method: GetRequestFormTemplateByCategoryIDAndTemplateID, MethodType: Get, Layer: RequestFormController, Parameters: RequestCategoryID = " + RequestCategoryID.ToString() + ", RequestTemplateID =" + RequestTemplateID.ToString());
            using (RequestFormBL requestFormBL = new RequestFormBL())
            {
                return requestFormBL.GetRequestFormTemplateByCategoryIDAndTemplateID(RequestCategoryID, RequestTemplateID);
            }
        }
        /// <summary> Save or Update RequestFormTemplate </summary>
        /// <param name="requestTemplate">RequestTemplate Object</param>
        /// <returns>Integer Object</returns>
        [Route("SaveRequestFormTemplate")]
        [HttpPost]
        public Int64 SaveRequestFormTemplate(RequestTemplateDetails requestTemplate)
        {
            Logging.LogDebugMessage("Method: SaveRequestFormTemplate, MethodType: Post, Layer: RequestFormController, Parameters:  requestTemplate = " + JsonConvert.SerializeObject(requestTemplate));
            using (RequestFormBL requestFormBL = new RequestFormBL())
            {
                return requestFormBL.SaveRequestFormTemplate(requestTemplate);
            }

        }
    }
}