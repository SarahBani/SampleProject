using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class CountryRepositoryTests : ReadOnlyRepositoryTests<Country, short>
    {

        #region Properties


        #endregion /Properties

        #region Constructors

        public CountryRepositoryTests()
            : base()
        {
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

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetRepository<CountryRepository>();
        }

        #endregion /Methods

    }
}
