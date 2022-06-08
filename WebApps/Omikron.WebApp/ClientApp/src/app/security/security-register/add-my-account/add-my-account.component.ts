import { Component, Input, OnInit } from '@angular/core';

import { EventBusService } from '../../../core/events/event-bus.service';
import { EmitEvent } from '../../../core/models/emit-event';
import { Events } from '../../../core/models/events';
import { DashboardView } from '../../../layout/models/dashboard-view';
import { LogoutTriggerService } from '../../../shared/utilities/logout-trigger.service';

@Component({
    selector: "app-add-my-account",
    templateUrl: "./add-my-account.component.html",
    styleUrls: ["./add-my-account.component.scss"],
})
export class AddMyAccountComponent implements OnInit {
    redirectUrl: string = "/authenticate/response";
    navigateUrl : string = "/home";
    @Input() darkTheme : boolean;
    constructor(
        private eventBusService : EventBusService,
        private readonly logoutTriggerService: LogoutTriggerService,
    ) {this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));}

    ngOnInit() {
        this.logoutTriggerService.activateLogoutTimer();
    }
}
