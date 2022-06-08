namespace Omikron.IdentityService.Infrastructure.Data.Model
{
    public enum AccountStatus
    {
        New = 1,
        OnBoarding = 2,
        Active = 3,
        Disabled = 4,
        PerformKyc = 5,
        AddBankAccount = 6
    }
}