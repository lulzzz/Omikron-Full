import { AuthService } from './../../shared/auth.service';
import { VaultApiService } from 'src/app/vault/vault-api.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { AnalyticsService } from '../analytics.service';
import { Months, MonthsList } from './../../shared/models/shared.models';
import { TimePeriod } from './../analytics.model';

@Component({
    selector: 'app-analytics-date',
    templateUrl: './analytics-date.component.html',
    styleUrls: ['./analytics-date.component.scss']
})
export class AnalyticsDateComponent implements OnInit {
    months: string[] = MonthsList;
    currentMonth: number;
    minMonth: number;
    maxMonth: number;
    currentYear: number;
    maxYear: number;
    minYear: number;
    selectedTimePeriod: string = TimePeriod.Month;
    subscriptions: Subscription[] = [];

    constructor(private analyticsService: AnalyticsService, private vaultApiService: VaultApiService, private authService: AuthService) { }

    ngOnInit(): void {
        this.initiateDate();
        this.changeTimePeriod();
        this.setMinimumDate();
    }

    initiateDate() {
        let date = new Date();
        this.maxMonth = date.getMonth();
        this.currentMonth = date.getMonth();

        this.maxYear = date.getFullYear();
        this.currentYear = date.getFullYear();
        this.sendDate();
        this.analyticsService.sendYear(this.currentYear);
    }

    nextDate() {
        if (this.selectedTimePeriod === TimePeriod.Month) {
            this.currentMonth++;

            if (this.currentMonth === Months.December + 1) {
                this.currentMonth = Months.January;
                this.currentYear++;
                this.analyticsService.sendYear(this.currentYear);
            }
        }
        else {
            this.currentYear++;
        }
        this.sendDate();
    }

    previousDate() {
        if (this.selectedTimePeriod === TimePeriod.Month) {
            this.currentMonth--;

            if (this.currentMonth === Months.January - 1) {
                this.currentMonth = Months.December;
                this.currentYear--;
                this.analyticsService.sendYear(this.currentYear);
            }
        }
        else {
            this.currentYear--;
        }
        this.sendDate();
    }

    sendDate() {
        let date = this.selectedTimePeriod === TimePeriod.Month ? `${this.months[this.currentMonth]}, ` : "";
        this.analyticsService.sendDate(`${date}${this.currentYear}`)
    }

    ngOnDestroy() {
        this.subscriptions.forEach(s => s.unsubscribe());
    }

    private setMinimumDate() {
        this.vaultApiService.getMinimumAnalyticsDate(this.authService.getUserProfile()).subscribe(
            data => {
                var date = new Date(data.records);
                this.minYear = date.getFullYear();
                this.minMonth = date.getMonth();
            }
        );

    }

    private changeTimePeriod() {
        this.subscriptions.push(this.analyticsService.selectedTimePeriod$.subscribe(data => {
            this.selectedTimePeriod = data;
            this.sendDate();
        }));
    }
}
