import { MaintenanceStatus } from './../../models/ObProvider';
import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { UsersApiService } from '../../../users/users-api.service';
import { ObProvider } from '../../models/ObProvider';
import { AddAccountManuallyComponent } from '../add-account-manually/add-account-manually.component';

@Component({
    selector: 'app-providers-list',
    templateUrl: './providers-list.component.html',
    styleUrls: ['./providers-list.component.scss']
})
export class ProvidersListComponent implements OnInit {
    constructor(
        private usersApiService: UsersApiService,
        private cd: ChangeDetectorRef,
        private modalService: NgbModal) { }

    maintenanceStatus = MaintenanceStatus;
    @Input() darkTheme: boolean;
    isResponse: boolean;
    isBusy: boolean;
    keys: string[] = [];
    obProviders: ObProvider[] = [];
    records: any;
    searchFilter: string;
    providers;
    timeOut;
    timeOutDuration = 500;
    @Input() redirectUrl;
    @Input() navigateUrl;
    ngOnInit() {
        this.setBusy(true);
        this.PopulateProviders();
    }

    openBankingLogin(bankProvider: string): void {
        this.usersApiService
            .openBankingLogin(bankProvider, this.redirectUrl)
            .subscribe((data) => {
                document.location.href = data.records.url;
            });
    }

    PopulateProviders() {
        this.usersApiService.getListOfObProviders().subscribe((data) => {
            this.providers = data.records;
            this.setBusy(false);
        });
    }

    SearchProviders() {
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
            this.usersApiService
                .getListOfObProviders(this.searchFilter)
                .subscribe((data) => {
                    this.providers = data.records;
                });
        }, this.timeOutDuration);
    }

    addAccountManually() {
        const modalRef = this.modalService.open(AddAccountManuallyComponent);
        modalRef.componentInstance.darkTheme = this.darkTheme;
        modalRef.componentInstance.navigateUrl = this.navigateUrl;
    }

    private setBusy(isBusy: boolean): void {
        this.isBusy = isBusy;
        this.cd.markForCheck();
    }
}
