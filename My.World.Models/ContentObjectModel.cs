using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace My.World.Api.Models
{
	public class ContentObjectModel : BaseModel
	{
		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("object_id")]
		[DisplayName("Object Id")]
		public Int64 object_id { get; set; }

		[JsonProperty("object_name")]
		[DisplayName("Object Name")]
		public String object_name { get; set; }

		[JsonProperty("object_size")]
		[DisplayName("Object Size")]
		public Int64 object_size { get; set; }

		[JsonProperty("object_type")]
		[DisplayName("Object Type")]
		public String object_type { get; set; }

		[JsonProperty("file")]
		[DisplayName("File")]
		public MemoryStream file { get; set; }

		[JsonProperty("file_url")]
		[DisplayName("File Url")]
		public string file_url { get; set; }

		public ContentObjectModel()
		{
		}

	}
}
