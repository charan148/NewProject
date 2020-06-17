import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';
import * as _ from 'lodash';
import { Router } from '@angular/router'
import { FuseConfigService } from '@fuse/services/config.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { SharedServiceService } from 'app/services/shared-service.service';
import { navigation } from 'app/navigation/navigation';
import { Subscription } from 'rxjs';
@Component({
    selector: 'toolbar',
    templateUrl: './toolbar.component.html',
    styleUrls: ['./toolbar.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class ToolbarComponent implements OnInit, OnDestroy {
    horizontalNavbar: boolean;
    rightNavbar: boolean;
    hiddenNavbar: boolean;
    languages: any;
    navigation: any;
    selectedLanguage: any;
    userStatusOptions: any[];
    isLogin: boolean;
    isAdminLogged: boolean;
    userObj: any = {};
    LoggedUserName = '';
    subscription: Subscription;
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {TranslateService} _translateService
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _fuseSidebarService: FuseSidebarService,
        private _translateService: TranslateService,
        private _router: Router,
        private _sharedService: SharedServiceService
    ) {
        // this._sharedService.currentMessage.subscribe(message => this.isLogin = message == 'true' ? true : false);
        this._sharedService.currentMessage.subscribe((message) => {
            if (message == 'true') {
                this.isLogin = true;
                this.isAdminLogged = false;
                this.userUpdate();

            } else {
                this.isLogin = false;
                this.isAdminLogged = true;
            }
        })
        // Set the defaults
        this.userStatusOptions = [
            {
                title: 'Online',
                icon: 'icon-checkbox-marked-circle',
                color: '#4CAF50'
            },
            {
                title: 'Away',
                icon: 'icon-clock',
                color: '#FFC107'
            },
            {
                title: 'Do not Disturb',
                icon: 'icon-minus-circle',
                color: '#F44336'
            },
            {
                title: 'Invisible',
                icon: 'icon-checkbox-blank-circle-outline',
                color: '#BDBDBD'
            },
            {
                title: 'Offline',
                icon: 'icon-checkbox-blank-circle-outline',
                color: '#616161'
            }
        ];

        this.languages = [
            {
                id: 'en',
                title: 'English',
                flag: 'us'
            },
            {
                id: 'tr',
                title: 'Turkish',
                flag: 'tr'
            }
        ];

        this.navigation = navigation;

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    reqClick() {
        let userObj = localStorage.getItem('login');
        if (userObj != null && userObj != undefined) {
            this.isLogin = true;
            this.isAdminLogged = false;
            this._router.navigate(['/citizen/requests']);

        } else {
            this.isAdminLogged = true;
            this._router.navigate(['/authentication/login']);
        }
    }
    logout() {
        localStorage.clear();
        localStorage.removeItem('sessionPermissions');
        localStorage.removeItem('roleNameOfLoginUser');
        localStorage.removeItem('login');
        this.isLogin = false;
        this._router.navigate(['/citizen/home']);
        this._sharedService.changeMessage('false');
    }
    userUpdate() {
        this.userObj = JSON.parse(localStorage.getItem('login'));
        if (this.userObj != null && this.userObj != undefined) {
            this.LoggedUserName = this.userObj.firstName + ' ' + this.userObj.lastName;
            this.isLogin = true;
            this.isAdminLogged = false;
        }
    }
    //public toolbarLogin() {
    //    this.isLogin = false;
    //}
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.userUpdate();
        //let userobject: any = {};
        //userobject.userName = 'joecitizen@gmail.com';
        //userobject.Emailid = 'Citizen#1234';
        //LocalStorage.setItem('login', JSON.stringify(userobject));
        //this.userObj = JSON.parse(localStorage.getItem('login'));
        //console.log(this.userObj);
        //if (this.userObj != null && this.userObj != undefined) {
        //    this.LoggedUserName = this.userObj.firstName + ' ' + this.userObj.lastName;
        //    console.log(this.LoggedUserName)
        //    this.isLogin = true;
        //}
        // Subscribe to the config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((settings) => {
                this.horizontalNavbar = settings.layout.navbar.position === 'top';
                this.rightNavbar = settings.layout.navbar.position === 'right';
                this.hiddenNavbar = settings.layout.navbar.hidden === true;
            });

        // Set the selected language from default languages
        this.selectedLanguage = _.find(this.languages, { id: this._translateService.currentLang });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Toggle sidebar open
     *
     * @param key
     */
    toggleSidebarOpen(key): void {
        this._fuseSidebarService.getSidebar(key).toggleOpen();
    }

    /**
     * Search
     *
     * @param value
     */
    search(value): void {
        // Do your search here...
        console.log(value);
    }

    /**
     * Set the language
     *
     * @param lang
     */
    setLanguage(lang): void {
        // Set the selected language for the toolbar
        this.selectedLanguage = lang;

        // Use the selected language for translations
        this._translateService.use(lang.id);
    }


}
