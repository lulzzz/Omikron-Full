import { Component, OnInit } from '@angular/core';
import { Chart, ChartDataset } from 'chart.js';
import { Subscription } from 'rxjs';

import { UsersApiService } from '../../users/users-api.service';
import { VaultApiService } from '../../vault/vault-api.service';
import { Filters, NetPositionsChartData, UserRegistrationDateViewModel } from '../analytics.model';
import { AuthService } from './../../shared/auth.service';
import { TimePeriod } from './../analytics.model';
import { AnalyticsService } from './../analytics.service';

@Component({
    selector: "app-analytics-net-position-chart",
    templateUrl: "./analytics-net-position-chart.component.html",
    styleUrls: ["./analytics-net-position-chart.component.scss"],
})
export class AnalyticsNetPositionChartComponent implements OnInit {
    chart: Chart;
    subscriptions: Subscription[] = [];
    months: string[] = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
    ];
    startDate: UserRegistrationDateViewModel;
    chartData: NetPositionsChartData[];
    monthMode: boolean = true;
    year: number = new Date().getFullYear();

    filters: Filters = {
        assetLiabilityTypes: [],
        vaultEntries: [],
        categories: [],
        archived: true
    }

    constructor(
        private vaultApiService: VaultApiService,
        private authService: AuthService,
        private analyticsService: AnalyticsService,
        private usersApiService: UsersApiService
    ) { }

    ngOnInit(): void {
        this.getStartDate();
        this.createNetPositionsChart();
        this.updateChart();
        this.changeTimePeriod();
        this.changeYearInMonthMode();
    }

    private getStartDate() {
        this.usersApiService.getGetUserRegistrationDate().subscribe((data) => {
            this.startDate = data.records;
        });
    }

    private changeYearInMonthMode() {
        this.subscriptions.push(
            this.analyticsService.yearChanged$.subscribe((data) => {
                this.reDrawTheChart();
                this.year = data;
                this.populateChart();
            })
        );
    }

    private changeTimePeriod() {
        this.subscriptions.push(
            this.analyticsService.selectedTimePeriod$.subscribe((data) => {
                if (data === TimePeriod.Month) {
                    this.monthMode = true;
                    this.year = new Date().getFullYear();
                    this.reDrawTheChart();
                    this.populateChart();
                } else {
                    this.monthMode = false;
                    this.reDrawTheChart();
                    this.populateChart();
                }
            })
        );
    }

    private reDrawTheChart() {
        if (this.chart) {
            this.chart.destroy();
        }
        this.createNetPositionsChart();
    }

    private updateChart() {
        this.subscriptions.push(this.analyticsService.filters$.subscribe(
            (data) => {
                this.filters = data;
                this.subscriptions.push(
                    this.vaultApiService
                        .getNetPositionsChartData(
                            this.authService.getUserProfile(),
                            this.monthMode,
                            this.year,
                            this.filters.assetLiabilityTypes,
                            this.filters.vaultEntries,
                            this.filters.archived
                        )
                        .subscribe((apiData) => {
                            this.chart.data.datasets = this.createChartDatasets(
                                apiData.records
                            );
                            this.chart.update();
                        })
                );
            })
        );
    }

    private populateChart() {
        this.subscriptions.push(this.vaultApiService.getNetPositionsChartData(
            this.authService.getUserProfile(),
            this.monthMode,
            this.year,
            this.filters.assetLiabilityTypes,
            this.filters.vaultEntries,
            this.filters.archived)
            .subscribe(
                (data) => {
                    this.chart.data.labels = this.monthMode
                        ? data.records.map((x) => this.months[x.monthIndex - 1])
                        : data.records.map((x) => x.monthIndex);
                    this.chart.data.datasets = this.createChartDatasets(
                        data.records
                    );

                    this.chart.update();
                })
        );
    }

    createNetPositionsChart(): void {
        const canvas = <HTMLCanvasElement>(
            document.getElementById("mainBarChartCanvas")
        );
        const ctx = canvas.getContext("2d");

        const journeyMessage: any = this.createJourneyMessage();

        this.chart = new Chart(ctx, {
            type: "bar",
            data: {
                labels: this.months,
                datasets: [
                    {
                        label: "Net Position",
                        data: [],
                        borderColor: "#958AF8",
                        backgroundColor: "#958AF8",
                        stack: "combined",
                        type: "line",
                        tension: 0.4,
                        pointRadius: 0,
                    },
                    {
                        label: "Assets",
                        data: [],
                        backgroundColor: "#A2DDFF",
                        stack: "stack 0",
                        type: "bar",
                        borderRadius: 8,
                    },
                    {
                        label: "Liabilities",
                        data: [],
                        backgroundColor: "#FF9489",
                        stack: "stack 1",
                        type: "bar",
                        borderRadius: 8,
                    },
                ],
            },
            plugins: [journeyMessage],
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: "top",
                        align: "end",
                        labels: {
                            color: "#242424",
                        },
                    },
                    tooltip: {
                        titleAlign: "center",
                        backgroundColor: "#fff",
                        borderColor: "#ECECEC",
                        borderWidth: 1,
                        bodyColor: "#242424",
                        bodyFont: {
                            size: 14,
                        },
                        titleFont: {
                            size: 14,
                        },
                        titleColor: "#BBB",
                        titleMarginBottom: 15,
                        caretPadding: 20,
                        caretSize: 0,
                        enabled: true,
                        intersect: false,
                        mode: "index",
                        padding: 16,
                        cornerRadius: 8,
                        yAlign: "bottom",
                        usePointStyle: true,
                        bodySpacing: 5,
                        callbacks: {
                            label: function (context) {
                                var label: string;
                                if (context.parsed.y !== null) {
                                    label = new Intl.NumberFormat("en-GB", {
                                        style: "currency",
                                        currency: "GBP",
                                        minimumFractionDigits: 0,
                                    }).format(context.parsed.y);
                                }

                                return [context.dataset.label, label, ""];
                            },
                        },
                    },
                },
                scales: {
                    xAxis: {
                        stacked: true,
                        beginAtZero: true,
                        grid: {
                            color: "#E4E4E7",
                            display: false,
                        },
                        ticks: {
                            color: "#BBB",
                        },
                    },
                    yAxis: {
                        stacked: true,
                        position: "left",
                        beginAtZero: true,
                        grid: {
                            color: "#E4E4E7",
                            borderDash: [25],
                            drawBorder: false,
                        },
                        ticks: {
                            color: "#BBB",
                            callback: function (value) {
                                return value.toLocaleString("en-GB", {
                                    style: "currency",
                                    currency: "GBP",
                                    minimumFractionDigits: 0,
                                });
                            },
                        },
                    },
                },
            },
        });
    }

    private createJourneyMessage(): any {
        return {
            id: "journeyMessage",
            afterDraw: (chart: any) => {
                if (this.monthMode) {
                    if (this.year === this.startDate?.year ?? 0) {
                        const meta = chart.getDatasetMeta(0);
                        var xAxis = 0;
                        if (meta.data[0] != undefined) {
                            xAxis = meta.data[this.startDate.month - 1].x;
                        }
                        var journeyImage = document.getElementById("journeyImage");
                        if (journeyImage != undefined) {
                            chart.ctx.drawImage(journeyImage, xAxis - 82, chart.chartArea.bottom - 135);
                        }
                    }
                }
            },
        };
    }

    private createChartDatasets(data: NetPositionsChartData[]): ChartDataset[] {
        return [
            {
                label: "Net Position",
                data: data.map((x) => x.data.net),
                borderColor: "#958AF8",
                backgroundColor: "#958AF8",
                stack: "combined",
                type: "line",
                tension: 0.4,
                pointRadius: 0,
            },
            {
                label: "Assets",
                data: data.map((x) => x.data.assets),
                backgroundColor: "#A2DDFF",
                stack: "stack 0",
                type: "bar",
                borderRadius: 8,
            },
            {
                label: "Liabilities",
                data: data.map((x) => x.data.liabilities == 0 ? x.data.liabilities : -x.data.liabilities),
                backgroundColor: "#FF9489",
                stack: "stack 1",
                type: "bar",
                borderRadius: 8,
            },
        ];
    }

    ngOnDestroy() {
        this.subscriptions.forEach((s) => s.unsubscribe());
    }
}
