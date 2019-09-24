using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class BankServiceTests : BaseServiceTests<IBankRepository, Bank, int>
    {

        #region Properties

        protected override Bank Entity
        {
            get => new Bank()
            {
                Id = 3,
                Name = "sdgsg"
            };
        }

        protected override IList<Bank> EntityList
        {
            get => new List<Bank>
                {
                    new Bank()
                    {
                        Id = 3,
                        Name = "sdgsg"
                    },
                    new Bank()
                    {
                        Id = 4,
                        Name = "سیبسیب"
                    }
                };
        }

        #endregion /Properties

        #region Constructors

        public BankServiceTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetService<BankService>();
        }

        #endregion /Methods

    }
}
