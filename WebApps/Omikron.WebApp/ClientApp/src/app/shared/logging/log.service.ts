import { Injectable, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Log } from './log';
import { 
    LogEventType,
    ILogException,
    ILogPayload
} from './abstract';

@Injectable({
    providedIn: 'root'
})
export class LogService {
    private observable: Observable<Log>;
    private subject: Subject<Log>;

    constructor() {
        this.subject = new Subject<Log>();
        this.observable = this.subject;
    }

    getObservable(): Observable<Log>
    {
        return this.observable;
    }

    information(payload: ILogPayload): void {
        payload.name = "Information";
        this.subject.next({eventType: LogEventType.Information, payload: payload});
    }

    warning(payload: ILogPayload): void {
        payload.name = "Warning";
        this.subject.next({eventType: LogEventType.Warning, payload: payload});
    }

    debug(payload: ILogPayload): void {
        payload.name = "Debug";
        this.subject.next({eventType: LogEventType.Debug, payload: payload});
    }

    error(payload: ILogException): void {
        payload.name = "Error";
        this.subject.next({eventType: LogEventType.Error, payload: payload});
    }

    fatal(payload: ILogException): void {
        payload.name = "Fatal";
        this.subject.next({eventType: LogEventType.Fatal, payload: payload});
    }
}
