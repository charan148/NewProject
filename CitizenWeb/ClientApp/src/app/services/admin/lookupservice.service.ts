import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class LookupService {
    _referenceData = new BehaviorSubject<any>(null);

    constructor(private _http: HttpClient) { }
    private _serverError(err: any): Observable<any> {
        return throwError(err || 'backend server error');
    }

    GetRequestFormTemplateByCategoryIDAndTemplateID(RequestCategoryID: any, RequestTemplateID: any): Observable<any> {
        return this._http.get('api/RequestFormController/GetRequestFormTemplateByCategoryIDAndTemplateID/' + RequestCategoryID + '/' + RequestTemplateID)
            .pipe(map(response => response), catchError(this._serverError));
    }
}
