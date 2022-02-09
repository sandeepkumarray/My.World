using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My.World.Api.Models
{
	public class UsersModel
	{
		[JsonProperty("age")]
		[DisplayName("Age")]
		public String age { get; set; }

		[JsonProperty("bio")]
		[DisplayName("Bio")]
		public String bio { get; set; }

		[JsonProperty("community_features_enabled")]
		[DisplayName("Community Features Enabled")]
		public Boolean community_features_enabled { get; set; }

		[JsonProperty("created_at")]
		[DisplayName("Created At")]
		public DateTime created_at { get; set; }

		[JsonProperty("current_sign_in_at")]
		[DisplayName("Current Sign In At")]
		public DateTime current_sign_in_at { get; set; }

		[JsonProperty("current_sign_in_ip")]
		[DisplayName("Current Sign In Ip")]
		public String current_sign_in_ip { get; set; }

		[JsonProperty("dark_mode_enabled")]
		[DisplayName("Dark Mode Enabled")]
		public Boolean dark_mode_enabled { get; set; }

		[JsonProperty("deleted_at")]
		[DisplayName("Deleted At")]
		public DateTime deleted_at { get; set; }

		[JsonProperty("email")]
		[DisplayName("Email")]
		public String email { get; set; }

		[JsonProperty("email_confirm")]
		[DisplayName("Email Confirm")]
		public Boolean email_confirm { get; set; }
		
		[JsonProperty("email_updates")]
		[DisplayName("Email Updates")]
		public Boolean email_updates { get; set; }

		[JsonProperty("encrypted_password")]
		[DisplayName("Encrypted Password")]
		public String encrypted_password { get; set; }

		[JsonProperty("favorite_author")]
		[DisplayName("Favorite Author")]
		public String favorite_author { get; set; }

		[JsonProperty("favorite_book")]
		[DisplayName("Favorite Book")]
		public String favorite_book { get; set; }

		[JsonProperty("favorite_genre")]
		[DisplayName("Favorite Genre")]
		public String favorite_genre { get; set; }

		[JsonProperty("favorite_page_type")]
		[DisplayName("Favorite Page Type")]
		public String favorite_page_type { get; set; }

		[JsonProperty("favorite_quote")]
		[DisplayName("Favorite Quote")]
		public String favorite_quote { get; set; }

		[JsonProperty("fluid_preference")]
		[DisplayName("Fluid Preference")]
		public Boolean fluid_preference { get; set; }

		[JsonProperty("forum_administrator")]
		[DisplayName("Forum Administrator")]
		public Boolean forum_administrator { get; set; }

		[JsonProperty("forum_moderator")]
		[DisplayName("Forum Moderator")]
		public Boolean forum_moderator { get; set; }

		[JsonProperty("forums_badge_text")]
		[DisplayName("Forums Badge Text")]
		public String forums_badge_text { get; set; }

		[JsonProperty("gender")]
		[DisplayName("Gender")]
		public String gender { get; set; }

		[JsonProperty("id")]
		[DisplayName("Id")]
		public Int64 id { get; set; }

		[JsonProperty("inspirations")]
		[DisplayName("Inspirations")]
		public String inspirations { get; set; }

		[JsonProperty("interests")]
		[DisplayName("Interests")]
		public String interests { get; set; }

		[JsonProperty("keyboard_shortcuts_preference")]
		[DisplayName("Keyboard Shortcuts Preference")]
		public Boolean keyboard_shortcuts_preference { get; set; }

		[JsonProperty("last_sign_in_at")]
		[DisplayName("Last Sign In At")]
		public DateTime last_sign_in_at { get; set; }

		[JsonProperty("last_sign_in_ip")]
		[DisplayName("Last Sign In Ip")]
		public String last_sign_in_ip { get; set; }

		[JsonProperty("location")]
		[DisplayName("Location")]
		public String location { get; set; }

		[JsonProperty("name")]
		[DisplayName("Name")]
		public String name { get; set; }

		[JsonProperty("notification_updates")]
		[DisplayName("Notification Updates")]
		public Boolean notification_updates { get; set; }

		[JsonProperty("occupation")]
		[DisplayName("Occupation")]
		public String occupation { get; set; }

		[JsonProperty("old_password")]
		[DisplayName("Old Password")]
		public String old_password { get; set; }

		[JsonProperty("other_names")]
		[DisplayName("Other Names")]
		public String other_names { get; set; }

		[JsonProperty("site_template")]
		[DisplayName("Site Template")]
		public String site_template { get; set; }

		[JsonProperty("private_profile")]
		[DisplayName("Private Profile")]
		public Boolean private_profile { get; set; }

		[JsonProperty("remember_created_at")]
		[DisplayName("Remember Created At")]
		public DateTime remember_created_at { get; set; }

		[JsonProperty("reset_password_sent_at")]
		[DisplayName("Reset Password Sent At")]
		public DateTime reset_password_sent_at { get; set; }

		[JsonProperty("reset_password_token")]
		[DisplayName("Reset Password Token")]
		public String reset_password_token { get; set; }

		[JsonProperty("secure_code")]
		[DisplayName("Secure Code")]
		public String secure_code { get; set; }

		[JsonProperty("selected_billing_plan_id")]
		[DisplayName("Selected Billing Plan Id")]
		public Int32 selected_billing_plan_id { get; set; }

		[JsonProperty("sign_in_count")]
		[DisplayName("Sign In Count")]
		public Int32 sign_in_count { get; set; }

		[JsonProperty("site_administrator")]
		[DisplayName("Site Administrator")]
		public Boolean site_administrator { get; set; }

		[JsonProperty("stripe_customer_id")]
		[DisplayName("Stripe Customer Id")]
		public String stripe_customer_id { get; set; }

		[JsonProperty("updated_at")]
		[DisplayName("Updated At")]
		public DateTime updated_at { get; set; }

		[JsonProperty("upload_bandwidth_kb")]
		[DisplayName("Upload Bandwidth Kb")]
		public Int32 upload_bandwidth_kb { get; set; }

		[JsonProperty("username")]
		[DisplayName("Username")]
		public String username { get; set; }

		[JsonProperty("password")]
		[DisplayName("Password")]
		public String password { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid password")]
		[JsonProperty("new_password")]
		[DisplayName("New Password")]
		public String new_password { get; set; }

		[Compare(otherProperty: "new_password", ErrorMessage = "New & Confirm Password does not match")]
		[JsonProperty("confirm_password")]
		[DisplayName("Confirm Password")]
		public String confirm_password { get; set; }

		[JsonProperty("website")]
		[DisplayName("Website")]
		public String website { get; set; }


		[JsonProperty("user_plan")]
		public ContentPlansModel user_plan { get; set; }

		[JsonProperty("content_template")]
		public ContentTemplateModel content_template { get; set; }

		public UsersModel()
		{
		}

	}
}
