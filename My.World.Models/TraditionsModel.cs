using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class TraditionsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

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

		[JsonProperty("type_of_tradition")]
		[DisplayName("Type Of Tradition")]
		public String Type_of_tradition { get; set; }

		[JsonProperty("countries")]
		[DisplayName("Countries")]
		public String Countries { get; set; }

		[JsonProperty("dates")]
		[DisplayName("Dates")]
		public String Dates { get; set; }

		[JsonProperty("groups")]
		[DisplayName("Groups")]
		public String Groups { get; set; }

		[JsonProperty("towns")]
		[DisplayName("Towns")]
		public String Towns { get; set; }

		[JsonProperty("gifts")]
		[DisplayName("Gifts")]
		public String Gifts { get; set; }

		[JsonProperty("food")]
		[DisplayName("Food")]
		public String Food { get; set; }

		[JsonProperty("symbolism")]
		[DisplayName("Symbolism")]
		public String Symbolism { get; set; }

		[JsonProperty("games")]
		[DisplayName("Games")]
		public String Games { get; set; }

		[JsonProperty("activities")]
		[DisplayName("Activities")]
		public String Activities { get; set; }

		[JsonProperty("etymology")]
		[DisplayName("Etymology")]
		public String Etymology { get; set; }

		[JsonProperty("origin")]
		[DisplayName("Origin")]
		public String Origin { get; set; }

		[JsonProperty("significance")]
		[DisplayName("Significance")]
		public String Significance { get; set; }

		[JsonProperty("religions")]
		[DisplayName("Religions")]
		public String Religions { get; set; }

		[JsonProperty("notable_events")]
		[DisplayName("Notable Events")]
		public String Notable_events { get; set; }

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


		public TraditionsModel()
		{
		}

	}
}
