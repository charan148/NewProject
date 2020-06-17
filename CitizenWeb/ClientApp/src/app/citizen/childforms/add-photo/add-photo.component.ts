import { Component, OnDestroy, OnInit, ViewChild, ElementRef, ViewEncapsulation, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { FuseConfigService } from '@fuse/services/config.service'; 
import { takeUntil } from 'rxjs/operators';
import { MatCarousel, MatCarouselComponent } from '@ngmodule/material-carousel';
import { MatSnackBar } from '@angular/material/snack-bar';

import { MatDialog } from '@angular/material/dialog';
import { AlertComponent } from '../../../custom_alerts/alert/alert.component';
import { AppConstants} from '../../../app.constants';

@Component({
  selector: 'app-add-photo',
  templateUrl: './add-photo.component.html',
  styleUrls: ['./add-photo.component.scss']
})
export class AddPhotoComponent implements OnInit {

    @ViewChild('browse', { static: false }) private browseref: ElementRef;
    @ViewChild('search', { static: true }) public searchElementRef: ElementRef;
    @ViewChild('scroll', { static: true }) private scrollLast: ElementRef;
    @Output() addClick = new EventEmitter();
    @Output() removeClick = new EventEmitter();

    private _unsubscribeAll: Subject<any>;
    isUpload: boolean = false;
    DownUpload: boolean = true;
    fileData: any = [];

    urls: any = [];
   
    constructor(
        private _fuseConfigService: FuseConfigService,
	 private appConstants: AppConstants,
        public dialog: MatDialog
    ) {
        this._unsubscribeAll = new Subject();
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: false
                },
                toolbar: {
                    hidden: false
                },
                footer: {
                    hidden: false
                },
                sidepanel: {
                    hidden: false
                }
            }
        };
    }

    ngOnInit(): void {
        this.isUpload = false;
        this.DownUpload = true;

    }
    uploadClick() {
        this.browseref.nativeElement.value = "";
    }
    fileProgress(fileInput: any) {
        this.fileData = <File>fileInput.target.files;
        let count = this.urls.length;
        count = count + this.fileData.length;
        if (count <= 3) {
            this.preview();
        } else {
            this.showError(this.appConstants.COMM_ALERT_SELECT_IMAGE);
            
            return;
        }
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
    preview() {
        // Show preview
        let files = this.fileData;
        if (files) {
            for (let file of files) {
                let reader = new FileReader();
                reader.onload = (e: any) => {
                    this.urls.push({ ImageUrl: reader.result, ImageName: file.name });
                    //this.urls.push( reader.result);
                    this.addClick.emit(this.urls);
                   
                    if (this.urls.length == 3) {
                        this.isUpload = false;
                        this.DownUpload = false;
                    }
                }
                reader.readAsDataURL(file);
            }

        }
       

    }
    removeImage(item) {
        let index = this.urls.indexOf(item);
        this.urls.splice(index, 1);
        this.removeClick.emit(this.urls);
        
        if (this.urls.length < 3) {
            this.isUpload = false;
            this.DownUpload = true;

        }
        this.browseref.nativeElement.value = "";
    }
}
