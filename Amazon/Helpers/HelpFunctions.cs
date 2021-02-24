using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Amazon.Helpers
{
    class HelpFunctions
    {        
        public static decimal Transform(string text)
        {
            NumberStyles style;
            CultureInfo culture;
            decimal result;
            //set cultures for a correct price comparison
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture("es-MX");
            //adjust the price text in case it is composed by multiple lines
            text = Regex.Replace(text, "\r\n", ".");
            //text = text.Split(".",);
            ;
            if (!Decimal.TryParse(text, style, culture, out result))
            {
                Assert.Fail("No price was found");
            }
            return result;
        }
    }
}
