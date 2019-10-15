using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using Test.Common.Models;

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
            get => new CountryModel().Entity;
        }

        protected override IList<Country> EntityList
        {
            get => new CountryModel().EntityList;
        }

        #endregion /Properties

        #region Constructors

        public CountryServiceTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public override void Setup()
        {
            base.SetService<CountryService>();
        }

        #endregion /Methods

    }
}
