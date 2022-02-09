using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace My.World.Api.Models
{
	public class ContentTypesModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("max_count")]
		[DisplayName("Max Count")]
		public Int32 max_count { get; set; }

		[JsonProperty("created_date")]
		[DisplayName("Created Date")]
		public DateTime created_date { get; set; }

		[JsonProperty("created_by")]
		[DisplayName("Created By")]
		public Int32 created_by { get; set; }

		[JsonProperty("OrderId")]
		public int OrderId { get; set; }

		[JsonProperty("icon")]
		[DisplayName("Icon")]
		public String icon { get; set; }

		[JsonProperty("primary_color")]
		[DisplayName("Primary Color")]
		public String primary_color { get; set; }

		[JsonProperty("sec_color")]
		[DisplayName("Sec Color")]
		public String sec_color { get; set; }
		public ContentTypesModel()
		{
		}

	}
}
