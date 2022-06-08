import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    forwardRef,
    OnDestroy,
    OnInit,
    ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';
import { TenantResolverService } from 'src/app/shared/tenant-resolver.service';
import { UserInterfaceUtilityService } from 'src/app/shared/utilities/user-interface-utility.service';
import { Tenant } from 'src/app/tenants/tenants.models';

import { EventBusService } from '../../core/events/event-bus.service';
import { EmitEvent } from '../../core/models/emit-event';
import { Events } from '../../core/models/events';
import { DashboardView } from './../../layout/models/dashboard-view';
import { SecurityLoginFormComponent } from './security-login-form.component';

@Component({
    selector: "security-login",
    templateUrl: "security-login.component.html",
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SecurityLoginComponent implements OnInit, OnDestroy {
    users = [];
    tenant: Tenant;
    remeberMe: boolean;
    bypassForm: boolean;
    error: any;
    verification : boolean;

    @ViewChild(forwardRef(() => SecurityLoginFormComponent))
    loginForm: SecurityLoginFormComponent;

    constructor(
        private readonly userInterfaceUtilityService: UserInterfaceUtilityService,
        private readonly eventBusService : EventBusService,
        private readonly cd: ChangeDetectorRef,
        private readonly tenantResolver: TenantResolverService,
        private readonly router: Router,
        private readonly authService: AuthService
    ) { this.eventBusService.emit(new EmitEvent(Events.LayoutSelected, DashboardView.Public));}

    ngOnInit(): void {
        this.setAuthenticationClass();
        this.tryResolveTenant();
    }

    onError($event: any): void {
        this.error = $event;
        this.cd.markForCheck();
    }

    setAuthenticationClass(): void {
        this.userInterfaceUtilityService.removeBodyClass('bg-light');
        this.userInterfaceUtilityService.addBodyClass('authentication');
    }

    removeAuthenticationClass(): void {
        this.userInterfaceUtilityService.addBodyClass('bg-light');
        this.userInterfaceUtilityService.removeBodyClass('authentication');
    }

    tryResolveTenant(): void {
        this.tenant = this.tenantResolver.getTenant();
        if (!this.tenant) this.router.navigate(["/errors/404"]);
    }

    ngOnDestroy(): void {
        this.removeAuthenticationClass()
    }
}
