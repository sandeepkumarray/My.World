using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class CountriesModel : BaseModel
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

		[JsonProperty("landmarks")]
		[DisplayName("Landmarks")]
		public String Landmarks { get; set; }

		[JsonProperty("locations")]
		[DisplayName("Locations")]
		public String Locations { get; set; }

		[JsonProperty("towns")]
		[DisplayName("Towns")]
		public String Towns { get; set; }

		[JsonProperty("bordering_countries")]
		[DisplayName("Bordering Countries")]
		public String Bordering_countries { get; set; }

		[JsonProperty("education")]
		[DisplayName("Education")]
		public String Education { get; set; }

		[JsonProperty("governments")]
		[DisplayName("Governments")]
		public String Governments { get; set; }

		[JsonProperty("religions")]
		[DisplayName("Religions")]
		public String Religions { get; set; }

		[JsonProperty("languages")]
		[DisplayName("Languages")]
		public String Languages { get; set; }

		[JsonProperty("sports")]
		[DisplayName("Sports")]
		public String Sports { get; set; }

		[JsonProperty("architecture")]
		[DisplayName("Architecture")]
		public String Architecture { get; set; }

		[JsonProperty("music")]
		[DisplayName("Music")]
		public String Music { get; set; }

		[JsonProperty("pop_culture")]
		[DisplayName("Pop Culture")]
		public String Pop_culture { get; set; }

		[JsonProperty("laws")]
		[DisplayName("Laws")]
		public String Laws { get; set; }

		[JsonProperty("currency")]
		[DisplayName("Currency")]
		public String Currency { get; set; }

		[JsonProperty("social_hierarchy")]
		[DisplayName("Social Hierarchy")]
		public String Social_hierarchy { get; set; }

		[JsonProperty("population")]
		[DisplayName("Population")]
		public Double Population { get; set; }

		[JsonProperty("area")]
		[DisplayName("Area")]
		public Double Area { get; set; }

		[JsonProperty("crops")]
		[DisplayName("Crops")]
		public String Crops { get; set; }

		[JsonProperty("climate")]
		[DisplayName("Climate")]
		public String Climate { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("flora")]
		[DisplayName("Flora")]
		public String Flora { get; set; }

		[JsonProperty("established_year")]
		[DisplayName("Established Year")]
		public Int64 Established_year { get; set; }

		[JsonProperty("notable_wars")]
		[DisplayName("Notable Wars")]
		public String Notable_wars { get; set; }

		[JsonProperty("founding_story")]
		[DisplayName("Founding Story")]
		public String Founding_story { get; set; }

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


		public CountriesModel()
		{
		}

	}
}
