import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventBusService } from 'src/app/core/events/event-bus.service';
import { EmitEvent } from 'src/app/core/models/emit-event';
import { Events } from 'src/app/core/models/events';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { UsersApiService } from 'src/app/users/users-api.service';

import { VaultApiService } from '../../vault-api.service';
import {
    VaultManualAccountDetailsComponent,
} from '../../vault-manual-account-details/vault-manual-account-details.component';
import { EditPropertyViewModel, VaultAssetType } from '../../vault.models';

@Component({
    selector: "app-vault-edit-property",
    templateUrl: "./vault-edit-property.component.html",
    styleUrls: ["./vault-edit-property.component.scss"],
})
export class VaultEditPropertyComponent implements OnInit {
    accountId: string;
    formEditProperty: FormGroup;

    validation: ValidationAggregate = new ValidationAggregate();
    automaticallyReValueProperty: boolean = false;
    showAddress: boolean;
    propertyValueProcessing = false;
    propertyValueAdded = false;
    propertyAddressProcessing = false;
    postcode: string;
    hasFinanceAgreement = true;
    itemPhoto: string;

    private postcodeRegex =
        "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$";

    @ViewChild("address") dropdown: ElementRef;

    constructor(
        private fb: FormBuilder,
        private modalService: NgbModal,
        private vaultApiService: VaultApiService,
        private alertNotificationService: AlertNotificationService,
        private usersApiService: UsersApiService,
        private eventBusService: EventBusService
    ) {}

    ngOnInit(): void {
        this.formEditProperty = this.createAddPropertyForm();
        this.setFormValidation();
        this.showAddress = true;
        this.vaultApiService.getPropertyDetails(this.accountId).subscribe(
            (item) => {
                this.formEditProperty.patchValue(item.records);
                this.getAddress(item.records.address);
                if (item.records.financeAgreementName.length < 1) {
                    this.hasFinanceAgreement = false;
                    this.formEditProperty.controls.newFinanceBalance.setValue(0);
                }
            },
            (error) => {
                console.log(error);
                this.alertNotificationService.showWarning({
                    title: "Failed to find property",
                    text: "Failed to find property",
                });
            }
        );
    }

    backToPropertyDetails() {
        this.modalService.dismissAll();
        const instance = this.modalService.open(
            VaultManualAccountDetailsComponent
        );
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemType = VaultAssetType.Property;
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
            pattern: "Please enter a positive number.",
        });
        this.validation.addValidationMessage("address", {
            required: "Please enter the property address",
        });

        this.validation.bindValueChangesWithValidator(this.formEditProperty);
    }

    createAddPropertyForm(): FormGroup {
        return this.fb.group({
            propertyName: new FormControl({ value: "" }, [
                Validators.compose([Validators.required]),
            ]),
            numberOfBedrooms: new FormControl({ value: 0 }, [
                Validators.compose([Validators.required]),
            ]),
            postcode: new FormControl({ value: "" }, [
                Validators.compose([
                    Validators.required,
                    Validators.pattern(this.postcodeRegex),
                ]),
            ]),
            address: new FormControl({ value: "" }, [
                Validators.compose([Validators.required]),
            ]),
            propertyValue: new FormControl(0, [
                Validators.compose([Validators.required]),
            ]),
            automaticallyReValueProperty: new FormControl(),
            newFinanceBalance: new FormControl({ value: 0 }),
            financeAgreementName: new FormControl({ value: "" }),
            reference: new FormControl({ value: "" }),
            notes: new FormControl({ value: "" }),
        });
    }

    updateProperty() {
        if (!this.formEditProperty.valid) {
            this.formEditProperty.markAllAsTouched();
            this.validation.getValidationErrors(this.formEditProperty, true);
            return;
        }

        if (this.formEditProperty.controls.propertyValue.value < 0) {
            this.formEditProperty.controls.propertyValue.setValue(
                this.formEditProperty.controls.propertyValue.value * -1
            );
        }

        const property: EditPropertyViewModel =
            this.formEditProperty.getRawValue();
        property.propertyId = this.accountId;
        property.propertyValueChange =
            this.formEditProperty.controls.propertyValue.dirty;
        property.mortgageBalanceChange =
            this.formEditProperty.controls.newFinanceBalance.dirty;
        property.propertyPhoto = this.itemPhoto;

        this.vaultApiService.updateProperty(property).subscribe(
            () => {
                this.alertNotificationService.showSuccess({
                    title: "Property Updated Successfully",
                });
                this.modalService.dismissAll();
                this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
            },
            (error) => {
                console.log(error);
                this.alertNotificationService.showWarning({
                    title: "Failed to find property",
                    text: "Failed to find property",
                });
            }
        );
    }

    valueMyProperty(): void {
        if (
            !this.formEditProperty.get("postcode").valid &&
            !this.formEditProperty.get("numberOfBedrooms").valid
        ) {
            this.formEditProperty.markAllAsTouched();
            return;
        }

        if (this.formEditProperty.controls.propertyValue.value < 0) {
            this.formEditProperty.controls.propertyValue.setValue(
                this.formEditProperty.controls.propertyValue.value * -1
            );
        }

        this.propertyValueProcessing = true;
        this.vaultApiService
            .getPropertyValue(
                this.formEditProperty.get("postcode").value,
                this.formEditProperty.get("numberOfBedrooms").value
            )
            .subscribe(
                (item) => {
                    var currentValue = this.formEditProperty.controls.propertyValue.value;
                    this.formEditProperty.patchValue({
                        propertyValue: item.records.value,
                    });
                    this.propertyValueAdded = true;
                    this.propertyValueProcessing = false;
                    if(currentValue != item.records.value)
                    {
                        this.formEditProperty.controls.propertyValue.markAsDirty();
                    }
                },
                () => {
                    this.propertyValueProcessing = false;
                    this.alertNotificationService.showWarning({
                        title: "We were unable to value property",
                    });
                }
            );
    }

    getAddress(currentAddress: string = null): void {
        if (!this.formEditProperty.get("postcode").value) {
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

        this.postcode = this.formEditProperty.get("postcode").value;
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

                if (currentAddress != null) {
                    this.formEditProperty.controls.address.patchValue(
                        currentAddress
                    );
                }
            },
            (error) => {
                console.error(error);
                this.alertNotificationService.showInfo({
                    text: "Sorry, we couldn't find any address for provided postcode.",
                });
            }
        );
    }

    getPhoto(photo: string) {
        this.itemPhoto = photo;
    }


    closeModal(): void {
        this.modalService.dismissAll();
    }
}
