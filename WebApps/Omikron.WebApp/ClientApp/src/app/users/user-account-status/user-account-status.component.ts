import { Component, OnInit, Input } from "@angular/core";
import { UserBase, AccountStatus } from "../../shared";

@Component({
    selector: "user-account-status",
    template: "<span class='badge font-size-15' [class.badge-success]='user.accountStatus == accountStatus.Active' [class.badge-danger]='user.accountStatus == accountStatus.Disabled' [class.badge-warning]='user.accountStatus == accountStatus.OnBoarding' [class.badge-primary]='user.accountStatus == accountStatus.New'><span class='circle'></span> {{user.accountStatus | userAccountStatus}}<span>"
})
export class UserAccountStatusComponent {
    @Input() user: UserBase;
    accountStatus = AccountStatus;
}