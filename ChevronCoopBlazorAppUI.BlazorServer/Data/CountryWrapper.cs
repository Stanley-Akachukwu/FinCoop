using CountryData;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CountryWrapper : ICountryInfo
    {
        public string Name { get; set; }
        public string DialingCode { get; set; }

        public string Iso { get; set; }

        public string Iso3 { get; set; }

        public ushort IsoNumeric { get; set; }

        public Fips? Fips { get; set; }

        public string Capital { get; set; }

        public double? Area { get; set; }

        public uint Population { get; set; }

        public string Continent { get; set; }

        public string TopLevelDomain { get; set; }

        public CurrencyCode? CurrencyCode { get; set; }

        public string CurrencyName { get; set; }

        public string PhonePrefix { get; set; }

        public string PostCodeFormat { get; set; }

        public string PostCodeRegex { get; set; }

        public IReadOnlyList<string> Languages { get; set; }
    }
}
