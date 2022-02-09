using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class FoldersModel
	{
		[JsonProperty("context")]
		[DisplayName("Context")]
		public String context { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("parent_folder_id")]
		[DisplayName("Parent Folder")]
		public Int64 parent_folder_id { get; set; }

		[JsonProperty("title")]
		[DisplayName("Title")]
		[Required]
		public String title { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public FoldersModel()
		{
		}

	}
}
