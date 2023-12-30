using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WMS_API
{
    public static class RequestData
    {
        public static string SendPost(string url, string username, string password, string json, out string error)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);
            //request.PreAuthenticate = true;
            request.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                streamWriter.Write(json);

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
            var result = ParseResponse(response, out error);
            response.Close();
            return result;
        }

        private static string ParseResponse(HttpWebResponse response, out string error)
        {
            error = "";
            string responseBody = null;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                error += response.StatusCode.ToString();
            }

            try
            {
                using (Stream inputStream = response.GetResponseStream())
                {
                    if (inputStream != null)
                    {
                        responseBody = new StreamReader(inputStream, Encoding.UTF8).ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                error += " " + ex.Message;
                return responseBody;
            }
            return responseBody;
        }
    }
}
