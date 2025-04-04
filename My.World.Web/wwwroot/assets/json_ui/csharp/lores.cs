// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var lores = Lores.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Lores
    {
        [JsonProperty("about")]
        public About About { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("culture")]
        public About Culture { get; set; }

        [JsonProperty("gallery")]
        public Gallery Gallery { get; set; }

        [JsonProperty("history")]
        public Content History { get; set; }

        [JsonProperty("notes")]
        public Notes Notes { get; set; }

        [JsonProperty("origin")]
        public About Origin { get; set; }

        [JsonProperty("overview")]
        public About Overview { get; set; }

        [JsonProperty("setting")]
        public About Setting { get; set; }

        [JsonProperty("truthiness")]
        public About Truthiness { get; set; }

        [JsonProperty("variations")]
        public About Variations { get; set; }
    }

    public partial class About
    {
        [JsonProperty("attributes")]
        public AboutAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class AboutAttribute
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("field_type", NullValueHandling = NullValueHandling.Ignore)]
        public FieldType? FieldType { get; set; }
    }

    public partial class Content
    {
        [JsonProperty("attributes")]
        public ContentAttribute[] Attributes { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class ContentAttribute
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

    public enum FieldType { Link, Name, Tags, Universe };

    public partial class Lores
    {
        public static Lores FromJson(string json) => JsonConvert.DeserializeObject<Lores>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Lores self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                FieldTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class FieldTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FieldType) || t == typeof(FieldType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "link":
                    return FieldType.Link;
                case "name":
                    return FieldType.Name;
                case "tags":
                    return FieldType.Tags;
                case "universe":
                    return FieldType.Universe;
            }
            throw new Exception("Cannot unmarshal type FieldType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FieldType)untypedValue;
            switch (value)
            {
                case FieldType.Link:
                    serializer.Serialize(writer, "link");
                    return;
                case FieldType.Name:
                    serializer.Serialize(writer, "name");
                    return;
                case FieldType.Tags:
                    serializer.Serialize(writer, "tags");
                    return;
                case FieldType.Universe:
                    serializer.Serialize(writer, "universe");
                    return;
            }
            throw new Exception("Cannot marshal type FieldType");
        }

        public static readonly FieldTypeConverter Singleton = new FieldTypeConverter();
    }
}
