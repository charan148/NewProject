using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CitizenWeb.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace CitizenWeb.DAL
{
    /// <summary>RequestTransactionDAL.This data access layer contains all the methods which are related to Setting and Getting Request Transaction.</summary>
    public class RequestTransactionDAL : IDisposable
    {
        private string connectionString { get; set; }
        AppConstants appConstants = new AppConstants();
        private bool disposed = false;
        public RequestTransactionDAL()
        {
            this.connectionString = AppSettings.ConnectionString.ToString();
        }

        /// <summary> Submit or track the Request </summary>
        /// <param name="requestTransaction">The object of type RequestTransaction</param>
        /// <returns> Integer object </returns>
        public int SubmitOrTrackRequest(RequestTransaction request)
        {
            int transactionID = 0;
            Logging.LogDebugMessage("Method: SubmitOrTrackRequest, MethodType: Post, Layer: RequestTransactionDAL, Parameters: request = " + JsonConvert.SerializeObject(request));
            var ds = new DataSet();
            DataTable dataTableOne = new DataTable();
            dataTableOne = EntityCollectionHelper.ConvertTo<RequestLocation>(request.requestLocation);
            DataTable dataTableTwo = new DataTable();
            dataTableTwo = EntityCollectionHelper.ConvertTo<RequestPhoto>(request.requestPhotos);
            DataTable dataTableThree = new DataTable();
            dataTableThree = EntityCollectionHelper.ConvertTo<RequestTransactionDetails>(request.requestTransactionDetails);
            DataTable dataTableFour = new DataTable();
            dataTableFour = EntityCollectionHelper.ConvertTo<RequestTransactionTrack>(request.requestTransactionTracks);
            DataTable dataTableFive = new DataTable();
            dataTableFive = EntityCollectionHelper.ConvertTo<RequestTransactionHistory>(request.requestTransactionHistory);
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_SumitOrTrackRequest";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTransactionId", Value = request.RequestTransactionId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateId", Value = request.RequestTemplateId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestStatus", Value = request.RequestStatus });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = request.UserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RowStatus", Value = request.RowStatus });
                    SqlParameter structuredParamOne = new SqlParameter("@RequestLocation", SqlDbType.Structured);
                    structuredParamOne.Value = dataTableOne;
                    command.Parameters.Add(structuredParamOne);
                    SqlParameter structuredParamTwo = new SqlParameter("@RequestPhotos", SqlDbType.Structured);
                    structuredParamTwo.Value = dataTableTwo;
                    command.Parameters.Add(structuredParamTwo);
                    SqlParameter structuredParamthree = new SqlParameter("@RequestTransactionDetails", SqlDbType.Structured);
                    structuredParamthree.Value = dataTableThree;
                    command.Parameters.Add(structuredParamthree);
                    SqlParameter structuredParamFour = new SqlParameter("@RequestTransactionTrack", SqlDbType.Structured);
                    structuredParamFour.Value = dataTableFour;
                    command.Parameters.Add(structuredParamFour);
                    SqlParameter structuredParamFive = new SqlParameter("@RequestTransactionHistory", SqlDbType.Structured);
                    structuredParamFive.Value = dataTableFive;
                    command.Parameters.Add(structuredParamFive);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            transactionID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SubmitOrTrackRequest, Layer: RequestTransactionDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SubmitOrTrackRequest, Layer: RequestTransactionDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return transactionID;
        }

        /// <summary>
        /// Gets the similar requests list
        /// </summary>
        /// <param name="Latitude">The string object</param>
        /// <param name="Longitude">The string object</param>
        /// <param name="RequestTemplateId">The integer object</param>
        /// <returns>list of SimilarRequestTransactions object</returns>
        public List<SimilarRequestTransaction> GetSimilarRequests(string Latitude, string Longitude, int RequestTemplateID)
        {
            Logging.LogDebugMessage("Method: GetSimilarRequests, MethodType: Get, Layer: RequestTransactionDAL, Parameters: : Latitude = " + Latitude + ",Longitude = " + Longitude + ",RequestTemplateID = " + RequestTemplateID.ToString());
            var dataSet = new DataSet();
            List<SimilarRequestTransaction> SimilarRequests = new List<SimilarRequestTransaction>();

            List<RequestLocation> RequestLocationList = new List<RequestLocation>();
            List<RequestPhoto> RequestPhotosList = new List<RequestPhoto>();
            List<RequestTransactionDetails> RequestTransactionDetailsList = new List<RequestTransactionDetails>();
            List<RequestTransactionDetailsControls> RequestTransactionDetailsControlsList = new List<RequestTransactionDetailsControls>();
            List<RequestTransactionDetailsControls> ControlsList = new List<RequestTransactionDetailsControls>();
            List<RequestTransactionDetailsControlType> RequestTransactionDetailsControlTypeList = new List<RequestTransactionDetailsControlType>();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_GetSimilarRequests";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Latitude", Value = Latitude });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@Longitude", Value = Longitude });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateID", Value = RequestTemplateID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            SimilarRequests = EntityCollectionHelper.ConvertTo<SimilarRequestTransaction>(dataSet.Tables[0]).ToList();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 1)
                        {
                            RequestLocationList = EntityCollectionHelper.ConvertTo<RequestLocation>(dataSet.Tables[1]).ToList();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 2)
                        {
                            RequestPhotosList = EntityCollectionHelper.ConvertTo<RequestPhoto>(dataSet.Tables[2]).ToList();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 3)
                        {
                            RequestTransactionDetailsList = EntityCollectionHelper.ConvertTo<RequestTransactionDetails>(dataSet.Tables[3]).ToList();
                        }

                        if (dataSet != null && dataSet.Tables.Count > 4)
                        {
                            RequestTransactionDetailsControlsList = EntityCollectionHelper.ConvertTo<RequestTransactionDetailsControls>(dataSet.Tables[4]).ToList();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 5)
                        {
                            RequestTransactionDetailsControlTypeList = EntityCollectionHelper.ConvertTo<RequestTransactionDetailsControlType>(dataSet.Tables[5]).ToList();
                            foreach (RequestTransactionDetailsControlType transactionDetailsControlType in RequestTransactionDetailsControlTypeList)
                            {
                                transactionDetailsControlType.ControlLabel = transactionDetailsControlType.ControlLabel.Split("(")[0];
                            }
                        }

                        foreach (RequestTransactionDetailsControls requestTransactionDetailsControl in RequestTransactionDetailsControlsList)
                        {
                            if (ControlsList.Where(control => control.RequestTransactionId == requestTransactionDetailsControl.RequestTransactionId && control.SeqNo == requestTransactionDetailsControl.SeqNo) == null ||
                                ControlsList.Where(control => control.RequestTransactionId == requestTransactionDetailsControl.RequestTransactionId && control.SeqNo == requestTransactionDetailsControl.SeqNo).Count() == 0)
                            {
                                requestTransactionDetailsControl.ControlLabel = requestTransactionDetailsControl.ControlLabel.Split("(")[0];
                                ControlsList.Add(requestTransactionDetailsControl);
                            }
                        }

                        foreach (RequestTransactionDetailsControls requestTransactionDetailsControl in ControlsList)
                        {
                            requestTransactionDetailsControl.CheckBoxOptions = RequestTransactionDetailsControlTypeList.Where(control => control.RequestTransactionId == requestTransactionDetailsControl.RequestTransactionId && control.SeqNo == requestTransactionDetailsControl.SeqNo).ToList();

                        }

                        if (SimilarRequests != null && SimilarRequests.Count > 0)
                        {
                            foreach (SimilarRequestTransaction similarRequest in SimilarRequests)
                            {
                                similarRequest.requestLocation = RequestLocationList.Where(location => location.RequestTransactionId == similarRequest.RequestTransactionId).FirstOrDefault();
                                similarRequest.requestPhotos = RequestPhotosList.Where(photo => photo.RequestTransactionId == similarRequest.RequestTransactionId).ToList();
                                similarRequest.requestTransactionDetails = RequestTransactionDetailsList.Where(transactionDetails => transactionDetails.RequestTransactionId == similarRequest.RequestTransactionId).ToList();
                                similarRequest.Controls = ControlsList.Where(control => control.RequestTransactionId == similarRequest.RequestTransactionId).ToList();
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetSimilarRequests, Layer: RequestTransactionDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetSimilarRequests, Layer: RequestTransactionDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return SimilarRequests;
        }

        /// <summary>
        /// Get User Requests list by UserId 
        /// </summary>
        /// <param name="UserId">the long object</param>
        /// <returns>List of UserRequestTransactions Object</returns>
        public List<UserRequestTransactions> GetRequestsByUserId(long UserId)
        {
            Logging.LogDebugMessage("Method: GetRequestsByUserId, MethodType: Get, Layer: RequestTransactionDAL, Parameters: : UserId = " + UserId.ToString());
            var dataSet = new DataSet();
            List<UserRequestTransactions> UserRequests = new List<UserRequestTransactions>();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_GetRequestsByUserId";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = UserId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            UserRequests = EntityCollectionHelper.ConvertTo<UserRequestTransactions>(dataSet.Tables[0]).ToList();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRequestsByUserId, Layer: RequestTransactionDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRequestsByUserId, Layer: RequestTransactionDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return UserRequests;

        }

        /// <summary>
        /// Gets the Email Content Object
        /// </summary>
        /// <param name="UserID">The integer object</param>
        /// <param name="RequestTransactionID">The integer object</param>
        /// <param name="RequestTemplateID">The integer object</param>
        /// <returns>list of Email Object object</returns>
        public EmailObject GetEmailContent(Int64 UserID, int RequestTransactionID, Int64 RequestTemplateID)
        {
            Logging.LogDebugMessage("Method: GetEmailContent, MethodType: Get, Layer: RequestTransactionDAL, Parameters: : UserID = " + UserID.ToString() + ",RequestTransactionID = " + RequestTransactionID.ToString() + ",RequestTemplateID = " + RequestTemplateID.ToString());
            var dataSet = new DataSet();
            EmailObject emailObject = new EmailObject();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_GetEmailContent";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserID", Value = UserID });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTransactionID", Value = RequestTransactionID });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTemplateID", Value = RequestTemplateID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            emailObject = EntityCollectionHelper.ConvertTo<EmailObject>(dataSet.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetEmailContent, Layer: RequestTransactionDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetEmailContent, Layer: RequestTransactionDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return emailObject;
        }

        /// <summary>
        /// Get My request details by TransactionId
        /// </summary>
        /// <param name="RequestTransactionId">the Integer object</param>
        /// <returns>MyRequestDetails Object</returns>
        public MyRequestDetails GetMyRequestDetailsByTransactionId(int RequestTransactionId)
        {
            Logging.LogDebugMessage("Method: GetMyRequestDetailsByTransactionId, MethodType: Get, Layer: RequestTransactionDAL, Parameters: : RequestTransactionId = " + RequestTransactionId.ToString());
            var dataSet = new DataSet();
            MyRequestDetails myRequestDetails = new MyRequestDetails();
            List<MyRequestDetailControls> myRequestDetailControlsList = new List<MyRequestDetailControls>();
            List<MyRequestDetailControls> ControlsList = new List<MyRequestDetailControls>();
            List<RequestPhoto> requestPhotos = new List<RequestPhoto>();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_GetMyrequestDetailsByTransactionId";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTransactionId", Value = RequestTransactionId });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            myRequestDetails = EntityCollectionHelper.ConvertTo<MyRequestDetails>(dataSet.Tables[0]).FirstOrDefault();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 1)
                        {
                            ControlsList = EntityCollectionHelper.ConvertTo<MyRequestDetailControls>(dataSet.Tables[1]).ToList();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 2)
                        {
                            requestPhotos = EntityCollectionHelper.ConvertTo<RequestPhoto>(dataSet.Tables[2]).ToList();
                        }
                        myRequestDetails.requestPhotos = requestPhotos;


                        foreach (MyRequestDetailControls Control in ControlsList)
                        {
                            Control.Label = Control.Label.Split("(")[0];
                            if (myRequestDetailControlsList.Where(control => control.SeqNo == Control.SeqNo) == null ||
                                myRequestDetailControlsList.Where(control => control.SeqNo == Control.SeqNo).Count() == 0)
                            {

                                myRequestDetailControlsList.Add(Control);
                            }
                        }
                        foreach (MyRequestDetailControls Control in myRequestDetailControlsList)
                        {
                            if (ControlsList.Where(cntrl => cntrl.Type.ToLower() == appConstants.CheckBox && cntrl.SeqNo == Control.SeqNo).ToList() != null && ControlsList.Where(cntrl => cntrl.Type.ToLower() == appConstants.CheckBox && cntrl.SeqNo == Control.SeqNo).ToList().Count() > 0)
                            {
                                List<MyRequestDetailControlOptions> myRequestDetailControlOptions = ControlsList.Where(cntrl => cntrl.Type.ToLower() == appConstants.CheckBox && cntrl.SeqNo == Control.SeqNo).Select(controlOptions => new MyRequestDetailControlOptions()
                                {
                                    RequestTemplateSectionControlId = controlOptions.RequestTemplateSectionControlId,
                                    Value = controlOptions.Value != null && controlOptions.Value != "" && controlOptions.RequestTransactionDetailId != 0 ? true : false,
                                    Label = controlOptions.Value,
                                    Key = Regex.Replace(controlOptions.Value.ToLower(), @"\s+", "")
                                }).ToList();
                                myRequestDetailControlOptions = myRequestDetailControlOptions.OrderBy(requestDetails => requestDetails.RequestTemplateSectionControlId).ToList();
                                Control.CheckBoxOptions = myRequestDetailControlOptions;
                            }
                        }

                        myRequestDetails.myRequestDetailControls = myRequestDetailControlsList;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetMyRequestDetailsByTransactionId, Layer: RequestTransactionDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetMyRequestDetailsByTransactionId, Layer: RequestTransactionDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return myRequestDetails;

        }


        /// <summary> Track the Request Transaction </summary>
        /// <param name="requestTransaction">The object of type TrackRequestTransaction</param>
        /// <returns> Integer object </returns>
        public int TrackRequestTransaction(TrackRequestTransaction request)
        {
            int transactionID = 0;
            Logging.LogDebugMessage("Method: TrackRequestTransaction, MethodType: Post, Layer: RequestTransactionDAL, Parameters: request = " + JsonConvert.SerializeObject(request));
            var ds = new DataSet();

            DataTable dataTableOne = new DataTable();
            dataTableOne = EntityCollectionHelper.ConvertTo<RequestTransactionTrack>(request.requestTransactionTracks);
            DataTable dataTableTwo = new DataTable();
            dataTableTwo = EntityCollectionHelper.ConvertTo<RequestTransactionHistory>(request.requestTransactionHistory);
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.connectionString);
                    command.CommandText = "USP_TrackRequestTransaction";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RequestTransactionId", Value = request.RequestTransactionId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = request.UserId });
                    SqlParameter structuredParamOne = new SqlParameter("@RequestTransactionTrack", SqlDbType.Structured);
                    structuredParamOne.Value = dataTableOne;
                    command.Parameters.Add(structuredParamOne);
                    SqlParameter structuredParamTwo = new SqlParameter("@RequestTransactionHistory", SqlDbType.Structured);
                    structuredParamTwo.Value = dataTableTwo;
                    command.Parameters.Add(structuredParamTwo);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            transactionID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: TrackRequestTransaction, Layer: RequestTransactionDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: TrackRequestTransaction, Layer: RequestTransactionDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return transactionID;
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
