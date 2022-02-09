using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace My.World.Api.Models
{
	public class DocumentsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("body")]
		[DisplayName("Body")]
		public String body { get; set; }

		[JsonProperty("cached_word_count")]
		[DisplayName("Cached Word Count")]
		public Int64 cached_word_count { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("deleted_at")]
		[DisplayName("Deleted At")]
		public DateTime deleted_at { get; set; }

		[JsonProperty("favorite")]
		[DisplayName("Favorite")]
		public Boolean favorite { get; set; }

		[JsonProperty("folder_id")]
		[DisplayName("Folder Id")]
		public Int64 folder_id { get; set; }

		[JsonProperty("notes_text")]
		[DisplayName("Notes Text")]
		public String notes_text { get; set; }

		[JsonProperty("privacy")]
		[DisplayName("Privacy")]
		public Boolean privacy { get; set; }

		[JsonProperty("synopsis")]
		[DisplayName("Synopsis")]
		public String synopsis { get; set; }

		[JsonProperty("title")]
		[DisplayName("Title")]
		public String title { get; set; }

		[JsonProperty("universe_id")]
		[DisplayName("Universe Id")]
		public Int64 universe_id { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		public DocumentsModel()
		{
		}

	}
}
