import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RequestTransactionService {
    _referenceData = new BehaviorSubject<any>(null);

    constructor(private _http: HttpClient) { }
    private _serverError(err: any): Observable<any> {
        return throwError(err || 'backend server error');
    }

    SubmitOrTrackRequest(RequestTransaction: any): Observable<any> {
        return this._http.post('api/RequestTransactionController/SubmitOrTrackRequest', RequestTransaction)
            .pipe(map(response => response), catchError(this._serverError));
    }

    TrackRequestTransaction(trackReqTransaction: any): Observable<any> {
        return this._http.post('api/RequestTransactionController/TrackRequestTransaction', trackReqTransaction)
            .pipe(map(response => response), catchError(this._serverError));
    }

    GetSimilarRequests(Latitude: any, Longitude: any, RequestTemplateID: any): Observable<any> {
        return this._http.get('api/RequestTransactionController/GetSimilarRequests/' + Latitude + '/' + Longitude + '/' + RequestTemplateID)
            .pipe(map(response => response), catchError(this._serverError));
    }
}
