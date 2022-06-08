import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';

import { AlertNotificationService } from '../../overlay/services/alert-notification.service';

@Component({
    selector: "app-confirm-token",
    templateUrl: "./confirm-token.component.html",
    styleUrls: ["./confirm-token.component.scss"],
})
export class ConfirmTokenComponent implements OnInit {
    @Input() phoneNumber: string;
    @Output() verify = new EventEmitter();
    @Output() resend = new EventEmitter();
    @Output() back = new EventEmitter();
    tokenVerificationForm: FormGroup;
    number: FormControl;
    validation: ValidationAggregate = new ValidationAggregate();
    constructor(
        private readonly alertNotificationService: AlertNotificationService
    ) {}

    ngOnInit(): void {
        this.setFormValidation();
    }

    resendCode() {
        this.resend.emit();
        this.alertNotificationService.showSuccess({
            text: "The code has been succesfully sent.",
        });
    }

    verifyNumber(formValues) {
        this.tokenVerificationForm.valid ? this.verify.emit(formValues.number) : this.validation.getValidationErrors(this.tokenVerificationForm, true);
    }

    onBackClick() {
        this.back.emit();
    }

    setFormValidation(): void {

        (this.number = new FormControl("", [
            Validators.compose([
                Validators.required,
                Validators.minLength(6),
                Validators.maxLength(6),
            ]),
        ])),
            (this.tokenVerificationForm = new FormGroup({
                number: this.number,
            }));


        this.validation.addValidationMessage("number", {
            required: "Please enter your verification code.",
            minlength: "Verification code must be a combination of six numbers.",
            maxlenght: "Verification code must be a combination of six numbers."
        });

        this.validation.bindValueChangesWithValidator(this.tokenVerificationForm);
    }
}
