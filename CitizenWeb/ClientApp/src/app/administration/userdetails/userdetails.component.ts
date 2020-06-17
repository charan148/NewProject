import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { AdministrationService } from '../administration.service';
import { Router } from '@angular/router';
import { MatAccordion } from '@angular/material/expansion';
import { FormControl,Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Base64Service } from '../../authentication/base64.service';
import { AppConstants } from 'app/app.constants';
import { ConfirmationDailogService } from '../../custom_alerts/confirmation-dailog.service';
import { PasswordValidation } from '../../authentication/password-validator/passwordvalidate';
@Component({
    selector: 'app-userdetails',
    templateUrl: './userdetails.component.html',
    styleUrls: ['./userdetails.component.scss']
})
export class UserdetailsComponent implements OnInit {
    userActive: boolean = false;
    userAutoSave: boolean = false;
    userForm: FormGroup;
    userobj: any = {};
    EmailAddress: any;
    phoneNumber: any;
    userid: any;
    UserName: any;
    userID: any;
    disable = false;
    isReadOnly = false;
    submitted = false;
    isloggedIn: any = [];
    previewUrl: any;
    Password: any;
    phoneFormControl = new FormControl('', [
        //Validators.required,
        Validators.pattern(/(\(?[0-9]{3}\)?-?\s?[0-9]{3}-?[0-9]{4})/)
    ]);
    emailFormControl = new FormControl('', [
        Validators.required,
        Validators.pattern('^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$')
    ]);
    public mask = ['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/];
    form;
    @ViewChild(MatAccordion, { static: true }) accordion: MatAccordion;
    constructor(private router: Router,
        public snackBar: MatSnackBar,
        public dialog: MatDialog,
        private administrationservice: AdministrationService,
        private formBuilder: FormBuilder,
        private base64: Base64Service,
        private appConstants: AppConstants,
        private confirmationDialogService: ConfirmationDailogService,
    ) {
        this.userForm = this.formBuilder.group({
            FirstName: ['', Validators.required],
            LastName: ['', Validators.required],
            UserName: ['', Validators.required],
            Password: ['', Validators.required],
            ConfirmPassword: ['', Validators.required],
            EmailAddress: this.emailFormControl,
            PhoneNumber: this.phoneFormControl,
        },
            {
                validator: PasswordValidation.MatchPassword
            }
        );
    }

    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.administrationservice.detailsObj.subscribe((obj) => {
            let userObj: any = obj;
            if (userObj) {
                if (userObj.toString().indexOf(';') > -1) {
                    const varobjSplitsemi = obj.split(';');
                    if (varobjSplitsemi.length > 0) {
                        this.userID = JSON.parse(varobjSplitsemi[0]);
                        if (this.userID > 0) {
                            if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'View') {
                                this.disable = true;
                                this.getUserDetailsView(this.userID);
                            } else if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'Edit') {
                                this.disable = false;
                                this.isReadOnly = true;
                                this.getUserDetails(this.userID);
                            }
                        } else {
                            this.userobj = {};
                            this.userID = 0;
                            this.userActive = true;
                        }
                    }

                }
            }
        })
        
        
    }
    get f() { return this.userForm.controls; }
    // fecth user by id
    // user Details
    getUserDetails(id: any): void {

        this.administrationservice.getUserDetails(id).subscribe(result => {
            const res = result;

            this.userForm.setValue({
                FirstName: res[0].firstName, LastName: res[0].lastName, UserName: res[0].userName, Password: res[0].password, ConfirmPassword: res[0].password,
                EmailAddress: res[0].emailAddress, PhoneNumber: res[0].phoneNumber
            });

            if (res[0].userImage != '') {
                this.previewUrl = res[0].userImage
            }
            if (res[0].isAutoSave != false) {
                this.userAutoSave = res[0].isAutoSave
            }

            this.userid = res[0].userID;

        });
    }

    // for user in view
    getUserDetailsView(id: any): void {

        this.administrationservice.getUserDetails(id).subscribe(result => {
            const res = result;
            this.userid = res[0].userId;

            this.userForm = this.formBuilder.group({
                FirstName: [{ value: res[0].firstName, disabled: this.disable }],
                LastName: [{ value: res[0].lastName, disabled: this.disable }],
                UserName: [{ value: res[0].userName, disabled: this.disable }],
                Password: [{ value: res[0].password, disabled: this.disable }],
                ConfirmPassword: [{ value: res[0].password, disabled: this.disable }],
                EmailAddress: [{ value: res[0].emailAddress, disabled: this.disable }],
                PhoneNumber: [{ value: res[0].phoneNumber, disabled: this.disable }],

            });
        });

    }
    // Save New User
    saveUser(): void {
        if (this.userID === 0) {
            this.administrationservice._getUserBasicDetails(this.f.UserName.value).subscribe(
                data => {
                    if (data != null) {
                        this.snackBar.open(this.appConstants.COMM_USER_NAME_EXISTS, '', {
                            duration: 2000,
                            verticalPosition: 'top',
                            horizontalPosition: 'end',
                            panelClass: ['red-snackbar']

                        });
                    } else {
                        this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_SAVE_Alert_Header,
                            this.appConstants.COMM_ALERT_SAVE_RECORD)
                            .then((ConfirmTrue) => {

                                const user = this.userForm.value;
                                const encodepss = this.base64.encode(user.Password);
                                user.Password = encodepss;
                                user.CreateUserId = this.isloggedIn.userId;
                                user.PhoneNumber = this.f.PhoneNumber.value.replace(/\D+/g, '');
                                this.administrationservice.saveDetails(user).subscribe(result => {
                                    const res = result;
                                    if (res) {
                                        this.snackBar.open(this.appConstants.COMM_USER_CREATE, '', {
                                            duration: 2000,
                                            verticalPosition: 'top',
                                            horizontalPosition: 'end',
                                            panelClass: ['green-snackbar']

                                        });
                                        this.userForm.reset();
                                        //  this.router.navigate(['./administration/security']);
                                    }
                                });
                            }, (ConfirmFalse) => { })
                            .catch((ConfirmFalse) => { });
                    }
                });
        } else {
            this.updateDetail();
        }

    }

    // update users data
    updateDetail(): void {
        this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_UPDATE_Alert_Header, this.appConstants.COMM_ALERT_UPDATE_RECORD)
            .then((ConfirmTrue) => {
                const user = this.userForm.value;
                const encodepss = this.base64.encode(user.Password);
                user.Password = encodepss;
                user.CreateUserId = 1;
                user.UserId = this.userID;
                user.PhoneNumber = this.f.PhoneNumber.value.replace(/\D+/g, '');
                user.UpdateUserId = this.isloggedIn.userId;
                user.UserImage = this.previewUrl;
                this.administrationservice.updateDetail(user).subscribe(result => {
                    const res = result;
                    if (res) {
                        this.snackBar.open(this.appConstants.COMM_USER_UPDATED, '', {
                            duration: 2000,
                            verticalPosition: 'top',
                            horizontalPosition: 'end',
                            panelClass: ['green-snackbar']

                        });
                        this.userForm.reset();
                        this.router.navigate(['./administration/users']);
                    }
                });
            }, (ConfirmFalse) => { })
            .catch((ConfirmFalse) => { });
    }

    firstNameChange(val: any) {
        if (val._pendingValue.trim() == "") {
            this.userForm.controls.FirstName.setErrors({ required: true })
        }
    }
    lastNameChange(val: any) {
        if (val._pendingValue.trim() == "") {
            this.userForm.controls.LastName.setErrors({ required: true })
        }
    }
    userNameChange(val: any) {
        if (val._pendingValue.trim() == "") {
            this.userForm.controls.UserName.setErrors({ required: true })
        }
    }
    passwordChange(val: any, cnfrmval: any) {
        if (val._pendingValue.trim() == "") {
            this.userForm.controls.Password.setErrors({ required: true })
        }
        if (val._pendingValue == '') {
            this.userForm.controls.ConfirmPassword.setValue('');
        }

    }

    // refresh form
    refresh(): void {
        if (this.userID > 0) {
            this.getUserDetails(this.userID);
        } else {
            this.userForm.reset();
        }
    }

    // Close Page
    closeUser(): void {
        this.router.navigate(['./administration/users']);
    }

}
