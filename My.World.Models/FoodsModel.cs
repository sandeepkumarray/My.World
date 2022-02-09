using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class FoodsModel : BaseModel
	{
		[JsonProperty("color")]
		[DisplayName("Color")]
		public String Color { get; set; }

		[JsonProperty("conditions")]
		[DisplayName("Conditions")]
		public String Conditions { get; set; }

		[JsonProperty("cooking_method")]
		[DisplayName("Cooking Method")]
		public String Cooking_method { get; set; }

		[JsonProperty("cost")]
		[DisplayName("Cost")]
		public String Cost { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("flavor")]
		[DisplayName("Flavor")]
		public String Flavor { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("ingredients")]
		[DisplayName("Ingredients")]
		public String Ingredients { get; set; }

		[JsonProperty("meal")]
		[DisplayName("Meal")]
		public String Meal { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("nutrition")]
		[DisplayName("Nutrition")]
		public String Nutrition { get; set; }

		[JsonProperty("origin_story")]
		[DisplayName("Origin Story")]
		public String Origin_story { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("place_of_origin")]
		[DisplayName("Place Of Origin")]
		public String Place_of_origin { get; set; }

		[JsonProperty("preparation")]
		[DisplayName("Preparation")]
		public String Preparation { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("rarity")]
		[DisplayName("Rarity")]
		public String Rarity { get; set; }

		[JsonProperty("related_foods")]
		[DisplayName("Related Foods")]
		public String Related_foods { get; set; }

		[JsonProperty("reputation")]
		[DisplayName("Reputation")]
		public String Reputation { get; set; }

		[JsonProperty("scent")]
		[DisplayName("Scent")]
		public String Scent { get; set; }

		[JsonProperty("serving")]
		[DisplayName("Serving")]
		public String Serving { get; set; }

		[JsonProperty("shelf_life")]
		[DisplayName("Shelf Life")]
		public String Shelf_life { get; set; }

		[JsonProperty("side_effects")]
		[DisplayName("Side Effects")]
		public String Side_effects { get; set; }

		[JsonProperty("size")]
		[DisplayName("Size")]
		public Double Size { get; set; }

		[JsonProperty("smell")]
		[DisplayName("Smell")]
		public String Smell { get; set; }

		[JsonProperty("sold_by")]
		[DisplayName("Sold By")]
		public String Sold_by { get; set; }

		[JsonProperty("spices")]
		[DisplayName("Spices")]
		public String Spices { get; set; }

		[JsonProperty("symbolisms")]
		[DisplayName("Symbolisms")]
		public String Symbolisms { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("texture")]
		[DisplayName("Texture")]
		public String Texture { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("type_of_food")]
		[DisplayName("Type Of Food")]
		public String Type_of_food { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("utensils_needed")]
		[DisplayName("Utensils Needed")]
		public String Utensils_needed { get; set; }

		[JsonProperty("variations")]
		[DisplayName("Variations")]
		public String Variations { get; set; }

		[JsonProperty("yield")]
		[DisplayName("Yield")]
		public String Yield { get; set; }


		public FoodsModel()
		{
		}

	}
}
