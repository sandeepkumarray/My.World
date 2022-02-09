using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ConditionsModel : BaseModel
	{
		[JsonProperty("alternate_names")]
		[DisplayName("Alternate Names")]
		public String Alternate_names { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("diagnostic_method")]
		[DisplayName("Diagnostic Method")]
		public String Diagnostic_method { get; set; }

		[JsonProperty("duration")]
		[DisplayName("Duration")]
		public String Duration { get; set; }

		[JsonProperty("environmental_factors")]
		[DisplayName("Environmental Factors")]
		public String Environmental_factors { get; set; }

		[JsonProperty("epidemiology")]
		[DisplayName("Epidemiology")]
		public String Epidemiology { get; set; }

		[JsonProperty("evolution")]
		[DisplayName("Evolution")]
		public String Evolution { get; set; }

		[JsonProperty("genetic_factors")]
		[DisplayName("Genetic Factors")]
		public String Genetic_factors { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("immunization")]
		[DisplayName("Immunization")]
		public String Immunization { get; set; }

		[JsonProperty("lifestyle_factors")]
		[DisplayName("Lifestyle Factors")]
		public String Lifestyle_factors { get; set; }

		[JsonProperty("medication")]
		[DisplayName("Medication")]
		public String Medication { get; set; }

		[JsonProperty("mental_effects")]
		[DisplayName("Mental Effects")]
		public String Mental_effects { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("origin")]
		[DisplayName("Origin")]
		public String Origin { get; set; }

		[JsonProperty("prevention")]
		[DisplayName("Prevention")]
		public String Prevention { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("prognosis")]
		[DisplayName("Prognosis")]
		public String Prognosis { get; set; }

		[JsonProperty("rarity")]
		[DisplayName("Rarity")]
		public String Rarity { get; set; }

		[JsonProperty("specialty_field")]
		[DisplayName("Specialty Field")]
		public String Specialty_Field { get; set; }

		[JsonProperty("symbolism")]
		[DisplayName("Symbolism")]
		public String Symbolism { get; set; }

		[JsonProperty("symptoms")]
		[DisplayName("Symptoms")]
		public String Symptoms { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("transmission")]
		[DisplayName("Transmission")]
		public String Transmission { get; set; }

		[JsonProperty("treatment")]
		[DisplayName("Treatment")]
		public String Treatment { get; set; }

		[JsonProperty("type_of_condition")]
		[DisplayName("Type Of Condition")]
		public String Type_of_condition { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("variations")]
		[DisplayName("Variations")]
		public String Variations { get; set; }

		[JsonProperty("visual_effects")]
		[DisplayName("Visual Effects")]
		public String Visual_effects { get; set; }


		public ConditionsModel()
		{
		}

	}
}
