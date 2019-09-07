using Microsoft.AspNetCore.Identity;

namespace Core.DomainModel.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }

    }
}
