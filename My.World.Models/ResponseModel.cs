using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace My.World.Api.Models
{
    public partial class ResponseModel<T>
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("reason")]
        public List<string> Reason { get; set; }

        [JsonProperty("issuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("httpstatuscode")]
        public string HttpStatusCode { get; set; }
        [JsonProperty("value")]
        public T Value { get; set; }
        public ResponseModel()
        {
            Reason = new List<string>();
        }

    }

    public static partial class Serialize
    {
        public static string ToJson<T>(this T self) { return JsonConvert.SerializeObject(self, Converter.Settings); }
        public static T FromJson<T>(string json) { return JsonConvert.DeserializeObject<T>(json, Converter.Settings); }
        public static string convertByteArrayToString(this byte[] input)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(input);
        }

    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}