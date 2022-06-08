import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from 'src/app/shared/http-service/base-api.service';

import {
    CategoriesListViewModel,
    DashboardChartData,
    MerchantContainerViewModel,
    NetPositionsChartData,
    VaultEntryListViewModel,
} from '../analytics/analytics.model';
import { AppConfig } from '../app-config';
import { ApiErrorResponse, ApiResponse, UserBase } from '../shared';
import {
    EditInvestment,
    EditManualAccount,
    EditVehicle,
    Investment,
    ManualAccount,
    PersonalItem,
    PersonalItemViewModel,
    Vehicle,
    VehicleViewModel,
} from './models/models';
import {
    AccountDetailsViewModel,
    AssetValue,
    EditPropertyViewModel,
    LoanViewModel,
    ManualAccountDetailsVieWModel,
    ManualAccountViewModel,
    PropertyViewModel,
    TransactionsViewModelContainer,
    VaultAssetType,
    VaultViewModel,
} from './vault.models';

@Injectable({
    providedIn: 'root'
})
export class VaultApiService extends BaseApiService {

    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    getVault(user: UserBase): Observable<ApiResponse<VaultViewModel>> {
        return this.get<ApiResponse<VaultViewModel>>(`${this.baseUrl}/${user.id}/get-vault`);
    }

    getVaultEntryList(user: UserBase, assetLiabilityTypes: string[]): Observable<ApiResponse<VaultEntryListViewModel[]>> {
        return this.get<ApiResponse<VaultEntryListViewModel[]>>(`${this.baseUrl}/${user.id}/vault-entry-list?assetLiabilityTypes=${assetLiabilityTypes.join("&assetLiabilityTypes=")}`);
    }

    getNetPositionsChartData(user: UserBase, monthMode: boolean = true, year: number = null, assetLiabilityTypes: string[] = null, vaultEntries: string[] = null, archivedAccounts: boolean = true): Observable<ApiResponse<NetPositionsChartData[]>> {

        let url = `${this.baseUrl}/${user.id}/net-positions-chart-data?monthMode=${monthMode}&archivedAccounts=${archivedAccounts}`;

        if (monthMode) {
            url = `${url}&year=${year}`;
        }

        if (assetLiabilityTypes.length) {
            url = `${url}&assetLiabilityTypes=${assetLiabilityTypes.join("&assetLiabilityTypes=")}`;
        }

        if (vaultEntries.length) {
            url = `${url}&vaultEntries=${vaultEntries.join("&vaultEntries=")}`;
        }

        return this.get<ApiResponse<NetPositionsChartData[]>>(url);
    }

    getDashboardChartData(user: UserBase, allTimeFilter: boolean, numberOfMonths: number = null): Observable<ApiResponse<DashboardChartData[]>> {
        return this.get<ApiResponse<DashboardChartData[]>>(`${this.baseUrl}/${user.id}/dashboard-chart-data?numberOfMonths=${numberOfMonths}&allTimeFilter=${allTimeFilter}`);
    }

    getAccountDetails(accountId: string): Observable<ApiResponse<AccountDetailsViewModel>> {
        return this.get<ApiResponse<AccountDetailsViewModel>>(`${this.baseUrl}/${accountId}/account-details`);
    }

    getAccountTransactions(accountId: string, page: number, search: string): Observable<ApiResponse<TransactionsViewModelContainer[]>> {
        return this.get<ApiResponse<TransactionsViewModelContainer[]>>(`${this.baseUrl}/${accountId}/transactions?page=${page}&search=${search}`);
    }

    getMinimumAnalyticsDate(user: UserBase): Observable<ApiResponse<string>> {
        return this.get<ApiResponse<string>>(`${this.baseUrl}/${user.id}/min-analytics-date`);
    }

    getTopMerchants(user: UserBase, year: number, page: number, assetLiabilityTypes: string[], vaultEntries: string[], categories: string[], monthIndex: number = null): Observable<ApiResponse<MerchantContainerViewModel>> {
        let url = `${this.baseUrl}/${user.id}/top-merchants?year=${year}&page=${page}`;

        if (monthIndex !== null) {
            url = `${url}&monthIndex=${monthIndex}`;
        }

        if (assetLiabilityTypes.length) {
            url = `${url}&assetLiabilityTypes=${assetLiabilityTypes.join("&assetLiabilityTypes=")}`;
        }

        if (vaultEntries.length) {
            url = `${url}&vaultEntries=${vaultEntries.join("&vaultEntries=")}`;
        }

        if (categories.length) {
            url = `${url}&categories=${categories.join("&categories=")}`;
        }

        return this.get<ApiResponse<MerchantContainerViewModel>>(url);
    }

