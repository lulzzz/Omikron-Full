import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime } from 'rxjs/operators';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { CustomValidators } from 'src/custom-validators';

import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { DashboardView } from '../../../layout/models/dashboard-view';
import { AlertNotificationService } from '../../../overlay/services/alert-notification.service';
import { AccountStatus } from '../../../shared';
import { AuthService } from '../../../shared/auth.service';
import { UsersApiService } from '../../../users/users-api.service';
import { MyDetails } from '../../models/my-details';
import { AddAddressManuallyComponent } from '../add-address-manually/add-address-manually.component';
import { SecurityService } from './../../services/security.service';

@Component({
    selector: "app-security-my-details",
    templateUrl: "./security-my-details.component.html",
    styleUrls: ["./security-my-details.component.scss"],
})
export class SecurityMyDetailsComponent implements OnInit {
    constructor(
        private fb: FormBuilder,
        private usersApiService: UsersApiService,
        private securityService: SecurityService,
        private authService: AuthService,
        private router: Router,
        private alertNotificationService: AlertNotificationService,
        private eventBusService: EventBusService,
        private modalService: NgbModal
    ) {
        this.eventBusService.emit(
            new EmitEvent(Events.LayoutSelected, DashboardView.Public)
        );
    }

    formMyDetails: FormGroup;
    showAddress: boolean = false;
    showNextStep: boolean;
    postcode: string;
    errorMessage: string;
    validation: ValidationAggregate = new ValidationAggregate();
    @ViewChild("address") dropdown: ElementRef;
    buttonDisabled: boolean = false;

    ngOnInit() {
        this.formMyDetails = this.createMyDetailsForm();
        this.setFormValidation();
        this.formMyDetails.valueChanges
            .pipe(debounceTime(500))
            .subscribe((value) => {
                this.showNextStep = this.formMyDetails.valid;
            });
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("title", {
            min: "Please select a title.",
        });
        this.validation.addValidationMessage("firstName", {
            required: "Please enter your first name.",
        });
        this.validation.addValidationMessage("lastName", {
            required: "Please enter your last name.",
        });
        this.validation.addValidationMessage("dateOfBirth", {
            required: "Please enter your date of birth.",
            correctFormat: "Please enter date in correct format.",
        });
        this.validation.addValidationMessage("postcode", {
            required: "Please enter your postcode.",
        });
        this.validation.addValidationMessage("address", {
            required: "Please select your address.",
        });
        this.validation.bindValueChangesWithValidator(this.formMyDetails);
    }

    verifyDetails(): void {
        const userDetails = this.formMyDetails.value as MyDetails;
        userDetails.title = +userDetails.title;
        if (this.formMyDetails.valid) {
            this.sendDetails(userDetails);
        } else {
            this.validation.getValidationErrors(this.formMyDetails, true);
            this.formMyDetails.markAsTouched();
        }
    }

    sendDetails(userDetails: MyDetails): void {
        this.buttonDisabled = true;
        this.alertNotificationService.showInfo({ text: "Please wait while we verify your data." })
        this.securityService.addUserDetails(userDetails).subscribe(
            (data) => {
                this.buttonDisabled = false;
                var user = this.authService.getUserProfile();
                user.accountStatus = AccountStatus.AddBankAccount;
                user.firstName = this.formMyDetails.controls["firstName"].value;
                this.usersApiService
                    .updateUserAccountStatus(user)
                    .subscribe(() => this.authService.updateClaims(user));
                this.router.navigate(["/authenticate/add-account"]);
            },
            (error) => {
                this.buttonDisabled = false;
                this.alertNotificationService.showWarning({
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    createMyDetailsForm(): FormGroup {
        return this.fb.group({
            title: new FormControl(-1, [Validators.min(0)]),
            firstName: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            lastName: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            dateOfBirth: new FormControl("", [
                Validators.compose([Validators.required]),
                CustomValidators.patternValidator(
                    /(?:19|20|21)[0-9]{2}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-9])|(?:(?!02)(?:0[1-9]|1[0-2])-(?:30))|(?:(?:0[13578]|1[02])-31))/,
                    {
                        correctFormat: true,
                    }
                ),
            ]),
            postcode: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            address: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            isManualAddress: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
        });
    }

    getAddress(): void {
        if (!this.formMyDetails.get("postcode").value) {
            this.alertNotificationService.showInfo({
                text: "Please enter your postcode so we could find related address.",
            });
            return;
        }

        while (this.dropdown.nativeElement.firstChild) {
            this.dropdown.nativeElement.removeChild(
                this.dropdown.nativeElement.firstChild
            );
        }

        this.postcode = this.formMyDetails.get("postcode").value;
        this.usersApiService.getUserAddress(this.postcode).subscribe(
            (data) => {
                this.formMyDetails.controls["isManualAddress"].patchValue(false);
                this.showAddress = true;
                for (let i = 0; i < data.records.locations.length; i++) {
                    let option = document.createElement("OPTION"),
                        text = document.createTextNode(
                            data.records.locations[i]
                        );
                    option.appendChild(text);
                    this.dropdown.nativeElement.insertBefore(
                        option,
                        this.dropdown.nativeElement.lastChild
                    );
                }
                this.formMyDetails.controls["address"].patchValue(this.dropdown.nativeElement.lastChild.label);
            },
            (error) => {
                console.error(error);
                this.alertNotificationService.showInfo({
                    text: "Sorry, we couldn't find any address for provided postcode.",
                });
            }
        );
    }

    addAddressManually() {
        const modalRef = this.modalService.open(AddAddressManuallyComponent);

        modalRef.componentInstance.manualPostcode.subscribe((postcode) => {
            this.formMyDetails.controls["postcode"].patchValue(postcode);
        });

        modalRef.componentInstance.manualAddress.subscribe((address) => {
            let option = document.createElement("OPTION");
            option.appendChild(document.createTextNode(address));
            this.dropdown.nativeElement.add(option);
            this.formMyDetails.controls["address"].patchValue(address);
            this.formMyDetails.controls["isManualAddress"].patchValue(true);
            this.showAddress = true;
        });
    }
}
