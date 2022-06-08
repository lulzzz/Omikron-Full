import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { AuthService } from 'src/app/shared/auth.service';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { FinanceAgreement, Vehicle } from '../models/models';
import { VaultAddAccountComponent } from '../vault-add-account/vault-add-account.component';
import { VaultApiService } from '../vault-api.service';
import { LoanViewModel } from '../vault.models';

@Component({
    selector: "app-vault-add-vehicle",
    templateUrl: "./vault-add-vehicle.component.html",
    styleUrls: ["./vault-add-vehicle.component.scss"],
})
export class VaultAddVehicleComponent implements OnInit {
    formAddVehicle: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    automaticallyReValueVehicle: boolean = false;
    itemPhoto: string;

    stepOneHidden: boolean = false;
    stepTwoHidden: boolean = true;
    stepThreeHidden: boolean = true;
    addNewHidden: boolean = false;
    linkExistingHidden: boolean = true;
    hideLoans = true;

    loans: LoanViewModel[] = [];
    loansFiltered: LoanViewModel[] = [];
    searchTerms = new Subject<string>();

    vehicleValueProcessing = false;
    vehicleValueReturned = false;

    private vehicleId: string;

    constructor(private fb: FormBuilder,
        protected modalService: NgbModal,
        protected vaultApiService: VaultApiService,
        private authService: AuthService,
        protected alertNotificationService: AlertNotificationService,
        protected eventBusService: EventBusService) { }
    ngOnInit(): void {
        this.formAddVehicle = this.createAddVehicleForm();
        this.setFormValidation();
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("vehicleName", {
            required: "Please enter the vehicle name.",
        });
        this.validation.addValidationMessage("registration", {
            required: "Please enter the registration."
        });
        this.validation.addValidationMessage("mileage", {
            required: "Please enter the mileage.",
        });
        this.validation.addValidationMessage("vehicleValue", {
            required: "Please enter the vehicle value.",
            pattern: "Please enter a positive number."
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

        this.validation.bindValueChangesWithValidator(this.formAddVehicle);
    }

    createAddVehicleForm(): FormGroup {
        return this.fb.group({
            vehicleName: new FormControl("", Validators.compose([Validators.required])),
            registration: new FormControl("", [Validators.compose([Validators.required]),]),
            mileage: new FormControl("", Validators.compose([Validators.required])),
            vehicleValue: new FormControl("", Validators.compose([Validators.required])),
            financeAgreementName: new FormControl("", Validators.compose([Validators.required])),
            newFinanceBalance: new FormControl("", Validators.compose([Validators.required])),
            automaticallyReValueVehicle: new FormControl(false),
            notes: new FormControl(""),
            reference: new FormControl(""),
            purchaseValue: new FormControl(undefined),
            purchaseDate: new FormControl(undefined),
            financeAgreementId: new FormControl(null),
            searchFinanceAgreement: new FormControl(null),
            existingFinanceBalance: new FormControl(null),
            openBalance: new FormControl(undefined),
            openDate: new FormControl(undefined),
        });
    }

    valueMyVehicle(): void {
        if (
            !this.formAddVehicle.get("registration").valid ||
            !this.formAddVehicle.get("mileage").valid
        ) {
            this.validation.getValidationErrors(this.formAddVehicle, true);
            this.formAddVehicle.markAsTouched();

            return;
        }

        this.vehicleValueProcessing = true;

        this.vaultApiService
            .getVehicleValue(
                this.formAddVehicle.get("registration").value,
                this.formAddVehicle.get("mileage").value
            )
            .subscribe(
                (item) => {
                    this.formAddVehicle.patchValue({
                        vehicleValue: item.records.value,
                    });
                    this.vehicleValueProcessing = false;
                    this.vehicleValueReturned = true;
                },
                (error) => {
                    this.alertNotificationService.showWarning({
                        text: error.error.errors
                            ? Object.values(error.error.errors)[0]
                            : error.message
                    });
                });
    }

    closeModal(): void {
        this.modalService.dismissAll();
    }

    showAddAccountModal() {
        this.modalService.open(VaultAddAccountComponent);
    }

    showStepOne() {
        this.changeStep(false, true, true);
    }

    showStepTwo() {
        this.formAddVehicle.controls.vehicleName.valid && this.formAddVehicle.controls.registration.valid &&
            this.formAddVehicle.controls.vehicleValue.valid && this.formAddVehicle.controls.mileage.valid
            ? this.changeStep(true, false, true) : this.validation.getValidationErrors(this.formAddVehicle, true);

        if (this.loans.length === 0) {
            this.vaultApiService
                .searchLoans(this.authService.getUserProfile(), "")
                .subscribe(
                    (data) => {
                        this.loans = data.records;
                        this.loansFiltered = data.records;
                    },
                    (error) => {
                        console.error(error);
                        this.alertNotificationService.showInfo({text: "We were unable to retrieve available loans, please try again later or contact an administrator."})
                    }
                );
        }
    }

    showStepThree() {
        this.formAddVehicle.controls.vehicleName.valid && this.formAddVehicle.controls.registration.valid &&
            this.formAddVehicle.controls.vehicleValue.valid && this.formAddVehicle.controls.mileage.valid
            ? this.changeStep(true, true, false) : this.validation.getValidationErrors(this.formAddVehicle, true);
    }

    changeStep(step1: boolean, step2: boolean, step3: boolean) {
        this.stepOneHidden = step1;
        this.stepTwoHidden = step2;
        this.stepThreeHidden = step3;
    }

    addVehicle() {
        if(this.verifyVehicleForm())
        {
            if(this.formAddVehicle.controls.vehicleValue.value < 0)
            {
                this.formAddVehicle.controls.vehicleValue.setValue(this.formAddVehicle.controls.vehicleValue.value * -1)
            }

            this.vaultApiService.addVehicle(this.vehicleItemFactory()).subscribe(
                () => {
                    this.alertNotificationService.showSuccess({
                        text: "Vehicle successfully added!",
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
            )

        }else{
            this.alertNotificationService.showWarning({
                text: "Please validate form inputs!",
            });
        }
    }

    verifyVehicleForm(): boolean {
        if (this.formAddVehicle.controls.financeAgreementName.value.length > 0 ||
            this.formAddVehicle.controls.newFinanceBalance.valid) {
            return this.formAddVehicle.valid ? true : false;
        } else {
            return this.formAddVehicle.controls.vehicleName.valid && this.formAddVehicle.controls.registration.valid &&
                this.formAddVehicle.controls.vehicleValue.valid && this.formAddVehicle.controls.mileage.valid ? true : false;
        }
    }

    vehicleItemFactory() {
        var financeAgreement: FinanceAgreement;
        if (this.formAddVehicle.controls.financeAgreementName.value.length > 0 &&
            this.formAddVehicle.controls.financeAgreementName.touched) {
            financeAgreement = {
                name: this.formAddVehicle.controls.financeAgreementName.value,
                balance: this.formAddVehicle.controls.newFinanceBalance.value,
                notes: this.formAddVehicle.controls.notes.value,
                openBalance: this.formAddVehicle.controls.openBalance.value,
                openDate: this.formAddVehicle.controls.openDate.value,
                reference: this.formAddVehicle.controls.reference.value
            };
        } else {
            financeAgreement = null;
        }

        var vehicle: Vehicle = {
            vehicleId: '',
            userId: this.authService.getUserProfile().id,
            vehicleName: this.formAddVehicle.controls.vehicleName.value,
            registration: this.formAddVehicle.controls.registration.value,
            mileage: this.formAddVehicle.controls.mileage.value,
            vehicleValue: this.formAddVehicle.controls.vehicleValue.value,
            vehiclePhoto: this.itemPhoto,
            purchaseValue: this.formAddVehicle.controls.purchaseValue.value,
            purchaseDate: this.formAddVehicle.controls.purchaseDate.value,
            automaticallyReValueVehicle: this.automaticallyReValueVehicle,
            existingFinanceAgreementId:
                this.formAddVehicle.controls.financeAgreementId.value,
            financeAgreement: financeAgreement,
        };

        return vehicle;
    }

    search(term: string) {
        this.searchTerms.next(term);
    }

    showLoans() {
        this.hideLoans = false;
    }

    selectLoan(loan: LoanViewModel) {
        this.formAddVehicle.controls.existingFinanceBalance.setValue(
            loan.balance
        );
        this.formAddVehicle.controls.searchFinanceAgreement.setValue(loan.name);
        this.formAddVehicle.controls.financeAgreementId.setValue(loan.id);
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
