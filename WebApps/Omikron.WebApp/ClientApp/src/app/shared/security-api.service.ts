import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiErrorResponse, ApiResponse } from '.';
import { AppConfig } from '../app-config';
import { BaseApiService } from './http-service/base-api.service';

@Injectable()
export class SecurityApiService extends BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    resetPassword(email: string, bypassEmailConfirmation: boolean = false): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/reset-password`, { email: email, bypassEmailConfirmation: bypassEmailConfirmation });
    }

    generateToken(email: string): Observable<ApiResponse<string>> {
        return this.put<ApiResponse<string>>(`${this.baseUrl}/generate-token`, { email: email });
    }

    changePassword(email: string, password: string, token: number): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/password-reset`, { email: email, password: password, verificationToken: token });
    }

    confirmUserEmailByToken(token: string): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/confirm-email`, { token: token });
    }

    resetEmailToken(token: string): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/reset-email-token`, { token: token });
    }

    resetEmailTokenByUserId(userId: string): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${userId}/reset-email-token`, { userId: userId });
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.identityService.url}/${this.appConfig.appSettings.apiEndpoints.identityService.version}/user`;
    }
}
