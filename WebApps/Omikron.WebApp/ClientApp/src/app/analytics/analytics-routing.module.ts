import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../security/guards/auth-guard.service';
import { AnalyticsComponent } from './analytics.component';

const routes: Routes = [
    {
        path: "",
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                component: AnalyticsComponent,
                data: {
                    title: "analytics",
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
export class AnalyticsRoutingModule {}
