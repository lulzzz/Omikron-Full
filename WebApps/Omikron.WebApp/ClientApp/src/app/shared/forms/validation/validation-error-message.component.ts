import { Component, Input } from "@angular/core";
import { FormControl, NgForm } from '@angular/forms';

@Component({
    selector: "validation-error-message",
    templateUrl: "validation-error-message.component.html"
})
export class ValidationErrorMessageComponent {
    @Input() form: NgForm;
    @Input() control: FormControl;
    @Input() errorRequired: string;
    @Input() errorMinLength: string;
    @Input() errorMaxLength: string;
    @Input() errorPattern: string;
    @Input() email: string;
    @Input() errors: string[];
}
