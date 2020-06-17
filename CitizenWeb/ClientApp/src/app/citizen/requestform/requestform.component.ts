import { Component, OnDestroy, OnInit, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { MatStepper } from '@angular/material/stepper';
import { FuseConfigService } from '@fuse/services/config.service';
import { SharedServiceService } from 'app/services/shared-service.service';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { takeUntil } from 'rxjs/operators';
import { AuthenticationService } from '../../authentication/authentication.service';
import { SubmitComponent } from '../childforms/submit/submit.component'
import { RequestFormService } from 'app/services/requestform.service';
import { RequestTransactionService } from 'app/services/request-transaction.service';
import { Base64Service } from 'app/authentication/base64.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { MatDialog } from '@angular/material/dialog';
import { AppConstants } from '../../app.constants';
declare var google;

@Component({
    selector: 'requestform',
    templateUrl: './requestform.component.html',
    styleUrls: ['./requestform.component.scss']
})
export class RequestFormComponent implements OnInit, OnDestroy {
    @ViewChild(SubmitComponent, { static: false }) submitcomponent: SubmitComponent;
    @ViewChild('stepper') stepper: MatStepper;

    horizontalStepperStep1: FormGroup;
    horizontalStepperStep2: FormGroup;
    horizontalStepperStep3: FormGroup;
    similarRequestsList: any;
    // Vertical Stepper
    verticalStepperStep1: FormGroup;
    verticalStepperStep2: FormGroup;
    verticalStepperStep3: FormGroup;

    locations = [];

    submitIdentity: any;
    OpenRequestStatus: any;
    UserID: any = null;
    SelectedlocationObj: any;
    trackData = [];
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _authenticationService: AuthenticationService,
        private requestFormService: RequestFormService,
        private sharedService: SharedServiceService,
        private requestTransactionService: RequestTransactionService,
        public snackBar: MatSnackBar,
        private _base64: Base64Service,
        public dialog: MatDialog,
        private appConstants: AppConstants

    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: false
                },
                toolbar: {
                    hidden: false
                },
                footer: {
                    hidden: false
                },
                sidepanel: {
                    hidden: false
                }
            }
        };
    }
    get f() { return this.registerForm.controls; }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    reportName: string = '';
    discription = '';
    location: string = '';
    garbagecheck: boolean;
    recycle: boolean;
    yardwaste: boolean;
    //register = true;
    btnName = 'Continue';
    reportExist: boolean;
    fallowbtn: boolean;
    reqTrsctnId = 0;
    submitself = 'Submit as yourself';
    submitAnony = 'Submit Anonymously';
    submitstepperhead = 'Submit';
    tracktepperhead = 'Submit';
    @ViewChild('browse', { static: false }) private browseref: ElementRef;
    @ViewChild('search', { static: true }) public searchElementRef: ElementRef;
    @ViewChild('scroll', { static: true }) private scrollLast: ElementRef;

    public latitude: number;
    public longitude: number;

    public zoom: number;

    multichek: boolean;
    locationchk: boolean;
    isReq: boolean;
    isHideTab: boolean;
    register = true;
    isRegButton: boolean;
    isSub: boolean;
    isForgot: boolean;
    isReg: boolean;
    isSignIn: boolean;
    isloggedIn: any;
    isSignButton: boolean;
    isSubReq: boolean;
    form: FormGroup;
    loginForm: FormGroup;
    registerForm: FormGroup;
    forgotPasswordForm: FormGroup;
    urls: any = [];
    isCompleted = false;


    public detailsForm: FormGroup;
    unsubcribe: any


    previewData = [];
    requestformData: any;
    dynamicJsonFields = [];


    //save
    requestTransaction = {};
    requestTemplateId: any;
    locationObj = [];
    requestPhotos = [];
    requestTransactionDetails = [];
    userList: any = [];
    emailAddress: any = [];
    userEmail: any = [];
    SubmitTrackLookupList: any = [];
    RequestStatusLookupList: any = [];
    IsEmailSend: any;


    ngOnInit(): void {
        this.reportName = sessionStorage.getItem('reportName');

        if (this.reportName == 'Report a Missed Garbage Dumpster Pick-Up') {
            this.multichek = true;
            this.requestTemplateId = 2;
        } else {
            this.multichek = false;

            this.requestTemplateId = 16;
        }

        this.requestFormService.GetRequestFormTemplateByCategoryIDAndTemplateID(1, this.requestTemplateId).subscribe(
            (data) => {
                this.requestformData = data;
                this.previewData = [];
                let lookups = data.lookupsList;
                //this.SubmitLookupList = lookups.filter(tt => tt.lookupName == 'Submit');
                //this.TrackLookupList = lookups.filter(tt => tt.lookupName == 'Track');
                this.SubmitTrackLookupList = lookups.filter(tt => tt.lookupName != 'Request Status');
                this.RequestStatusLookupList = lookups.filter(tt => tt.lookupName == 'Request Status');
                this.OpenRequestStatus = this.RequestStatusLookupList.filter(tt => tt.lookupDetailsValue == 'Open')[0].lookupDetailsId;
                this.previewData = this.requestformData.previewDetails.previewTemplateControlsList;
                for (let k = 0; k < this.previewData.length; k++) {
                    if (this.previewData[k].type == 'label') {
                        this.previewData[k][this.previewData[k].name] = ''
                    }
                    if (this.previewData[k].type == 'ul') {
                        this.previewData[k][this.previewData[k].name] = [];
                    }

                    if (this.previewData[k].type == '') {
                        this.previewData[k][this.previewData[k].name] = '';
                    }

                }
                let details = data.templateDetailsList.filter(tt => tt.taskStepName == 'Details');
                if (details.length != 0) {
                    this.dynamicJsonFields = details[0].templateControls;
                    for (let i = 0; i < this.dynamicJsonFields.length; i++) {
                        if (this.dynamicJsonFields[i].type == 'textarea' || this.dynamicJsonFields[i].type == 'textbox') {
                            this.dynamicJsonFields[i].input = this.inputChange.bind(this);
                        }
                        if (this.dynamicJsonFields[i].type == 'checkbox') {
                            for (let j = 0; j < this.dynamicJsonFields[i].templateControlOptions.length; j++) {
                                this.dynamicJsonFields[i].templateControlOptions[j].change = this.checkValue.bind(this);
                            }
                        }
                        if (this.dynamicJsonFields[i].type == 'dropdown' || this.dynamicJsonFields[i].type == 'radiobutton') {
                            this.dynamicJsonFields[i].change = this.checkValue.bind(this);
                            //for (let k = 0; k < this.dynamicJsonFields[i].templateControlOptions.length; k++) {
                            //    this.dynamicJsonFields[i].templateControlOptions[k].change = this.checkValue.bind(this);
                            //}
                        }
                    }

                    this.reportExist = false;
                }


                // this.map();
                this.locations = [];



                this.detailsForm = new FormGroup({
                    fields: new FormControl(JSON.stringify(this.dynamicJsonFields))
                })
                this.unsubcribe = this.detailsForm.valueChanges.subscribe((update) => {
                    this.dynamicJsonFields = JSON.parse(update.fields);
                });

                this.isloggedIn = localStorage.getItem('login');
                if (this.isloggedIn != null && this.isloggedIn != undefined && this.isloggedIn != 'null') {
                    this.isloggedIn = JSON.parse(this.isloggedIn);
                    this.UserID = this.isloggedIn.userId;
                    this.isHideTab = false;
                    this.register = false;
                } else {
                    this.isHideTab = true;
                    this.register = true;
                    this.UserID = null;
                }

            });


    }

    locationSearchs(data) {
        this.location = data;
        this.locationObj.push({ RequestLocationId: 0, RequestTransactionId: 0, Location: data, Latitude: '', Longitude: '', Street: '', City: '', Zip: '', UserId: this.UserID, RowStatus: 'A' });
    }

    confirmDetails(details: any) {

        this.discription = details.textarea1;
    }
    //getFields() {
    //    return this.dynamicJsonFields;
    //}
    inputChange(event, name) {
        let controlId = this.dynamicJsonFields.find(tt => tt.name == name)['requestTemplateSectionControlId'];
        let controlType = this.dynamicJsonFields.find(tt => tt.name == name)['controlType'];
        if (event.value[name].trim() != '') {
            for (let i = 0; i < this.previewData.length; i++) {
                if (this.previewData[i].name == name) {
                    this.previewData[i][name] = event.value[name];

                    if (this.requestTransactionDetails.length == 0) {
                        this.requestTransactionDetails.push({ RequestTransactionDetailId: 0, RequestTransactionId: 0, RequestTemplateSectionControlId: controlId, Content: event.value[name], ControlType: controlType, Comments: '', UserId: this.UserID, RowStatus: 'A' });
                    } else {
                        const chkCount = this.requestTransactionDetails.filter(tt => tt.RequestTemplateSectionControlId == controlId);
                        if (chkCount.length != 0) {
                            const index = this.requestTransactionDetails.findIndex(tt => tt.RequestTemplateSectionControlId == controlId);
                            this.requestTransactionDetails.splice(index, 1);
                        }
                        this.requestTransactionDetails.push({ RequestTransactionDetailId: 0, RequestTransactionId: 0, RequestTemplateSectionControlId: controlId, Content: event.value[name], ControlType: controlType, Comments: '', UserId: this.UserID, RowStatus: 'A' });
                    }
                }
            }
        } else {
            const chkCount = this.requestTransactionDetails.filter(tt => tt.RequestTemplateSectionControlId == controlId);
            if (chkCount.length != 0) {
                const index = this.requestTransactionDetails.findIndex(tt => tt.RequestTemplateSectionControlId == controlId);
                this.requestTransactionDetails.splice(index, 1);
            }
        }
        const requiredValues = this.dynamicJsonFields.filter(tt => tt.required == true);
        for (let i = 0; i < requiredValues.length; i++) {
            if (requiredValues[i].type == 'textarea') {
                if (event.value[requiredValues[i].name] == undefined || event.value[requiredValues[i].name] == null || event.value[requiredValues[i].name] == '') {
                    this.isCompleted = false;
                    return;
                }
            }
            if (requiredValues[i].type == 'checkbox') {
                let chkValidation = false;
                for (let k = 0; k < requiredValues[i].templateControlOptions.length; k++) {
                    if (event.value[requiredValues[i].name][requiredValues[i].templateControlOptions[k].key]) {
                        chkValidation = true;
                    }
                }
                if (!chkValidation) {
                    this.isCompleted = false;
                    return;

                }

            }
        }
        this.isCompleted = true;
    }
    listItems = [];
    checkValue(key, event, label, name, type) {
        let controlId = this.dynamicJsonFields.find(tt => tt.name == name).templateControlOptions.find(tt => tt.key == key)['requestTemplateSectionControlId']
        let controlType = this.dynamicJsonFields.find(tt => tt.name == name)['controlType'];
        if (type == 'check') {
            const value = event.value[name][key];
            if (value) {
                for (let i = 0; i < this.previewData.length; i++) {
                    if (this.previewData[i].name == name) {
                        this.previewData[i][name].push({ key: key, label: label });
                        this.requestTransactionDetails.push({ RequestTransactionDetailId: 0, RequestTransactionId: 0, RequestTemplateSectionControlId: controlId, Content: label, ControlType: controlType, Comments: '', UserId: this.UserID, RowStatus: 'A' });
                    }
                }
                this.dynamicJsonFields.find(tt => tt.name == name)['errormsg'] = false;
            } else {
                for (let i = 0; i < this.previewData.length; i++) {
                    if (this.previewData[i].name == name) {
                        const index = this.previewData[i][name].findIndex(tt => tt.key == key);
                        this.previewData[i][name].splice(index, 1);
                        const reIndex = this.requestTransactionDetails.findIndex(tt => tt.Content == label);
                        this.requestTransactionDetails.splice(reIndex, 1);
                    }
                }

            }

        } else if (type == 'dropdwn' || type == 'radio') {
                for (let i = 0; i < this.previewData.length; i++) {
                    if (this.previewData[i].name == name) {
                        this.previewData[i][name] = label;
                        this.requestTransactionDetails.push({ RequestTransactionDetailId: 0, RequestTransactionId: 0, RequestTemplateSectionControlId: controlId, Content: label, ControlType: controlType, Comments: '', UserId: this.UserID, RowStatus: 'A' });
                    }
                }
                this.dynamicJsonFields.find(tt => tt.name == name)['errormsg'] = false;
        }
        //else if (type == 'radio') {
        //    for (let i = 0; i < this.previewData.length; i++) {
        //        if (this.previewData[i].name == name) {
        //            this.previewData[i][name] = label;
        //            this.requestTransactionDetails.push({ RequestTransactionDetailId: 0, RequestTransactionId: 0, RequestTemplateSectionControlId: controlId, Content: label, ControlType: controlType, Comments: '', UserId: this.UserID, RowStatus: 'A' });
        //        }
        //    }
        //    this.dynamicJsonFields.find(tt => tt.name == name)['errormsg'] = false;
        //}

        const requiredValues = this.dynamicJsonFields.filter(tt => tt.required == true);
        for (let i = 0; i < requiredValues.length; i++) {
            if (requiredValues[i].type == 'textarea') {
                if (event.value[requiredValues[i].name] == undefined || event.value[requiredValues[i].name] == null || event.value[requiredValues[i].name] == '') {
                    this.isCompleted = false;
                    return;
                }
            }
            if (requiredValues[i].type == 'checkbox') {
                let chkValidation = false;
                for (let k = 0; k < requiredValues[i].templateControlOptions.length; k++) {
                    if (event.value[requiredValues[i].name][requiredValues[i].templateControlOptions[k].key]) {
                        chkValidation = true;
                    }
                }
                if (!chkValidation) {
                    this.isCompleted = false;
                    return;

                }

            }
        }
        this.isCompleted = true;
    }



    locationSearch(val: any) {

        this.locations = [{ locationId: 1, name: '121 S Orange Ave, Orlando, FL 32801, USA' }, {
            locationId: 21, name: '1511 E State Road 434, Ste. 3033, Winter Springs, FL 32708'
        }];

        this.locations = this.locations.filter(tt => tt.name.includes(val));
        this.location = val;
        if (val == '') {
            this.locations = [];
        }

    }
    reset(stepper: MatStepper) {
        stepper.reset();

        this.locations = [];
        this.locationchk = false;
        this.reportExist = false;
        this.fallowbtn = false;
        this.reqTrsctnId = 0;
        this.discription = ''
        this.garbagecheck = false;
        this.yardwaste = false;
        this.recycle = false;
        this.urls = [];
        this.trackData = [];
        this.requestTransaction = {};
        // this.previewUrl = '';
        this.browseref.nativeElement.value = "";
        this.submitself = 'Submit as yourself';
        this.submitAnony = 'Submit Anonymously';
        this.btnName = 'Continue';
        this.submitstepperhead = 'Submit';
        this.tracktepperhead = 'Submit';
        this.isRegButton = false;
        if (this.isHideTab) {
            this.register = true;
        } else {
            this.register = false;
        }

        this.isSub = false;
        this.isReg = false;
        this.loginsuc = false;
        this.isForgot = false;
        this.isSignIn = false;


    }
    optionSelected(val: any) {
        this.location = val;
        if (this.location != '121 S Orange Ave, Orlando, FL 32801, USA') {
            this.locationchk = true;
            this.locations = [];
        } else {
            this.locationchk = false;
        }

    }



    resolved(captchaResponse: string, res) {
        this.isReq = true;

    }
    update() {
        this.location = '';
        this.locationchk = false;
        this.locations = [];
    }
    confirmLoaction(locationObj: any, stepper: MatStepper) {
        this.SelectedlocationObj = locationObj;
        if (locationObj !== null && locationObj !== undefined) {
            this.locationObj[0].Latitude = locationObj.latitude;
            this.locationObj[0].Longitude = locationObj.longitude;


            this.requestTransactionService.GetSimilarRequests(locationObj.latitude, locationObj.longitude, this.requestTemplateId).subscribe((result) => {
                
                if (result != null && result != undefined && result.length > 0) {
                    this.similarRequestsList = result;
                    this.reportExist = true;
                    this.getNextTab(stepper);
                } else {
                    this.reportExist = false;
                    this.getNextTab(stepper);
                }
            })
        }

    }

    getNextTab(stepper: MatStepper) {
        this.fallowbtn = false;
        this.reqTrsctnId = 0;
        this.discription = ''
        this.garbagecheck = false;
        this.yardwaste = false;
        this.recycle = false;
        this.urls = [];
        //this.previewUrl = '';
        if (this.browseref != null && this.browseref != undefined) {
            this.browseref.nativeElement.value = "";
        }
        this.submitself = 'Submit as yourself';
        this.submitAnony = 'Submit Anonymously';
        this.btnName = 'Continue';
        this.submitstepperhead = 'Submit';
        this.tracktepperhead = 'Submit';
        this.isRegButton = false;
        if (this.isHideTab) {
            this.register = true;
        } else {
            this.register = false;
        }

        this.isSub = false;
        this.isReg = false;
        this.loginsuc = false;
        this.isForgot = false;
        this.isSignIn = false;
        this.stepper.selected.completed = true;
        stepper.next();
    }
    uploadClick() {

        this.browseref.nativeElement.value = "";
    }
    duplicateContinue(stepper: MatStepper) {
        this.fallowbtn = false;
        this.reqTrsctnId = 0;
        this.discription = ''
        this.garbagecheck = false;
        this.yardwaste = false;
        this.recycle = false;
        this.urls = [];
        this.trackData = [];
        //this.previewUrl = '';
        if (this.browseref !== null && this.browseref !== undefined) {
            this.browseref.nativeElement.value = "";
        }
        this.submitself = 'Submit as yourself';
        this.submitAnony = 'Submit Anonymously';
        this.btnName = 'Continue';
        this.submitstepperhead = 'Submit';
        this.tracktepperhead = 'Submit';
        this.reportExist = false;
        this.submitcomponent.render(this.tracktepperhead);
        try {
            setTimeout(() => {
                this.scrollLast.nativeElement.scrollTop = 0;
            }, 0);

        } catch (err) { }

    }
    follow(stepper: MatStepper, event) {
        if (event.controls.length != 0) {
            this.trackData = event.controls;
            this.trackData.sort(function (a, b) {
                return a.seqNo - b.seqNo;
            });
        }
        this.urls = [];
        if (event.requestPhotos.length != 0) {
            const reqPhotos = event.requestPhotos;
            for (let i = 0; i < reqPhotos.length; i++) {
                this.urls.push(reqPhotos[i].imageUrl);
            }
        }
        this.reqTrsctnId = event.requestTransactionId;
        this.discription = 'This text is added in description box.'
        this.garbagecheck = true;
        this.yardwaste = true;
        this.recycle = true;
        this.submitself = 'Track as yourself';
        this.btnName = 'Track';
        this.submitAnony = 'Track Anonymously';
        this.fallowbtn = true;
        //  this.previewUrl = './assets/images/dsc_0429.jpg';
        this.submitstepperhead = 'Tracking';
        this.tracktepperhead = 'Track';
        this.submitcomponent.render(this.tracktepperhead);
        stepper.next();
    }

    addClick(imgList) {

        this.requestPhotos = [];
        let imageUrls = [];
        for (let i = 0; i < imgList.length; i++) {
            this.requestPhotos.push({ RequestPhotoId: 0, RequestTransactionId: 0, RequestTemplateId: this.requestTemplateId, ImageName: imgList[i].ImageName, ImageType: imgList[i].ImageName.split('.')[1], ImageUrl: imgList[i].ImageUrl.split(',')[1], UserId: this.UserID, RowStatus: 'A' })
            imageUrls.push(imgList[i].ImageUrl)
        }
        this.urls = imageUrls;
    }
    removeImage(imgList) {
        this.requestPhotos = [];
        let imageUrls = [];
        for (let i = 0; i < imgList.length; i++) {
            this.requestPhotos.push({ RequestPhotoId: 0, RequestTransactionId: 0, RequestTemplateId: this.requestTemplateId, ImageName: imgList[i].ImageName, ImageType: imgList[i].ImageName.split('.')[1], ImageUrl: imgList[i].ImageUrl.split(',')[1], UserId: this.UserID, RowStatus: 'A' })
            imageUrls.push(imgList[i].ImageUrl)
        }
        this.urls = imageUrls;
    }
    //removeImage() {
    //    this.browseref.nativeElement.value = "";
    //    this.previewUrl = '';
    //}
    setradio(val: any) {
        this.isloggedIn = localStorage.getItem('login');
        this.submitIdentity = val.lookupDetailsId;
        if (this.isloggedIn != null && this.isloggedIn != undefined) {
            this.isloggedIn = JSON.parse(this.isloggedIn);
            this.isHideTab = false;
            this.register = false;
            if (!this.fallowbtn) {
                if (val.lookupDetailsDescription == 'submit') {
                    this.UserID = null;
                    this.IsEmailSend = false;
                    this.btnName = 'Submit';
                } else {
                    this.IsEmailSend = true;
                    this.btnName = 'Continue';
                }
            }
        } else {
            if (this.fallowbtn) {
                if (val.lookupDetailsDescription == 'submit') {
                    this.UserID = null;
                    this.IsEmailSend = false;
                    this.register = false;
                } else {
                    this.IsEmailSend = true;
                    this.register = true;
                }
            } else {
                if (val.lookupDetailsDescription == 'submit') {
                    this.UserID = null;
                    this.IsEmailSend = false;
                    this.register = false;
                    this.btnName = 'Submit';
                } else {
                    this.IsEmailSend = true;
                    this.register = true;
                    this.btnName = 'Continue';
                }
            }
        }


    }
    regSubmit() {
        this.isReg = true;
        this.isSignIn = false;
        this.isSignButton = true;
        this.isRegButton = true;
    }
    signSubmit() {
        this.isSignIn = true;
        this.isReg = false;
        this.isSignButton = true;
        this.isRegButton = true;
    }
    back() {
        this.isSignIn = false;
        this.isReg = false;
        this.isRegButton = false;
        this.isSignButton = false;
    }
    forgotClick() {
        this.isForgot = true;
        this.isSignIn = false;
    }
    email: any = "joe@mail.com";
    pwd: any = "test@123";
    emailChange(event: any) {
        this.email = event;
    }
    pwdChange(event: any) {
        this.pwd = event;

    }
    backLoginClick() {
        this.isSignIn = true;
        this.isForgot = false;
    }
    loginsuc: boolean;
    submit(event, stepper: MatStepper) {
        const encodepss = this._base64.encode(event.Password);
        this._authenticationService._login(event.email, encodepss).subscribe((data) => {
            localStorage.setItem('login', JSON.stringify(data));
            this.UserID = data.userId;
            this.sharedService.changeMessage('true');
            if (data != null) {
                this.snackBar.open('login successfully', '', {
                    duration: 3000,
                    verticalPosition: 'top',
                    horizontalPosition: 'end',
                    panelClass: ['green-snackbar']
                })
                this.loginsuc = true;
                this.isSubReq = true;
                setTimeout(() => {
                    stepper.next();
                }, 0)
                setTimeout(() => {
                    this.register = false;
                    this.isHideTab = false;

                }, 0)

            } else {
                //this.snackBar.open('please enter valid email id and password', '', {
                //    duration: 3000,
                //    verticalPosition: 'top',
                //    horizontalPosition: 'end',
                //    panelClass: ['red-snackbar']

                //});
                this.showError(this.appConstants.COMM_ALERT_MAIL_PWD);
            }
        })

        //this.loginsuc = true;
        //this.isSubReq = true;
        //setTimeout(() => {
        //    stepper.next();
        //}, 0)
        //setTimeout(() => {
        //    this.register = false;
        //    this.isHideTab = false;

        //}, 0)

        //this.sharedService.changeMessage('true');
        //let userobject: any = {};
        //userobject.userName = 'joecitizen@gmail.com';
        //userobject.Emailid = 'Citizen#1234';
        //localStorage.setItem('login', JSON.stringify(userobject));

    }
    // alert message
    showError(error: string): void {
        this.dialog.open(AlertComponent, {
            disableClose: true,
            data: { errorMsg: error },
            width: '400px',
            height: 'auto',
            panelClass: 'alerrtpop-center'
        });
    }
    registerAcc(event, stepper: MatStepper) {
        this.email = event.emailAddress;
        let userEmail: any = { EmailAddress: this.email };
        this._authenticationService.isMailExist(userEmail).subscribe((value) => {

            if (value) {
                //this.snackBar.open('mail id already Exists', '', {
                //    duration: 3000,
                //    verticalPosition: 'top',
                //    horizontalPosition: 'end',
                //    panelClass: ['red-snackbar']
                //});
                this.showError(this.appConstants.COMM_ALERT_MAIL_EXIST);

                return;
            } else {
                const encodepss = this._base64.encode(event.Password);
                let accessFailedCount: number = 0;
                event.AccessFailedCount = accessFailedCount;
                event.Password = encodepss;
                this._authenticationService.saveDetails(event).subscribe((data) => {
                    localStorage.setItem('login', JSON.stringify(data));
                    this.UserID = data.userId;
                    this.sharedService.changeMessage('true');
                    this.snackBar.open('user saved successfully', '', {
                        duration: 3000,
                        verticalPosition: 'top',
                        horizontalPosition: 'end',
                        panelClass: ['green-snackbar']

                    });
                    this.loginsuc = true;
                    this.isSubReq = true;
                    setTimeout(() => {
                        stepper.next();
                    }, 0)
                    setTimeout(() => {
                        this.register = false;
                        this.isHideTab = false;

                    }, 0)


                })
            }

        })



        //let userobject: any = {};
        //userobject.userName = 'joecitizen@gmail.com';
        //userobject.Emailid = 'Citizen#1234';



    }
    submitReq() {
        this.isSubReq = false;
        this.isSub = true;
        this.submitForm();
    }

    frgtBack() {
        this.isSignIn = true;
        this.isForgot = false;
    }

    submitForm() {
        

        if (this.fallowbtn) {
            let requestTransactionHistory = [];
            requestTransactionHistory.push({ RequestTransactionHistoryId: 0, RequestTransactionId: this.reqTrsctnId, RequestStatus: this.OpenRequestStatus, Comment: '', UserId: this.UserID, RowStatus: 'A' });
            let requestTransactionTrack = [];
            requestTransactionTrack.push({ RequestTransactionTrackId: 0, RequestTransactionId: this.reqTrsctnId, RequestTemplateId: this.requestTemplateId, UserId: this.UserID, SubmitIdentity: this.submitIdentity, IsTrack: true, RowStatus: 'A' });
            this.requestTransaction = {
                RequestTransactionId: this.reqTrsctnId,
                UserId: this.UserID,
                IsEmailSend: this.IsEmailSend,
                RequestTemplateId: this.requestTemplateId,
                requestTransactionHistory: requestTransactionHistory,
                requestTransactionTracks: requestTransactionTrack
            };
            this.requestTransactionService.TrackRequestTransaction(this.requestTransaction).subscribe((data) => {
            })
        } else {
            let requestTransactionHistory = [];
            requestTransactionHistory.push({ RequestTransactionHistoryId: 0, RequestTransactionId: 0, RequestStatus: this.OpenRequestStatus, Comment: '', UserId: this.UserID, RowStatus: 'A' });
            let requestTransactionTrack = [];
            requestTransactionTrack.push({ RequestTransactionTrackId: 0, RequestTransactionId: 0, RequestTemplateId: this.requestTemplateId, UserId: this.UserID, SubmitIdentity: this.submitIdentity, IsTrack: false, RowStatus: 'A' });
            if (this.requestTransactionDetails != null && this.requestTransactionDetails != undefined && this.requestTransactionDetails.length > 0) {
                for (var i = 0; i < this.requestTransactionDetails.length; i++) {
                    this.requestTransactionDetails[i].UserId = this.UserID;
                }
            }
            this.requestTransaction = {
                RequestTransactionId: 0, RequestTemplateId: this.requestTemplateId, RequestStatus: this.OpenRequestStatus, UserId: this.UserID, RowStatus: 'A', IsEmailSend: this.IsEmailSend,

                requestLocation: this.locationObj,
                requestPhotos: this.requestPhotos,
                requestTransactionDetails: this.requestTransactionDetails,
                requestTransactionHistory: requestTransactionHistory,
                requestTransactionTracks: requestTransactionTrack
            };

            this.requestTransactionService.SubmitOrTrackRequest(this.requestTransaction).subscribe((data) => {
            })


        }

    }
    backTOPrevious() {
        this.trackData = [];
        this.urls = [];
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Finish the horizontal stepper
     */
    finishHorizontalStepper(): void {
        // alert('You have finished the horizontal stepper!');
        this.showError(this.appConstants.COMM_ALERT_HORIZANTAL);
    }

    /**
     * Finish the vertical stepper
     */
    finishVerticalStepper(): void {
        // alert('You have finished the vertical stepper!');
        this.showError(this.appConstants.COMM_ALERT_HORIZANTAL);
    }
    //previewUrl: any = '';
    //fileData: any;
    //fileProgress(fileInput: any) {
    //    this.fileData = <File>fileInput.target.files[0];
    //    this.preview();

    //}

    //preview() {
    //    // Show preview 
    //    var mimeType = this.fileData.type;
    //    if (mimeType.match(/image\/*/) == null) {

    //        return;
    //    }
    //    var reader = new FileReader();
    //    reader.readAsDataURL(this.fileData);
    //    reader.onload = (_event) => {
    //        this.previewUrl = reader.result;
    //        //this.UserImg = reader.result.toString().split(',')[1];
    //    }
    //}
}
export const confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    if (!control.parent || !control) {
        return null;
    }

    const password = control.parent.get('password');
    const passwordConfirm = control.parent.get('passwordConfirm');

    if (!password || !passwordConfirm) {
        return null;
    }

    if (passwordConfirm.value === '') {
        return null;
    }

    if (password.value === passwordConfirm.value) {
        return null;
    }

    return { passwordsNotMatching: true };
};