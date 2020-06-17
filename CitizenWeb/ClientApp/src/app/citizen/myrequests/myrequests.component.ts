import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DataSource, SelectionModel } from '@angular/cdk/collections';
import { BehaviorSubject, fromEvent, merge, Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';

import { takeUntil } from 'rxjs/internal/operators';
import { MatTableDataSource } from '@angular/material/table';
import { RequestFormService } from '../../services/requestform.service';
import { Router } from '@angular/router';


export interface LookupData {
    lookupName: any;
    lookupLongDescription: any;
    lookupDescription: any;
    lookupShortDescription: any;
    lookupDisplayDescription: any;
    isUsedInDisplay: any;
    rowStatus: any;
    effectiveFrom: any;
    effectiveTo: any;
    lookupStatus: any;
}
export interface RequestData {
    requestName: any;
    department: any;
    requestStatusText: any;
    priority: any;
    createdDate: any;
    lastActivity: any;
}

@Component({
    selector: 'myrequests',
    templateUrl: './myrequests.component.html',
    styleUrls: ['./myrequests.component.scss'],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None
})


export class MyRequestsComponent implements OnInit, OnDestroy {
  //  dataSource: FilesDataSource | null;
    displayedColumns = ['requestName', 'department', 'requestStatusText', 'priority', 'createdDate', 'lastActivity'];

    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild('filter', { static: true })
    filter: ElementRef;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;
   
    // Private
    private _unsubscribeAll: Subject<any>;
    dataSource: MatTableDataSource<RequestData>;

    selection = new SelectionModel<RequestData>(true, []);
    isloggedIn: any = [];
    userId: any = [];
    requestList: any = [];
    selectedRequestData: any;
   
    constructor( 
        private requestService: RequestFormService,
        private router: Router,
    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        this.userId = this.isloggedIn.userId
        this.requestService.getRequestbyUserId(this.userId).subscribe((data) => {
            this.requestList = data;
            this.dataSource = new MatTableDataSource(this.requestList);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        })


        fromEvent(this.filter.nativeElement, 'keyup')
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(150),
                distinctUntilChanged()
            )
            .subscribe(() => {
                if (!this.dataSource) {
                    return;
                }
                this.dataSource.filter = this.filter.nativeElement.value;
            });
    }

    rowClick(ID) {
        sessionStorage.setItem("ID", ID);
        this.router.navigate(['/citizen/requestdetails']);
    }
    MyRequestSearch(search: string): void {
        this.dataSource.filter = search.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

export class FilesDataSource extends DataSource<any>
{
    // Private
    private _filterChange = new BehaviorSubject('');
    private _filteredDataChange = new BehaviorSubject('');

    /**
     * Constructor
     *
     * @param {EcommerceOrdersService} _ecommerceOrdersService
     * @param {MatPaginator} _matPaginator
     * @param {MatSort} _matSort
     */
    constructor(
        private _ecommerceOrdersService: any = [],
        private _matPaginator: MatPaginator,
        private _matSort: MatSort
    ) {
        super();

        this.filteredData = this._ecommerceOrdersService;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    // Filtered data
    get filteredData(): any {
        return this._filteredDataChange.value;
    }

    set filteredData(value: any) {
        this._filteredDataChange.next(value);
    }

    // Filter
    get filter(): string {
        return this._filterChange.value;
    }

    set filter(filter: string) {
        this._filterChange.next(filter);
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Connect function called by the table to retrieve one stream containing the data to render.
     *
     * @returns {Observable<any[]>}
     */
    connect(): Observable<any[]> {
        const displayDataChanges = [
            this._ecommerceOrdersService,
            this._matPaginator.page,
            this._filterChange,
            this._matSort.sortChange
        ];

        return merge(...displayDataChanges).pipe(map(() => {

            let data = this._ecommerceOrdersService.slice();

            data = this.filterData(data);

            this.filteredData = [...data];

            data = this.sortData(data);

            // Grab the page's slice of data.
            const startIndex = this._matPaginator.pageIndex * this._matPaginator.pageSize;
            return data.splice(startIndex, this._matPaginator.pageSize);
        })
        );

    }

    /**
     * Filter data
     *
     * @param data
     * @returns {any}
     */
    filterData(data): any {
        if (!this.filter) {
            return data;
        }
        return FuseUtils.filterArrayByString(data, this.filter);
    }

    /**
     * Sort data
     *
     * @param data
     * @returns {any[]}
     */
    sortData(data): any[] {
        if (!this._matSort.active || this._matSort.direction === '') {
            return data;
        }

        return data.sort((a, b) => {
            let propertyA: number | string = '';
            let propertyB: number | string = '';

            switch (this._matSort.active) {
                case 'id':
                    [propertyA, propertyB] = [a.id, b.id];
                    break;
                case 'reference':
                    [propertyA, propertyB] = [a.reference, b.reference];
                    break;
                case 'firstName':
                    [propertyA, propertyB] = [a.firstName, b.firstName];
                    break;
                case 'status':
                    [propertyA, propertyB] = [a.status, b.status];
                    break;
                case 'priority':
                    [propertyA, propertyB] = [a.priority, b.priority];
                    break;
                case 'status':
                    [propertyA, propertyB] = [a.date, b.date];
                    break;
                case 'date':
                    [propertyA, propertyB] = [a.lastactivity, b.lastactivity];
                    break;
            }

            const valueA = isNaN(+propertyA) ? propertyA : +propertyA;
            const valueB = isNaN(+propertyB) ? propertyB : +propertyB;

            return (valueA < valueB ? -1 : 1) * (this._matSort.direction === 'asc' ? 1 : -1);
        });
    }
    /**
     * Disconnect
     */
    disconnect(): void {
    }


}
