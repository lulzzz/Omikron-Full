import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Event as NavigationEvent, NavigationEnd, Router } from '@angular/router';

import { CustomValidators } from '../../../../custom-validators';
import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { DashboardView } from '../../../layout/models/dashboard-view';
import { Registration, RegistrationSteps } from './../../models/registration-steps.enum';

@Component({
    selector: "security-register",
    templateUrl: "./security-register.component.html",
    styleUrls: ["./security-register.component.scss"],
})
export class SecurityRegisterComponent implements OnInit {
    formSignup: FormGroup;
    registration: Registration = new Registration();
    responseState : boolean;
    redirectUrl : string = "/authenticate/response";
    redirectAddAccount : string = "/authenticate/add-account";
    constructor(
        private fb: FormBuilder,
        private router: Router,
        private eventBusService : EventBusService
    ) {this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));
        this.router.events.forEach((event: NavigationEvent) => {
            if (event instanceof NavigationEnd) {
                switch (event.url.split('&')[0]) {
                    case "/authenticate/register": {
                        this.registration.activeStep =
                            RegistrationSteps.CreateAccount;
                        break;
                    }
                    case "/authenticate/my-details": {
                        this.registration.activeStep =
                            RegistrationSteps.MyDetails;
                        this.registration.passedStep =
                            RegistrationSteps.MyDetails;
                        break;
                    }
                    case "/authenticate/add-account": {
                        this.registration.activeStep =
                            RegistrationSteps.MyAccount;
                        this.registration.passedStep =
                            RegistrationSteps.MyAccount;
                        break;
                    }
                    case "/authenticate/response?status=success":{
                        this.registration.activeStep = 5;
                        this.responseState = true;
                        break;
                    }
                    case "/authenticate/response?status=failure":{
                        this.registration.activeStep = 5;
                        this.responseState = false;
                        break;
                    }
                }
            }
        });
    }

    ngOnInit(): void {
        this.formSignup = this.createSignupForm();
    }

    createSignupForm(): FormGroup {
        return this.fb.group({
            nickname: new FormControl("", [
                Validators.compose([
                    Validators.minLength(1),
                    Validators.required,
                ]),
            ]),
            email: new FormControl("", [
                Validators.compose([
                    Validators.required,
                    Validators.email
                ]),
            ]),
            password: new FormControl("", [
                Validators.compose([
                    Validators.required,
                    // check whether the entered password has a number
                    CustomValidators.patternValidator(/\d/, {
                        hasNumber: true,
                    }),
                    // check whether the entered password has upper case letter
                    CustomValidators.patternValidator(/[A-Z]/, {
                        hasCapitalCase: true,
                    }),
                    // check whether the entered password has a lower case letter
                    CustomValidators.patternValidator(/[a-z]/, {
                        hasSmallCase: true,
                    }),
                    Validators.minLength(8),
                ]),
            ]),
            termsAndConditions: new FormControl(false, Validators.requiredTrue),
            emailSubscription: new FormControl(false),
            phoneNumber: new FormControl(""),
            verificationToken: new FormControl(""),
        });
    }

    contactUs()
    {
        window.open("https://omikronmoney.io/contact-us", "_blank");
    }
}
