import { Component, OnInit } from '@angular/core';

import { LayoutService } from '../layout/autentificated/layout/layout.service';
import { UserBase } from '../shared/models/shared.models';
import { AuthService } from './../shared/auth.service';

@Component({
    selector: "app-analytics",
    templateUrl: "./analytics.component.html",
})
export class AnalyticsComponent implements OnInit {
    paragraph: string = "Let’s analyze what’s in your Vault";
    user: UserBase;
    constructor(
        private layoutService: LayoutService,
        private authService: AuthService
    ) {}

    ngOnInit(): void {
        this.sendHeaderContent();
    }

    private sendHeaderContent() {
        this.layoutService.sendHeaderContent({
            header: `Welcome ${this.authService.getUserProfile().firstName},`,
            paragraph: this.paragraph,
            vaultHeader: false,
        });
    }
}
