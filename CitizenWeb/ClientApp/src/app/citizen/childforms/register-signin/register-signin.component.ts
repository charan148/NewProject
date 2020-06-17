import { Component, OnDestroy, OnInit, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { CitizenService } from 'app/citizen/citizen.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasswordValidation } from '../../../authentication/password-validator/passwordvalidate';

@Component({
    selector: 'register-signin',
    templateUrl: './register-signin.component.html',
    styleUrls: ['./register-signin.component.scss'],
    animations: fuseAnimations
})
export class RegisterSignInComponent implements OnInit, OnDestroy {

    // Private
    private _unsubscribeAll: Subject<any>;
    @Output() registerAccClick = new EventEmitter();
    @Output() submitClick = new EventEmitter();

    isReg: boolean;
    isSignIn: boolean;
    isRegButton: boolean;
    isSignButton: boolean;
    isSubReq: boolean;

    isSub: boolean;
    isForgot: boolean;
    isloggedIn: any;
    isHideTab: boolean;
    register = true;

    form: FormGroup;
    loginForm: FormGroup;
    registerForm: FormGroup;
    forgotPasswordForm: FormGroup;

    constructor(
        private _citizenService: CitizenService,
        private _formBuilder: FormBuilder,
    ) {
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this.loginForm = this._formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            Password: ['', Validators.required]
        });

        this.registerForm = this._formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            userName: ['', Validators.required],
            emailAddress: ['', [Validators.required, Validators.email]],
            Password: ['', Validators.required],
            ConfirmPassword: ['', Validators.required],
            phoneNumber: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
        },
            {
                validator: PasswordValidation.MatchPassword
            });
        this.forgotPasswordForm = this._formBuilder.group({
            email: ['', [Validators.required, Validators.email]]
        });
       // this.registerForm.get("nearestHome").setValue("121 S Orange Ave, Orlando, FL 32801, USA");

        // Update the validity of the 'passwordConfirm' field
        // when the 'password' field changes
        this.registerForm.get('password').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
                this.registerForm.get('passwordConfirm').updateValueAndValidity();
            });
        this.isloggedIn = localStorage.getItem('login');
        if (this.isloggedIn != null && this.isloggedIn != undefined) {
            this.isHideTab = false;
            this.register = false;
        } else {
            this.isHideTab = true;
            this.register = true;

        }
    }

    regSubmit() {
        this.isReg = true;
        this.isSignIn = false;
        this.isSignButton = true;
        this.isRegButton = true;
    }
    signSubmit() {
        this.isSignIn = true;
        this.isReg = false;
        this.isSignButton = true;
        this.isRegButton = true;
    }
    back() {
        this.isSignIn = false;
        this.isReg = false;
        this.isRegButton = false;
        this.isSignButton = false;
    }
    forgotClick() {
        this.isForgot = true;
        this.isSignIn = false;
    }
    email: any = "joecitizen@mail.com";
    pwd: any = "test@123";
    emailChange(event: any) {
        this.email = event;
    }
    pwdChange(event: any) {
        this.pwd = event;

    }
    backLoginClick() {
        this.isSignIn = true;
        this.isForgot = false;
    }
    loginsuc: boolean;

    registerAcc() {
        this.registerAccClick.emit(this.registerForm.value);
    }

    signIn() {
        this.submitClick.emit(this.loginForm.value);
    }
    passwordChange(val: any, cnfrmval: any) {
        if (val._pendingValue.trim() == "") {
            this.registerForm.controls.Password.setErrors({ required: true })
        }
        if (val._pendingValue == '') {
            this.registerForm.controls.ConfirmPassword.setValue('');
        }

    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
