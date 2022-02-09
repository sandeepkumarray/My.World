using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class LandmarksModel : BaseModel
	{
		[JsonProperty("colors")]
		[DisplayName("Colors")]
		public String Colors { get; set; }

		[JsonProperty("country")]
		[DisplayName("Country")]
		public String Country { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creation_story")]
		[DisplayName("Creation Story")]
		public String Creation_story { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("established_year")]
		[DisplayName("Established Year")]
		public Int32 Established_year { get; set; }

		[JsonProperty("flora")]
		[DisplayName("Flora")]
		public String Flora { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("materials")]
		[DisplayName("Materials")]
		public String Materials { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("nearby_towns")]
		[DisplayName("Nearby Towns")]
		public String Nearby_towns { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public Double Size { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("type_of_landmark")]
		[DisplayName("Type Of Landmark")]
		public String Type_of_landmark { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public LandmarksModel()
		{
		}

	}
}
