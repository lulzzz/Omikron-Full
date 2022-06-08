import { UserBase } from '../shared';

export enum TenantStatus {
    Active = 1,
    Inactive = 2,
    Deleted = 3
}

export enum TenantAzureAssetStatus {
    New = 1,
    Creating = 2,
    Error = 3,
    Success = 4
}

export interface TenantAssetViewModel {
    assetStatus: TenantAzureAssetStatus;
    sqlDatabaseResourceUri: string;
    sqlDatabaseName: string;
    assertCreationIsTooLong: boolean;
}

export interface Tenant {
    id: string;
    identifier: string;
    name: string;
    logo: string;
    cssFile: string;
    cssStyleContent: string;
    assetViewModel: TenantAssetViewModel;
    status: TenantStatus;
    administrators: UserBase[]
    isReadyForUse: boolean;
}