import { Component, OnInit } from '@angular/core';
import { DashboardApiService } from 'src/app/home/dashboard/dashboard-api.service';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { AuthService } from 'src/app/shared/auth.service';
import { SyncApiService } from 'src/app/shared/sync-api.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {
    lastUpdateTime: string;
    constructor(
        private authService: AuthService,
        private dashboardApiService: DashboardApiService,
        private syncApiService: SyncApiService,
        private alertNotificationService: AlertNotificationService
    ) {}

    ngOnInit() {
        this.getLastRefresh();
    }

    getLastRefresh() {
        this.dashboardApiService
            .getLastRefresh(this.authService.getUserProfile())
            .subscribe(
                (data) => {
                    this.lastUpdateTime = data.records.lastRefresh;
                },
                (error) => {
                    console.error(error);
                }
            );
    }

    refresh() {
        this.syncApiService.startSync().subscribe();
        this.alertNotificationService.showInfo({text: "Data sync initialized."})
    }
}
