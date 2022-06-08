import { ILogPayload } from './log-payload';

export declare interface ILogException extends ILogPayload {
    exception?: Error;
}
