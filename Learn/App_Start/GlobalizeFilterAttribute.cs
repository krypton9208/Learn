using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Learn.Language;
using System.Globalization;
using System.Threading;
using System.Web;

namespace Learn.App_Start
{
    class GlobalizeFilterAttribute : ActionFilterAttribute
    {
        public static string CookieLangEntry { get; private set; }
        public static string CookieName { get; private set; }
        public static String GetSavedCultureOrDefault(HttpRequestBase httpRequestBase)
        {
            var culture = "";
            var cookie = httpRequestBase.Cookies[CookieName];
            if (cookie != null)
                culture = cookie.Values[CookieLangEntry];
            return culture;
        }


        //public GlobalizeFilterAttribute(ILanguage _Lang)
        //{
        //    Lang = _Lang;
        //}

        public static void SavePreferredCulture(HttpResponseBase response, string language, int expireDays = 1)
        {
            var cookie = new HttpCookie(CookieName)
            {
                Expires = DateTime.Now.AddDays(expireDays)
            };
            cookie.Values[CookieLangEntry] = language;
            response.Cookies.Add(cookie);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CookieName = "LearnCookie";
            var language = GetSavedCultureOrDefault(filterContext.HttpContext.Request);

            if (!string.IsNullOrWhiteSpace(language))
            {
                var cultureInfo =  new Language.Language().GetResource(language);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
