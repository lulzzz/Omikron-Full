import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventBusService } from 'src/app/core/events/event-bus.service';
import { EmitEvent } from 'src/app/core/models/emit-event';
import { Events } from 'src/app/core/models/events';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';

import { PersonalItemViewModel } from '../../models/models';
import { VaultAddAccountComponent } from '../../vault-add-account/vault-add-account.component';
import { VaultApiService } from '../../vault-api.service';
import {
    VaultManualAccountDetailsComponent,
} from '../../vault-manual-account-details/vault-manual-account-details.component';
import { VaultAssetType } from '../../vault.models';

@Component({
    selector: "app-vault-edit-personal-item",
    templateUrl: "./vault-edit-personal-item.component.html",
    styleUrls: ["./vault-edit-personal-item.component.scss"],
})
export class VaultEditPersonalItemComponent implements OnInit {
    accountId: string;
    personalItemForm: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    hasFinance: number;
    itemPhoto: string;
    constructor(
        private modalService: NgbModal,
        private fb: FormBuilder,
        private vaultApiService: VaultApiService,
        private alertNotificationService: AlertNotificationService,
        private eventBusService: EventBusService
    ) {}

    ngOnInit(): void {
        this.personalItemForm = this.createPersonalItemDetailsForm();
        this.setFormValidation();
        this.vaultApiService.getPersonalItemDetails(this.accountId).subscribe(
            (item) => {
                this.personalItemForm.patchValue(item.records);
                this.hasFinance = item.records.newFinanceBalance;
                if (this.hasFinance == undefined) {
                    this.personalItemForm.controls.newFinanceBalance.setValue(0);
                }
            },
            (error) => {
                this.alertNotificationService.showWarning({
                    title: "Failed to find personal item",
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    createPersonalItemDetailsForm(): FormGroup {
        return this.fb.group({
            itemName: new FormControl(
                { value: "" },
                Validators.compose([Validators.required])
            ),
            value: new FormControl(
                { value: "" },
                Validators.compose([Validators.required])
            ),
            financeAgreementName: new FormControl({ value: ""}),
            newFinanceBalance: new FormControl({ value: 0 }),
            notes: new FormControl(""),
        });
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("itemName", {
            required: "Please enter item name.",
        });

        this.validation.addValidationMessage("value", {
            required: "Please enter your item value.",
            pattern: "Please enter a positive number.",
        });

        this.validation.bindValueChangesWithValidator(this.personalItemForm);
    }

    updatePersonalItem() {
        if (!this.personalItemForm.valid) {
            this.personalItemForm.markAllAsTouched();
            this.validation.getValidationErrors(this.personalItemForm, true);
            return;
        }

        if (this.personalItemForm.controls.value.value < 0) {
            this.personalItemForm.controls.value.setValue(
                this.personalItemForm.controls.value.value * -1
            );
        }

        const personalItem: PersonalItemViewModel =
            this.personalItemForm.getRawValue();
        personalItem.personalItemValueChange =
            this.personalItemForm.controls["value"].dirty;
        personalItem.financeBalanceChange =
            this.personalItemForm.controls["newFinanceBalance"].dirty;
        personalItem.personalItemId = this.accountId;
        personalItem.itemPhoto = this.itemPhoto;

        this.vaultApiService.updatePersonalItem(personalItem).subscribe(
            () => {
                this.alertNotificationService.showSuccess({
                    title: "Personal Item Updated Successfully",
                });
                this.modalService.dismissAll();
                this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
            },
            (error) => {
                this.alertNotificationService.showWarning({
                    title: "Failed to update personal item",
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    showAddAccountModal() {
        this.modalService.dismissAll();
        this.modalService.open(VaultAddAccountComponent);
    }

    closeModal() {
        this.modalService.dismissAll();
    }

    getPhoto(photo: string) {
        this.itemPhoto = photo;
    }

    backToPersonalItemDetails() {
        this.modalService.dismissAll();
        const instance = this.modalService.open(
            VaultManualAccountDetailsComponent
        );
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemType = VaultAssetType.PersonalItem;
    }
}
