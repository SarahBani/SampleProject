using Core.DomainService.Models;

namespace Test.Common.Models
{
    public class UserCredentialModel
    {
        public UserCredential Entity
        {
            get => new UserCredential()
            {
                Username = "User",
                Password = "123"
            };
        }

    }
}
