import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { FuseSharedModule } from '@fuse/shared.module';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FuseSidebarModule } from '@fuse/components';
import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';
import { AgmCoreModule } from '@agm/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDialogModule } from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import { TreeviewModule } from 'ngx-treeview';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';

import { UsersComponent } from './users/users.component';
import { AdministrationService } from '../administration/administration.service';
import { MatMenuModule } from '@angular/material/menu';
import { UserdetailsComponent } from './userdetails/userdetails.component'
import { Base64Service } from '../authentication/base64.service';
import { RolesComponent } from './roles/roles.component';
import { RolesdetailsComponent } from './rolesdetails/rolesdetails.component';
import { LookupComponent } from './lookup/lookup.component';
import { LookupdetailsComponent } from './lookupdetails/lookupdetails.component';
import {LookupdatailspopupComponent } from '../administration/lookupdatailspopup/lookupdatailspopup.component';

import { SecurityComponent } from './security/security.component';
import { SecurityLookUpComponent } from './security-look-up/security-look-up.component';
const routes = [
    {
        path: 'users',
        component: UsersComponent
    },
    {
        path: 'user-details',
        component: UserdetailsComponent
    },
    {
        path: 'roles',
        component: RolesComponent
    },
    {
        path: 'roles-details',
        component: RolesdetailsComponent
    },
    {
        path: 'lookup',
        component: LookupComponent
    },
    {
        path: 'lookupdetails',
        component: LookupdetailsComponent
    },
    {
        path: 'security',
        component: SecurityComponent
    },
	
   
   
    {
        path: '**',
        redirectTo: 'users'
    }
    
];

@NgModule({
    declarations: [UsersComponent, UserdetailsComponent, RolesComponent, RolesdetailsComponent, LookupComponent, 
        LookupdetailsComponent, LookupdatailspopupComponent, SecurityComponent, SecurityLookUpComponent],
  imports: [
      RouterModule.forChild(routes),
      MatButtonModule,
      MatFormFieldModule,
      MatIconModule,
      MatInputModule,
      MatSelectModule,
      MatDatepickerModule,
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
      MatMenuModule,
      MatDialogModule,
      MatRadioModule,
      TreeviewModule,
      TreeviewModule.forRoot(),
      //MatCarouselModule.forRoot(),
      AgmCoreModule.forRoot({
          apiKey: "AIzaSyCDhjF3LNn2qqYUivCkiyYD8lQMAzihz7I",
          libraries: ["places"]
      }),
    ], entryComponents: [LookupdatailspopupComponent, LookupdetailsComponent, SecurityLookUpComponent, SecurityComponent],
    providers: [
        AdministrationService, Base64Service
    ]
})
export class AdministrationModule { }
