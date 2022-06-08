import { Injectable, OnInit } from '@angular/core';
import { AppConfig } from '../../../app-config';
import { LogService } from '../log.service';
import { filter } from 'rxjs/operators';
import { LogEventType, ILogPayload, ILogException, ILogHandler } from '../abstract';

@Injectable({
    providedIn: 'root'
})
export class ConsoleLogService implements OnInit {
    constructor(private logService: LogService, 
        private appConfig: AppConfig) {
    }

    ngOnInit(): void {
        if(this.appConfig.appSettings.logging.console.enabled)
        {
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
            eventType: LogEventType.Information,
            handler: this.information
        },
        {
            eventType: LogEventType.Warning,
            handler: this.warning
        },
        {
            eventType: LogEventType.Debug,
            handler: this.debug
        },
        {
            eventType: LogEventType.Error,
            handler: this.error
        },
        {
            eventType: LogEventType.Fatal,
            handler: this.fatal
        }];

        return handlers;
    }

    private information(payload: ILogPayload): void {
        console.info(payload);
    }

    private warning(payload: ILogPayload): void {
        console.warn(payload);
    }

    private debug(payload: ILogPayload): void {
        console.debug(payload);
    }

    private error(payload: ILogException): void {
        console.error(payload);
    }

    private fatal(payload: ILogException): void {
        console.error("Fatal: %o", payload);
    }
}
