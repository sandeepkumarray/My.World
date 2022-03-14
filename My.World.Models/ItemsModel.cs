using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ItemsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("item_type")]
		[DisplayName("Item Type")]
		public String Item_Type { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("weight")]
		[DisplayName("Weight")]
		public Double Weight { get; set; }

		[JsonProperty("materials")]
		[DisplayName("Materials")]
		public String Materials { get; set; }

		[JsonProperty("past_owners")]
		[DisplayName("Past Owners")]
		public String Past_Owners { get; set; }

		[JsonProperty("year_it_was_made")]
		[DisplayName("Year It Was Made")]
		public Int64 Year_it_was_made { get; set; }

		[JsonProperty("makers")]
		[DisplayName("Makers")]
		public String Makers { get; set; }

		[JsonProperty("current_owners")]
		[DisplayName("Current Owners")]
		public String Current_Owners { get; set; }

		[JsonProperty("original_owners")]
		[DisplayName("Original Owners")]
		public String Original_Owners { get; set; }

		[JsonProperty("magical_effects")]
		[DisplayName("Magical Effects")]
		public String Magical_effects { get; set; }

		[JsonProperty("magic")]
		[DisplayName("Magic")]
		public String Magic { get; set; }

		[JsonProperty("technical_effects")]
		[DisplayName("Technical Effects")]
		public String Technical_effects { get; set; }

		[JsonProperty("technology")]
		[DisplayName("Technology")]
		public String Technology { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public ItemsModel()
		{
		}

	}
}