    getCategoryTransactions(user: UserBase, year: number, assetLiabilityTypes: string[], vaultEntries: string[], categories: string[], monthIndex: number = null): Observable<ApiResponse<CategoriesListViewModel>> {

        let url = `${this.baseUrl}/${user.id}/category-transactions?year=${year}`;
        if (monthIndex !== null) {
            url = `${url}&monthIndex=${monthIndex}`;
        }

        if (assetLiabilityTypes.length) {
            url = `${url}&assetLiabilityTypes=${assetLiabilityTypes.join("&assetLiabilityTypes=")}`;
        }

        if (vaultEntries.length) {
            url = `${url}&vaultEntries=${vaultEntries.join("&vaultEntries=")}`;
        }

        if (categories.length) {
            url = `${url}&categories=${categories.join("&categories=")}`;
        }

        return this.get<ApiResponse<CategoriesListViewModel>>(url);
    }

    revokeConsent(provider: string): Observable<ApiResponse<void>> {
        return this.delete<ApiResponse<void>>(`${this.baseUrl}/revoke-consent/${provider}`);
    }

    addManualAccount(account: ManualAccount): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.post<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/manual-account`, account);
    }

    searchLoans(user: UserBase, search: string): Observable<ApiResponse<LoanViewModel[]>> {
        return this.get<ApiResponse<LoanViewModel[]>>(`${this.baseUrl}/${user.id}/available-loans?search=${search}`);
    }

    getVehicleValue(registration: string, mileage: number): Observable<ApiResponse<AssetValue>> {
        return this.get<ApiResponse<AssetValue>>(`${this.baseUrl}/vehicle-value?registration=${registration}&mileage=${mileage}`);
    }

    addVehicle(vehicle: Vehicle): Observable<ApiResponse<string>> {
        return this.post<ApiResponse<string>>(`${this.baseUrl}/vehicle`, vehicle);
    }

    getVehicleDetails(accountId: string): Observable<ApiResponse<VehicleViewModel>> {
        return this.get<ApiResponse<VehicleViewModel>>(`${this.baseUrl}/vehicle?accountId=${accountId}`);
    }

    updateVehicle(vehicle: EditVehicle): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/vehicle`, vehicle);
    }

    getPropertyValue(postcode: string, numberOfBedrooms: number): Observable<ApiResponse<AssetValue>> {
        return this.get<ApiResponse<AssetValue>>(`${this.baseUrl}/property-value?postcode=${postcode}&numberOfBedrooms=${numberOfBedrooms}`);
    }

    addProperty(property: PropertyViewModel): Observable<ApiResponse<string>> {
        return this.post<ApiResponse<string>>(`${this.baseUrl}/property`, property);
    }

    getPropertyDetails(accountId: string): Observable<ApiResponse<EditPropertyViewModel>> {
        return this.get<ApiResponse<EditPropertyViewModel>>(`${this.baseUrl}/property?accountId=${accountId}`);
    }

    updateProperty(property: PropertyViewModel): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/property`, property);
    }

    addInvestment(investment: Investment): Observable<ApiResponse<string>> {
        return this.post<ApiResponse<string>>(`${this.baseUrl}/investment`, investment);
    }

    addPersonalItem(personalItem: PersonalItem): Observable<ApiResponse<string>> {
        return this.post<ApiResponse<string>>(`${this.baseUrl}/personal-item`, personalItem);
    }

    getManualAccountDetails(accountId: string, itemType: VaultAssetType, financeId: string): Observable<ApiResponse<ManualAccountDetailsVieWModel>> {
        return this.get<ApiResponse<ManualAccountDetailsVieWModel>>(`${this.baseUrl}/manual-account-details/${accountId}/${itemType}/${financeId}`);
    }

    removeManualAccount(accountId: string, accountType: VaultAssetType, isArchived: boolean): Observable<ApiResponse<void>> {
        return this.delete(`${this.baseUrl}/remove-manual-account/${accountId}/${accountType}/${isArchived}`);
    }

    deleteVaultItemPhoto(blobName: string): Observable<ApiResponse<string>> {
        return this.delete<ApiResponse<string>>(`${this.baseUrl}/vault-item-photo/${blobName}`);
    }

    getPersonalItemDetails(accountId: string): Observable<ApiResponse<PersonalItemViewModel>> {
        return this.get<ApiResponse<PersonalItemViewModel>>(`${this.baseUrl}/personal-item?accountId=${accountId}`)
    }

    updatePersonalItem(personalItem: PersonalItemViewModel): Observable<ApiResponse<void>> {
        return this.put(`${this.baseUrl}/personal-item`, personalItem);
    }

    getInvestmentDetails(accountId: string): Observable<ApiResponse<Investment>> {
        return this.get<ApiResponse<Investment>>(`${this.baseUrl}/investment?accountId=${accountId}`);
    }

    updateInvestment(investment: EditInvestment): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/investment`, investment);
    }

    getManualEditAccountDetails(accountId: string): Observable<ApiResponse<EditManualAccount>> {
        return this.get<ApiResponse<EditManualAccount>>(`${this.baseUrl}/manual-account?accountId=${accountId}`);
    }

    getManualAccount(accountId: string): Observable<ApiResponse<ManualAccountViewModel>> {
        return this.get<ApiResponse<ManualAccountViewModel>>(`${this.baseUrl}/manual-account?accountId=${accountId}`);
    }

    updateManualAccount(account): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/manual-account`, account);
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.vaultService.url}/${this.appConfig.appSettings.apiEndpoints.vaultService.version}/vault`;
    }
}
