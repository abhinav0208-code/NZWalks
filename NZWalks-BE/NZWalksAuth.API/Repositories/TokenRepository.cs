using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZWalksAuth.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private IConfiguration configuration;
        public TokenRepository(IConfiguration _configuration)
        {
            this.configuration = _configuration;

        }

        public string GetJWTToken(IdentityUser user, List<string> roles)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }

            SymmetricSecurityKey secureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(secureKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
