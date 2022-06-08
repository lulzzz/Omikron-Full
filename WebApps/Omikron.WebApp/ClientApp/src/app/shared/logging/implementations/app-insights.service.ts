import { Injectable, OnInit } from '@angular/core';
import { ApplicationInsights } from '@microsoft/applicationinsights-web'
import { AppConfig } from '../../../app-config';
import { LogService } from '../log.service';
import { filter } from 'rxjs/operators';
import { LogEventType, ILogPayload, ILogException, ILogHandler } from '../abstract';

const appInsights = new ApplicationInsights({config: {instrumentationKey: ''}});

@Injectable({
    providedIn: 'root'
})
export class AppInsightsService implements OnInit {
    constructor(private logService: LogService, private appConfig: AppConfig) {
    }

    ngOnInit(): void {
        if(this.appConfig.appSettings.logging.appInsights.enabled) {
            appInsights.config.instrumentationKey = this.appConfig.appSettings.logging.appInsights.instrumentationKey;
            appInsights.loadAppInsights();
            this.setupListeners();
        }
    }

    private setupListeners(): void {
        let handlers = this.getSupportedHandlers();
        let observable = this.logService.getObservable();
        for(let handler of handlers)
        {
            observable
                .pipe(filter(f => f.eventType == handler.eventType))
                .subscribe(payload => handler.handler(payload.payload));
        }
    }

    getSupportedHandlers(): ILogHandler[] {
        let handlers: ILogHandler[] = [{
            eventType: LogEventType.Information | LogEventType.Warning | LogEventType.Debug,
            handler: this.log
        },
        {
            eventType: LogEventType.Error,
            handler: this.logError
        },
        {
            eventType: LogEventType.Fatal,
            handler: this.logFatal
        }];

        return handlers;
    }

    private log(payload: ILogPayload): void {
        if (payload.properties) {
            payload.properties.push({ message: payload.message });
        } else {
            payload.properties = [{ message: payload.message }];
        }

        appInsights.trackEvent({
            name: payload.name,
            properties: payload.properties,
            measurements: payload.measurements
        }, { message: payload.message });
    }

    private logError(payload: ILogException): void {
        let error = payload.exception;
        if(error == null)
        {
            error = new Error(payload.message);
        }

        appInsights.trackException({
            error: error,
            properties: payload.properties,
            measurements: payload.measurements
        });
    }

    private logFatal(payload: ILogException): void {
        let payloadExtra = { "message": payload.message, "fatal": true };
        if(payload.properties)
        {
            payload.properties.push(payloadExtra);
        } else {
            payload.properties = { message: payload.message };
        }

        appInsights.trackException({
            error: payload.exception,
            properties: payload.properties,
            measurements: payload.measurements
        });
    }
}
