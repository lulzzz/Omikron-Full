import { Injectable } from "@angular/core";
import { AppConfig } from '../app-config';
import { Tenant } from '../tenants/tenants.models';

@Injectable()
export class TenantResolverService {
    constructor(private appConfig: AppConfig) {}

    getTenantIdentity(): string {
        const parts = location.hostname.split('.');
        if (parts.length < 2)
            return 'omikron';

        return parts[0];
    }

    getTenant(): Tenant {
       return this.appConfig.tenant;
    }
}
