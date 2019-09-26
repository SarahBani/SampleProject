using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class BankRepositoryTests : RepositoryTests<Bank, int>
    {

        #region Properties


        #endregion /Properties

        #region Constructors

        public BankRepositoryTests()
            : base()
        {
        }

        protected override Bank Entity
        {
            get => new Bank()
            {
                Id = 3,
                Name = "Bank1"
            };
        }

        protected override IList<Bank> EntityList
        {
            get => new List<Bank>
                {
                    new Bank()
                    {
                        Id = 3,
                        Name = "Bank1"
                    },
                    new Bank()
                    {
                        Id = 4,
                        Name = "hhhh"
                    }
                };
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetRepository<BankRepository>();
        }

        #endregion /Methods

    }
}
