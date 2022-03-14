using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class SportsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("nicknames")]
		[DisplayName("Nicknames")]
		public String Nicknames { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("how_to_win")]
		[DisplayName("How To Win")]
		public String How_to_win { get; set; }

		[JsonProperty("penalties")]
		[DisplayName("Penalties")]
		public String Penalties { get; set; }

		[JsonProperty("scoring")]
		[DisplayName("Scoring")]
		public String Scoring { get; set; }

		[JsonProperty("number_of_players")]
		[DisplayName("Number Of Players")]
		public Int64 Number_of_players { get; set; }

		[JsonProperty("equipment")]
		[DisplayName("Equipment")]
		public String Equipment { get; set; }

		[JsonProperty("play_area")]
		[DisplayName("Play Area")]
		public String Play_area { get; set; }

		[JsonProperty("most_important_muscles")]
		[DisplayName("Most Important Muscles")]
		public String Most_important_muscles { get; set; }

		[JsonProperty("common_injuries")]
		[DisplayName("Common Injuries")]
		public String Common_injuries { get; set; }

		[JsonProperty("strategies")]
		[DisplayName("Strategies")]
		public String Strategies { get; set; }

		[JsonProperty("positions")]
		[DisplayName("Positions")]
		public Int64 Positions { get; set; }

		[JsonProperty("game_time")]
		[DisplayName("Game Time")]
		public Double Game_time { get; set; }

		[JsonProperty("rules")]
		[DisplayName("Rules")]
		public String Rules { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("teams")]
		[DisplayName("Teams")]
		public String Teams { get; set; }

		[JsonProperty("countries")]
		[DisplayName("Countries")]
		public String Countries { get; set; }

		[JsonProperty("players")]
		[DisplayName("Players")]
		public String Players { get; set; }

		[JsonProperty("popularity")]
		[DisplayName("Popularity")]
		public String Popularity { get; set; }

		[JsonProperty("merchandise")]
		[DisplayName("Merchandise")]
		public String Merchandise { get; set; }

		[JsonProperty("uniforms")]
		[DisplayName("Uniforms")]
		public String Uniforms { get; set; }

		[JsonProperty("famous_games")]
		[DisplayName("Famous Games")]
		public String Famous_games { get; set; }

		[JsonProperty("evolution")]
		[DisplayName("Evolution")]
		public String Evolution { get; set; }

		[JsonProperty("creators")]
		[DisplayName("Creators")]
		public String Creators { get; set; }

		[JsonProperty("origin_story")]
		[DisplayName("Origin Story")]
		public String Origin_story { get; set; }

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


		public SportsModel()
		{
		}

	}
}
