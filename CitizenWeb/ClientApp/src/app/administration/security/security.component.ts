import { Component, OnInit, Input, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { AdministrationService } from '../administration.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { TreeviewItem } from '../../../../node_modules/ngx-treeview';
import { SecurityLookUpComponent } from '../././././security-look-up/security-look-up.component';
import { SelectionModel } from '@angular/cdk/collections';
import { ConfirmationDailogService } from 'app/custom_alerts/confirmation-dailog.service';
import { AppConstants } from 'app/app.constants';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
//import { FuseNavigationComponent } from '../../../../@fuse/components/navigation/navigation.component'
declare var $: any;
export interface UserData {

    userName: any;
}
@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.scss']
})
export class SecurityComponent implements OnInit {
    Modules: any = [];
    PageSelect: any = [];
    Roles: any = [];
    Users: any = [];
    Result: any = [];
    RoleAllItems: any;
    RoleItems: any;
    favoriteSeason: any;
    RolePrivilegeList: any = [];
    modulePriviliges: any = [];
    rolebasedusers: any = [];
    GettingUserROle: any = [];
    RoleSelectModuleId: any = 0;
    RoleSelectPageId: any = 0;
    UsersMaster: any;
    UseRoleID: any;
    UsersList: any = [];
    Role: any;
    checkME: any = [];
    arguments: any = [];
    results: any = [];
    finalRoleSelectedItems: any = [];
    roleprivilegeobj: any = {};
    RolesObj: any = {};
    finalArray: any = [];
    UserRoles: any = [];
    remainUser: any = [];
    UserRoleRes: any = [];
    UserRoleAdd: any = {};
    Roleuserlist: any = {};
    selectedRecords: any = [];
    PageSelected: any = [];
    filterPage: any = [];
    filtered: any = [];
    selectedPage: any = [];
    isloggedIn: any = [];
    displayedColumns: string[] = ['checkbox', 'userName'];
    dataSource: MatTableDataSource<UserData>;
    selection = new SelectionModel<UserData>(true, []);
    @Input() public user: SecurityLookUpComponent;
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    constructor(private router: Router,
        private securityService: AdministrationService,
        public dialog: MatDialog,
        private changeDetectorRefs: ChangeDetectorRef, public snackBar: MatSnackBar,
        private appConstants: AppConstants,
        private confirmationDialogService: ConfirmationDailogService
    ) { }

    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.getModules();
        this.getpages();
        this.GetActiveRoles();
        this.GetAllModPagPrev();
        this.GetAllUsers();
        this.RoleSelectModuleId = 105284985447;
        this.RoleSelectPageId = 5478911223;
    }
    GetActiveRoles() {
        this.securityService.GetAllActiveRoles().subscribe(
            (roles) => {
                this.Roles = roles;


            }
        );
    }
    GetAllUsers() {
        this.securityService.GetAllUsers().subscribe(
            (data) => {
                this.Users = data;
                // console.log(data);
                const user = this.Users;
                this.Users = [];
                for (let i = 0; i < user.length; i++) {
                    //if (user[i].isActive == true) {
                        this.Users.push({ FirstName: user[i].firstName, UserId: user[i].userId, UserName: user[i].userName, RoleID: user[i].roleId });
                    //}
                }
                this.UsersMaster = Object.assign([], this.Users);
            });

    }
    getModules() {
        this.securityService.GetAllModules().subscribe(
            (module) => {
                this.Modules = module;
                this.Modules.push({ 'moduleID': 105284985447, 'moduleName': "All" });
                this.Modules.sort(function (a, b) { return b.moduleID - a.moduleID; });
                this.filtered = this.Modules;


            });
    }

    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
    getpages() {

        this.securityService.Getpages().subscribe(
            (page) => {
                this.PageSelect = page;
                this.PageSelected = this.PageSelect;
                this.PageSelect.push({ 'moduleID': 105284985447, 'pageID': 5478911223, 'pageName': "All" });
                this.PageSelect.sort(function (a, b) { return b.pageID - a.pageID; });
                // console.log(this.PageSelect);
            });
    }
    GetAllModPagPrev() {
        this.securityService.GetAllModPagPrev().subscribe(
            (data) => {
                this.Result = data;
                //console.log(data);
                this.Result.sort(function (a, b) { return a.moduleID - b.moduleID; });
                if (this.Result != null) {
                    this.bindRolestree(this.Result);
                }
            }
        );
    }
    bindRolestree(result) {
        this.RoleAllItems = [];
        this.RoleItems = [];

        if (this.Modules != null && result.length > 0) {

            for (let i = 0; i < result.length; i++) {

                const obj = {
                    text: result[i].moduleName,
                    value: 'Module_' + i,
                    checked: false,
                    children: []
                };
                if (result[i].pages != null && result[i].pages !== undefined) {
                    for (let j = 0; j < result[i].pages.length; j++) {

                        const pgObj = {
                            text: result[i].pages[j].pageName,
                            value: 'Pages_' + j,
                            checked: false,
                            children: []
                        };
                        if (result[i].pages[j].privileges != null && result[i].pages[j].privileges !== undefined) {
                            for (let k = 0; k < result[i].pages[j].privileges.length; k++) {

                                const actionObj = {
                                    text: result[i].pages[j].privileges[k].privilegeName,
                                    value: result[i].pages[j].privileges[k].privilegeID,
                                    checked: false,
                                    children: []
                                };
                                pgObj.children.push(actionObj);
                            }
                        }
                        obj.children.push(pgObj);
                    }
                }

                this.RoleAllItems.push(new TreeviewItem({ text: obj.text, value: obj.value, children: obj.children }));
            }


            this.RoleItems = this.RoleAllItems;
           setTimeout(() => {
                $('.form-check-input').attr('aria-label', 'Check Uncheck');
           }, 1000);



        }
    }
    GetRolePrivileges(roleId) {
        this.filtered = this.Modules;
        this.RoleSelectModuleId = 105284985447;
        this.RoleSelectPageId = 5478911223;
        this.PageSelected = this.PageSelect.filter(x => x.moduleID === this.RoleSelectModuleId);
        this.UseRoleID = roleId;
        this.Role = {};
        this.Role.roleId = roleId;
        sessionStorage.setItem('roleId', this.UseRoleID);
        this.securityService.RolePrivilegeSelect(this.Role).subscribe(
            (result) => {
                this.RolePrivilegeList = result;
                // console.log(result);
                this.bindRolePrivilegestree(this.RolePrivilegeList, this.Result);
            });
        this.securityService.GetAllUserRoles(roleId).subscribe(
            (data) => {
                this.UserRoles = data;

                this.dataSource = new MatTableDataSource(this.UserRoles);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
                this.changeDetectorRefs.detectChanges();
            });
    }

    bindRolePrivilegestree(result, filterResult) {
        this.RoleAllItems = [];
        this.RoleItems = [];

        if (this.Modules != null && filterResult.length > 0) {

            for (let i = 0; i < filterResult.length; i++) {

                const obj = {
                    text: filterResult[i].moduleName,
                    value: 'Module_' + i,
                    checked: false,
                    children: []
                };
                if (filterResult[i].pages != null && filterResult[i].pages !== undefined) {
                    for (let j = 0; j < filterResult[i].pages.length; j++) {

                        const pgObj = {
                            text: filterResult[i].pages[j].pageName,
                            value: 'Pages_' + j,
                            checked: false,
                            children: []
                        };

                        if (filterResult[i].pages[j].privileges != null && filterResult[i].pages[j].privileges !== undefined) {
                            for (let k = 0; k < filterResult[i].pages[j].privileges.length; k++) {
                                const actionObj = {
                                    text: filterResult[i].pages[j].privileges[k].privilegeName,
                                    value: filterResult[i].pages[j].privileges[k].privilegeId,
                                    id: filterResult[i].pages[j].privileges[k].privilegeId,
                                    checked: false,
                                    children: []
                                };
                                if (result != null && result.length > 0) {
                                    for (let l = 0; l < result.length; l++) {
                                        if (filterResult[i].pages[j].privileges[k].privilegeId === result[l].privilegeID) {
                                            actionObj.checked = result[l].isActive;
                                        }
                                    }
                                }
                                pgObj.children.push(actionObj);


                            }
                        }
                        obj.children.push(pgObj);
                    }
                }

                this.RoleAllItems.push(new TreeviewItem({
                    text: obj.text, value: obj.value, children: obj.children
                }));
            }


            this.RoleItems = this.RoleAllItems;
            setTimeout(() => {
                $('.form-check-input').attr('aria-label', 'Check Uncheck');
            }, 1000);
        }
    }
    // RoleSelectPageId: any;
    onRoleSelectedChange(args) {
        if (args.length > 0) {
            this.checkME.push(args);
            if (this.finalRoleSelectedItems != null && this.finalRoleSelectedItems.length > 0) {
                if (this.RoleSelectModuleId > 0) {
                    if (this.RoleSelectPageId > 0) {
                        for (let i = 0; i < this.finalRoleSelectedItems.length; i++) {
                            var itm = this.finalRoleSelectedItems[i];
                            if (itm != undefined) {
                                if (itm[1] === this.RoleSelectModuleId && itm[2] === this.RoleSelectPageId) {
                                    this.finalRoleSelectedItems.splice(i, 1);
                                }
                            }
                        }
                        for (let j = 0; j < args.length; j++) {
                            this.finalRoleSelectedItems.push(args[j]);
                        }
                    } else {
                        for (let i = 0; i < this.finalRoleSelectedItems.length; i++) {
                            var itm = this.finalRoleSelectedItems[i];
                            if (itm[1] === this.RoleSelectModuleId) {
                                this.finalRoleSelectedItems.splice(i, 1);
                            }
                        }
                        for (let j = 0; j < args.length; j++) {
                            this.finalRoleSelectedItems.push(args[j]);
                        }
                    }
                } else {
                    this.finalRoleSelectedItems = args;
                }
            } else {
                this.finalRoleSelectedItems = args;
            }
        } else {
            this.checkME.push(args);
        }


    }
    RolePrivilegeUpsert() {
        this.PageSelected = null;
        this.filtered = null;
        const last: any = this.checkME[this.checkME.length - 1];
        for (let i = 0; i < last.length; i++) {
            this.finalArray.push({ PrivilegeId: last[i], RoleId: this.Role.roleId, IsActive: true });
        }
        this.roleprivilegeobj = {
            RoleId: this.Role.roleId, createUserId: this.isloggedIn.userId, UpdateUserId: this.isloggedIn.userId
        };


        this.RolesObj = {
            privilegelist: this.finalArray,
            roleslist: this.roleprivilegeobj,

        };
        this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_UPDATE_Alert_Header, this.appConstants.COMM_ALERT_SECURITY_SAVE_RECORD)
            .then((ConfirmTrue) => {
                this.securityService.RolePrivilegeUpsert(this.RolesObj).subscribe(
                    (data) => {
                        const RolePrivilegeUpsertlist = data;
                        if (RolePrivilegeUpsertlist === true) {
                            if (this.Roles.find(item => item.roleId == this.favoriteSeason)['roleName'] == 'Administrator') {
                                let adminPages = this.RoleItems
                                let dashboardPage = adminPages[1].children[0].internalChildren
                                adminPages = adminPages[0].children[0].internalChildren
                            }
                            this.snackBar.open(this.appConstants.COMM_ROLE_PREVILEGE_UPDATED, '', {
                                duration: 2000,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['green-snackbar']
                            });
                        }
                    });
                this.finalArray = [];
            }, (ConfirmFalse) => { })
            .catch((ConfirmFalse) => { });


    }
    Adduser() {
        this.rolebasedusers = [];
        // const userlist = this.UsersMaster;
        //this.rolebasedusers = userlist;

        //for (var i = 0; i < userlist.length; i++) {

        //    this.rolebasedusers.push(userlist[i]);

        //}


        //for (let i = 0; i < this.UserRoles.length; i++) {
        //  for (let j = this.rolebasedusers.length - 1; j >= 0; j--) {
        //    if (this.rolebasedusers[j].UserId === this.UserRoles[i].userId) {
        //      this.rolebasedusers.splice(j, 1);

        //    }
        //}
        //  }
        this.rolebasedusers = this.UsersMaster.filter(tt => tt.RoleID == 0);


        const dialogRef = this.dialog.open(SecurityLookUpComponent, {
            data: {
                user: this.rolebasedusers
            },
            width: '800px',

        });
        dialogRef.afterClosed().subscribe(result => {

            if (result !== true) {
                this.securityService.GetAllUserRoles(this.Role.roleId).subscribe(
                    (data) => {
                        this.UserRoles = data;
                        for (let i = 0; i < this.rolebasedusers.length; i++) {
                            let lenAddedUsr = this.UserRoles.filter(tt => tt.userId == this.rolebasedusers[i].UserId);
                            if (lenAddedUsr.length != 0) {
                                const chkrole = this.UserRoles.find(tt => tt.userId == this.rolebasedusers[i].UserId)['roleId'];
                                if (chkrole != 0) {
                                    this.UsersMaster.find(tt => tt.UserId == this.rolebasedusers[i].UserId)['RoleID'] = chkrole;
                                }
                            }
                        }
                        //console.log(data);
                        this.dataSource = new MatTableDataSource(this.UserRoles);
                        this.dataSource.paginator = this.paginator;
                        this.dataSource.sort = this.sort;
                        this.changeDetectorRefs.detectChanges();
                        this.selection = new SelectionModel<UserData>(true, []);
                    });
            }
        });

    }
    DeleteUserRole() {
        this.selectedRecords = [];
        this.selectedRecords = this.selection.selected;
        if (this.selectedRecords.length === 0) {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
            return;

        } else {
            let rolebasedusers = [];
            rolebasedusers = this.UserRoles;
            for (let i = 0; i < this.selectedRecords.length; i++) {
                for (let j = rolebasedusers.length - 1; j >= 0; j--) {
                    if (rolebasedusers[j].userId === this.selectedRecords[i].userId) {
                        rolebasedusers.splice(j, 1);
                    }

                }

            }

            const roleusers = [];
            for (let i = 0; i < rolebasedusers.length; i++) {
                roleusers.push({ IntegerColumn: rolebasedusers[i].userId });
            }

            this.UsersMaster = Object.assign([], this.Users);

            this.Roleuserlist = { UpdateUserId: this.isloggedIn.userId, RoleID: this.Role.roleId };

            this.UserRoleAdd = { Addroleuser: roleusers, roleuserlist: this.Roleuserlist };
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_DELETE_Alert_Header, this.appConstants.COMM_ALERT_DELETE_RECORD)
                .then((ConfirmTrue) => {
                    this.securityService.UserRoleUpsert(this.UserRoleAdd).subscribe((result) => {
                        for (let i = 0; i < this.selectedRecords.length; i++) {

                            this.UsersMaster.find(tt => tt.UserId == this.selectedRecords[i].userId)['RoleID'] = 0;
                        }

                        this.UserRoleRes = result;
                        this.UserRoles = rolebasedusers;
                        this.refresh();
                        if (result === true) {
                            this.snackBar.open(this.appConstants.COMM_USER_ROLE_DELETE, '', {
                                duration: 2000,
                                verticalPosition: 'top',
                                horizontalPosition: 'end',
                                panelClass: ['green-snackbar']

                            });
                        }

                    });
                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });

        }
    }
    selectModule() {
        if (this.RoleSelectModuleId != 105284985447) {
            this.modulePriviliges = [];
            if (this.RoleSelectModuleId !== 0) {
                // this.pagesenable = true;
                this.filtered = this.Result.filter(x => x.moduleID === this.RoleSelectModuleId);
                this.filterPage = this.filtered;
                this.bindRolePrivilegestree(this.RolePrivilegeList, this.filtered);
                this.PageSelected = this.PageSelect.filter(x => x.moduleId === this.RoleSelectModuleId);
                this.PageSelected.push({ 'moduleID': 105284985447, 'pageID': 5478911223, 'pageName': "All" });
                this.PageSelected.sort(function (a, b) { return b.pageID - a.pageID; });
            } else {
                this.GetRolePrivileges(this.UseRoleID);
                this.PageSelected = null;
                this.filtered = null;

            }
        }
        else if (this.RoleSelectModuleId == 105284985447) {
            this.PageSelected = this.PageSelect.filter(x => x.moduleID === this.RoleSelectModuleId);
            this.bindRolePrivilegestree(this.RolePrivilegeList, this.Result);
        } else {
            this.ngOnInit();
        }

    }
    selectPage() {
        if (this.RoleSelectPageId !== 5478911223) {
            const pageFilter = this.Result.filter(x => x.moduleID === this.RoleSelectModuleId);
            this.selectedPage = [];
            for (let i = 0; i < pageFilter[0].pages.length; i++) {
                if (pageFilter[0].pages[i].pageID === this.RoleSelectPageId) {
                    this.selectedPage.push(pageFilter[0].pages[i]);
                }
            }
            const count = [];
            count.push({ moduleID: pageFilter[0].moduleID, moduleName: pageFilter[0].moduleName, pages: this.selectedPage });
            this.bindRolePrivilegestree(this.RolePrivilegeList, count);
        } else if (this.RoleSelectPageId == 5478911223) {
            this.selectModule();

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
    refresh(): void {
        this.securityService.GetAllUserRoles(this.Role.roleId).subscribe(
            (data) => {
                this.UserRoles = data;
                this.dataSource = new MatTableDataSource(this.UserRoles);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
                this.changeDetectorRefs.detectChanges();
                this.selection = new SelectionModel<UserData>(true, []);
            });
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
