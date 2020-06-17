import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { AdministrationService } from '../administration.service';
import { Router } from '@angular/router';
import { LookupdetailsComponent } from '../lookupdetails/lookupdetails.component';
import { MatDialog, MatDialogRef, MatDialogClose, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppConstants } from 'app/app.constants';
import { ConfirmationDailogService } from 'app/custom_alerts/confirmation-dailog.service';

@Component({
    selector: 'app-lookupdatailspopup',
    templateUrl: './lookupdatailspopup.component.html',
    styleUrls: ['./lookupdatailspopup.component.scss']

})
export class LookupdatailspopupComponent implements OnInit {

    lookdetailsid: any;
    LookupDetailsobj: any = {};
    disablepopup = false;
    LookupID: any;
    UpdateUserId: any;
    CreateUserId: any;
    endmaxDate: any;
    startminDate: any;
    currentUser: any = [];
    StatusList: any = [
        { id: 1, value: 'Active' },
        { id: 2, value: 'In Active' }
    ];

    constructor(
        private router: Router,
        private dialog: MatDialogRef<LookupdatailspopupComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants,
        private administrationservice: AdministrationService,
        public snackBar: MatSnackBar,
    ) { }



    ngOnInit(): void {
        this.currentUser = JSON.parse(localStorage.getItem('login')); 
        //    const obj = this.administrationservice.getList();
        //if (obj !== null && obj !== undefined &&
        //    obj !== 'null' && obj !== 'undefined') {
        //    if (obj.toString().indexOf(';') > -1) {
        //        const varobjSplitsemi = obj.split(';');
        //        if (varobjSplitsemi.length > 0) {
        //            this.lookdetailsid = JSON.parse(varobjSplitsemi[0]);
        //            if (this.lookdetailsid > 0) {
        //                if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'View') {
        //                    this.disablepopup = true;
        //                    this.getLookupDetailsById(this.lookdetailsid);
        //                } else if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'Edit') {
                            
        //                    this.disablepopup = false;
        //                    this.administrationservice.GetLookupDetailsById(this.lookdetailsid).subscribe(data => {
        //                        this.LookupDetailsobj = data;
        //                        if (this.LookupDetailsobj.rowStatus == 'A') {
        //                            this.LookupDetailsobj.rowStatus = 'Active';
        //                        } else if (this.LookupDetailsobj.rowStatus == 'D') {
        //                            this.LookupDetailsobj.rowStatus = 'In Active';
        //                        }
        //                    });
        //                }
        //            } else {
        //                this.LookupDetailsobj = {};
        //                this.lookdetailsid = 0;
        //                this.LookupDetailsobj.rowStatus = 'Active';
        //            }

        //        }
        //    }
        // }
  

        //if (sessionStorage.getItem('LookupPageAction') !== null && sessionStorage.getItem('LookupPageAction') !== undefined &&
        //    sessionStorage.getItem('LookupPageAction') !== 'null' && sessionStorage.getItem('LookupPageAction') !== 'undefined') {
        //    const obj = JSON.parse(sessionStorage.getItem('LookupPageAction'));
        //    if (obj.toString().indexOf(';') > -1) {
        //        const varobjSplitsemi = obj.split(';');
        //        if (varobjSplitsemi.length > 0) {
        //            this.LookupID = JSON.parse(varobjSplitsemi[0]);
        //            if (this.LookupID > 0) {

        //            } else {
        //                this.LookupID = 0;
        //            }

        //        }
        //    }
        //}
        if (sessionStorage.getItem('LookupDetailsPage') !== null && sessionStorage.getItem('LookupDetailsPage') !== undefined &&
            sessionStorage.getItem('LookupDetailsPage') !== 'null' && sessionStorage.getItem('LookupDetailsPage') !== 'undefined') {
            const obj = JSON.parse(sessionStorage.getItem('LookupDetailsPage'));
            if (obj.toString().indexOf(';') > -1) {
                const varobjSplitsemi = obj.split(';');
                if (varobjSplitsemi.length > 0) {
                    this.lookdetailsid = JSON.parse(varobjSplitsemi[0]);
                    if (this.lookdetailsid > 0) {
                        if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'View') {
                            this.disablepopup = true;
                            this.getLookupDetailsById(this.lookdetailsid);
                        } else if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'Edit') {
                            this.disablepopup = false;
                            this.administrationservice.GetLookupDetailsById(this.lookdetailsid).subscribe(data => {
                                this.LookupDetailsobj = data;
                                if (this.LookupDetailsobj.rowStatus == 'A') {
                                    this.LookupDetailsobj.rowStatus = 'Active';
                                } else if (this.LookupDetailsobj.rowStatus == 'D') {
                                    this.LookupDetailsobj.rowStatus = 'In Active';
                                }
                            });
                        }
                    } else {
                        this.LookupDetailsobj = {};
                        this.lookdetailsid = 0;
                        this.LookupDetailsobj.rowStatus = 'Active';
                    }

                }
            }
        }

        if (sessionStorage.getItem('LookupPageAction') !== null && sessionStorage.getItem('LookupPageAction') !== undefined &&
            sessionStorage.getItem('LookupPageAction') !== 'null' && sessionStorage.getItem('LookupPageAction') !== 'undefined') {
            const obj = JSON.parse(sessionStorage.getItem('LookupPageAction'));
            if (obj.toString().indexOf(';') > -1) {
                const varobjSplitsemi = obj.split(';');
                if (varobjSplitsemi.length > 0) {
                    this.LookupID = JSON.parse(varobjSplitsemi[0]);
                    if (this.LookupID > 0) {

                    } else {
                        this.LookupID = 0;
                    }

                }
            }
        }

    

    }

    getLookupDetailsById(id): void {
        this.administrationservice.GetLookupDetailsById(id).subscribe(data => {
            this.LookupDetailsobj = data;
            if (this.LookupDetailsobj.rowStatus == 'A') {
                this.LookupDetailsobj.rowStatus = 'Active';
            } else if (this.LookupDetailsobj.rowStatus == 'D') {
                this.LookupDetailsobj.rowStatus = 'In Active';
            }
        });
    }
    startDateChange(event): void {
        this.endmaxDate = new Date(event);
    }
    endDateChange(event): void {
        this.startminDate = new Date(event);
    }

    saveLookUpDetails(): void {
        //if (this.LookupDetailsobj.lookupDetailsEffectiveFrom != null) {
        //  let dfrom = new Date(this.LookupDetailsobj.lookupDetailsEffectiveFrom);
        //  const yearfrom = dfrom.getFullYear();
        //  const monthfrom = dfrom.getMonth() + 1;
        //  const dayfrom = dfrom.getDate()
        //  this.LookupDetailsobj.lookupDetailsEffectiveFrom = monthfrom + '/' + dayfrom + '/' + yearfrom;
        //}


        //if (this.LookupDetailsobj.lookupDetailsEffectiveTo != null) {
        //  let dto = new Date(this.LookupDetailsobj.lookupDetailsEffectiveTo);
        //  const yearto = dto.getFullYear();
        //  const monthto = dto.getMonth() + 1;
        //  const dayto = dto.getDate()
        //  this.LookupDetailsobj.lookupDetailsEffectiveTo = monthto + '/' + dayto + '/' + yearto;
        //}
        if (this.lookdetailsid > 0) {
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_UPDATE_Alert_Header, this.appConstants.COMM_ALERT_UPDATE_RECORD)
                .then((ConfirmTrue) => {
                    // this.LookupDetailsobj.UpdateUserId = 2;
                    if (this.LookupDetailsobj.rowStatus == 'Active') {
                        this.LookupDetailsobj.rowStatus = 'A';
                    } else if (this.LookupDetailsobj.rowStatus == 'In Active') {
                        this.LookupDetailsobj.rowStatus = 'D';
                    }
                    this.LookupDetailsobj.updateUserId = this.currentUser.userId;
                    this.administrationservice.SaveLookupDetails(this.LookupDetailsobj).subscribe(data => {
                        const res = data;
                        if (res) {
                            this.snackBar.open(this.appConstants.COMM_LOOKUP_DETAILS_UPDATE, '', {
                                duration: 2000,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['green-snackbar']

                            });
                        }
                        this.dialog.close();


                    });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
        } else {
            this.LookupDetailsobj.lookupDetailsId = 0;
            if (this.LookupDetailsobj.rowStatus == 'Active') {
                this.LookupDetailsobj.rowStatus = 'A';
            } else if (this.LookupDetailsobj.rowStatus == 'In Active') {
                this.LookupDetailsobj.rowStatus = 'D';
            }
            this.LookupDetailsobj.lookupId = this.LookupID;
            // this.LookupDetailsobj.CreateUserId = 2;
            this.LookupDetailsobj.CreateUserId = this.currentUser.userId;

            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_SAVE_Alert_Header, this.appConstants.COMM_ALERT_SAVE_RECORD)
                .then((ConfirmTrue) => {
                    this.administrationservice.SaveLookupDetails(this.LookupDetailsobj).subscribe(data => {
                        const res = data;
                        if (res) {
                            this.snackBar.open(this.appConstants.COMM_LOOKUP_DETAILS_SAVE, '', {
                                duration: 2000,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['green-snackbar']

                            });

                        }
                        this.dialog.close();

                    });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });

        }
    }
}
