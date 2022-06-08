import { Injectable } from "@angular/core";
import { VaultAssetType } from "./vault.models";

@Injectable({
    providedIn: 'root'
})
export class VaultAccountDeleteService {
    public accountId: string;
    public accountType: VaultAssetType;
    public provider: string;
}