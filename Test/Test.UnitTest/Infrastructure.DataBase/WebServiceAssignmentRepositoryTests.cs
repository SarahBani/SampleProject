using System;
using System.Collections.Generic;
using Core.DomainModel.Entities;
using Infrastructure.DataBase.Repository;
using NUnit.Framework;

namespace Test.UnitTest.Infrastructure.DataBase
{
    [TestFixture]
    public class WebServiceAssignmentRepositoryTests : ReadOnlyRepositoryTests<WebServiceAssignment, short>
    {

        #region Properties


        #endregion /Properties

        #region Constructors

        public WebServiceAssignmentRepositoryTests()
            : base()
        {
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
