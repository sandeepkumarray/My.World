using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Web.ViewModel
{
    public class UserSignUpViewModel
    {
		[Required(AllowEmptyStrings = false, ErrorMessage = "Email is mandatory")]
		[EmailAddress(ErrorMessage = "Please enter a valid Email")]
		[JsonProperty("email")]
		[DisplayName("Email")]
		public String email { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Password is mandatory")]
		[PasswordPropertyText]
		[JsonProperty("password")]
		[DisplayName("Password")]
		public String password { get; set; }

		[Compare(otherProperty: "password", ErrorMessage = "Password & Confirm Password does not match")]
		[JsonProperty("confirm_password")]
		[DisplayName("Confirm Password")]
		public String confirm_password { get; set; }
	}
}
