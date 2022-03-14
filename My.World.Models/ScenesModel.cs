using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ScenesModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("summary")]
		[DisplayName("Summary")]
		public String Summary { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("items_in_scene")]
		[DisplayName("Items In Scene")]
		public String Items_in_scene { get; set; }

		[JsonProperty("locations_in_scene")]
		[DisplayName("Locations In Scene")]
		public String Locations_in_scene { get; set; }

		[JsonProperty("characters_in_scene")]
		[DisplayName("Characters In Scene")]
		public String Characters_in_scene { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("results")]
		[DisplayName("Results")]
		public String Results { get; set; }

		[JsonProperty("what_caused_this")]
		[DisplayName("What Caused This")]
		public String What_caused_this { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_notes { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public ScenesModel()
		{
		}

	}
}
