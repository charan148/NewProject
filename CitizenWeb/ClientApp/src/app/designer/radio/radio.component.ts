import { Component, OnInit, Input } from '@angular/core';
import { AddoptionpopupComponent } from '../addoptionpopup/addoptionpopup.component'
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'app-radio',
  templateUrl: './radio.component.html',
  styleUrls: ['./radio.component.scss']
})
export class RadioComponent implements OnInit {
    @Input() item: any;
    @Input() chkpre: any;
    constructor(private dialog: MatDialog) { }

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
                } else {

                }

            });
    }
}
