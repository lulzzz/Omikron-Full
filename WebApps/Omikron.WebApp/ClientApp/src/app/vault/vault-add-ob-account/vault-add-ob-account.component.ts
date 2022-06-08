import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: "app-vault-add-ob-account",
    templateUrl: "./vault-add-ob-account.component.html",
    styleUrls: ["./vault-add-ob-account.component.scss"],
})
export class VaultAddObAccountComponent implements OnInit {
    redirectUrl: string = "/vault/response";
    navigateUrl : string = "/vault";
    constructor(private modalService: NgbModal) {}

    ngOnInit() {}

    hideModal() {
        this.modalService.dismissAll();
    }
}
