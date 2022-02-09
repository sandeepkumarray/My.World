using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class TownsModel : BaseModel
	{
		[JsonProperty("buildings")]
		[DisplayName("Buildings")]
		public Int32 Buildings { get; set; }

		[JsonProperty("busy_areas")]
		[DisplayName("Busy Areas")]
		public String Busy_areas { get; set; }

		[JsonProperty("citizens")]
		[DisplayName("Citizens")]
		public Double Citizens { get; set; }

		[JsonProperty("country")]
		[DisplayName("Country")]
		public Int32 Country { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("energy_sources")]
		[DisplayName("Energy Sources")]
		public String Energy_sources { get; set; }

		[JsonProperty("established_year")]
		[DisplayName("Established Year")]
		public Int32 Established_year { get; set; }

		[JsonProperty("flora")]
		[DisplayName("Flora")]
		public String Flora { get; set; }

		[JsonProperty("food_sources")]
		[DisplayName("Food Sources")]
		public String Food_sources { get; set; }

		[JsonProperty("founding_story")]
		[DisplayName("Founding Story")]
		public String Founding_story { get; set; }

		[JsonProperty("groups")]
		[DisplayName("Groups")]
		public String Groups { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("landmarks")]
		[DisplayName("Landmarks")]
		public String Landmarks { get; set; }

		[JsonProperty("languages")]
		[DisplayName("Languages")]
		public String Languages { get; set; }

		[JsonProperty("laws")]
		[DisplayName("Laws")]
		public String Laws { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("neighborhoods")]
		[DisplayName("Neighborhoods")]
		public Int32 Neighborhoods { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_names { get; set; }

		[JsonProperty("politics")]
		[DisplayName("Politics")]
		public String Politics { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("recycling")]
		[DisplayName("Recycling")]
		public String Recycling { get; set; }

		[JsonProperty("sports")]
		[DisplayName("Sports")]
		public String Sports { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("waste")]
		[DisplayName("Waste")]
		public String Waste { get; set; }


		public TownsModel()
		{
		}

	}
}
