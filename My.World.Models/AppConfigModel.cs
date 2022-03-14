using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class AppConfigModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("key")]
		[DisplayName("Key")]
		public String key { get; set; }

		[JsonProperty("value")]
		[DisplayName("Value")]
		public String value { get; set; }

		[JsonProperty("isactive")]
		[DisplayName("Isactive")]
		public Int64 isactive { get; set; }


		public AppConfigModel()
		{
		}

	}
}
