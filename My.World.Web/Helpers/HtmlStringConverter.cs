using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web
{
    public class HtmlStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IHtmlContent).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            // Specifically MvcHtmlString
           
            // Generally HtmlString
            if (objectType == typeof(HtmlString))
            {
                return new HtmlString(value);
            }

            // Fallback for other (future?) implementations of IHtmlString
            return Activator.CreateInstance(objectType, value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var htmlString = value as HtmlString;
            if (htmlString == null)
            {
                return;
            }

            writer.WriteValue(htmlString.ToString());
        }
    }
}
