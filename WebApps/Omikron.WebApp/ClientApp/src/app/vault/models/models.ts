import { VaultItemValueViewModel } from '../vault.models';

export interface Investment {
    ownerId: string,
    investmentName: string,
    category: string,
    tickerCode: string,
    unitPrice: number,
    quantity: number,
    totalValue: number,
    purchaseValue: number,
    purchaseDate: Date,
    automaticallyRevalueInvestment: boolean
}

export interface EditInvestment extends Investment {
    investmentValueChanged: boolean;
    investmentId: string;
}

export interface InvestmentWithTransactions extends Investment {
    transactions: VaultItemValueViewModel[];
}

export interface ManualAccount {
    type: number,
    name: string,
    balance: number,
    notes: string,
    ownerId: string,
    referenceNumber: string,
    creditDebitIndicator: number,
    openBalance: number,
    openDate: Date
}

export interface EditManualAccount extends ManualAccount {
    accountId: string;
    accountBalanceChanged: boolean;
}

export interface Vehicle {
    vehicleId: string;
    vehicleName: string;
    registration: string;
    mileage: number;
    vehicleValue: number;
    automaticallyReValueVehicle: boolean;
    financeAgreement: FinanceAgreement,
    existingFinanceAgreementId: string,
    userId: string;
    vehiclePhoto: string,
    purchaseValue: number,
    purchaseDate: Date,
}

export interface VehicleViewModel {
    vehicleId: string;
    vehicleName: string;
    registration: string;
    mileage: number;
    vehicleValue: number;
    automaticallyReValueVehicle: boolean;
    financeAgreementName: string;
    newFinanceBalance: number;
    reference: string;
    notes: string;
    vehiclePhoto: string;
}

export interface EditVehicle extends VehicleViewModel {
    vehicleValueChanged: boolean;
    financeBalanceChanged: boolean;
}

export interface VehicleWithTransactions extends VehicleViewModel {
    transactions: VaultItemValueViewModel[];
}

export interface PersonalItem {
    userId: string,
    itemName: string,
    value: number,
    financeAgreement: FinanceAgreement,
    existingFinanceAgreementId: string,
    itemPhoto: string,
    purchaseValue: number,
    purchaseDate: Date,
}

export interface FinanceAgreement {
    name: string,
    notes: string,
    balance: number,
    reference: string,
    openBalance: number,
    openDate: Date
}

export interface PersonalItemViewModel {
    itemName: string;
    value: number;
    financeAgreementName: string;
    newFinanceBalance: number;
    notes: string;
    personalItemValueChange: boolean;
    financeBalanceChange: boolean;
    personalItemId: string;
    itemPhoto: string;
}

export interface PersonalItemWithTransactions extends PersonalItemViewModel {
    transactions: VaultItemValueViewModel[];
}
