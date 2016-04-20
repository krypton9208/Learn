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
        public CultureInfo GetResource(string _lang)
        {
            return CultureInfo.CreateSpecificCulture(_lang);
        }

    }
}
