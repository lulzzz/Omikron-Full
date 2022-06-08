import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Event as NavigationEvent, NavigationEnd, Router } from '@angular/router';

import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { AuthService } from '../../../shared/auth.service';
import { DashboardView } from '../../models/dashboard-view';

@Component({
    selector: "app-sidebar",
    templateUrl: "./sidebar.component.html",
    styleUrls: ["./sidebar.component.scss"],
})
export class SidebarComponent implements OnInit {
    constructor(
        private authService: AuthService,
        private router: Router,
        private eventBusService: EventBusService
    ) {}
    @Output() navigationEvent = new EventEmitter<boolean>();
    navigationClosed: boolean;
    activeRoute: number;

    ngOnInit(): void {
        this.changeActiveRoute();
        this.checkIfMobile();
    }

    LogOut(): void {
        this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));
        this.authService.logOut();
    }

    changeNavigation(): void {
        if (this.navigationClosed == true) {
            this.navigationEvent.emit(false);
            this.navigationClosed = false;
        } else {
            this.navigationEvent.emit(true);
            this.navigationClosed = true;
        }
    }

    private checkIfMobile(): void {
        if(document.body.offsetWidth < 1024)
        {
            this.navigationEvent.emit(true);
            this.navigationClosed = true;
        }
    }

    changeActiveRoute() {
        this.router.events.forEach((event: NavigationEvent) => {
            if (event instanceof NavigationEnd) {
                switch (event.url) {
                    case "/analytics": {
                        this.activeRoute = 1;
                        break;
                    }
                    case "/vault": {
                        this.activeRoute = 2;
                        break;
                    }
                    case "/profile": {
                        this.activeRoute = 4;
                        break;
                    }
                    default: {
                        this.activeRoute = 0;
                        break;
                    }
                }
            }
        });
    }
}
