using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainService.Repositoy;
using NUnit.Framework;
using System.Collections.Generic;
using Test.UnitTest.Common.Models;

namespace Test.UnitTest.Core.ApplicationService
{
    [TestFixture]
    public class WebServiceAssignmentServiceTests : BaseReadOnlyServiceTests<IWebServiceAssignmentRepository, WebServiceAssignment, short>
    {

        #region Properties

        protected new WebServiceAssignmentService Service
        {
            get => base.Service as WebServiceAssignmentService;
        }

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

        public WebServiceAssignmentServiceTests()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods

        [SetUp]
        public override void Setup()
        {
            base.SetService<WebServiceAssignmentService>();
        }

        #endregion /Methods

    }
}
