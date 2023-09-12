namespace AP.ChevronCoop.Commons
{
    public static class NumberUtils
    {

        public static double ToDouble(this decimal number)
        {
            return (double)number;
        }

        public static decimal ToDecimal(this double number)
        {
            return (decimal)number;
        }


        public static double ToDouble(this int number)
        {
            return (double)number;
        }

        public static decimal ToDecimal(this int number)
        {
            return (decimal)number;
        }
    }
}

