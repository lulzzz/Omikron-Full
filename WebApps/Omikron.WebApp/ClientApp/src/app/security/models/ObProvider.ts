export interface ObProvider {
    provider :  string;
    displayName : string;
    maintenanceStatus : MaintenanceStatus;
    icon : string;
    regions : string[];
}

export interface ObProviderUrl{
    url: string;
}

export enum NavigateUrl{
    vault = "/vault"
}

export enum MaintenanceStatus{
    Active = "active",
    Inactive = "inactive"
}
