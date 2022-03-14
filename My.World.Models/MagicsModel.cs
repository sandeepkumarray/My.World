using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class MagicsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("type_of_magic")]
		[DisplayName("Type Of Magic")]
		public String Type_of_magic { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("effects")]
		[DisplayName("Effects")]
		public String Effects { get; set; }

		[JsonProperty("visuals")]
		[DisplayName("Visuals")]
		public String Visuals { get; set; }

		[JsonProperty("aftereffects")]
		[DisplayName("Aftereffects")]
		public String Aftereffects { get; set; }

		[JsonProperty("conditions")]
		[DisplayName("Conditions")]
		public String Conditions { get; set; }

		[JsonProperty("scale")]
		[DisplayName("Scale")]
		public Double Scale { get; set; }

		[JsonProperty("negative_effects")]
		[DisplayName("Negative Effects")]
		public String Negative_effects { get; set; }

		[JsonProperty("neutral_effects")]
		[DisplayName("Neutral Effects")]
		public String Neutral_effects { get; set; }

		[JsonProperty("positive_effects")]
		[DisplayName("Positive Effects")]
		public String Positive_effects { get; set; }

		[JsonProperty("deities")]
		[DisplayName("Deities")]
		public String Deities { get; set; }

		[JsonProperty("element")]
		[DisplayName("Element")]
		public String Element { get; set; }

		[JsonProperty("materials_required")]
		[DisplayName("Materials Required")]
		public String Materials_required { get; set; }

		[JsonProperty("skills_required")]
		[DisplayName("Skills Required")]
		public String Skills_required { get; set; }

		[JsonProperty("education")]
		[DisplayName("Education")]
		public String Education { get; set; }

		[JsonProperty("resource_costs")]
		[DisplayName("Resource Costs")]
		public String Resource_costs { get; set; }

		[JsonProperty("limitations")]
		[DisplayName("Limitations")]
		public String Limitations { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_notes { get; set; }

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


		public MagicsModel()
		{
		}

	}
}
