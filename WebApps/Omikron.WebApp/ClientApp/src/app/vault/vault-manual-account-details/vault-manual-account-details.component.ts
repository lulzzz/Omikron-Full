import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { VaultAccountDeleteService } from '../vault-account-delete-service';
import { VaultEditInvestmentComponent } from '../vault-add-investment/vault-edit-investment/vault-edit-investment.component';
import {
    VaultEditPersonalItemComponent,
} from '../vault-add-personal-item/vault-edit-personal-item/vault-edit-personal-item.component';
import { VaultEditPropertyComponent } from '../vault-add-property/vault-edit-property/vault-edit-property.component';
import { VaultEditVehicleComponent } from '../vault-add-vehicle/vault-edit-vehicle/vault-edit-vehicle.component';
import { VaultApiService } from '../vault-api.service';
import {
    VaultDeleteVerificationPromptComponent,
} from '../vault-delete-verification-prompt/vault-delete-verification-prompt.component';
import { VaultEditManualAccountComponent } from '../vault-edit-manual-account/vault-edit-manual-account.component';
import { ManualAccountDetailsVieWModel, VaultAssetType } from '../vault.models';
import { AlertNotificationService } from './../../overlay/services/alert-notification.service';

@Component({
    selector: "app-vault-manual-account-details",
    templateUrl: "./vault-manual-account-details.component.html",
    styleUrls: ["./vault-manual-account-details.component.scss"],
})
export class VaultManualAccountDetailsComponent implements OnInit {
    @Input() accountId: string;
    @Input() itemType: VaultAssetType;
    @Input() financeId: string = "";
    isBusy: boolean;

    accountDetails: ManualAccountDetailsVieWModel;
    showSeeMoreButton: boolean;

    private editComponents: { [key: string]: any } = {
        Property: VaultEditPropertyComponent,
        Properties: VaultEditPropertyComponent,
        Mortgage: VaultEditPropertyComponent,
        Vehicle: VaultEditVehicleComponent,
        Vehicles: VaultEditVehicleComponent,
        VehicleFinance: VaultEditVehicleComponent,
        PersonalItem: VaultEditPersonalItemComponent,
        PersonalItems: VaultEditPersonalItemComponent,
        Investment: VaultEditInvestmentComponent,
        Account: VaultEditManualAccountComponent,
    };

    constructor(
        private cd: ChangeDetectorRef,
        private modalService: NgbModal,
        private vaultApi: VaultApiService,
        private vaultAccountDeleteService: VaultAccountDeleteService,
        private alertNotificationService: AlertNotificationService
    ) {}

    ngOnInit(): void {
        this.setBusy(true);
        this.vaultApi
            .getManualAccountDetails(
                this.accountId,
                this.itemType,
                this.financeId
            )
            .subscribe(
                (item) => {
                    this.accountDetails = item.records
                    this.setBusy(false);
                },
                (error) => {
                    this.alertNotificationService.showWarning({
                        title: "We were unable to display the account. Please try again",
                        text: error.error.errors
                            ? error.error.errors[0]
                            : error.error.Message,
                    });
                }
            );
    }

    verifyAccountDelete() {
        this.vaultAccountDeleteService.accountId = this.accountId;
        this.vaultAccountDeleteService.accountType = this.itemType;
        this.modalService.open(VaultDeleteVerificationPromptComponent);
    }

    showFinance() {
        this.modalService.dismissAll();
        const instance = this.modalService.open(
            VaultManualAccountDetailsComponent
        );
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemType = this.accountDetails.financeType;
        instance.componentInstance.financeId = this.accountDetails.financeId;
    }

    closeModal() {
        this.modalService.dismissAll();
    }

    editAccount() {
        if(this.itemType.includes(VaultAssetType.PersonalItemFinance) ||
           this.itemType.includes(VaultAssetType.VehicleFinance) ||
           this.itemType.includes(VaultAssetType.Mortgage))
        {
            this.itemType = VaultAssetType.Account;
            this.accountId = this.financeId;
        }
        const component = this.editComponents[this.itemType];
        this.modalService.dismissAll();
        const instance = this.modalService.open(component);
        instance.componentInstance.accountId = this.accountId;
        instance.componentInstance.itemPhoto = this.accountDetails.itemPhoto;
    }

    private setBusy(isBusy: boolean): void {
        this.isBusy = isBusy;
        this.cd.markForCheck();
    }
}
