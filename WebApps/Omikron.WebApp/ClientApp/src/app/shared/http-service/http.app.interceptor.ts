import { Injectable } from "@angular/core";
import { tap } from "rxjs/operators";
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from "@angular/common/http";
import { Observable } from "rxjs";
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { Constants } from '..';

@Injectable()
export class HttpAppInterceptor implements HttpInterceptor {
    constructor(private oauthService: OAuthService, private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            tap(
                event => { },
                error => {
                    if (error.status === 401 || error.status === 403) {
                        this.oauthService.logOut(true);
                        localStorage.removeItem(Constants.UserPermissionsStorageKey);
                        this.router.navigate(["/authenticate/login"]);
                    }
                }
            )
        );
    }
}
