import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { Subscription } from 'rxjs';

import { AnalyticsService } from '../analytics.service';

@Component({
    selector: 'app-analytics-pie-chart',
    templateUrl: './analytics-pie-chart.component.html',
    styleUrls: ['./analytics-pie-chart.component.scss']
})
export class AnalyticsPieChartComponent implements OnInit {
    date: string;
    subscriptions: Subscription[] = [];
    chart: Chart;

    categoryColors: { [key: string]: string } = {
        General: "#70C798",
        Bills: "#6BD5E1",
        Shopping: "#958AF8",
        Finances: "#32B67A",
        PersonalFinances: "#8ECD78",
        Groceries: "#FFC14A",
        EatingOut: "#FF7CA3",
        Travel: "#A2DDFF",
        PersonalCare: "#EC60C5",
        Transport: "#4B8DDA",
        Entertainment: "#B76BF2",
        Expenses: "#B76BF2",
        Holidays: "#A2DDFF",
        Internal: "#FF964A"
    }

    constructor(private analyticsService: AnalyticsService) { }

    ngOnInit(): void {
        this.createPieChart();
        this.initiateDate();
        this.initiatePieChartData();
    }

    private initiatePieChartData() {
        this.subscriptions.push(this.analyticsService.pieChartData$.subscribe(
            data => {
                let colors = data.map(c => this.categoryColors[c.category.replace(" ", "")])

                this.chart.data.labels = data.map(c => c.category);
                this.chart.data.datasets = [{
                    data: data.map(c => c.amount),
                    backgroundColor: colors
                }]

                this.chart.update();
            }
        ));
    }

    private initiateDate() {
        this.subscriptions.push(this.analyticsService.dateChanged$.subscribe(
            data => {
                this.date = data;
            }
        ));
    }

    createPieChart(): void {
        const canvas = <HTMLCanvasElement>(
            document.getElementById("pieChartCanvas")
        );
        const ctx = canvas.getContext("2d");

        this.chart = new Chart(ctx, {
            type: "pie",
            data: {
                labels: [],
                datasets: [
                    {
                        data: [],
                        backgroundColor: [],
                    },
                ],
            },
            options: {
                plugins: {
                    legend: {
                        display: false,
                    },
                    tooltip: {
                        backgroundColor: "#fff",
                        borderColor: "#ECECEC",
                        borderWidth: 1,
                        bodyColor: "#242424",
                        bodyFont: {
                            size: 14,
                        },
                        caretPadding: 20,
                        caretSize: 0,
                        enabled: true,
                        mode: "index",
                        padding: 22,
                        cornerRadius: 8,
                        yAlign: "top",
                        usePointStyle: true,
                        bodySpacing: 5,
                        callbacks: {
                            label: function (context) {
                                var label: string;
                                if (context.parsed !== null) {
                                    label = new Intl.NumberFormat("en-GB", {
                                        style: "currency",
                                        currency: "GBP",
                                        minimumFractionDigits: 2,
                                        maximumFractionDigits: 2
                                    }).format(context.parsed);
                                }

                                return [context.label, label];
                            },
                        },
                    },
                },
            },
        });
    }

    ngOnDestroy() {
        this.subscriptions.forEach(s => s.unsubscribe());
    }
}
