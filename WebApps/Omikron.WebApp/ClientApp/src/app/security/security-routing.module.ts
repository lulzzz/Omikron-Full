import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConfirmTokenComponent } from '../security/confirm-token/confirm-token.component';
import { AuthGuard } from './guards/auth-guard.service';
import { LoginGuard } from './guards/login-guard.service';
import { SecurityForgotPasswordComponent } from './security-forgot-password/security-forgot-password.component';
import { SecurityLoginFormComponent } from './security-login/security-login-form.component';
import { SecurityLoginComponent } from './security-login/security-login.component';
import { SecurityOnboardingComponent } from './security-onboarding/security-onboarding.component';
import { SecurityRegisterComponent } from './security-register/security-register/security-register.component';


const routes: Routes = [
    {
        path: "",
        data: { title: "Login" },
        component: SecurityLoginComponent,
        children: [
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'login', component: SecurityLoginFormComponent, canActivate: [LoginGuard] },
            { path: 'forgot-password', component: SecurityForgotPasswordComponent, canActivate: [LoginGuard] },
            { path: 'onboarding', component: SecurityOnboardingComponent, canActivate: [AuthGuard] }
        ]
    },
    {
        path: "register",
        data: { title: "Register" },
        component: SecurityRegisterComponent,
    },
    {
        path: "my-details",
        data: { title: "MyDetails" },
        component: SecurityRegisterComponent,
        canActivate: [AuthGuard]
    },
    {
        path: "add-account",
        data: { title: "AddMyAccount" },
        component: SecurityRegisterComponent,
        canActivate: [AuthGuard]
    },
    {
        path: "response",
        data: { title: "Response" },
        component: SecurityRegisterComponent,
        canActivate: [AuthGuard]
    },
    {
        path: "verification",
        data: { title: "LoginVerification" },
        component: ConfirmTokenComponent,
        canActivate: [LoginGuard]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SecurityRoutingModule {}
