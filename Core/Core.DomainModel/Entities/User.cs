using Microsoft.AspNetCore.Identity;

namespace Core.DomainModel.Entities
{
    public class User : IdentityUser<int>
    {
        public string CustomTag { get; set; }
    }
}
