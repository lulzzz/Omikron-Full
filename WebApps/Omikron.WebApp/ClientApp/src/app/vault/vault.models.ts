import { FinanceAgreement } from './models/models';

export interface VaultViewModel {
    accounts: AccountGroupViewModel[],
    assets: AssetGroupViewModel[]
}

export interface AssetGroupViewModel {
    assetType: VaultItemType,
    assetTypeName: string,
    count: number,
    total: number,
    assets: AssetViewModel[]
}

export interface AssetViewModel {
    hostId: string,
    imageUrl: string,
    name: string,
    value: number
}

export enum VaultItemType {
    Account,
    Property,
    Vehicle,
    Investments = "Investments"
}

export interface AccountGroupViewModel {
    accountTypes: AccountType,
    count: number,
    total: number,
    accounts: AccountViewModel[]
}

export enum AccountType {
    CurrentAccount,
    Savings,
    Pensions,
    Investments,
    CreditCards,
    Loans = "Loans"
}

export enum EditAccountType {
    Current,
    Saving,
    Credit,
    Loan,
    Pension,
    Investment,
}

export enum AccountSource {
    BudApi = "Bud API",
    Investment = "Investments",
    Manual = "Manual"
}

export interface AccountViewModel extends AssetViewModel {
    provider: string,
    identificationNumber: string,
    authorizationStatus: string,
    accountType: string,
    assetType: VaultAssetType,
    providerColour: string,
    imageUrl: string
}

export interface AccountDetailsViewModel {
    provider: string,
    identificationNumber: string,
    authorizationStatus: string,
    hostId: string,
    imageUrl: string,
    accountSource: string,
    accountType: string,
    name: string,
    value: number
}

export interface ManualAccountViewModel {
    accountType: number,
    accountName: string,
    accountBalance: number,
    notes: string,
    ownerId: string,
    accountSource: string,
    referenceNumber: string,
    loanType: string,
    assetId: string;
    assetType: string;
    itemPhoto: string;
}

export enum LoanType {
    Mortgage = "Mortgage",
    FinancialAgreement = "FinancialAgreement"
}

export interface TransactionsViewModel {
    merchantName: string,
    merchantLogo: string,
    category: string,
    amount: number,
    currency: string,
    transactionInformation: string,
    creditDebitIndicator: CreditDebitIndicator
}

export enum CreditDebitIndicator {
    Credit = "Credit",
    Debit = "Debit"
}

export enum CreditDebitIntIndicator {
    Credit = 1,
    Debit = 2
}

export interface TransactionsViewModelContainer {
    date: string,
    transactions: TransactionsViewModel[]
}

export interface LoanViewModel {
    id: string;
    name: string;
    balance: number;
}

export interface CategoriesViewModel {
    expenditure: Category[];
    income: Category[];
    totalExpenditure: number;
    totalIncome: number;
    totalNumberOfExpenditureTransactions: number;
    totalNumberOfIncomeTransactions: number;
}

export interface Category {
    categoryName: string;
    amount: number;
    numberOfTransactions: number;
    icon: string;
}

export interface VehicleValueModel {
    value: number;
}

export interface VehicleAddResponse {
    id: string;
}

export interface AssetValue {
    value: number;
}

export interface VehicleAddResponse {
    id: string;
}

export interface PropertyValueViewModel {
    price: number;
}

export interface PropertyViewModel {
    propertyId: string;
    propertyName: string;
    numberOfBedrooms: number;
    postcode: string;
    propertyValue: number;
    automaticallyReValueProperty: boolean;
    userId: string;
    address: string;
    mortgage: FinanceAgreement,
    existingMortgageId: string,
    propertyPhoto: string,
    purchaseValue: number,
    purchaseDate: Date,
}

export interface EditPropertyViewModel extends PropertyViewModel {
    propertyValueChange: boolean;
    mortgageBalanceChange: boolean;
    financeAgreementName: string;
    reference: string;
    notes: string;
}

export interface PropertySubmittedViewModel {
    id: string;
}

export interface DetailsViewModel {
    name: string;
    value: string;
    renderCurrency: boolean;
}

export interface ManualAccountDetailsVieWModel {
    name: string;
    financeName: string;
    financeType: VaultAssetType;
    financeId: string;
    accountId: string;
    totalBalance: number;
    currencyCode: string;
    notes: string;
    itemPhoto: string;
    details: DetailsViewModel[];
    transactions: VaultItemValueViewModel[];
}

export interface VaultItemValueViewModel {
    type: string;
    date: Date;
    amount: string;
    currency: string;
}

export enum VaultAssetType {
    Vehicle = 'Vehicle',
    Property = 'Property',
    Mortgage = 'Mortgage',
    VehicleFinance = 'VehicleFinance',
    Vehicles = 'Vehicles',
    Properties = 'Properties',
    PersonalItem = 'PersonalItem',
    PersonalItems = 'PersonalItems',
    Investment = 'Investment',
    Pension = 'Pension',
    Loan = 'Loan',
    CreditCard = 'CreditCard',
    SavingsAccount = 'SavingsAccount',
    CurrentAccount = 'CurrentAccount',
    Account = 'Account',
    PersonalItemFinance = "PersonalItemFinance"
}
