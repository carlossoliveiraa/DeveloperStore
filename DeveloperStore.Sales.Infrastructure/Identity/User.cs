using Microsoft.AspNetCore.Identity;

namespace DeveloperStore.Sales.Infrastructure.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;
    }
}
