import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { FuseSharedModule } from '@fuse/shared.module';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AuthenticationService } from 'app/authentication/authentication.service';
import {Base64Service } from 'app/authentication/base64.service';
import { FuseSidebarModule } from '@fuse/components';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

const routes = [

    {
        path: 'login',
        component: LoginComponent,
        resolve: {
            data: AuthenticationService
        }

    },
    {
        path: 'register',
        component: RegisterComponent,

    },
    {
        path: 'forgot-password',
        component: ForgotPasswordComponent,

    }
];

@NgModule({
    declarations: [
        LoginComponent,
        RegisterComponent,
        ForgotPasswordComponent
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
        MatSnackBarModule
    ],
    providers: [
        AuthenticationService, Base64Service
    ]
})
export class AuthenticationModule {
}
