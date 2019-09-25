using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class CountryServiceTests : BaseReadOnlyServiceTests<ICountryRepository, Country, short>
    {

        #region Properties

        protected new CountryService Service
        {
            get => base.Service as CountryService;
        }

        protected override Country Entity
        {
            get => new Country()
            {
                Id = 3,
                Name = "Iran"
            };
        }

        protected override IList<Country> EntityList
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

        #endregion /Properties

        #region Constructors

        public CountryServiceTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetService<CountryService>();
        }

        #endregion /Methods

    }
}
