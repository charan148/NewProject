import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
//import 'rxjs/Rx';

@Injectable()
export class AdministrationService {
    onCategoriesChanged: BehaviorSubject<any>;
    onCoursesChanged: BehaviorSubject<any>;
    constructor(private _httpClient: HttpClient) { }
    private _serverError(err: any): Observable<any> {
        return throwError(err || 'backend server error');
    }


    private listSoruce = new BehaviorSubject<any>(null);
    public detailsObj = this.listSoruce.asObservable();

    set list(v) { this.listSoruce.next(v) }
    value: string;

    public setList(value: string) {
        this.value = value;
    }
    public getList() {
        return this.value ;
    }

    // get all users
    GetAllUsers(): Observable<any> {
        // return this._http.get(this.appSettings.API_URL + '/UserController/fetchAllUsers');
        return this._httpClient.get('api/UserController/FetchAllUsers')
            .pipe(map(response => response), catchError(this._serverError));
    }
    // get users by id
    getUserDetails(Id: any): Observable<any> {
        const body = 'userId=' + Id;
        return this._httpClient.get<any>('api/UserController/FetchUserById/' + Id)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // save user details
    saveDetails(objUser): Observable<any> {
        return this._httpClient.post('api/UserController/userInsert', objUser)
            .pipe(map(response => response), catchError(this._serverError));
    }

    _getUserBasicDetails(_username: string): Observable<any> {
        const body = _username;
        return this._httpClient.get<any>('api/UserController/FetchUserByName/' + body)
            .pipe(map(response => response));
    }

    // update users
    updateDetail(obj): Observable<any> {
        return this._httpClient.post('api/UserController/UpdateUser', obj)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // delete user

    deleteUser(inputParms: any): Observable<any> {
        return this._httpClient.post('api/UserController/DeleteUsers', inputParms)
            .pipe(map(response => response), catchError(this._serverError));
    }
    //Get All Roles
    GetAllRoles(): Observable<any> {
        return this._httpClient.get('api/RolesController/GetAllRoles')
            .pipe(map(response => response), catchError(this._serverError));
    }
    GetRoleExistsByRoleName(RoleName): Observable<any> {
        return this._httpClient.get('api/RolesController/GetRoleExistsByRoleName/' + RoleName);
    }
    // UpdateRole
    UpdateRole(UpdateRoleobj): Observable<any> {
        return this._httpClient.post('api/RolesController/UpdateRole', UpdateRoleobj)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // GetRoleById
    GetRolesById(roleId): Observable<any> {
        return this._httpClient.get('api/RolesController/GetRolesById/' + roleId)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // SaveRole
    SaveRole(saveRoleobj): Observable<any> {
        return this._httpClient.post('api/RolesController/InsertRole', saveRoleobj)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // Delete Role
    deleteRole(inputParms: any): Observable<any> {
        return this._httpClient.post('api/RolesController/DeleteRoles', inputParms)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // GetAllLookups
    GetAllLookups(): Observable<any> {
        return this._httpClient.get('api/LookupsController/GetAllLookups')
            .pipe(map(response => response), catchError(this._serverError));
    }

    
    // GetLookupById
    GetLookupById(lookupid): Observable<any> {
        return this._httpClient.get('api/LookupsController/GetLookupById/' + lookupid)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // GetLookupDetailsByLookupNames
    GetLookupDetailsByLookupNames(LookupNamesString): Observable<any> {
        return this._httpClient.get('api/LookupsController/GetLookupDetailsByLookupNames/' + LookupNamesString)
            .pipe(map(response => response), catchError(this._serverError));
    }
    getRequestTemplateDetailsbylookup(id): Observable<any> {
        return this._httpClient.get('api/LookupsController/GetDesignerLookupsWithCategories/' + id)
            .pipe(map(response => response), catchError(this._serverError));
    }

    GetLookupDetailsById(id): Observable<any> {
        return this._httpClient.get('api/LookupsController/GetLookupDetailsById/' + id)
            .pipe(map(response => response), catchError(this._serverError));
    }

    //GetLookupDetailsByLookupId
    GetLookupDetailsByLookupId(id): Observable<any> {
        return this._httpClient.get('api/LookupsController/GetLookupDetailsByLookupId/' + id)
            .pipe(map(response => response), catchError(this._serverError));
    }

    // SaveLookup
    SaveLookup(obj): Observable<any> {
        return this._httpClient.post('api/LookupsController/SaveLookup', obj)
            .pipe(map(response => response), catchError(this._serverError));
    }

    // SaveLookupDetails
    SaveLookupDetails(obj): Observable<any> {
        return this._httpClient.post('api/LookupsController/SaveLookupDetails', obj)
            .pipe(map(response => response), catchError(this._serverError));
    }

    // DeleteLookup
    DeleteLookup(obj): Observable<any> {
        return this._httpClient.post('api/LookupsController/DeleteLookup',obj)
            .pipe(map(response => response), catchError(this._serverError));
    }

    // DeleteLookupDetails
    DeleteLookupDetails(obj): Observable<any> {
        return this._httpClient.post('api/LookupsController/DeleteLookupDetails', obj)
            .pipe(map(response => response), catchError(this._serverError));
    }
	 // GetAllRequestTemplates
    GetAllRequestTemplates(): Observable<any> {
        return this._httpClient.get('api/RequestTemplateController/GetAllRequestTemplate')
            .pipe(map(response => response), catchError(this._serverError));
    }
    // Delete Request Template
    deleterequestTemplate(inputParms: any): Observable<any> {
        return this._httpClient.post('api/RequestTemplateController/DeleteRequestTemplate', inputParms)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // SaveDesignerLookupDetails
    SaveDesignerLookupDetails(obj): Observable<any> {
        return this._httpClient.post('api/LookupsController/SaveLookupByDesiger', obj)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // get active roles
    GetAllActiveRoles(): Observable<any> {

        return this._httpClient.get('api/SecurityController/GetAllActiveRoles');
    }


    // get all modules
    GetAllModules(): Observable<any> {

        return this._httpClient.get('api/SecurityController/GetAllmodules');
    }

    // get all pages
    Getpages(): Observable<any> {

        return this._httpClient.get('api/SecurityController/GetAllPages');
    }
    RolePrivilegeSelect(Role: any): Observable<any> {

        return this._httpClient.post('api/SecurityController/GetAllRolePrivileges', Role);
    }
    GetAllUserRoles(userid: any): Observable<any> {
        return this._httpClient.get('api/SecurityController/GetAllUserRoles/' + userid)
            .pipe(map(response => response), catchError(this._serverError));
    }
    // get all previvileges
    GetAllModPagPrev(): Observable<any> {

        return this._httpClient.get('api/SecurityController/GetAllPrivileges');
    }
    RolePrivilegeUpsert(roleprivilegeobj): Observable<any> {
        return this._httpClient.post('api/SecurityController/RolePrivilegeUpsert', roleprivilegeobj);
    }

    UserRoleInsertBulk(obj): Observable<any> {
        return this._httpClient.post('api/SecurityController/UserRoleInsertBulk', obj);
    }
    UserRoleUpsert(obj): Observable<any> {
        return this._httpClient.post('api/SecurityController/UserRoleUpsert', obj);
    }
}

