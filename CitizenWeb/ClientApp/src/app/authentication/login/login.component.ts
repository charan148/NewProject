import { Component, OnInit, ViewEncapsulation,ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { SharedServiceService } from 'app/services/shared-service.service';
import { AuthenticationService } from './../authentication.service';
import { Base64Service } from '../base64.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { MatDialog } from '@angular/material/dialog';
import { AppConstants } from '../../app.constants';
import { FuseNavigationComponent } from '../../../@fuse/components/navigation/navigation.component';
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: fuseAnimations,
    providers: [
        FuseNavigationComponent,
    ]
})
export class LoginComponent implements OnInit {

    loading = false;
    roleNames: string = '';
    roleObj: any = [];
    loginobj: any = {};
    loginForm: FormGroup;
    domain = '';

    roleIds = 0;
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _sharedService: SharedServiceService,
        private _authenticationService: AuthenticationService,
        private _base64: Base64Service,
        public snackBar: MatSnackBar,
        private appConstants: AppConstants,
        public dialog: MatDialog,
        private fuseNavigationComponent: FuseNavigationComponent,

        //private toolbarComp: ToolbarComponent
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
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.loginForm = this._formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', Validators.required]
        });
    } 
    email: any = 'joecitizen@mail.com';
    pwd: any = 'test@123';
    emailChange(event: any) {
        this.email = event;
    }
    pwdChange(event: any) {
        this.pwd = event;

    }
    get f() { return this.loginForm.controls; }

    submit() {

        //let userobject: any = {};
        //userobject.userName = 'joecitizen@gmail.com';
        //userobject.Emailid = 'Citizen#1234';
        //localStorage.setItem('login', JSON.stringify(userobject));

        //this._sharedService.changeMessage('true');

        //this._router.navigate(['/citizen/requests']);

        const encodepss = this._base64.encode(this.f.password.value);
        this._authenticationService._login(this.f.email.value, encodepss).subscribe((data) => {
            const myObj: {} = data;
            this.loginobj = data;
            localStorage.setItem('login', JSON.stringify(data));
            this._authenticationService._getUserRoleById(this.loginobj.userId).subscribe(dataRole => {
                if (dataRole != null && dataRole.length > 0) {
                    this.roleObj = dataRole;
                    localStorage.setItem('roleNameOfLoginUser', JSON.stringify(this.roleObj));
                    for (let i = 0; i < this.roleObj.length; i++) {
                        //this.roleNames = this.roleNames + ',' + this.roleObj[i].roleId;
                        this.roleIds = this.roleObj[i].roleId;
                    }
                    this._authenticationService._rolePriviligeSelect(this.roleIds).subscribe(data => {
                        let dataBind: any = [];
                        const perm: any = [];
                        dataBind = data;
                        for (let i = 0; i < dataBind.length; i++) {
                            perm.push(data[i]);
                        }
                        localStorage.setItem('sessionPermissions', JSON.stringify(perm));
                        this.fuseNavigationComponent.ngOnInit();
                        this._router.navigate(['/citizen/requests']);
                    });
                }
                else {
                    this.loading = false;
                    this.clearLocalStorage();
                    // this.showError(this.appConstants.LOGIN_SUCCESS_NO_ROLES);
                }

            });
            if (data != null) {
                this.snackBar.open('login successfully', '', {
                    duration: 3000,
                    verticalPosition: 'top',
                    horizontalPosition: 'end',
                    panelClass: ['green-snackbar']

                });
                this._sharedService.changeMessage('true');
                this._router.navigate(['/citizen/requests']);
            } else{
	    //this.snackBar.open('please enter valid email id and password', '', {
                //    duration: 3000,
                //    verticalPosition: 'top',
                //    horizontalPosition: 'end',
                //    panelClass: ['red-snackbar']

                //});
                this.showError(this.appConstants.COMM_ALERT_MAIL_PWD);
        	}
        })


        // }

        //this._authenticationService._getUserBasicDetails("lee", "EPIC").subscribe(
        //    (data) => {
        //        this._router.navigate(['/citizen/requests']);
        //    });
    }
    clearLocalStorage() {
        localStorage.removeItem('sessionPermissions');
        localStorage.removeItem('roleNameOfLoginUser');
        localStorage.removeItem('login');
        //this._authenticationService.logout();
    }
     showError(error: string): void {
        this.dialog.open(AlertComponent, {
            disableClose: true,
            data: { errorMsg: error },
            width: '400px',
            height: 'auto',
            panelClass: 'alerrtpop-center'
        });
    }
    forgotClick() {
        this._router.navigate(['/authentication/forgot-password']);
    }
    
}
