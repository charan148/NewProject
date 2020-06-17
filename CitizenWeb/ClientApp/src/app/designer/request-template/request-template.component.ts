import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { AppConstants } from 'app/app.constants';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { ConfirmationDailogService } from '../../custom_alerts/confirmation-dailog.service';
import { AdministrationService } from '../../administration/administration.service';
export interface RequestTemplateData {
    
    requestName: any;
    requestDescription: any;
    displayType: any;
    createdDate: any;
    visibility: any;
    categoryText: any;
    priorityText: any;
}

@Component({
  selector: 'app-request-template',
  templateUrl: './request-template.component.html',
  styleUrls: ['./request-template.component.scss']
})
export class RequestTemplateComponent implements OnInit {

    RequestTemplateList: any = [];
    selectedRequestTemplateData: any;
    filterrequesTtemplate: any;
    displayedColumns: string[] = ['select', 'requestName', 'requestDescription', 'displayType', 'createdDate',
        'visibility', 'categoryText', 'priorityText'];
    dataSource: MatTableDataSource<RequestTemplateData>;
    selection = new SelectionModel<RequestTemplateData>(true, []);
    isloggedIn: any = [];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    constructor(private administrationservice: AdministrationService,
        private router: Router,
        private appConstants: AppConstants,
        private confirmationDialogService: ConfirmationDailogService,
        public dialog: MatDialog,
        public snackBar: MatSnackBar, ) { }

    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.administrationservice.GetAllRequestTemplates().subscribe(
            (data) => {
                var requesttemplatedata = data;
                for (var i = 0; i < requesttemplatedata.length; i++) {
                    if (requesttemplatedata[i].isActive == true) {
                        this.RequestTemplateList.push(requesttemplatedata[i]);
                    }
                }
                this.RequestTemplateList = this.RequestTemplateList.filter(tt => tt.requestTemplateId != this.isloggedIn.userId);
                const requesttemplate = requesttemplatedata;
                this.dataSource = new MatTableDataSource(requesttemplate);
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
    // create new Request Template
    CreateNewTemplate(): void {
        sessionStorage.setItem('RequestTemplatePageAction', JSON.stringify(0 + ';' + 'Add'));
        let templatePageAction: any = JSON.stringify(0) + ';' + 'Add';
        this.administrationservice.setList(templatePageAction);
       // this.router.navigate(['/administration/requesttemplatedetails']);
        this.router.navigate(['/designer/designer']);
    }

    // edit  Request Template
    editTemplate(): void {
        if (this.selection.selected.length !== 1) {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        } else {
            this.selectedRequestTemplateData = this.selection.selected[0];
           // sessionStorage.setItem('RequestTemplatePageAction', JSON.stringify(this.selectedRequestTemplateData.requestTemplateId + ';' + 'Edit'));
            let templatePageAction: any = JSON.stringify(this.selectedRequestTemplateData.requestTemplateId) + ';' + 'Edit';
            this.administrationservice.setList(templatePageAction);
            this.router.navigate(['/designer/designer']);
            // this.router.navigate(['administration/requesttemplatedetails']);
        }
    }
   
    // Request Template view
    viewTemplate(): void {
        if (this.selection.selected.length !== 1) {
            this.showError(this.appConstants.COMM_SINGLE_RECORD);
        } else {
            this.selectedRequestTemplateData = this.selection.selected[0];
          //  sessionStorage.setItem('RequestTemplatePageAction', JSON.stringify(this.selectedRequestTemplateData.requestTemplateId + ';' + 'View'));
            let templatePageAction: any = JSON.stringify(this.selectedRequestTemplateData.requestTemplateId) + ';' + 'View';
            this.administrationservice.setList (templatePageAction);
            this.router.navigate(['/designer/designer']);
           // this.router.navigate(['administration/requesttemplatedetails']);
        }
    }
    //Request Template delete
    deleteTemplate(): void {
        let requesttempalteids = '';
        if (this.selection.selected.length == 0) {
            this.showError(this.appConstants.COMM_ATLEAST_SINGLE_RECORD);
        } else {
            this.selection.selected.forEach(item => {
                let a: any = item;
                if (requesttempalteids == '') {
                    requesttempalteids = a.requestTemplateId.toString();
                } else {
                    requesttempalteids = requesttempalteids + ',' + a.requestTemplateId;
                }
            });

            let DeleteObj: any = {};
            DeleteObj.Admin6Id = this.isloggedIn.userId;
            DeleteObj.RequestTemplateIds = requesttempalteids;
              this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_DELETE_Alert_Header,
               this.appConstants.COMM_ALERT_DELETE_RECORD)
              .then((ConfirmTrue) => {
            this.administrationservice.deleterequestTemplate(DeleteObj).subscribe(
                (response) => {
                    const res = response;
                    if (res) {
                        this.snackBar.open(this.appConstants.COMM_REQUESTTEMPLATE_DEACTIVATED, '', {
                        duration: 2000,
                        verticalPosition: 'top',
                        horizontalPosition: 'end',
                        panelClass: ['green-snackbar']

                        });
                        this.refresh();
                    }
                });
              }, (ConfirmFalse) => { })
               .catch((ConfirmFalse) => { });
            //  this._router.navigate(['administration/userdetails']);
        }
    }

    // refresh the grid
    refresh(): void {
        this.filterrequesTtemplate = '';
        this.administrationservice.GetAllRequestTemplates().subscribe(
            (data) => {
                this.RequestTemplateList = data;
                this.RequestTemplateList = [];
                var requesttemplatedata = data;
                for (var i = 0; i < requesttemplatedata.length; i++) {
                    if (requesttemplatedata[i].isActive == true) {
                        this.RequestTemplateList.push(requesttemplatedata[i]);
                    }
                }
                this.dataSource = new MatTableDataSource(data);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
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
