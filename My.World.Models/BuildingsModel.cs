using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class BuildingsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("type_of_building")]
		[DisplayName("Type Of Building")]
		public String Type_of_building { get; set; }

		[JsonProperty("alternate_names")]
		[DisplayName("Alternate Names")]
		public String Alternate_names { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("capacity")]
		[DisplayName("Capacity")]
		public Int64 Capacity { get; set; }

		[JsonProperty("price")]
		[DisplayName("Price")]
		public Double Price { get; set; }

		[JsonProperty("owner")]
		[DisplayName("Owner")]
		public String Owner { get; set; }

		[JsonProperty("tenants")]
		[DisplayName("Tenants")]
		public String Tenants { get; set; }

		[JsonProperty("affiliation")]
		[DisplayName("Affiliation")]
		public String Affiliation { get; set; }

		[JsonProperty("facade")]
		[DisplayName("Facade")]
		public String Facade { get; set; }

		[JsonProperty("floor_count")]
		[DisplayName("Floor Count")]
		public Int64 Floor_count { get; set; }

		[JsonProperty("dimensions")]
		[DisplayName("Dimensions")]
		public Int64 Dimensions { get; set; }

		[JsonProperty("architectural_style")]
		[DisplayName("Architectural Style")]
		public String Architectural_style { get; set; }

		[JsonProperty("permits")]
		[DisplayName("Permits")]
		public String Permits { get; set; }

		[JsonProperty("purpose")]
		[DisplayName("Purpose")]
		public String Purpose { get; set; }

		[JsonProperty("address")]
		[DisplayName("Address")]
		public String Address { get; set; }

		[JsonProperty("architect")]
		[DisplayName("Architect")]
		public String Architect { get; set; }

		[JsonProperty("developer")]
		[DisplayName("Developer")]
		public String Developer { get; set; }

		[JsonProperty("notable_events")]
		[DisplayName("Notable Events")]
		public String Notable_events { get; set; }

		[JsonProperty("constructed_year")]
		[DisplayName("Constructed Year")]
		public Int64 Constructed_year { get; set; }

		[JsonProperty("construction_cost")]
		[DisplayName("Construction Cost")]
		public Double Construction_cost { get; set; }

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


		public BuildingsModel()
		{
		}

	}
}
