namespace Omikron.SharedKernel.Infrastructure.Vault.Events
{
	public class VehicleSyncPriceEvent
    {
		public string VehicleId { get; set; }
		public string VehicleRegistration { get; set; }
		public string VehicleName { get; set; }
		public int VehicleMileage { get; set; }
		public decimal NewVehicleValue { get; set; }
		public bool AutoRevalue { get; set; }

		public VehicleSyncPriceEvent(string vehicleId, string vehicleRegistration, string vehicleName, int vehicleMileage, bool autoRevalue, decimal newVehicleValue)
		{
			VehicleId = vehicleId;
			VehicleRegistration = vehicleRegistration;
			VehicleName = vehicleName;
			VehicleMileage = vehicleMileage;
			AutoRevalue = autoRevalue;
			NewVehicleValue = newVehicleValue;
		}
	}
}
