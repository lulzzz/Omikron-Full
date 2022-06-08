import { LayoutService } from './layout.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: "app-layout",
    templateUrl: "./layout.component.html",
    styleUrls: ["./layout.component.scss"],
})
export class LayoutComponent implements OnInit {
    navigationClosed: boolean;

    constructor(private layoutService: LayoutService) { }

    ngOnInit(): void {
    }

    changeNavigation(value) {
        this.navigationClosed = value;
    }

    changeHeaderBackground() {
        let v = document.getElementById("mainContent").scrollTop;
        if (v > 20) {
            this.layoutService.changeBackground(false);
        }
        else {
            this.layoutService.changeBackground(true);
        }
    }
}
