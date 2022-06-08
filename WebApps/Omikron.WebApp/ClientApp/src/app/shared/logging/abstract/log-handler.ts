import { LogEventType } from "./log-event-type";
import { ILogPayload } from './log-payload';

export declare interface ILogHandler
{
    eventType: LogEventType;
    handler: (logPayload: ILogPayload) => void;
}