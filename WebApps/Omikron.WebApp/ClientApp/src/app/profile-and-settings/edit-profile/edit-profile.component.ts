import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CountryISO, PhoneNumberFormat, SearchCountryField } from 'ngx-intl-tel-input';

import { ValidationAggregate } from '../../core/utilities/validation-aggregate';
import { AlertNotificationService } from '../../overlay/services/alert-notification.service';
import { AuthService } from '../../shared/auth.service';
import { UserBase } from '../../shared/models/shared.models';
import { UsersApiService } from '../../users/users-api.service';
import { ProfileDetailsViewModel } from '../profile.models';
import { DetailsUpdatedService } from './../details-updated.service';
import { EditProfileDetailsCommand } from './../profile.models';


@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  profileDetailsViewModel: ProfileDetailsViewModel;
  verification: boolean = false;
  phoneNumber: string;
  user: UserBase;
  SearchCountryField = SearchCountryField;
  CountryISO = CountryISO;
  PhoneNumberFormat = PhoneNumberFormat;

  editProfileForm: FormGroup;
  editProfileDetailsCommand: EditProfileDetailsCommand;

  validation: ValidationAggregate = new ValidationAggregate();

  constructor(private fb: FormBuilder,
    private modalService: NgbModal,
    private usersApiService: UsersApiService,
    private authService: AuthService,
    private alertNotificationService: AlertNotificationService,
    private detailsUpdatedService: DetailsUpdatedService) { }

  ngOnInit(): void {
    this.user = this.authService.getUserProfile();
    this.editProfileForm = this.createEditProfileForm();
    this.setFormValidation();
  }

  createEditProfileForm(): FormGroup {
    return this.fb.group({
      nickname: new FormControl(this.profileDetailsViewModel.nickname, Validators.compose([Validators.required, Validators.minLength(2)])),
      email: new FormControl(this.profileDetailsViewModel.email, Validators.compose([Validators.required, Validators.email])),
      phoneNumber: new FormControl(this.profileDetailsViewModel.phoneNumber, [Validators.required])
    })
  }

  setFormValidation(): void {
    this.validation.addValidationMessage('nickname', {
      required: 'Please enter your nickname.',
      minlength: "Nickname must be longer than 2 characters"
    });
    this.validation.addValidationMessage('email', {
      required: 'Please enter your email address.',
      email: 'Email is not valid.'
    });

    this.validation.addValidationMessage('phone', {
      required: 'Please enter your phone number.',
      validatePhoneNumber: "Please enter valid number for selected country."
    });

    this.validation.bindValueChangesWithValidator(this.editProfileForm);
  }

  submit() {
    if (this.editProfileForm.valid) {
      this.editProfileDetailsCommand = {
        userId: this.user.id,
        phoneNumber: this.editProfileForm.controls["phoneNumber"].value.number,
        nickname: this.editProfileForm.controls["nickname"].value,
        email: this.editProfileForm.controls["email"].value
      }

      this.usersApiService.generateTokenForNewNumber(this.editProfileDetailsCommand).subscribe(
        data => {
          this.phoneNumber = data.records;
          this.verification = true;
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

    this.validation.getValidationErrors(this.editProfileForm, true);
  }

  editProfile(token: number) {
    this.editProfileDetailsCommand.verificationToken = token;

    this.usersApiService.editProfileDetails(this.editProfileDetailsCommand).subscribe(
      () => {
        this.alertNotificationService.showSuccess({ text: "You have successfully updated your profile." });

        let details: ProfileDetailsViewModel = {
          nickname: this.editProfileDetailsCommand.nickname,
          email: this.editProfileDetailsCommand.email,
          phoneNumber: this.editProfileDetailsCommand.phoneNumber
        };
        this.user.username = this.editProfileDetailsCommand.email;
        this.user.phoneNumber = this.editProfileDetailsCommand.phoneNumber;
        this.detailsUpdatedService.updateDetails(details);
        this.authService.updateClaims(this.user);
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
