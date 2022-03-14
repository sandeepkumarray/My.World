using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class RacesModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("general_weight")]
		[DisplayName("General Weight")]
		public Double General_weight { get; set; }

		[JsonProperty("notable_features")]
		[DisplayName("Notable Features")]
		public String Notable_features { get; set; }

		[JsonProperty("physical_variance")]
		[DisplayName("Physical Variance")]
		public String Physical_variance { get; set; }

		[JsonProperty("typical_clothing")]
		[DisplayName("Typical Clothing")]
		public String Typical_clothing { get; set; }

		[JsonProperty("body_shape")]
		[DisplayName("Body Shape")]
		public Double Body_shape { get; set; }

		[JsonProperty("skin_colors")]
		[DisplayName("Skin Colors")]
		public String Skin_colors { get; set; }

		[JsonProperty("general_height")]
		[DisplayName("General Height")]
		public Double General_height { get; set; }

		[JsonProperty("weaknesses")]
		[DisplayName("Weaknesses")]
		public String Weaknesses { get; set; }

		[JsonProperty("conditions")]
		[DisplayName("Conditions")]
		public String Conditions { get; set; }

		[JsonProperty("strengths")]
		[DisplayName("Strengths")]
		public String Strengths { get; set; }

		[JsonProperty("favorite_foods")]
		[DisplayName("Favorite Foods")]
		public String Favorite_foods { get; set; }

		[JsonProperty("famous_figures")]
		[DisplayName("Famous Figures")]
		public String Famous_figures { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("beliefs")]
		[DisplayName("Beliefs")]
		public String Beliefs { get; set; }

		[JsonProperty("governments")]
		[DisplayName("Governments")]
		public String Governments { get; set; }

		[JsonProperty("technologies")]
		[DisplayName("Technologies")]
		public String Technologies { get; set; }

		[JsonProperty("occupations")]
		[DisplayName("Occupations")]
		public String Occupations { get; set; }

		[JsonProperty("economics")]
		[DisplayName("Economics")]
		public String Economics { get; set; }

		[JsonProperty("notable_events")]
		[DisplayName("Notable Events")]
		public String Notable_events { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_notes { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public RacesModel()
		{
		}

	}
}
