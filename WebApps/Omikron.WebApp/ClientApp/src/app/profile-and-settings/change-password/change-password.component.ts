import { PasswordChangeCommand } from './../profile.models';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { UsersApiService } from 'src/app/users/users-api.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ValidationAggregate } from 'src/app/core/utilities/validation-aggregate';
import { CustomValidators } from 'src/custom-validators';
import { MustMatch } from '../utilities/must-match.validator';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  verification: boolean = false;
  phoneNumber: string;
  passwordChangeCommand: PasswordChangeCommand;

  changePasswordForm: FormGroup;

  validation: ValidationAggregate = new ValidationAggregate();

  constructor(private fb: FormBuilder, private modalService: NgbModal, private usersApiService: UsersApiService, private alertNotificationService: AlertNotificationService) { }

  ngOnInit(): void {
    this.changePasswordForm = this.createEditProfileForm();
    this.setFormValidation();
  }

  createEditProfileForm(): FormGroup {
    return this.fb.group({
      currentPassword: new FormControl("", [
        Validators.compose([
          Validators.required
        ])
      ]),
      password: new FormControl("", [
        Validators.compose([
          Validators.required,
          CustomValidators.patternValidator(/\d/, {
            hasNumber: true,
          }),
          CustomValidators.patternValidator(/[A-Z]/, {
            hasCapitalCase: true,
          }),
          CustomValidators.patternValidator(/[a-z]/, {
            hasSmallCase: true,
          }),
          Validators.minLength(8),
        ])
      ]),
      confirmPassword: new FormControl("", [
        Validators.compose([
          Validators.required
        ])
      ])
    }, {
        validator: MustMatch('password', 'confirmPassword')
    })
  }

  setFormValidation(): void {

    this.validation.addValidationMessage('currentPassword', {
      required: 'Please enter your current password.'
    });

    this.validation.addValidationMessage('password', {
      required: 'Please create a password.',
      minLength: 'Use at least 8 characters.',
      hasNumber: 'Use 1 or more numbers.',
      hasCapitalCase: 'Use upper and lower case characters.'
    });

    this.validation.addValidationMessage('confirmPassword', {
      required: 'Please confirm your password.',
      mustMatch: 'Passwords do not match.'
    });

    this.validation.bindValueChangesWithValidator(this.changePasswordForm);
  }

  submit() {
    if (this.changePasswordForm.valid) {
      this.usersApiService.generateTokenToChangePassword({ currentPassword: this.changePasswordForm.controls["currentPassword"].value }).subscribe(
        data => {
          this.passwordChangeCommand = {
            currentPassword: this.changePasswordForm.controls["currentPassword"].value,
            newPassword: this.changePasswordForm.controls["password"].value,
          }
          this.verification = true;
          this.phoneNumber = data.records;
        },
        error => {
          this.alertNotificationService.showWarning({
            text: error.error.errors
              ? error.error.errors[0]
              : error.error.Message,
          });
        }
      )
      return;
    }

    this.validation.getValidationErrors(this.changePasswordForm, true);
  }

  changePassword(token: number) {
    this.passwordChangeCommand.verificationToken = token;

    this.usersApiService.passwordChange(this.passwordChangeCommand).subscribe(
      () => {
        this.alertNotificationService.showSuccess({ text: "Your password has been successfully changed" });
        this.hideVerification();
        this.closeModal();
      },
      error => {
        this.alertNotificationService.showWarning({
          text: error.error.errors
            ? error.error.errors[0]
            : error.error.Message,
        });
      }
    )
  }

  hideVerification() {
    this.verification = false;
  }

  closeModal() {
    this.modalService.dismissAll();
  }
}
