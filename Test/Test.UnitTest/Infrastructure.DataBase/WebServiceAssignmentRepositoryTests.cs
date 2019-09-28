using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;
using Test.UnitTest.Common.Models;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class WebServiceAssignmentRepositoryTests : ReadOnlyRepositoryTests<WebServiceAssignment, short>
    {

        #region Properties
        
        protected override WebServiceAssignment Entity
        {
            get => new WebServiceAssignmentModel().Entity;
        }

        protected override IList<WebServiceAssignment> EntityList
        {
            get => new WebServiceAssignmentModel().EntityList;
        }

        #endregion /Properties

        #region Constructors

        public WebServiceAssignmentRepositoryTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetRepository<WebServiceAssignmentRepository>();
        }

        #endregion /Methods

    }
}
