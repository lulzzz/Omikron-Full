import { Injectable } from "@angular/core";
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from "@angular/common/http";
import { Observable } from "rxjs";
import { TenantResolverService } from '../tenant-resolver.service';
import { OAuthStorage } from 'angular-oauth2-oidc';
import { Constants } from '../models/shared.models';

@Injectable()
export class HttpTenantAuthorizationInterceptor implements HttpInterceptor {
    constructor(private tenantResolverService: TenantResolverService, private authStorage: OAuthStorage) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // const tenant = this.tenantResolverService.getTenantIdentity();
        const tenant = "omikron";
        const token =  localStorage.getItem(Constants.AccessTokenStorageKey);
        const header = `${Constants.AuthorizationHeaderPrefixKey} ${token}`;

        let headers = request.headers.set(Constants.TenantHeaderKey, tenant);
        if (token) {
            headers = headers.set(Constants.AuthorizationHeaderKey, header);
        }

        request = request.clone({
            headers: headers
        });
        return next.handle(request);
    }
}
