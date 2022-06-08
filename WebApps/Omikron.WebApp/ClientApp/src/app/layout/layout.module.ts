import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { SharedModule } from "../shared/shared.module";
import { HeaderComponent } from "./header.component";
import { FooterComponent } from "./footer.component";
import { TopBarComponent } from "../navigation/top-bar.component";
import { LayoutComponent } from './autentificated/layout/layout.component';
import { SidebarComponent } from './autentificated/sidebar/sidebar.component';
import { NotificationComponent } from './autentificated/notification/notification.component';
import { StickyHeaderComponent } from './autentificated/layout/sticky-header/sticky-header.component';

@NgModule({
    imports: [RouterModule, SharedModule],
    declarations: [
        HeaderComponent,
        FooterComponent,
        TopBarComponent,
        LayoutComponent,
        SidebarComponent,
        NotificationComponent,
        StickyHeaderComponent
    ],
    exports: [HeaderComponent, FooterComponent, LayoutComponent, NotificationComponent]
})
export class LayoutModule {}
