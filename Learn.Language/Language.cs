using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.Language
{
    public class Language : ILanguage
    {
        private string lang;
        public CultureInfo GetResource(string _lang)
        {
            lang = _lang;
            return CultureInfo.CreateSpecificCulture(_lang);
        }

        public string GetResourceLogout(string l)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(l);
            l = lang;
            return Resource.Logout;
        }

    }
}
