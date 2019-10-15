using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;
using Test.Common.Models;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class BankRepositoryTests : RepositoryTests<Bank, int>
    {

        #region Properties

        protected override Bank Entity
        {
            get => new BankModel().Entity;
        }

        protected override IList<Bank> EntityList
        {
            get => new BankModel().EntityList;
        }


        #endregion /Properties

        #region Constructors

        public BankRepositoryTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public override void Setup()
        {
            base.SetRepository<BankRepository>();
        }

        #endregion /Methods

    }
}
