using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;
using Test.UnitTest.Common.Models;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class BranchRepositoryTests : RepositoryTests<Branch, int>
    {

        #region Properties
        
        protected override Branch Entity
        {
            get => new BranchModel().Entity;
        }

        protected override IList<Branch> EntityList
        {
            get => new BranchModel().EntityList;
        }

        #endregion /Properties

        #region Constructors

        public BranchRepositoryTests()
            : base()
        {
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
