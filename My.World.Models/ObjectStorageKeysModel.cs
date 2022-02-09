using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class ObjectStorageKeysModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("accesskey")]
		[DisplayName("Accesskey")]
		public String accessKey { get; set; }

		[JsonProperty("bucketname")]
		[DisplayName("Bucketname")]
		public String bucketName { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime Created_at { get; set; }

		[JsonProperty("endpoint")]
		[DisplayName("Endpoint")]
		public String endpoint { get; set; }

		[JsonProperty("location")]
		[DisplayName("Location")]
		public String location { get; set; }

		[JsonProperty("secretkey")]
		[DisplayName("Secretkey")]
		public String secretKey { get; set; }


		public ObjectStorageKeysModel()
		{
		}

	}
}
