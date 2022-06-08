namespace Omikron.SharedKernel.Utils
{
    //TODO Move these endpoints to configuration
    public static class BudApiEndpoints
    {
        public const string Authentication = "v1/oauth/token";
        public const string CreateCustomer = "v1/customers";
        public const string ListObProviders = "v1/open-banking/providers";
        public const string RetrieveAuthorisationGatewayUrl = "v1/open-banking/authorisation-gateway-url";
        public const string InitiateRevokeConsent = "v1/open-banking/account-access-consent/revoke";
        public const string ListTransactions = "v1/open-banking/transactions";
        public const string ListCustomerConsents = "v1/open-banking/account-access-consents";
        public const string RetrieveCategoryTotals = "v1/categories/totals"; 
        public const string InitiatePersonCheck = "v1/kyc/check/person";
        public const string RetrieveKYCCheck = "v1/kyc/check";
        public const string RetrieveRevokeConsentStatus = "v1/open-banking/account-access-consent/revoke";
        public const string RemoveProviderData = "v1/provider";

        //Testing purposes
        public const string ListAccounts = "v1/open-banking/accounts";
        public const string InitiateRefresh = "v1/open-banking/refresh";
        public const string RetrieveMerchantTotals = "v1/merchants/totals";

        //Testing purposes
        public const string RetrieveRefreshStatus = "v1/open-banking/refresh";
    }
}
