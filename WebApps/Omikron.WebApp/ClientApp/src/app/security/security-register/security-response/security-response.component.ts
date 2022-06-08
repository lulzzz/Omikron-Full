import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { DashboardView } from '../../../layout/models/dashboard-view';
import { AlertNotificationService } from '../../../overlay/services/alert-notification.service';
import { AuthService } from '../../../shared/auth.service';
import { AccountStatus, UserBase } from '../../../shared/models/shared.models';
import { UsersApiService } from '../../../users/users-api.service';
import { VaultAddAccountComponent } from '../../../vault/vault-add-account/vault-add-account.component';
import { SyncApiService } from './../../../shared/sync-api.service';

@Component({
    selector: "app-security-response",
    templateUrl: "./security-response.component.html",
    styleUrls: ["./security-response.component.scss"],
})
export class SecurityResponseComponent implements OnInit {
    constructor(
        private router: Router,
        private usersApiService: UsersApiService,
        private authService: AuthService,
        private eventBusService: EventBusService,
        private syncApiService: SyncApiService,
        private alertNotificationService: AlertNotificationService,
        private modalService: NgbModal
    ) { }

    @Input() responseState;
    @Input() redirectAddAccount;
    @Input() registration;
    user: UserBase;
    buttonDisabled: boolean = false;

    ngOnInit() {
    }

    dashboardRoute(): void {
        this.buttonDisabled = true;
        this.alertNotificationService.showInfo({ text: "Please wait while we sync your data. Once it is done, we will redirect you to the dashboard." })
        this.changeAccountStatus();
        this.syncApiService.startSync().subscribe(_ => {
            this.router.navigate(["/home"]);
        },
            error => {
                this.buttonDisabled = false;
                console.error(error);
                this.alertNotificationService.showWarning({ text: "Account sync failed. Please try adding your accounts again." })
                this.addAccount();
            });
    }

    vaultRoute(): void {
        this.buttonDisabled = true;
        this.alertNotificationService.showInfo({ text: "Please wait while we sync your data. Once it is done, we will redirect you to the vault." })
        this.syncApiService.startSync().subscribe(_ => {
            this.eventBusService.emit(
                new EmitEvent(Events.LayoutSelected, DashboardView.PublicAdmin)
            );
            this.router.navigate(["/vault"]);
        },
            error => {
                this.buttonDisabled = false;
                this.alertNotificationService.showWarning({ text: "Account sync failed. Please try adding your accounts again." })
                this.eventBusService.emit(
                    new EmitEvent(Events.LayoutSelected, DashboardView.PublicAdmin)
                );
                this.router.navigate(["/vault"]);
            });
    }

    addAccount(): void {
        this.router.navigate([this.redirectAddAccount]);

        if(!this.registration)
        {
            this.modalService.open(VaultAddAccountComponent);
        }

        this.registration ? this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public))
            : this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.PublicAdmin));
    }

    redirectToHome(): void {
        this.router.navigate(["/home"]);
    }

    changeAccountStatus(): void {
        if (this.authService.isLoggedIn()) {
            this.user = this.authService.getUserProfile();
            if (this.user.accountStatus != AccountStatus.Active) {
                this.user.accountStatus = AccountStatus.Active;
                this.usersApiService.updateUserAccountStatus(this.user).subscribe(
                    () => this.authService.updateClaims(this.user));
            }
        }
    }
}
