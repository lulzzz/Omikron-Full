import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map, switchMap } from 'rxjs/operators';
import { AlertNotificationService } from 'src/app/overlay/services/alert-notification.service';
import { UsersApiService } from 'src/app/users/users-api.service';

import {
    VaultDeleteVerificationPromptComponent,
} from '../vault-delete-verification-prompt/vault-delete-verification-prompt.component';
import { VaultApiService } from './../vault-api.service';
import { AccountDetailsViewModel, TransactionsViewModelContainer, CreditDebitIndicator } from './../vault.models';

@Component({
    selector: "app-vault-account-details",
    templateUrl: "./vault-account-details.component.html",
    styleUrls: ["./vault-account-details.component.scss"],
})
export class VaultAccountDetailsComponent implements OnInit {
    @Input() accountId;
    page: number = 1;
    showSeeMoreButton: boolean = true;
    CreditDebitIndicator = CreditDebitIndicator;

    accountDetails: AccountDetailsViewModel;
    accountTransactions: TransactionsViewModelContainer[] = [];

    constructor(
        private vaultApiService: VaultApiService,
        private usersApiService: UsersApiService,
        private alertNotificationService: AlertNotificationService,
        private modalService: NgbModal
    ) { }

    ngOnInit(): void {
        this.getAccountDetails(this.accountId);
        this.getAccountTransactions(this.accountId);

        this.initiateSearch();
    }

    private initiateSearch() {
        const searchTransactions = document.getElementById(
            "searchTransactions"
        ) as HTMLInputElement;

        const typeahead = fromEvent(searchTransactions, "input").pipe(
            map((e) => (e.target as HTMLInputElement).value),
            filter((text) => text.length > 2 || text.length === 0),
            debounceTime(400),
            distinctUntilChanged(),
            switchMap((searchTerm) =>
                this.vaultApiService.getAccountTransactions(
                    this.accountId,
                    this.page,
                    searchTerm.toString()
                )
            )
        );

        typeahead.subscribe(
            (data) => (this.accountTransactions = data.records),
            (error) => console.error(error)
        );
    }

    getAccountTransactions(accountId: string, search = "") {
        this.vaultApiService
            .getAccountTransactions(accountId, this.page, search)
            .subscribe(
                (data) => {
                    if(data.records.length < 5){
                        this.showSeeMoreButton = false;
                    }
                    this.accountTransactions = this.accountTransactions.concat(data.records);
                },
                (error) => console.error(error)
            );
    }

    getMoreTransactions() {
        const searchTransactions = document.getElementById(
            "searchTransactions"
        ) as HTMLInputElement;

        this.page++;
        this.getAccountTransactions(this.accountId, searchTransactions.value);
    }

    getAccountDetails(accountId: string) {
        this.vaultApiService.getAccountDetails(accountId).subscribe(
            (data) => {
                this.accountDetails = data.records;
            },
            (error) => console.error(error)
        );
    }

    reAuthorise() {
        this.usersApiService
            .openBankingLogin(this.accountDetails.provider, "/vault/response")
            .subscribe(
                (data) => {
                    document.location.href = data.records.url;
                },
                (error) => {
                    console.error(error);
                    this.alertNotificationService.showInfo({
                        text: "Something went wrong. Please try again or contact us.",
                    });
                }
            );
    }

    verifyAccountDelete() {
        const modalRef = this.modalService.open(VaultDeleteVerificationPromptComponent);
        modalRef.componentInstance.provider = this.accountDetails.provider;
    }

    closeModal() {
        this.modalService.dismissAll();
    }
}
