import { DetailsUpdatedService } from './../details-updated.service';
import { AlertNotificationService } from './../../overlay/services/alert-notification.service';
import { UsersApiService } from 'src/app/users/users-api.service';
import { AuthService } from 'src/app/shared/auth.service';
import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ProfileDeleteAccountComponent } from '../profile-delete-account/profile-delete-account.component';
import { ProfileDetailsViewModel } from '../profile.models';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-profile-content',
  templateUrl: './profile-content.component.html',
  styleUrls: ['./profile-content.component.scss']
})
export class ProfileContentComponent implements OnInit {
  profileDetailsViewModel: ProfileDetailsViewModel;

  constructor(private modalService: NgbModal,
    private authService: AuthService,
    private usersApiService: UsersApiService,
    private alertNotificationService: AlertNotificationService,
    private detailsUpdatedService: DetailsUpdatedService) {
    detailsUpdatedService.detailsUpdated$.subscribe(
      profileDetails => this.profileDetailsViewModel = profileDetails
    )
  }

  ngOnInit(): void {
    this.populateDetails();
  }

  populateDetails() {
    this.usersApiService.getProfileDetails(this.authService.getUserProfile().id).subscribe(
      data => {
        this.profileDetailsViewModel = data.records;
      },
      error => {
        this.alertNotificationService.showWarning({ text: "There was a problem with loading your data. Please try again later." })
        console.error(error);
      }
    )
  }

  updateMarketingCommunications() {
    this.usersApiService.updateMarketingCommunications(!this.profileDetailsViewModel.marketingCommunications).subscribe(
      () => {
        this.profileDetailsViewModel.marketingCommunications = !this.profileDetailsViewModel.marketingCommunications
      },
      error => {
        this.alertNotificationService.showWarning({ text: "There was an error with updating your Marketing Communication consent. Please try again later." })
        console.error(error);
      })
  }

  updateAccountNotifications() {
    this.usersApiService.updateAccountNotifications(!this.profileDetailsViewModel.accountNotifications).subscribe(
      () => {
        this.profileDetailsViewModel.accountNotifications = !this.profileDetailsViewModel.accountNotifications
      },
      error => {
        this.alertNotificationService.showWarning({ text: "There was an error with updating your Marketing Communication consent. Please try again later." })
        console.error(error);
      })
  }

  changePassword() {
    const modalRef = this.modalService.open(ChangePasswordComponent);
  }

  editProfile() {
    const modalRef = this.modalService.open(EditProfileComponent);
    modalRef.componentInstance.profileDetailsViewModel = this.profileDetailsViewModel;
  }

  deleteAccount(): void {
    const modalRef = this.modalService.open(ProfileDeleteAccountComponent);
  }
}
