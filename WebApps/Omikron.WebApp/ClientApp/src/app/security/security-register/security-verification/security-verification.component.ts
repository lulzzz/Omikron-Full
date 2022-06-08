import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CountryISO, PhoneNumberFormat, SearchCountryField } from 'ngx-intl-tel-input';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { environment } from 'src/environments/environment';

import { AccountStatus } from '../../../shared';
import { AuthService } from '../../../shared/auth.service';
import { Register } from '../../models/register';
import { Registration, RegistrationSteps } from '../../models/registration-steps.enum';
import { SecurityService } from '../../services/security.service';

@Component({
    selector: "app-security-verification",
    templateUrl: "./security-verification.component.html",
    styleUrls: ["security-verification.component.scss"],
})
export class SecurityVerificationComponent implements OnInit {
    SearchCountryField = SearchCountryField;
    CountryISO = CountryISO;
    PhoneNumberFormat = PhoneNumberFormat;
    countries: any;
    phoneForm = new FormGroup({
        phone: new FormControl(undefined, [Validators.required]),
    });
    validation: ValidationAggregate = new ValidationAggregate();

    @Input() formSignup: FormGroup;
    @Input() registration: Registration;
    verifyNumber: FormControl = new FormControl("", Validators.required);
    errorMessage: string;

    showVerificationCode: boolean = false;
    constructor(
        private securityService: SecurityService,
        private router: Router,
        private authService: AuthService,
        private alertNotificationService: AlertNotificationService
    ) {}

    ngOnInit(): void {
       this.setFormValidation();
       this.countries = environment.production ? [CountryISO.UnitedKingdom]
       : [CountryISO.UnitedKingdom, CountryISO.BosniaAndHerzegovina]
    }

    setFormValidation(): void {
		this.validation.addValidationMessage('phone', {
			required: 'Please enter your phone number.'
		});
		this.validation.bindValueChangesWithValidator(this.phoneForm);
	}

    verifyTelephone(): void {
        if(this.verifyNumber.valid){
            this.formSignup.controls["verificationToken"].setValue(
                this.verifyNumber.value
            );
            const register = this.formSignup.value as Register;
            this.securityService.createUser(register).subscribe(
                (data) => {
                    this.alertNotificationService.showSuccess({text: 'Your account has been successfully created.'});
                    this.authService
                        .login(register.email, register.password)
                        .then(() => this.onSuccessfully())
                },(error) => {
                   this.alertNotificationService.showWarning({text: error.error.errors ? error.error.errors[0] : error.error.message});
                }
            );
        }else{
            this.verifyNumber.markAsTouched();
        }

    }

    private onSuccessfully(): void {
        if (this.authService.isLoggedIn()) {
            const profile = this.authService.getUserProfile();

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
    }

    resendCode(): void {
        this.submitCode(this.formSignup.controls["phoneNumber"].value);
    }

    back(): void {
        this.registration.activeStep = RegistrationSteps.CreateAccount;
    }

    backPhoneNumber(){
        this.showVerificationCode = false;
    }

    sendCode(): void {
        if(this.phoneForm.valid){
            if (this.phoneForm.get("phone").valid) {
                this.submitCode(this.phoneForm.get("phone").value.e164Number);
            }
        }else{
            this.validation.getValidationErrors(this.phoneForm, true);
            this.phoneForm.markAsTouched();
        }
    }

    submitCode(telephone: string): void {
        this.securityService.phoneVerification(telephone, this.formSignup.controls["email"].value).subscribe(
            (data) => {
                this.formSignup.controls["phoneNumber"].setValue(telephone);
                this.showVerificationCode = true;
            },
            (error) => {
                this.alertNotificationService.showWarning({text: error.error.errors ? error.error.errors[0] : error.error.Message});
            }
        );
    }
}
