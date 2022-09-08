using System;

namespace SampaioDias.DamageMeter.Utility
{
    /// <summary>
    /// Transforms damage numbers into more readable strings. For example, 123456 becomes "123,4K" by default.
    /// </summary>
    public static class DamageStringFormat
    {
        public static string Format(int value, DamageNumberOptions options)
        {
            return Format((double) value, options);
        }

        public static string Format(double value, DamageNumberOptions options)
        {
            if (value < options.initialValueForFormat)
            {
                return value.ToString(options.toStringFormatter);
            }

            if (value >= 1000 && value < 1000000)
            {
                return $"{((float) value / 1000).ToString(options.toStringFormatter)}{options.thousandSymbol}";
            }

            return $"{((float) value / 1000000).ToString(options.toStringFormatter)}{options.millionSymbol}";
        }

        [Serializable]
        public struct DamageNumberOptions
        {
            /// <summary>
            /// Minimum value to start shortening strings.
            /// </summary>
            public double initialValueForFormat;
            public string thousandSymbol;
            public string millionSymbol;
            /// <summary>
            /// e.g.: "n2" shows 2 decimal places. https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
            /// </summary>
            public string toStringFormatter;

            public string textLayout;

            public DamageNumberOptions(double initialValueForFormat, string thousandSymbol, string millionSymbol, 
                string toStringFormatter, string textLayout)
            {
                this.initialValueForFormat = initialValueForFormat;
                this.thousandSymbol = thousandSymbol;
                this.millionSymbol = millionSymbol;
                this.toStringFormatter = toStringFormatter;
                this.textLayout = textLayout;
            }
        }
    }
}