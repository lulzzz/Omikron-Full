namespace Omikron.SharedKernel.Security
{
    public interface ITokenService
    {
        string GetToken();
        string GetUserToken();
    }
}