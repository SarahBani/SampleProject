using Core.DomainModel.Entities;
using System;
using System.Collections.Generic;

namespace Test.UnitTest.Common.Models
{
    public class WebServiceAssignmentModel : BaseModel<WebServiceAssignment>
    {

        public WebServiceAssignment Entity
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

        public IList<WebServiceAssignment> EntityList
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
                        Token = new Guid("81993344-5566-7788-99AA-BB88DDEEF65F"),
                    },
                    new WebServiceAssignment()
                    {
                        Id = 8,
                        CompanyName = "jghjj",
                        ValidationDate = DateTime.Now.AddDays(-50), // expired
                        Token = new Guid("24593344-5566-7788-99AA-BB88D2233DF2"),
                    },
                };
        }

    }
}
