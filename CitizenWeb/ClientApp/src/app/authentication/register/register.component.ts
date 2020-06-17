import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { Router } from '@angular/router';
import { SharedServiceService } from 'app/services/shared-service.service';
import { AuthenticationService } from 'app/authentication/authentication.service';
import { Base64Service } from '../base64.service';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { MatDialog } from '@angular/material/dialog';
import { AppConstants } from '../../app.constants';
import { PasswordValidation } from '../../authentication/password-validator/passwordvalidate';
@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class RegisterComponent implements OnInit, OnDestroy {

    registerForm: FormGroup;
    currentUser: any;
    userList: any = [];
    email: any;
    userEmail: any;
    isDisabled = false;
    value: any;
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _sharedService: SharedServiceService,
        private _authenticationService: AuthenticationService,
        private _base64: Base64Service,
        public snackBar: MatSnackBar,
        public dialog: MatDialog,
        private appConstants: AppConstants

    ) {
        // Configure the layout
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

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }
    get f() { return this.registerForm.controls; }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        //this._authenticationService.GetAllUsers().subscribe((data) => {
        //    this.userList = data;
        //    //this.email = this.userList.filter(tt => tt.emailAddress);

        //})
        this.registerForm = this._formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            userName: ['', Validators.required],
            emailAddress: ['', [Validators.required, Validators.email]],
            Password: ['', Validators.required],
            ConfirmPassword: ['', Validators.required],
            phoneNumber: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
        },
            {
                validator: PasswordValidation.MatchPassword
            });

        //Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        //this.registerForm.valueChanges
        //    .subscribe((changedObj: any) => {
        //        this.isDisabled = this.registerForm.valid;
        //    });


        // Update the validity of the 'passwordConfirm' field
        // when the 'password' field changes
        //this.registerForm.get('password').valueChanges
        //    .pipe(takeUntil(this._unsubscribeAll))
        //    .subscribe(() => {
        //        this.registerForm.get('passwordConfirm').updateValueAndValidity();
        //    });
        // this.registerForm.get("nearestHome").setValue("121 S Orange Ave, Orlando, FL 32801, USA");
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

    register() {
        this.email = this.registerForm.value.emailAddress;
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
                const encodepss = this._base64.encode(this.f.Password.value);
                let accessFailedCount: number = 0;
                this.registerForm.value.AccessFailedCount = accessFailedCount;
                this.registerForm.value.Password = encodepss;
                let userId = 0;
                this.registerForm.value.userId = userId;
                this._authenticationService.saveDetails(this.registerForm.value).subscribe((data) => {
                    localStorage.setItem('login', JSON.stringify(data));
                    this.snackBar.open('user saved successfully', '', {
                        duration: 3000,
                        verticalPosition: 'top',
                        horizontalPosition: 'end',
                        panelClass: ['green-snackbar']

                    });

                    this._sharedService.changeMessage('true');
                    this._router.navigate(['/apps/user/myrequests']);
                })

            }
        })





    }

    passwordChange(val: any, cnfrmval: any) {
        if (val._pendingValue.trim() == "") {
            this.registerForm.controls.Password.setErrors({ required: true })
        }
        if (val._pendingValue == '') {
            this.registerForm.controls.ConfirmPassword.setValue('');
        }

    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

/**
 * Confirm password validator
 *
 * @param {AbstractControl} control
 * @returns {ValidationErrors | null}
 */
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
