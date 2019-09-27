using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class BranchRepositoryTests : RepositoryTests<Branch, int>
    {

        #region Properties


        #endregion /Properties

        #region Constructors

        public BranchRepositoryTests()
            : base()
        {
        }

        protected override Branch Entity
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

        protected override IList<Branch> EntityList
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

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetRepository<BranchRepository>();
        }

        #endregion /Methods

    }
}
