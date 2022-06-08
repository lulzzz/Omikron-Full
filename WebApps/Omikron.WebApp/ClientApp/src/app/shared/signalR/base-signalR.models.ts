export enum SignalStatus {
    Accepted = 1,
    Rejected = 2,
    Error = 3
}

export interface IBaseSignal {
}


export interface ISignalResponse<TPayloadType> {
    status?: SignalStatus;
    message?: string;
    payload?: TPayloadType;
}
