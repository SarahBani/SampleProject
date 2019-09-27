using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices.Repositoy;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            get => new WebServiceAssignment()
            {
                Id = 3,
                CompanyName = "Company1",
                ValidationDate = DateTime.Now.AddDays(365),
                Token = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                //WebServiceType =  
            };
        }

        protected override IList<WebServiceAssignment> EntityList
        {
            get => new List<WebServiceAssignment>
                {
                    new WebServiceAssignment()
                    {
                        Id = 3,
                        CompanyName = "Company1",
                        ValidationDate = DateTime.Now.AddDays(365),
                        Token = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                    },
                    new WebServiceAssignment()
                    {
                        Id = 4,
                        CompanyName = "dfgdfg",
                        ValidationDate = DateTime.Now.AddDays(50),
                        Token = new Guid("81993344-5566-7788-99AA-BB88DDEEFF22"),
                    },
                    new WebServiceAssignment()
                    {
                        Id = 8,
                        CompanyName = "jghjj",
                        ValidationDate = DateTime.Now.AddDays(-50), // expired
                        Token = new Guid("24593344-5566-7788-99AA-BB88D223322"),
                    },
                };
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
