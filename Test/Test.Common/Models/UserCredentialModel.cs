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
                Password = "Test@123"
            };
        }

        public UserCredential NullUserNameEntity
        {
            get => new UserCredential()
            {
                Username = null,
                Password = "123"
            };
        }

        public UserCredential NullPasswordEntity
        {
            get => new UserCredential()
            {
                Username = "dfdfg",
                Password = null,
            };
        }

        public UserCredential NotAuthenticatedEntity
        {
            get => new UserCredential()
            {
                Username = "asd",
                Password = "qwe"
            };
        }

    }
}
