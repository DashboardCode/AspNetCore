using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace DashboardCode.AspNetCore
{
    public static class FormCollectionExtensions
    {
        public static DateTime? GetNDate(this IFormCollection formCollection, string pairName, string format) // "MM/dd/yyyy"
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
