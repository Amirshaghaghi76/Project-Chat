using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey? _key;

        public TokenService(IConfiguration confing)
        {
            string? tokenValue = confing["tokenKey"];

            _ = tokenValue ?? throw new ArgumentException("token value cannot be null", nameof(tokenValue));

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValue!));
        }

        public string CreateToken(AppUser appUser)
        {
            _ = _key ?? throw new ArgumentNullException("_key cannot be null", nameof(_key));

            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.NameId,appUser.Id!)
            //  new Claim(JwtRegisteredClaimNames.Email,appUser.Email) 
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler= new JwtSecurityTokenHandler();

            var token=tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}