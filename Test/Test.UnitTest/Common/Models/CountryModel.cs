using Core.DomainModel.Entities;
using System.Collections.Generic;

namespace Test.UnitTest.Common.Models
{
    public class CountryModel : BaseModel<Country>
    {

        public Country Entity
        {
            get => new Country()
            {
                Id = 3,
                Name = "Iran"
            };
        }

        public IList<Country> EntityList
        {
            get => new List<Country>
                {
                    new Country()
                    {
                        Id = 3,
                        Name = "Iran"
                    },
                    new Country()
                    {
                        Id = 4,
                        Name = "Turkey"
                    }
                };
        }

    }
}
