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
    /// <summary>RequestTransactionController.This controller contains all the methods which are related to Setting and Getting RequestTranscation</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/RequestTransactionController")]
    [ApiController]
    public class RequestTransactionController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public RequestTransactionController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary> Submit or track the Request </summary>
        /// <param name="requestTransaction">The object of type RequestTransaction</param>
        /// <returns> Integer object </returns>
        [Route("SubmitOrTrackRequest")]
        [HttpPost]
        public int SubmitOrTrackRequest(RequestTransaction requestTransaction)
        {
            Logging.LogDebugMessage("Method: SubmitOrTrackRequest, MethodType: Post, Layer: RequestTransactionController, Parameters: requestTransaction = " + JsonConvert.SerializeObject(requestTransaction));
            using (RequestTransactionBL requestTransactionBL = new RequestTransactionBL())
            {
                return requestTransactionBL.SubmitOrTrackRequest(requestTransaction);
            }
        }

        /// <summary>
        /// Track the Request Transaction.
        /// </summary>
        /// <param name="requestTransaction">The object of type TrackRequestTransaction.</param>
        /// <returns>Integer object.</returns>
        [Route("TrackRequestTransaction")]
        [HttpPost]
        public int TrackRequestTransaction(TrackRequestTransaction request)
        {
            Logging.LogDebugMessage("Method: TrackRequestTransaction, MethodType: Post, Layer: RequestTransactionController, Parameters: request = " + JsonConvert.SerializeObject(request));
            using (RequestTransactionBL requestTransactionBL = new RequestTransactionBL())
            {
                return requestTransactionBL.TrackRequestTransaction(request);
            }
        }

        /// <summary>
        /// Gets the similar requests list
        /// </summary>
        /// <param name="Latitude">The string object</param>
        /// <param name="Longitude">The string object</param>
        /// <param name="RequestTemplateId">The integer object</param>
        /// <returns>list of SimilarRequestTransactions object</returns>
        [Route("GetSimilarRequests/{Latitude}/{Longitude}/{RequestTemplateID}")]
        [HttpGet]
        public List<SimilarRequestTransaction> GetSimilarRequests(string Latitude, string Longitude, int RequestTemplateID)
        {
            Logging.LogDebugMessage("Method: GetSimilarRequests, MethodType: Get, Layer: RequestTransactionController, Parameters: : Latitude = " + Latitude + ",Longitude = " + Longitude + ",RequestTemplateID = " + RequestTemplateID.ToString());

            //Latitude = "1.282101559453930";
            //Longitude = "103.817224802631630";
            //RequestTemplateID = 1;
            using (RequestTransactionBL requestTransactionBL = new RequestTransactionBL())
            {
                return requestTransactionBL.GetSimilarRequests(Latitude, Longitude, RequestTemplateID);
            }
        }

        /// <summary>
        /// Get User Requests list by UserId 
        /// </summary>
        /// <param name="UserId">the long object</param>
        /// <returns>List of UserRequestTransactions Object</returns>
        [Route("GetRequestsByUserId/{UserId}")]
        [HttpGet]
        public List<UserRequestTransactions> GetRequestsByUserId(long UserId)
        {
            Logging.LogDebugMessage("Method: GetRequestsByUserId, MethodType: Get, Layer: RequestTransactionController, Parameters: : UserId = " + UserId.ToString());
            using (RequestTransactionBL requestTransactionBL = new RequestTransactionBL())
            {
                return requestTransactionBL.GetRequestsByUserId(UserId);
            }

        }

        /// <summary>
        /// Get My request details by TransactionId
        /// </summary>
        /// <param name="RequestTransactionId">the Integer object</param>
        /// <returns>MyRequestDetails Object</returns>
        [Route("GetMyRequestDetailsByTransactionId/{RequestTransactionId}")]
        [HttpGet]
        public MyRequestDetails GetMyRequestDetailsByTransactionId(int RequestTransactionId)
        {
            Logging.LogDebugMessage("Method: GetMyRequestDetailsByTransactionId, MethodType: Get, Layer: RequestTransactionController, Parameters: : RequestTransactionId = " + RequestTransactionId.ToString());
            using (RequestTransactionBL requestTransactionBL = new RequestTransactionBL())
            {
                return requestTransactionBL.GetMyRequestDetailsByTransactionId(RequestTransactionId);
            }

        }


        [Route("SendEmail")]
        [HttpGet]
        public bool SendEmail()
        {
            using (RequestTransactionBL requestTransactionBL = new RequestTransactionBL())
            {
                int UserID = 1;
                int RequestTransactionID = 1;
                int RequestTemplateID = 1;
                return requestTransactionBL.SendEmail(UserID, RequestTransactionID, RequestTemplateID);
            }
        }
    }
}