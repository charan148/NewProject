import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

    @Output() formSubmit = new EventEmitter();
    @Input() fields: any[] = [];
    form: FormGroup;
    constructor(   
    private snackBar: MatSnackBar
    ) { }

    ngOnInit() {
        let fieldsCtrls = {};
        for (let f of this.fields) {
            if (f.type != 'checkbox') {
                fieldsCtrls[f.name] = new FormControl()
            } else {
                let opts = {};
                for (let opt of f.templateControlOptions) {
                    opts[opt.key] = new FormControl(opt.value);
                }
                fieldsCtrls[f.name] = new FormGroup(opts)
            }
        }
        this.form = new FormGroup(fieldsCtrls);
    }
   onSubmit() {
        const requiredValues = this.fields.filter(tt => tt.required == true);
        for (let i = 0; i < requiredValues.length; i++) {
            if (requiredValues[i].type == 'textarea') {
                if (this.form.value[requiredValues[i].name] == undefined || this.form.value[requiredValues[i].name] == null || this.form.value[requiredValues[i].name] == '') {
                    // alert('textarea')
                }
            }
            if (requiredValues[i].type == 'checkbox') {
                let chkValidation = false;
                for (let k = 0; k < requiredValues[i].templateControlOptions.length; k++) {
                    if (this.form.value[requiredValues[i].name][requiredValues[i].templateControlOptions[k].key]) {
                        chkValidation = true;
                    }
                }
                if (!chkValidation) {
                    this.fields.find(tt => tt.requestTemplateSectionControlId == requiredValues[i].requestTemplateSectionControlId)['errormsg'] = true;
                }
                if (chkValidation) {
                    this.formSubmit.emit(this.form.value);
                }
            }
        }
       
      //  this.formSubmit.emit(this.form.value);
    }
   

   
}
