using Core.DomainModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using Core.DomainModel.Settings;

namespace Core.DomainServices
{
    public static class Utility
    {

        public static string GetConnectionString(IConfiguration config)
        {
            return config.GetConnectionString(Constant.AppSetting_DefaultConnection);
        }

        public static T GetApplicationSettingSecion<T>(IConfiguration config) where T : class, ISetting
        {
            return config.GetSection(typeof(T).Name).Get<T>();
        }

        public static string GetApplicationSetting(IConfiguration config, string key)
        {
            return config.GetSection(key).Value;
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

        //#region Error Handling

        //public static void SaveError(HttpContext context, Exception baseException)
        //{
        //    try
        //    {
        //        SaveError(baseException, string.Format(@"{0}\{1}", MyWebRequest.WebRootPath, Utility.GetApplicationSetting(Constant.AppSetting_ErrorLogFile)));
        //    }
        //    catch (System.IO.IOException)
        //    {
        //        try
        //        {
        //            int intRandom = new Random().Next();
        //            SaveError(baseException, string.Format(@"{0}\Log\ErrorLog\{1}.txt", MyWebRequest.WebRootPath, intRandom.ToString()));
        //        }
        //        catch
        //        {
        //            // Do Nothing
        //        }
        //    }
        //    catch
        //    {
        //        // Do Nothing
        //    }
        //}

        //private static void SaveError(Exception baseException, string path)
        //{
        //    try
        //    {
        //        string strMessage = baseException.Message;
        //        string strStackTrace = baseException.StackTrace;

        //        using (StreamWriter oStream = new StreamWriter(path,
        //                                                       true,
        //                                                       Encoding.UTF8))
        //        {
        //            oStream.WriteLine("-----------------------------------------------------");
        //            oStream.WriteLine(string.Format("Message: {0} ", strMessage));
        //            oStream.WriteLine(string.Format("StackTrace: {0} ", strStackTrace));
        //            oStream.WriteLine(string.Format("DateTime: {0} ", DateTime.Now);
        //            //if (UserID > 0)
        //            //{
        //            //    oStream.WriteLine(string.Format("UserID: {0} ", UserID.ToString()));
        //            //}
        //            oStream.WriteLine(string.Format("Browser: {0} Version {1} ", HttpContext.Current.Request.Browser.Type,
        //                                                                         HttpContext.Current.Request.Browser.Version));
        //            oStream.WriteLine("-----------------------------------------------------");
        //            oStream.WriteLine("\n");

        //            oStream.Close();
        //            oStream.Dispose();
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public static void SavePaymentError(string zarinpalQueryString)
        //{
        //    try
        //    {
        //        SavePaymentError(zarinpalQueryString,
        //                        HttpContext.Current.Server.MapPath(@"~\" + Utility.GetAppSetting(Constant.AppSetting_PaymentErrorLogFile)));
        //    }
        //    catch (System.IO.IOException)
        //    {
        //        try
        //        {
        //            int intRandom = new Random().Next();
        //            SavePaymentError(zarinpalQueryString,
        //                            HttpContext.Current.Server.MapPath(@"~\Log\PaymentErrorLog" + intRandom.ToString() + ".txt"));
        //        }
        //        catch
        //        {
        //            Exception ex = HttpContext.Current.Server.GetLastError();
        //            SaveError(ex.GetBaseException());
        //        }
        //    }
        //    catch
        //    {
        //        Exception ex = HttpContext.Current.Server.GetLastError();
        //        SaveError(ex.GetBaseException());
        //    }
        //}

        //private static void SavePaymentError(string zarinpalQueryString, string path)
        //{
        //    try
        //    {
        //        using (StreamWriter oStream = new StreamWriter(path,
        //                                                       true,
        //                                                       Encoding.UTF8))
        //        {
        //            oStream.WriteLine("-----------------------------------------------------");
        //            oStream.WriteLine(string.Format("Zarinpal QueryString: {0} ", zarinpalQueryString));
        //            oStream.WriteLine(string.Format("DateTime: {0} ", DateTime.Now));
        //            //if (UserID > 0)
        //            //{
        //            //    oStream.WriteLine(string.Format("UserID: {0} ", UserID.ToString()));
        //            //}
        //            oStream.WriteLine("-----------------------------------------------------");
        //            oStream.WriteLine("\n");

        //            oStream.Close();
        //            oStream.Dispose();
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //#endregion /Error Handling

        #region Images

        // Resize an Image File   
        public static Bitmap ResizeImage(Stream fileStream, int width, int height)
        {
            return ResizeImage(new Bitmap(fileStream), width, height);
        }

        // Resize an Image File with preserve aspect ratio
        public static Bitmap ResizeRatioImage(Stream fileStream, int maxWidth, int maxHeight)
        {
            return ResizeRatioImage(new Bitmap(fileStream), maxWidth, maxHeight);
        }

        // Resize a Bitmap   
        public static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                                     new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            image.Dispose();
            return resizedImage;
        }

        // Resize a Bitmap with preserve aspect ratio   
        public static Bitmap ResizeRatioImage(Bitmap image, int maxWidth, int maxHeight)
        {
            int newWidth = maxWidth;
            int newHeight = maxHeight;
            if ((decimal)image.Width / (decimal)maxWidth > (decimal)image.Height / (decimal)maxHeight)
            {
                newWidth = maxWidth;
                newHeight = (int)Math.Round((maxWidth / (double)image.Width) * image.Height);
            }
            else
            {
                newHeight = maxHeight;
                newWidth = (int)Math.Round((maxHeight / (double)image.Height) * image.Width);
            }

            Bitmap resizedImage = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight),
                                     new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            image.Dispose();
            return resizedImage;
        }

        #endregion /Images

        #region Excel

        //public static string GetExcelFilePath(string fileName)
        //{
        //    return Utility.GetDirectoryPhysicalPath(Utility.GetAppSetting(Constant.AppSetting_ExcelFilePath)) + fileName;
        //}

        //public static string GetDirectoryPhysicalPath(string directoryRelativePath)
        //{
        //    string directoryPhysicalPath = MyWebRequest.WebRootPath + directoryRelativePath);
        //    //string directoryPhysicalPath = AppDomain.CurrentDomain.BaseDirectory + directoryRelativePath;
        //    if (!Directory.Exists(directoryPhysicalPath))
        //    {
        //        Directory.CreateDirectory(directoryPhysicalPath);
        //    }
        //    return directoryPhysicalPath;
        //}

        #endregion /Excel

    }
}
