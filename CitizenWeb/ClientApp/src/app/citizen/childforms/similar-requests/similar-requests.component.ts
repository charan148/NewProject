import { Component, OnDestroy, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { CitizenService } from 'app/citizen/citizen.service';
import { RequestTransactionService } from 'app/services/request-transaction.service';

@Component({
    selector: 'similar-requests',
    templateUrl: './similar-requests.component.html',
    styleUrls: ['./similar-requests.component.scss'],
    animations: fuseAnimations
})
export class SimilarRequestsComponent implements OnInit, OnDestroy {
    @Input() similarRequestsList: any;
    @Input() reportName: any;
    
    
    similarReq: any = [];
    // Private
    private _unsubscribeAll: Subject<any>;
    @Output() followClick = new EventEmitter();
    p: number = 1;
    constructor(
        private _citizenService: CitizenService,
        private requestTransactionService: RequestTransactionService
    ) {
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        
        
    }

    follow(requestTransactionId) {
        this.similarReq = this.similarRequestsList.filter(tt => tt.requestTransactionId == requestTransactionId);
        this.followClick.emit(this.similarReq[0]);
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
