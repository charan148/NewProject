import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DesignerComponent } from '../designer/designer.component'
@Component({
    selector: 'app-layoutpopup',
    templateUrl: './layoutpopup.component.html',
    styleUrls: ['./layoutpopup.component.scss']
})
export class LayoutpopupComponent implements OnInit {

    constructor(public dialog: MatDialogRef<DesignerComponent>) { }
    public detailsForm: FormGroup;
    Layouts = [];
    modifyTem: boolean;
    public garbaefields: any[] = [
        {
            type: 'textarea',
            name: 'description',
            label: 'Description (Optional; The Content will be displayed publicly)',
            value: 'Label',
            required: false,


        },
        {
            type: 'checkbox',
            name: 'notpicked',
            label: 'What was not picked up? (Required; The Content will not be displayed publicly)',
            required: true,
            value: 'Label',
            templateControlOptions: [
                { label: 'Garbage', key: 'garbage', name: 'Garbage' },
                { label: 'Recycling', key: 'recycling', name: 'Recycling' },
                { label: 'Yard Waste', key: 'yardWaste', name: 'Yard Waste' }
            ]
        }, {
            type: 'label',
            name: 'span',
            label: 'Please leave your cart at the curb and we will pick up within two business days.If you submit this request over the weekend, we will process your request on Monday morning.',


        },




    ];


    public dumpsterfields: any[] = [
        {
            type: 'textarea',
            name: 'description',
            label: 'Description (Optional; The Content will be displayed publicly)',
            value: 'Label',
            required: false,


        },
        {
            type: 'checkbox',
            name: 'notpicked',
            label: 'What was not picked up? (Required; The Content will not be displayed publicly)',
            required: true,
            value: 'Label',
            templateControlOptions: [
                { label: 'Garbage', key: 'garbage', name: 'Garbage' },
                { label: 'Recycling', key: 'recycling', name: 'Recycling' },

            ]
        },
        {
            type: 'textbox',
            name: 'dumpster',
            label: 'What is the dumpster number? Example: 4-4057 (Required; The Content will not be displayed publicly)',
            value: 'Label',
            required: false,

        },
        {
            type: 'label',
            name: 'span',
            label: 'You will have 3 options to submit your request. We encourage you to submit your request by registering for a Simplifyi3<sup>&reg;</sup> Citizen account to receive email updates on your request.',


        },



    ];
    controls: any;
    ngOnInit(): void {

        this.Layouts = [{ imagesrc: './assets/images/layout1.png', checked: true, label: 'Layout 1' }, { imagesrc: './assets/images/layout2.png', checked: false, label: 'Layout 2' }];
        this.fields = this.garbaefields;

        //this.detailsForm = new FormGroup({
        //    fields: new FormControl(JSON.stringify(this.fields))
        //})
    }
    fields = [];
    form: FormGroup;
    layOutChange(label: any) {
        if (label == 'Layout 1') {
            this.fields = this.garbaefields;

            //this.detailsForm = new FormGroup({
            //    fields: new FormControl(JSON.stringify(this.fields))
            //})
        } else if (label == 'Layout 2') {
            this.fields = this.dumpsterfields;

            //this.detailsForm = new FormGroup({
            //    fields: new FormControl(JSON.stringify(this.fields))
            //})
        }
    }
    removeuseTemplate() {
        this.controls = { label: "Template", type: "template", template: 'remove', id: 1, columns: this.fields };
        this.dialog.close(this.controls);
    }
    appendTemplate() {
        this.controls = { label: "Template", type: "template", template: 'append', columns: this.fields };
        this.dialog.close(this.controls);
    }
    modifyTemplate() {
        this.modifyTem = true;
    }
}
