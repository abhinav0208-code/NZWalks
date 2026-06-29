using Microsoft.AspNetCore.Identity;

namespace NZWalksAuth.API.Repositories
{
    public interface ITokenRepository
    {

        public string GetJWTToken(IdentityUser user, List<string> roles);
    }
}
