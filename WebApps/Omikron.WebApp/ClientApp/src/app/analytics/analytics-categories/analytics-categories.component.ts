import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/auth.service';
import { VaultApiService } from 'src/app/vault/vault-api.service';

import { CategoriesListViewModel, Filters } from '../analytics.model';
import { AnalyticsService } from '../analytics.service';
import { AlertNotificationService } from './../../overlay/services/alert-notification.service';
import { MonthsList } from './../../shared/models/shared.models';
import { InitialIncomeAndExpenditure, PieChartData } from './../analytics.model';

@Component({
    selector: 'app-analytics-categories',
    templateUrl: './analytics-categories.component.html',
    styleUrls: ['./analytics-categories.component.scss']
})
export class AnalyticsCategoriesComponent implements OnInit {
    categoriesListViewModel: CategoriesListViewModel;
    isBusy: boolean;
    date: string;
    subscriptions: Subscription[] = [];

    filters: Filters = {
        assetLiabilityTypes: [],
        vaultEntries: [],
        categories: [],
        archived: true
    }

    constructor(private vaultService: VaultApiService, private cd: ChangeDetectorRef,
        private analyticsService: AnalyticsService, private alertNotificationService: AlertNotificationService, private authService: AuthService) { }

    ngOnInit(): void {
        this.initiateDate();
        this.filterCategories();
    }

    retrieveCategories() {
        let components = this.date.split(", ");

        // Only year is sent
        if (components.length === 1) {
            this.initiateCategories(+this.date);
        }
        // Month and year are sent
        else {
            let month = MonthsList.indexOf(components[0]);
            let year = components[1];
            this.initiateCategories(+year, month + 1);
        }
    }

    ngOnDestroy() {
        this.subscriptions.forEach(s => s.unsubscribe());
    }

    private filterCategories() {
        this.subscriptions.push(this.analyticsService.filters$.subscribe(
            data => {
                this.filters = data;
                this.retrieveCategories();
            }
        ));
    }

    private initiateDate() {
        this.subscriptions.push(this.analyticsService.dateChanged$.subscribe(
            data => {
                this.date = data;
                this.retrieveCategories();
            }
        ));
    }

    private initiateCategories(year: number, monthIndex: number = null) {
        this.subscriptions.push(this.vaultService.getCategoryTransactions(this.authService.getUserProfile(), year, this.filters.assetLiabilityTypes, this.filters.vaultEntries, this.filters.categories, monthIndex).subscribe(
            (data) => {
                this.categoriesListViewModel = data.records;
                this.setBusy(false);

                this.sendCategories();
                this.sendIncomeAndExpenditure(data.records);
                this.sendPieChartData();
            },
            (error) => {
                console.log(error);
            }
        ));
    }

    private sendPieChartData() {
        let pieChartData = this.categoriesListViewModel.expenditure.map(e => {
            let pieData: PieChartData = {
                category: e.categoryName,
                amount: e.amount
            };
            return pieData;
        });

        this.analyticsService.sendPieChartData(pieChartData);
    }

    private sendCategories() {
        let categories = [...new Set(this.categoriesListViewModel.expenditure.map(c => c.categoryName).concat(this.categoriesListViewModel.income.map(c => c.categoryName)))];
        this.analyticsService.sendCategories(categories);
    }

    private sendIncomeAndExpenditure(data: CategoriesListViewModel) {
        let incomeAndExpenditure: InitialIncomeAndExpenditure = {
            income: data.totalIncome,
            expenditure: data.totalExpenditure
        };
        this.analyticsService.sendIncomeAndExpenditure(incomeAndExpenditure);
    }

    private setBusy(isBusy: boolean): void {
        this.isBusy = isBusy;
        this.cd.markForCheck();
    }

    onImageError(event) {
        event.target.src = '../../../images/categories/general.svg';
    }
}
