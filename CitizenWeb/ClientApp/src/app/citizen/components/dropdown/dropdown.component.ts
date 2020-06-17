import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';;
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent implements OnInit {
    @Input() field: any = {};
    @Input() form: FormGroup;
    @Input() chkpages: any;
    selectedValue :any;
    constructor() { }

    ngOnInit(): void {
        
    }
    

}
