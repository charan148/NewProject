import { Component, OnInit, ViewChild, PACKAGE_ROOT_URL } from '@angular/core';
import { Router } from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';
import { AppConstants } from 'app/app.constants';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { AdministrationService } from '../administration.service';
import { ConfirmationDailogService } from '../../custom_alerts/confirmation-dailog.service';

export interface LookupData {
    lookupName: any;
    lookupLongDescription: any;
    lookupDescription: any;
    lookupShortDescription: any
    lookupDisplayDescription: any
    isUsedInDisplay: any;
    rowStatus: any;
    effectiveFrom: any;
    effectiveTo: any;
}

@Component({
    selector: 'app-lookup',
    templateUrl: './lookup.component.html',
    styleUrls: ['./lookup.component.scss']
})
export class LookupComponent implements OnInit {
    lookupsList: any;
    recId: any;
    isloggedIn: any;
    createlookUpButton = false;
    editlookUpButton = false;
    viewlookUpButton = false;
    deletelookUpButton = false;
    permissions: any = [];
    displayedColumns: string[] = ['select', 'lookupName', 'lookupLongDescription', 'lookupDescription', 'lookupShortDescription', 'lookupDisplayDescription', 'isUsedInDisplay', 'effectiveFrom', 'effectiveTo', 'rowStatus'];
    dataSource: MatTableDataSource<LookupData>;
    selection = new SelectionModel<LookupData>(true, []);

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    constructor(
        private router: Router,
        private administrationservice: AdministrationService,
        public dialog: MatDialog,
        private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants,
        public snackBar: MatSnackBar
    ) { }

    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.permissions = JSON.parse(localStorage.getItem('sessionPermissions'));
        for (let i = 0; i < this.permissions.length; i++) {
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[9].privilegeCode) {
                this.createlookUpButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[10].privilegeCode) {
                this.editlookUpButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[11].privilegeCode) {
                this.viewlookUpButton = true;
            } 
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[12].privilegeCode) {
                this.deletelookUpButton = true;
            }
        }
        this.administrationservice.GetAllLookups().subscribe(
            (data) => {
                this.lookupsList = data;
                const lookups = this.lookupsList;
                this.dataSource = new MatTableDataSource(lookups);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            });
    }
    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    // Create New Lookup
    createNewLookup(): void {
       // sessionStorage.setItem('LookupPageAction', JSON.stringify(0 + ';' + 'Add'));
        let lookupPageAction: any = JSON.stringify(0) + ';' + 'Add';
        this.administrationservice.list = lookupPageAction;
        this.router.navigate(['administration/lookupdetails']);
    }

    // Edit Lookup
    editLookup(): void {
        const numSelected = this.selection.selected.length;
        if (this.selection.selected.length === 1) {
            this.recId = this.selection.selected[0];
           // sessionStorage.setItem('LookupPageAction', JSON.stringify(this.recId.lookupId + ';' + 'Edit'));
            let lookupPageAction: any = JSON.stringify(this.recId.lookupId) + ';' + 'Edit';
            this.administrationservice.list = lookupPageAction;
            this.router.navigate(['administration/lookupdetails']);
        } else {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        }
    }
    lookupclick(val: any) {
       // sessionStorage.setItem('LookupPageAction', JSON.stringify(val + ';' + 'Edit'));
        let lookupPageAction: any = JSON.stringify(val) + ';' + 'Edit';
        this.administrationservice.list = lookupPageAction;
        this.router.navigate(['administration/lookupdetails'])
    }
    // View Lookup
    viewLookup(): void {
        const numSelected = this.selection.selected.length;
        if (this.selection.selected.length === 1) {
            this.recId = this.selection.selected[0];
           // sessionStorage.setItem('LookupPageAction', JSON.stringify(this.recId.lookupId + ';' + 'View'));
            let lookupPageAction: any = JSON.stringify(this.recId.lookupId) + ';' + 'View';
            this.administrationservice.list = lookupPageAction;
            this.router.navigate(['administration/lookupdetails']);
        } else {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        }
    }
    // Refresh Grid
    refreshLookup(): void {
        this.administrationservice.GetAllLookups().subscribe(
            (data) => {
                this.lookupsList = data;
                const lookups = this.lookupsList;
                this.dataSource = new MatTableDataSource(lookups);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
                this.selection = new SelectionModel<LookupData>(true, []);
            });
    }

    deleteLookup(): void {
        let lookupids = '';
        if (this.selection.selected.length == 0) {
            this.showError(this.appConstants.COMM_ATLEAST_SINGLE_RECORD);
        }
        else {
            this.selection.selected.forEach(item => {
                let a: any = item;
                if (lookupids == '') {
                    lookupids = a.lookupId.toString();
                } else {
                    lookupids = lookupids + ',' + a.lookupId.toString();
                }
            });
            let DeleteObj: any = {};
            DeleteObj.AdminUserID = this.isloggedIn.userId;
            DeleteObj.LookupIds = lookupids;
            // DeleteObj.RowStatus = 'D';;
            // this.recId = this.selection.selected[0];

            // this.recId.RowStatus = 'D';
            // this.recId.UpdateUserId = this.isloggedIn.userId;
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_DELETE_Alert_Header,
                this.appConstants.COMM_ALERT_DELETE_RECORD)
                .then((ConfirmTrue) => {
            this.administrationservice.DeleteLookup(DeleteObj).subscribe(
                (data) => {
                    const res = data;
                    if (res) {
                        this.snackBar.open(this.appConstants.COMM_LOOKUP_DELETE, '', {
                            duration: 2000,
                            verticalPosition: 'top',
                            horizontalPosition: 'end',
                            panelClass: ['green-snackbar']
                        });
                    }
                    this.refreshLookup();
                });
            }, (ConfirmFalse) => { })
            .catch((ConfirmFalse) => { });
        }

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

    // Alert message
    showError(error: string): void {
        this.dialog.open(AlertComponent, {
            data: { errorMsg: error },
            width: '400px',
            height: '100px',
            panelClass: 'alerrtpop-center'
        });
    }

}
