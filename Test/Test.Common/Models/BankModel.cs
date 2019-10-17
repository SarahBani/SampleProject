using Core.DomainModel.Entities;
using System.Collections.Generic;

namespace Test.Common.Models
{
    public class BankModel : BaseModel<Bank>
    {

        public Bank Entity
        {
            get => new Bank()
            {
                Id = 3,
                Name = "sdgsg",
                Grade = Grade.B
            };
        }

        public IList<Bank> EntityList
        {
            get => new List<Bank>
                {
                    new Bank()
                    {
                        Id = 3,
                        Name = "sdgsg",
                        Grade = Grade.B
                    },
                    new Bank()
                    {
                        Id = 4,
                        Name = "hhhh"
                    },
                    new Bank()
                    {
                        Id = 5,
                        Name = "gggggggggggg",
                        Grade = Grade.A
                    }
                };
        }

    }
}
