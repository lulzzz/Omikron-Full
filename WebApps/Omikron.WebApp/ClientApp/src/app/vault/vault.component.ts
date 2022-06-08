import { Component, OnInit } from '@angular/core';

import { LayoutService } from './../layout/autentificated/layout/layout.service';

@Component({
    selector: "app-vault",
    templateUrl: "./vault.component.html",
    styleUrls: ["./vault.component.scss"],
})
export class VaultComponent implements OnInit {
    header: string = "OMIKRON Vault";
    paragraph: string = "Vault Balance: ";
    constructor(private layoutService: LayoutService) {}

    ngOnInit(): void {}

    sendHeaderData(totalBalance: number) {
        this.layoutService.sendHeaderContent({
            header: this.header,
            paragraph: this.paragraph,
            vaultHeader: true,
            vaultBalance: totalBalance,
        });
    }
}
