import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name:'vaultManualAccountImage'})
export class VaultManualAccountImagePipe implements PipeTransform {

    public providerImageUrls: { [key: string]: string } = {
        Vehicles: '../../../images/Vault/vehicle.svg',
        Vehicle: '../../../images/Vault/vehicle.svg',
        Property: '../../../images/Vault/property.svg',
        Properties: '../../../images/Vault/property.svg',
        Investment: '../../../images/Vault/investment.svg',
        PersonalItem: '../../../images/Vault/personal_item.svg',
        Account: '../../../images/Vault/current_account.svg',
        Mortgage: '../../../images/Vault/current_account.svg',
        VehicleFinance: '../../../images/Vault/current_account.svg',
        PersonalItemFinance: '../../../images/Vault/current_account.svg'
      };

    transform(value: string) {
        return this.providerImageUrls[value];
    }

}
