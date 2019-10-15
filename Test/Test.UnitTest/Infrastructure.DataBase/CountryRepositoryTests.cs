using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;
using Test.Common.Models;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class CountryRepositoryTests : ReadOnlyRepositoryTests<Country, short>
    {

        #region Properties

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

        public CountryRepositoryTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public override void Setup()
        {
            base.SetRepository<CountryRepository>();
        }

        #endregion /Methods

    }
}
