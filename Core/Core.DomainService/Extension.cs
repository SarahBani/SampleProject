using Core.DomainModel;
using Core.DomainModel.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Core.DomainService
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

        public static bool IsAssignedGenericType(this Type type, Type genericType)
        {
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            Type baseType = type.BaseType;
            if (baseType == null)
            {
                return false;
            }
            return baseType.IsAssignedGenericType(genericType);
        }

        public static Type GetBaseDeclaringType(this MethodBase method)
        {
            Type declaringType = method.DeclaringType;
            while (declaringType.DeclaringType != null)
            {
                declaringType = declaringType.DeclaringType;
            }
            return declaringType;
        }

        public static MethodBase GetRealMethod(this MethodBase method)
        {
            Type generatedType = method.DeclaringType;
            Type originalType = generatedType.DeclaringType;
            if (originalType != null)
            {
                var matchingMethods =
                    from methodInfo in originalType.GetMethods()
                    let attr = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>()
                    where attr != null && attr.StateMachineType == generatedType
                    select methodInfo;

                if (matchingMethods.Any())
                {
                    return matchingMethods.Single();
                }
                else
                {
                    return null;
                }
            }
            return method;
        }

        public static ModelStateDictionary AddModelRequiredError(this ModelStateDictionary modelState, string key)
        {
            modelState.AddModelError(key, string.Format(Constant.Validation_RequiredField, key));
            return modelState;
        }

        public static void ResetValueGenerators(this DbContext context)
        {
            var cache = context.GetService<IValueGeneratorCache>();

            foreach (var keyProperty in context.Model.GetEntityTypes()
                .Select(e => e.FindPrimaryKey().Properties[0])
                .Where(p => p.ClrType == typeof(int) && p.ValueGenerated == ValueGenerated.OnAdd))
            {
                var generator = (ResettableValueGenerator)cache.GetOrAdd(
                    keyProperty,
                    keyProperty.DeclaringEntityType,
                    (p, e) => new ResettableValueGenerator());

                generator.Reset();
            }
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
