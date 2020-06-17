using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CitizenWeb.DAL;
using CitizenWeb.Models;
using Newtonsoft.Json;
using System.Net.Mail;
using CitizenWeb.Models;

namespace CitizenWeb.BL
{
    /// <summary>RequestTransactionBL.This business layer contains all the methods which are related to Setting and Getting Request Transaction.</summary>
    public class RequestTransactionBL : IDisposable
    {
        private bool disposed = false;
        private string Host = "";
        private string AdminEmailID = "";
        private string AdminEmailPassword = "";
        private string Subject = "";

        public RequestTransactionBL()
        {
            this.Host = AppSettings.Host;
            this.AdminEmailID = AppSettings.AdminEmailID;
            this.AdminEmailPassword = AppSettings.AdminEmailPassword;
            this.Subject = AppSettings.Subject;
        }

        /// <summary> Submit or track the Request </summary>
        /// <param name="requestTransaction">The object of type RequestTransaction</param>
        /// <returns> Integer object </returns>
        public int SubmitOrTrackRequest(RequestTransaction requestTransaction)
        {
            Logging.LogDebugMessage("Method: SubmitOrTrackRequest, MethodType: Post, Layer: RequestTransactionBL, Parameters: requestTransaction = " + JsonConvert.SerializeObject(requestTransaction));
            try
            {
                using (RequestTransactionDAL dl = new RequestTransactionDAL())
                {
                    string ImagesPath = Models.AppSettings.ImagesUploadPath.ToString();
                    List<RequestPhoto> lst = requestTransaction.requestPhotos;
                    if (lst != null && lst.Count > 0)
                    {
                        foreach (RequestPhoto item in lst)
                        {
                            item.ImageName = CreateFileInLocal(item.ImageUrl, item.ImageName, ImagesPath);
                            //item.ImageUrl = ImagesPath + "\\" + item.ImageName;
                            item.ImageUrl = "./" + ImagesPath.Split("\\")[ImagesPath.Split("\\").Length - 2] + "/" + ImagesPath.Split("\\")[ImagesPath.Split("\\").Length - 1] + "/" + item.ImageName;
                        }
                    }
                    requestTransaction.requestPhotos = lst;
                    int requestTransactionId = dl.SubmitOrTrackRequest(requestTransaction);
                    if (requestTransaction.IsEmailSend && requestTransaction.UserId != null)
                    {
                        Int64 userId = Convert.ToInt64(requestTransaction.UserId);
                        this.SendEmail(userId, requestTransactionId, requestTransaction.RequestTemplateId);
                    }
                    return requestTransactionId;
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SubmitOrTrackRequest, Layer: RequestTransactionBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SubmitOrTrackRequest, Layer: RequestTransactionBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary>Create File In Local.</summary>
        /// <param name="fileString">The String Object for FileString.</param>
        /// <param name="fileName">The String Object for FileName.</param>
        /// <param name="localpath">The String Object for localpath.</param>
        /// <returns>The String Object.</returns>
        public string CreateFileInLocal(string fileString, string fileName, string localpath)
        {
            string filename = "";
            try
            {
                byte[] buffer = Convert.FromBase64String(fileString);
                filename = fileName.Split(".")[0] + "_" + DateTime.Now.ToString("MMddyyyyHHmmssFFF") + "." + fileName.Split(".")[1];
                System.IO.FileStream fileStream =
                     new System.IO.FileStream(localpath + "\\" + filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return filename;
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
            Logging.LogDebugMessage("Method: GetSimilarRequests, MethodType: Get, Layer: RequestTransactionBL, Parameters: : Latitude = " + Latitude + ",Longitude = " + Longitude + ",RequestTemplateID = " + RequestTemplateID.ToString());
            try
            {
                using (RequestTransactionDAL dl = new RequestTransactionDAL())
                {
                    return dl.GetSimilarRequests(Latitude, Longitude, RequestTemplateID);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetSimilarRequests, Layer: RequestTransactionBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetSimilarRequests, Layer: RequestTransactionBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Get User Requests list by UserId 
        /// </summary>
        /// <param name="UserId">the long object</param>
        /// <returns>List of UserRequestTransactions Object</returns>
        public List<UserRequestTransactions> GetRequestsByUserId(long UserId)
        {
            Logging.LogDebugMessage("Method: GetRequestsByUserId, MethodType: Get, Layer: RequestTransactionBL, Parameters: : UserId = " + UserId.ToString());
            try
            {
                using (RequestTransactionDAL requestTransactionDAL = new RequestTransactionDAL())
                {
                    return requestTransactionDAL.GetRequestsByUserId(UserId);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetRequestsByUserId, Layer: RequestTransactionBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetRequestsByUserId, Layer: RequestTransactionBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }


        /// <summary>
        /// Get My request details by TransactionId
        /// </summary>
        /// <param name="RequestTransactionId">the Integer object</param>
        /// <returns>MyRequestDetails Object</returns>
        public MyRequestDetails GetMyRequestDetailsByTransactionId(int RequestTransactionId)
        {
            Logging.LogDebugMessage("Method: GetMyRequestDetailsByTransactionId, MethodType: Get, Layer: RequestTransactionBL, Parameters: : RequestTransactionId = " + RequestTransactionId.ToString());
            try
            {
                using (RequestTransactionDAL requestTransactionDAL = new RequestTransactionDAL())
                {
                    return requestTransactionDAL.GetMyRequestDetailsByTransactionId(RequestTransactionId);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetMyRequestDetailsByTransactionId, Layer: RequestTransactionBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetMyRequestDetailsByTransactionId, Layer: RequestTransactionBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// This method send mail to the request submitted user
        /// </summary>
        /// <param name="UserID">Integer Object</param>
        /// <param name="RequestTransactionID">Integer Object</param>
        /// <param name="RequestTemplateID">Integer Object</param>
        /// <returns>Boolean Object</returns>
        public bool SendEmail(Int64 UserID, int RequestTransactionID, Int64 RequestTemplateID)
        {
            try
            {
                EmailObject emailObject = new EmailObject();
                using (RequestTransactionDAL requestTransactionDAL = new RequestTransactionDAL())
                {
                    emailObject = requestTransactionDAL.GetEmailContent(UserID, RequestTransactionID, RequestTemplateID);
                }
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Host);

                mail.From = new MailAddress(AdminEmailID);
                mail.To.Add("venkata.kishore26@gmail.com");    //TO DO :: emailObject.EmailAddress
                // mail.Subject = Subject + "<span style='font-family:Tahoma,sans-serif;color:#15c;'><u>" + emailObject.RequestTransactionID + "</u></span> - " + emailObject.RequestStatus;
                mail.Subject = Subject + emailObject.RequestTransactionID + " - " + emailObject.RequestStatus;

                mail.IsBodyHtml = true;
                string htmlBody;

                htmlBody = GetEmailBodyContent(emailObject);
                //htmlBody = GetEmailBody();

                mail.Body = htmlBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(AdminEmailID, AdminEmailPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Track the Request Transaction.
        /// </summary>
        /// <param name="requestTransaction">The object of type TrackRequestTransaction.</param>
        /// <returns>Integer object.</returns>
        public int TrackRequestTransaction(TrackRequestTransaction request)
        {
            Logging.LogDebugMessage("Method: TrackRequestTransaction, MethodType: Post, Layer: RequestTransactionBL, Parameters: : request = " + JsonConvert.SerializeObject(request));
            try
            {
                using (RequestTransactionDAL requestTransactionDAL = new RequestTransactionDAL())
                {
                    //return requestTransactionDAL.TrackRequestTransaction(request);

                    int requestTransactionId = requestTransactionDAL.TrackRequestTransaction(request);
                    if (request.IsEmailSend && request.UserId != null)
                    {
                        Int64 userId = Convert.ToInt64(request.UserId);
                        this.SendEmail(userId, requestTransactionId, request.RequestTemplateId);
                    }
                    return requestTransactionId;
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: TrackRequestTransaction, Layer: RequestTransactionBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: TrackRequestTransaction, Layer: RequestTransactionBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        private string GetEmailBody()
        {
            //string body = "<span style='font-size: 12px;font-family:Tahoma,sans-serif'><i>This is an automated message. Please do not reply to this email.</i></span><br>" +
            //                                               "________________________________________________<br><br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>Thank you for submitting a 311 service request on 2020/05/01 16:04:12. This email provides notification that work has been completed (or referred to a responding agency) for 311 Tracking UID# <span style='color:#15c;'><b><u>1465546 </u></b></span> with the following Activity Description:<i></span><br> <br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>Address: 14432 Chicora Crossing Blvd Orlando, FL 32828 Details: Black Nissan Altima without tags has been abandoned for a month at the corner of Chicora Crossing Blvd and Rainbow Springs Blvd. I've checked with the neighbors and it does not belong to them. First Name: Last Name: Email: <span style='color:#15c;'>jbelkclark@gmail.com</span> Cell Phone: OCFL 311 iOS application<i></span><br> <br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>If your initial concern or inquiry persists, please contact us again when you have the opportunity. We are staffed <span style='color:#15c;'> from 7:00 a.m. to 9:00 p.m. Monday </span> through Friday, and <span style='color:#15c;'>9:00 a.m. to 5:00 p.m. on Saturday </span> and Sunday (excluding County holidays). You may also visit our website at http://www.ocfl.net/311.<i></span><br> <br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>Thank you,<i></span> <br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>Orange County 311  <i></span><br> <br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>311 or <span style='color:#15c;'>(407) 836-3111<i></span></span> <br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>On the Web at http://www.ocfl.net/311 <i></span><br>" +
            //    "<span style='font-size: 14px;font-family:Tahoma,sans-serif'><i>Orange County website: http://www.orangecountyfl.net/ <i></span>";

            //string body = String.Format("{0}{3}This is an automated message.Please do not reply to this email.{4}{1}{2}" +
            //                                               "________________________________________________{2}{2}" +
            //    "{10}{3}Thank you for submitting a 311 service request on 2020/05/01 16:04:12. This email provides notification that work has been completed (or referred to a responding agency) for 311 Tracking UID# {5}{6}{8}1465546 {9}{7}{1} with the following Activity Description:{4}{1}{2} {2}" +
            //    "{10}{3}Address: 14432 Chicora Crossing Blvd Orlando, FL 32828 Details: Black Nissan Altima without tags has been abandoned for a month at the corner of Chicora Crossing Blvd and Rainbow Springs Blvd. I've checked with the neighbors and it does not belong to them. First Name: Last Name: Email: {5}jbelkclark@gmail.com{1} Cell Phone: OCFL 311 iOS application{4}{1}{2} {2}" +
            //    "{10}{3}If your initial concern or inquiry persists, please contact us again when you have the opportunity. We are staffed {5} from 7:00 a.m. to 9:00 p.m. Monday {1} through Friday, and {5}9:00 a.m. to 5:00 p.m. on Saturday {1} and Sunday (excluding County holidays). You may also visit our website at http://www.ocfl.net/311.{4}{1}{2} {2}" +
            //    "{10}{3}Thank you,{4}{1} {2}" +
            //    "{10}{3}Orange County 311  {4}{1}{2} {2}" +
            //    "{10}{3}311 or {5}(407) 836-3111{4}{1}{1} {2}" +
            //    "{10}{3}On the Web at http://www.ocfl.net/311 {4}{1}{2}" +
            //    "{10}{3}Orange County website: http://www.orangecountyfl.net/ {4}{1}",
            //    "<span style='font-size:12px;font-family:Tahoma,sans-serif'>", "</span>", "<br>", "<i>", "</i>", "<span style='color:#15c;'>", "<b>", "</b>", "<u>", 
            //    "</u>", "<span style='font-size: 14px;font-family:Tahoma,sans-serif'>");


            string body = String.Format("{0}{3}This is an automated message.Please do not reply to this email.{4}{1}{2}" +
                                                           "___________________________________________{2}{2}" +
                "{10}{3}Thank you for submitting a 311 service request on {18}. This email provides notification that work has been {12} (or referred to a responding agency) for 311 Tracking UID# {5}{6}{8}{11} {9}{7}{1} with the following Activity Description:{4}{1}{2} {2}" +
                "{10}{3}Address:{13}  {14}. First Name:{15} Last Name:{16} Email: {5}{17}{1} Cell Phone: {19}{4}{1}{2} {2}" +
                "{10}{3}If your initial concern or inquiry persists, please contact us again when you have the opportunity. We are staffed {5} from 7:00 a.m. to 9:00 p.m. Monday {1} through Friday, and {5}9:00 a.m. to 5:00 p.m. on Saturday {1} and Sunday (excluding County holidays). You may also visit our website at http://www.ocfl.net/311.{4}{1}{2} {2}" +
                "{10}{3}Thank you,{4}{1} {2}" +
                "{10}{3}Orange County 311  {4}{1}{2} {2}" +
                "{10}{3}311 or {5}(407) 836-3111{4}{1}{1} {2}" +
                "{10}{3}On the Web at http://www.ocfl.net/311 {4}{1}{2}" +
                "{10}{3}Orange County website: http://www.orangecountyfl.net/ {4}{1}",


                "<span style='font-size:11px;font-family:Tahoma,sans-serif'>",
                "</span>",
                "<br>",
                "<i>",
                "</i>",
                "<span style='color:#15c;'>",
                "<b>",
                "</b>",
                "<u>",
                "</u>",
                "<span style='font-size: 13px;font-family:Tahoma,sans-serif'>",

                "1465546",
                "Open", //lower
                "14432 Chicora Crossing Blvd Orlando, FL 32828",
                "Details: Black Nissan Altima without tags has been abandoned for a month at the corner of Chicora Crossing Blvd and Rainbow Springs Blvd.I've checked with the neighbors and it does not belong to them",
                "Kishore",
                "Venkata",
                "venkata.kishore26@gmail.com",
                "2020/05/01 16:04:12",
                "8888888888"
                );




            return body;
        }

        private string GetEmailBodyContent(EmailObject emailObject)
        {
            string body = String.Format(emailObject.EmailContent,

                "<span style='font-size:12px;font-family:Tahoma,sans-serif'>",
                "</span>",
                "<br>",
                "<i>",
                "</i>",
                "<span style='color:#15c;'>",
                "<b>",
                "</b>",
                "<u>",
                "</u>",
                "<span style='font-size: 14px;font-family:Tahoma,sans-serif'>",

                emailObject.RequestTransactionID.ToString(),
                emailObject.RequestStatus.ToLower(),
                emailObject.LocationAddress,
                emailObject.Description,
                emailObject.FirstName,
                emailObject.LastName,
                emailObject.EmailAddress,
                emailObject.CreatedDatetime,
                emailObject.PhoneNumber
                ); ;




            return body;
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