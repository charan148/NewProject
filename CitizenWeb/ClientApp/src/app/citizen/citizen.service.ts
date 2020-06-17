import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
@Injectable()
export class CitizenService implements Resolve<any>
{
    onCategoriesChanged: BehaviorSubject<any>;
    onCoursesChanged: BehaviorSubject<any>;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(

        private _httpClient: HttpClient,

    )
    {
        // Set the defaults
        this.onCategoriesChanged = new BehaviorSubject({});
        this.onCoursesChanged = new BehaviorSubject({});
    }
    private _serverError(err: any): Observable<any> {
        return throwError(err || 'backend server error');
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any
    {
        return new Promise((resolve, reject) => {

            Promise.all([
                //this.getCategories(),
                //this.getCourses()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    /**
     * Get categories
     *
     * @returns {Promise<any>}
     */
    //getCategories(): Promise<any>
    //{
    //    //return new Promise((resolve, reject) => {
    //    //    this._httpClient.get('api/academy-categories')
    //    //        .subscribe((response: any) => {
    //    //            this.onCategoriesChanged.next(response);
    //    //            resolve(response);
    //    //        }, reject);
    //    //});
    //}

    /**
     * Get courses
     *
     * @returns {Promise<any>}
     */
    //getCourses(): Promise<any>
    //{
    //    return new Promise((resolve, reject) => {
    //        this._httpClient.get('api/academy-courses')
    //            .subscribe((response: any) => {
    //                this.onCoursesChanged.next(response);
    //                resolve(response);
    //            }, reject);
    //    });
    //}


    // get users by id
    getRequestbyUserId(Id: any): Observable<any> {
        const body = 'userId=' + Id;
        return this._httpClient.get<any>('api/RequestTransactionController/GetRequestsByUserId/' + Id)
            .pipe(map(response => response), catchError(this._serverError));
    }

}
