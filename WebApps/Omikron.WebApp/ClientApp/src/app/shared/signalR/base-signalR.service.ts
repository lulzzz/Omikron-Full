import * as signalR from "@microsoft/signalr";
import { Subject } from "rxjs";
import { HubConnectionState } from "@microsoft/signalr";
import { AppConfig } from "../../app-config";
import { Constants } from "../../shared/models/shared.models";
import { AuthService } from '../auth.service';


export abstract class BaseSignalRService {

    options: signalR.IHttpConnectionOptions = {
        accessTokenFactory: () => {
            return this.authService.getAccessToken();
          }
    };

    protected hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(`${this.url}?${Constants.TenantIdQueryKey}=${this.appConfig.tenant.identifier}`, this.options)
        .build();

    onConnected = new Subject<HubConnectionState>();
    onDisconnected = new Subject();

    protected constructor(private readonly url: string, protected readonly appConfig: AppConfig, private readonly authService: AuthService) {}

    startConnection(): void {
        this.hubConnection
            .onclose(() => this.onDisconnected.next());

        this.hubConnection
            .start()
            .then(() => this.onConnected.next(this.hubConnection.state))
            .catch(error => console.debug(error));

    };

    closeConnection(): void {
        this.hubConnection
            .stop()
            .catch(err => console.debug(err));
    };
}
