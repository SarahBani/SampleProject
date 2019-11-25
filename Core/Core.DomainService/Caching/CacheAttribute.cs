using Core.DomainModel;
using System;

namespace Core.DomainService.Caching
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {

        #region Properties

        public CachingDuration Duration { get; set; }

        #endregion /Properties

        #region Constructors

        public CacheAttribute()
        {
        }

        #endregion /Constructors

    }
}
