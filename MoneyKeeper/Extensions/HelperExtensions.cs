using MoneyKeeper.Error;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MoneyKeeper.Extensions
{
    public static class HeplerExtensions
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return System.Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }
    }
}
