using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace My.World.Api.Models
{
	public class UserDetailsModel
	{
		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("last_seen_at")]
		[DisplayName("Last Seen At")]
		public DateTime last_seen_at { get; set; }

		[JsonProperty("latest_activity_at")]
		[DisplayName("Latest Activity At")]
		public DateTime latest_activity_at { get; set; }

		[JsonProperty("moderation_state")]
		[DisplayName("Moderation State")]
		public Int32 moderation_state { get; set; }

		[JsonProperty("moderation_state_changed_at")]
		[DisplayName("Moderation State Changed At")]
		public DateTime moderation_state_changed_at { get; set; }

		[JsonProperty("posts_count")]
		[DisplayName("Posts Count")]
		public Int32 posts_count { get; set; }

		[JsonProperty("topics_count")]
		[DisplayName("Topics Count")]
		public Int32 topics_count { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int32 user_id { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		public UserDetailsModel()
		{
		}

	}
}
