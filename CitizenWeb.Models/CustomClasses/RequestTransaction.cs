using CitizenWeb.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CitizenWeb.Models
{
    public class RequestTransaction
    {
        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>The Request Template Id.</value>
        public long RequestTemplateId { get; set; }

        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public int RequestStatus { get; set; }

        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public long? UserId { get; set; }

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }

        /// <summary>Gets or sets the IsEmailSend.</summary>
        /// <value>The boolean Object.</value>
        public bool IsEmailSend { get; set; }

        /// <summary>Gets or sets the Request Location.</summary>
        /// <value>The Request Location.</value>
        public List<RequestLocation> requestLocation { get; set; }

        /// <summary>Gets or sets the Request Photos.</summary>
        /// <value>The Request Photos.</value>
        public List<RequestPhoto> requestPhotos { get; set; }

        /// <summary>Gets or sets the Request Transaction Details.</summary>
        /// <value>The Request Transaction Details.</value>
        public List<RequestTransactionDetails> requestTransactionDetails { get; set; }

        /// <summary>Gets or sets the Request Transaction History.</summary>
        /// <value>The Request Transaction History.</value>
        public List<RequestTransactionHistory> requestTransactionHistory { get; set; }

        /// <summary>Gets or sets the Request Transaction Track.</summary>
        /// <value>The Request Transaction Track.</value>
        public List<RequestTransactionTrack> requestTransactionTracks { get; set; }
    }

    public class RequestLocation
    {
        /// <summary>Gets or sets the Request Location Id.</summary>
        /// <value>The Request Location Id.</value>
        public int RequestLocationId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Location.</summary>
        /// <value>The Location.</value>
        public string Location { get; set; }

        /// <summary>Gets or sets the Latitude.</summary>
        /// <value>The Latitude.</value>
        public string Latitude { get; set; }

        /// <summary>Gets or sets the Longitude.</summary>
        /// <value>The Longitude.</value>
        public string Longitude { get; set; }

        /// <summary>Gets or sets the Street.</summary>
        /// <value>The Street.</value>
        public string Street { get; set; }

        /// <summary>Gets or sets the City.</summary>
        /// <value>The City.</value>
        public string City { get; set; }

        /// <summary>Gets or sets the State.</summary>
        /// <value>The State.</value>
        public string State { get; set; }

        /// <summary>Gets or sets the Zip.</summary>
        /// <value>The Zip.</value>
        public string Zip { get; set; }
       

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }

    }

    public class RequestPhoto
    {
        /// <summary>Gets or sets the Request Photo Id.</summary>
        /// <value>The Request Photo Id.</value>
        public int RequestPhotoId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>The Request Template Id.</value>
        public long RequestTemplateId { get; set; }

        /// <summary>Gets or sets the Image Name.</summary>
        /// <value>The Image Name.</value>
        public string ImageName { get; set; }

        /// <summary>Gets or sets the Image Type.</summary>
        /// <value>The Image Type.</value>
        public string ImageType { get; set; }

        /// <summary>Gets or sets the Image URL.</summary>
        /// <value>The Image URL.</value>
        public string ImageUrl { get; set; }

        

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }
    }

    public class RequestTransactionDetails
    {
        /// <summary>Gets or sets the Request Transaction Detail Id.</summary>
        /// <value>The Request Transaction Detail Id.</value>
        public int RequestTransactionDetailId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Section Control Id.</summary>
        /// <value>The Request Template Section Control Id.</value>
        public int RequestTemplateSectionControlId { get; set; }

        /// <summary>Gets or sets the Content.</summary>
        /// <value>The Content.</value>
        public string Content { get; set; }

        /// <summary>Gets or sets the Control Type.</summary>
        /// <value>The Control Type.</value>
        public int ControlType { get; set; }

        /// <summary>Gets or sets the Comments.</summary>
        /// <value>The Comments.</value>
        public string Comments { get; set; }

       

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }
    }

    public class RequestTransactionHistory
    {
        /// <summary>Gets or sets the Request Transaction History Id.</summary>
        /// <value>The Request Transaction History Id.</value>
        public int RequestTransactionHistoryId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public int RequestStatus { get; set; }

        /// <summary>Gets or sets the Comment.</summary>
        /// <value>The Comment.</value>
        public string Comment { get; set; }

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }
    }

    public class RequestTransactionTrack
    {
        /// <summary>Gets or sets the Request Transaction Track Id.</summary>
        /// <value>The Request Transaction Track Id.</value>
        public int RequestTransactionTrackId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>The Request Template Id.</value>
        public long RequestTemplateId { get; set; }
      

        /// <summary>Gets or sets the Submit Identity.</summary>
        /// <value>The Submit Identity.</value>
        public int SubmitIdentity { get; set; }

        /// <summary>Gets or sets the IsTrack.</summary>
        /// <value>The IsTrack.</value>
        public bool IsTrack { get; set; }

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }
    }   

    public class UserRequestTransactions
    {
        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }
        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>The Request Template Id.</value>
        public long RequestTemplateId { get; set; }
        /// <summary>Gets or sets the Request Name.</summary>
        /// <value>The Request Name.</value>
        public string RequestName { get; set; }
        /// <summary>Gets or sets the Request Category Id.</summary>
        /// <value>The Request Category Id.</value>
        public int RequestCategoryId { get; set; }
        /// <summary>Gets or sets the Department.</summary>
        /// <value>The Department.</value>
        public string Department { get; set; }
        /// <summary>Gets or sets the Created Date.</summary>
        /// <value>The Created Date.</value>
        public DateTime CreatedDate { get; set; }
        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public int RequestStatus { get; set; }
        /// <summary>Gets or sets the Request Status Text.</summary>
        /// <value>The Request Status Text.</value>
        public string RequestStatusText { get; set; }
        /// <summary>Gets or sets the Priority.</summary>
        /// <value>The Priority.</value>
        public string Priority { get; set; }
        /// <summary>Gets or sets the Last Activity.</summary>
        /// <value>The Last Activity.</value>
        public string LastActivity { get; set; }
        /// <summary>Gets or sets the Request Status Color.</summary>
        /// <value>Request Status Color.</value>
        public string RequestStatusColor { get; set; }
        /// <summary>Gets or sets the Priority Color.</summary>
        /// <value>The Priority Color.</value>
        public string PriorityColor { get; set; }
    }

    public class EmailObject
    {
        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Integer Object.</value>
        public int RequestTransactionID { get; set; }

        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The String Object.</value>
        public string RequestStatus { get; set; }

        /// <summary>Gets or sets the Created Datetime.</summary>
        /// <value>The String Object.</value>
        public string CreatedDatetime { get; set; }

        /// <summary>Gets or sets the Location Address.</summary>
        /// <value>The String Object.</value>
        public string LocationAddress { get; set; }

        /// <summary>Gets or sets the Description.</summary>
        /// <value>The String Object.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the First Name.</summary>
        /// <value>The String Object.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the Last Name.</summary>
        /// <value>The String Object.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the Email Address.</summary>
        /// <value>The String Object.</value>
        public string EmailAddress { get; set; }

        /// <summary>Gets or sets the Phone Number.</summary>
        /// <value>The String Object.</value>
        public string PhoneNumber { get; set; }

        /// <summary>Gets or sets the Email Content.</summary>
        /// <value>The String Object.</value>
        public string EmailContent { get; set; }
    }

    public class SimilarRequestTransaction
    {
        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>The Request Template Id.</value>
        public long RequestTemplateId { get; set; }

        /// <summary>Gets or sets the Request Template Name.</summary>
        /// <value>The Request Template Name.</value>
        public string RequestTemplateName { get; set; }

        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public int RequestStatus { get; set; }

        /// <summary>Gets or sets the Request Status Text.</summary>
        /// <value>The Request Status Text.</value>
        public string RequestStatusText { get; set; }

        /// <summary>Gets or sets the Color.</summary>
        /// <value>String Object.</value>
        public string Color { get; set; }

        /// <summary>Gets or sets the Created Date Text.</summary>
        /// <value>String Object.</value>
        public string CreatedDateText { get; set; }

        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public long? UserId { get; set; }

        /// <summary>Gets or sets the RowStatus.</summary>
        /// <value>The RowStatus.</value>
        public string RowStatus { get; set; }

        /// <summary>Gets or sets the Request Location.</summary>
        /// <value>The Request Location.</value>
        public RequestLocation requestLocation { get; set; }

        /// <summary>Gets or sets the Request Photos.</summary>
        /// <value>The Request Photos.</value>
        public List<RequestPhoto> requestPhotos { get; set; }

        /// <summary>Gets or sets the Request Transaction Details.</summary>
        /// <value>The Request Transaction Details.</value>
        public List<RequestTransactionDetails> requestTransactionDetails { get; set; }
        /// <summary>Gets or sets the Controls.</summary>
        /// <value>List of RequestTransactionDetailsControls object</value>
        public List<RequestTransactionDetailsControls> Controls { get; set; }

    }

    public class RequestTransactionDetailsControls
    {
        /// <summary>Gets or sets the Request Transaction Detail Id.</summary>
        /// <value>The Request Transaction Detail Id.</value>
        public int RequestTransactionDetailId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Section Control Id.</summary>
        /// <value>The Request Template Section Control Id.</value>
        public int RequestTemplateSectionControlId { get; set; }

        /// <summary>Gets or sets the Content.</summary>
        /// <value>The Content.</value>
        public string Content { get; set; }

        /// <summary>Gets or sets the Control Type.</summary>
        /// <value>The Control Type.</value>
        public int ControlType { get; set; }

        /// <summary>Gets or sets the Control Type Text.</summary>
        /// <value>String Object.</value>
        public string ControlTypeText { get; set; }

        /// <summary>Gets or sets the Control Label.</summary>
        /// <value>String Object.</value>
        public string ControlLabel { get; set; }

        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>Integer Object..</value>
        public Int64 RequestTemplateId { get; set; }

        /// <summary>Gets or sets the Seq No.</summary>
        /// <value>Integer Object.</value>
        public int SeqNo { get; set; }
        /// <summary>Gets or sets the Check Box Options.</summary>
        /// <value>List of RequestTransactionDetailsControlTypes Object.</value>
        public List<RequestTransactionDetailsControlType> CheckBoxOptions { get; set; }

    }

    public class RequestTransactionDetailsControlType
    {
        /// <summary>Gets or sets the Request Transaction Detail Id.</summary>
        /// <value>The Request Transaction Detail Id.</value>
        public int RequestTransactionDetailId { get; set; }

        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }

        /// <summary>Gets or sets the Request Template Section Control Id.</summary>
        /// <value>The Request Template Section Control Id.</value>
        public int RequestTemplateSectionControlId { get; set; }

        /// <summary>Gets or sets the Control Type Text.</summary>
        /// <value>String Object.</value>
        public string ControlTypeText { get; set; }

        /// <summary>Gets or sets the Control Label.</summary>
        /// <value>String Object.</value>
        public string ControlLabel { get; set; }

        /// <summary>Gets or sets the Seq No.</summary>
        /// <value>Integer Object.</value>
        public int SeqNo { get; set; }

        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>Integer Object..</value>
        public Int64 RequestTemplateId { get; set; }
    }

    public class MyRequestDetails
    {
        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }
        /// <summary>Gets or sets the Request Template Id.</summary>
        /// <value>The Request Template Id.</value>
        public long RequestTemplateId { get; set; }
        /// <summary>Gets or sets the Request Name.</summary>
        /// <value>The Request Name.</value>
        public string RequestName { get; set; }

        /// <summary>Gets or sets the Created Date.</summary>
        /// <value>The Created Date.</value>
        public string CreatedDate { get; set; }
        /// <summary>Gets or sets the Request Status.</summary>
        /// <value>The Request Status.</value>
        public int RequestStatus { get; set; }
        /// <summary>Gets or sets the Request Status Text.</summary>
        /// <value>The Request Status Text.</value>
        public string RequestStatusText { get; set; }
        /// <summary>Gets or sets the Priority.</summary>
        /// <value>The Priority.</value>
        public string Priority { get; set; }

        public List<MyRequestDetailControls> myRequestDetailControls { get; set; }
        public List<RequestPhoto> requestPhotos { get; set; }
    }

    public class MyRequestDetailControls
    {
        /// <summary>Gets or sets the Request Transaction Detail Id.</summary>
        /// <value>The Request Transaction Detail Id.</value>
        public int RequestTransactionDetailId { get; set; }
        /// <summary>Gets or sets the Request Transaction Id.</summary>
        /// <value>The Request Transaction Id.</value>
        public int RequestTransactionId { get; set; }
        /// <summary>Gets or sets the Request Template Section ControlId.</summary>
        /// <value>The Request Template Section ControlId.</value>
        public int RequestTemplateSectionControlId { get; set; }
        /// <summary>Gets or sets the Value.</summary>
        /// <value>The Value.</value>
        public string Value { get; set; }

        /// <summary>Gets or sets the Name.</summary>
        /// <value>The Name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the SeqNo.</summary>
        /// <value>The SeqNo.</value>
        public int SeqNo { get; set; }

        /// <summary>Gets or sets the Type.</summary>
        /// <value>The Type.</value>
        public string Type { get; set; }

        /// <summary>Gets or sets the  Label.</summary>
        /// <value>The Label.</value>
        public string Label { get; set; }

        /// <summary>Gets or sets the Required.</summary>
        /// <value>The Required.</value>
        public bool Required { get; set; }

        public List<MyRequestDetailControlOptions> CheckBoxOptions { get; set; }
    }

    public class MyRequestDetailControlOptions
    {
        /// <summary>Gets or sets the RequestTemplateSectionControlId.</summary>
        /// <value>The Integer Object.</value>
        public int RequestTemplateSectionControlId { get; set; }
        
        /// <summary>Gets or sets the Value.</summary>
        /// <value>The Value.</value>
        public bool Value { get; set; }

        /// <summary>Gets or sets the Key.</summary>
        /// <value>The Key.</value>
        public string Key { get; set; }

        /// <summary>Gets or sets the Label.</summary>
        /// <value>The Label.</value>
        public string Label { get; set; }
    }

}

public class TrackRequestTransaction
{
    /// <summary>Gets or sets the Request Transaction Id.</summary>
    /// <value>The Request Transaction Id.</value>
    public int RequestTransactionId { get; set; }
    /// <summary>Gets or sets the User Id.</summary>
    /// <value>The UserId.</value>
    public long? UserId { get; set; }

    /// <summary>Gets or sets the IsEmailSend.</summary>
    /// <value>The boolean Object.</value>
    public bool IsEmailSend { get; set; }

    /// <summary>Gets or sets the Request Template Id.</summary>
    /// <value>Integer Object..</value>
    public Int64 RequestTemplateId { get; set; }
    /// <summary>Gets or sets the Request Transaction History.</summary>
    /// <value>The Request Transaction History.</value>
    public List<RequestTransactionHistory> requestTransactionHistory { get; set; }

    /// <summary>Gets or sets the Request Transaction Track.</summary>
    /// <value>The Request Transaction Track.</value>
    public List<RequestTransactionTrack> requestTransactionTracks { get; set; }
}
