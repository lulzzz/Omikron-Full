import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventBusService } from 'src/app/core/events/event-bus.service';
import { EmitEvent } from 'src/app/core/models/emit-event';
import { Events } from 'src/app/core/models/events';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';

import { EditInvestment } from '../../models/models';
import { VaultAddAccountComponent } from '../../vault-add-account/vault-add-account.component';
import { VaultApiService } from '../../vault-api.service';
import {
    VaultManualAccountDetailsComponent,
} from '../../vault-manual-account-details/vault-manual-account-details.component';
import { VaultAssetType } from '../../vault.models';

@Component({
    selector: "app-vault-edit-investment",
    templateUrl: "./vault-edit-investment.component.html",
    styleUrls: ["./vault-edit-investment.component.scss"],
})
export class VaultEditInvestmentComponent implements OnInit {
    accountId: string;
    private originalValue: number;

    constructor(
        private fb: FormBuilder,
        private modalService: NgbModal,
        private vaultApiService: VaultApiService,
        private alertNotificationService: AlertNotificationService,
        private eventBusService: EventBusService
    ) {}

    formEditInvestment: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    automaticallyRevalueInvestment: boolean = false;

    ngOnInit(): void {
        this.formEditInvestment = this.createEditInvestmentForm();
        this.setFormValidation();

        this.vaultApiService.getInvestmentDetails(this.accountId).subscribe(
            (item) => {
                this.formEditInvestment.patchValue(item.records);
                this.originalValue = item.records.totalValue;
            },
            (error) => {
                console.log(error);
                this.alertNotificationService.showWarning({
                    title: "Failed to find investment",
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("investmentName", {
            required: "Please enter the investment name.",
        });
        this.validation.addValidationMessage("unitPrice", {
            required: "Please enter the unit price.",
            pattern: "Please enter a positive number.",
        });
        this.validation.addValidationMessage("quantity", {
            required: "Please enter the quantity.",
            pattern: "Please enter a positive number.",
        });

        this.validation.bindValueChangesWithValidator(this.formEditInvestment);
    }

    createEditInvestmentForm(): FormGroup {
        return this.fb.group({
            investmentName: new FormControl({ value: "", disabled: true }, [
                Validators.compose([Validators.required]),
            ]),
            category: new FormControl(""),
            tickerCode: new FormControl(""),
            unitPrice: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            quantity: new FormControl("", [
                Validators.compose([Validators.required]),
                Validators.pattern("(^[0-9]+.?([0-9]+)*$)")]),
            totalValue: new FormControl(0),
            automaticallyRevalueInvestment: new FormControl(false),
        });
    }

    updateInvestment() {
        if (!this.formEditInvestment.valid) {
            this.formEditInvestment.markAllAsTouched();
            this.validation.getValidationErrors(this.formEditInvestment, true);
            return;
        }

        if (this.formEditInvestment.controls.unitPrice.value < 0) {
            this.formEditInvestment.controls.unitPrice.setValue(
                this.formEditInvestment.controls.unitPrice.value * -1
            );
        }

        this.formEditInvestment.controls.totalValue.setValue(
            this.formEditInvestment.controls.unitPrice.value *
                this.formEditInvestment.controls.quantity.value
        );

        const investment: EditInvestment =
            this.formEditInvestment.getRawValue();
        investment.investmentId = this.accountId;

        investment.investmentValueChanged =
            this.formEditInvestment.controls.totalValue.value !==
            this.originalValue;

        this.vaultApiService.updateInvestment(investment).subscribe(
            () => {
                this.alertNotificationService.showSuccess({
                    title: "Investment Updated Successfully",
                });
                this.modalService.dismissAll();
                this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
            },
            (error) => {
                console.log(error);
                this.alertNotificationService.showWarning({
                    title: "Failed to update investment",
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    closeModal(): void {
        this.modalService.dismissAll();
    }

    showAddAccountModal() {
        this.modalService.open(VaultAddAccountComponent);
    }

    backToInvestmentDetails() {
        this.modalService.dismissAll();
        const instance = this.modalService.open(
            VaultManualAccountDetailsComponent
        );
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemType = VaultAssetType.Investment;
    }
}
