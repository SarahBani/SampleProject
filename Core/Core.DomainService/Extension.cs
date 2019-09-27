using Core.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DomainServices
{
    public static class Extension
    {

        public static IQueryable<TEntity> SetOrder<TEntity>(this IQueryable<TEntity> query, IList<Sort> sorts)
        {
            if (sorts != null)
            {
                foreach (var sort in sorts)
                {
                    var propertyType = typeof(TEntity).GetProperty(sort.SortField).PropertyType;

                    var method = typeof(Utility).GetMethod("SetOrderExpression");
                    var genericMethod = method.MakeGenericMethod(typeof(TEntity), propertyType);
                    query = genericMethod.Invoke(null, new object[] { query, sort }) as IQueryable<TEntity>;
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
