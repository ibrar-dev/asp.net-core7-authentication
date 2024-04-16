using AuthenticationApp.DB;
using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace AuthenticationApp.Services.RefreshToken
{
    public class RefreshTokenGenerator: IRefreshTokenGenerator
    {
        private readonly AuthenticationAPIContext _context;

        public RefreshTokenGenerator(AuthenticationAPIContext Auth_DB)
        {
            _context = Auth_DB;
        }
        public string GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string RefreshToken = Convert.ToBase64String(randomnumber);
                Console.WriteLine("******************************************");
                Console.WriteLine(RefreshToken);
                return RefreshToken;
            }
        }
    }
}
