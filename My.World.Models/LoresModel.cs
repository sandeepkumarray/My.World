using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class LoresModel : BaseModel
	{
		[JsonProperty("background_information")]
		[DisplayName("Background Information")]
		public String Background_information { get; set; }

		[JsonProperty("believability")]
		[DisplayName("Believability")]
		public String Believability { get; set; }

		[JsonProperty("believers")]
		[DisplayName("Believers")]
		public String Believers { get; set; }

		[JsonProperty("buildings")]
		[DisplayName("Buildings")]
		public String Buildings { get; set; }

		[JsonProperty("characters")]
		[DisplayName("Characters")]
		public String Characters { get; set; }

		[JsonProperty("conditions")]
		[DisplayName("Conditions")]
		public String Conditions { get; set; }

		[JsonProperty("continents")]
		[DisplayName("Continents")]
		public String Continents { get; set; }

		[JsonProperty("countries")]
		[DisplayName("Countries")]
		public String Countries { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("created_phrases")]
		[DisplayName("Created Phrases")]
		public String Created_phrases { get; set; }

		[JsonProperty("created_traditions")]
		[DisplayName("Created Traditions")]
		public String Created_traditions { get; set; }

		[JsonProperty("creatures")]
		[DisplayName("Creatures")]
		public String Creatures { get; set; }

		[JsonProperty("criticism")]
		[DisplayName("Criticism")]
		public String Criticism { get; set; }

		[JsonProperty("date_recorded")]
		[DisplayName("Date Recorded")]
		public String Date_recorded { get; set; }

		[JsonProperty("deities")]
		[DisplayName("Deities")]
		public String Deities { get; set; }

		[JsonProperty("dialect")]
		[DisplayName("Dialect")]
		public String Dialect { get; set; }

		[JsonProperty("evolution_over_time")]
		[DisplayName("Evolution Over Time")]
		public String Evolution_over_time { get; set; }

		[JsonProperty("false_parts")]
		[DisplayName("False Parts")]
		public String False_parts { get; set; }

		[JsonProperty("floras")]
		[DisplayName("Floras")]
		public String Floras { get; set; }

		[JsonProperty("foods")]
		[DisplayName("Foods")]
		public String Foods { get; set; }

		[JsonProperty("full_text")]
		[DisplayName("Full Text")]
		public String Full_text { get; set; }

		[JsonProperty("genre")]
		[DisplayName("Genre")]
		public String Genre { get; set; }

		[JsonProperty("geographical_variations")]
		[DisplayName("Geographical Variations")]
		public String Geographical_variations { get; set; }

		[JsonProperty("governments")]
		[DisplayName("Governments")]
		public String Governments { get; set; }

		[JsonProperty("groups")]
		[DisplayName("Groups")]
		public String Groups { get; set; }

		[JsonProperty("historical_context")]
		[DisplayName("Historical Context")]
		public String Historical_context { get; set; }

		[JsonProperty("hoaxes")]
		[DisplayName("Hoaxes")]
		public String Hoaxes { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("impact")]
		[DisplayName("Impact")]
		public String Impact { get; set; }

		[JsonProperty("important_translations")]
		[DisplayName("Important Translations")]
		public String Important_translations { get; set; }

		[JsonProperty("influence_on_modern_times")]
		[DisplayName("Influence On Modern Times")]
		public String Influence_on_modern_times { get; set; }

		[JsonProperty("inspirations")]
		[DisplayName("Inspirations")]
		public String Inspirations { get; set; }

		[JsonProperty("interpretations")]
		[DisplayName("Interpretations")]
		public String Interpretations { get; set; }

		[JsonProperty("jobs")]
		[DisplayName("Jobs")]
		public String Jobs { get; set; }

		[JsonProperty("landmarks")]
		[DisplayName("Landmarks")]
		public String Landmarks { get; set; }

		[JsonProperty("magic")]
		[DisplayName("Magic")]
		public String Magic { get; set; }

		[JsonProperty("media_adaptations")]
		[DisplayName("Media Adaptations")]
		public String Media_adaptations { get; set; }

		[JsonProperty("morals")]
		[DisplayName("Morals")]
		public String Morals { get; set; }

		[JsonProperty("motivations")]
		[DisplayName("Motivations")]
		public String Motivations { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("original_author")]
		[DisplayName("Original Author")]
		public String Original_author { get; set; }

		[JsonProperty("original_languages")]
		[DisplayName("Original Languages")]
		public String Original_languages { get; set; }

		[JsonProperty("original_telling")]
		[DisplayName("Original Telling")]
		public String Original_telling { get; set; }

		[JsonProperty("planets")]
		[DisplayName("Planets")]
		public String Planets { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("propagation_method")]
		[DisplayName("Propagation Method")]
		public String Propagation_method { get; set; }

		[JsonProperty("races")]
		[DisplayName("Races")]
		public String Races { get; set; }

		[JsonProperty("reception")]
		[DisplayName("Reception")]
		public String Reception { get; set; }

		[JsonProperty("related_lores")]
		[DisplayName("Related Lores")]
		public String Related_lores { get; set; }

		[JsonProperty("religions")]
		[DisplayName("Religions")]
		public String Religions { get; set; }

		[JsonProperty("schools")]
		[DisplayName("Schools")]
		public String Schools { get; set; }

		[JsonProperty("source")]
		[DisplayName("Source")]
		public String Source { get; set; }

		[JsonProperty("sports")]
		[DisplayName("Sports")]
		public String Sports { get; set; }

		[JsonProperty("structure")]
		[DisplayName("Structure")]
		public String Structure { get; set; }

		[JsonProperty("subjects")]
		[DisplayName("Subjects")]
		public String Subjects { get; set; }

		[JsonProperty("summary")]
		[DisplayName("Summary")]
		public String Summary { get; set; }

		[JsonProperty("symbolisms")]
		[DisplayName("Symbolisms")]
		public String Symbolisms { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("technologies")]
		[DisplayName("Technologies")]
		public String Technologies { get; set; }

		[JsonProperty("time_period")]
		[DisplayName("Time Period")]
		public String Time_period { get; set; }

		[JsonProperty("tone")]
		[DisplayName("Tone")]
		public String Tone { get; set; }

		[JsonProperty("towns")]
		[DisplayName("Towns")]
		public String Towns { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("translation_variations")]
		[DisplayName("Translation Variations")]
		public String Translation_variations { get; set; }

		[JsonProperty("true_parts")]
		[DisplayName("True Parts")]
		public String True_parts { get; set; }

		[JsonProperty("type")]
		[DisplayName("Type")]
		public String Type { get; set; }

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

		[JsonProperty("vehicles")]
		[DisplayName("Vehicles")]
		public String Vehicles { get; set; }


		public LoresModel()
		{
		}

	}
}
