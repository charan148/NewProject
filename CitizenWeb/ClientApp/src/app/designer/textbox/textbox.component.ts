import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-textbox',
    templateUrl: './textbox.component.html',
    styleUrls: ['./textbox.component.css']
})
export class TextboxComponent implements OnInit {
    @Input() item: any;
    @Input() chkpre: any;
    constructor() { }

    ngOnInit() {
       
    }
    checkChange(item: any, event: any) {
        item.required = event.checked;
        
    }
}
