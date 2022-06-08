import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { DashboardView } from '../../layout/models/dashboard-view';
import { AlertNotificationService } from '../../overlay/services/alert-notification.service';
import { AuthService } from '../../shared/auth.service';
import { UserBase } from '../../shared/models/shared.models';
import { SecurityApiService } from '../../shared/security-api.service';
import { UsersApiService } from '../../users/users-api.service';

@Component({
    selector: "app-profile-delete-account",
    templateUrl: "./profile-delete-account.component.html",
    styleUrls: ["./profile-delete-account.component.scss"],
})
export class ProfileDeleteAccountComponent implements OnInit {
    isBusy: boolean;
    verification: boolean = false;
    phoneNumber: string;
    user: UserBase;
    constructor(
        private readonly modalService: NgbModal,
        private readonly userApiService: UsersApiService,
        private readonly securityApiService : SecurityApiService,
        private readonly alertNotificationService: AlertNotificationService,
        private readonly authService: AuthService,
        private eventBusService: EventBusService,
        private cd : ChangeDetectorRef
    ) {}

    ngOnInit(): void {}

    hideModal() {
        const modalRef = this.modalService.dismissAll();
    }

    verifyAction() {

        this.user = this.authService.getUserProfile();
        this.securityApiService.generateToken(this.user.username).subscribe(
            (data) => {
                this.phoneNumber = data.records;
                this.verification = true;
                this.cd.markForCheck();
            },
            (error) => {
                this.alertNotificationService.showWarning({
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    deleteAccount(token : number): void {
        this.setBusy(true);
        this.userApiService.deleteAccount(this.user, token, false).subscribe(
            () => {
                this.setBusy(false);
                this.modalService.dismissAll();
                this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));
                this.authService.logOut();
                this.alertNotificationService.showSuccess({
                    text: "Your account has been successfully deleted.",
                });
            },
            (error) => {
                this.setBusy(false);
                this.alertNotificationService.showWarning({
                    text: error.error.errors
                        ? error.error.errors[0]
                        : error.error.Message,
                });
            }
        );
    }

    private setBusy(isBusy: boolean): void {
        this.isBusy = isBusy;
    }
}
