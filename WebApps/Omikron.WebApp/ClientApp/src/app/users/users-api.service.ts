import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserRegistrationDateViewModel } from '../analytics/analytics.model';

import { AppConfig } from '../app-config';
import { EditProfileDetailsCommand, GenerateTokenForNewNumberCommand, GenerateTokenToChangePasswordCommand, PasswordChangeCommand, ProfileDetailsViewModel } from '../profile-and-settings/profile.models';
import { Address } from '../security/models/adress';
import { ObProvider, ObProviderUrl } from '../security/models/ObProvider';
import { ApiErrorResponse, ApiResponse, UserBase } from '../shared';
import { BaseApiService } from '../shared/http-service/base-api.service';

@Injectable()
export class UsersApiService extends BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {
        super(http, appConfig);
    }

    getUserById(userId: string): Observable<ApiResponse<UserBase>> {
        return this.get<ApiResponse<UserBase>>(`${this.baseUrl}/${userId}`);
    }

    getProfileDetails(userId: string): Observable<ApiResponse<ProfileDetailsViewModel>> {
        return this.get<ApiResponse<ProfileDetailsViewModel>>(`${this.baseUrl}/profile-details/${userId}`);
    }

    getPermissionsByUserId(userId: string): Observable<ApiResponse<string[]>> {
        return this.get<ApiResponse<string[]>>(`${this.baseUrl}/${userId}/permissions`);
    }

    getListOfObProviders(searchFilter?: string): Observable<ApiResponse<{ [key: string]: ObProvider[] }>> {
        if (searchFilter != undefined)
            return this.get<ApiResponse<{ [key: string]: ObProvider[] }>>(`${this.baseUrl}/list-ob-providers?search=${searchFilter}`);

        return this.get<ApiResponse<{ [key: string]: ObProvider[] }>>(`${this.baseUrl}/list-ob-providers`);
    }

    getUserAddress(postcode: string): Observable<ApiResponse<Address>> {
        var url = `${this.appConfig.appSettings.apiEndpoints.identityService.url}/${this.appConfig.appSettings.apiEndpoints.identityService.version}`;
        return this.get<ApiResponse<Address>>(`${url}/location?postcode=${postcode}`);
    }

    createAccount(user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.post<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}`, { firstName: user.firstName, lastName: user.lastName, email: user.username, phoneNumber: user.phoneNumber });
    }

    deleteAccount(user: UserBase, token: number, isAdmin: boolean): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.delete<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${user.id}/${token}/${isAdmin}`);
    }

    updateUserBasicInformation(user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/basic-information`, user);
    }

    updateUserBasicInformationById(user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${user.id}/basic-information`, user);
    }

    updateOnboarding(user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/onboarding`, user);
    }

    updateUserAccountRole(user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${user.id}/roles`, user);
    }

    searchUsers(keyword: string, page: number, pageSize: number): Observable<ApiResponse<UserBase[]>> {
        return this.get<ApiResponse<UserBase[]>>(`${this.baseUrl}/search?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    deleteProfileImage(userId: string): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.delete<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${userId}/profile-image`);
    }

    updateUserAccountStatus(user: UserBase): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/${user.id}/account-status`, { status: +user.accountStatus });
    }

    updateMarketingCommunications(receiveMarketingCommunications: boolean): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/update-marketing`, { receiveMarketingCommunications: receiveMarketingCommunications });
    }

    updateAccountNotifications(receiveAccountNotifications: boolean): Observable<ApiResponse<void> | ApiErrorResponse> {
        return this.put<ApiResponse<void> | ApiErrorResponse>(`${this.baseUrl}/update-notifications`, { receiveAccountNotifications: receiveAccountNotifications });
    }

    openBankingLogin(bankProvider: string, redirectUrlScreen: string): Observable<ApiResponse<ObProviderUrl>> {
        return this.post<ApiResponse<ObProviderUrl>>(`${this.baseUrl}/open-banking-login`, { redirectUrl: window.location.origin + redirectUrlScreen, providerName: bankProvider });
    }

    generateTokenForNewNumber(generateTokenForNewNumberCommand: GenerateTokenForNewNumberCommand): Observable<ApiResponse<string>> {
        return this.put<ApiResponse<string>>(`${this.baseUrl}/generate-token-for-new-number`, generateTokenForNewNumberCommand);
    }

    editProfileDetails(editProfileDetailsCommand: EditProfileDetailsCommand): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/edit-profile-details`, editProfileDetailsCommand);
    }

    generateTokenToChangePassword(generateTokenToChangePasswordCommand: GenerateTokenToChangePasswordCommand): Observable<ApiResponse<string>> {
        return this.put<ApiResponse<string>>(`${this.baseUrl}/generate-token-to-change-password`, generateTokenToChangePasswordCommand)
    }

    passwordChange(passwordChangeCommand: PasswordChangeCommand): Observable<ApiResponse<void>> {
        return this.put<ApiResponse<void>>(`${this.baseUrl}/password-change`, passwordChangeCommand)
    }

    getGetUserRegistrationDate(): Observable<ApiResponse<UserRegistrationDateViewModel>> {
        return this.get<ApiResponse<UserRegistrationDateViewModel>>(`${this.baseUrl}/registration-date`)
    }

    get baseUrl(): string {
        return `${this.appConfig.appSettings.apiEndpoints.identityService.url}/${this.appConfig.appSettings.apiEndpoints.identityService.version}/user`;
    }
}
