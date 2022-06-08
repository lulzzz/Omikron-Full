import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AppConfig } from "../app-config";
import { Observable } from "rxjs";
import { BaseApiService } from '../shared/http-service/base-api.service';
import { Tenant } from './tenants.models';
import { ApiResponse, ApiErrorResponse, UserBase } from '../shared/models/shared.models';


@Injectable()
export class TenantsApiService extends BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    createTenant(tenant: Tenant): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.post<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}`, tenant);
    }

    searchTenants(keyword: string, page: number): Observable<ApiResponse<Tenant[]>> {
        return this.get<ApiResponse<Tenant[]>>(`${this.baseUrl}?keyword=${keyword}&page=${page}`);
    }

    getTenantById(id: string): Observable<ApiResponse<Tenant>> {
        return this.get<ApiResponse<Tenant>>(`${this.baseUrl}/${id}`);
    }

    updateBasicInformation(tenant: Tenant): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/${tenant.id}/basic-information`, tenant);
    }

    deleteLogoImage(tenantId: string): Observable<ApiResponse<void>> {
        return this.delete<ApiResponse<void>>(`${this.baseUrl}/${tenantId}/logo-image`);
    }

    deleteCssFile(tenantId: string): Observable<ApiResponse<void>> {
        return this.delete<ApiResponse<void>>(`${this.baseUrl}/${tenantId}/css`);
    }

    updateStatus(tenant: Tenant): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/${tenant.id}/status`, tenant);
    }

    createSqlDatabaseTenant(tenantId: string): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/${tenantId}/db`);
    }

    deleteTenant(tenantId: string): Observable<ApiResponse<void>> {
        return this.delete<ApiResponse<void>>(`${this.baseUrl}/${tenantId}`);
    }

    addAdministrator(tenantId: string, user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.post<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${tenantId}/administrator`, { firstName: user.firstName, lastName: user.lastName, email: user.username });
    }

    updateAdministrator(tenantId: string, user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${tenantId}/administrator`, { firstName: user.firstName, lastName: user.lastName, email: user.username });
    }

    deleteAdministrator(tenantId: string, userId: string): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.delete<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${tenantId}/administrator/${userId}`);
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.tenantService.url}/${this.appConfig.appSettings.apiEndpoints.tenantService.version}/tenant`;
    }
}