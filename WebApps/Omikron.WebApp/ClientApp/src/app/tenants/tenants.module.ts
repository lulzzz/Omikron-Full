import { NgModule } from "@angular/core";
import { SharedModule } from "../shared/shared.module";
import { TenantsRoutingModule } from "./tenants-routing.module";
import { TenantsApiService } from "./tenants.service";

import { LayoutModule } from '../layout/layout.module';

@NgModule({
    providers: [TenantsApiService],
    imports: [TenantsRoutingModule, SharedModule, LayoutModule]
})
export class TenantsModule {}
