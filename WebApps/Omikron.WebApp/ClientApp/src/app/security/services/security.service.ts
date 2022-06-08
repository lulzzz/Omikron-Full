import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from 'src/app/app-config';
import { ApiErrorResponse, ApiResponse } from 'src/app/shared';
import { BaseApiService } from 'src/app/shared/http-service/base-api.service';
import { UserLoginDetails, UserVerificationDetails } from 'src/app/users/user.models';

import { MyDetails } from '../models/my-details';
import { Register } from '../models/register';


@Injectable()
export class SecurityService extends BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    phoneVerification(number: string, email: string): Observable<ApiResponse<string> | ApiErrorResponse> {
        return this.post<ApiResponse<string> | ApiErrorResponse>(`${this.baseUrl}/phone-number`, { number: number, email: email })
    }

    createUser(user: Register): Observable<ApiResponse<string> | ApiErrorResponse> {
        return this.post<ApiResponse<string> | ApiErrorResponse>(`${this.baseUrl}/create`, user);
    }

    addUserDetails(user: MyDetails): Observable<ApiResponse<string> | ApiErrorResponse> {
        return this.put<ApiResponse<string> | ApiErrorResponse>(`${this.baseUrl}/kyc`, user);
    }

    login(userLoginDetails: UserLoginDetails): Observable<ApiResponse<string>> {
        return this.post<ApiResponse<string>>(`${this.baseUrl}/login`, userLoginDetails);
    }

    loginVerification(verificationDetails: UserVerificationDetails): Observable<ApiResponse<void>> {
        return this.post<ApiResponse<void>>(`${this.baseUrl}/login-verification`, verificationDetails);
    }

    activateRememberMe(userName: string, rememberMeAt : Date): Observable<ApiResponse<string>> {
        return this.put<ApiResponse<string>>(`${this.baseUrl}/remember-me`, {userName: userName, rememberMeAt: rememberMeAt });
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.identityService.url}/${this.appConfig.appSettings.apiEndpoints.identityService.version}/user`;
    }
}
