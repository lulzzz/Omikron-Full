import { NgModule } from '@angular/core';

import { LayoutModule } from '../../layout/layout.module';
import { SharedModule } from '../../shared/shared.module';
import { AssetsAndLiabilitiesComponent } from './assets-and-liabilities/assets-and-liabilities.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { DiagramsComponent } from './diagrams/diagrams.component';
import { UpgradePanelComponent } from './upgrade-panel/upgrade-panel.component';

@NgModule({
    imports: [
        DashboardRoutingModule,
        SharedModule,
        LayoutModule,
    ],
    declarations: [
        DashboardComponent,
        AssetsAndLiabilitiesComponent,
        DiagramsComponent,
        UpgradePanelComponent
    ],
    providers: [
    ],
    exports: [
    ]
})
export class DashboardModule { }
