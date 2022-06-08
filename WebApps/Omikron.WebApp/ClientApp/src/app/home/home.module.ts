import { NgModule } from '@angular/core';

import { HomeComponent } from '.';
import { LayoutModule } from '../layout/layout.module';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
    imports: [
        HomeRoutingModule,
        SharedModule,
        LayoutModule,
    ],
    declarations: [
        HomeComponent,
    ],
    providers: [
    ],
    exports: [
    ]
})
export class HomeModule { }
