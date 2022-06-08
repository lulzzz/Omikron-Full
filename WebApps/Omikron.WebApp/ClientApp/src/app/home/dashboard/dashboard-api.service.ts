import { DashboardAccountGroupingViewModel, Refresh, TotalSummary } from './dashboard.models';
import { Observable } from 'rxjs';
import { AppConfig } from '../../app-config';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from 'src/app/shared/http-service/base-api.service';
import { ApiResponse, UserBase } from 'src/app/shared';

@Injectable()
export class DashboardApiService extends BaseApiService {

    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    getAccountsSummary(user: UserBase): Observable<ApiResponse<DashboardAccountGroupingViewModel>> {
        return this.get<ApiResponse<DashboardAccountGroupingViewModel>>(`${this.baseUrl}/${user.id}/get-accounts`);
    }

    getTotalSummary(user: UserBase): Observable<ApiResponse<TotalSummary>> {
        return this.get<ApiResponse<TotalSummary>>(`${this.baseUrl}/${user.id}/get-summary`);
    }

    getLastRefresh(user: UserBase): Observable<ApiResponse<Refresh>> {
        return this.get<ApiResponse<Refresh>>(`${this.baseUrl}/${user.id}/get-last-refresh`);
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.vaultService.url}/${this.appConfig.appSettings.apiEndpoints.vaultService.version}/vault`;
    }
}
