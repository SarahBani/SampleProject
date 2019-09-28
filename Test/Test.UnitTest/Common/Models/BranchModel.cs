using Core.DomainModel.Entities;
using System.Collections.Generic;

namespace Test.UnitTest.Common.Models
{
    public class BranchModel : BaseModel<Branch>
    {

        public Branch Entity
        {
            get
            {
                var country = new Country()
                {
                    Id = 5,
                    Name = "ffffff"
                };
                var address = new Address(country, "Rasht", "fhfh fghjf dgh", "17", "1998737863");
                return new Branch()
                {
                    Id = 3,
                    BankId = 5,
                    Code = 50,
                    Name = "sdgsg",
                    Address = address
                };
            }
        }

        public IList<Branch> EntityList
        {
            get => new List<Branch>
                {
                    new Branch()
                    {
                        Id = 3,
                        Name = "sdgsg"
                    },
                    new Branch()
                    {
                        Id = 4,
                        Name = "hjgj"
                    }
                };
        }

    }
}
