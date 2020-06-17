import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { TranslateModule } from '@ngx-translate/core';
import { ConfirmationDailogService } from 'app/custom_alerts/confirmation-dailog.service';
import { ConfirmationDailogComponent } from 'app/custom_alerts/confirmation-dailog/confirmation-dailog.component';
import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { FakeDbService } from 'app/fake-db/fake-db.service';
import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';
import { AgmCoreModule } from '@agm/core';
import { MatDialogModule } from '@angular/material/dialog';
import { AppConstants } from 'app/app.constants';
const appRoutes: Routes = [
    {
        path: 'authentication',
        loadChildren: './authentication/authentication.module#AuthenticationModule'
    },
    {
        path: 'citizen',
        loadChildren: './citizen/citizen.module#CitizenModule'
    },
    {
        path: 'administration',
        loadChildren: './administration/administration.module#AdministrationModule'
    },
    {
        path: 'user/analytics',
        loadChildren: './user/analytics/analytics.module#AnalyticsModule'
    },
    {
        path: 'user/myreport',
        loadChildren: './user/myreport/myreport.module#MyReportModule'
    },
    {
        path: 'designer',
        loadChildren: './designer/designer.module#DesignerModule'
    },
    {
        path: '**',
        redirectTo: 'citizen/home'
    },

];

@NgModule({
    declarations: [
        AppComponent, ConfirmationDailogComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),
        RecaptchaModule,  //this is the recaptcha main module
        RecaptchaFormsModule, //this is the module for form incase form validation
        TranslateModule.forRoot(),
        AgmCoreModule.forRoot({
            apiKey: "AIzaSyCDhjF3LNn2qqYUivCkiyYD8lQMAzihz7I",
            libraries: ["places"]
        }),
        InMemoryWebApiModule.forRoot(FakeDbService, {
            delay: 0,
            passThruUnknownUrl: true
        }),

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,
        MatDialogModule,
        // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,
        FuseThemeOptionsModule,

        // App modules
        LayoutModule
    ],
    providers: [
        AppConstants, ConfirmationDailogService
    ],
    entryComponents: [ConfirmationDailogComponent],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
}
