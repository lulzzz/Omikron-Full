import { NgModule } from '@angular/core';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';

import { MaterialCustomModule } from '../material-custom/material-custom.module';
import { OverlayModule } from '../overlay/overlay.module';
import { SharedModule } from '../shared/shared.module';
import { UsersModule } from '../users/users.module';
import { ConfirmTokenComponent } from './confirm-token/confirm-token.component';
import { AuthGuard } from './guards/auth-guard.service';
import { LoginGuard } from './guards/login-guard.service';
import { SecurityForgotPasswordComponent } from './security-forgot-password/security-forgot-password.component';
import { SecurityLoginFormComponent } from './security-login/security-login-form.component';
import { SecurityLoginComponent } from './security-login/security-login.component';
import { SecurityOnboardingComponent } from './security-onboarding/security-onboarding.component';
import { AddMyAccountComponent } from './security-register/add-my-account/add-my-account.component';
import { ProvidersListComponent } from './security-register/providers-list/providers-list.component';
import {
    SecurityCreateAccountComponent,
} from './security-register/security-create-account/security-create-account.component';
import { SecurityMyDetailsComponent } from './security-register/security-my-details/security-my-details.component';
import { SecurityNavigationComponent } from './security-register/security-navigation/security-navigation.component';
import { SecurityRegisterComponent } from './security-register/security-register/security-register.component';
import { SecurityResponseComponent } from './security-register/security-response/security-response.component';
import { SecurityVerificationComponent } from './security-register/security-verification/security-verification.component';
import { SecurityRoutingModule } from './security-routing.module';
import { SecurityUserPersistenceService } from './security-user-persistence.service';
import { SecurityService } from './services/security.service';
import { AddAddressManuallyComponent } from './security-register/add-address-manually/add-address-manually.component';
import { AddAccountManuallyComponent } from './security-register/add-account-manually/add-account-manually.component';

@NgModule({
    imports: [SecurityRoutingModule, SharedModule, UsersModule, NgxIntlTelInputModule, MaterialCustomModule, OverlayModule],
    declarations: [
        SecurityLoginComponent,
        SecurityLoginFormComponent,
        SecurityForgotPasswordComponent,
        SecurityOnboardingComponent,
        SecurityRegisterComponent,
        SecurityNavigationComponent,
        SecurityVerificationComponent,
        SecurityCreateAccountComponent,
        SecurityMyDetailsComponent,
        SecurityResponseComponent,
        AddMyAccountComponent,
        ConfirmTokenComponent,
        ProvidersListComponent,
        AddAddressManuallyComponent,
        AddAccountManuallyComponent
    ],
    providers: [AuthGuard, LoginGuard, SecurityUserPersistenceService, SecurityService],
    exports: [SecurityResponseComponent, ProvidersListComponent, ConfirmTokenComponent]
})
export class SecurityModule {}
