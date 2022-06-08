import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { CustomValidators } from '../../../custom-validators';
import { AccountStatus } from '../../shared';
import { AuthService } from '../../shared/auth.service';
import { UserInterfaceUtilityService } from '../../shared/utilities/user-interface-utility.service';
import { SecurityUserPersistenceService } from '../security-user-persistence.service';
import { SecurityService } from '../services/security.service';
import { ValidationAggregate } from './../../core/utilities/validation-aggregate';
import { AlertNotificationService } from './../../overlay/services/alert-notification.service';
import { LogoutTriggerService } from './../../shared/utilities/logout-trigger.service';


@Component({
    selector: "security-login-form",
    templateUrl: "security-login-form.component.html",
    changeDetection: ChangeDetectionStrategy.OnPush,
    styleUrls: ["security-login-form.component.scss"],
})
export class SecurityLoginFormComponent implements OnInit {
    isBusy = false;
    rememberMeAt: boolean = false;
    user: any = {};
    error: any;
    paramSubscribe: any;
    antiBotProtectionSucceeded: boolean;
    passwordTextType: boolean;
    signInForm: FormGroup;
    verification: boolean = false;
    phoneNumber: string;
    validation: ValidationAggregate = new ValidationAggregate();
    @Output() onError: EventEmitter<any> = new EventEmitter();

    constructor(
        private readonly cd: ChangeDetectorRef,
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly userInterfaceUtilityService: UserInterfaceUtilityService,
        private readonly userPersistenceService: SecurityUserPersistenceService,
        private readonly securityService: SecurityService,
        private readonly alertNotificationService: AlertNotificationService,
        private readonly formBuilder: FormBuilder,
        private readonly logoutTriggerService: LogoutTriggerService,
    ) { }

    ngOnInit(): void {
        this.signInForm = this.createSignInForm();
        this.setFormValidation();
    }

    setFormValidation() {
        this.validation.addValidationMessage("userName", {
            required: "Please enter your email address.",
            email: "Email is not valid.",
        });

        this.validation.addValidationMessage("password", {
            required: "Please enter your password.",
            minlength: "Use at least 8 characters.",
            hasNumber: "Use 1 or more numbers.",
            hasCapitalCase: "Use upper and lower case characters.",
            hasSmallCase: "Use upper and lower case characters.",
        });

        this.validation.bindValueChangesWithValidator(this.signInForm);
    }

    createSignInForm(): FormGroup {
        return this.formBuilder.group({
            userName: new FormControl("", [
                Validators.compose([Validators.required, Validators.email]),
            ]),
            password: new FormControl("", [
                Validators.required,
                CustomValidators.patternValidator(/\d/, {
                    hasNumber: true,
                }),
                CustomValidators.patternValidator(/[A-Z]/, {
                    hasCapitalCase: true,
                }),
                CustomValidators.patternValidator(/[a-z]/, {
                    hasSmallCase: true,
                }),
                Validators.minLength(8),
            ]),
            rememberMeAt: new FormControl(),
        });
    }

    submit() {
        if (this.signInForm.valid) {
            this.preLogin(this.signInForm.value);
        } else {
            this.validation.getValidationErrors(this.signInForm, true);
        }
    }

    preLogin(user: any): void {
        this.user = user;
        this.securityService.login(user).subscribe(
            (data) => {
                if (data.records == "RememberMeActivated") {
                    this.login(user);
                } else {
                    this.phoneNumber = data.records;
                    this.verification = true;
                    this.cd.detectChanges();
                }
            },
            (error) => {
                this.alertNotificationService.showWarning({
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    resendCode() {
        this.preLogin(this.user);
    }

    verifyPhoneNumber(token: number) {
        let loginVerification = { userName: this.user.userName, token: token };
        this.securityService.loginVerification(loginVerification).subscribe(
            () => this.login(this.user),
            (error) =>
                this.alertNotificationService.showWarning({
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.message,
                })
        );
    }

    login(user: any): void {
        this.error = null;
        this.isBusy = true;
        this.authService
            .login(user.userName, user.password)
            .then(() => this.onSuccessfully())
            .catch((data) => this.onFailed(data.error));
    }

    saveLogin(): void {
        if (this.rememberMeAt) {
            this.securityService.activateRememberMe(this.user.userName, new Date()).subscribe(
                () => { this.alertNotificationService.showSuccess({
                    text: "Remember me activated for 30 days."}),
                (error) => {
                    this.alertNotificationService.showWarning({
                        text: error.error.errors
                            ? error.error.errors[0]
                            : error.error.Message,
                    });
                }});
            const profile = this.authService.getUserProfile();
            this.userPersistenceService.save(profile);
        }
        else {
            this.logoutTriggerService.activateLogoutTimer();
        }
    }

    removeAuthenticationClass(): void {
        this.userInterfaceUtilityService.addBodyClass("bg-light");
        this.userInterfaceUtilityService.removeBodyClass("authentication");
    }

    private onSuccessfully(): void {
        if (this.authService.isLoggedIn()) {
            const profile = this.authService.getUserProfile();
            this.saveLogin();

            switch (profile.accountStatus) {
                case AccountStatus.Active:
                    this.router.navigate(["/home"]);
                    break;
                case AccountStatus.PerformKyc:
                        this.router.navigate(["/authenticate/my-details"]);
                        break;
                case AccountStatus.AddBankAccount:
                    this.router.navigate(["/authenticate/add-account"]);
                    break;
                default:
                    this.router.navigate(["/error/404"]);
                    break;
            }
        }

        this.isBusy = false;
        this.cd.markForCheck();
    }

    private onFailed(error): void {
        if (this.onError) {
            this.onError.emit(error);
        }
        this.isBusy = false;
        this.error = error;
        this.cd.markForCheck();
    }

    toggleFieldTextType(): void {
        this.passwordTextType = !this.passwordTextType;
    }

    hideVerification(){
        this.verification = false;
    }

    contactUs()
    {
        window.open("https://omikronmoney.io/contact-us", "_blank");
    }
}
