import { Component, OnInit, ViewChild, ChangeDetectorRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AdministrationService } from '../administration.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogRef } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TreeviewItem } from '../../../../node_modules/ngx-treeview';
import { SelectionModel } from '@angular/cdk/collections';
//import { ConfirmationDailogService } from 'app/custom_alerts/confirmation-dailog.service';
import { AppConstants } from 'app/app.constants';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { SecurityComponent } from '../security/security.component';
export interface UserData {

    UserName: any;
}
@Component({
  selector: 'app-security-look-up',
  templateUrl: './security-look-up.component.html',
  styleUrls: ['./security-look-up.component.scss']
})
export class SecurityLookUpComponent implements OnInit {
    UsersList: number;
    isloggedIn: any = [];
    displayedColumns: string[] = ['checkbox', 'UserName'];
    dataSource: MatTableDataSource<UserData>;
    selection = new SelectionModel<UserData>(true, []);
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    public user: SecurityComponent;
    constructor(
        private router: Router,
        private dialog: MatDialogRef<SecurityLookUpComponent>,
        public dialoge: MatDialog,
        @Inject(MAT_DIALOG_DATA) public data: { user: SecurityComponent },
        private administrationservice: AdministrationService,
        //private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants, public snackBar: MatSnackBar
    ) { }

    ngOnInit(): void {
        this.UsersList = parseInt(sessionStorage.getItem('roleId'));
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.DataGetting();
    }
    close() {
        this.dialog.afterClosed().subscribe(result => {

        });

        this.dialog.close();
    }
    users: any = [];
    DataGetting() {
        this.users = this.data.user;
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    UserRoleInsert: any;
    UserRoles: any = [];
    SaveUsersPopup() {
        if (this.selection.selected.length === 0) {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
            return;

        } else {
            let selectedRecords = [];
            selectedRecords = this.selection.selected;
            const roleuserarray = [];
            for (let i = 0; i < selectedRecords.length; i++) {

                roleuserarray.push({
                    IntegerColumn1: selectedRecords[i].UserId,
                    IntegerColumn2: this.UsersList
                }); // UserID -IntegerColumn1   // RoleID -IntegerColumn2
            }
            this.UserRoleInsert = { insertroleuser: roleuserarray, CreatedByUserID: this.isloggedIn.userId, UpdatedByUserID: this.isloggedIn.userId };
            //this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_UPDATE_Alert_Header, this.appConstants.COMM_ALERT_SAVE_RECORD)
            //    .then((ConfirmTrue) => {
                    this.administrationservice.UserRoleInsertBulk(this.UserRoleInsert).subscribe(result => {
                        const UserRoleResult = result;
                        if (UserRoleResult) {
                            this.snackBar.open(this.appConstants.COMM_USER_ROLE_SAVE, '', {
                                duration: 2500,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['green-snackbar']
                            });
                        }

                        this.dialog.close();
                    });
                //}, (ConfirmFalse) => { })
                //.catch((ConfirmFalse) => { });
        }
    }


    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource.data.length;
        return numSelected === numRows;
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
        this.isAllSelected() ?
            this.selection.clear() :
            this.dataSource.data.forEach(row => this.selection.select(row));
    }
    showError(error: string): void {
        this.dialoge.open(AlertComponent, {
            data: { errorMsg: error },
            width: '250px',
            height: '100px',
            panelClass: 'alerrtpop-center'
        });
    }

    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

}
