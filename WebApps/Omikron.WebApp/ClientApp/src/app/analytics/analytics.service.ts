import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { InitialIncomeAndExpenditure, MinDateData, PieChartData, Filters } from './analytics.model';

@Injectable({
    providedIn: 'root'
})
export class AnalyticsService {
    private dateChangedSource = new Subject<string>();
    private yearChangedSource = new Subject<number>();
    private categoriesSource = new Subject<string[]>();
    private incomeAndExpenditureSource = new Subject<InitialIncomeAndExpenditure>();
    private pieChartDataSource = new Subject<PieChartData[]>();
    private selectedTimePeriodChangedSource = new Subject<string>();
    private minDateSource = new Subject<MinDateData>();
    private filtersSource = new Subject<Filters>();

    dateChanged$ = this.dateChangedSource.asObservable();
    yearChanged$ = this.yearChangedSource.asObservable();
    categories$ = this.categoriesSource.asObservable();
    incomeAndExpenditure$ = this.incomeAndExpenditureSource.asObservable();
    pieChartData$ = this.pieChartDataSource.asObservable();
    selectedTimePeriod$ = this.selectedTimePeriodChangedSource.asObservable();
    minDate$ = this.minDateSource.asObservable();
    filters$ = this.filtersSource.asObservable();

    sendFilters(filters: Filters){
        this.filtersSource.next(filters);
    }

    sendDate(date: string) {
        this.dateChangedSource.next(date);
    }

    sendYear(year: number) {
        this.yearChangedSource.next(year);
    }

    sendCategories(categories: string[]) {
        this.categoriesSource.next(categories);
    }

    sendIncomeAndExpenditure(incomeAndExpenditure: InitialIncomeAndExpenditure) {
        this.incomeAndExpenditureSource.next(incomeAndExpenditure);
    }

    sendPieChartData(data: PieChartData[]) {
        this.pieChartDataSource.next(data);
    }

    changeSelectedTimePeriod(timePeriod: string) {
        this.selectedTimePeriodChangedSource.next(timePeriod);
    }

    sendMinDate(minDate: MinDateData){
        this.minDateSource.next(minDate);
    }
}
