import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AdministrationService } from '../administration.service';
import { AppConstants } from '../../app.constants';
import { ConfirmationDailogService } from '../../custom_alerts/confirmation-dailog.service';

@Component({
    selector: 'app-rolesdetails',
    templateUrl: './rolesdetails.component.html',
    styleUrls: ['./rolesdetails.component.scss']
})
export class RolesdetailsComponent implements OnInit {

    Roleobj: any = {};
    RoleID: any;
    disable = false;
    isloggedIn: any = [];

    constructor(
        private router: Router,
        private administrationservice: AdministrationService,
        public snackBar: MatSnackBar,
        private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants
    ) { }

    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        //if (sessionStorage.getItem('RolePageAction') != null &&
        //    sessionStorage.getItem('RolePageAction') !== undefined && sessionStorage.getItem('RolePageAction') !== 'null' &&
        //    sessionStorage.getItem('RolePageAction') !== 'undefined') {
        //    const obj = JSON.parse(sessionStorage.getItem('RolePageAction'));
        this.administrationservice.detailsObj.subscribe((obj) => {
            let Obj: any = obj;
            if (Obj) {
                if (obj.toString().indexOf(';') > -1) {
                    const varobjSplitsemi = obj.split(';');
                    if (varobjSplitsemi.length > 0) {
                        this.RoleID = JSON.parse(varobjSplitsemi[0]);
                        if (this.RoleID > 0) {
                            if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'View') {
                                this.disable = true;
                                this.getRoleById(this.RoleID);
                            } else if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'Edit') {
                                this.disable = false;
                                this.getRoleById(this.RoleID);
                            }
                        } else {
                            this.Roleobj = {};
                            this.RoleID = 0;
                        }
                    }
                }
            }
        })
    }

    // Get Role By ID
    getRoleById(id): void {
        this.administrationservice.GetRolesById(id).subscribe(data => {
            this.Roleobj = data;
        });
    }

    // Save & Update Role
    saveRole(): void {
        if (this.RoleID > 0) {
            if (this.Roleobj.roleName !== '' && this.Roleobj.roleName !== undefined && this.Roleobj.roleName != null) {
                const Roleobj = {
                    RoleName: this.Roleobj.roleName,
                    IsActive: this.Roleobj.isActive,
                    UpdateUserId: this.isloggedIn.userId,
                    RoleId: this.RoleID
                };
                this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_UPDATE_Alert_Header,
                    this.appConstants.COMM_ALERT_UPDATE_RECORD)
                    .then((ConfirmTrue) => {
                        this.administrationservice.UpdateRole(Roleobj).subscribe(data => {
                            const res = data;
                            if (res === true) {
                                this.snackBar.open(this.appConstants.COMM_ROLE_UPDATED, '', {
                                    duration: 2000,
                                    verticalPosition: 'top',
                                    horizontalPosition: 'end',
                                    panelClass: ['green-snackbar']
                                });
                                this.router.navigate(['administration/roles']);
                            }
                        });
                    }, (ConfirmFalse) => { })
                    .catch((ConfirmFalse) => { });
            } else {
                this.snackBar.open(this.appConstants.COMM_ROLE_ENTER, '', {
                    duration: 2000,
                    verticalPosition: 'top',
                    horizontalPosition: 'end',
                    panelClass: ['red-snackbar']
                });
            }
        } else {
            if (this.Roleobj.roleName !== '' && this.Roleobj.roleName !== undefined && this.Roleobj.roleName != null) {
                const obj = {
                    RoleName: this.Roleobj.roleName,
                    IsActive: this.Roleobj.isActive,
                    CreateUserId: this.isloggedIn
                        .userId,
                    RoleId: 0
                };
                // GetRoleExistsByRoleName

                this.administrationservice.GetRoleExistsByRoleName(this.Roleobj.roleName).subscribe(
                    data => {
                        if (data != false) {
                            this.snackBar.open(this.appConstants.COMM_ROLE_EXISTS, '', {
                                duration: 2000,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['red-snackbar']

                            });
                        } else {
                            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_SAVE_Alert_Header,
                                this.appConstants.COMM_ALERT_SAVE_RECORD)
                                .then((ConfirmTrue) => {
                                    this.administrationservice.SaveRole(obj).subscribe(data => {
                                        const roleid = data;
                                        if (roleid > 0) {
                                            this.snackBar.open(this.appConstants.COMM_ROLE_CREATE, '', {
                                                duration: 2000,
                                                verticalPosition: 'top',
                                                horizontalPosition: 'end',
                                                panelClass: ['green-snackbar']
                                            });
                                            this.router.navigate(['administration/roles']);
                                        }
                                    });
                                }, (ConfirmFalse) => { })
                                .catch((ConfirmFalse) => { });
                        }
                    });
            } else {
                this.snackBar.open(this.appConstants.COMM_ROLE_ENTER, '', {
                    duration: 2000,
                    verticalPosition: 'top',
                    horizontalPosition: 'end',
                    panelClass: ['red-snackbar']
                });
            }

        }
    }

    // Refresh Page
    refresh(): void {
        if (this.RoleID > 0) {
            this.getRoleById(this.RoleID);
        } else {
            this.Roleobj = {};
            this.RoleID = 0;
        }

    }

    // Close Page
    close(): void {
        this.router.navigate(['administration/roles']);
    }

    roleNameChange(val: any) {
        if (val.trim() == "") {
            this.Roleobj.roleName = val.trim();
        }
    }
}
