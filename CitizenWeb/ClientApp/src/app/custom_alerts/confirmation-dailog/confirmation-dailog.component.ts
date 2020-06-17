import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-confirmation-dailog',
  templateUrl: './confirmation-dailog.component.html',
  styleUrls: ['./confirmation-dailog.component.scss']
})
export class ConfirmationDailogComponent implements OnInit {
  @Input() title: string;
  @Input() message: string;
  @Input() btnOkText: string;
  @Input() btnCancelText: string;
  nameOfSheet: any;
  actionType: any;
  actionNameTitle = '';
  buttonName = '';
  buttonTitle = '';
  ButtonName = '';
  constructor(private activeModal: NgbActiveModal, private router: Router) { }

  ngOnInit() {
    this.actionType = (localStorage.getItem('actionType'));
    this.ButtonName = sessionStorage.getItem('ActionName');
    if (this.actionType !== null) {
      if (this.actionType == 'HoursUnSwap') {
        this.actionNameTitle = 'Un-Swap Hours';
        this.buttonName = 'Un-Swap';
      } else if (this.actionType == 'UnitsUnSwap') {
        this.actionNameTitle = 'Un-Swap Units';
        this.buttonName = 'Un-Swap';
      } else if (this.actionType == 'FTEUnitsSave') {
        this.actionNameTitle = 'FTE Units Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'FTEUnitsNegotiate') {
        this.actionNameTitle = 'FTE Units Negotiate';
        this.buttonName = 'Negotiate';
      } else if (this.actionType == 'FTEHoursNegotiate') {
        this.actionNameTitle = 'FTE Hours Negotiate';
        this.buttonName = 'Negotiate';
      } else if (this.actionType == 'ConHoursNegotiate') {
        this.actionNameTitle = 'Consultant Hours Negotiate';
        this.buttonName = 'Negotiate';
      } else if (this.actionType == 'ConUnitsNegotiate') {
        this.actionNameTitle = 'Consultant Units Negotiate';
        this.buttonName = 'Negotiate';
      } else if (this.actionType == 'UnitsUnSubmit') {
        this.actionNameTitle = 'Un-Submit Units';
        this.buttonName = 'Un-Submit';
      } else if (this.actionType == 'HoursUnSubmit') {
        this.actionNameTitle = 'Un-Submit Hours';
        this.buttonName = 'Un-Submit';
      } else if (this.actionType == 'FTEHoursSave') {
        this.actionNameTitle = 'FTE Hours Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'FTEUnitsNegotiateSave') {
        this.actionNameTitle = 'FTE Units Negotiation Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'ConUnitsNegotiateSave') {
        this.actionNameTitle = 'Consultant Units Negotiation Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'ConHoursNegotiateSave') {
        this.actionNameTitle = 'Consultant Hours Negotiation Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'FTEHoursNegotiateSave') {
        this.actionNameTitle = 'FTE Hours Negotiation Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'ConHoursSave') {
        this.actionNameTitle = 'Consultant Hours Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'ConUnitsSave') {
        this.actionNameTitle = 'Consultant Units Save';
        this.buttonName = 'Save';
      } else if (this.actionType == 'HoursSwap') {
        this.actionNameTitle = 'Swap Hours';
        this.buttonName = 'Swap';
      } else if (this.actionType == 'UnitsSwap') {
        this.actionNameTitle = 'Swap Units';
        this.buttonName = 'Swap';
      } else if (this.actionType == 'FTEHoursSubmit') {
        this.actionNameTitle = 'FTE Hours Submit ';
        this.buttonName = 'Submit';
      } else if (this.actionType == 'FTEUnitsSubmit') {
        this.actionNameTitle = 'FTE Units Submit';
        this.buttonName = 'Submit';
      } else if (this.actionType = 'ConUnitsSubmit') {
        this.actionNameTitle = 'Consultant Units Submit';
        this.buttonName = 'Submit';
      } else if (this.actionType = 'ConHoursSubmit') {
        this.actionNameTitle = 'Consultant Hours Submit';
        this.buttonName = 'Submit';
      } else if (this.actionType = 'FTEUnitsNegotiateSubmit') {
        this.actionNameTitle = 'FTE Units Submit';
        this.buttonName = 'Submit';
      } else if (this.actionType = 'ConUnitsNegotiateSubmit') {
        this.actionNameTitle = 'Consultant Units Submit';
        this.buttonName = 'Submit';
      } else if (this.actionType = 'FTEHoursNegotiateSubmit') {
        this.actionNameTitle = 'FTE Hours Submit';
        this.buttonName = 'Submit';
      } else if (this.actionType = 'ConHoursNegotiateSubmit') {
        this.actionNameTitle = 'Consultant Hours Submit';
        this.buttonName = 'Submit';
      }
    }
    this.nameOfSheet = localStorage.getItem('DSsheetname');
    if (this.router.url == '/projects/latestProject/ftedesignview') {
      if (this.buttonName != '') {
        this.buttonTitle = this.buttonName;
        this.actionNameTitle = this.nameOfSheet;
        this.btnCancelText = 'Cancel';
      } else {
        this.buttonTitle = 'OK';
        this.actionNameTitle = this.nameOfSheet;
        this.btnCancelText = 'Cancel';
      }

      if (this.ButtonName == 'true') {
        this.buttonTitle = 'Ok';
        this.actionNameTitle = this.nameOfSheet;
        this.btnCancelText = 'Cancel';
      }
    } else {
      this.actionNameTitle = '';
      this.buttonTitle = 'Ok';
    }

  }

  public decline() {
    this.activeModal.dismiss();
  }

  public accept() {
    this.activeModal.close(true);
  }

  public dismiss() {
    this.activeModal.dismiss();
  }
}
