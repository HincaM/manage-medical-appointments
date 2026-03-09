namespace Authentication.Api.Domain.Interfaces
{
    public interface ITokenService
    {
        string GetToken(string userName, string password);
    }
}
