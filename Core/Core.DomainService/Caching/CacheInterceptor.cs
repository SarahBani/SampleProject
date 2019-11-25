using Castle.DynamicProxy;
using System.Reflection;

namespace Core.DomainService.Caching
{
    public class CacheInterceptor : IInterceptor
    {

        #region Properties

        #endregion

        #region Constructors

        public CacheInterceptor()
        {
        }

        #endregion /Constructors

        #region Methods

        public void Intercept(IInvocation invocation)
        {
            string methodFullName = GetMethodName(invocation.Method);
            var cacheAttribute = invocation.MethodInvocationTarget.GetCustomAttribute<CacheAttribute>();
            if (cacheAttribute != null)
            {  
                string cacheKey = methodFullName + GetParametersValues(invocation.Arguments);
                var cachedItem = CachingProvider.Instance.GetItem(cacheKey);
                if (cachedItem != null)
                {
                    invocation.ReturnValue = cachedItem;
                }
                else
                {
                    invocation.Proceed();
                    CachingProvider.Instance.AddItem(cacheKey, invocation.ReturnValue, cacheAttribute.Duration);
                }
            }
            else
            {
                invocation.Proceed();
            }
        }

        private string GetMethodName(MethodBase method)
        {
            return method.ReflectedType.Name + "." + method.Name;
        }

        private string GetParametersValues(object[] args)
        {
            string parametersValues = string.Empty;
            foreach (var arg in args)
            {
                string paramValue = arg != null ? arg.ToString() : "null";
                parametersValues += string.Format("_{0}", paramValue.Replace(" ", "-"));
            }

            return parametersValues;
        }

        #endregion /Methods

    }
}
