namespace Omikron.IdentityService.ViewModel
{
	public class UserRegistrationDateViewModel
    {
		public int Day { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }

		public UserRegistrationDateViewModel(int day, int month, int year)
		{
			Day = day;
			Month = month;
			Year = year;
		}
	}
}
