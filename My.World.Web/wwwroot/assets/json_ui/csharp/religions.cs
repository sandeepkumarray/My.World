// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var religions = Religions.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Religions
    {
        [JsonProperty("beliefs")]
        public Beliefs Beliefs { get; set; }

        [JsonProperty("gallery")]
        public Gallery Gallery { get; set; }

        [JsonProperty("history")]
        public Beliefs History { get; set; }

        [JsonProperty("notes")]
        public Notes Notes { get; set; }

        [JsonProperty("overview")]
        public Beliefs Overview { get; set; }

        [JsonProperty("spread")]
        public Beliefs Spread { get; set; }

        [JsonProperty("traditions")]
        public Traditions Traditions { get; set; }
    }

    public partial class Beliefs
    {
        [JsonProperty("attributes")]
        public BeliefsAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class BeliefsAttribute
    {
        [JsonProperty("field_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FieldType { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Gallery
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class Notes
    {
        [JsonProperty("attributes")]
        public NotesAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class NotesAttribute
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class Traditions
    {
        [JsonProperty("attributes")]
        public TraditionsAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class TraditionsAttribute
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Religions
    {
        public static Religions FromJson(string json) => JsonConvert.DeserializeObject<Religions>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Religions self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
