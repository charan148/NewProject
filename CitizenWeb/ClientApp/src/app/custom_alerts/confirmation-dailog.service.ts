import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationDailogComponent } from 'app/custom_alerts/confirmation-dailog/confirmation-dailog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationDailogService {

    constructor(private modalService: NgbModal) { }
    public confirm(
        title: string,
        message: string,
        btnOkText: string = 'Yes',
        btnCancelText: string = 'Cancel',
        dialogSize: 'sm' | 'lg' = 'lg'): Promise<boolean> {
        const modalRef = this.modalService.open(ConfirmationDailogComponent, { centered: true });
        modalRef.componentInstance.title = title;
        modalRef.componentInstance.message = message;
        modalRef.componentInstance.btnOkText = btnOkText;
        modalRef.componentInstance.btnCancelText = btnCancelText;

        return modalRef.result;
    }
}
