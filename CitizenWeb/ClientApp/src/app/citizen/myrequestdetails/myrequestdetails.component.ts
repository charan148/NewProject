import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { AppConstants } from '../../app.constants';
import { MatDialog } from '@angular/material/dialog';
import { AlertComponent } from '../../custom_alerts/alert/alert.component';
import { RequestFormService } from '../../services/requestform.service';



@Component({
    selector: 'myrequestdetails',
    templateUrl: './myrequestdetails.component.html',
    styleUrls: ['./myrequestdetails.component.scss'],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None
})
export class MyRequestDetailsComponent implements OnInit {

    requestdetails: any = {};
    isShow: boolean;
    isShowYard: boolean;
    discription = '';
    dynamicJsonFields = [];
    reportName: string = '';
    images: any = [];
    public form: FormGroup;
    unsubcribe: any;
    requestId: any;
    reqImages: any = [];
    constructor(private appConstants: AppConstants,
        private requestService: RequestFormService,
        public dialog: MatDialog) {

    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */

    public garbaefields: any[] = [
        {
            type: 'textarea',
            name: 'description',
            label: 'Description',
            value: 'value',
            required: false,
            disable:true,
            input: this.inputChange.bind(this)

        },
        {
            type: 'checkbox',
            name: 'notpicked',
            label: 'What was not picked up?',
            required: false,
            disable: true,
            options: [
                { value: true, key: 'garbage', label: 'Garbage' },
                { value: false, key: 'recycling', label: 'Recycling' },
                { value: true, key: 'yardWaste', label: 'Yard Waste' }
            ]
        }]

    public dumpsterfields: any[] = [
        {
            type: 'textarea',
            name: 'description',
            label: 'Description',
            value: 'value',
            required: false,
            input: this.inputChange.bind(this)

        },
        {
            type: 'checkbox',
            name: 'notpicked',
            label: 'What was not picked up?',
            required: false,
            options: [
                { value: false, key: 'garbage', label: 'Garbage' },
                { value: false, key: 'recycling', label: 'Recycling' }
            ]
        },
        {
            type: 'textarea',
            name: 'dumpster number',
            label: 'dumpster number',
            value: 'value',
            required: false,
            input: this.inputChange.bind(this)

        }]

    ngOnInit(): void {
        this.requestId = JSON.parse(sessionStorage.getItem('ID'));
        this.requestService.getMyRequestDetailsByTransactionId(this.requestId).subscribe((data) => {
            this.requestdetails = data;
            this.reqImages = data.requestPhotos;
            if (this.reqImages != null && this.reqImages != undefined && this.reqImages != '') {
                this.images = this.reqImages;
            }

            this.dynamicJsonFields = this.requestdetails.myRequestDetailControls; 
           
    })




        //this.images = [
        //    { image: './assets/images/garbage.jpg' },
        //    { image: './assets/images/recycling.jpg' },
        //    { image: './assets/images/yardwaste.jpg' }

        //]

       // let id = sessionStorage.getItem("ID");
       // const obj = JSON.parse(sessionStorage.getItem('RequestPageAction'));

        //if (id == 'GB-03') {
        //    this.dynamicJsonFields = this.dumpsterfields;
        //} else {
        //    this.dynamicJsonFields = this.garbaefields;
        //}

        this.form = new FormGroup({            
        fields: new FormControl(JSON.stringify(this.dynamicJsonFields))
    })

this.unsubcribe = this.form.valueChanges.subscribe((update) => {
    this.dynamicJsonFields = JSON.parse(update.fields);
});
//if (id == 'GB-03') {
//    this.isShow = true;
//    this.isShowYard = false;
//} else {
//    this.isShow = true;
//    this.isShowYard = true;
//}

let fieldsCtrls = {};
for (let f of this.dynamicJsonFields) {
    if (f.type != 'checkbox') {
        fieldsCtrls[f.name] = new FormControl()
    } else {
        let opts = {};
        for (let opt of f.options) {
            opts[opt.key] = new FormControl(opt.value);
        }
        fieldsCtrls[f.name] = new FormGroup(opts)
    }
}
this.form = new FormGroup(fieldsCtrls);
    }
SubmitClick() {
    this.showError(this.appConstants.COMM_ALERT_SUBMIT);
}
// alert message
showError(error: string): void {
    this.dialog.open(AlertComponent, {
        disableClose: true,
        data: { errorMsg: error },
        width: '400px',
        height: 'auto',
        panelClass: 'alerrtpop-center'
    });
}
confirmDetails(details: any) {

    this.discription = details.description;
}
getFields() {
    return this.garbaefields;
}
inputChange(event) {
    this.confirmDetails(event.value);
}
listItems = [];
checkValue(key, event, label, name) {
    const value = event.value[name][key];

    if (value) {
        this.listItems.push({ key: key, label: label });
    } else {
        const index = this.listItems.findIndex(tt => tt.key == key);
        this.listItems.splice(index, 1);
    }
}
}


