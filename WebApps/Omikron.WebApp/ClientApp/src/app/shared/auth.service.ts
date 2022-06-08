import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { Subject } from 'rxjs';

import { UserBase } from '.';
import { AppConfig } from '../app-config';
import { CurrentUserInfoSharingService } from '../users/services/user-currentuserinfo-sharing.service';
import { UsersApiService } from '../users/users-api.service';
import { Claims, Constants } from './models/shared.models';

@Injectable()
export class AuthService {
    onAuthenticationSucceed = new Subject<UserBase>();

    constructor(
        private oauthService: OAuthService,
        private usersApiService: UsersApiService,
        private appConfig: AppConfig,
        private router: Router,
        private readonly userInfoService: CurrentUserInfoSharingService
    ) {
        this.initOauthService();
    }

    login(user: string, password: string): Promise<object> {
        return this.oauthService
            .fetchTokenUsingPasswordFlowAndLoadUserProfile(user, password)
            .then(user => {
                const userProfile = this.getUserProfile();
                return this.usersApiService
                    .getPermissionsByUserId(userProfile.id)
                    .toPromise();
            })
            .then(response => {
                localStorage.setItem(Constants.UserPermissionsStorageKey, JSON.stringify(response.records));
                const user = this.getUserProfile();
                this.onAuthenticationSucceed.next(user);
                return user;
            });
    }

    logOut(): void {
        this.oauthService.logOut(true);
        localStorage.removeItem(Constants.UserPermissionsStorageKey);
        this.router.navigate(["/authenticate/login"]);
    }

    isLoggedIn(): boolean {
        return this.oauthService.hasValidAccessToken();
    }

    getAccessToken(): string {
        return this.oauthService.getAccessToken();
    }

    hasValidAccessToken(): boolean {
        return this.oauthService.hasValidAccessToken();
    }

    authorizationHeader(): string {
        return this.oauthService.authorizationHeader();
    }

    getIdentityClaims(): object {
        return this.oauthService.getIdentityClaims();
    }

    getPermissions(): string[] {
        const userPermissionsPayload = localStorage.getItem(Constants.UserPermissionsStorageKey);
        if (userPermissionsPayload) {
            return <string[]>JSON.parse(userPermissionsPayload) || [];
        }
        return [];
    }

    getBlobAccessToken(): string {
        const claims = this.getIdentityClaims();
        return claims[Claims.BlobAccessToken];
    }

    updateClaims(user: UserBase) {
        const key = "id_token_claims_obj";
        const payload = localStorage.getItem(key);
        const claims = JSON.parse(payload);

        claims[Claims.FirstName] = user.firstName;
        claims[Claims.LastName] = user.lastName;
        claims[Claims.ProfilePhoto] = user.profilePhoto;
        claims[Claims.ProfilePhoto] = user.profilePhoto;
        claims[Claims.AccountStatus] = user.accountStatus;
        claims[Claims.PhoneNumber] = user.phoneNumber;
        claims[Claims.Username] = user.username;

        localStorage.setItem(key, JSON.stringify(claims));
        this.userInfoService.updateUserInfo(user);
    }

    getUserProfile(): UserBase {
        const claims = this.getIdentityClaims();
        const user = <UserBase>{
            firstName: claims[Claims.FirstName],
            lastName: claims[Claims.LastName],
            id: claims[Claims.UserId],
            username: claims[Claims.Username],
            profilePhoto: claims[Claims.ProfilePhoto],
            tenantIdentifier: claims[Claims.TenantIdentifier],
            tenantName: claims[Claims.TenantName],
            accountStatus: claims[Claims.AccountStatus],
            phoneNumber: claims[Claims.PhoneNumber],
            roles: claims[Claims.Roles]
        };

        return user;
    }

    initOauthService(): void {
        console.log(this.appConfig.appSettings);
        this.oauthService.setStorage(localStorage);
        this.oauthService.clientId = this.appConfig.appSettings.idp.clientId;
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        this.oauthService.dummyClientSecret = this.appConfig.appSettings.idp.clientSecret;
        this.oauthService.scope = this.appConfig.appSettings.idp.scope;
        this.oauthService.oidc = false;
        this.oauthService.tokenEndpoint = `${this.appConfig.appSettings.idp.endpoint}/connect/token`;
        this.oauthService.userinfoEndpoint = `${this.appConfig.appSettings.idp.endpoint}/connect/userinfo`;
        this.oauthService.logoutUrl = `${this.appConfig.appSettings.idp.endpoint}/connect/endsession`;
    }
}
