import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { SharedServiceService } from 'app/services/shared-service.service';
import { takeUntil } from 'rxjs/operators';

import { FuseConfigService } from '@fuse/services/config.service';
import { navigation } from 'app/navigation/navigation';

@Component({
    selector: 'vertical-layout-1',
    templateUrl: './layout-1.component.html',
    styleUrls: ['./layout-1.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class VerticalLayout1Component implements OnInit, OnDestroy {
    fuseConfig: any;
    navigation: any;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     */
    isLogin: boolean;
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _sharedService: SharedServiceService,
    ) {
        // Set the defaults
        this.navigation = navigation;

        // Set the private defaults
        this._unsubscribeAll = new Subject();
        this._sharedService.currentMessage.subscribe((message) => {
            let rslt = JSON.parse(localStorage.getItem('login'));
            if (message == 'true') {
                if (rslt != null && rslt != undefined) {
                    if (rslt.isAdmin) {
                        this.isLogin = true;
                    } else {
                        this.isLogin = false;
                    }
                } else {
                    this.isLogin = false;
                }
            } else {
                this.isLogin = false;
            }
        })

    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        let rslt = JSON.parse(localStorage.getItem("login"));
        if (rslt != null && rslt != undefined) {
            if (rslt.isAdmin) {
                this.isLogin = true;
            } else {
                this.isLogin = false;
            }
        } else {
            this.isLogin = false;
        }
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
                this.fuseConfig = config;
            });
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
