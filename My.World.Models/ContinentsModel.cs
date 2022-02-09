using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ContinentsModel : BaseModel
	{
		[JsonProperty("architecture")]
		[DisplayName("Architecture")]
		public String Architecture { get; set; }

		[JsonProperty("area")]
		[DisplayName("Area")]
		public Double Area { get; set; }

		[JsonProperty("bodies_of_water")]
		[DisplayName("Bodies Of Water")]
		public String Bodies_of_water { get; set; }

		[JsonProperty("countries")]
		[DisplayName("Countries")]
		public String Countries { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("crops")]
		[DisplayName("Crops")]
		public String Crops { get; set; }

		[JsonProperty("demonym")]
		[DisplayName("Demonym")]
		public String Demonym { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("discovery")]
		[DisplayName("Discovery")]
		public String Discovery { get; set; }

		[JsonProperty("economy")]
		[DisplayName("Economy")]
		public String Economy { get; set; }

		[JsonProperty("floras")]
		[DisplayName("Floras")]
		public String Floras { get; set; }

		[JsonProperty("formation")]
		[DisplayName("Formation")]
		public String Formation { get; set; }

		[JsonProperty("governments")]
		[DisplayName("Governments")]
		public String Governments { get; set; }

		[JsonProperty("humidity")]
		[DisplayName("Humidity")]
		public String Humidity { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("landmarks")]
		[DisplayName("Landmarks")]
		public String Landmarks { get; set; }

		[JsonProperty("languages")]
		[DisplayName("Languages")]
		public String Languages { get; set; }

		[JsonProperty("local_name")]
		[DisplayName("Local Name")]
		public String Local_name { get; set; }

		[JsonProperty("mineralogy")]
		[DisplayName("Mineralogy")]
		public String Mineralogy { get; set; }

		[JsonProperty("natural_disasters")]
		[DisplayName("Natural Disasters")]
		public String Natural_disasters { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("politics")]
		[DisplayName("Politics")]
		public String Politics { get; set; }

		[JsonProperty("popular_foods")]
		[DisplayName("Popular Foods")]
		public String Popular_foods { get; set; }

		[JsonProperty("population")]
		[DisplayName("Population")]
		public String Population { get; set; }

		[JsonProperty("precipitation")]
		[DisplayName("Precipitation")]
		public String Precipitation { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("regional_advantages")]
		[DisplayName("Regional Advantages")]
		public String Regional_advantages { get; set; }

		[JsonProperty("regional_disadvantages")]
		[DisplayName("Regional Disadvantages")]
		public String Regional_disadvantages { get; set; }

		[JsonProperty("reputation")]
		[DisplayName("Reputation")]
		public String Reputation { get; set; }

		[JsonProperty("ruins")]
		[DisplayName("Ruins")]
		public String Ruins { get; set; }

		[JsonProperty("seasons")]
		[DisplayName("Seasons")]
		public String Seasons { get; set; }

		[JsonProperty("shape")]
		[DisplayName("Shape")]
		public String Shape { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("temperature")]
		[DisplayName("Temperature")]
		public String Temperature { get; set; }

		[JsonProperty("topography")]
		[DisplayName("Topography")]
		public String Topography { get; set; }

		[JsonProperty("tourism")]
		[DisplayName("Tourism")]
		public String Tourism { get; set; }

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

		[JsonProperty("wars")]
		[DisplayName("Wars")]
		public String Wars { get; set; }

		[JsonProperty("winds")]
		[DisplayName("Winds")]
		public String Winds { get; set; }


		public ContinentsModel()
		{
		}

	}
}
