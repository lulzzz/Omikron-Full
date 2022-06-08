import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { AuthService } from 'src/app/shared/auth.service';
import { UsersApiService } from 'src/app/users/users-api.service';
import { PairNotEmpty } from 'src/custom-validators';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { UserBase } from '../../shared';
import { FinanceAgreement } from '../models/models';
import { VaultAddAccountComponent } from '../vault-add-account/vault-add-account.component';
import { VaultApiService } from '../vault-api.service';
import { LoanViewModel, PropertyViewModel } from '../vault.models';

@Component({
    selector: "app-vault-add-property",
    templateUrl: "./vault-add-property.component.html",
    styleUrls: ["./vault-add-property.component.scss"],
})
export class VaultAddPropertyComponent implements OnInit {
    formAddProperty: FormGroup;
    formAddMortgage: FormGroup;

    validation: ValidationAggregate = new ValidationAggregate();
    automaticallyReValueProperty: boolean = false;
    propertyValueProcessing = false;
    propertyValueAdded = false;
    propertyAddressProcessing = false;
    postcode: string;
    showAddress: boolean;
    itemPhoto: string;

    stepOneHidden: boolean = false;
    stepTwoHidden: boolean = true;
    stepThreeHidden: boolean = true;

    addNewHidden: boolean = false;
    linkExistingHidden: boolean = true;
    hideLoans = true;
    loans: LoanViewModel[] = [];
    loansFiltered: LoanViewModel[] = [];
    searchTerms = new Subject<string>();
    user: UserBase;

    @ViewChild("address") dropdown: ElementRef;

    private postcodeRegex =
        "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$";

    constructor(
        private fb: FormBuilder,
        protected modalService: NgbModal,
        private authService: AuthService,
        protected vaultApiService: VaultApiService,
        protected alertNotificationService: AlertNotificationService,
        private usersApiService: UsersApiService,
        protected eventBusService: EventBusService
    ) { }
    ngOnInit(): void {
        this.user = this.authService.getUserProfile();
        this.formAddProperty = this.createAddPropertyForm();
        this.formAddMortgage = this.fb.group({ mortgageProvider: new FormControl(), mortgageReference: new FormControl(), mortgageBalance: new FormControl(), notes: new FormControl() });
        this.setFormValidation();
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("propertyName", {
            required: "Please enter the property name.",
        });

        this.validation.addValidationMessage("numberOfBedrooms", {
            required: "Please enter the number of bedrooms.",
        });

        this.validation.addValidationMessage("postcode", {
            required: "Please enter the property address.",
            pattern: "Invalid postcode.",
        });

        this.validation.addValidationMessage("propertyValue", {
            required: "Please enter the property value.",
            pattern: "Please enter a positive number."
        });

        this.validation.addValidationMessage("address", {
            required: "Please enter the property address",
        });

        this.validation.addValidationMessage("purchaseValue", {
            pairNotEmpty: "Please insert both purchase value and purchase date.",
        });

        this.validation.addValidationMessage("purchaseDate", {
            pairNotEmpty: "Please insert both purchase value and purchase date.",
        });

        this.validation.addValidationMessage("openBalance", {
            pairNotEmpty: "Please insert both open date and open balance.",
        });

        this.validation.addValidationMessage("openDate", {
            pairNotEmpty: "Please insert both open date and open balance.",
        });

        this.validation.bindValueChangesWithValidator(this.formAddProperty);
    }

    createAddPropertyForm(): FormGroup {
        return this.fb.group({
            propertyName: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            numberOfBedrooms: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            postcode: new FormControl("", [
                Validators.compose([
                    Validators.required,
                    Validators.pattern(this.postcodeRegex),
                ]),
            ]),
            address: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            propertyValue: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            purchaseValue: new FormControl(undefined),
            purchaseDate: new FormControl(undefined),
            automaticallyReValueProperty: new FormControl(),
            searchFinanceAgreement: new FormControl(""),
            newFinanceBalance: new FormControl(""),
            openBalance: new FormControl(undefined),
            openDate: new FormControl(undefined),
            financeAgreementName: new FormControl(""),
            reference: new FormControl(""),
            notes: new FormControl(null),
            financeAgreementId: new FormControl(null),
            existingFinanceBalance: new FormControl(null),
        },
            {
                validator: [PairNotEmpty("purchaseValue", "purchaseDate"),
                PairNotEmpty("openBalance", "openDate")]
            });
    }

    search(term: string) {
        this.searchTerms.next(term);
    }

    showStepOne() {
        this.changeStep(false, true, true);
    }

    showStepTwo() {
        this.formAddProperty.valid
            ? this.changeStep(true, false, true)
            : this.validation.getValidationErrors(this.formAddProperty, true);

        if (this.loans.length === 0) {
            this.vaultApiService.searchLoans(this.user, "").subscribe(
                (data) => {
                    this.loans = data.records;
                    this.loansFiltered = data.records;
                },
                (error) => {
                    console.log(error);
                }
            );
        }
    }

    showStepThree() {
        this.formAddProperty.valid
            ? this.changeStep(true, true, false)
            : this.validation.getValidationErrors(this.formAddProperty, true);
    }

    changeStep(step1: boolean, step2: boolean, step3: boolean) {
        this.stepOneHidden = step1;
        this.stepTwoHidden = step2;
        this.stepThreeHidden = step3;
    }

    verifyPropertyForm(): boolean {
        if (!this.formAddProperty.valid) {
            this.validation.getValidationErrors(this.formAddProperty, true);
            this.formAddProperty.markAsTouched();
            return false;
        }

        return true;
    }

