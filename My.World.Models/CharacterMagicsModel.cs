using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class CharacterMagicsModel : BaseModel
	{
		[JsonProperty("character_id")]
		[DisplayName("Character Id")]
		public Int32 character_id { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("magic_id")]
		[DisplayName("Magic Id")]
		public Int32 magic_id { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int32 user_id { get; set; }


		public CharacterMagicsModel()
		{
		}

	}
}
