using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class GovernmentsModel : BaseModel
	{
		[JsonProperty("airforce")]
		[DisplayName("Airforce")]
		public String Airforce { get; set; }

		[JsonProperty("approval_ratings")]
		[DisplayName("Approval Ratings")]
		public String Approval_Ratings { get; set; }

		[JsonProperty("checks_and_balances")]
		[DisplayName("Checks And Balances")]
		public String Checks_And_Balances { get; set; }

		[JsonProperty("civilian_life")]
		[DisplayName("Civilian Life")]
		public String Civilian_Life { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("criminal_system")]
		[DisplayName("Criminal System")]
		public String Criminal_System { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("electoral_process")]
		[DisplayName("Electoral Process")]
		public String Electoral_Process { get; set; }

		[JsonProperty("flag_design_story")]
		[DisplayName("Flag Design Story")]
		public String Flag_Design_Story { get; set; }

		[JsonProperty("founding_story")]
		[DisplayName("Founding Story")]
		public String Founding_Story { get; set; }

		[JsonProperty("geocultural")]
		[DisplayName("Geocultural")]
		public String Geocultural { get; set; }

		[JsonProperty("groups")]
		[DisplayName("Groups")]
		public String Groups { get; set; }

		[JsonProperty("holidays")]
		[DisplayName("Holidays")]
		public String Holidays { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("immigration")]
		[DisplayName("Immigration")]
		public String Immigration { get; set; }

		[JsonProperty("international_relations")]
		[DisplayName("International Relations")]
		public String International_Relations { get; set; }

		[JsonProperty("items")]
		[DisplayName("Items")]
		public String Items { get; set; }

		[JsonProperty("jobs")]
		[DisplayName("Jobs")]
		public String Jobs { get; set; }

		[JsonProperty("laws")]
		[DisplayName("Laws")]
		public String Laws { get; set; }

		[JsonProperty("leaders")]
		[DisplayName("Leaders")]
		public String Leaders { get; set; }

		[JsonProperty("military")]
		[DisplayName("Military")]
		public String Military { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("navy")]
		[DisplayName("Navy")]
		public String Navy { get; set; }

		[JsonProperty("notable_wars")]
		[DisplayName("Notable Wars")]
		public String Notable_Wars { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("political_figures")]
		[DisplayName("Political Figures")]
		public String Political_figures { get; set; }

		[JsonProperty("power_source")]
		[DisplayName("Power Source")]
		public String Power_Source { get; set; }

		[JsonProperty("power_structure")]
		[DisplayName("Power Structure")]
		public String Power_Structure { get; set; }

		[JsonProperty("privacy_ideologies")]
		[DisplayName("Privacy Ideologies")]
		public String Privacy_Ideologies { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("socioeconomical")]
		[DisplayName("Socioeconomical")]
		public String Socioeconomical { get; set; }

		[JsonProperty("sociopolitical")]
		[DisplayName("Sociopolitical")]
		public String Sociopolitical { get; set; }

		[JsonProperty("space_program")]
		[DisplayName("Space Program")]
		public String Space_Program { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("technologies")]
		[DisplayName("Technologies")]
		public String Technologies { get; set; }

		[JsonProperty("term_lengths")]
		[DisplayName("Term Lengths")]
		public String Term_Lengths { get; set; }

		[JsonProperty("type_of_government")]
		[DisplayName("Type Of Government")]
		public String Type_Of_Government { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("vehicles")]
		[DisplayName("Vehicles")]
		public String Vehicles { get; set; }


		public GovernmentsModel()
		{
		}

	}
}
