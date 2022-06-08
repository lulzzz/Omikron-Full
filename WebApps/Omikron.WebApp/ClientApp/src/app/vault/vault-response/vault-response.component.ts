import { Component, OnInit } from '@angular/core';
import { Event as NavigationEvent, NavigationEnd, Router } from '@angular/router';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { DashboardView } from '../../layout/models/dashboard-view';

@Component({
    selector: "app-vault-response",
    templateUrl: "./vault-response.component.html",
    styleUrls: ["./vault-response.component.scss"],
})
export class VaultResponseComponent implements OnInit {
    responseState: boolean;
    redirectAddAccount: string = "/vault";

    constructor(private router: Router,
        private eventBusService : EventBusService) {
        this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));
        this.router.events.forEach((event: NavigationEvent) => {
            if (event instanceof NavigationEnd) {
                switch (event.url.split("&")[0]) {
                    case "/vault/response?status=success": {
                        localStorage.setItem("responseState", "true");
                        break;
                    }
                    case "/vault/response?status=failure": {
                        localStorage.setItem("responseState", "false");
                        break;
                    }
                }
            }
        });
    }

    ngOnInit(): void {
        this.responseState = localStorage.getItem("responseState") === 'true';
    }
}
