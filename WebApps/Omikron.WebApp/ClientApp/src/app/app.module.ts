import { CurrencyPipe } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import {
    APP_INITIALIZER,
    Injector,
    MissingTranslationStrategy,
    NgModule,
} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { NgxDaterangepickerMd } from 'ngx-daterangepicker-material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardApiService } from './home/dashboard/dashboard-api.service';
import { LayoutModule } from './layout/layout.module';
import { SecurityModule } from './security/security.module';
import { ConfigService } from './shared/config/config.service';
import { HttpAppInterceptor } from './shared/http-service/http.app.interceptor';
import { HttpTenantAuthorizationInterceptor } from './shared/http-service/http.tenant-authorization.interceptor';
import { IdleTimerService } from './shared/idle-timer/idle-time.service';
import { AppInsightsService, ConsoleLogService } from './shared/logging/implementations';
import { LogService } from './shared/logging/log.service';
import { SecurityApiService } from './shared/security-api.service';
import { SharedModule } from './shared/shared.module';
import { SyncApiService } from './shared/sync-api.service';

declare const require;

export function configAndLogsServiceFactory(
    config: ConfigService,
    logService: LogService,
    injector: Injector,
    appInsightsService: AppInsightsService,
    consoleLogService: ConsoleLogService
) {
    return () =>
        config.load()
            .then(() => {
                appInsightsService.ngOnInit();
                consoleLogService.ngOnInit();
            })
            .catch((error: HttpErrorResponse) => {
                const router = injector.get(Router);
                if (error.status === 404) router.navigate(["/errors/404"]);
            })
}

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        SecurityModule,
        HttpClientModule,
        SharedModule,
        LayoutModule,
        NgxDaterangepickerMd
    ],
    providers: [
        ConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: configAndLogsServiceFactory,
            deps: [
                ConfigService,
                LogService,
                Injector,
                ConsoleLogService,
                AppInsightsService
            ],
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpAppInterceptor,
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpTenantAuthorizationInterceptor,
            multi: true
        },
        SecurityApiService,
        SyncApiService,
        IdleTimerService,
        DashboardApiService,
        CurrencyPipe
    ],
    exports: [SecurityModule],
    bootstrap: [AppComponent]
})
export class AppModule { }
