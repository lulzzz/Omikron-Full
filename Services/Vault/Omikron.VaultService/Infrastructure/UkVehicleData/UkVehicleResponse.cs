using System;

namespace Omikron.VaultService.Infrastructure.UkVehicleData
{
    public class UkVehicleResponse
    {
        public Billingaccount BillingAccount { get; set; }
        public Technicalsupport TechnicalSupport { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
    }

    public class Billingaccount
    {
        public string AccountType { get; set; }
        public object AccountBalance { get; set; }
        public object TransactionCost { get; set; }
        public Extrainformation ExtraInformation { get; set; }
    }

    public class Extrainformation
    {
    }

    public class Technicalsupport
    {
        public string ServerId { get; set; }
        public string RequestId { get; set; }
        public int QueryDurationMs { get; set; }
        public string SupportDate { get; set; }
        public string SupportTime { get; set; }
        public string SupportCode { get; set; }
        public string[] SupportInformationList { get; set; }
    }

    public class Request
    {
        public string RequestGuid { get; set; }
        public string PackageId { get; set; }
        public int PackageVersion { get; set; }
        public int ResponseVersion { get; set; }
        public Datakeys DataKeys { get; set; }
    }

    public class Datakeys
    {
        public string Vrm { get; set; }
        public string Mileage { get; set; }
    }

    public class Response
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public Statusinformation StatusInformation { get; set; }
        public Dataitems DataItems { get; set; }
    }

    public class Statusinformation
    {
        public Lookup Lookup { get; set; }
    }

    public class Lookup
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object[] AdviceTextList { get; set; }
    }

    public class Dataitems
    {
        public string Vrm { get; set; }
        public string Mileage { get; set; }
        public string PlateYear { get; set; }
        public Valuationlist ValuationList { get; set; }
        public DateTime ValuationTime { get; set; }
        public string VehicleDescription { get; set; }
        public string ValuationBook { get; set; }
        public int ExtractNumber { get; set; }
    }

    public class Valuationlist
    {
        public string OTR { get; set; }
        public string DealerForecourt { get; set; }
        public string TradeRetail { get; set; }
        public string PrivateClean { get; set; }
        public string PrivateAverage { get; set; }
        public string PartExchange { get; set; }
        public string Auction { get; set; }
        public string TradeAverage { get; set; }
        public string TradePoor { get; set; }
    }
}
