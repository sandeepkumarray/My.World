using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class VehiclesModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("type_of_vehicle")]
		[DisplayName("Type Of Vehicle")]
		public String Type_of_vehicle { get; set; }

		[JsonProperty("alternate_names")]
		[DisplayName("Alternate Names")]
		public String Alternate_names { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("dimensions")]
		[DisplayName("Dimensions")]
		public Double Dimensions { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public Double Size { get; set; }

		[JsonProperty("doors")]
		[DisplayName("Doors")]
		public Int64 Doors { get; set; }

		[JsonProperty("materials")]
		[DisplayName("Materials")]
		public String Materials { get; set; }

		[JsonProperty("designer")]
		[DisplayName("Designer")]
		public String Designer { get; set; }

		[JsonProperty("windows")]
		[DisplayName("Windows")]
		public Int64 Windows { get; set; }

		[JsonProperty("colors")]
		[DisplayName("Colors")]
		public String Colors { get; set; }

		[JsonProperty("distance")]
		[DisplayName("Distance")]
		public String Distance { get; set; }

		[JsonProperty("features")]
		[DisplayName("Features")]
		public String Features { get; set; }

		[JsonProperty("safety")]
		[DisplayName("Safety")]
		public String Safety { get; set; }

		[JsonProperty("fuel")]
		[DisplayName("Fuel")]
		public Double Fuel { get; set; }

		[JsonProperty("speed")]
		[DisplayName("Speed")]
		public Double Speed { get; set; }

		[JsonProperty("variants")]
		[DisplayName("Variants")]
		public String Variants { get; set; }

		[JsonProperty("manufacturer")]
		[DisplayName("Manufacturer")]
		public String Manufacturer { get; set; }

		[JsonProperty("costs")]
		[DisplayName("Costs")]
		public Double Costs { get; set; }

		[JsonProperty("weight")]
		[DisplayName("Weight")]
		public Double Weight { get; set; }

		[JsonProperty("country")]
		[DisplayName("Country")]
		public Int64 Country { get; set; }

		[JsonProperty("owner")]
		[DisplayName("Owner")]
		public String Owner { get; set; }

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


		public VehiclesModel()
		{
		}

	}
}
