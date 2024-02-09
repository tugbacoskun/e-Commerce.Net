using Microsoft.AspNetCore.Identity;

namespace Api.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}