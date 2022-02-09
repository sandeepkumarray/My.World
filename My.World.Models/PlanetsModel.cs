using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class PlanetsModel : BaseModel
	{
		[JsonProperty("atmosphere")]
		[DisplayName("Atmosphere")]
		public String Atmosphere { get; set; }

		[JsonProperty("calendar_system")]
		[DisplayName("Calendar System")]
		public String Calendar_System { get; set; }

		[JsonProperty("climate")]
		[DisplayName("Climate")]
		public String Climate { get; set; }

		[JsonProperty("continents")]
		[DisplayName("Continents")]
		public String Continents { get; set; }

		[JsonProperty("countries")]
		[DisplayName("Countries")]
		public String Countries { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("day_sky")]
		[DisplayName("Day Sky")]
		public String Day_sky { get; set; }

		[JsonProperty("deities")]
		[DisplayName("Deities")]
		public String Deities { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("first_inhabitants_story")]
		[DisplayName("First Inhabitants Story")]
		public String First_Inhabitants_Story { get; set; }

		[JsonProperty("flora")]
		[DisplayName("Flora")]
		public String Flora { get; set; }

		[JsonProperty("groups")]
		[DisplayName("Groups")]
		public String Groups { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("landmarks")]
		[DisplayName("Landmarks")]
		public String Landmarks { get; set; }

		[JsonProperty("languages")]
		[DisplayName("Languages")]
		public String Languages { get; set; }

		[JsonProperty("length_of_day")]
		[DisplayName("Length Of Day")]
		public Double Length_Of_Day { get; set; }

		[JsonProperty("length_of_night")]
		[DisplayName("Length Of Night")]
		public Double Length_Of_Night { get; set; }

		[JsonProperty("locations")]
		[DisplayName("Locations")]
		public String Locations { get; set; }

		[JsonProperty("moons")]
		[DisplayName("Moons")]
		public String Moons { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("natural_diasters")]
		[DisplayName("Natural Diasters")]
		public String Natural_diasters { get; set; }

		[JsonProperty("natural_resources")]
		[DisplayName("Natural Resources")]
		public String Natural_Resources { get; set; }

		[JsonProperty("nearby_planets")]
		[DisplayName("Nearby Planets")]
		public String Nearby_planets { get; set; }

		[JsonProperty("night_sky")]
		[DisplayName("Night Sky")]
		public String Night_sky { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("orbit")]
		[DisplayName("Orbit")]
		public String Orbit { get; set; }

		[JsonProperty("population")]
		[DisplayName("Population")]
		public Double Population { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("races")]
		[DisplayName("Races")]
		public String Races { get; set; }

		[JsonProperty("religions")]
		[DisplayName("Religions")]
		public String Religions { get; set; }

		[JsonProperty("seasons")]
		[DisplayName("Seasons")]
		public String Seasons { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public Double Size { get; set; }

		[JsonProperty("suns")]
		[DisplayName("Suns")]
		public String Suns { get; set; }

		[JsonProperty("surface")]
		[DisplayName("Surface")]
		public Double Surface { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("temperature")]
		[DisplayName("Temperature")]
		public Double Temperature { get; set; }

		[JsonProperty("towns")]
		[DisplayName("Towns")]
		public String Towns { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("visible_constellations")]
		[DisplayName("Visible Constellations")]
		public String Visible_Constellations { get; set; }

		[JsonProperty("water_content")]
		[DisplayName("Water Content")]
		public Double Water_Content { get; set; }

		[JsonProperty("weather")]
		[DisplayName("Weather")]
		public String Weather { get; set; }

		[JsonProperty("world_history")]
		[DisplayName("World History")]
		public String World_History { get; set; }


		public PlanetsModel()
		{
		}

	}
}
