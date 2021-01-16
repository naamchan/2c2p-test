using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _2c2p_test.Common
{
    public static class ISO4217CurrencySymbol
    {
        private static HashSet<string> currencySymbols = new HashSet<string>();
        static ISO4217CurrencySymbol()
        {
            currencySymbols = new HashSet<string>(CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select((x) => new RegionInfo(x.LCID).ISOCurrencySymbol)
                .Distinct()
            );
        }

        public static bool Validate(string currencyCode)
        {
            return currencySymbols.Contains(currencyCode);
        }
    }
}