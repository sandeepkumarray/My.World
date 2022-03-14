using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class FlorasModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("order")]
		[DisplayName("Order")]
		public String Order { get; set; }

		[JsonProperty("related_flora")]
		[DisplayName("Related Flora")]
		public String Related_flora { get; set; }

		[JsonProperty("genus")]
		[DisplayName("Genus")]
		public String Genus { get; set; }

		[JsonProperty("family")]
		[DisplayName("Family")]
		public String Family { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public String Size { get; set; }

		[JsonProperty("smell")]
		[DisplayName("Smell")]
		public String Smell { get; set; }

		[JsonProperty("taste")]
		[DisplayName("Taste")]
		public String Taste { get; set; }

		[JsonProperty("colorings")]
		[DisplayName("Colorings")]
		public String Colorings { get; set; }

		[JsonProperty("fruits")]
		[DisplayName("Fruits")]
		public String Fruits { get; set; }

		[JsonProperty("magical_effects")]
		[DisplayName("Magical Effects")]
		public String Magical_effects { get; set; }

		[JsonProperty("material_uses")]
		[DisplayName("Material Uses")]
		public String Material_uses { get; set; }

		[JsonProperty("medicinal_purposes")]
		[DisplayName("Medicinal Purposes")]
		public String Medicinal_purposes { get; set; }

		[JsonProperty("berries")]
		[DisplayName("Berries")]
		public String Berries { get; set; }

		[JsonProperty("nuts")]
		[DisplayName("Nuts")]
		public String Nuts { get; set; }

		[JsonProperty("seeds")]
		[DisplayName("Seeds")]
		public String Seeds { get; set; }

		[JsonProperty("seasonality")]
		[DisplayName("Seasonality")]
		public String Seasonality { get; set; }

		[JsonProperty("locations")]
		[DisplayName("Locations")]
		public String Locations { get; set; }

		[JsonProperty("reproduction")]
		[DisplayName("Reproduction")]
		public String Reproduction { get; set; }

		[JsonProperty("eaten_by")]
		[DisplayName("Eaten By")]
		public String Eaten_by { get; set; }

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


		public FlorasModel()
		{
		}

	}
}
