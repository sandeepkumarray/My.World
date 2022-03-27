using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace My.World.Api.Models
{
    public class BaseModel
	{
		[JsonProperty("_id")]
		[DisplayName("_Id")]
		public Int64 _id { get; set; }

		[JsonProperty("column_type")]
		[DisplayName("Column Type")]
		public String column_type { get; set; }

		[JsonProperty("column_value")]
		[DisplayName("Column Value")]
		public object column_value { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }

		[JsonProperty("object_id")]
		[DisplayName("Object Id")]
		public Int64 object_id { get; set; }

		[JsonProperty("object_name")]
		[DisplayName("Object Name")]
		public String object_name { get; set; }

		[JsonProperty("image_url")]
		[DisplayName("Image Url")]
		public String image_url { get; set; }

		[JsonProperty("content_name")]
		[DisplayName("Content Name")]
		public String content_name { get; set; }
	}

    public class Principal
    {
        public List<string> AWS { get; set; }
    }

    public class Statement
    {
        public string Effect { get; set; }
        public Principal Principal { get; set; }
        public List<string> Action { get; set; }
        public List<string> Resource { get; set; }
    }

    public class MinioPolicy
    {
        public string Version { get; set; }
        public List<Statement> Statement { get; set; }
    }
}
