import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { PairNotEmpty } from 'src/custom-validators';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { AlertNotificationService } from '../../overlay/services/alert-notification.service';
import { UserBase } from '../../shared/models/shared.models';
import { FinanceAgreement, PersonalItem } from '../models/models';
import { VaultAddAccountComponent } from '../vault-add-account/vault-add-account.component';
import { AuthService } from './../../shared/auth.service';
import { VaultApiService } from './../vault-api.service';
import { LoanViewModel } from './../vault.models';

@Component({
    selector: "app-vault-add-personal-item",
    templateUrl: "./vault-add-personal-item.component.html",
    styleUrls: ["./vault-add-personal-item.component.scss"],
})
export class VaultAddPersonalItemComponent implements OnInit {
    personalItemForm: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();

    stepOneHidden: boolean = false;
    stepTwoHidden: boolean = true;
    stepThreeHidden: boolean = true;

    addNewHidden: boolean = false;
    linkExistingHidden: boolean = true;
    hideLoans = true;
    user: UserBase;
    loans: LoanViewModel[] = [];
    loansFiltered: LoanViewModel[] = [];
    searchTerms = new Subject<string>();
    itemPhoto: string;

    constructor(
        protected modalService: NgbModal,
        private fb: FormBuilder,
        protected vaultApiService: VaultApiService,
        private authService: AuthService,
        protected alertNotificationService: AlertNotificationService,
        protected eventBusService: EventBusService
    ) { }

    ngOnInit(): void {
        this.user = this.authService.getUserProfile();
        this.personalItemForm = this.createPersonalItemDetailsForm();
        this.setFormValidation();

        this.searchTerms
            .pipe(
                debounceTime(300),
                distinctUntilChanged(),
                switchMap((term: string) => this.filterLoans(term))
            )
            .subscribe();
    }

    filterLoans(term: string): LoanViewModel[] {
        this.loansFiltered = this.loans.filter(
            (l) => l.name.toLowerCase().indexOf(term.toLowerCase()) > -1
        );
        if (this.loansFiltered.length > 0) {
            this.hideLoans = false;
        } else {
            this.hideLoans = true;
        }
        return this.loansFiltered;
    }

    search(term: string) {
        this.searchTerms.next(term);
    }

