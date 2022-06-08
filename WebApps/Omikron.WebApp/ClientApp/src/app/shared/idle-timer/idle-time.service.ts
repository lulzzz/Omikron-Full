import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { DashboardView } from '../../layout/models/dashboard-view';
import { AuthService } from './../auth.service';

@Injectable()
export class IdleTimerService {
    eventHandler;
    interval;
    timeoutTracker;
    timeout = 600;
    interactionTrackDelayTime = 300;
    expiredTimeRecalculationTime = 1000;

    constructor(
        private authService: AuthService,
        private eventBusService: EventBusService,
        private modalService: NgbModal
    ) {}

    start() {
        this.eventHandler = this.updateExpiredTime.bind(this);
        this.tracker();
        this.startInterval();
    }

    onTimeout() {
        this.modalService.dismissAll();
        localStorage.removeItem("logoutActivated");
        this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));
        this.authService.logOut();
    }

    startInterval() {
        this.updateExpiredTime();
        this.interval = setInterval(() => {
            const expiredTime = parseInt(
                localStorage.getItem("_expiredTime"),
                10
            );
            if (expiredTime < Date.now()) {
                this.onTimeout();
                this.cleanUp();
            }
        }, this.expiredTimeRecalculationTime);
    }

    updateExpiredTime() {
        if (this.timeoutTracker) {
            clearTimeout(this.timeoutTracker);
        }
        this.timeoutTracker = setTimeout(() => {
            localStorage.setItem(
                "_expiredTime",
                (Date.now() + this.timeout * 1000).toString()
            );
        }, this.interactionTrackDelayTime);
    }

    tracker() {
        window.addEventListener("mousemove", this.eventHandler);
        window.addEventListener("scroll", this.eventHandler);
        window.addEventListener("keydown", this.eventHandler);
    }

    cleanUp() {
        localStorage.removeItem("_expiredTime");
        clearInterval(this.interval);
        window.removeEventListener("mousemove", this.eventHandler);
        window.removeEventListener("scroll", this.eventHandler);
        window.removeEventListener("keydown", this.eventHandler);
    }
}
