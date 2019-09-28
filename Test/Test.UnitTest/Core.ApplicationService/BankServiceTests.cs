using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using NUnit.Framework;
using System.Collections.Generic;
using Test.UnitTest.Common.Models;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class BankServiceTests : BaseServiceTests<IBankRepository, Bank, int>
    {

        #region Properties

        protected new BankService Service
        {
            get => base.Service as BankService;
        }

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
