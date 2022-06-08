import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class LogoutTriggerService {
    private triggerLogoutSource = new Subject();

    triggerLogoutActivated$ = this.triggerLogoutSource.asObservable();

    activateLogoutTimer() {
        localStorage.setItem("logoutActivated", "true");
        this.triggerLogoutSource.next();
    }
}