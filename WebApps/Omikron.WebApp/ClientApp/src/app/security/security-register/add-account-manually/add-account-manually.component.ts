import { ManualAccount } from './../../../vault/models/models';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { AlertNotificationService } from '../../../overlay/services/alert-notification.service';
import { AccountStatus } from '../../../shared';
import { AuthService } from '../../../shared/auth.service';
import { UsersApiService } from '../../../users/users-api.service';
import { VaultAddAccountComponent } from '../../../vault/vault-add-account/vault-add-account.component';
import { VaultAddObAccountComponent } from '../../../vault/vault-add-ob-account/vault-add-ob-account.component';
import { VaultApiService } from '../../../vault/vault-api.service';
import { CreditDebitIntIndicator } from '../../../vault/vault.models';
import { ValidationAggregate } from './../../../core/utilities/validation-aggregate';
import { NavigateUrl } from './../../models/ObProvider';
import { PairNotEmpty } from 'src/custom-validators';

@Component({
    selector: "app-add-account-manually",
    templateUrl: "./add-account-manually.component.html",
    styleUrls: ["./add-account-manually.component.scss"],
})
export class AddAccountManuallyComponent implements OnInit {
    addAccountForm: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    darkTheme: boolean = false;
    isLoanOrPension: boolean = false;
    navigateUrl: string;
    buttonDisabled: boolean = false;

    constructor(
        private fb: FormBuilder,
        protected modalService: NgbModal,
        protected vaultApiService: VaultApiService,
        protected alertNotificationService: AlertNotificationService,
        private usersApiService: UsersApiService,
        private authService: AuthService,
        private router: Router,
        protected eventBusService: EventBusService
    ) { }

    ngOnInit(): void {
        this.addAccountForm = this.createAddAccountForm();
        this.setFormValidation();
    }

    createAddAccountForm(): FormGroup {
        return this.fb.group({
            type: new FormControl(-1, [Validators.min(0)]),
            name: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            balance: new FormControl("", [
                Validators.compose([Validators.required]),
            ]),
            referenceNumber: new FormControl("", []),
            notes: new FormControl("", []),
            ownerId: new FormControl(""),
            creditDebitIndicator: new FormControl(""),
            openBalance: new FormControl(undefined),
            openDate: new FormControl(undefined)
        },
            {
                validator: PairNotEmpty("openBalance", "openDate")
            })
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("type", {
            min: "Please select the account type.",
        });
        this.validation.addValidationMessage("name", {
            required: "Please enter the account name.",
        });
        this.validation.addValidationMessage("balance", {
            required: "Please enter the account balance.",
        });

        this.validation.addValidationMessage("openBalance", {
            dependentFields: "Please insert both open date and open balance.",
        });

        this.validation.addValidationMessage("openDate", {
            dependentFields: "Please insert both open date and open balance.",
        });

        this.validation.bindValueChangesWithValidator(this.addAccountForm);
    }

    submit() {
        if (this.addAccountForm.valid) {
            if (this.addAccountForm.controls.balance.value < 0) {
                this.addAccountForm.controls.balance.setValue(this.addAccountForm.controls.balance.value * -1)
                this.addAccountForm.controls.creditDebitIndicator.setValue(CreditDebitIntIndicator.Debit)
            }
            else {
                this.addAccountForm.controls.creditDebitIndicator.setValue(CreditDebitIntIndicator.Credit)
            }

            this.buttonDisabled = true;
            var user = this.authService.getUserProfile();
            this.addAccountForm.controls.ownerId.setValue(user.id);

            var manualAccount = this.addAccountForm.value as ManualAccount;

            this.vaultApiService.addManualAccount(manualAccount).subscribe(
                () => {
                    if (this.darkTheme) {
                        this.changeAccountStatus();
                    }
                    else {
                        if (this.navigateUrl == NavigateUrl.vault) {
                            this.modalService.dismissAll();
                            this.eventBusService.emit(new EmitEvent(Events.VaultRefresh));
                        }
                        else {
                            this.router.navigate([this.navigateUrl]);
                            this.modalService.dismissAll();
                        }
                    }
                    this.alertNotificationService.showSuccess({ text: "Your account has been successfully added." })
                },
                (error) => {
                    this.buttonDisabled = false;

                    this.alertNotificationService.showWarning({
                        text: error.error.errors
                            ? Object.values(error.error.errors)[0]
                            : error.message
                    });
                })
            return;
        }
        this.validation.getValidationErrors(this.addAccountForm, true);
    }

    closeModal() {
        this.modalService.dismissAll();
    }

    changeAccountStatus(): void {
        if (this.authService.isLoggedIn()) {
            var user = this.authService.getUserProfile();
            if (user.accountStatus != AccountStatus.Active) {
                user.accountStatus = AccountStatus.Active;
                this.usersApiService.updateUserAccountStatus(user).subscribe(
                    () => {
                        this.authService.updateClaims(user)
                        this.router.navigate(["/home"]);
                        this.modalService.dismissAll();
                    });
            }
        }
    }

    back() {
        if (this.darkTheme) {
            this.modalService.dismissAll();
            return;
        }

        this.modalService.dismissAll();
        this.isLoanOrPension ? this.modalService.open(VaultAddAccountComponent)
            : this.modalService.open(VaultAddObAccountComponent);
    }
}
