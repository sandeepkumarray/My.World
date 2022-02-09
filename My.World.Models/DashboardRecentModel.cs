using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace My.World.Api.Models
{
    public class DashboardRecentModel : MentionsModel
    {
        [JsonProperty("user_id")]
        [DisplayName("User Id")]
        public Int32 user_id { get; set; }

        [JsonProperty("updated_at")]
        [DisplayName("Updated At")]
        public DateTime updated_at { get; set; }

        [JsonProperty("icon")]
        [DisplayName("Icon")]
        public string icon { get; set; }

        [JsonProperty("primary_color")]
        [DisplayName("Primary Color")]
        public String primary_color { get; set; }

    }
}
