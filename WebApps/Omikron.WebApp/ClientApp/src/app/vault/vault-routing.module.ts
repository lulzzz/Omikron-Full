import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../security/guards/auth-guard.service';
import { VaultResponseComponent } from './vault-response/vault-response.component';
import { VaultComponent } from './vault.component';

const routes: Routes = [
    {
        path: "",
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                component: VaultComponent,
                data: {
                    title: "vault",
                    showTopTitleBar: true,
                },
                canActivate: [AuthGuard],
            },
            {
                path: "response",
                component: VaultResponseComponent,
                data: {
                    title: "vault-response",
                    showTopTitleBar: true,
                },
                canActivate: [AuthGuard],
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VaultRoutingModule {}
