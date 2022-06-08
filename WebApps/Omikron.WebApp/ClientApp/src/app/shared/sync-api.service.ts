import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiErrorResponse, ApiResponse } from '.';
import { AppConfig } from '../app-config';
import { BaseApiService } from './http-service/base-api.service';

@Injectable()
export class SyncApiService extends BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    startSync(): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.post<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/start-sync`, {});
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.syncService.url}/${this.appConfig.appSettings.apiEndpoints.syncService.version}/sync`;
    }
}
