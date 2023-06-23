using Microsoft.AspNetCore.Identity;

namespace NZWalks.Repository.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
