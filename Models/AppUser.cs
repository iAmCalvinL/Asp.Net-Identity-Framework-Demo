using Microsoft.AspNetCore.Identity;

namespace identity_demo.Models
{
    // inheriting from IdentityUser automatically generates stuff like user & password fields
    public class AppUser : IdentityUser
    {
        
    }
}