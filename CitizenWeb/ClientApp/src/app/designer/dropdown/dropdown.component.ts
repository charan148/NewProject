import { Component, OnInit, Input } from '@angular/core';
import { AddoptionpopupComponent } from '../addoptionpopup/addoptionpopup.component'
import { MatDialog } from '@angular/material/dialog';
@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.css']
})
export class DropdownComponent implements OnInit {
    @Input() item: any;
    @Input() chkpre: any;
    constructor(private dialog: MatDialog) { }
    selectedOption: any;
    ngOnInit() {
    }
    checkChange(item: any, event: any) {
        item.required = event.checked;

    }
    addOptions(item: any) {
        const dialogRef = this.dialog.open(AddoptionpopupComponent, {
            width: '800px',
        });
        dialogRef.afterClosed()
            .subscribe(response => {
                if (response != null && response != undefined) {
                    item.templateControlOptions = response;
                    this.selectedOption = item.templateControlOptions[0].label;
                } else {

                }

            });
    }
}
