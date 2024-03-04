using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMS_API.ModelABM;

namespace WMS_API
{
    public static class ExtensionSQL
    {
        public static string CheckOnError(this string json)
        {
            if (json == null) return null;
            Response result;
            try
            {
                result = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            if (result.Success)
                return null;
            else
                return $"Code: {result.Code}; Error: {result.Description}";
        }

        public static T Deserialize<T>(this string json, out string error) where T : class
        {
            error = "";
            if (json == null) return null;
            T result = null;
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
            return result;
        }

        public static string PackingCode(int codetvun, int idClassifierPackage)
        {
            var result = codetvun.ToString("0000000") + "_" + idClassifierPackage.ToString("000");
            return result;
        }

        public static string PackingCode(string codetvun, int idClassifierPackage)
        {
            var result = ("0000000" + codetvun).Substring(codetvun.Length, 7) + "_" + idClassifierPackage.ToString("000");
            return result;
        }

        public static string Ekran(this string data)
        {
            var result = Regex.Replace(data, @"'", @"''");
            return result;
        }

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
