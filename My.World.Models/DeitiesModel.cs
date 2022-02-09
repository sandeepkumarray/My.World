using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class DeitiesModel : BaseModel
	{
		[JsonProperty("abilities")]
		[DisplayName("Abilities")]
		public String Abilities { get; set; }

		[JsonProperty("children")]
		[DisplayName("Children")]
		public String Children { get; set; }

		[JsonProperty("conditions")]
		[DisplayName("Conditions")]
		public String Conditions { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("elements")]
		[DisplayName("Elements")]
		public String Elements { get; set; }

		[JsonProperty("family_history")]
		[DisplayName("Family History")]
		public String Family_History { get; set; }

		[JsonProperty("floras")]
		[DisplayName("Floras")]
		public String Floras { get; set; }

		[JsonProperty("height")]
		[DisplayName("Height")]
		public Double Height { get; set; }

		[JsonProperty("human_interaction")]
		[DisplayName("Human Interaction")]
		public String Human_Interaction { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("life_story")]
		[DisplayName("Life Story")]
		public String Life_Story { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notable_events")]
		[DisplayName("Notable Events")]
		public String Notable_Events { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("parents")]
		[DisplayName("Parents")]
		public String Parents { get; set; }

		[JsonProperty("partners")]
		[DisplayName("Partners")]
		public String Partners { get; set; }

		[JsonProperty("physical_description")]
		[DisplayName("Physical Description")]
		public String Physical_Description { get; set; }

		[JsonProperty("prayers")]
		[DisplayName("Prayers")]
		public String Prayers { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("related_landmarks")]
		[DisplayName("Related Landmarks")]
		public String Related_landmarks { get; set; }

		[JsonProperty("related_races")]
		[DisplayName("Related Races")]
		public String Related_races { get; set; }

		[JsonProperty("related_towns")]
		[DisplayName("Related Towns")]
		public String Related_towns { get; set; }

		[JsonProperty("relics")]
		[DisplayName("Relics")]
		public String Relics { get; set; }

		[JsonProperty("religions")]
		[DisplayName("Religions")]
		public String Religions { get; set; }

		[JsonProperty("rituals")]
		[DisplayName("Rituals")]
		public String Rituals { get; set; }

		[JsonProperty("siblings")]
		[DisplayName("Siblings")]
		public String Siblings { get; set; }

		[JsonProperty("strengths")]
		[DisplayName("Strengths")]
		public String Strengths { get; set; }

		[JsonProperty("symbols")]
		[DisplayName("Symbols")]
		public String Symbols { get; set; }

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

		[JsonProperty("weaknesses")]
		[DisplayName("Weaknesses")]
		public String Weaknesses { get; set; }

		[JsonProperty("weight")]
		[DisplayName("Weight")]
		public Double Weight { get; set; }


		public DeitiesModel()
		{
		}

	}
}
