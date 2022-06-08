import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import {
    AddAccountManuallyComponent,
} from '../../security/security-register/add-account-manually/add-account-manually.component';
import { EditManualAccount } from '../models/models';
import { VaultApiService } from '../vault-api.service';
import { VaultManualAccountDetailsComponent } from '../vault-manual-account-details/vault-manual-account-details.component';
import { CreditDebitIntIndicator, EditAccountType, VaultAssetType } from '../vault.models';
import { EventBusService } from './../../core/events/event-bus.service';
import { AlertNotificationService } from './../../overlay/services/alert-notification.service';
import { AuthService } from './../../shared/auth.service';
import { UsersApiService } from './../../users/users-api.service';

@Component({
    selector: "app-vault-edit-manual-account",
    templateUrl: "./vault-edit-manual-account.component.html",
    styleUrls: ["./vault-edit-manual-account.component.scss"],
})
export class VaultEditManualAccountComponent
    extends AddAccountManuallyComponent
    implements OnInit
{
    accountId: string;
    accountType: string;

    constructor(
        fb: FormBuilder,
        modalService: NgbModal,
        vaultApiService: VaultApiService,
        alertNotificationService: AlertNotificationService,
        usersApiService: UsersApiService,
        authService: AuthService,
        router: Router,
        eventBusService: EventBusService
    ) {
        super(
            fb,
            modalService,
            vaultApiService,
            alertNotificationService,
            usersApiService,
            authService,
            router,
            eventBusService
        );
    }

    ngOnInit(): void {
        super.ngOnInit();
        this.vaultApiService
            .getManualEditAccountDetails(this.accountId)
            .subscribe(
                (item) => {
                    this.addAccountForm.patchValue(item.records);
                    this.addAccountForm.patchValue({
                        accountType: item.records.type,
                    });
                    this.accountType =
                        EditAccountType[item.records.type - 1];
                },
                (error) => {
                    this.alertNotificationService.showWarning({
                        title: "Failed to find the account",
                        text: error.error.errors
                            ? error.error.errors[0]
                            : error.error.Message,
                    });
                }
            );
    }

    updateAccount() {
        if (!this.addAccountForm.valid) {
            this.addAccountForm.markAllAsTouched();
            this.validation.getValidationErrors(this.addAccountForm, true);
            return;
        }

        if(this.addAccountForm.controls.balance.value < 0)
        {
            this.addAccountForm.controls.balance.setValue(this.addAccountForm.controls.balance.value * -1)
            this.addAccountForm.controls.creditDebitIndicator.setValue(CreditDebitIntIndicator.Debit)
        }
        else{
            this.addAccountForm.controls.creditDebitIndicator.setValue(CreditDebitIntIndicator.Credit)
        }

        const account: EditManualAccount = this.addAccountForm.value;
        account.accountBalanceChanged =
            this.addAccountForm.controls.balance.dirty;
        account.accountId = this.accountId;

        this.vaultApiService.updateManualAccount(account).subscribe(
            () => {
                this.alertNotificationService.showSuccess({
                    title: "Account Updated Successfully",
                });
                this.modalService.dismissAll();
                this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
            },
            (error) => {
                this.alertNotificationService.showWarning({
                    title: "Failed to update the account",
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    back() {
        this.modalService.dismissAll();
        const instance = this.modalService.open(
            VaultManualAccountDetailsComponent
        );
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemType = VaultAssetType.Account;
        //TODO Refactor this function to support multiple loan options.
        // Loan options -> PersonalItemFinance, VehicleFinance, Mortgage. Let's try to do this in 1.2 release
    }
}
