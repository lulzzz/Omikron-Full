import { Component, OnInit } from '@angular/core';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { LayoutService } from '../../layout/autentificated/layout/layout.service';
import { DashboardView } from '../../layout/models/dashboard-view';
import { UserBase } from '../../shared';
import { AuthService } from '../../shared/auth.service';
import { TotalSummary } from './dashboard.models';

@Component({
    selector: "dashboard",
    templateUrl: "dashboard.component.html",
})
export class DashboardComponent implements OnInit {
    user: UserBase;
    totalSummary: TotalSummary;
    paragraph: string = "Here’s what is happening in your wallet";

    constructor(
        private eventBusService: EventBusService,
        private layoutService: LayoutService,
        private authService: AuthService
    ) {
        this.eventBusService.emit(
            new EmitEvent(Events.LayoutSelected, DashboardView.PublicAdmin)
        );
    }

    ngOnInit(): void {
        this.user = this.authService.getUserProfile();
        this.sendHeaderContent();
    }

    private sendHeaderContent() {
        this.layoutService.sendHeaderContent({
            header: `Welcome ${this.user.firstName},`,
            paragraph: this.paragraph,
            vaultHeader: false,
        });
    }

    saveSummary(summary: TotalSummary) {
        this.totalSummary = summary;
    }
}
