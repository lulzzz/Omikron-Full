import { TotalSummary } from '../home/dashboard/dashboard.models';

export interface MerchantContainerViewModel {
    numberOfMerchants: number,
    totalValue: number,
    currency: string,
    merchants: MerchantViewModel[]
}

export interface MerchantViewModel {
    logo: string,
    transactions: number,
    currency: string,
    amount: number
}

export interface UserRegistrationDateViewModel {
    day: number;
    month: number;
    year: number;
}

export interface CategoriesListViewModel {
    expenditure: CategoryViewModel[],
    income: CategoryViewModel[],
    totalExpenditure: number,
    totalIncome: number,
    totalNumberOfExpenditureTransactions: number,
    totalNumberOfIncomeTransactions: number
}

export interface CategoryViewModel {
    categoryName: string,
    amount: number,
    numberOfTransactions: number,
    icon: string
}

export interface InitialIncomeAndExpenditure {
    income: number;
    expenditure: number;
}

export interface PieChartData {
    category: string;
    amount: number;
}

export interface NetPositionsChartData {
    monthIndex: number,
    data: TotalSummary
}

export interface DashboardChartData {
    dateIndex: number,
    data: TotalSummary
}

export interface VaultEntryListViewModel {
    groupName: string,
    vaultEntries: VaultEntry[]
}

export interface VaultEntry {
    vaultEntryId: string,
    vaultEntryName: string
}

export interface MinDateData {
    minYear: number;
    minMonth: number;
}

export enum TimePeriod {
    Month = "Month",
    Year = "Year"
}

export interface Filters {
    assetLiabilityTypes: string[],
    vaultEntries: string[],
    categories: string[],
    archived:boolean
}
