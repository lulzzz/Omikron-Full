import { LogEventType, ILogPayload } from './abstract';

export class Log
{
    eventType: LogEventType;
    payload: ILogPayload;
}
