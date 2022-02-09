using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace My.World.Api.Models
{
	public class UserPlanModelold
	{
		[JsonProperty("user_plan_id")]
		[DisplayName("user_plan_id")]
		public Int32 user_plan_id { get; set; }

		[JsonProperty("content_plan_id")]
		[DisplayName("content_plan_id")]
		public Int32 content_plan_id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("plan_template")]
		[DisplayName("Plan Template")]
		public String plan_template { get; set; }

		[JsonProperty("plan_description")]
		[DisplayName("Plan Description")]
		public String plan_description { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int32 user_id { get; set; }

		[JsonProperty("plan_id")]
		[DisplayName("Plan Id")]
		public Int32 plan_id { get; set; }

		public UserPlanModelold()
		{
		}

	}
}
