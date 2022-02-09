using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Web.ViewModel
{
    public class UserAccountViewModel
    {
        [JsonProperty("age")]
        [DisplayName("Age")]
        public String age { get; set; }

        [JsonProperty("email")]
        [DisplayName("Email")]
        public String email { get; set; }

        [JsonProperty("email_updates")]
        [DisplayName("Email Updates")]
        public Boolean email_updates { get; set; }

		[JsonProperty("favorite_author")]
		[DisplayName("Favorite Author")]
		public String favorite_author { get; set; }

		[JsonProperty("favorite_book")]
		[DisplayName("Favorite Book")]
		public String favorite_book { get; set; }

		[JsonProperty("favorite_genre")]
		[DisplayName("Favorite Genre")]
		public String favorite_genre { get; set; }

		[JsonProperty("favorite_page_type")]
		[DisplayName("Favorite Page Type")]
		public String favorite_page_type { get; set; }

		[JsonProperty("favorite_quote")]
		[DisplayName("Favorite Quote")]
		public String favorite_quote { get; set; }

		[JsonProperty("fluid_preference")]
		[DisplayName("Fluid Preference")]
		public Boolean fluid_preference { get; set; }

		[JsonProperty("forum_administrator")]
		[DisplayName("Forum Administrator")]
		public Boolean forum_administrator { get; set; }

		[JsonProperty("forum_moderator")]
		[DisplayName("Forum Moderator")]
		public Boolean forum_moderator { get; set; }

		[JsonProperty("forums_badge_text")]
		[DisplayName("Forums Badge Text")]
		public String forums_badge_text { get; set; }

		[JsonProperty("gender")]
		[DisplayName("Gender")]
		public String gender { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("inspirations")]
		[DisplayName("Inspirations")]
		public String inspirations { get; set; }

		[JsonProperty("interests")]
		[DisplayName("Interests")]
		public String interests { get; set; }

		[JsonProperty("location")]
		[DisplayName("Location")]
		public String location { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("notification_updates")]
		[DisplayName("Notification Updates")]
		public Boolean notification_updates { get; set; }

		[JsonProperty("occupation")]
		[DisplayName("Occupation")]
		public String occupation { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String other_names { get; set; }

	}
}