    valueMyProperty(): void {
        if (
            !this.formAddProperty.get("postcode").valid &&
            !this.formAddProperty.get("numberOfBedrooms").valid
        ) {
            this.formAddProperty.markAllAsTouched();
            return;
        }

        this.propertyValueProcessing = true;
        this.vaultApiService
            .getPropertyValue(
                this.formAddProperty.get("postcode").value,
                this.formAddProperty.get("numberOfBedrooms").value
            )
            .subscribe(
                (item) => {
                    this.formAddProperty.patchValue({
                        propertyValue: item.records.value,
                    });
                    this.propertyValueAdded = true;
                    this.propertyValueProcessing = false;
                },
                () => {
                    this.propertyValueProcessing = false;
                    this.alertNotificationService.showWarning({ title: 'We were unable to value property' });
                }
            );
    }

    getAddress(): void {
        if (!this.formAddProperty.get("postcode").value) {
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

        this.postcode = this.formAddProperty.get("postcode").value;
        this.usersApiService.getUserAddress(this.postcode).subscribe(
            (data) => {
                this.showAddress = true;
                for (let location of data.records.locations) {
                    let option = document.createElement("OPTION");
                    let text = document.createTextNode(location);
                    option.appendChild(text);
                    this.dropdown.nativeElement.insertBefore(
                        option,
                        this.dropdown.nativeElement.lastChild
                    );
                }
                this.formAddProperty.controls["address"].patchValue(
                    this.dropdown.nativeElement.lastChild.label
                );
            },
            (error) => {
                console.error(error);
                this.alertNotificationService.showInfo({
                    text: "Sorry, we couldn't find any address for provided postcode.",
                });
            }
        );
    }

    showLoans() {
        this.hideLoans = false;
    }

    selectLoan(loan: LoanViewModel) {
        this.formAddProperty.controls["existingFinanceBalance"].setValue(
            loan.balance
        );
        this.formAddProperty.controls["searchFinanceAgreement"].setValue(
            loan.name
        );
        this.formAddProperty.controls["financeAgreementId"].setValue(loan.id);
        this.hideLoans = true;
    }

    showAddNewFinanceAgreement() {
        this.addNewHidden = false;
        this.linkExistingHidden = true;
    }

    showLinkExistingFinanceAgreement() {
        this.addNewHidden = true;
        this.linkExistingHidden = false;
    }

    showAddAccountModal() {
        this.modalService.open(VaultAddAccountComponent);
    }

    closeModal(): void {
        this.modalService.dismissAll();
    }

    back() {
        this.modalService.dismissAll();
        this.modalService.open(VaultAddAccountComponent);
    }

    addProperty() {
        if (this.formAddProperty.valid) {
            if (this.formAddProperty.controls.propertyValue.value < 0) {
                this.formAddProperty.controls.propertyValue.setValue(
                    this.formAddProperty.controls.propertyValue.value * -1
                );
            }

            this.vaultApiService.addProperty(this.PropertyItemFactory()).subscribe(
                () => {
                    this.alertNotificationService.showSuccess({
                        text: "Property successfully added!",
                    });
                    this.modalService.dismissAll();
                    this.eventBusService.emit(
                        new EmitEvent(Events.VaultRefresh)
                    );
                },
                (error) => {
                    this.alertNotificationService.showWarning({
                        text: error.error.errors
                            ? Object.values(error.error.errors)[0]
                            : error.message
                    });
                }
            )
        }
        else {
            this.alertNotificationService.showWarning({
                text: "Please validate form inputs!",
            });
        }
    }

    PropertyItemFactory() {
        var mortgage: FinanceAgreement;
        if (this.formAddProperty.controls.financeAgreementName.value.length > 0 &&
            this.formAddProperty.controls.financeAgreementName.touched) {
            mortgage = {
                name: this.formAddProperty.controls.financeAgreementName.value,
                balance: this.formAddProperty.controls.newFinanceBalance.value,
                openDate: this.formAddProperty.controls.openDate.value === "" ? null : this.formAddProperty.controls.openDate.value,
                openBalance: this.formAddProperty.controls.openBalance.value,
                notes: this.formAddProperty.controls.notes.value,
                reference: this.formAddProperty.controls.reference.value
            };
        } else {
            mortgage = null;
        }

        var property: PropertyViewModel = {
            userId: this.authService.getUserProfile().id,
            propertyName: this.formAddProperty.controls.propertyName.value,
            numberOfBedrooms: this.formAddProperty.controls.numberOfBedrooms.value,
            postcode: this.formAddProperty.controls.postcode.value,
            address: this.formAddProperty.controls.address.value,
            propertyValue: this.formAddProperty.controls.propertyValue.value,
            purchaseValue: this.formAddProperty.controls.purchaseValue.value,
            purchaseDate: this.formAddProperty.controls.purchaseDate.value === "" ? null : this.formAddProperty.controls.purchaseDate.value,
            propertyPhoto: this.itemPhoto,
            automaticallyReValueProperty: this.automaticallyReValueProperty,
            existingMortgageId:
                this.formAddProperty.controls.financeAgreementId.value,
            mortgage: mortgage,
            propertyId: ''
        };

        return property;
    }

    getPhoto(photo: string) {
        this.itemPhoto = photo;
    }

    private showWarning(error: any) {
        this.alertNotificationService.showWarning({
            text: error.error.errors
                ? error.error.errors[0]
                : error.error.Message,
        });
    }
}
