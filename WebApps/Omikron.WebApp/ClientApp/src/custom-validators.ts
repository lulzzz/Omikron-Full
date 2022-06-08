import { AbstractControl, ValidationErrors, ValidatorFn, FormGroup } from '@angular/forms';

export class CustomValidators {
    static patternValidator(regex: RegExp, error: ValidationErrors): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } => {
            if (!control.value) {
                return null;
            }

            const valid = regex.test(control.value);
            return valid ? null : error;
        };
    }

    static passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
        const password = control.get('password');
        const confirmPassword = control.get('confirmPassword');

        return password && confirmPassword && password.value === confirmPassword.value ? null : { passwordMatch: true };
    };
}

export function PairNotEmpty(leftControlName: string, rightControlName: string) {
    return (formGroup: FormGroup) => {
        const leftControl = formGroup.controls[leftControlName];
        const rightControl = formGroup.controls[rightControlName];

        if (leftControl.value && !rightControl.value || !leftControl.value && rightControl.value) {
            rightControl.setErrors({ pairNotEmpty: true });
            leftControl.setErrors({ pairNotEmpty: true });
        }
        else {
            leftControl.setErrors(null);
            rightControl.setErrors(null);
        }
    }
}
