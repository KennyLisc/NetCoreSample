using Microsoft.AspNetCore.Identity;

namespace NetCoreSample.Core.Domain.User
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Department { get; set; }
    }
}
