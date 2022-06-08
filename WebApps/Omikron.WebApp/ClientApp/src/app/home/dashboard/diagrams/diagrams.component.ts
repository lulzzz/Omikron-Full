import { Component, Input, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import annotationPlugin from 'chartjs-plugin-annotation';

import { DashboardChartData, UserRegistrationDateViewModel } from '../../../analytics/analytics.model';
import { AuthService } from '../../../shared/auth.service';
import { UsersApiService } from '../../../users/users-api.service';
import { VaultApiService } from '../../../vault/vault-api.service';
import { Colors, numberOfMonths } from '../dashboard.models';
import { TotalSummary } from './../dashboard.models';

@Component({
    selector: "app-diagrams",
    templateUrl: "./diagrams.component.html",
    styleUrls: ["./diagrams.component.scss"],
})
export class DiagramsComponent implements OnInit {
    @Input() totalSummary: TotalSummary;

    context: CanvasRenderingContext2D;
    chart: Chart;
    chartData: DashboardChartData[];
    allTimeFilter: boolean = false;
    numberOfMonths = numberOfMonths;
    dateSpan: number = numberOfMonths.one;
    startDate: UserRegistrationDateViewModel;
    dateInterval: string;
    assetsActive = true;
    liabilitiesActive = false;
    netActive = false;
    constructor(
        private vaultApiService: VaultApiService,
        private authService: AuthService,
        private usersApiService: UsersApiService
    ) {}

    ngOnInit(): void {
        this.usersApiService
            .getGetUserRegistrationDate()
            .subscribe((data) => (this.startDate = data.records));

        Chart.register(annotationPlugin);
        this.getChartData(this.allTimeFilter, this.dateSpan);
    }

    getChartData(timeFilter: boolean, dateSpan: number): void {
        this.vaultApiService
            .getDashboardChartData(
                this.authService.getUserProfile(),
                timeFilter,
                dateSpan
            )
            .subscribe((data) => {
                this.dateInterval = this.getDateInterval(timeFilter, dateSpan);
                this.allTimeFilter = timeFilter;
                this.dateSpan = dateSpan;
                this.chartData = data.records;

                this.assetsActive
                    ? this.makeAssetsActive()
                    : this.netActive
                    ? this.makeAssetsActive()
                    : this.makeLiabilitiesActive();
            });
    }

    getDateInterval(timeFilter: boolean, dateSpan: number): string {
        var endDate = new Date();
        var startDate = new Date();

        timeFilter
            ? startDate.setFullYear(
                  this.startDate.year,
                  this.startDate.month - 1,
                  this.startDate.day
              )
            : startDate.setMonth(startDate.getMonth() - dateSpan);

        return (
            startDate.toLocaleDateString(undefined, {
                year: "numeric",
                month: "short",
                day: "2-digit",
            }) +
            " - " +
            endDate.toLocaleDateString(undefined, {
                year: "numeric",
                month: "short",
                day: "2-digit",
            })
        );
    }

    createChartWithOptions(
        rgbaColor: string,
        borderColor: string,
        dataLabels: any,
        dataValues: any
    ): void {
        if (this.chart) {
            this.chart.destroy();
        }

        const canvas = <HTMLCanvasElement>(
            document.getElementById("chartCanvas")
        );
        const ctx = canvas.getContext("2d");

        const tooltipLine: any = {
            id: "tooltipLine",
            beforeDraw: (chart) => {
                if (chart.tooltip._active && chart.tooltip._active.length) {
                    const ctx = chart.ctx;
                    ctx.save();
                    const activePoint = chart.tooltip._active[0];

                    ctx.beginPath();
                    ctx.setLineDash([25, 15]);
                    ctx.moveTo(activePoint.element.x, chart.chartArea.top);
                    ctx.lineTo(activePoint.element.x, activePoint.element.y);
                    ctx.lineWidth = 1;
                    ctx.strokeStyle = " #E4E4E7";
                    ctx.stroke();
                    ctx.restore();
                }
            },
        };

        const journeyMessage: any = {
            id: "journeyMessage",
            afterDatasetsDraw: (chart: any) => {
                var startDate = this.startDate
                    ? `${this.startDate.day.toLocaleString(undefined, {
                        minimumIntegerDigits: 2,
                    })}. ${this.startDate.month.toLocaleString(undefined, {
                        minimumIntegerDigits: 2,
                    })}. ${this.startDate.year}.`
                    : "";
                var yearModeDate = this.startDate
                    ? `${this.startDate.month.toLocaleString(undefined, {
                          minimumIntegerDigits: 2,
                      })}. ${this.startDate.year}.`
                    : "";
                if (
                    startDate.match(this.chartData[0].dateIndex.toString()) ||
                    yearModeDate.match(this.chartData[0].dateIndex.toString())
                ) {
                    const meta = chart.getDatasetMeta(0);
                    var xAxis = 0;
                    var yAxis = 0;
                    if (meta.data[0] != undefined) {
                        xAxis = meta.data[0].x;
                        yAxis = meta.data[0].y;
                    }

                    var journeyImage =
                        +yAxis.toPrecision(2) < 100
                            ? document.getElementById("journeyImageTop")
                            : document.getElementById("journeyImage");

                    if (journeyImage != undefined) {
                        chart.ctx.drawImage(
                            journeyImage,
                            xAxis - 82,
                            journeyImage.id == "journeyImageTop"
                                ? yAxis
                                : yAxis - 135
                        );
                    }
                }
            },
        };

        const color_gradient = ctx.createLinearGradient(0, 0, 0, 500);
        color_gradient.addColorStop(0, "rgba(" + rgbaColor + ", 0.5");
        color_gradient.addColorStop(1, "rgba(" + rgbaColor + ", 0.048");
        this.chart = new Chart(ctx, {
            type: "line",
            data: {
                labels: dataLabels,
                datasets: [
                    {
                        label: "",
                        data: dataValues,
                        backgroundColor: color_gradient,
                        hoverBorderWidth: 2,
                        hoverBorderColor: "#68C8FF",
                        borderWidth: 2,
                        borderColor: borderColor,
                        fill: {
                            target: "origin",
                            above: color_gradient,
                        },
                        tension: 0.4,
                    },
                ],
            },
            plugins: [tooltipLine, journeyMessage],
            options: {
                layout: {
                    padding: {
                        left: 55,
                    },
                },
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false,
                    },
                    tooltip: {
                        titleAlign: "center",
                        bodyAlign: "center",
                        backgroundColor: "#fff",
                        bodyColor: "#242424",
                        bodyFont: {
                            size: 14,
                        },
                        titleFont: {
                            size: 14,
                        },
                        titleColor: "#BBB",
                        caretPadding: 20,
                        caretSize: 0,
                        displayColors: false,
                        enabled: true,
                        intersect: false,
                        mode: "x",
                        padding: 16,
                        cornerRadius: 8,
                        yAlign: "top",
                        callbacks: {
                            label: function (context) {
                                var label = context.dataset.label || "";

                                if (context.parsed.y !== null) {
                                    label += new Intl.NumberFormat("en-GB", {
                                        style: "currency",
                                        currency: "GBP",
                                        minimumFractionDigits: 2,
                                        maximumFractionDigits: 2
                                    }).format(context.parsed.y);
                                }
                                return label;
                            },
                        },
                    },
                },
                scales: {
                    xAxis: {
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
                        beginAtZero: true,
                        position: "right",
                        grid: {
                            color: "#E4E4E7",
                            borderDash: [25],
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

    makeAssetsActive() {
        this.changeActiveChart(true, false, false);
        this.createChartWithOptions(
            Colors.assets,
            Colors.assetsLine,
            this.chartData.map((x) => x.dateIndex),
            this.chartData.map((x) => x.data.assets)
        );

        let assetsButton = document.getElementById("assetsButton");
        let liabilitiesButton = document.getElementById("liabilitiesButton");
        let netButton = document.getElementById("netButton");

        if (!assetsButton.classList.contains("assets-active")) {
            assetsButton.classList.add("assets-active");
        }

        liabilitiesButton.classList.remove("liabilities-active");
        netButton.classList.remove("net-active");
    }

    makeLiabilitiesActive() {
        this.changeActiveChart(false, true, false);
        this.createChartWithOptions(
            Colors.liabilities,
            Colors.liabilitiesLine,
            this.chartData.map((x) => x.dateIndex),
            this.chartData.map((x) => x.data.liabilities)
        );

        let assetsButton = document.getElementById("assetsButton");
        let liabilitiesButton = document.getElementById("liabilitiesButton");
        let netButton = document.getElementById("netButton");

        if (!liabilitiesButton.classList.contains("liabilities-active")) {
            liabilitiesButton.classList.add("liabilities-active");
        }

        assetsButton.classList.remove("assets-active");
        netButton.classList.remove("net-active");
    }

    makeNetActive() {
        this.changeActiveChart(false, false, true);
        this.createChartWithOptions(
            Colors.net,
            Colors.netLine,
            this.chartData.map((x) => x.dateIndex),
            this.chartData.map((x) => x.data.net)
        );

        let assetsButton = document.getElementById("assetsButton");
        let liabilitiesButton = document.getElementById("liabilitiesButton");
        let netButton = document.getElementById("netButton");

        if (!netButton.classList.contains("net-active")) {
            netButton.classList.add("net-active");
        }

        liabilitiesButton.classList.remove("liabilities-active");
        assetsButton.classList.remove("assets-active");
    }

    changeActiveChart(assets: boolean, liabilities: boolean, net: boolean) {
        this.assetsActive = assets;
        this.liabilitiesActive = liabilities;
        this.netActive = net;
    }
}
