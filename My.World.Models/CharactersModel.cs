using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class CharactersModel : BaseModel
	{
		[JsonProperty("age")]
		[DisplayName("Age")]
		public String Age { get; set; }

		[JsonProperty("aliases")]
		[DisplayName("Aliases")]
		public String Aliases { get; set; }

		[JsonProperty("archived_at")]
		[DisplayName("Archived At")]
		public DateTime archived_at { get; set; }

		[JsonProperty("background")]
		[DisplayName("Background")]
		public String Background { get; set; }

		[JsonProperty("birthday")]
		[DisplayName("Birthday")]
		public String Birthday { get; set; }

		[JsonProperty("birthplace")]
		[DisplayName("Birthplace")]
		public String Birthplace { get; set; }

		[JsonProperty("bodytype")]
		[DisplayName("Bodytype")]
		public String Bodytype { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("deleted_at")]
		[DisplayName("Deleted At")]
		public DateTime deleted_at { get; set; }

		[JsonProperty("education")]
		[DisplayName("Education")]
		public String Education { get; set; }

		[JsonProperty("eyecolor")]
		[DisplayName("Eyecolor")]
		public String Eyecolor { get; set; }

		[JsonProperty("facialhair")]
		[DisplayName("Facialhair")]
		public String Facialhair { get; set; }

		[JsonProperty("fave_animal")]
		[DisplayName("Fave Animal")]
		public String Fave_animal { get; set; }

		[JsonProperty("fave_color")]
		[DisplayName("Fave Color")]
		public String Fave_color { get; set; }

		[JsonProperty("fave_food")]
		[DisplayName("Fave Food")]
		public String Fave_food { get; set; }

		[JsonProperty("fave_possession")]
		[DisplayName("Fave Possession")]
		public String Fave_possession { get; set; }

		[JsonProperty("fave_weapon")]
		[DisplayName("Fave Weapon")]
		public String Fave_weapon { get; set; }

		[JsonProperty("favorite")]
		[DisplayName("Favorite")]
		public Boolean Favorite { get; set; }

		[JsonProperty("flaws")]
		[DisplayName("Flaws")]
		public String Flaws { get; set; }

		[JsonProperty("gender")]
		[DisplayName("Gender")]
		public String Gender { get; set; }

		[JsonProperty("haircolor")]
		[DisplayName("Haircolor")]
		public String Haircolor { get; set; }

		[JsonProperty("hairstyle")]
		[DisplayName("Hairstyle")]
		public String Hairstyle { get; set; }

		[JsonProperty("height")]
		[DisplayName("Height")]
		public String Height { get; set; }

		[JsonProperty("hobbies")]
		[DisplayName("Hobbies")]
		public String Hobbies { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Double id { get; set; }

		[JsonProperty("identmarks")]
		[DisplayName("Identmarks")]
		public String Identmarks { get; set; }

		[JsonProperty("mannerisms")]
		[DisplayName("Mannerisms")]
		public String Mannerisms { get; set; }

		[JsonProperty("motivations")]
		[DisplayName("Motivations")]
		public String Motivations { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("occupation")]
		[DisplayName("Occupation")]
		public String Occupation { get; set; }

		[JsonProperty("personality_type")]
		[DisplayName("Personality Type")]
		public String Personality_type { get; set; }

		[JsonProperty("pets")]
		[DisplayName("Pets")]
		public String Pets { get; set; }

		[JsonProperty("politics")]
		[DisplayName("Politics")]
		public String Politics { get; set; }

		[JsonProperty("prejudices")]
		[DisplayName("Prejudices")]
		public String Prejudices { get; set; }

		[JsonProperty("privacy")]
		[DisplayName("Privacy")]
		public String Privacy { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_notes { get; set; }

		[JsonProperty("race")]
		[DisplayName("Race")]
		public String Race { get; set; }

		[JsonProperty("religion")]
		[DisplayName("Religion")]
		public String Religion { get; set; }

		[JsonProperty("role")]
		[DisplayName("Role")]
		public String Role { get; set; }

		[JsonProperty("skintone")]
		[DisplayName("Skintone")]
		public String Skintone { get; set; }

		[JsonProperty("talents")]
		[DisplayName("Talents")]
		public String Talents { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("weight")]
		[DisplayName("Weight")]
		public String Weight { get; set; }


		public CharactersModel()
		{
		}

	}
}
