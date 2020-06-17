export class AppConstants {
   // public COMM_ALERT_SAVE_RECORD: string = 'Do you want to save the record?';


    public COMM_ALERT_SELECT_IMAGE: string = 'Please select maximum 3 images.';
    public COMM_ALERT_MAIL_PWD: string = 'Please enter valid Email Id and Password.';
    public COMM_ALERT_SUBMIT: string = 'Submitted, Thank you for your support.';
    public COMM_ALERT_MAIL_EXIST: string = 'Mail Id already Exists.';
    public COMM_ALERT_HORIZANTAL: string = 'You have finished the horizontal stepper!';
    public COMM_ALERT_VERTICAL: string = 'You have finished the vertical stepper!';



    // Save Confirmations
    public COMM_ALERT_SAVE_RECORD: string = 'Do you want to save the record?';

    public COMM_ALERT_SAVE_Alert_Header: string = 'Save';

    public COMM_MSG_SAVE: string = 'Record details saved successfully.';

    public COMM_ALERT_DELETE_Alert_Header: string = 'Delete';

    public COMM_ALERT_Message: string = 'Please Select Record.';

    public AUDIT_SCHEDULE: string = 'Update AuditSchedule Failed';

    public APP_VERSION: any = '7.0.1';

    public COMM_ALERT_DELETE_RECORD: string = 'Do you want to delete the record?';
    public COMM_ALERT_DELETE_ITEMS_RECORD: string = 'Are you sure, You want to remove all the Assigned Items?';

    public COMM_ALERT_UPDATE_Alert_Header: string = 'Update';
    public COMM_ALERT_UNSAVED_Alert_Header: string = 'Unsaved Changes';

    public COMM_REQUESTTEMPLATE_DEACTIVATED: string = 'Template Deactivated Successfully..';
    public COMM_ALERT_UPDATE_RECORD: string = 'Do you want to update the record?';
    public COMM_ALERT_SECURITY_SAVE_RECORD: string = 'Do you want to save the changes ?';
    public COMM_ALERT_UNSAVED_COLUMNS: string = 'Do you want to update the column values?';
    public COMM_ALERT_SAVE_Imagr_Alert_Header: string = 'Are you sure you want save these images as attachment?';



    public COMM_SNACK_BAR_SHEET_UPDATE: string = 'Sheet Updated Successfully..';
    public COMM_INVALID_LOGIN: string = 'Please enter valid userName and Password';
    public COMM_REGISTRATION_COMPLETED: string = 'Registration completed successfully..';
    public COMM_USER_CREATE: string = 'User Created Succesfully..';
    public COMM_REQUEST_TEMPLATE_CREATE: string = 'Request Template Created Succesfully..';
    public COMM_USER_UPDATED: string = 'User Updated Succesfully.';
    public COMM_USER_NOT_VALID: string = 'Not a valid user';
    public COMM_USER_NAME_EXISTS: string = 'User Name Already Exists..';
    public COMM_USER_DEACTIVATED: string = 'User Deactivated Successfully..';
    public COMM_LOOKUP_DELETE: string = 'LookUp Deleted Successfully..';
    public COMM_LOOKUP_DETAILS_DELETE: string = 'LookUp Details Deleted Successfully..';
    public COMM_SINGLE_RECORD: string = 'Please select single record....';
    public COMM_LOOKUP_SAVE: string = 'LookUp Saved Successfully..';
    public COMM_LOOKUP_DETAILS_SAVE: string = 'LookUp Details Saved Successfully..';
    public COMM_LOOKUP_DETAILS_UPDATE: string = 'LookUp Details Updated Successfully..';
    public COMM_LOOKUP_UPDATE: string = 'LookUp Updated Successfully..';
    public COMM_MANDATORY_FIELDS: string = 'Please enter all mandatory fields.';
    public COMM_FACILITY_NAME_EXISTS: string = 'Facility Name Already Exists..';
    public COMM_FACILITY_SAVE: string = 'Facility Details Saved Succesfully..';
    public COMM_FACILITY_DELETE: string = 'Facility deleted Successfully..';
    public COMM_SERVICE_AREA_EXISTS: string = 'Service Area Name Already Exists..';
    public COMM_SERVICE_AREA_SAVE: string = 'Service Area Saved Successfully..';
    public COMM_SERVICE_AREA_DELETE: string = 'Service Area deleted Successfully..';
    public COMM_REGION_NAME: string = 'Please Enter Region Name';
    public COMM_REGION_SAVE: string = 'Region Saved Succesfully..';
    public COMM_SERVICEAREA_FACILITY_ADDED: string = 'Service Area Facility Added Successfully..';
    public COMM_SERVICEAREA_FACILITY_DELETE: string = 'Service Area Facility deleted Successfully..';
    public COMM_SERVICEAREA_TO_FACILITY_ADD: string = 'Please Select Service Area to add Facilities..';
    public COMM_ROLE_DEACTIVATED: string = 'Role Deactivated Succesfully.';
    public COMM_ROLE_UPDATED: string = 'Role Updated Successfully..';
    public COMM_ROLE_EXISTS: string = 'Role Name Already Exists..';
    public COMM_ROLE_CREATE: string = 'Role Created Successfully..';
    public COMM_ROLE_ENTER: string = 'Please Enter Role Name.';
    public COMM_ROLE_PREVILEGE_UPDATED: string = 'Role Privilege Updated Successfully..';
    public COMM_USER_ROLE_DELETE: string = 'UserRole Deleted Successfully..';
    public COMM_USER_ROLE_SAVE: string = 'User Role Saved Successfully..';
    public COMM_PAGE_CONFIGURATION_SAVE: string = 'Page Configuration Details Saved Successfully..';
    public COMM_ASSIGN_SERVICEAREA_TO_FACILTY: string = 'Please Assign facility for every service area';
    public COMM_COMMENTS_DELETE: string = 'Comments Deleted Successfully..';
    public COMM_COMMENTS_SAVE: string = 'Comments saved Successfully..';
    public COMM_COMMENTS_ENTER: string = 'Please Enter Comments for Save';
    public COMM_DATE_INVALID: string = 'Date is invalid..';
    public IDEL_AUTO_SAVE: number = 5;

    public IDEL_TIME_OUT: number = 7200;

    public NO_ROLE: string = 'No Role Assigned to this user, contact Admin';
    public USER_INACTIVE: string = 'This user Inactive';


    public COMM_ATLEAST_SINGLE_RECORD: string = 'Please select atleast single record....';

    public COMM_PRIVILEGES: any = [
        

        { privilegeCode: 'Create Users' },                         //0
        { privilegeCode: 'Edit Users' },                           //1
        { privilegeCode: 'View Users' },                          //2
        { privilegeCode: 'Delete Users' },                        //3
        { privilegeCode: 'Create Roles' },                         //4
        { privilegeCode: 'Edit Roles' },                           //5
        { privilegeCode: 'View Roles' },                          //6
        { privilegeCode: 'Delete Roles' },                        //7  
        { privilegeCode: 'Show Security' },                       //8
        { privilegeCode: 'Create Lookups' },                      //9
        { privilegeCode: 'Edit Lookups' },                        //10
        { privilegeCode: 'View Lookups' },                        //11
        { privilegeCode: 'Delete Lookups' },                      //12

        

    ];
}