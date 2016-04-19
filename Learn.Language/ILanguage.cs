using System.Globalization;
using System.Web;

namespace Learn.Language
{
    public interface ILanguage
    {
        CultureInfo GetResource(string _lang);
        string GetResourceLogout(string l);
    }
}