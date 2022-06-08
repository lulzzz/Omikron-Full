import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { HomeModule } from '../home/home.module';
import { MaterialCustomModule } from '../material-custom/material-custom.module';
import { SecurityModule } from '../security/security.module';
import { SharedModule } from '../shared/shared.module';
import { VaultRoutingModule } from '../vault/vault-routing.module';
import { VaultComponent } from '../vault/vault.component';
import { PictureUrlPipe } from './picture-url.pipe';
import { VaultAccountDetailsComponent } from './vault-account-details/vault-account-details.component';
import { VaultAddAccountComponent } from './vault-add-account/vault-add-account.component';
import { VaultAddInvestmentComponent } from './vault-add-investment/vault-add-investment.component';
import { VaultEditInvestmentComponent } from './vault-add-investment/vault-edit-investment/vault-edit-investment.component';
import {
    VaultInvestmentDetailsComponent,
} from './vault-add-investment/vault-investment-details/vault-investment-details.component';
import { VaultAddObAccountComponent } from './vault-add-ob-account/vault-add-ob-account.component';
import { VaultAddPersonalItemComponent } from './vault-add-personal-item/vault-add-personal-item.component';
import {
    VaultEditPersonalItemComponent,
} from './vault-add-personal-item/vault-edit-personal-item/vault-edit-personal-item.component';
import { VaultAddPropertyComponent } from './vault-add-property/vault-add-property.component';
import { VaultEditPropertyComponent } from './vault-add-property/vault-edit-property/vault-edit-property.component';
import { VaultAddVehicleComponent } from './vault-add-vehicle/vault-add-vehicle.component';
import { VaultEditVehicleComponent } from './vault-add-vehicle/vault-edit-vehicle/vault-edit-vehicle.component';
import { VaultContentComponent } from './vault-content/vault-content.component';
import {
    VaultDeleteVerificationPromptComponent,
} from './vault-delete-verification-prompt/vault-delete-verification-prompt.component';
import { VaultEditManualAccountComponent } from './vault-edit-manual-account/vault-edit-manual-account.component';
import { VaultManualAccountDetailsComponent } from './vault-manual-account-details/vault-manual-account-details.component';
import { VaultManualAccountImagePipe } from './vault-manual-account-image-pipe';
import { VaultResponseComponent } from './vault-response/vault-response.component';
import { VaultUploadPictureComponent } from './vault-upload-picture/vault-upload-picture.component';

@NgModule({
    declarations: [
        VaultComponent,
        VaultContentComponent,
        VaultAccountDetailsComponent,
        VaultAddAccountComponent,
        VaultAddObAccountComponent,
        VaultResponseComponent,
        VaultAddPersonalItemComponent,
        VaultAddVehicleComponent,
        VaultDeleteVerificationPromptComponent,
        VaultAddInvestmentComponent,
        VaultInvestmentDetailsComponent,
        VaultDeleteVerificationPromptComponent,
        VaultManualAccountDetailsComponent,
        VaultManualAccountImagePipe,
        VaultAddPropertyComponent,
        VaultDeleteVerificationPromptComponent,
        VaultEditPropertyComponent,
        VaultEditVehicleComponent,
        PictureUrlPipe,
        VaultEditPersonalItemComponent,
        VaultEditInvestmentComponent,
        VaultEditManualAccountComponent,
        VaultUploadPictureComponent
    ],
    imports: [
        VaultRoutingModule,
        CommonModule,
        HomeModule,
        FormsModule,
        SecurityModule,
        SharedModule,
        MaterialCustomModule
    ],
})
export class VaultModule { }
