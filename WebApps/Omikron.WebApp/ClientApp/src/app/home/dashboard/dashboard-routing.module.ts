import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FooterComponent, HeaderComponent } from '../../layout';
import { AuthGuard } from '../../security/guards/auth-guard.service';
import { DashboardComponent } from './dashboard.component';


const routes: Routes = [
    {
        path: "",
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                component: DashboardComponent,
                data: {
                    title: "dashboard",
                    showTopTitleBar: true,
                },
                canActivate: [AuthGuard],
            },
            { path: "", component: HeaderComponent, outlet: "header" },
            { path: "", component: FooterComponent, outlet: "footer" }
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardRoutingModule {}
