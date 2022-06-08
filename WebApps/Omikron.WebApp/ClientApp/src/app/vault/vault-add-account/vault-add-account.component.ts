import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { NavigateUrl } from '../../security/models/ObProvider';
import {
    AddAccountManuallyComponent,
} from '../../security/security-register/add-account-manually/add-account-manually.component';
import { VaultAddInvestmentComponent } from '../vault-add-investment/vault-add-investment.component';
import { VaultAddObAccountComponent } from '../vault-add-ob-account/vault-add-ob-account.component';
import { VaultAddPersonalItemComponent } from '../vault-add-personal-item/vault-add-personal-item.component';
import { VaultAddPropertyComponent } from '../vault-add-property/vault-add-property.component';
import { VaultAddVehicleComponent } from '../vault-add-vehicle/vault-add-vehicle.component';

@Component({
    selector: "app-vault-add-account",
    templateUrl: "./vault-add-account.component.html",
    styleUrls: ["./vault-add-account.component.scss"],
})
export class VaultAddAccountComponent implements OnInit {
    constructor(private modalService: NgbModal) {}
    VaultAddObAccountComponent = VaultAddObAccountComponent;
    VaultAddVehicleComponent = VaultAddVehicleComponent;
    VaultAddPersonalItemComponent = VaultAddPersonalItemComponent;
    VaultAddPropertyComponent = VaultAddPropertyComponent;
    VaultAddInvestmentComponent = VaultAddInvestmentComponent;

    ngOnInit(): void {}

    hideModal() {
        this.modalService.dismissAll();
    }

    addAccount(component : any)
    {
        this.modalService.dismissAll();
        this.modalService.open(component);
    }

    addAccountManually() {
        const modalRef = this.modalService.open(AddAccountManuallyComponent);
        modalRef.componentInstance.navigateUrl = NavigateUrl.vault;
        modalRef.componentInstance.isLoanOrPension = true;
    }
}
