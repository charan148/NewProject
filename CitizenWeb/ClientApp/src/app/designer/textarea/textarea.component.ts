import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-textarea',
    templateUrl: './textarea.component.html',
    styleUrls: ['./textarea.component.css']
})
export class TextareaComponent implements OnInit {
    @Input() item: any;
    @Input() chkpre: any;
 
    constructor() { }

    ngOnInit() {
       
    }
    checkChange(item: any, event: any) {
        item.required = event.checked;

    }
}
