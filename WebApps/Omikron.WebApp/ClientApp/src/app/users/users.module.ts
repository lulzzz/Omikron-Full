import { NgModule } from '@angular/core';

import { LayoutModule } from '../layout/layout.module';
import { SharedModule } from '../shared/shared.module';
import { UserAccountStatusComponent } from './user-account-status/user-account-status.component';
import { UserAccountStatusPipe } from './user-account-status/user-account-status.pipe';
import { UsersApiService } from './users-api.service';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
    declarations: [
        UserAccountStatusComponent,
        UserAccountStatusPipe,
    ],
    imports: [UsersRoutingModule, SharedModule, LayoutModule],
    providers: [UsersApiService]
})
export class UsersModule { }
