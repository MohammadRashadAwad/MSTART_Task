namespace MSTART_Task.Models
{

    public struct Currency
    {
        public string Country { get; set; }
        public string CurrencyName { get; set; }
        public string Code { get; set; }

        public static readonly List<Currency> AvailableCurrencies = new List<Currency>
        {
             new Currency{Country ="JORDAN" ,CurrencyName="Jordanian Dinar", Code="JOD"},
             new Currency{Country ="IRAQ" ,CurrencyName="Iraqi Dinar", Code="IQD"},
             new Currency{Country ="UNITED STATES OF AMERICA" ,CurrencyName="US Dollar", Code="USN"},
        };
    }


}

