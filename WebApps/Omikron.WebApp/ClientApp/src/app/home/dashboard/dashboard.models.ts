export interface AccountsSummary {
    types: string;
    total: number;
    currency: string;
}

export interface Account {
    name: string;
    provider: string;
    balance: number;
}

export interface Refresh {
    lastRefresh: string;
}

export interface TotalSummary{
    assets: number;
    liabilities: number;
    net: number;
}

export interface DashboardAccountGroupingViewModel{
    assets: DashboardAccountGroupsViewModel;
    liabilities: DashboardAccountGroupsViewModel;

}

export interface DashboardAccountGroupsViewModel{
    total: number;
    currency: string;
    items: AccountsSummary[]
}

export enum Colors{
    assets= "162, 220, 255",
    assetsLine= "#68C8FF",
    liabilities= "232, 110, 97",
    liabilitiesLine= "#E86E61",
    net= "149, 138, 248",
    netLine= "#958AF8"
}

export enum numberOfMonths{
    zero = 0,
    one = 1,
    three = 3,
    six = 6,
    twelve = 12
}
