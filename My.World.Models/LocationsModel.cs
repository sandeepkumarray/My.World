using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class LocationsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("type")]
		[DisplayName("Type")]
		public String Type { get; set; }

		[JsonProperty("leaders")]
		[DisplayName("Leaders")]
		public String Leaders { get; set; }

		[JsonProperty("language")]
		[DisplayName("Language")]
		public String Language { get; set; }

		[JsonProperty("population")]
		[DisplayName("Population")]
		public Double Population { get; set; }

		[JsonProperty("currency")]
		[DisplayName("Currency")]
		public String Currency { get; set; }

		[JsonProperty("motto")]
		[DisplayName("Motto")]
		public String Motto { get; set; }

		[JsonProperty("sports")]
		[DisplayName("Sports")]
		public String Sports { get; set; }

		[JsonProperty("laws")]
		[DisplayName("Laws")]
		public String Laws { get; set; }

		[JsonProperty("spoken_languages")]
		[DisplayName("Spoken Languages")]
		public String Spoken_Languages { get; set; }

		[JsonProperty("largest_cities")]
		[DisplayName("Largest Cities")]
		public String Largest_cities { get; set; }

		[JsonProperty("notable_cities")]
		[DisplayName("Notable Cities")]
		public String Notable_cities { get; set; }

		[JsonProperty("capital_cities")]
		[DisplayName("Capital Cities")]
		public String Capital_cities { get; set; }

		[JsonProperty("landmarks")]
		[DisplayName("Landmarks")]
		public String Landmarks { get; set; }

		[JsonProperty("area")]
		[DisplayName("Area")]
		public Double Area { get; set; }

		[JsonProperty("crops")]
		[DisplayName("Crops")]
		public String Crops { get; set; }

		[JsonProperty("located_at")]
		[DisplayName("Located At")]
		public String Located_at { get; set; }

		[JsonProperty("climate")]
		[DisplayName("Climate")]
		public String Climate { get; set; }

		[JsonProperty("notable_wars")]
		[DisplayName("Notable Wars")]
		public String Notable_Wars { get; set; }

		[JsonProperty("founding_story")]
		[DisplayName("Founding Story")]
		public String Founding_Story { get; set; }

		[JsonProperty("established_year")]
		[DisplayName("Established Year")]
		public Int64 Established_Year { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public LocationsModel()
		{
		}

	}
}
