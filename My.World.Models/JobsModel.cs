using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class JobsModel : BaseModel
	{
		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("type_of_job")]
		[DisplayName("Type Of Job")]
		public String Type_of_job { get; set; }

		[JsonProperty("alternate_names")]
		[DisplayName("Alternate Names")]
		public String Alternate_names { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("experience")]
		[DisplayName("Experience")]
		public String Experience { get; set; }

		[JsonProperty("education")]
		[DisplayName("Education")]
		public String Education { get; set; }

		[JsonProperty("work_hours")]
		[DisplayName("Work Hours")]
		public Double Work_hours { get; set; }

		[JsonProperty("vehicles")]
		[DisplayName("Vehicles")]
		public String Vehicles { get; set; }

		[JsonProperty("training")]
		[DisplayName("Training")]
		public String Training { get; set; }

		[JsonProperty("long_term_risks")]
		[DisplayName("Long Term Risks")]
		public String Long_term_risks { get; set; }

		[JsonProperty("occupational_hazards")]
		[DisplayName("Occupational Hazards")]
		public String Occupational_hazards { get; set; }

		[JsonProperty("pay_rate")]
		[DisplayName("Pay Rate")]
		public Double Pay_rate { get; set; }

		[JsonProperty("time_off")]
		[DisplayName("Time Off")]
		public String Time_off { get; set; }

		[JsonProperty("similar_jobs")]
		[DisplayName("Similar Jobs")]
		public String Similar_jobs { get; set; }

		[JsonProperty("promotions")]
		[DisplayName("Promotions")]
		public String Promotions { get; set; }

		[JsonProperty("specializations")]
		[DisplayName("Specializations")]
		public String Specializations { get; set; }

		[JsonProperty("field")]
		[DisplayName("Field")]
		public String Field { get; set; }

		[JsonProperty("ranks")]
		[DisplayName("Ranks")]
		public String Ranks { get; set; }

		[JsonProperty("traditions")]
		[DisplayName("Traditions")]
		public String Traditions { get; set; }

		[JsonProperty("job_origin")]
		[DisplayName("Job Origin")]
		public String Job_origin { get; set; }

		[JsonProperty("initial_goal")]
		[DisplayName("Initial Goal")]
		public String Initial_goal { get; set; }

		[JsonProperty("notable_figures")]
		[DisplayName("Notable Figures")]
		public String Notable_figures { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public JobsModel()
		{
		}

	}
}
