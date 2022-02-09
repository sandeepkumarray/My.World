using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class UserViewModel
    {
		[JsonProperty("username")]
		[DisplayName("Username")]
		public String username { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid password")]
		[JsonProperty("password")]
		[DisplayName("Password")]
		public String password { get; set; }
	}
}
