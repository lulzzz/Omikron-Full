import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';

import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { DashboardView } from '../../../layout/models/dashboard-view';
import { Registration, RegistrationSteps } from '../../models/registration-steps.enum';

@Component({
    selector: "app-security-create-account",
    templateUrl: "./security-create-account.component.html",
    styleUrls: ["./security-create-account.component.scss"],
})
export class SecurityCreateAccountComponent implements OnInit {
    @Input() formSignup: FormGroup;
    @Input() registration: Registration;
    validation: ValidationAggregate = new ValidationAggregate();
    passwordTextType: boolean;
    repeatPasswordTextType: boolean;
    constructor(private eventBusService : EventBusService) {
        this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public))
    }

    ngOnInit(): void {
        this.setFormValidation();
    }

    setFormValidation(): void {
		this.validation.addValidationMessage('nickname', {
			required: 'Please enter your nickname.'
		});
        this.validation.addValidationMessage('email', {
			required: 'Please enter your email address.',
			email: 'Email is not valid.'
		});
        this.validation.addValidationMessage('termsAndConditions', {
			required: 'Please confirm you accept our Ts&Cs and Privacy Policy.'
		});
        this.validation.addValidationMessage('password', {
			required: 'Please create a password.',
			minlength: 'Use at least 8 characters.',
			hasNumber: 'Use 1 or more numbers.',
			hasCapitalCase: 'Use upper and lower case characters.'
		});

		this.validation.bindValueChangesWithValidator(this.formSignup);
	}

    toggleFieldTextType(): void {
        this.passwordTextType = !this.passwordTextType;
    }

    toggleRepeatFieldTextType(): void {
        this.repeatPasswordTextType = !this.repeatPasswordTextType;
    }

    submit() {
       if(this.formSignup.valid){
            this.registration.activeStep = RegistrationSteps.SMSVerification;
            this.registration.passedStep = RegistrationSteps.SMSVerification;
       }else{
            this.validation.getValidationErrors(this.formSignup, true);
       }
    }
}
