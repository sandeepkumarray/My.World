// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var conditions = Conditions.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Conditions
    {
        [JsonProperty("analysis")]
        public Analysis Analysis { get; set; }

        [JsonProperty("causes")]
        public Analysis Causes { get; set; }

        [JsonProperty("effects")]
        public Analysis Effects { get; set; }

        [JsonProperty("gallery")]
        public Gallery Gallery { get; set; }

        [JsonProperty("history")]
        public Analysis History { get; set; }

        [JsonProperty("notes")]
        public Notes Notes { get; set; }

        [JsonProperty("overview")]
        public Overview Overview { get; set; }

        [JsonProperty("treatment")]
        public Analysis Treatment { get; set; }
    }

    public partial class Analysis
    {
        [JsonProperty("attributes")]
        public AnalysisAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class AnalysisAttribute
    {
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

    public partial class Overview
    {
        [JsonProperty("attributes")]
        public OverviewAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class OverviewAttribute
    {
        [JsonProperty("field_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FieldType { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Conditions
    {
        public static Conditions FromJson(string json) => JsonConvert.DeserializeObject<Conditions>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Conditions self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
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
