import { AppConfig } from "src/app/app-config";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

export abstract class BaseApiService {
    constructor(readonly http: HttpClient, readonly appConfig: AppConfig) {}

    get<TResponse>(url: string, headers?: HttpHeaders): Observable<TResponse> {
        return this.http.get<TResponse>(url, { headers: headers });
    }

    post<TResponse>(
        url: string,
        body: any,
        headers?: HttpHeaders
    ): Observable<TResponse> {
        return this.http.post<TResponse>(url, body, { headers: headers });
    }

    put<TResponse>(
        url: string,
        body?: any,
        headers?: HttpHeaders
    ): Observable<TResponse> {
        return this.http.put<TResponse>(url, body, { headers: headers });
    }

    delete<TResponse>(
        url: string,
        headers?: HttpHeaders
    ): Observable<TResponse> {
        return this.http.delete<TResponse>(url, { headers: headers });
    }
}
