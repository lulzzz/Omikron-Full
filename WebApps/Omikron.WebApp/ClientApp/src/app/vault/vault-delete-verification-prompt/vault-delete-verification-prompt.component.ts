import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { VaultAccountDeleteService } from '../vault-account-delete-service';
import { VaultApiService } from '../vault-api.service';
import { VaultAssetType } from '../vault.models';

@Component({
    selector: "app-vault-delete-verification-prompt",
    templateUrl: "./vault-delete-verification-prompt.component.html",
    styleUrls: ["./vault-delete-verification-prompt.component.scss"],
})
export class VaultDeleteVerificationPromptComponent implements OnInit {
    buttonDisabled: boolean = false;

    constructor(
        private serviceModal: NgbModal,
        private vaultApiService: VaultApiService,
        private alertNotificationService: AlertNotificationService,
        private eventBusService: EventBusService,
        private modalService: NgbModal,
        private vaultAccountDeleteService: VaultAccountDeleteService
    ) { }

    provider: string;
    accountId: string;
    accountType: VaultAssetType;

    ngOnInit(): void {
        this.accountId = this.vaultAccountDeleteService.accountId;
        this.accountType = this.vaultAccountDeleteService.accountType;
    }

    revokeConsent(isArchived: boolean = false) {
        this.buttonDisabled = true;
        if (this.accountId && this.accountType) {
            this.vaultApiService
                .removeManualAccount(this.accountId, this.accountType, isArchived)
                .subscribe(
                    () => {
                        var response = isArchived ? 'archived' : 'removed';
                        this.alertNotificationService.showSuccess({
                            text: "Account has been successfully " + response,
                        });
                        this.modalService.dismissAll();
                        this.eventBusService.emit(
                            new EmitEvent(Events.VaultRefresh)
                        );
                    },
                    (error) => {
                        this.buttonDisabled = false;
                        this.deleteErrorResponse(
                            "We were unable to remove the account. Please try again",
                            error
                        )
                    }
                );
        } else {
            this.vaultApiService.revokeConsent(this.provider).subscribe(
                () => {
                    this.modalService.dismissAll()
                    this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
                    this.alertNotificationService.showSuccess({
                        text: "Consent has been successfully removed",
                    });
                },
                (error) => {
                    this.buttonDisabled = false;
                    this.modalService.dismissAll()
                    this.deleteErrorResponse(
                        "We were unable to remove consent. Please try again or contact an administrator.",
                        error
                    )
                }
            );
        }
    }

    closeModal() {
        this.serviceModal.dismissAll();
    }

    deleteErrorResponse(message: string, error: any) {
        console.error(error);
        this.alertNotificationService.showWarning({
            text: message,
        });
    }
}
