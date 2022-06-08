import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';

@Component({
    selector: "app-add-address-manually",
    templateUrl: "./add-address-manually.component.html",
    styleUrls: ["./add-address-manually.component.scss"],
})
export class AddAddressManuallyComponent implements OnInit {
    addAddressForm: FormGroup;
    validation: ValidationAggregate = new ValidationAggregate();
    @Output() manualAddress = new EventEmitter();
    @Output() manualPostcode = new EventEmitter();
    private postcodeRegex = "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$";
    constructor(private fb: FormBuilder, private modalService: NgbModal) {}

    ngOnInit(): void {
        this.addAddressForm = this.createAddAddressForm();
        this.setFormValidation();
    }

    createAddAddressForm(): FormGroup {
        return this.fb.group({
            houseNumber: new FormControl("", [
                Validators.compose([Validators.required])]),
            addressLine1: new FormControl("", [
                Validators.compose([Validators.required])]),
            addressLine2: new FormControl("", []),
            town: new FormControl("", [
                Validators.compose([Validators.required])]),
            postcode: new FormControl("", [
                Validators.compose([Validators.required, Validators.pattern(this.postcodeRegex)])]),
        });
    }

    setFormValidation(): void {
        this.validation.addValidationMessage("houseNumber", {
            required: "Please enter your house number/name.",
        });
        this.validation.addValidationMessage("addressLine1", {
            required: "Please enter the first line of your address.",
        });
        this.validation.addValidationMessage("town", {
            required: "Please enter your town/city.",
        });
        this.validation.addValidationMessage("postcode", {
            required: "Please enter your post code.",
            pattern: "Invalid Postcode."
        });

        this.validation.bindValueChangesWithValidator(this.addAddressForm);
    }

    submit() {
        if (this.addAddressForm.valid) {
            var form = this.addAddressForm.value;
            var address = form.addressLine2 == "" ? form.houseNumber + ', ' + form.addressLine1 + ', ' + form.town
            : form.houseNumber + ', ' + form.addressLine1 + ', ' + form.addressLine2 + ', ' + form.town ;

            this.manualAddress.emit(address)
            this.manualPostcode.emit(form.postcode)
            this.modalService.dismissAll();

            return;
        }

        this.validation.getValidationErrors(this.addAddressForm, true);
    }

    closeModal() {
        this.modalService.dismissAll();
    }
}
