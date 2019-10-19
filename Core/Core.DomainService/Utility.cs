using Core.DomainModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace Core.DomainService
{
    public static class Utility
    {

        public static string GetConnectionString(IConfiguration config)
        {
            return config.GetConnectionString(Constant.AppSetting_DefaultConnection);
        }

        //public static T GetApplicationSettingSecion<T>(IConfiguration config) where T : class, ISetting
        //{
        //    return config.GetSection(typeof(T).Name).Get<T>();
        //}

        public static string GetApplicationSetting(IConfiguration config, string key)
        {
            return config.GetSection(key).Value;
        }

        public static IQueryable<T> SetOrderExpression<T, K>(IQueryable<T> query, Sort sort)
        {
            var expression = GetRelatedPropertyExpression<T, K>(sort.SortField);
            if (sort.SortDirection == SortDirection.ASC)
            {
                return query.OrderBy(expression);
            }
            else
            {
                return query.OrderByDescending(expression);
            }
        }

        public static Expression<Func<T, K>> GetRelatedPropertyExpression<T, K>(string property)
        {
            var param = Expression.Parameter(typeof(T), "q");
            var body = Expression.PropertyOrField(param, property);
            var lambda = Expression.Lambda<Func<T, K>>(body, param);

            return lambda;
        }

        //public static string GetUserIPAddress(IHttpContextAccessor httpContextAccessor)
        //{
        //    var context = httpContextAccessor.HttpContext;
        //    //string strIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; // when user is behind proxy server
        //    //if (String.IsNullOrEmpty(strIPAddress))
        //    //{
        //    //    return context.Request.ServerVariables["REMOTE_ADDR"]; // Without proxy
        //    //}
        //    //else
        //    //{
        //    //    string[] strIPArray = strIPAddress.Split(',');
        //    //    return strIPArray[0];
        //    //}

        //    return context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
        //}

        //public static IList<InvalidAttempt> GetInvalidAttempts()
        //{
        //    IList<InvalidAttempt> invalidAttempts = null;

        //    if (HttpContext.Current.Application[Constant.InvalidAttempts] == null) // Adding List to Application State
        //    {
        //        invalidAttempts = new List<InvalidAttempt>();
        //    }
        //    else
        //    {
        //        invalidAttempts = (IList<InvalidAttempt>)HttpContext.Current.Application[Constant.InvalidAttempts];
        //    }
        //    return invalidAttempts;
        //}

        //public static void SaveInvalidAttempts(IHttpContextAccessor httpContextAccessor, 
        //    IList<InvalidAttempt> invalidAttempts)
        //{
        //    var application = httpContextAccessor.HttpContext.Application;
        //    application.Lock();
        //    application[Constant.InvalidAttempts] = invalidAttempts;
        //    application.UnLock();
        //}

        //public static bool NeedsCaptcha()
        //{
        //    string ip = GetUserIPAddress();
        //    var invalidIPAttempt = GetInvalidAttempts().ToList().Find(x => x.IP.Equals(ip));
        //    return (invalidIPAttempt != null && (invalidIPAttempt.Count >= Utility.MaxInvalidAttempt));
        //}

        //public static SmtpClient GetSmtpSection(string sectionName)
        //{
        //    SmtpSection smtpSection = WebConfigurationManager.GetWebApplicationSection("mailSettings/" + sectionName) as SmtpSection;
        //    SmtpNetworkElement network = smtpSection.Network;
        //    return new SmtpClient()
        //    {
        //        Host = network.Host,
        //        Port = network.Port,
        //        UseDefaultCredentials = network.DefaultCredentials,
        //        Credentials = new NetworkCredential(network.UserName, network.Password),
        //        EnableSsl = network.EnableSsl
        //    };
        //}

        public static string SetLineBreak(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                return content.Replace("\r\n", "<br />");
            }
            return content;
        }

    }
}
