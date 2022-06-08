import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { VaultApiService } from '../../vault/vault-api.service';
import { VaultEntry, VaultEntryListViewModel } from '../analytics.model';
import { AuthService } from './../../shared/auth.service';
import { Filters, TimePeriod } from './../analytics.model';
import { AnalyticsService } from './../analytics.service';

@Component({
    selector: 'app-analytics-filters',
    templateUrl: './analytics-filters.component.html',
    styleUrls: ['./analytics-filters.component.scss']
})
export class AnalyticsFiltersComponent implements OnInit {
    vaultEntryListViewModel: VaultEntryListViewModel[]
    assetLiabilityTypes: string[] = [
        "Current Accounts",
        "Savings",
        "Credit Cards",
        "Loans",
        "Pensions",
        "Investments",
        "Properties",
        "Vehicles",
        "Personal Items",
    ]

    timePeriodEnum = TimePeriod;
    timePeriod: TimePeriod = TimePeriod.Month;
    currentYear: number = new Date().getFullYear();

    archived: boolean = true;

    assetLiabilityTypeFiltersValue: string = "All";
    vaultEntryFiltersValue: string = "All";
    incomeExpenditureFiltersValue: string = "All";

    assetLiabilityTypeFiltersId: string = "assetLiabilityTypeFilters";
    vaultEntryFiltersId: string = "vaultEntryFilters";
    incomeExpenditureFiltersId: string = "incomeExpenditureFilters";
    archivedAccountsId = 'archivedAccounts';

    categoriesList: string[] = [];

    showFilters: boolean = false;
    subscriptions: Subscription[] = [];

    assetLiabilityTypeFilters: SelectionModel<string> = new SelectionModel<string>(true, []);
    incomeExpenditureFilters: SelectionModel<string> = new SelectionModel<string>(true, []);
    vaultEntryFilters: SelectionModel<VaultEntry> = new SelectionModel<VaultEntry>(true, []);

    constructor(private analyticsService: AnalyticsService, private vaultApiService: VaultApiService, private authService: AuthService) { }

    ngOnInit(): void {
        this.getCategories();
        this.getVaultEntriesList();
        this.changeYear();
    }

    toggleFilter(idSelector: string, elementId: string) {
        let element = <HTMLInputElement>document.getElementById(elementId);
        element.checked = !element.checked;
        let parent = document.getElementById(idSelector);
        let selectAll = <HTMLInputElement>parent.querySelector("li[class=select-all-filter] input[type=checkbox]");
        selectAll.checked = false;

        if (idSelector === this.incomeExpenditureFiltersId) {
            this.incomeExpenditureFilters.toggle(elementId);
            this.setFilterTitleValue(idSelector, `${this.categoriesList.length - this.incomeExpenditureFilters.selected.length}/${this.categoriesList.length}`)
        }
        else if (idSelector === this.assetLiabilityTypeFiltersId) {
            this.assetLiabilityTypeFilters.toggle(elementId);
            this.setFilterTitleValue(idSelector, `${this.assetLiabilityTypes.length - this.assetLiabilityTypeFilters.selected.length}/${this.assetLiabilityTypes.length}`)
        }
        else if(elementId === this.archivedAccountsId)
        {
            this.archived = !this.archived;
        }
        else {
            let vaultEntries = this.vaultEntryListViewModel.flatMap(x => x.vaultEntries);
            let vaultEntry = vaultEntries.find(x => x.vaultEntryId === elementId);
            this.vaultEntryFilters.toggle(vaultEntry);
            this.setFilterTitleValue(idSelector, `${vaultEntries.length - this.vaultEntryFilters.selected.length}/${vaultEntries.length}`)
        }

        this.sendFilters(this.archived);
    }

    private sendFilters(archived: boolean) {
        let filters: Filters = {
            assetLiabilityTypes: this.assetLiabilityTypeFilters.selected,
            vaultEntries: this.vaultEntryFilters.selected.map(x => x.vaultEntryId),
            categories: this.incomeExpenditureFilters.selected,
            archived: archived
        };
        this.analyticsService.sendFilters(filters);
    }

    selectAllFilters(idSelector: string) {
        let element = document.getElementById(idSelector);
        let children = element.querySelectorAll("li");

        let checkedValue = this.toggleCheckboxes(element, children);

        if (idSelector === this.incomeExpenditureFiltersId) {
            this.incomeExpenditureFilters = new SelectionModel<string>(true, checkedValue ? [] : this.categoriesList);
            this.setFilterTitleValue(idSelector, checkedValue ? "All" : `0/${this.categoriesList.length}`);
        }

        else if (idSelector === this.vaultEntryFiltersId) {
            this.vaultEntryFilters = new SelectionModel<VaultEntry>(true, checkedValue ? [] : this.vaultEntryListViewModel.flatMap(x => x.vaultEntries))
            this.setFilterTitleValue(idSelector, checkedValue ? "All" : `0/${this.vaultEntryListViewModel.length}`);

        }
        else {
            this.assetLiabilityTypeFilters = new SelectionModel<string>(true, checkedValue ? [] : this.assetLiabilityTypes);
            this.setFilterTitleValue(idSelector, checkedValue ? "All" : `0/${this.assetLiabilityTypes.length}`);

        }

        this.sendFilters(true);
    }

    private toggleCheckboxes(element: HTMLElement, children: NodeListOf<HTMLLIElement>) {
        let selectAll = <HTMLInputElement>element.querySelector("li[class=select-all-filter] input[type=checkbox]");
        let checkedValue = !selectAll.checked;

        children.forEach(e => {
            let checkbox = <HTMLInputElement>e.querySelector("input[type=checkbox]");
            checkbox.checked = checkedValue;
        });
        return checkedValue;
    }

    setFilterTitleValue(idSelector: string, value: string) {
        if (idSelector === this.assetLiabilityTypeFiltersId) {
            this.assetLiabilityTypeFiltersValue = value;
            this.getVaultEntriesList();
        }
        else if (idSelector === this.vaultEntryFiltersId) {
            this.vaultEntryFiltersValue = value;
        }
        else {
            this.incomeExpenditureFiltersValue = value;
        }
    }

    toggleFilters() {
        this.showFilters = !this.showFilters;
    }

    setTimePeriod(period: TimePeriod) {
        this.timePeriod = period;
        this.analyticsService.changeSelectedTimePeriod(period);
    }

    ngOnDestroy() {
        this.subscriptions.forEach(s => s.unsubscribe);
    }

    private changeYear() {
        this.subscriptions.push(this.analyticsService.yearChanged$.subscribe(
            data => {
                this.currentYear = data;
            }
        ));
    }

    private getVaultEntriesList() {
        this.subscriptions.push(this.vaultApiService.getVaultEntryList(this.authService.getUserProfile(), this.assetLiabilityTypeFilters.selected).subscribe(
            data => {
                this.vaultEntryListViewModel = data.records;
            },
            error => {
                console.log(error);
            }
        ));
    }

    private getCategories() {
        this.subscriptions.push(this.analyticsService.categories$.subscribe(
            data => {
                this.categoriesList = [...new Set(this.categoriesList.concat(data))];

                if (this.incomeExpenditureFiltersValue !== "All") {
                    this.setFilterTitleValue(this.incomeExpenditureFiltersId, `${this.categoriesList.length - this.incomeExpenditureFilters.selected.length}/${this.categoriesList.length}`)
                }
            }
        ));
    }
}
