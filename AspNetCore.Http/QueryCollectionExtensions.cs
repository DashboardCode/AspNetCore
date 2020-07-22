using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace DashboardCode.AspNetCore
{
    public static class QueryCollectionExtensions
    {
        public static string GetString(this IQueryCollection formCollection, string pairName)
        {
            var @value = default(string);
            if (formCollection.TryGetValue(pairName, out var stringValues))
                if (stringValues.Count > 0)
                    @value = stringValues.ToString();
            return @value;
        }

        public static int? GetNInt(this IQueryCollection formCollection, string pairName)
        {
            var @value = default(int?);
            if (formCollection.TryGetValue(pairName, out var stringValues))
                if (stringValues.Count > 0 && int.TryParse(stringValues.ToString(), out var intValue))
                    @value = intValue;
            return @value;
        }

        public static DateTime? GetNDate(this IQueryCollection formCollection, string pairName, string format)
        {
            var @value = default(DateTime?);
            if (formCollection.TryGetValue(pairName, out var stringValues))
                if (stringValues.Count > 0 && 
                    DateTime.TryParseExact(
                        stringValues.ToString(), format, 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeValue))
                    @value = dateTimeValue;
            return @value;
        }
    }
}