import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AlertNotificationService } from '../overlay/services/alert-notification.service';
import { AuthService } from '../shared/auth.service';
import { AccountStatus, UserBase } from '../shared/models/shared.models';
import { UsersApiService } from '../users/users-api.service';

@Component({
    selector: "home",
    templateUrl: "home.component.html",
})
export class HomeComponent implements OnInit {
    user: UserBase;

    constructor(
        private authService: AuthService,
        private userApiService: UsersApiService,
        private alertNotificationService: AlertNotificationService,
        private router: Router,
        private cd: ChangeDetectorRef,
    ) { }

    ngOnInit(): void {
        this.user = this.authService.getUserProfile();
        this.redirectUser();
        if (this.redirectUser()) {
            this.changeLayout();
        }
    }

    private redirectUser(): boolean {
        if (this.authService.isLoggedIn()) {
            switch (this.user.accountStatus) {
                case AccountStatus.Active:
                    return true;
                case AccountStatus.PerformKyc:
                    this.router.navigate(["/authenticate/my-details"]);
                    return false;
                case AccountStatus.AddBankAccount:
                    this.router.navigate(["/authenticate/add-account"]);
                    return false;
                default:
                    this.router.navigate(["/error/404"]);
                    return false;
            }
        }
    }

    private changeLayout(): void {
        this.userApiService.getUserById(this.user.id).subscribe(
            () => this.onSuccess(),
            (error) => {
                this.alertNotificationService.showWarning({
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    private onSuccess(): void {

        this.router.navigate(["/dashboard"]);
        this.cd.markForCheck();
    }
}
