import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../layout.service';
import { VaultAddAccountComponent } from 'src/app/vault/vault-add-account/vault-add-account.component';

@Component({
    selector: 'app-sticky-header',
    templateUrl: './sticky-header.component.html',
    styleUrls: ['./sticky-header.component.scss']
})
export class StickyHeaderComponent implements OnInit {
    subscriptions: Subscription[] = [];
    transparent: boolean = true;

    header: string = "";
    paragraph: string = "";

    vaultHeader: boolean = false;
    vaultBalance: number = 0;

    constructor(private layoutService: LayoutService, private modalService: NgbModal) { }

    ngOnInit(): void {
        this.changeBackground();

        this.subscriptions.push(this.layoutService.headerContent$.subscribe(
            data => {
                this.header = data.header;
                this.paragraph = data.paragraph;

                if(data.vaultHeader){
                    this.vaultHeader = true;
                    this.vaultBalance = data.vaultBalance;
                }
            }
        ))
    }

    private changeBackground() {
        this.subscriptions.push(this.layoutService.backgroundChanged$.subscribe(
            transparent => {
                this.transparent = transparent;
            }
        ));
    }

    addToVaultModal() {
        this.modalService.open(VaultAddAccountComponent);
    }
}
