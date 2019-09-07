using Core.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DomainServices
{
    public static class Extension
    {

        public static IQueryable<TEntity> SetOrder<TEntity>(this IQueryable<TEntity> query, IList<Sort> sorts)
        {
            if (sorts != null && sorts.Count() > 0)
            {
                foreach (var sort in sorts)
                {
                    var propertyExpression = Utility.GetRelatedPropertyExpression<TEntity, TEntity>(sort.SortField);

                    if (sort.SortDirection == SortDirection.ASC)
                    {
                        query = query.OrderBy(propertyExpression);
                    }
                    else
                    {
                        query = query.OrderByDescending(propertyExpression);
                    }
                }
            }
            return query;
        }

        public static IQueryable<TEntity> SetPage<TEntity>(this IQueryable<TEntity> query, Page page)
        {
            if (page != null)
            {
                query = query.Skip(page.FirstRowIndex).Take(page.Count);
            }
            return query;
        }

        public static TEntity Trim<TEntity, TKey>(this TEntity entity)
            where TEntity : Entity<TKey>
        {
            var type = typeof(Entity<TKey>);
            var properties = type.GetProperties()
             .Where(q => q.PropertyType == typeof(string)); // Obtain all string & char properties

            foreach (var prop in properties) // Loop through properties
            {
                Type propertyType = prop.PropertyType;
                object propertyValue = Convert.ChangeType(prop, propertyType);
                var newPropValue = Convert.ChangeType(propertyValue.ToString().Trim(), propertyType);
                prop.SetValue(entity, newPropValue);
            }
            return entity;
        }

        //public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        //{
        //    var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
        //                        && !x.IsAbstract
        //                        && x != compareType
        //                        && x.GetInterfaces()
        //                                .Any(i => i.IsGenericType
        //                                        && i.GetGenericTypeDefinition() == compareType))?.ToList();

        //    return typeInfoList;
        //}

        //public static void AddClassesAsImplementedInterface(
        //        this IServiceCollection services,
        //        Assembly assembly,
        //        Type compareType,
        //        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        //{
        //    assembly.GetTypesAssignableTo(compareType).ForEach(type =>
        //    {
        //        foreach (var implementedInterface in type.ImplementedInterfaces)
        //        {
        //            switch (lifetime)
        //            {
        //                case ServiceLifetime.Scoped:
        //                    services.AddScoped(implementedInterface, type);
        //                    break;
        //                case ServiceLifetime.Singleton:
        //                    services.AddSingleton(implementedInterface, type);
        //                    break;
        //                case ServiceLifetime.Transient:
        //                    services.AddTransient(implementedInterface, type);
        //                    break;
        //            }
        //        }
        //    });
        //}

    }
}
