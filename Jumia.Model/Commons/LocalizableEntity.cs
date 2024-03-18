using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model.Commons
{
    public class LocalizableEntity : BaseEntity
    {
        public string GetLocalized(string textAr, string text)
        {
            CultureInfo lang = Thread.CurrentThread.CurrentCulture;
            if (lang.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;
            return text;
        }
        /*public string GetLocalized(string textAr, string text)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            Console.WriteLine("Current culture: " + culture.Name);
            if (culture.TwoLetterISOLanguageName.ToLower() == "ar")
                return textAr;
            return text;
        }*/
    }
}
