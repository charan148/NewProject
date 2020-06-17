import { Component, ViewChild, OnInit } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTabGroup } from '@angular/material/tabs';
import { AdministrationService } from '../../administration/administration.service';

@Component({
    selector: 'app-addoptionpopup',
    templateUrl: './addoptionpopup.component.html',
    styleUrls: ['./addoptionpopup.component.scss']
})
export class AddoptionpopupComponent implements OnInit {
    @ViewChild(MatTabGroup, { static: false }) tabGroup: MatTabGroup;
    constructor(public dialog: MatDialogRef<AddoptionpopupComponent>, private administrationservice: AdministrationService,
    ) { }
    DesignerLookup: any = {};
    selectedOption = 'garbage';
    options = [];
    // dropDownData = [{ name: 'Garbage', value: 'garbage' }, { name: 'Dumpster', value:'dumpster'},];
    dropDownData: any;
    //staticLookupdata = {
    //    garbage: [{ id: 1, name: 'option1', value: 'Option 1' }, { id: 2, name: 'option2', value: 'Option 2' }, { id: 3, name: 'option3', value: 'Option 3' }, { id: 4, name: 'option4', value: 'Option 4' }],
    //    dumpster: [{ id: 1, name: 'choice1', value: 'Choice 1' }, { id: 2, name: 'choice2', value: 'Choice 2' }, { id: 3, name: 'choice3', value: 'Choice 3' }, { id: 4, name: 'choice4', value: 'Choice 4' }]
    //}
    isloggedIn: any;
    index = 0;
    groupName: any;
    newLookup = [];
    distinctLookupName: boolean;
    
    ngOnInit() {
        // this.options = this.staticLookupdata[this.selectedOption];
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.lookupGroupName();
        this.newLookup.push({ id: this.index, LookupDetailsValue: '', LookupDetailsDescription: '', LookupDetailsSequenceOrder: 0 });
    }
    lookupGroupName() {
        this.administrationservice.GetAllLookups().subscribe(
            (data) => {
                this.dropDownData = data;
                this.selectedOption = data[0].lookupId;
                this.lookupDetailsByID();
            });
    }
    lookupDetailsByID() {
        this.administrationservice.GetLookupById(this.selectedOption).subscribe(data => {
            this.options = data.lookupDetails;
        });
    }
    lookupchange() {
        this.lookupDetailsByID();
    }
    
    addDetails() {
        this.index++;
        this.newLookup.push({ id: this.index, LookupDetailsValue: '', LookupDetailsDescription: '', LookupDetailsSequenceOrder:0 });
    }
    savelookupDetails() {
        let DesignerLookupDetails = [];
        this.DesignerLookup = { LookupName: this.DesignerLookup.LookupName, LookupDescription: this.DesignerLookup.LookupName, LookupShortDescription: this.DesignerLookup.LookupName, CreateUserId: this.isloggedIn.userId, designerLookupDetails: [] };        
        for (let i = 0; i < this.newLookup.length; i++) {
            if (this.newLookup[i].LookupDetailsValue != '') {
                this.newLookup[i].error = false;
                DesignerLookupDetails.push({ LookupDetailsValue: this.newLookup[i].LookupDetailsValue, LookupDetailsDescription: this.newLookup[i].LookupDetailsValue, LookupDetailsSequenceOrder: i + 1 });
            } else {
                this.newLookup[i].error = true;
            }            
        }
        const lengtherror = this.newLookup.filter(tt => tt.error == true);
        if (lengtherror.length == 0) {                    
            this.DesignerLookup.designerLookupDetails = DesignerLookupDetails;
            this.administrationservice.SaveDesignerLookupDetails(this.DesignerLookup).subscribe(data => {
                console.log(data);
                this.lookupGroupName();
                this.tabGroup.selectedIndex = 0;
                this.newLookup = [];
                this.DesignerLookup = {};
            });         
        }            
    }
    inputEvent(details) {
        if (details.LookupDetailsValue != '') {
            details.error = false;
        } else {
            details.error = true;
        }
    }
    lookupBlurEvent() {
        if (this.DesignerLookup.LookupName != '') {
            const distinctLookuName = this.dropDownData.filter(tt => tt.lookupName.toLowerCase() == this.DesignerLookup.LookupName.toLowerCase());
            if (distinctLookuName.length != 0) {
                this.distinctLookupName = true;
                this.DesignerLookup.LookupName = '';
            } else {
                this.distinctLookupName = false;
                
            }

        }
        
    }
    lookupsave() {

        for (let i = 0; i < this.options.length; i++) {
            this.options[i].label = this.options[i].lookupDetailsDescription;
        }
        this.dialog.close(this.options);
    }
    removeDetails(detail: any) {
        const indexof = this.newLookup.indexOf(detail);
        this.newLookup.splice(indexof, 1);
    }

}
