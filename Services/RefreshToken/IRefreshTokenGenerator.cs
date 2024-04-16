namespace AuthenticationApp.Services.RefreshToken
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken(string username);
    }
}
