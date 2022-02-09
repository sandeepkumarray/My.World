using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class TechnologiesModel : BaseModel
	{
		[JsonProperty("characters")]
		[DisplayName("Characters")]
		public String Characters { get; set; }

		[JsonProperty("child_technologies")]
		[DisplayName("Child Technologies")]
		public String Child_technologies { get; set; }

		[JsonProperty("colors")]
		[DisplayName("Colors")]
		public String Colors { get; set; }

		[JsonProperty("cost")]
		[DisplayName("Cost")]
		public Double Cost { get; set; }

		[JsonProperty("countries")]
		[DisplayName("Countries")]
		public String Countries { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("groups")]
		[DisplayName("Groups")]
		public String Groups { get; set; }

		[JsonProperty("how_it_works")]
		[DisplayName("How It Works")]
		public String How_It_Works { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("magic_effects")]
		[DisplayName("Magic Effects")]
		public String Magic_effects { get; set; }

		[JsonProperty("manufacturing_process")]
		[DisplayName("Manufacturing Process")]
		public String Manufacturing_Process { get; set; }

		[JsonProperty("materials")]
		[DisplayName("Materials")]
		public String Materials { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("parent_technologies")]
		[DisplayName("Parent Technologies")]
		public String Parent_technologies { get; set; }

		[JsonProperty("physical_description")]
		[DisplayName("Physical Description")]
		public String Physical_Description { get; set; }

		[JsonProperty("planets")]
		[DisplayName("Planets")]
		public String Planets { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("purpose")]
		[DisplayName("Purpose")]
		public String Purpose { get; set; }

		[JsonProperty("rarity")]
		[DisplayName("Rarity")]
		public String Rarity { get; set; }

		[JsonProperty("related_technologies")]
		[DisplayName("Related Technologies")]
		public String Related_technologies { get; set; }

		[JsonProperty("resources_used")]
		[DisplayName("Resources Used")]
		public String Resources_Used { get; set; }

		[JsonProperty("sales_process")]
		[DisplayName("Sales Process")]
		public String Sales_Process { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public Double Size { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("towns")]
		[DisplayName("Towns")]
		public String Towns { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("weight")]
		[DisplayName("Weight")]
		public Double Weight { get; set; }


		public TechnologiesModel()
		{
		}

	}
}