    createPersonalItemDetailsForm(): FormGroup {
        return this.fb.group({
            itemName: new FormControl(
                null,
                Validators.compose([Validators.required])
            ),
            value: new FormControl(
                null,
                Validators.compose([Validators.required])
            ),
            purchaseValue: new FormControl(undefined),
            purchaseDate: new FormControl(undefined),
            financeAgreementName: new FormControl(
                "",
                Validators.compose([Validators.required])
            ),
            newFinanceBalance: new FormControl(
                "",
                Validators.compose([Validators.required])
            ),
            openBalance: new FormControl(undefined),
            openDate: new FormControl(undefined),
            notes: new FormControl(null),
            searchFinanceAgreement: new FormControl(null),
            financeAgreementId: new FormControl(null),
            existingFinanceBalance: new FormControl(null),
        },
            {
                validator: [PairNotEmpty("purchaseValue", "purchaseDate"),
                PairNotEmpty("openBalance", "openDate")]
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

        this.validation.addValidationMessage("purchaseValue", {
            pairNotEmpty: "Please insert both purchase value and purchase date.",
        });

        this.validation.addValidationMessage("purchaseDate", {
            pairNotEmpty: "Please insert both purchase value and purchase date.",
        });

        this.validation.addValidationMessage("openBalance", {
            pairNotEmpty: "Please insert both open date and open balance.",
        });

        this.validation.addValidationMessage("openDate", {
            pairNotEmpty: "Please insert both open date and open balance.",
        });

        this.validation.bindValueChangesWithValidator(this.personalItemForm);
    }

    showAddAccountModal() {
        this.modalService.dismissAll();
        this.modalService.open(VaultAddAccountComponent);
    }

    showStepOne() {
        this.changeStep(false, true, true);
    }

    showStepTwo() {
        this.personalItemForm.controls.itemName.valid &&
            this.personalItemForm.controls.value.valid
            ? this.changeStep(true, false, true)
            : this.validation.getValidationErrors(this.personalItemForm, true);

        if (this.loans.length === 0) {
            this.vaultApiService.searchLoans(this.user, "").subscribe(
                (data) => {
                    this.loans = data.records;
                    this.loansFiltered = data.records;
                },
                (error) => {
                    console.log(error);
                }
            );
        }
    }

    showStepThree() {
        this.personalItemForm.controls.itemName.valid &&
            this.personalItemForm.controls.value.valid
            ? this.changeStep(true, true, false)
            : this.validation.getValidationErrors(this.personalItemForm, true);
    }

    changeStep(step1: boolean, step2: boolean, step3: boolean) {
        this.stepOneHidden = step1;
        this.stepTwoHidden = step2;
        this.stepThreeHidden = step3;
    }

    addPersonalItem() {
        if (this.verifyPersonalItemForm()) {
            if (this.personalItemForm.controls.value.value < 0) {
                this.personalItemForm.controls.value.setValue(
                    this.personalItemForm.controls.value.value * -1
                );
            }

            this.vaultApiService
                .addPersonalItem(this.personalItemFactory())
                .subscribe(
                    (data) => {
                        this.alertNotificationService.showSuccess({
                            text: "Personal item successfully added!",
                        });
                        this.modalService.dismissAll();
                        this.eventBusService.emit(
                            new EmitEvent(Events.VaultRefresh)
                        );
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
            this.alertNotificationService.showWarning({
                text: "Please validate your inputs!",
            });
        }
    }

    verifyPersonalItemForm(): boolean {
        if (
            this.personalItemForm.controls.financeAgreementName.value.length >
            0 ||
            this.personalItemForm.controls.newFinanceBalance.valid
        ) {
            return this.personalItemForm.valid ? true : false;
        } else {
            return this.personalItemForm.controls.itemName.valid &&
                this.personalItemForm.controls.value.valid
                ? true
                : false;
        }
    }

    personalItemFactory() {
        var financeAgreement: FinanceAgreement;
        if (
            this.personalItemForm.controls.financeAgreementName.value.length >
            0 &&
            this.personalItemForm.controls.financeAgreementName.touched
        ) {
            financeAgreement = {
                name: this.personalItemForm.controls.financeAgreementName.value,
                balance: this.personalItemForm.controls.newFinanceBalance.value,
                notes: this.personalItemForm.controls.notes.value,
                openDate: this.personalItemForm.controls.openDate.value,
                openBalance: this.personalItemForm.controls.openBalance.value,
                reference: null,
            };
        } else {
            financeAgreement = null;
        }

        var personalItem: PersonalItem = {
            userId: this.user.id,
            itemName: this.personalItemForm.controls.itemName.value,
            value: this.personalItemForm.controls.value.value,
            purchaseValue: this.personalItemForm.controls.purchaseValue.value,
            purchaseDate: this.personalItemForm.controls.purchaseDate.value,
            itemPhoto: this.itemPhoto,
            existingFinanceAgreementId:
                this.personalItemForm.controls.financeAgreementId.value,
            financeAgreement: financeAgreement,
        };

        return personalItem;
    }

    showLoans() {
        this.hideLoans = false;
    }

    selectLoan(loan: LoanViewModel) {
        this.personalItemForm.controls["existingFinanceBalance"].setValue(
            loan.balance
        );
        this.personalItemForm.controls["searchFinanceAgreement"].setValue(
            loan.name
        );
        this.personalItemForm.controls["financeAgreementId"].setValue(loan.id);
        this.hideLoans = true;
    }

    showAddNewFinanceAgreement() {
        this.addNewHidden = false;
        this.linkExistingHidden = true;
    }

    showLinkExistingFinanceAgreement() {
        this.addNewHidden = true;
        this.linkExistingHidden = false;
    }

    closeModal() {
        this.modalService.dismissAll();
    }

    getPhoto(photo: string) {
        this.itemPhoto = photo;
    }

    private showWarning(error: any) {
        this.alertNotificationService.showWarning({
            text: error.error.errors
                ? error.error.errors[0]
                : error.error.Message,
        });
    }
}
