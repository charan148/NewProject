import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class AuthenticationService implements Resolve<any>
{
    onCategoriesChanged: BehaviorSubject<any>;
    onCoursesChanged: BehaviorSubject<any>;
    userAuthenticate: any = {};

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(
        private _httpClient: HttpClient
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

    //_getUserBasicDetails(UName: string, PWD: string): Observable<any> {
    //    return this._httpClient.get<any>('api/UserController/Authenticate/' + UName + '/' + PWD)
    //        .pipe(map(response => response));
    //}

    _login(_email: string, _password: string): Observable<any> {
        this.userAuthenticate.EmailAddress = _email;
        this.userAuthenticate.Password = encodeURIComponent(_password);

        return this._httpClient.post<any>('api/UserController/Authenticate', this.userAuthenticate)
            .pipe(map(response => response));
            //.pipe(map(data => {
            //    // login successful if there's a jwt token in the response
            //    //if (data && data.token) {
            //    //    // store user details and jwt token in local storage to keep user logged in between page refreshes
            //    //    localStorage.setItem('currentUser', JSON.stringify(data));

            //    //    this.currentUserSubject.next(data);
            //    //}

            //    return data;
            //}));
    }

    saveDetails(objUser): Observable<any> {
        return this._httpClient.post('api/UserController/userInsert', objUser)
            .pipe(map(response => response), catchError(this._serverError));
    }

    GetAllUsers(): Observable<any> {
        return this._httpClient.get('api/UserController/fetchAllUsers')
            .pipe(map(response => response), catchError(this._serverError));
    }

    isMailExist(email): Observable<any> {
        return this._httpClient.post('api/UserController/CheckEmailExistsOrNot', email)
            .pipe(map(response => response), catchError(this._serverError));
    }
    _getUserRoleById(userid: any): Observable<any> {
        return this._httpClient.get('api/SecurityController/GetUserRoles/' + userid)
            .pipe(map(response => response));
    }
    _rolePriviligeSelect(roleIds: any): Observable<any> {
        return this._httpClient.get('api/SecurityController/GetAllRolePrivilegesByRoleID/' + roleIds)
            .pipe(map(response => response));
    }

}
