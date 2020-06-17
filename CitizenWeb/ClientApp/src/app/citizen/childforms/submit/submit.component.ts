import { Component, OnDestroy, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-submit',
    templateUrl: './submit.component.html',
    styleUrls: ['./submit.component.scss']
})
export class SubmitComponent implements OnInit, OnDestroy {
    tracktepperhead = 'Submit';
    register = true;
    fallowbtn: boolean;
    btnName = 'Continue';

    submitself = 'Submit as yourself';
    submitAnony = 'Submit Anonymously';
    isHideTab: boolean;
    isloggedIn: any;
    private _unsubscribeAll: Subject<any>;
    @Output() radioClick: EventEmitter<string> = new EventEmitter<string>();
    @Input() category: any;
    @Input() lookups: any;
    @Input() visibility: any;
    SubmitLookupList: any = [];
    TrackLookupList: any = [];
    LookupList: any = [];

    constructor() {
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this.SubmitLookupList = this.lookups.filter(tt => tt.lookupName == 'Submit');
        this.TrackLookupList = this.lookups.filter(tt => tt.lookupName == 'Track');
        this.LookupList = this.SubmitLookupList;
        let lkpdet: any = { lookupDetailsId: this.LookupList[0].lookupDetailsId, lookupDetailsDescription: this.LookupList[0].lookupDetailsDescription };
        this.radioClick.emit(lkpdet);
    }
    setradio(lookupDetailsDescription: any, lookupDetailsId: any) {
        let lkpdet:any = { lookupDetailsId: lookupDetailsId, lookupDetailsDescription: lookupDetailsDescription };
        this.radioClick.emit(lkpdet);
    }
    render(val: any) {
        this.tracktepperhead = val;
        if (val == 'Track') {
            this.LookupList = this.TrackLookupList;
            //this.submitself = 'Track as yourself';
            //this.submitAnony = 'Track Anonymously';

        } else {
            //this.submitself = 'Submit as yourself';
            //this.submitAnony = 'Submit Anonymously';
            this.LookupList = this.SubmitLookupList;
        }
        let lkpdet: any = { lookupDetailsId: this.LookupList[0].lookupDetailsId, lookupDetailsDescription: this.LookupList[0].lookupDetailsDescription };
        this.radioClick.emit(lkpdet);
    }
    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
