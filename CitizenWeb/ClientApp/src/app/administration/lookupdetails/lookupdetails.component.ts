import { Component, OnInit, ViewChild, PACKAGE_ROOT_URL, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';
import { AppConstants } from 'app/app.constants';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog} from '@angular/material/dialog';
import { AdministrationService } from '../administration.service';
import { LookupdatailspopupComponent } from '../lookupdatailspopup/lookupdatailspopup.component';
import { ConfirmationDailogService } from 'app/custom_alerts/confirmation-dailog.service';

export interface LookupDatailsData {
    lookupDetailsValue: any;
    lookupDetailsDescription: any;
}
@Component({
    selector: 'app-lookupdetails',
    templateUrl: './lookupdetails.component.html',
    styleUrls: ['./lookupdetails.component.scss']
})
export class LookupdetailsComponent implements OnInit {
    isloggedIn: any = [];
    Lookupobj: any = {};
    LookupID: any;
    disable = false;
    savebuttondisable = false;
    resetbuttonhide = false;
    disablefields = false;
    recId: any;
    LookupDetailsobj: any = {};
    lookupDescription: any;
    filterName: any;
    viewdisable = false;
    hidefooter = false;
    lookupDetailList: any = [];
    displayedColumns: string[] = ['select', 'lookupDetailsValue', 'lookupDetailsDescription'];
    dataSource: MatTableDataSource<LookupDatailsData>;
    selection = new SelectionModel<LookupDatailsData>(true, []);

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    startminDate: any;
    endmaxDate: any;
    StatusList: any = [
        { id: 1, value: 'Active' },
        { id: 2, value: 'In Active' }
    ];

    constructor(
        private router: Router,
        private administrationservice: AdministrationService,
        public snackBar: MatSnackBar,
        private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants,
        private changeDetectorRefs: ChangeDetectorRef,
        public dialog: MatDialog
    ) { }
    ngAfterViewChecked() {
        // check if it change, tell CD update view
        this.changeDetectorRefs.detectChanges();
    }
    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
       
