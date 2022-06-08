import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HomeModule } from '../home/home.module';
import { SecurityModule } from '../security/security.module';
import { SharedModule } from '../shared/shared.module';
import { ProfileAndSettingsRoutingModule } from './profile-and-settings-routing.module.';
import { ProfileAndSettingsComponent } from './profile-and-settings.component';
import { ProfileContentComponent } from './profile-content/profile-content.component';
import { ProfileDeleteAccountComponent } from './profile-delete-account/profile-delete-account.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';
import { ChangePasswordComponent } from './change-password/change-password.component';



@NgModule({
  declarations: [
    ProfileContentComponent,
    ProfileAndSettingsComponent,
    ProfileDeleteAccountComponent,
    EditProfileComponent,
    ChangePasswordComponent],
  imports: [
    CommonModule,
    ProfileAndSettingsRoutingModule,
    HomeModule,
    SharedModule,
    SecurityModule,
    NgxIntlTelInputModule
  ]
})
export class ProfileAndSettingsModule { }
