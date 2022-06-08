import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FooterComponent } from '../layout/footer.component';
import { HeaderComponent } from '../layout/header.component';
import { AuthGuard } from '../security/guards/auth-guard.service';
import { HomeComponent } from './home.component';

const routes: Routes = [
    {
        path: "",
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                component: HomeComponent,
                data: {
                    title: "navigation.home-title-bar",
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
export class HomeRoutingModule {}
