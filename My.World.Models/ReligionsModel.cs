using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ReligionsModel : BaseModel
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

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("notable_figures")]
		[DisplayName("Notable Figures")]
		public String Notable_figures { get; set; }

		[JsonProperty("origin_story")]
		[DisplayName("Origin Story")]
		public String Origin_story { get; set; }

		[JsonProperty("artifacts")]
		[DisplayName("Artifacts")]
		public String Artifacts { get; set; }

		[JsonProperty("places_of_worship")]
		[DisplayName("Places Of Worship")]
		public String Places_of_worship { get; set; }

		[JsonProperty("vision_of_paradise")]
		[DisplayName("Vision Of Paradise")]
		public String Vision_of_paradise { get; set; }

		[JsonProperty("obligations")]
		[DisplayName("Obligations")]
		public String Obligations { get; set; }

		[JsonProperty("worship_services")]
		[DisplayName("Worship Services")]
		public String Worship_services { get; set; }

		[JsonProperty("prophecies")]
		[DisplayName("Prophecies")]
		public String Prophecies { get; set; }

		[JsonProperty("teachings")]
		[DisplayName("Teachings")]
		public String Teachings { get; set; }

		[JsonProperty("deities")]
		[DisplayName("Deities")]
		public String Deities { get; set; }

		[JsonProperty("initiation_process")]
		[DisplayName("Initiation Process")]
		public String Initiation_process { get; set; }

		[JsonProperty("rituals")]
		[DisplayName("Rituals")]
		public String Rituals { get; set; }

		[JsonProperty("holidays")]
		[DisplayName("Holidays")]
		public String Holidays { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("practicing_locations")]
		[DisplayName("Practicing Locations")]
		public String Practicing_locations { get; set; }

		[JsonProperty("practicing_races")]
		[DisplayName("Practicing Races")]
		public String Practicing_races { get; set; }

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


		public ReligionsModel()
		{
		}

	}
}
