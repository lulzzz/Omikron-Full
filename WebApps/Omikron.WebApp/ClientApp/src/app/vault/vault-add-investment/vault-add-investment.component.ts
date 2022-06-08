import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { ValidationAggregate } from '../../core/utilities/validation-aggregate';
import { AuthService } from '../../shared/auth.service';
import { VaultAddAccountComponent } from '../vault-add-account/vault-add-account.component';
import { VaultApiService } from '../vault-api.service';

@Component({
    selector: "app-vault-add-investment",
    templateUrl: "./vault-add-investment.component.html",
    styleUrls: ["./vault-add-investment.component.scss"],
})
export class VaultAddInvestmentComponent implements OnInit {
    formAddInvestment: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    automaticallyRevalueInvestment: boolean = false;

    constructor(
        private fb: FormBuilder,
        protected modalService: NgbModal,
        protected vaultApiService: VaultApiService,
        private authService: AuthService,
        protected alertNotificationService: AlertNotificationService,
        protected eventBusService : EventBusService
    ) {}

    ngOnInit(): void {
        this.formAddInvestment = this.createAddInvestmentForm();
        this.setFormValidation();
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("investmentName", {
            required: "Please enter the investment name.",
        });

        this.validation.addValidationMessage("unitPrice", {
            required: "Please enter the unit price.",
        });

        this.validation.addValidationMessage("quantity", {
            required: "Please enter the quantity.",
            pattern: "Please enter a positive number.",
        });

        this.validation.addValidationMessage("purchaseValue", {
            pairNotEmpty: "Please insert both purchase value and purchase date.",
        });

        this.validation.addValidationMessage("purchaseDate", {
            pairNotEmpty: "Please insert both purchase value and purchase date.",
        });

        this.validation.bindValueChangesWithValidator(this.formAddInvestment);
    }

    createAddInvestmentForm(): FormGroup {
        return this.fb.group({
            investmentName: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            category: new FormControl(""),
            tickerCode: new FormControl(""),
            unitPrice: new FormControl("", [
                Validators.compose([Validators.required])
            ]),
            quantity: new FormControl("", [
                Validators.compose([Validators.required]),
                Validators.pattern("(^[0-9]+.?([0-9]+)*$)")]),
            totalValue: new FormControl(0),
            automaticallyRevalueInvestment: new FormControl(false),
            ownerId: new FormControl(""),
            purchaseValue: new FormControl(undefined),
            purchaseDate: new FormControl(undefined)
        });
    }

    addInvestment(): void {
        if (this.formAddInvestment.valid) {

            if(this.formAddInvestment.controls.unitPrice.value < 0)
            {
                this.formAddInvestment.controls.unitPrice.setValue(this.formAddInvestment.controls.unitPrice.value * -1)
            }

            this.formAddInvestment.controls.totalValue.setValue(
                this.formAddInvestment.controls.unitPrice.value *
                    this.formAddInvestment.controls.quantity.value
            );
            var user = this.authService.getUserProfile();
            this.formAddInvestment.controls.ownerId.setValue(user.id);
            this.vaultApiService
                .addInvestment(this.formAddInvestment.value)
                .subscribe(
                    (data) => {
                        this.alertNotificationService.showSuccess({text: "Investment successfully added!",});
                        this.modalService.dismissAll();
                        this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
                    },
                    (error) => {
                        this.alertNotificationService.showWarning({
                            text: error.error.errors
                                ? Object.values(error.error.errors)[0]
                                : error.message
                        });
                    }
                );
        } else {
            this.validation.getValidationErrors(this.formAddInvestment, true);
            this.formAddInvestment.markAsTouched();
        }
    }

    closeModal(): void {
        this.modalService.dismissAll();
    }

    showAddAccountModal() {
        this.modalService.open(VaultAddAccountComponent);
    }
}
