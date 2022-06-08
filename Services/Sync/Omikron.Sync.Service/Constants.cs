namespace Omikron.Sync.Service
{
    public static class Constants
    {
        public static string ServiceName = "Omikron.Sync.ApiService";
        public static string ServiceDescription = $"Represent the {ServiceName} which is the main responsibility for managing sync and accounts.";
        public const string DefaultCurrencyCode = "GBP";
        public const string SortCodeAccountNumberSchemeName = "UK.OBIE.SortCodeAccountNumber";
        public const string PanSchemeName = "UK.OBIE.PAN";
        public const int NumberOfKnownDigitsOfPanNumber = 4;
    }
}