           // const obj = JSON.parse(sessionStorage.getItem('LookupPageAction'));
        this.administrationservice.detailsObj.subscribe((obj) => {           
            let lookupobj: any = obj;
            if (lookupobj !== null && lookupobj !== undefined &&
                lookupobj !== 'null' && lookupobj !== 'undefined') {
                if (lookupobj.toString().indexOf(';') > -1) {
                    const varobjSplitsemi = obj.split(';');
                    if (varobjSplitsemi.length > 0) {
                        this.LookupID = JSON.parse(varobjSplitsemi[0]);
                        if (this.LookupID > 0) {
                            if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'View') {
                                this.disable = false;
                                this.viewdisable = true;
                                this.hidefooter = true;
                                this.savebuttondisable = true;
                                this.resetbuttonhide = true;
                                this.disablefields = true;
                                this.getLookupById(this.LookupID);
                            } else if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'Edit') {
                                this.disable = false;
                                this.savebuttondisable = false;
                                this.disablefields = false;
                                this.getLookupById(this.LookupID);
                            }
                        } else {
                            this.Lookupobj = {};
                            this.LookupID = 0;
                            this.disable = true;
                            this.viewdisable = true;
                            this.savebuttondisable = false;
                            this.Lookupobj.rowStatus = 'Active';
                            this.disablefields = false;
                        }

                    }
                }
            }
        })
        

    }
    applyFilter(filterValue: string): void {
        this.dataSource.filter = filterValue.trim().toLowerCase();
        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
    startDateChange(event): void {


        this.endmaxDate = new Date(event);


    }
    endDateChange(event): void {
        this.startminDate = new Date(event);
    }
    // Get Role By ID
    getLookupById(id): void {
        this.administrationservice.GetLookupById(id).subscribe(data => {
            this.Lookupobj = data;
            if (this.Lookupobj.rowStatus == 'A') {
                this.Lookupobj.rowStatus = 'Active';
            } else if (this.Lookupobj.rowStatus == 'D') {
                this.Lookupobj.rowStatus = 'In Active';
            }
            const lookups = this.Lookupobj.lookupDetails;
            this.dataSource = new MatTableDataSource(lookups);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
            this.changeDetectorRefs.detectChanges();
            this.selection = new SelectionModel<LookupDatailsData>(true, []);
        });
    }

    getLookupDetailById(id): void {
        this.administrationservice.GetLookupDetailsByLookupId(id).subscribe(data => {
            this.lookupDetailList = data;
            this.dataSource = new MatTableDataSource(this.lookupDetailList);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
            this.selection = new SelectionModel<LookupDatailsData>(true, []);
        });
    }
    // Save & Update
    SaveLookup(): void {
        
        if (this.LookupID > 0) {
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_UPDATE_Alert_Header, this.appConstants.COMM_ALERT_UPDATE_RECORD)
                .then(() => {
                    // this.Lookupobj.UpdateUserId = 2;
                    if (this.Lookupobj.rowStatus == 'Active') {
                        this.Lookupobj.rowStatus = 'A';
                    } else if (this.Lookupobj.rowStatus == 'In Active') {
                        this.Lookupobj.rowStatus = 'D';
                    }
                    this.Lookupobj.updateUserId = this.isloggedIn.userId;

                    this.administrationservice.SaveLookup(this.Lookupobj).subscribe(data => {
                        const res = data;
                        if (res) {
                            this.snackBar.open(this.appConstants.COMM_LOOKUP_UPDATE, '', {
                                duration: 2000,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['green-snackbar']

                            });
                            this.router.navigate(['/administration/lookup']);
                        }


                    });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
        } else {
            this.Lookupobj.lookupId = 0;
            if (this.Lookupobj.rowStatus == 'Active') {
                this.Lookupobj.rowStatus = 'A';
            } else if (this.Lookupobj.rowStatus == 'In Active') {
                this.Lookupobj.rowStatus = 'D';
            }
            this.Lookupobj.createUserId = this.isloggedIn.userId;
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_SAVE_Alert_Header, this.appConstants.COMM_ALERT_SAVE_RECORD)
                .then((ConfirmTrue) => {
                    this.administrationservice.SaveLookup(this.Lookupobj).subscribe(data => {
                        const res = data;
                        if (res.lookupId > 0) {
                            this.LookupID = res.lookupId;
                            let lookupPageAction: any = JSON.stringify(this.LookupID) + ';' + 'Edit';
                            this.administrationservice.list = lookupPageAction;
                            this.disable = false;
                            if (res) {
                                this.snackBar.open(this.appConstants.COMM_LOOKUP_SAVE, '', {
                                    duration: 2000,
                                    verticalPosition: 'top',
                                    horizontalPosition: 'end',
                                    panelClass: ['green-snackbar']

                                });
                                this.router.navigate(['/administration/lookup']);
                            }
                        }

                    });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
        }

    }

    deleteLookUpDetails(): void {
        let lookupDetailsIds = '';
        if (this.selection.selected.length == 0) {
            this.showError(this.appConstants.COMM_ATLEAST_SINGLE_RECORD);
        }
        else {
            this.selection.selected.forEach(item => {
                let a: any = item;
                if (lookupDetailsIds == '') {
                    lookupDetailsIds = a.lookupDetailsId.toString();
                } else {
                    lookupDetailsIds = lookupDetailsIds + ',' + a.lookupDetailsId.toString();
                }
            });
            let DeleteObj: any = {};
            DeleteObj.AdminUserID = this.isloggedIn.userId;
            DeleteObj.LookupDetailsIds = lookupDetailsIds;           
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_DELETE_Alert_Header,
                this.appConstants.COMM_ALERT_DELETE_RECORD)
                .then((ConfirmTrue) => {
                    this.administrationservice.DeleteLookupDetails(DeleteObj).subscribe(
                        (data) => {
                            const res = data;
                            if (res) {
                                this.snackBar.open(this.appConstants.COMM_LOOKUP_DETAILS_DELETE, '', {
                                    duration: 2000,
                                    verticalPosition: 'top',
                                    horizontalPosition: 'end',
                                    panelClass: ['green-snackbar']
                                });
                            }
                            this.getLookupDetailById(this.LookupID);

                        });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
        }
    }


    addLookUpDetails(): void {
        this.LookupDetailsobj = {};
        sessionStorage.setItem('LookupDetailsPage', JSON.stringify(0 + ';' + 'Add'));
       //let lookupDetailsPageAction: any = JSON.stringify(0) + ';' + 'Add';
       // this.administrationservice.list = lookupDetailsPageAction;
        const dialogRef = this.dialog.open(LookupdatailspopupComponent);

        dialogRef.afterClosed().subscribe(result => {

            if (result !== true) {
                this.getLookupById(this.LookupID);
            }
        });
    }
    viewLookup(): void {
        if (this.selection.selected.length === 1) {
            this.recId = this.selection.selected[0];
             sessionStorage.setItem('LookupDetailsPage', JSON.stringify(this.recId.lookupDetailsId + ';' + 'View'));
            //let lookupDetailsPageAction: any = JSON.stringify(this.recId.lookupDetailsId) + ';' + 'View';
            //this.administrationservice.list = lookupDetailsPageAction;
            const dialogRef = this.dialog.open(LookupdatailspopupComponent);
            dialogRef.afterClosed().subscribe(result => {
                if (result !== true) {
                    this.getLookupById(this.LookupID);
                }
            });
        } else {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        }
    }
    editLookUpDetails(): void {

        if (this.selection.selected.length === 1) {
            this.recId = this.selection.selected[0];
            sessionStorage.setItem('LookupDetailsPage', JSON.stringify(this.recId.lookupDetailsId + ';' + 'Edit'));
           // let lookupDetailsPageAction: any = JSON.stringify(this.recId.lookupDetailsId) + ';' + 'Edit';
            // this.administrationservice.setList(lookupDetailsPageAction);
            const dialogRef = this.dialog.open(LookupdatailspopupComponent);
            
            dialogRef.afterClosed().subscribe(result => {
                if (result !== true) {
                    this.getLookupById(this.LookupID);
                }
            });
        } else {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        }
    }

    // Refresh Page
    refresh(): void {
        this.filterName = '';
        if (this.LookupID > 0) {
            this.getLookupById(this.LookupID);
            // this.lookupform.reset();
        } else {
            this.Lookupobj = {};
            this.LookupID = 0;
            this.Lookupobj.rowStatus = 'Active';
        }

    }

    refreshlkpDetails() {

    }

    // Close Page
    close(): void {
        this.router.navigate(['administration/lookup']);
    }



    isAllSelected() {

        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource.data.length;
        return numSelected === numRows;
    }

    masterToggle() {
        this.isAllSelected() ?
            this.selection.clear() :
            this.dataSource.data.forEach(row => this.selection.select(row));
    }

    // alert message
    showError(error: string): void {
        this.dialog.open(AlertComponent, {
            data: { errorMsg: error },
            width: '250px',
            height: '100px',
            panelClass: 'alerrtpop-center'
        });
    }
}
