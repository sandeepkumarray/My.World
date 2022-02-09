using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace My.World.Api.Models
{
	public class UniversesModel : BaseModel
	{
		[JsonProperty("archived_at")]
		[DisplayName("Archived At")]
		public DateTime archived_at { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("deleted_at")]
		[DisplayName("Deleted At")]
		public DateTime deleted_at { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public string description { get; set; }

		[JsonProperty("favorite")]
		[DisplayName("Favorite")]
		public Boolean favorite { get; set; }

		[JsonProperty("genre")]
		[DisplayName("Genre")]
		public String genre { get; set; }

		[JsonProperty("history")]
		[DisplayName("History")]
		public string history { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("laws_of_physics")]
		[DisplayName("Laws Of Physics")]
		public String laws_of_physics { get; set; }

		[JsonProperty("magic_system")]
		[DisplayName("Magic System")]
		public String magic_system { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public string notes { get; set; }

		[JsonProperty("page_type")]
		[DisplayName("Page Type")]
		public String page_type { get; set; }

		[JsonProperty("privacy")]
		[DisplayName("Privacy")]
		public Boolean privacy { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String private_notes { get; set; }

		[JsonProperty("technology")]
		[DisplayName("Technology")]
		public String technology { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public UniversesModel()
		{
		}

	}
}
