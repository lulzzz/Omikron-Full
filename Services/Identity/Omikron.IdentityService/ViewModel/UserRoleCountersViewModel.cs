namespace Omikron.IdentityService.ViewModel
{
    public class UserRoleCountersViewModel
    {
        public int NumberOfUsers => NumberActiveUser + NumberInactiveUser;
        public int NumberOfRoles { get; set; }
        public int NumberActiveUser { get; set; }
        public int NumberInactiveUser { get; set; }
    }
}
