using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class OrganizationsModel : BaseModel
	{
		[JsonProperty("address")]
		[DisplayName("Address")]
		public String Address { get; set; }

		[JsonProperty("alternate_names")]
		[DisplayName("Alternate Names")]
		public String Alternate_names { get; set; }

		[JsonProperty("closure_year")]
		[DisplayName("Closure Year")]
		public Int64 Closure_year { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("description")]
		[DisplayName("Description")]
		public String Description { get; set; }

		[JsonProperty("formation_year")]
		[DisplayName("Formation Year")]
		public Int64 Formation_year { get; set; }

		[JsonProperty("headquarters")]
		[DisplayName("Headquarters")]
		public String Headquarters { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("locations")]
		[DisplayName("Locations")]
		public String Locations { get; set; }

		[JsonProperty("members")]
		[DisplayName("Members")]
		public String Members { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String Name { get; set; }

		[JsonProperty("notes")]
		[DisplayName("Notes")]
		public String Notes { get; set; }

		[JsonProperty("offices")]
		[DisplayName("Offices")]
		public String Offices { get; set; }

		[JsonProperty("organization_structure")]
		[DisplayName("Organization Structure")]
		public String Organization_structure { get; set; }

		[JsonProperty("owner")]
		[DisplayName("Owner")]
		public String Owner { get; set; }

		[JsonProperty("private_notes")]
		[DisplayName("Private Notes")]
		public String Private_Notes { get; set; }

		[JsonProperty("purpose")]
		[DisplayName("Purpose")]
		public String Purpose { get; set; }

		[JsonProperty("rival_organizations")]
		[DisplayName("Rival Organizations")]
		public String Rival_organizations { get; set; }

		[JsonProperty("services")]
		[DisplayName("Services")]
		public String Services { get; set; }

		[JsonProperty("sister_organizations")]
		[DisplayName("Sister Organizations")]
		public String Sister_organizations { get; set; }

		[JsonProperty("sub_organizations")]
		[DisplayName("Sub Organizations")]
		public String Sub_organizations { get; set; }

		[JsonProperty("super_organizations")]
		[DisplayName("Super Organizations")]
		public String Super_organizations { get; set; }

		[JsonProperty("tags")]
		[DisplayName("Tags")]
		public String Tags { get; set; }

		[JsonProperty("type_of_organization")]
		[DisplayName("Type Of Organization")]
		public String Type_of_organization { get; set; }

		[JsonProperty("universe")]
		[DisplayName("Universe")]
		public Int64 Universe { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("user_id")]
		[DisplayName("User Id")]
		public Int64 user_id { get; set; }


		public OrganizationsModel()
		{
		}

	}
}
