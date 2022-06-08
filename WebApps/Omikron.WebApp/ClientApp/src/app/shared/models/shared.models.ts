export interface PaginationInfo {
    page: number;
    pageSize: number;
    total: number;
}

export interface ApiErrorResponse {
    errors: any;
    title: string;
    status: number;
    traceId: string;
}

export interface ApiResponse<TEntity> {
    records: TEntity;
    paginationInfo: PaginationInfo;
}

export enum AccountStatus {
    New = "1",
    OnBoarding = "2",
    Active = "3",
    Disabled = "4",
    PerformKyc = "5",
    AddBankAccount = "6"
}

export interface UserBase {
    id?: string;
    firstName?: string;
    lastName?: string;
    fullName?: string;
    username?: string;
    phoneNumber?: string;
    profilePhoto?: string;
    tenantName?: string;
    tenantIdentifier?: string;
    accountStatus?: AccountStatus;
    signUpStatus?: AccountStatus;
    roles?: string[];
}

export class Claims {
    public static readonly UserId: string = "omikron.claim.profile.user.id";
    public static readonly Username: string = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    public static readonly Roles: string = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    public static readonly FirstName: string = "given_name";
    public static readonly LastName: string = "family_name";
    public static readonly ProfilePhoto: string = "omikron.claim.profile.photo";
    public static readonly TenantName: string = "omikron.claim.tenant.name";
    public static readonly TenantIdentifier: string = "omikron.claim.tenant.identifier";
    public static readonly AccountStatus: string = "omikron.claim.profile.user.account-status";
    public static readonly PhoneNumber: string = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
    public static readonly Permissions: string = "omikron.claim.profile.user.permissions";
    static readonly BlobAccessToken = 'aaf.claim.blob.access.token';
}

export class Constants {
    public static readonly TenantHeaderKey: string = "x-tenant-id";
    public static readonly TenantIdQueryKey: string = "tenantId";
    public static readonly AuthorizationHeaderKey: string = "Authorization";
    public static readonly AuthorizationHeaderPrefixKey: string = "Bearer";
    public static readonly AccessTokenStorageKey: string = "access_token";
    public static readonly UserPermissionsStorageKey: string = "user_permissions";
    public static readonly InteractiveRoleManagementClass: string = "interactive-role-management";
    public static readonly Title: string = "OMIKRON";
}


export const MonthsList: string[] =
    [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
    ];

export enum Months {
    January = 0,
    February = 1,
    March = 2,
    April = 3,
    May = 4,
    June = 5,
    July = 6,
    August = 7,
    September = 8,
    October = 9,
    November = 10,
    December = 11
}
