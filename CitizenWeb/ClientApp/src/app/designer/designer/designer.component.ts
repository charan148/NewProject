import { Component, OnInit } from '@angular/core';
import { LayoutpopupComponent } from '../layoutpopup/layoutpopup.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, FormControl,ValidationErrors, ValidatorFn } from '@angular/forms';
import { AdministrationService } from '../../administration/administration.service';
import { RequestFormService } from '../../services/requestform.service';
import { Router } from '@angular/router';
import { ConfirmationDailogService } from '../../custom_alerts/confirmation-dailog.service';
import { AppConstants } from '../../app.constants';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
    selector: 'app-designer',
    templateUrl: './designer.component.html',
    styleUrls: ['./designer.component.scss']
})
export class DesignerComponent implements OnInit {

    LabelValid: boolean=false;
    requestTemplateForm: FormGroup;
    constructor(private dialog: MatDialog, private administrationservice: AdministrationService,
        private requestService: RequestFormService,
        private router: Router,
        private confirmationDialogService: ConfirmationDailogService,
        private appConstants: AppConstants,
        public snackBar: MatSnackBar,
        private formBuilder: FormBuilder,
    ) {
        
    }

    form: FormGroup;

    displayType = [];
    Visibility = [];
    Priority = [];
    category = [];
    selectedDisplayOption: any;
    selectedvisibilityOption: any;
    selectedCategoryOption: any;
    selectedpriorityOption: any;
    requesttemplateid: any;
    currentUser: any;
    RequestObj: any = {};
    disablefields = false;
    isSaveHide = false;
    RequestTemplateSectionControl: any;
    controlOPtions: any;
    Controls: any;
    public nestedList = {
        selected: null,
        templates: [],
        dropzones: [[]]
    };
    ngOnInit(): void {
        this.currentUser = JSON.parse(localStorage.getItem('login'));
        const obj = this.administrationservice.getList();
        if (obj !== null && obj !== undefined &&
            obj !== 'null' && obj !== 'undefined') {
            if (obj.toString().indexOf(';') > -1) {
                const varobjSplitsemi = obj.split(';');
                if (varobjSplitsemi.length > 0) {
                    this.requesttemplateid = JSON.parse(varobjSplitsemi[0]);
                    if (this.requesttemplateid > 0) {
                        if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'View') {
                            this.disablefields = true;
                            this.isSaveHide = true;
                            this.getRequestTemplateDetailsId(this.requesttemplateid);
                        } else if (varobjSplitsemi.length > 1 && varobjSplitsemi[1] === 'Edit') {
                            this.disablefields = false;
                            this.isSaveHide = false;
                            this.getRequestTemplateDetailsId(this.requesttemplateid);
                        }
                    } else {
                        this.disablefields = false;
                        this.getRequestTemplateDetailsByLookupNames();

                    }

                }
            }
        }
        this.requestTemplateForm = this.formBuilder.group({
            RequestName: ['', Validators.required],
            RequestDescription: ['', Validators.required],
            RequestDisplayType: ['', Validators.required],
            visibility: ['', Validators.required],
            category: ['', Validators.required],
            priority: ['', Validators.required]
        },
        );

    }
    get f() { return this.requestTemplateForm.controls; }
    getRequestTemplateDetailsId(id) {
        let LookupNamesString = 'Request Priority,Display Type,Visibility,Controls';
        this.administrationservice.getRequestTemplateDetailsbylookup(LookupNamesString).subscribe(
            (data) => {

                this.displayType = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Display Type');
                this.Visibility = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Visibility');
                this.Priority = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Request Priority');
                this.category = data.designerCategories;
                this.Controls = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Controls');
                this.controlOpt();
                this.requestService.getRequestTemplateDetailsId(id).subscribe((data) => {
                    this.RequestObj = data;
                    if (data.templateDetailsList.length != 0) {
                        this.nestedList.dropzones[0] = data.templateDetailsList[0].templateControls;
                        this.previewList = this.nestedList.dropzones[0];
                    }

                    if (data != null && data != undefined) {
                        this.selectedDisplayOption = data.displayType;
                        this.selectedvisibilityOption = data.visibility;
                        this.selectedpriorityOption = data.priority
                        this.selectedCategoryOption = data.requestCategoryId
                    }
                })
            });

    }
    labelNameChange() {

        this.LabelValid = false;
    }

    requestNameChange(val: any) {
        if (val._pendingValue.trim() == "") {
            this.requestTemplateForm.controls.RequestName.setErrors({ required: true })
        }
    }
    requestDescriptionChange(val: any) {
        if (val._pendingValue.trim() == "") {
            this.requestTemplateForm.controls.RequestDescription.setErrors({ required: true })
        }
    }
    saveRequestTemplate() {
        
        if (this.requesttemplateid > 0) {
            this.saveControls();
        } else {
            this.saveControls        }
    }
    controlOpt() {
        this.controlOPtions = [];
        for (let i = 0; i < this.Controls.length; i++) {
            if (this.Controls[i].lookupDetailsValue == 'textarea' || this.Controls[i].lookupDetailsValue == 'textbox') {
                this.controlOPtions.push({ controlName: this.Controls[i].lookupDetailsDescription, type: this.Controls[i].lookupDetailsValue, controlType: this.Controls[i].lookupDetailsId, id: this.Controls[i].lookupDetailsSequenceOrder, label: 'Label', required: false, disable: false, errormsg: '' })
            } else if (this.Controls[i].lookupDetailsValue == 'label') {
                this.controlOPtions.push({ controlName: this.Controls[i].lookupDetailsDescription, type: this.Controls[i].lookupDetailsValue, controlType: this.Controls[i].lookupDetailsId, id: this.Controls[i].lookupDetailsSequenceOrder, label: 'Label' })
            } else if (this.Controls[i].lookupDetailsValue == 'checkbox' || this.Controls[i].lookupDetailsValue == 'radiobutton' || this.Controls[i].lookupDetailsValue == 'dropdown') {
                this.controlOPtions.push({ controlName: this.Controls[i].lookupDetailsDescription, type: this.Controls[i].lookupDetailsValue, controlType: this.Controls[i].lookupDetailsId, id: this.Controls[i].lookupDetailsSequenceOrder, label: 'Label', templateControlOptions: [] })
            } else if (this.Controls[i].lookupDetailsValue == 'layout') {
                this.controlOPtions.push({ controlName: this.Controls[i].lookupDetailsDescription, type: this.Controls[i].lookupDetailsValue, controlType: this.Controls[i].lookupDetailsId, id: this.Controls[i].lookupDetailsSequenceOrder, columns: [], column1: [] })
            } else if (this.Controls[i].lookupDetailsValue == 'template') {
                this.controlOPtions.push({ controlName: this.Controls[i].lookupDetailsDescription, type: this.Controls[i].lookupDetailsValue, controlType: this.Controls[i].lookupDetailsId, id: this.Controls[i].lookupDetailsSequenceOrder, columns: [] })
            }
        }
        this.nestedList.templates = this.controlOPtions;
    }

    saveControls() {
        this.RequestTemplateSectionControl = []
        let chkValid = false;
        for (let i = 0; i < this.previewList.length; i++) {
            if (this.previewList[i].templateControlOptions == undefined) {
                if (this.previewList[i].label.trim() == '') {
                    chkValid = true;
                    this.previewList[i].valid = true;
                } else {
                    this.previewList[i].valid = false;
                }
                let len = null;
                if (this.previewList[i].type != 'label') {
                    len = this.previewList[i].type == 'textarea' ? 1000 : 500;
                }
                this.RequestTemplateSectionControl.push({ RequestTemplateSectionControlId: 0, RequestTemplateSectionId: 0, ControlType: this.previewList[i].controlType, ControlLabel: this.previewList[i].label, SeqNo: i + 1, MaxLen: len, IsChecked: null, IsRequired: this.previewList[i].required, DisplayField: null, ValueField: null, SourceName: null, RowLength: null, RowStatus: 'A' });
            } else if (this.previewList[i].templateControlOptions != undefined) {
                const type = this.controlOPtions.find(tt => tt.type == 'label')['controlType']
                if (this.previewList[i].label.trim() == '' || this.previewList[i].templateControlOptions.length == 0) {
                    chkValid = true;
                    this.previewList[i].valid = true;
                } else {
                    this.previewList[i].valid = false;
                }
                const chkType = this.previewList[i].type == 'checkbox' ? false : null;
                this.RequestTemplateSectionControl.push({ RequestTemplateSectionControlId: 0, RequestTemplateSectionId: 0, ControlType: type, ControlLabel: this.previewList[i].label, SeqNo: i + 1, MaxLen: null, IsChecked: null, IsRequired: this.previewList[i].required, DisplayField: null, ValueField: null, SourceName: null, RowLength: null, RowStatus: 'A' });
                for (let j = 0; j < this.previewList[i].templateControlOptions.length; j++) {
                    this.RequestTemplateSectionControl.push({ RequestTemplateSectionControlId: 0, RequestTemplateSectionId: 0, ControlType: this.previewList[i].controlType, ControlLabel: this.previewList[i].templateControlOptions[j].label, SeqNo: i + 1, MaxLen: null, IsChecked: chkType, IsRequired: false, DisplayField: null, ValueField: null, SourceName: null, RowLength: null, RowStatus: 'A' });
                }
            }
        }
        if (!chkValid) {
            this.confirmationDialogService.confirm(this.appConstants.COMM_ALERT_SAVE_Alert_Header,
                this.appConstants.COMM_ALERT_SAVE_RECORD)
                .then((ConfirmTrue) => {

                    let saveObj = {
                        RequestTemplateId: this.requesttemplateid,
                        RequestDescription: this.RequestObj.requestDescription,
                        RequestName: this.RequestObj.requestName,
                        DisplayType: this.selectedDisplayOption,
                        RequestCategoryId: this.selectedCategoryOption,
                        UserId: this.currentUser.userId,
                        Visibility: this.selectedvisibilityOption,
                        Priority: this.selectedpriorityOption,
                        RowStatus: 'A',
                        requestTemplateSection: [],
                        requestTemplateSectionControl: this.RequestTemplateSectionControl
                    };
                    this.saveUpdateTemplateDetails(saveObj);


                }, (ConfirmFalse) => { })
                .catch((ConfirmFalse) => { });
           

        }
    }
    saveUpdateTemplateDetails(reqObj) {
        this.requestService.saveTemplateDetails(reqObj).subscribe(result => {
            const res = result;
            if (res) {
                this.snackBar.open(this.appConstants.COMM_REQUEST_TEMPLATE_CREATE, '', {
                    duration: 2000,
                    verticalPosition: 'top',
                    horizontalPosition: 'end',
                    panelClass: ['green-snackbar']

                });
            }
            this.router.navigate(['designer/requesttemplate']);
        });
    }

    closeRequestTemplate() {
        this.router.navigate(['designer/requesttemplate']);
    }
    getRequestTemplateDetailsByLookupNames() {
        let LookupNamesString = 'Request Priority,Display Type,Visibility,Controls';
        this.administrationservice.getRequestTemplateDetailsbylookup(LookupNamesString).subscribe(
            (data) => {
                this.displayType = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Display Type');
                this.Visibility = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Visibility');
                this.Priority = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Request Priority');
                this.category = data.designerCategories;
                this.Controls = data.lookupDetailsWithLookupNames.filter(tt => tt.lookupName == 'Controls');
                this.controlOpt();
            });
    }
    style: any;

    previewList = [];



    dragArea() {
        let elmnt = document.getElementById("cstmDiv");
        let height = elmnt.offsetHeight + 500;
        this.style = document.createElement('style');
        this.style.type = 'text/css';
        this.style.innerHTML = '.cstmDivs { min-height:' + height + 'px!important}';
        document.getElementsByTagName('head')[0].appendChild(this.style);
        document.getElementById('cstmDiv').className = 'cstmDivs';
    }
    mouseupevent() {
        let elmnt = document.getElementById("cstDivs");
        if (elmnt != null) {
            let height = elmnt.offsetHeight;
            this.style = document.createElement('style');
            this.style.type = 'text/css';
            this.style.innerHTML = '.cstmDivs { min-height:' + height + 'px!important;}';
            document.getElementsByTagName('head')[0].appendChild(this.style);
            document.getElementById('cstmDiv').className = 'cstmDivs';
        } else {
            this.style = document.createElement('style');
            this.style.type = 'text/css';
            this.style.innerHTML = '.cstmDivs { min-height:' + 0 + 'px!important;}';
            document.getElementsByTagName('head')[0].appendChild(this.style);
            document.getElementById('cstmDiv').className = 'cstmDivs';
        }
    }


    dnbCopied(id: any, type: any) {
        if (type == 'template') {
            const dialogRef = this.dialog.open(LayoutpopupComponent, {
                width: '800px',
            });
            dialogRef.afterClosed()
                .subscribe(response => {
                    if (response != null && response != undefined) {
                        if (response.template == 'remove') {
                            this.nestedList.dropzones[0] = [];
                            this.nestedList.dropzones[0].push(response);
                            this.previewList = this.nestedList.dropzones[0];
                            console.log(this.previewList)
                        } else {
                            id = id - 1;
                            this.nestedList.dropzones[0].find(tt => tt.id == id && tt.type == type)['columns'] = response.columns;
                            this.previewList = this.nestedList.dropzones[0];
                            console.log(this.previewList)
                        }
                    } else {
                        id = id - 1;
                        const index = this.nestedList.dropzones[0].findIndex(tt => tt.id == id && tt.type == type);
                        this.nestedList.dropzones[0].splice(index, 1);
                        this.previewList = this.nestedList.dropzones[0];
                        console.log(this.previewList)
                    }
                });
        } else {
            this.previewList = this.nestedList.dropzones[0];
            console.log(this.previewList)
        }

        let elmnt = document.getElementById("cstDivs");
        if (elmnt != null) {
            let height = elmnt.offsetHeight;
            this.style = document.createElement('style');
            this.style.type = 'text/css';
            this.style.innerHTML = '.cstmDivs { min-height:' + height + 'px!important}';
            document.getElementsByTagName('head')[0].appendChild(this.style);
            document.getElementById('cstmDiv').className = 'cstmDivs';
        } else {
            this.style = document.createElement('style');
            this.style.type = 'text/css';
            this.style.innerHTML = '.cstmDivs { min-height:' + 0 + 'px!important}';
            document.getElementsByTagName('head')[0].appendChild(this.style);
            document.getElementById('cstmDiv').className = 'cstmDivs';
        }



    }
    rowDelete(item: any) {
        let index = this.nestedList.dropzones[0].indexOf(item);
        // let peviindex = this.previewList.indexOf(item);

        this.nestedList.dropzones[0].splice(index, 1);
        //this.previewList.splice(peviindex, 1);
    }


    public removeItem(item: any, list: any[]): void {
        list.splice(list.indexOf(item), 1);
    }

}
