using System.Globalization;

namespace Learn.Language
{
    public interface ILanguage
    {
        CultureInfo GetResource(string _lang);
    }
}