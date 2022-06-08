namespace Omikron.VaultService.Infrastructure.PropertyData
{
    public class PropertyDataResponse
    {
        public string Status { get; set; }
        public string Postcode { get; set; }
        public string Url { get; set; }
        public int Bedrooms { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Radius { get; set; }
        public int Average { get; set; }
        public int[] _70pc_range { get; set; }
        public int[] _80pc_range { get; set; }
        public int[] _90pc_range { get; set; }
        public int[] _100pc_range { get; set; }
    }

}