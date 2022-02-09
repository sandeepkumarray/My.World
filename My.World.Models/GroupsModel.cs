using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class GroupsModel : BaseModel
	{
		[JsonProperty("allies")]
		[DisplayName("Allies")]
		public String Allies { get; set; }

		[JsonProperty("clients")]
		[DisplayName("Clients")]
		public String Clients { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("enemies")]
		[DisplayName("Enemies")]
		public String Enemies { get; set; }

		[JsonProperty("equipment")]
		[DisplayName("Equipment")]
		public String Equipment { get; set; }

		[JsonProperty("goals")]
		[DisplayName("Goals")]
		public String Goals { get; set; }

		[JsonProperty("headquarters")]
		[DisplayName("Headquarters")]
		public String Headquarters { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("inventory")]
		[DisplayName("Inventory")]
		public String Inventory { get; set; }

		[JsonProperty("key_items")]
		[DisplayName("Key Items")]
		public String Key_items { get; set; }

		[JsonProperty("leaders")]
		[DisplayName("Leaders")]
		public String Leaders { get; set; }

		[JsonProperty("locations")]
		[DisplayName("Locations")]
		public String Locations { get; set; }

		[JsonProperty("members")]
		[DisplayName("Members")]
		public String Members { get; set; }

		[JsonProperty("motivations")]
		[DisplayName("Motivations")]
		public String Motivations { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("obstacles")]
		[DisplayName("Obstacles")]
		public String Obstacles { get; set; }

		[JsonProperty("offices")]
		[DisplayName("Offices")]
		public String Offices { get; set; }

		[JsonProperty("organization_structure")]
		[DisplayName("Organization Structure")]
		public String Organization_structure { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_notes { get; set; }

		[JsonProperty("risks")]
		[DisplayName("Risks")]
		public String Risks { get; set; }

		[JsonProperty("rivals")]
		[DisplayName("Rivals")]
		public String Rivals { get; set; }

		[JsonProperty("sistergroups")]
		[DisplayName("Sistergroups")]
		public String Sistergroups { get; set; }

		[JsonProperty("subgroups")]
		[DisplayName("Subgroups")]
		public String Subgroups { get; set; }

		[JsonProperty("supergroups")]
		[DisplayName("Supergroups")]
		public String Supergroups { get; set; }

		[JsonProperty("suppliers")]
		[DisplayName("Suppliers")]
		public String Suppliers { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public GroupsModel()
		{
		}

	}
}
