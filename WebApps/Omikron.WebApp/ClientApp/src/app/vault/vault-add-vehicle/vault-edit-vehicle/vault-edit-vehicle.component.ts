import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventBusService } from 'src/app/core/events/event-bus.service';
import { EmitEvent } from 'src/app/core/models/emit-event';
import { Events } from 'src/app/core/models/events';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';

import { EditVehicle } from '../../models/models';
import { VaultApiService } from '../../vault-api.service';
import {
    VaultManualAccountDetailsComponent,
} from '../../vault-manual-account-details/vault-manual-account-details.component';
import { VaultAssetType } from '../../vault.models';

@Component({
    selector: "app-vault-edit-vehicle",
    templateUrl: "./vault-edit-vehicle.component.html",
    styleUrls: ["./vault-edit-vehicle.component.scss"],
})
export class VaultEditVehicleComponent implements OnInit {
    accountId: string;
    formEditVehicle: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    automaticallyReValueVehicle: boolean = false;
    vehicleValueProcessing = false;
    vehicleValueReturned = false;
    hasFinanceAgreement = true;
    stepOneHidden = false;
    itemPhoto: string;

    constructor(
        private fb: FormBuilder,
        private modalService: NgbModal,
        private vaultApiService: VaultApiService,
        private alertNotificationService: AlertNotificationService,
        private eventBusService: EventBusService
    ) {}

    ngOnInit(): void {
        this.formEditVehicle = this.createAddVehicleForm();
        this.setFormValidation();

        this.vaultApiService.getVehicleDetails(this.accountId).subscribe(
            (item) => {
                this.formEditVehicle.patchValue(item.records);
                if(item.records.financeAgreementName.length < 1)
                {
                    this.hasFinanceAgreement = false;
                    this.formEditVehicle.controls.newFinanceBalance.setValue(0);
                }
            },
            (error) => {
                console.log(error);
                this.alertNotificationService.showWarning({
                    title: "Failed to find vehicle",
                    text: "Failed to find vehicle",
                });
            }
        );
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("vehicleName", {
            required: "Please enter the vehicle name.",
        });
        this.validation.addValidationMessage("registration", {
            required: "Please enter the registration.",
        });
        this.validation.addValidationMessage("mileage", {
            required: "Please enter the mileage.",
        });
        this.validation.addValidationMessage("vehicleValue", {
            required: "Please enter the vehicle value.",
            pattern: "Please enter a positive number.",
        });

        this.validation.bindValueChangesWithValidator(this.formEditVehicle);
    }

    createAddVehicleForm(): FormGroup {
        return this.fb.group({
            vehicleName: new FormControl(
                { value: "" },
                Validators.compose([Validators.required])
            ),
            registration: new FormControl({ value: "" }, [
                Validators.compose([Validators.required]),
            ]),
            mileage: new FormControl(
                { value: 0 },
                Validators.compose([Validators.required])
            ),
            vehicleValue: new FormControl(
                0,
                Validators.compose([Validators.required])
            ),
            financeAgreementName: new FormControl({value: ""}),
            newFinanceBalance: new FormControl({ value: 0 }),
            automaticallyReValueVehicle: new FormControl(false),
            notes: new FormControl({ value: "" }),
            reference: new FormControl({ value: "" }),
        });
    }

    valueMyVehicle(): void {
        if (
            !this.formEditVehicle.get("registration").valid ||
            !this.formEditVehicle.get("mileage").valid
        ) {
            this.validation.getValidationErrors(this.formEditVehicle, true);
            this.formEditVehicle.markAsTouched();

            return;
        }

        this.vehicleValueProcessing = true;

        this.vaultApiService
            .getVehicleValue(
                this.formEditVehicle.get("registration").value,
                this.formEditVehicle.get("mileage").value
            )
            .subscribe(
                (item) => {
                    var currentValue = this.formEditVehicle.controls.propertyValue.value;
                    this.formEditVehicle.patchValue({
                        vehicleValue: item.records.value,
                    });
                    this.vehicleValueProcessing = false;
                    this.vehicleValueReturned = true;
                    if(currentValue != item.records.value)
                    {
                        this.formEditVehicle.controls.vehicleValue.markAsDirty();
                    }
                },
                (error) => {
                    this.alertNotificationService.showWarning({
                        title: "We were unable to value your vehicle",
                    });
                    console.log(error);
                    this.vehicleValueProcessing = false;
                }
            );
    }

    showStepOne() {
        this.changeStep(false, true, true);
    }
    changeStep(step1: boolean, step2: boolean, step3: boolean) {
        this.stepOneHidden = step1;
    }

    closeModal(): void {
        this.modalService.dismissAll();
    }

    updateVehicle() {
        if (!this.formEditVehicle.valid) {
            this.formEditVehicle.markAllAsTouched();
            this.validation.getValidationErrors(this.formEditVehicle, true);
            return;
        }

        if (this.formEditVehicle.controls.vehicleValue.value < 0) {
            this.formEditVehicle.controls.vehicleValue.setValue(
                this.formEditVehicle.controls.vehicleValue.value * -1
            );
        }

        const vehicle: EditVehicle = this.formEditVehicle.getRawValue();
        vehicle.financeBalanceChanged =
            this.formEditVehicle.controls.newFinanceBalance.dirty;
        vehicle.vehicleValueChanged =
            this.formEditVehicle.controls.vehicleValue.dirty;

        vehicle.vehicleId = this.accountId;
        vehicle.vehiclePhoto = this.itemPhoto;

        this.vaultApiService.updateVehicle(vehicle).subscribe(
            () => {
                this.alertNotificationService.showSuccess({
                    title: "Vehicle Updated Successfully",
                });
                this.modalService.dismissAll();
                this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
            },
            (error) => {
                this.alertNotificationService.showWarning({
                    title: "Failed to update vehicle",
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    getPhoto(photo: string) {
        this.itemPhoto = photo;
    }

    backToVehicleDetails() {
        this.modalService.dismissAll();
        const instance = this.modalService.open(
            VaultManualAccountDetailsComponent
        );
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemType = VaultAssetType.Vehicle;
    }
}
