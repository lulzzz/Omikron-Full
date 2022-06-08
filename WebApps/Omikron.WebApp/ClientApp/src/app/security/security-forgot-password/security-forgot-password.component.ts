import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';

import { CustomValidators } from '../../../custom-validators';
import { AlertNotificationService } from '../../overlay/services/alert-notification.service';
import { SecurityApiService } from '../../shared/security-api.service';

@Component({
    selector: "security-forgot-password",
    templateUrl: "security-forgot-password.component.html",
    styleUrls: ["security-forgot-password.component.scss"],
})
export class SecurityForgotPasswordComponent implements OnInit {
    PasswordChangeForm: FormGroup;
    token: string;
    error: any;
    verification: boolean;
    passwordTextType: boolean;
    repeatPasswordTextType: boolean;
    phoneNumber: string;
    @Output() onError: EventEmitter<any> = new EventEmitter();

    constructor(
        private readonly alertNotificationService: AlertNotificationService,
        private readonly apiService: SecurityApiService,
        private readonly router: Router,
        private cd : ChangeDetectorRef,
        private fb: FormBuilder
    ) {}
    validation: ValidationAggregate = new ValidationAggregate();
    ngOnInit() {
        this.PasswordChangeForm = this.createPasswordChangeForm();
        this.setFormValidation();
    }

    generateToken(): void {
            if (this.PasswordChangeForm.valid) {
                this.apiService
                    .generateToken(this.PasswordChangeForm.get("email").value)
                    .subscribe(
                        (data) => {
                            this.phoneNumber = data.records;
                            this.verification = true;
                            this.cd.markForCheck();
                        },
                        (error) => {
                            this.alertNotificationService.showWarning({
                                text: error.error.errors
                                    ? error.error.errors[0]
                                    : error.error.Message,
                            });
                        }
                    );
            } else {
                this.validation.getValidationErrors(this.PasswordChangeForm, true);
            }
    }

    changePassword(token: number): void {
        this.apiService
            .changePassword(
                this.PasswordChangeForm.get("email").value,
                this.PasswordChangeForm.get("password").value,
                token
            )
            .subscribe(
                () => {
                    this.alertNotificationService.showSuccess({
                        text: "Your password has been successfully changed.",
                    });
                    this.router.navigate(["/authenticate/login"]);
                },
                (error) =>
                    this.alertNotificationService.showWarning({
                        text: error.error.errors
                            ? error.error.errors[0]
                            : error.message,
                    })
            );
    }

    toggleFieldTextType(): void {
        this.passwordTextType = !this.passwordTextType;
    }

    toggleRepeatFieldTextType(): void {
        this.repeatPasswordTextType = !this.repeatPasswordTextType;
    }

    hideVerification() {
        this.verification = false;
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("email", {
            required: "This input can’t be blank.",
            email: "Email is not valid.",
        });
        this.validation.addValidationMessage("password", {
            required: "This input can’t be blank.",
            minlength: "Use at least 8 characters.",
            hasNumber: "Use 1 or more numbers.",
            hasCapitalCase: "Use upper and lower case characters.",
        });
        this.validation.bindValueChangesWithValidator(this.PasswordChangeForm);
    }

    createPasswordChangeForm(): FormGroup {
        return this.fb.group({
            email: new FormControl("", [
                Validators.compose([Validators.required, Validators.email]),
            ]),
            password: new FormControl("", [
                Validators.compose([
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
            ]),
        });
    }
}
