import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { DashboardModule } from '../home/dashboard/dashboard.module';
import { HomeModule } from '../home/home.module';
import { MaterialCustomModule } from '../material-custom/material-custom.module';
import { SecurityModule } from '../security/security.module';
import { SharedModule } from '../shared/shared.module';
import { AnalyticsCategoriesComponent } from './analytics-categories/analytics-categories.component';
import { AnalyticsDateComponent } from './analytics-date/analytics-date.component';
import { AnalyticsFiltersComponent } from './analytics-filters/analytics-filters.component';
import {
    AnalyticsIncomeExpenditureChartComponent,
} from './analytics-income-expenditure-chart/analytics-income-expenditure-chart.component';
import { AnalyticsNetPositionChartComponent } from './analytics-net-position-chart/analytics-net-position-chart.component';
import { AnalyticsPieChartComponent } from './analytics-pie-chart/analytics-pie-chart.component';
import { AnalyticsRoutingModule } from './analytics-routing.module';
import { AnalyticsTopMerchantsComponent } from './analytics-top-merchants/analytics-top-merchants.component';
import { AnalyticsComponent } from './analytics.component';

@NgModule({
    declarations: [
        AnalyticsComponent,
        AnalyticsTopMerchantsComponent,
        AnalyticsNetPositionChartComponent,
        AnalyticsPieChartComponent,
        AnalyticsIncomeExpenditureChartComponent,
        AnalyticsCategoriesComponent,
        AnalyticsDateComponent,
        AnalyticsFiltersComponent
    ],
    imports: [
        AnalyticsRoutingModule,
        CommonModule,
        HomeModule,
        DashboardModule,
        FormsModule,
        SecurityModule,
        SharedModule,
        MaterialCustomModule,
    ],
})
export class AnalyticsModule { }
