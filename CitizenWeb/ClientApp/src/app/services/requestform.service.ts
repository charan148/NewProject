import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class RequestFormService {
    _referenceData = new BehaviorSubject<any>(null);

    constructor(private _http: HttpClient) { }
    private _serverError(err: any): Observable<any> {
        return throwError(err || 'backend server error');
    }

    GetRequestFormTemplateByCategoryIDAndTemplateID(RequestCategoryID: any, RequestTemplateID: any): Observable<any> {
        return this._http.get('api/RequestFormController/GetRequestFormTemplateByCategoryIDAndTemplateID/' + RequestCategoryID + '/' + RequestTemplateID)
            .pipe(map(response => response), catchError(this._serverError));
    }

    // get users by id
    getRequestbyUserId(Id: any): Observable<any> {
        const body = 'userId=' + Id;
        return this._http.get<any>('api/RequestTransactionController/GetRequestsByUserId/' + Id)
            .pipe(map(response => response), catchError(this._serverError));
    }

    getMyRequestDetailsByTransactionId(Id: any): Observable<any> {
        return this._http.get<any>('api/RequestTransactionController/GetMyRequestDetailsByTransactionId/' + Id)
            .pipe(map(response => response), catchError(this._serverError));
    }
    getRequestTemplateDetailsId(id): Observable<any> {
        return this._http.get('api/RequestTemplateController/GetRequestTemplateId/' + id)
            .pipe(map(response => response), catchError(this._serverError));
    }
    saveTemplateDetails(reqFofrmObj) {
        return this._http.post('api/RequestFormController/SaveRequestFormTemplate', reqFofrmObj)
            .pipe(map(response => response), catchError(this._serverError));
    }
}
