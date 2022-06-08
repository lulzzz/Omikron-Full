namespace Omikron.SharedKernel.Infrastructure.Vault.Events
{
	public class PropertySyncPriceEvent
    {
		public string PropertyId { get; set; }
		public string PropertyName { get; set; }
		public string PropertyAddress { get; set; }
		public string Postcode { get; set; }
		public decimal NewPropertyValue { get; set; }
		public bool AutoRevalue { get; set; }
		public int NumberOfBedrooms { get; set; }

		public PropertySyncPriceEvent(string propertyId, string propertyName, string propertyAddress, string postcode, decimal newPropertyValue, bool autoRevalue, int numberOfBedrooms)
		{
			PropertyId = propertyId;
			PropertyName = propertyName;
			PropertyAddress = propertyAddress;
			Postcode = postcode;
			NewPropertyValue = newPropertyValue;
			AutoRevalue = autoRevalue;
			NumberOfBedrooms = numberOfBedrooms;
		}
	}
}
