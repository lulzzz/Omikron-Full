import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { AppConfig } from './app-config';
import { EventBusService } from './core/events/event-bus.service';
import { Events } from './core/models/events';
import { DashboardView } from './layout/models/dashboard-view';
import { AccountStatus } from './shared';
import { AuthService } from './shared/auth.service';
import { IdleTimerService } from './shared/idle-timer/idle-time.service';
import { LogService } from './shared/logging/log.service';
import { DeviceService } from './shared/utilities/device.service';
import { LogoutTriggerService } from './shared/utilities/logout-trigger.service';

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
})
export class AppComponent implements OnInit, OnDestroy {
    dashboardView: DashboardView = DashboardView.Public;
    eventBusSub: Subscription;

    constructor(
        @Inject(DOCUMENT) private doc: any,
        private deviceService: DeviceService,
        private appConfig: AppConfig,
        private logService: LogService,
        private authService: AuthService,
        private eventBusService: EventBusService,
        private idleTimerService: IdleTimerService,
        private logoutTriggerService: LogoutTriggerService
    ) {
        this.logoutTriggerService.triggerLogoutActivated$.subscribe(() => {
            this.idleTimerService.start();
        });
    }

    ngOnInit(): void {
        this.logService.information({ message: "Application initialization" });

        this.eventBusSub = this.eventBusService.on(
			Events.LayoutSelected,
			(dashboardView: DashboardView) => {
				this.dashboardView = dashboardView;
			}
		);

        this.deviceService.initialize();

        if(localStorage.getItem("logoutActivated") === "true"){
            this.idleTimerService.start();
        }

        if (this.authService.isLoggedIn()) {
            const profile = this.authService.getUserProfile();
            if (profile.accountStatus == AccountStatus.Active)
            {
                this.dashboardView = DashboardView.PublicAdmin;
            }
        }
    }

    ngOnDestroy(): void {
		this.eventBusSub.unsubscribe();
	}
}
