import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { AuthService } from 'src/app/shared/auth.service';

import { EventBusService } from '../../core/events/event-bus.service';
import { Events } from '../../core/models/events';
import { VaultAccountDetailsComponent } from '../vault-account-details/vault-account-details.component';
import { VaultManualAccountDetailsComponent } from '../vault-manual-account-details/vault-manual-account-details.component';
import { AccountSource, AccountType, VaultItemType } from '../vault.models';
import { VaultApiService } from './../vault-api.service';
import { VaultAssetType, VaultViewModel } from './../vault.models';

@Component({
    selector: "app-vault-content",
    templateUrl: "./vault-content.component.html",
    styleUrls: ["./vault-content.component.scss"],
})
export class VaultContentComponent implements OnInit {
    vaultModel: VaultViewModel;
    eventBusSub: Subscription;
    vaultItemType = VaultItemType;
    @Output() vaultBalance = new EventEmitter();

    vaultItemTypes: { [key: string]: string } = {
        vehicles: "Vehciles",
        Property: "Property",
    };

    constructor(
        private vaultApiService: VaultApiService,
        private authService: AuthService,
        private modalService: NgbModal,
        private alertNotificationService: AlertNotificationService,
        private eventBusService: EventBusService
    ) {}

    ngOnInit(): void {
        this.getVault();

        this.eventBusSub = this.eventBusService.on(Events.VaultRefresh, () => {
            this.getVault();
        });
    }

    getVault(): void {
        this.vaultApiService
            .getVault(this.authService.getUserProfile())
            .subscribe(
                (data) => {
                    this.vaultModel = data.records;

                    let totalVaultBalance = 0;
                    data.records.accounts
                        .map((a) => a.total)
                        .concat(data.records.assets.map((a) => a.total))
                        .forEach((s) => (totalVaultBalance += s));

                    this.vaultBalance.emit(totalVaultBalance);
                },
                (error) => {
                    console.error(error);
                }
            );
    }

    getAccountDetails(
        accountId: string,
        accountSource: string,
        assetType: VaultAssetType,
        accountType: AccountType
    ) {
        if (accountSource === AccountSource.BudApi) {
            this.openModal(accountId, VaultAccountDetailsComponent);
        } else{
            if (accountType == AccountType.Loans) {
                this.vaultApiService.getManualAccount(accountId).subscribe(
                    (data) => {
                        if (data.records.loanType != undefined) {
                            const instance = this.modalService.open(
                                VaultManualAccountDetailsComponent
                            );
                            instance.componentInstance.accountId = data.records.assetId;
                            instance.componentInstance.itemType = data.records.assetType;
                            instance.componentInstance.financeId = accountId;
                        } else {
                            this.openModal(accountId, VaultManualAccountDetailsComponent, assetType);
                        }
                    },
                    (error) => {
                        this.alertNotificationService.showInfo({
                            text: "We experienced some issue while getting your loan account.",
                        });
                    }
                );
            } else {
                this.openModal(
                    accountId,
                    VaultManualAccountDetailsComponent,
                    assetType
                );
            }
        }
    }

    getManualAccountDetails(accountId: string, itemType: VaultAssetType) {
        if (!VaultAssetType[itemType]) {
            this.alertNotificationService.showInfo({
                text: "This is not supported yet",
            });
            return;
        }

        this.openModal(accountId, VaultManualAccountDetailsComponent, itemType);
    }

    openModal<T>(accountId: string, item: T, itemType: VaultAssetType = null) {
        const modalRef = this.modalService.open(item);
        modalRef.componentInstance.accountId = accountId;
        if (itemType) {
            modalRef.componentInstance.itemType = itemType;
        }
    }

    ngOnDestroy(): void {
        this.eventBusSub.unsubscribe();
    }
}
