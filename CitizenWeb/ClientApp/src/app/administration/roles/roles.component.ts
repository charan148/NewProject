import { Component, OnInit, ViewChild } from '@angular/core';
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

export interface RoleData {
    roleName: any;
    isActive: any;
}


@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
    styleUrls: ['./roles.component.scss'],
    providers: [MatSnackBar]
})
export class RolesComponent implements OnInit {
    RolesList: any = [];
    recId: any;
    filterrole: any;
    isloggedIn: any;
    permissions: any = [];
    deleteRoleButton = false;
    createRoleButton = false;
    editRoleButton = false;
    viewRoleButton = false;
    displayedColumns: string[] = ['select', 'roleName', 'isActive'];
    dataSource: MatTableDataSource<RoleData>;
    selection = new SelectionModel<RoleData>(true, []);

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    constructor(
        private router: Router,
        private administrationservice: AdministrationService,
        public dialog: MatDialog,
        private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants,
        public snackBar: MatSnackBar

    ) {

    }
    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.permissions = JSON.parse(localStorage.getItem('sessionPermissions'));

        for (let i = 0; i < this.permissions.length; i++) {
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[4].privilegeCode) {
                this.createRoleButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[5].privilegeCode) {
                this.editRoleButton = true;
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[6].privilegeCode) {
                this.viewRoleButton = true;
               
            }
            if (this.permissions[i].privilegeCode === this.appConstants.COMM_PRIVILEGES[7].privilegeCode) {
                this.deleteRoleButton = true;
            }
        }

        this.administrationservice.GetAllRoles().subscribe(
            (data) => {
                var roledata = data;
                for (var i = 0; i < roledata.length; i++) {
                    if (roledata[i].isActive == true) {
                        this.RolesList.push(roledata[i]);
                    }
                }

                const roles = this.RolesList;
                this.dataSource = new MatTableDataSource(roles);
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

    // Create New Role
    createNewRole(): void {
        let rolePageAction: any = JSON.stringify(0) + ';' + 'Add';
        this.administrationservice.list = rolePageAction;
        this.router.navigate(['administration/roles-details']);
    }

    // Edit Role
    editRole(): void {
       
        if (this.selection.selected.length === 1) {
            this.recId = this.selection.selected[0];
            let rolePageAction: any = JSON.stringify(this.recId.roleId) + ';' + 'Edit';
            this.administrationservice.list = rolePageAction;
            this.router.navigate(['administration/roles-details']);
        } else {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        }
    }
    roleclick(val: any) {
      
        let rolePageAction: any = JSON.stringify(val) + ';' + 'Edit';
        this.administrationservice.list = rolePageAction;
        this.router.navigate(['administration/roles-details'])
    }
    // View Role
    viewRole(): void {
        const numSelected = this.selection.selected.length;
        if (this.selection.selected.length === 1) {
            this.recId = this.selection.selected[0];
            let rolePageAction: any = JSON.stringify(this.recId.roleId) + ';' + 'View';
            this.administrationservice.list = rolePageAction;
            this.router.navigate(['administration/roles-details']);
        } else {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        }
    }
   //  Delete Role
    deleteRole(): void {
        let roleids = '';
        if (this.selection.selected.length == 0) {
            this.showError(this.appConstants.COMM_ATLEAST_SINGLE_RECORD);
        } else {
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_DELETE_Alert_Header,
                this.appConstants.COMM_ALERT_DELETE_RECORD)
                .then((ConfirmTrue) => {
            this.selection.selected.forEach(item => {
                let a: any = item;
                if (roleids == '') {
                    roleids = a.roleId.toString();
                } else {
                    roleids = roleids + ',' + a.roleId;
                }
            });

            let DeleteObj: any = {};
            DeleteObj.AdminUserId = this.isloggedIn.userId;
            DeleteObj.RoleIds = roleids;
            this.administrationservice.deleteRole(DeleteObj).subscribe(
                        (response) => {
                    const responseData = response;
                    this.dataSource = new MatTableDataSource(responseData);
                    this.dataSource.paginator = this.paginator;
                    this.dataSource.sort = this.sort;
                    this.selection = new SelectionModel<RoleData>(true, []);
                          
                                this.snackBar.open(this.appConstants.COMM_ROLE_DEACTIVATED, '', {
                                    duration: 2000,
                                    verticalPosition: 'top',
                                    horizontalPosition: 'end',
                                    panelClass: ['green-snackbar']

                                });
                             
                          
                        });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
            //  this._router.navigate(['administration/roles']);
        }
    }


    // Refresh Grid
    refreshRole(): void {
        this.filterrole = '';
        this.administrationservice.GetAllRoles().subscribe(
            (data) => {
                this.RolesList = [];
                var roledata = data;
                for (var i = 0; i < roledata.length; i++) {
                    if (roledata[i].isActive == true) {
                        this.RolesList.push(roledata[i]);
                    }
                }

                const roles = this.RolesList;
                this.dataSource = new MatTableDataSource(roles);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
                this.selection = new SelectionModel<RoleData>(true, []);
            });
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
            disableClose: true,
            width: '250px',
            height: '100px',
            panelClass: 'alerrtpop-center'
        });
    }
}
