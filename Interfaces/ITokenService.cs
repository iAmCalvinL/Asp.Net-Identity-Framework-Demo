using identity_demo.Models;

namespace identity_demo.Interfaces
{
    public interface ITokenService 
    {
        public string CreateToken(AppUser user);
    }
}