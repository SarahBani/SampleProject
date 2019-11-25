using Core.DomainModel;
using System;

namespace Core.DomainService.Caching
{
    public class CachingProvider : BaseCachingProvider
    {

        #region Properties

        public static CachingProvider Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private static string Today;

        private class Nested
        {

            internal static readonly CachingProvider instance = new CachingProvider();

            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

        }

        #endregion /Properties

        #region Constructors 

        protected CachingProvider()
            : base()
        {
        }

        #endregion /Constructors

        #region Methods  

        public object GetItem(string key)
        {
            return base.Get(key);
        }

        public void AddItem(string key, object value, CachingDuration duration)
        {
            base.Add(key, value, GetCurrentDate(duration));
        }

        public void RemoveItem(string key)
        {
            base.Remove(key);
        }

        private DateTimeOffset GetCurrentDate(CachingDuration duration)
        {
            switch (duration)
            {
                case CachingDuration.Hour:
                    return DateTime.Now.AddHours(1);
                case CachingDuration.Week:
                    return DateTime.Now.AddDays(7);
                case CachingDuration.Day:
                default:
                    return DateTime.Now.AddDays(1);
            }
        }

        #endregion /Methods

    }
}
