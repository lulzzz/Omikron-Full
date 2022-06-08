import { forwardRef, Inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot } from '@angular/router';

import { AccountStatus } from '../../shared';
import { AuthService } from '../../shared/auth.service';

@Injectable()
export class UserStatusGuard implements CanActivate, CanActivateChild {
    constructor(
        @Inject(forwardRef(() => AuthService)) private authService: AuthService,
        @Inject(forwardRef(() => Router)) private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.checkLogin();
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }

    private checkLogin(): boolean {
        const user = this.authService.getUserProfile();

        if (this.authService.isLoggedIn() && user.accountStatus != AccountStatus.Active) {
            this.authService.logOut();
            this.router.navigate(["/authenticate"]);
        }

        return true;
    }
}
