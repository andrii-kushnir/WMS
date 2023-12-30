using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API
{
    public static class ExtensionSQL
    {
        public static string DateToSQL(this DateTime date)
        {
            var dateBegin = new DateTime(1970, 1, 1);
            date = date < dateBegin ? dateBegin : date;
            var result = date.ToString("s");
            return result;
        }

        public static string DateToSQL(this DateTimeOffset date)
        {
            var dateBegin = new DateTimeOffset(new DateTime(1970, 1, 1));
            date = date < dateBegin ? dateBegin : date;
            var result = date.ToString("s");
            return result;
        }

        public static string NullCheck(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }

    }
}
