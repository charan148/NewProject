import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { AdministrationService } from '../administration.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { AppConstants } from 'app/app.constants';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { ConfirmationDailogService } from '../../custom_alerts/confirmation-dailog.service';

export interface UserData {
    firstName: string;
    lastName: any;
    userName: any;
    emailAddress: any;
}
@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
    UsersList: any = [];
    selectedUserData: any;
    filteruser: any;
    displayedColumns: string[] = ['select', 'firstName', 'lastName', 'userName', 'emailAddress'];
    dataSource: MatTableDataSource<UserData>;
    selection = new SelectionModel<UserData>(true, []);
    isloggedIn: any = [];
     permissions: any = [];
    UpdateUserId: any;
    createUserButton = false;
    editUserButton = false;
    viewUserButton = false;
    deleteUserButton = false;

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    constructor(private administrationservice: AdministrationService,
        private router: Router,
        private appConstants: AppConstants,
      private confirmationDialogService: ConfirmationDailogService,
        public dialog: MatDialog,
        public snackBar: MatSnackBar,) { }

    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.permissions = JSON.parse(localStorage.getItem('sessionPermissions'));
        for (let i = 0; i < this.permissions.length; i++) {
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[0].privilegeCode) {
                this.createUserButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[1].privilegeCode) {
                this.editUserButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[2].privilegeCode) {
                this.viewUserButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[3].privilegeCode) {
                this.deleteUserButton = true;
            }
        }
        this.administrationservice.GetAllUsers().subscribe(
            (data) => {
                var userdata = data;
                for (var i = 0; i < userdata.length; i++) {
                    if (userdata[i].isActive == true) {
                        this.UsersList.push(userdata[i]);
                    }
                }
                this.UsersList = this.UsersList.filter(tt => tt.userId != this.isloggedIn.userId);
                const users = userdata;
                this.dataSource = new MatTableDataSource(users);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            });
    }

    applyFilter(filterValue: string): void {
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    /** Whether the number of selected elements matches the total number of rows. */
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
    // create new user
    CreateNewUser(): void {
        let userPageAction: any = JSON.stringify(0) + ';' + 'Add';
        this.administrationservice.list = userPageAction;
        this.router.navigate(['/administration/user-details']);
    }

    // edit user
    editUser(): void {
        if (this.selection.selected.length !== 1) {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        } else {
            this.selectedUserData = this.selection.selected[0];
            let userPageAction: any = JSON.stringify(this.selectedUserData.userId) + ';' + 'Edit';
            this.administrationservice.list = userPageAction;
            this.router.navigate(['administration/user-details']);
        }
    }
    userclick(val: any) {
        let userPageAction: any = JSON.stringify(val) + ';' + 'Edit';
        this.administrationservice.list = userPageAction;
        this.router.navigate(['administration/user-details']);
    }

    // user view
    viewUser(): void {
        if (this.selection.selected.length !== 1) {
            this.showError(this.appConstants.COMM_ATLEAST_SINGLE_RECORD);
        } else {this.selectedUserData = this.selection.selected[0];
            let userPageAction: any = JSON.stringify(this.selectedUserData.userId) + ';' + 'View';
            this.administrationservice.list = userPageAction;
            this.router.navigate(['administration/user-details']);
        }
    }
     //user delete
    deleteUser(): void {
        let userids = '';
        if (this.selection.selected.length == 0) {
            this.showError(this.appConstants.COMM_ATLEAST_SINGLE_RECORD);
        } else {
            this.selection.selected.forEach(item => {
                let a: any = item;
                if (userids == '') {
                    userids = a.userId.toString();
                } else {
                    userids = userids + ',' + a.userId;
                }
            });

            let DeleteObj: any = {};
            DeleteObj.AdminUserId = this.isloggedIn.userId;
            DeleteObj.UserIds = userids;
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_DELETE_Alert_Header,
                this.appConstants.COMM_ALERT_DELETE_RECORD)
                .then((ConfirmTrue) => {
            this.administrationservice.deleteUser(DeleteObj).subscribe(
                        (response) => {
                    const responseData = response;
                    this.dataSource = new MatTableDataSource(responseData);
                    this.dataSource.paginator = this.paginator;
                    this.dataSource.sort = this.sort;
                    response  = new SelectionModel<UserData>(true, []);
                                this.snackBar.open(this.appConstants.COMM_USER_DEACTIVATED, '', {
                                    duration: 2000,
                                    verticalPosition: 'top',
                                    horizontalPosition: 'end',
                                    panelClass: ['green-snackbar']

                                });
                                this.refresh();
                        });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
        }
    }

    // refresh the grid
    refresh(): void {
        this.filteruser = '';
        this.administrationservice.GetAllUsers().subscribe(
            (data) => {
                this.UsersList = [];
                var userdata = data;
                for (var i = 0; i < userdata.length; i++) {
                    if (userdata[i].isActive == true) {
                        this.UsersList.push(userdata[i]);
                    }
                }
                const users = userdata;
                this.dataSource = new MatTableDataSource(users);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
                //this.changeDetectorRefs.detectChanges();
                this.selection = new SelectionModel<UserData>(true, []);
            })
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
