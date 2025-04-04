// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var continents = Continents.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Continents
    {
        [JsonProperty("climate")]
        public Climate Climate { get; set; }

        [JsonProperty("culture")]
        public Culture Culture { get; set; }

        [JsonProperty("gallery")]
        public Gallery Gallery { get; set; }

        [JsonProperty("geography")]
        public Culture Geography { get; set; }

        [JsonProperty("history")]
        public Climate History { get; set; }

        [JsonProperty("nature")]
        public Culture Nature { get; set; }

        [JsonProperty("notes")]
        public Notes Notes { get; set; }

        [JsonProperty("overview")]
        public Culture Overview { get; set; }
    }

    public partial class Climate
    {
        [JsonProperty("attributes")]
        public ClimateAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class ClimateAttribute
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Culture
    {
        [JsonProperty("attributes")]
        public CultureAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class CultureAttribute
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("field_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FieldType { get; set; }
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

    public partial class Continents
    {
        public static Continents FromJson(string json) => JsonConvert.DeserializeObject<Continents>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Continents self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
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
