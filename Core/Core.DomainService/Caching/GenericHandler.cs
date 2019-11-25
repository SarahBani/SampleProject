using Castle.DynamicProxy;
using Core.DomainModel.Entities;
using Core.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DomainService.Caching
{
    public static class GenericHandler
    {

        public static IList<TEntity> FetchCache<TEntity, TKey>(IList<TEntity> list, IInvocation invocation)
            where TEntity : Entity<TKey>
        {
            var paramExp = Expression.Parameter(typeof(TEntity), "q");
            var query = list.AsQueryable();
            for (int i = 0; i < invocation.Arguments.Count(); i++)
            {
                var paramName = Utility.GetPascalCase(invocation.Method.GetParameters()[i].Name);
                var paramValue = invocation.Arguments[i];
                var lambdaExp = Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Equal(Expression.Property(paramExp, paramName),
                    Expression.Constant(paramValue)), paramExp);
                query = query.Where(lambdaExp);
            }
            return query.ToList();
        }

    }
}
