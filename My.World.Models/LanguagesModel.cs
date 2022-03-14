using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class LanguagesModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String Other_Names { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("typology")]
		[DisplayName("Typology")]
		public String Typology { get; set; }

		[JsonProperty("dialectical_information")]
		[DisplayName("Dialectical Information")]
		public String Dialectical_information { get; set; }

		[JsonProperty("register")]
		[DisplayName("Register")]
		public String Register { get; set; }

		[JsonProperty("history")]
		[DisplayName("History")]
		public String History { get; set; }

		[JsonProperty("evolution")]
		[DisplayName("Evolution")]
		public String Evolution { get; set; }

		[JsonProperty("gestures")]
		[DisplayName("Gestures")]
		public String Gestures { get; set; }

		[JsonProperty("phonology")]
		[DisplayName("Phonology")]
		public String Phonology { get; set; }

		[JsonProperty("grammar")]
		[DisplayName("Grammar")]
		public String Grammar { get; set; }

		[JsonProperty("please")]
		[DisplayName("Please")]
		public String Please { get; set; }

		[JsonProperty("trade")]
		[DisplayName("Trade")]
		public String Trade { get; set; }

		[JsonProperty("family")]
		[DisplayName("Family")]
		public String Family { get; set; }

		[JsonProperty("body_parts")]
		[DisplayName("Body Parts")]
		public String Body_parts { get; set; }

		[JsonProperty("no_words")]
		[DisplayName("No Words")]
		public String No_words { get; set; }

		[JsonProperty("yes_words")]
		[DisplayName("Yes Words")]
		public String Yes_words { get; set; }

		[JsonProperty("sorry")]
		[DisplayName("Sorry")]
		public String Sorry { get; set; }

		[JsonProperty("you_are_welcome")]
		[DisplayName("You Are Welcome")]
		public String You_are_welcome { get; set; }

		[JsonProperty("thank_you")]
		[DisplayName("Thank You")]
		public String Thank_you { get; set; }

		[JsonProperty("goodbyes")]
		[DisplayName("Goodbyes")]
		public String Goodbyes { get; set; }

		[JsonProperty("greetings")]
		[DisplayName("Greetings")]
		public String Greetings { get; set; }

		[JsonProperty("pronouns")]
		[DisplayName("Pronouns")]
		public String Pronouns { get; set; }

		[JsonProperty("numbers")]
		[DisplayName("Numbers")]
		public String Numbers { get; set; }

		[JsonProperty("quantifiers")]
		[DisplayName("Quantifiers")]
		public String Quantifiers { get; set; }

		[JsonProperty("determiners")]
		[DisplayName("Determiners")]
		public String Determiners { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_notes { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public LanguagesModel()
		{
		}

	}
}
