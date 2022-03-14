using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class CreaturesModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("type_of_creature")]
		[DisplayName("Type Of Creature")]
		public String Type_of_creature { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("weight")]
		[DisplayName("Weight")]
		public Double Weight { get; set; }

		[JsonProperty("notable_features")]
		[DisplayName("Notable Features")]
		public String Notable_features { get; set; }

		[JsonProperty("materials")]
		[DisplayName("Materials")]
		public String Materials { get; set; }

		[JsonProperty("vestigial_features")]
		[DisplayName("Vestigial Features")]
		public String Vestigial_features { get; set; }

		[JsonProperty("color")]
		[DisplayName("Color")]
		public Int64 Color { get; set; }

		[JsonProperty("shape")]
		[DisplayName("Shape")]
		public String Shape { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public Double Size { get; set; }

		[JsonProperty("height")]
		[DisplayName("Height")]
		public Double Height { get; set; }

		[JsonProperty("strongest_sense")]
		[DisplayName("Strongest Sense")]
		public String Strongest_sense { get; set; }

		[JsonProperty("aggressiveness")]
		[DisplayName("Aggressiveness")]
		public String Aggressiveness { get; set; }

		[JsonProperty("method_of_attack")]
		[DisplayName("Method Of Attack")]
		public String Method_of_attack { get; set; }

		[JsonProperty("methods_of_defense")]
		[DisplayName("Methods Of Defense")]
		public String Methods_of_defense { get; set; }

		[JsonProperty("maximum_speed")]
		[DisplayName("Maximum Speed")]
		public Double Maximum_speed { get; set; }

		[JsonProperty("strengths")]
		[DisplayName("Strengths")]
		public String Strengths { get; set; }

		[JsonProperty("weaknesses")]
		[DisplayName("Weaknesses")]
		public String Weaknesses { get; set; }

		[JsonProperty("sounds")]
		[DisplayName("Sounds")]
		public String Sounds { get; set; }

		[JsonProperty("spoils")]
		[DisplayName("Spoils")]
		public String Spoils { get; set; }

		[JsonProperty("conditions")]
		[DisplayName("Conditions")]
		public String Conditions { get; set; }

		[JsonProperty("weakest_sense")]
		[DisplayName("Weakest Sense")]
		public String Weakest_sense { get; set; }

		[JsonProperty("herding_patterns")]
		[DisplayName("Herding Patterns")]
		public String Herding_patterns { get; set; }

		[JsonProperty("prey")]
		[DisplayName("Prey")]
		public String Prey { get; set; }

		[JsonProperty("predators")]
		[DisplayName("Predators")]
		public String Predators { get; set; }

		[JsonProperty("competitors")]
		[DisplayName("Competitors")]
		public String Competitors { get; set; }

		[JsonProperty("migratory_patterns")]
		[DisplayName("Migratory Patterns")]
		public String Migratory_patterns { get; set; }

		[JsonProperty("food_sources")]
		[DisplayName("Food Sources")]
		public String Food_sources { get; set; }

		[JsonProperty("habitats")]
		[DisplayName("Habitats")]
		public String Habitats { get; set; }

		[JsonProperty("preferred_habitat")]
		[DisplayName("Preferred Habitat")]
		public String Preferred_habitat { get; set; }

		[JsonProperty("similar_creatures")]
		[DisplayName("Similar Creatures")]
		public String Similar_creatures { get; set; }

		[JsonProperty("symbolisms")]
		[DisplayName("Symbolisms")]
		public String Symbolisms { get; set; }

		[JsonProperty("related_creatures")]
		[DisplayName("Related Creatures")]
		public String Related_creatures { get; set; }

		[JsonProperty("ancestors")]
		[DisplayName("Ancestors")]
		public String Ancestors { get; set; }

		[JsonProperty("evolutionary_drive")]
		[DisplayName("Evolutionary Drive")]
		public String Evolutionary_drive { get; set; }

		[JsonProperty("tradeoffs")]
		[DisplayName("Tradeoffs")]
		public String Tradeoffs { get; set; }

		[JsonProperty("predictions")]
		[DisplayName("Predictions")]
		public String Predictions { get; set; }

		[JsonProperty("mortality_rate")]
		[DisplayName("Mortality Rate")]
		public String Mortality_rate { get; set; }

		[JsonProperty("offspring_care")]
		[DisplayName("Offspring Care")]
		public String Offspring_care { get; set; }

		[JsonProperty("reproduction_age")]
		[DisplayName("Reproduction Age")]
		public Double Reproduction_age { get; set; }

		[JsonProperty("requirements")]
		[DisplayName("Requirements")]
		public String Requirements { get; set; }

		[JsonProperty("mating_ritual")]
		[DisplayName("Mating Ritual")]
		public String Mating_ritual { get; set; }

		[JsonProperty("reproduction")]
		[DisplayName("Reproduction")]
		public String Reproduction { get; set; }

		[JsonProperty("reproduction_frequency")]
		[DisplayName("Reproduction Frequency")]
		public String Reproduction_frequency { get; set; }

		[JsonProperty("parental_instincts")]
		[DisplayName("Parental Instincts")]
		public String Parental_instincts { get; set; }

		[JsonProperty("variations")]
		[DisplayName("Variations")]
		public String Variations { get; set; }

		[JsonProperty("phylum")]
		[DisplayName("Phylum")]
		public String Phylum { get; set; }

		[JsonProperty("class")]
		[DisplayName("Class")]
		public String Class { get; set; }

		[JsonProperty("order")]
		[DisplayName("Order")]
		public String Order { get; set; }

		[JsonProperty("family")]
		[DisplayName("Family")]
		public String Family { get; set; }

		[JsonProperty("genus")]
		[DisplayName("Genus")]
		public String Genus { get; set; }

		[JsonProperty("species")]
		[DisplayName("Species")]
		public String Species { get; set; }

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


		public CreaturesModel()
		{
		}

	}
}
