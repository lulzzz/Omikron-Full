import { Subscription } from 'rxjs';
import { AnalyticsService } from './../analytics.service';
import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';

@Component({
    selector: 'app-analytics-income-expenditure-chart',
    templateUrl: './analytics-income-expenditure-chart.component.html',
    styleUrls: ['./analytics-income-expenditure-chart.component.scss']
})
export class AnalyticsIncomeExpenditureChartComponent implements OnInit {
    totalIncome: number;
    totalExpenditure: number;
    remainingBalance = 0;
    chart: Chart;

    date: string;
    subscriptions: Subscription[] = [];

    constructor(private analyticsService: AnalyticsService) { }

    ngOnInit(): void {
        this.createIncomeExpenditureChart();

        this.updateDate();
        this.updateChart();
    }

    private updateDate() {
        this.subscriptions.push(this.analyticsService.dateChanged$.subscribe(
            data => {
                this.date = data;
            }
        ));
    }

    private updateChart() {
        this.subscriptions.push(this.analyticsService.incomeAndExpenditure$.subscribe(
            data => {
                this.totalExpenditure = data.expenditure;
                this.totalIncome = data.income;
                this.remainingBalance = this.totalIncome + this.totalExpenditure;

                this.chart.data.datasets = [
                    {
                        data: [this.totalIncome, this.totalExpenditure * -1],
                        backgroundColor: ["#958AF8", "#6BD5E1"],
                        borderRadius: 8
                    }]

                this.chart.update();
            }
        ));
    }

    createIncomeExpenditureChart(): void {
        const canvas = <HTMLCanvasElement>(
            document.getElementById("secondBarChart")
        );
        const ctx = canvas.getContext("2d");

        this.chart = new Chart(ctx, {
            type: "bar",
            data: {
                labels: [`+${this.totalIncome}`, `-${this.totalExpenditure}`],
                datasets: [
                    {
                        data: [this.totalIncome, this.totalExpenditure],
                        backgroundColor: ["#958AF8", "#6BD5E1"],
                        borderRadius: 8,
                    },
                ],
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false,
                    },
                    tooltip: {
                        enabled: false,
                    },
                },
                scales: {
                    xAxis: {
                        display: false,
                    },
                    yAxis: {
                        display: false,
                    },
                },
            },
        });
    }

    ngOnDestroy() {
        this.subscriptions.forEach(s => s.unsubscribe);
    }
}
