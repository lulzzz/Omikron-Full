import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/shared/auth.service';

import { DashboardApiService } from './../dashboard-api.service';
import { DashboardAccountGroupingViewModel, TotalSummary } from './../dashboard.models';

@Component({
    selector: 'app-assets-and-liabilities',
    templateUrl: './assets-and-liabilities.component.html',
    styleUrls: ['./assets-and-liabilities.component.scss']
})
export class AssetsAndLiabilitiesComponent implements OnInit {
    @Output() shareSummary = new EventEmitter();
    dashboardAccountGroupingViewModel: DashboardAccountGroupingViewModel;

    icons: { [key: string]: string } = {
        "Credit Cards": "ri-bank-card-fill",
        "Current Accounts": "ri-wallet-fill",
        "Savings": "ri-coin-fill",
        "Pensions": "ri-vip-diamond-fill",
        "Investments": "ri-flashlight-fill",
        "Properties": "ri-community-fill",
        "Vehicles": "ri-car-fill",
        "Loans": "ri-hand-coin-fill",
        "Mortgages": "ri-bank-fill",
        "Finance Agreements": "ri-file-list-3-line",
        "Personal Items": "ri-user-6-fill"
    }

    constructor(
        private dashboardApiService: DashboardApiService,
        private authService: AuthService
    ) { }

    ngOnInit(): void {
        this.getAssetsAndLiabilities();
    }

    private getAssetsAndLiabilities() {
        this.dashboardApiService.getAccountsSummary(this.authService.getUserProfile()).subscribe(
            data => {
                this.dashboardAccountGroupingViewModel = data.records;
                this.emitSummary();
            },
            error => {
                console.log(error);
            }
        );
    }

    private emitSummary() {
        let summary: TotalSummary = {
            assets: this.dashboardAccountGroupingViewModel.assets.total,
            liabilities: this.dashboardAccountGroupingViewModel.liabilities.total,
            net: this.dashboardAccountGroupingViewModel.assets.total + this.dashboardAccountGroupingViewModel.liabilities.total
        }
        this.shareSummary.emit(summary);
    }
}
