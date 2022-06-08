import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './security/guards/auth-guard.service';
import { UserStatusGuard } from './security/guards/status-guard.service';

const routes: Routes = [
    {
        path: "landing",
        canActivate: [AuthGuard],
        loadChildren: () =>
            import("./landing/landing.module").then(mod => mod.LandingModule)
    },
    {
        path: "users",
        canActivate: [AuthGuard],
        loadChildren: () =>
            import("./users/users.module").then(mod => mod.UsersModule)
    },
    {
        path: "home",
        canActivate: [UserStatusGuard],
        loadChildren: () =>
            import("./home/home.module").then(mod => mod.HomeModule)
    },
    {
        path: "dashboard",
        canActivate: [UserStatusGuard],
        loadChildren: () =>
            import("./home/dashboard/dashboard.module").then(mod => mod.DashboardModule)
    },
    {
        path: "vault",
        canActivate: [UserStatusGuard],
        loadChildren: () =>
            import("./vault/vault.module").then(mod => mod.VaultModule)
    },
    {
        path: "profile",
        canActivate: [UserStatusGuard],
        loadChildren: () =>
            import("./profile-and-settings/profile-and-settings.module").then(mod => mod.ProfileAndSettingsModule)
    },
    {
        path: "analytics",
        canActivate: [UserStatusGuard],
        loadChildren: () =>
            import("./analytics/analytics.module").then(mod => mod.AnalyticsModule)
    },
    {
        path: "authenticate",
        loadChildren: () =>
            import("./security/security.module").then(mod => mod.SecurityModule)
    },
    {
        path: "errors",
        loadChildren: () =>
            import("./error-pages/error-pages.module").then(
                mod => mod.ErrorPagesModule
            )
    },
    { path: "", redirectTo: "landing", pathMatch: "full" },
    { path: "**", redirectTo: "/errors/404", pathMatch: "full" }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    providers:[UserStatusGuard],
    exports: [RouterModule]
})
export class AppRoutingModule {}
