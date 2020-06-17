import { Component, Input, ViewChild, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';


@Component({
    selector: 'app-textarea',
    templateUrl: './textarea.component.html',
    styleUrls: ['./textarea.component.scss']
})

export class TextareaComponent implements OnInit {

    @Input() field: any = {};
    @Input() form: FormGroup;
    @Input() chkpages: any;

    get isValid() { return this.form.controls[this.field.name].valid; }
    get isDirty() { return this.form.controls[this.field.name].dirty; }
    constructor() { }
    ngOnInit(): void {

    }
    inputChange() {

    }

}
