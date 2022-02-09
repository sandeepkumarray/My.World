using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace My.World.Api.Models
{
	public class ContentPlansModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("plan_template")]
		[DisplayName("Plan Template")]
		public String plan_template { get; set; }

		[JsonProperty("plan_description")]
		[DisplayName("Plan Description")]
		public String plan_description { get; set; }

		[JsonProperty("available")]
		[DisplayName("Available")]
		public Boolean available { get; set; }

		[JsonProperty("monthly_cents")]
		[DisplayName("Monthly Cents")]
		public Double monthly_cents { get; set; }

		[JsonProperty("buildings_count")]
		[DisplayName("Buildings Count")]
		public Int32 buildings_count { get; set; }

		[JsonProperty("characters_count")]
		[DisplayName("Characters Count")]
		public Int32 characters_count { get; set; }

		[JsonProperty("conditions_count")]
		[DisplayName("Conditions Count")]
		public Int32 conditions_count { get; set; }

		[JsonProperty("continents_count")]
		[DisplayName("Continents Count")]
		public Int32 continents_count { get; set; }

		[JsonProperty("countries_count")]
		[DisplayName("Countries Count")]
		public Int32 countries_count { get; set; }

		[JsonProperty("creatures_count")]
		[DisplayName("Creatures Count")]
		public Int32 creatures_count { get; set; }

		[JsonProperty("deities_count")]
		[DisplayName("Deities Count")]
		public Int32 deities_count { get; set; }

		[JsonProperty("floras_count")]
		[DisplayName("Floras Count")]
		public Int32 floras_count { get; set; }

		[JsonProperty("foods_count")]
		[DisplayName("Foods Count")]
		public Int32 foods_count { get; set; }

		[JsonProperty("governments_count")]
		[DisplayName("Governments Count")]
		public Int32 governments_count { get; set; }

		[JsonProperty("groups_count")]
		[DisplayName("Groups Count")]
		public Int32 groups_count { get; set; }

		[JsonProperty("items_count")]
		[DisplayName("Items Count")]
		public Int32 items_count { get; set; }

		[JsonProperty("jobs_count")]
		[DisplayName("Jobs Count")]
		public Int32 jobs_count { get; set; }

		[JsonProperty("landmarks_count")]
		[DisplayName("Landmarks Count")]
		public Int32 landmarks_count { get; set; }

		[JsonProperty("languages_count")]
		[DisplayName("Languages Count")]
		public Int32 languages_count { get; set; }

		[JsonProperty("locations_count")]
		[DisplayName("Locations Count")]
		public Int32 locations_count { get; set; }

		[JsonProperty("lores_count")]
		[DisplayName("Lores Count")]
		public Int32 lores_count { get; set; }

		[JsonProperty("magics_count")]
		[DisplayName("Magics Count")]
		public Int32 magics_count { get; set; }

		[JsonProperty("planets_count")]
		[DisplayName("Planets Count")]
		public Int32 planets_count { get; set; }

		[JsonProperty("races_count")]
		[DisplayName("Races Count")]
		public Int32 races_count { get; set; }

		[JsonProperty("religions_count")]
		[DisplayName("Religions Count")]
		public Int32 religions_count { get; set; }

		[JsonProperty("scenes_count")]
		[DisplayName("Scenes Count")]
		public Int32 scenes_count { get; set; }

		[JsonProperty("sports_count")]
		[DisplayName("Sports Count")]
		public Int32 sports_count { get; set; }

		[JsonProperty("technologies_count")]
		[DisplayName("Technologies Count")]
		public Int32 technologies_count { get; set; }

		[JsonProperty("towns_count")]
		[DisplayName("Towns Count")]
		public Int32 towns_count { get; set; }

		[JsonProperty("traditions_count")]
		[DisplayName("Traditions Count")]
		public Int32 traditions_count { get; set; }

		[JsonProperty("universes_count")]
		[DisplayName("Universes Count")]
		public Int32 universes_count { get; set; }

		[JsonProperty("vehicles_count")]
		[DisplayName("Vehicles Count")]
		public Int32 vehicles_count { get; set; }

		[JsonProperty("created_by")]
		[DisplayName("Created By")]
		public Int32 created_by { get; set; }

		[JsonProperty("created_date")]
		[DisplayName("Created Date")]
		public DateTime created_date { get; set; }


		public ContentPlansModel()
		{
		}
		public object this[string property]
		{
			get
			{
				return this.GetType().GetProperty(property).GetValue(this, null);
			}
			set
			{
				this.GetType().GetProperty(property).SetValue(this, value, null);
			}
		}
	}
}
