import { Component, OnInit } from '@angular/core';

import { LayoutService } from '../layout/autentificated/layout/layout.service';
import { UserBase } from '../shared/models/shared.models';

@Component({
    selector: "app-profile",
    templateUrl: "./profile-and-settings.component.html",
    styleUrls: ["./profile-and-settings.component.scss"],
})
export class ProfileAndSettingsComponent implements OnInit {
    header: string = "Profile & Settings";
    paragraph: string = "Change your account details here.";
    user: UserBase;

    constructor(private layoutService: LayoutService) {}

    ngOnInit(): void {
        this.sendHeaderContent();
    }

    private sendHeaderContent() {
        this.layoutService.sendHeaderContent({
            header: this.header,
            paragraph: this.paragraph,
            vaultHeader: false,
        });
    }
}
