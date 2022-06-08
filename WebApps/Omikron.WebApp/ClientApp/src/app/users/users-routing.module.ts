import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { HeaderComponent, FooterComponent } from "../layout";
import { AuthGuard } from '../security/guards/auth-guard.service';

const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                redirectTo: "list",
                pathMatch: "full",
            },
            { path: "", component: HeaderComponent, outlet: "header" },
            { path: "", component: FooterComponent, outlet: "footer" }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UsersRoutingModule {}
