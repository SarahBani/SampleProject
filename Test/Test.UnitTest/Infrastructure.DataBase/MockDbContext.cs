using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Test.UnitTest.Infrastructure.DataBase
{
    //public static MockedDbContext<MyDbContext> :MockDbContext(
    //     IList<Contract> contracts = null,
    //     IList<User> users = null)
    //{
    //    var mockContext = new Mock<DbContext>();

    //    // Create the DbSet objects.
    //    var dbSets = new object[]
    //    {
    //        MoqUtilities.MockDbSet(contracts, (objects, contract) => contract.ContractId == (int)objects[0] && contract.AmendmentId == (int)objects[1]),
    //        MoqUtilities.MockDbSet(users, (objects, user) => user.Id == (int)objects[0])
    //    };

    //    return new MockedDbContext<SourcingDbContext>(mockContext, dbSets);
    //}

    public static class MockDbSet
    {
        public static Mock<DbSet<T>> CreateMockDbSet<T>(IList<T> data) where T : class
        {
            var mock = new Mock<DbSet<T>>();
            var queryData = data.AsQueryable();
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryData.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryData.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryData.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryData.GetEnumerator());

            Type type = typeof(T);
            string colName = "Id";
            var pk = type.GetProperty(colName);
            if (pk == null)
            {
                colName = "Id";
                pk = type.GetProperty(colName);
            }
            if (pk != null)
            {
                mock.Setup(x => x.Add(It.IsAny<T>())).Returns((T x) =>
                {
                    if (pk.PropertyType == typeof(int)
                        || pk.PropertyType == typeof(Int32))
                    {
                        var max = data.Select(d => (int)pk.GetValue(d)).Max();
                        pk.SetValue(x, max + 1);
                    }
                    else if (pk.PropertyType == typeof(Guid))
                    {
                        pk.SetValue(x, Guid.NewGuid());
                    }
                    data.Add(x);
                    return x as EntityEntry<T>;
                });
                mock.Setup(x => x.Remove(It.IsAny<T>())).Returns((T x) =>
                {
                    data.Remove(x);
                    return x as EntityEntry<T>;
                });
                mock.Setup(x => x.Find(It.IsAny<object[]>())).Returns((object[] id) =>
                {
                    var param = Expression.Parameter(type, "t");
                    var col = Expression.Property(param, colName);
                    var body = Expression.Equal(col, Expression.Constant(id[0]));
                    var lambda = Expression.Lambda<Func<T, bool>>(body, param);
                    return queryData.FirstOrDefault(lambda);
                });
                //var filter = It.IsAny<Expression<Func<T, bool>>>();
                //mock.Setup(x => x.Count(null)).Returns((object[] id) =>
                //{
                //    var param = Expression.Parameter(type, "t");
                //    var col = Expression.Property(param, colName);
                //    var body = Expression.Equal(col, Expression.Constant(id[0]));
                //    var lambda = Expression.Lambda<Func<T, bool>>(body, param);
                //    return queryData.Count(lambda);
                //});
                //mock.Setup(x => x.Count(filter)).Returns((object[] id) =>
                //{
                //    var param = Expression.Parameter(type, "t");
                //    var col = Expression.Property(param, colName);
                //    var body = Expression.Equal(col, Expression.Constant(id[0]));
                //    var lambda = Expression.Lambda<Func<T, bool>>(body, param);
                //    return queryData.Count(lambda);
                //});
            }

            return mock;
        }
    }
}
