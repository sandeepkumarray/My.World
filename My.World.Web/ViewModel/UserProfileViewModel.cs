using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Web.ViewModel
{
    public class UserProfileViewModel
    {
        [JsonProperty("bio")]
        [DisplayName("Bio")]
        public String bio { get; set; }

        [JsonProperty("community_features_enabled")]
        [DisplayName("Community Features Enabled")]
        public Boolean community_features_enabled { get; set; }

        [JsonProperty("dark_mode_enabled")]
        [DisplayName("Dark Mode Enabled")]
        public Boolean dark_mode_enabled { get; set; }

        [JsonProperty("forums_badge_text")]
        [DisplayName("Forums Badge Text")]
        public String forums_badge_text { get; set; }

        [JsonProperty("inspirations")]
        [DisplayName("Inspirations")]
        public String inspirations { get; set; }

        [JsonProperty("interests")]
        [DisplayName("Interests")]
        public String interests { get; set; }

        [JsonProperty("private_profile")]
        [DisplayName("Private Profile")]
        public Boolean private_profile { get; set; }

        [JsonProperty("website")]
        [DisplayName("Website")]
        public String website { get; set; }

        [JsonProperty("username")]
        [DisplayName("Username")]
        public String username { get; set; }
    }
}
