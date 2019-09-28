using System;
using System.Collections.Generic;

namespace Test.UnitTest.Common.Models
{
    public interface BaseModel<TEntity>
    {

        TEntity Entity { get; }

        IList<TEntity> EntityList { get; }

    }
}
