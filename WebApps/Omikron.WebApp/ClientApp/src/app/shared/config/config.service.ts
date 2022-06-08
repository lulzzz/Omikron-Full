import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { AppConfig } from '../../app-config';

@Injectable()
export class ConfigService {
    constructor(private http: HttpClient,
        private appConfig: AppConfig) { }

    load(): Promise<any> {
        let env = "";
        if (environment.hasOwnProperty("env")) {
            env = `.${environment["env"]}`;
        }

        const appSettings = `/settings/appsettings${env}.json`;

        const promise = this.http.get<AppConfig>(appSettings)
            .toPromise()
            .then(config =>
                {
                    this.appConfig.appSettings = config.appSettings;
                }
                 )
            .then(() => {
                this.appConfig.tenant = {
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

                return Promise.resolve();
            });

        return promise;
    }
}
