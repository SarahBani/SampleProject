using System.Collections.Generic;

namespace Core.DomainModel.Entities
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Bank : Entity<int>
    {

        public string Name { get; set; }
        public Grade? Grade { get; set; }

        public ICollection<Branch> Branches { get; set; }

    }
}
