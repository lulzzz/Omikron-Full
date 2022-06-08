import { Injectable } from "@angular/core";
import { Tenant } from './tenants/tenants.models';

@Injectable({ providedIn: "root" })
export class AppConfig {
    tenant: Tenant = {
        id: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
        identifier: "omikron",
        name: "Omikron Money Solution",
        logo: "images/software-logo.png",
        status: 1,
        assetViewModel: {
            assetStatus: 4,
            assertCreationIsTooLong: false,
            sqlDatabaseResourceUri: "",
            sqlDatabaseName: ""
        },
        administrators: [],
        isReadyForUse: true,
        cssFile: "",
        cssStyleContent: ""
    };
    appSettings: {
        apiEndpoints: {
            tenantService: {
                version: string;
                url: string
            };
            identityService: {
                version: string;
                url: string
            };
            supportingService: {
                version: string;
                url: string
            };
            auditService: {
                version: string;
                url: string
            };
            reportingService: {
                version: string;
                url: string;
            };
            notificationService: {
                version: string;
                url: string;
            };
            vaultService: {
                version: string;
                url: string;
            };
            syncService: {
                version: string;
                url: string;
            };
        };
        idp: {
            clientId: string;
            clientSecret: string;
            scope: string;
            endpoint: string;
        };
        hostname: string;
        documentationLink: string;
        logging: {
            appInsights: {
                instrumentationKey: string;
                enabled: boolean;
            };

            console: {
                enabled: boolean;
            }
        },
        reRecaptchaSettings: {
            enabled: boolean;
            key: string;
        },
        languageSettings: {
            loadLangaugeByRegionalSettings: boolean;
            defaultLanguage: string;
        }
    };
}
