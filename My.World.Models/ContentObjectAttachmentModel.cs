using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ContentObjectAttachmentModel : BaseModel
	{
		[JsonProperty("content_id")]
		[DisplayName("Content Id")]
		public Int64 content_id { get; set; }

		[JsonProperty("content_type")]
		[DisplayName("Content Type")]
		public String content_type { get; set; }

		[JsonProperty("object_id")]
		[DisplayName("Object Id")]
		public Int64 object_id { get; set; }


		public ContentObjectAttachmentModel()
		{
		}

	}
}
