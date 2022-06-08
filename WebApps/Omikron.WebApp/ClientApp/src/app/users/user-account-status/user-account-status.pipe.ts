import { Pipe, PipeTransform } from "@angular/core";
import { AccountStatus } from 'src/app/shared';

@Pipe({
    name: "userAccountStatus"
})
export class UserAccountStatusPipe implements PipeTransform {
    transform(status: AccountStatus, args?: any): string {
        switch (status.toString()) {
            case AccountStatus.Active:
                return "Active";
            case AccountStatus.OnBoarding:
                return "On-Boarding";
            case AccountStatus.New:
                return "New";
            case AccountStatus.Disabled:
                return "Disabled";
            default:
                return "N/A";
        }
    }
}