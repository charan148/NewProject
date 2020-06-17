import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';;
import { FormGroup } from '@angular/forms';


@Component({
    selector: 'app-checkbox',
    templateUrl: './checkbox.component.html',
    styleUrls: ['./checkbox.component.scss']
})
export class CheckboxComponent {

    @Input() field: any = {};
    @Input() form: FormGroup;
    @Input() chkpages: any;
    get isValid() { return this.form.controls[this.field.name].valid; }
    get isDirty() { return this.form.controls[this.field.name].dirty; }

}
