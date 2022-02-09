using My.World.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Models
{
    public class SiteTemplateModel
    {
        [JsonProperty("TemplateName")]
        public string TemplateName { get; set; }
        [JsonProperty("PlanContentList")]
        public List<ContentTypesModel> PlanContentList { get; set; }

        public SiteTemplateModel(string templateName)
        {
            TemplateName = templateName;
        }
    }

    public class DashboardContentOrder
    {
        [JsonProperty("ContentType")]
        public ContentTypesModel ContentType { get; set; }

        [JsonProperty("OrderId")]
        public int OrderId { get; set; }
    }
}
