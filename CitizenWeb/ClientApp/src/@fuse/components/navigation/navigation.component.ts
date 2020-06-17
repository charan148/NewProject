import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { merge, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { AppConstants } from 'app/app.constants';
@Component({
    selector       : 'fuse-navigation',
    templateUrl    : './navigation.component.html',
    styleUrls      : ['./navigation.component.scss'],
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class FuseNavigationComponent implements OnInit
{
    isloggedIn: any;
    currentUserRole: any;
    permissions: any;
    @Input()
    layout = 'vertical';

    @Input()
    navigation: any;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     *
     * @param {ChangeDetectorRef} _changeDetectorRef
     * @param {FuseNavigationService} _fuseNavigationService
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseNavigationService: FuseNavigationService,
        private appConstants: AppConstants,
    )
    {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.isloggedIn = JSON.parse(localStorage.getItem('login'));
        // Load the navigation either from the input or from the service
        this.navigation = this.navigation || this._fuseNavigationService.getCurrentNavigation();

        // Subscribe to the current navigation changes
        this._fuseNavigationService.onNavigationChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {

                // Load the navigation
                this.navigation = this._fuseNavigationService.getCurrentNavigation();

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to navigation item
        merge(
            this._fuseNavigationService.onNavigationItemAdded,
            this._fuseNavigationService.onNavigationItemUpdated,
            this._fuseNavigationService.onNavigationItemRemoved
        ).pipe(takeUntil(this._unsubscribeAll))
         .subscribe(() => {

             // Mark for check
             this._changeDetectorRef.markForCheck();
         });

        this.permissions = JSON.parse(localStorage.getItem('sessionPermissions'));
        this.currentUserRole = JSON.parse(localStorage.getItem('roleNameOfLoginUser'));
        if (this.currentUserRole !== null && this.currentUserRole !== undefined && this.currentUserRole !== '') {
            

            this.navigation[1].hidden = true;
            this.navigation[1].children[0].hidden = true;
            this.navigation[1].children[1].hidden = true;
            this.navigation[1].children[3].hidden= true;
            this.navigation[1].children[4].hidden = true;

            for (let i = 0; i < this.permissions.length; i++) {
                if (this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[0].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[1].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[2].privilegeCode
                    || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[3].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[4].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[5].privilegeCode
                    || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[6].privilegeCode
                    || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[7].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[8].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[9].privilegeCode
                    || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[10].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[11].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[12].privilegeCode) {
                    this.navigation[1].hidden = false;
                }
            }

            for (let i = 0; i < this.permissions.length; i++) {
               
                if (this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[0].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[1].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[2].privilegeCode) {
                    this.navigation[1].children[0].hidden = false;
                } else if (this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[4].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[5].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[6].privilegeCode) {
                    this.navigation[1].children[1].hidden = false;
                } else if (this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[9].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[10].privilegeCode || this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[11].privilegeCode) {
                    this.navigation[1].children[3].hidden = false;
                } else if (this.permissions[i].privilegeCode == this.appConstants.COMM_PRIVILEGES[8].privilegeCode) {
                    this.navigation[1].children[4].hidden = false;
                } 

            }
        }
    }
}
