import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AppConfig } from "../../app-config";
import { Observable } from "rxjs";
import { BaseApiService } from '../http-service/base-api.service';
import { ApiResponse, UserBase, Constants } from '../models/shared.models';
import { TenantSummary } from './form.models';

@Injectable()
export class FormApiService extends BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    searchTenantSummary(keyword: string): Observable<ApiResponse<TenantSummary[]>> {
        return this.get<ApiResponse<TenantSummary[]>>(`${this.tenantBaseUrl}/search/summary?keyword=${keyword}`);
    }

    searchUsers(keyword: string, tenantIdentifier: string): Observable<ApiResponse<UserBase[]>> {
        return this.get<ApiResponse<UserBase[]>>(`${this.userBaseUrl}/search?keyword=${keyword}&${Constants.TenantIdQueryKey}=${tenantIdentifier}`);
    }

    get userBaseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.identityService.url}/${this.appConfig.appSettings.apiEndpoints.identityService.version}/user`;
    }

    get tenantBaseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.tenantService.url}/${this.appConfig.appSettings.apiEndpoints.tenantService.version}/tenant`;
    }
}