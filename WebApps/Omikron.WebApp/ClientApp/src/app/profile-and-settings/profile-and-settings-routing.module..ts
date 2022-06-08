import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../security/guards/auth-guard.service';
import { ProfileAndSettingsComponent } from './profile-and-settings.component';

const routes: Routes = [
    {
        path: "",
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                component: ProfileAndSettingsComponent,
                data: {
                    title: "profile",
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
export class ProfileAndSettingsRoutingModule {}
