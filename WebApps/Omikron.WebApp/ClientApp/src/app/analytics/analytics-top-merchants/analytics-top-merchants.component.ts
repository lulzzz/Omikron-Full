import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MonthsList } from 'src/app/shared';
import { AuthService } from 'src/app/shared/auth.service';

import { MerchantContainerViewModel } from '../analytics.model';
import { VaultApiService } from './../../vault/vault-api.service';
import { Filters } from './../analytics.model';
import { AnalyticsService } from './../analytics.service';

@Component({
    selector: 'app-analytics-top-merchants',
    templateUrl: './analytics-top-merchants.component.html',
    styleUrls: ['./analytics-top-merchants.component.scss']
})
export class AnalyticsTopMerchantsComponent implements OnInit {
    merchantContainerViewModel: MerchantContainerViewModel = {
        merchants: [],
        currency: "GBP",
        numberOfMerchants: 0,
        totalValue: 0
    }

    page: number = 1;
    pageSize: number = 10;
    showLoadMoreButton: boolean = true;
    date: string;

    filters: Filters = {
        assetLiabilityTypes: [],
        vaultEntries: [],
        categories: [],
        archived: true
    }

    subscriptions: Subscription[] = [];
    constructor(private vaultApiService: VaultApiService, private analyticsService: AnalyticsService, private authService: AuthService) { }

    ngOnInit(): void {
        this.initiateDate();
        this.filterMerchants();
    }

    private filterMerchants() {
        this.subscriptions.push(this.analyticsService.filters$.subscribe(
            data => {
                this.page = 1;
                this.filters = data;
                this.retrieveMerchants();
            }
        ));
    }

    private initiateDate() {
        this.subscriptions.push(this.analyticsService.dateChanged$.subscribe(
            data => {
                this.page = 1;
                this.date = data;
                this.retrieveMerchants();
            }
        ));
    }

    nextPage() {
        this.page++;
        this.retrieveMerchants();
    }

    retrieveMerchants() {
        let components = this.date.split(", ");

        // Only year is sent
        if (components.length === 1) {
            this.getTopMerchants(+this.date, this.page);
        }
        // Month and year are sent
        else {
            let month = MonthsList.indexOf(components[0]);
            let year = components[1];
            this.getTopMerchants(+year, this.page, month + 1);
        }
    }

    getTopMerchants(year: number, page: number, monthIndex: number = null) {
        this.vaultApiService.getTopMerchants(this.authService.getUserProfile(),year, page, this.filters.assetLiabilityTypes, this.filters.vaultEntries, this.filters.categories, monthIndex).subscribe(
            data => {
                if (page === 1) {
                    this.merchantContainerViewModel.merchants = data.records.merchants;
                }
                else {
                    this.merchantContainerViewModel.merchants = this.merchantContainerViewModel.merchants.concat(data.records.merchants);
                }

                this.merchantContainerViewModel.currency = data.records.currency;
                this.merchantContainerViewModel.numberOfMerchants = data.records.numberOfMerchants;
                this.merchantContainerViewModel.totalValue = data.records.totalValue;

                this.showLoadMoreButton = data.records.merchants.length === this.pageSize;
            },
            error => {
                console.error(error);
            });
    }

    ngOnDestroy() {
        this.subscriptions.forEach(s => s.unsubscribe());
    }
}
