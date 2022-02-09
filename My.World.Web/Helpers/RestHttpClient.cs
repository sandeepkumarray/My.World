using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace My.World.Web
{
    public class RestHttpClient
    {
        public string Host = string.Empty;
        public string ApiUrl = string.Empty;
        public NameValueCollection Parameters = null;
        public object RequestBody = null;
        public string ContentType = string.Empty;
        public Method ServiceMethod = Method.POST;
        public Stream UploadeFileStream = null;
        public FileInfo UploadeFileInfo = null;
        public CookieContainer cookieContainer;

        public const string ContentTypeJson = "application/json";
        public const string ContentTypeXmI = " application/xml";
        public const string ContentTypeUrlEncode = " application/ form-urlencoded";
        public const string ContentTypeMultipart = "multipart/form-data; ";
        public const string ContentTypeText = "text/plain";
        public const string ContentTypeTextHtml = "text/html";
        public const string ContentTypeTextXml = "text/xml";

        public RestHttpClient()
        {
            Initialize();
        }

        public void Initialize()
        {
            Host = string.Empty;
            ApiUrl = string.Empty;
            Parameters = null;
            RequestBody = null;
            ContentType = ContentTypeJson;
            ServiceMethod = Method.POST;
        }

        public string GETAsync()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method
            HttpResponseMessage response = client.GetAsync(ApiUrl).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public string POSTAsync()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(JsonConvert.SerializeObject(RequestBody), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(ApiUrl, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public string GetResponseAsync()
        {
            HttpResponseMessage response = null;
            HttpContent httpcontent = null;
            if (RequestBody != null)
            {
                if (ContentType == ContentTypeJson)
                {
                    string JsonString = JsonConvert.SerializeObject(RequestBody);
                    httpcontent = new StringContent(JsonString, Encoding.UTF8, ContentType);
                }
                //else if (ContentType == ContentTypeXml)
                //{
                //    string XmlString = XmlUtility.ToXml(RequestBody);
                //    httpcontent = new StringContent(xmlstring, Encoding.UTF8, ContentType);
                //}
                else if (ContentType == ContentTypeMultipart)
                {
                    MultipartFormDataContent multiPartContent = new MultipartFormDataContent("httpClientBoundary----" +
                    DateTime.Now.ToString(CultureInfo.InvariantCulture));

                    multiPartContent.Add(new StreamContent((Stream)RequestBody), "file", "file.ext");
                    httpcontent = multiPartContent;
                    if (Parameters != null && Parameters.Count > 0)
                    {
                        var parameterList = Parameters.AllKeys.SelectMany(Parameters.GetValues, (k, v) => k + "=" + v);
                        ApiUrl = ApiUrl + "?" + string.Join("&", parameterList);
                    }
                }

                if (UploadeFileInfo != null)
                    httpcontent = new StreamContent(UploadeFileInfo.OpenRead());

                if (UploadeFileStream != null)
                    httpcontent = new StreamContent(UploadeFileStream);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, ss1p01icyErrors) =>
                    {
                        return true;
                    };

                    client.DefaultRequestHeaders.Add("cache-control", "no-cache");

                    if (ServiceMethod == Method.POST)
                        response = client.PostAsync(ApiUrl, httpcontent).Result;
                    if (ServiceMethod == Method.GET)
                        response = client.GetAsync(ApiUrl).Result;
                    if (ServiceMethod == Method.PUT)
                        response = client.PutAsync(ApiUrl, httpcontent).Result;
                    if (ServiceMethod == Method.DELETE)
                        response = client.DeleteAsync(ApiUrl).Result;
                }
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }
    public enum Method
    {
        GET = 0,
        HEAD = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4,
        CONNECT = 5,
        OPTIONS = 6,
        PATCH = 7
    }
}