import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { FuseSharedModule } from '@fuse/shared.module';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CitizenService } from 'app/citizen/citizen.service';
import { AuthenticationService } from 'app/authentication/authentication.service';
import { Base64Service } from 'app/authentication/base64.service';
import { FuseSidebarModule } from '@fuse/components';
import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';
import { AgmCoreModule } from '@agm/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatStepperModule } from '@angular/material/stepper';


import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseWidgetModule } from '@fuse/components/widget/widget.module';

import { HomeComponent } from 'app/citizen/home/home.component';
import { RequestFormComponent } from 'app/citizen/requestform/requestform.component';
import { MyRequestsComponent } from 'app/citizen/myrequests/myrequests.component';
import { MyRequestDetailsComponent } from 'app/citizen/myrequestdetails/myrequestdetails.component';

//Childs
import { SimilarRequestsComponent } from 'app/citizen/childforms/similar-requests/similar-requests.component';
import { RegisterSignInComponent } from 'app/citizen/childforms/register-signin/register-signin.component';
import { LocationComponent } from 'app/citizen/childforms/location/location.component';
import { SubmitComponent } from 'app/citizen/childforms/submit/submit.component';
import { DetailsComponent } from 'app/citizen/childforms/details/details.component';

import { FieldBuilderComponent } from '../citizen/components/field-builder/field-builder.component';
import { TextareaComponent } from '../citizen/components/textarea/textarea.component';
import { CheckboxComponent } from '../citizen/components/checkbox/checkbox.component';
import { LabelComponent } from '../citizen/components/label/label.component';
import { TextComponent } from './components/text/text.component';
import { DropdownComponent } from './components/dropdown/dropdown.component';
import {RadioComponent } from './components/radio/radio.component';
import { AddPhotoComponent } from '../citizen/childforms/add-photo/add-photo.component';
import { MatCarouselModule } from '@ngmodule/material-carousel';
import { RequestFormService } from 'app/services/requestform.service';
import { RequestTransactionService } from 'app/services/request-transaction.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatRadioModule } from '@angular/material/radio';

const routes = [
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'requestform',
        component: RequestFormComponent
    },
    {
        path: 'requests',
        component: MyRequestsComponent
    },
    {
        path: 'requestdetails',
        component: MyRequestDetailsComponent
    },
    {
        path: '**',
        redirectTo: 'home'
    }
];

@NgModule({
    declarations: [
        HomeComponent,
        RequestFormComponent,
        MyRequestsComponent,
        MyRequestDetailsComponent,
        AddPhotoComponent,
        SimilarRequestsComponent,
        RegisterSignInComponent,
        LocationComponent,
        SubmitComponent,
        DetailsComponent,
        FieldBuilderComponent,
        TextareaComponent,
        CheckboxComponent,
        DropdownComponent,
        LabelComponent,
        TextComponent,
        RadioComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        MatCheckboxModule,
        FuseSharedModule,
        FuseSidebarModule,
        MatAutocompleteModule,
        MatStepperModule,
        RecaptchaModule,  //this is the recaptcha main module
        RecaptchaFormsModule, //this is the module for form incase form validation
        MatChipsModule,
        MatExpansionModule,
        MatPaginatorModule,
        MatRippleModule,
        MatSortModule,
        MatSnackBarModule,
        MatTableModule,
        MatTabsModule,
        NgxPaginationModule,
        MatRadioModule,
        MatCarouselModule.forRoot(),
        AgmCoreModule.forRoot({
            apiKey: "AIzaSyCDhjF3LNn2qqYUivCkiyYD8lQMAzihz7I",
            libraries: ["places"]
        }),
    ],
    providers: [
        CitizenService,
        RequestFormService,
        RequestTransactionService,
        AuthenticationService,
        Base64Service
    ]
})
export class CitizenModule {
}
