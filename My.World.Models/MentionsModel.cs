using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace My.World.Api.Models
{
    public class MentionsModel
    {
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Double id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("content_type")]
		[DisplayName("Content Type")]
		public String content_type { get; set; }
	}
}
