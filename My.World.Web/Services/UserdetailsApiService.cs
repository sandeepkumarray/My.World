using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace My.World.Web.Services
{
	public class UserDetailsApiService : BaseAPIService, IUserDetailsApiService
	{

		public string AddUserDetails(UserDetailsModel model)
		{
			string userdetailsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "AddUserDetails";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			userdetailsModel = response.Value;
			return userdetailsModel;

		}

		public UserDetailsModel GetUserDetails(UserDetailsModel model)
		{
			UserDetailsModel userdetailsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "GetUserDetails";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<UserDetailsModel> response = JsonConvert.DeserializeObject<ResponseModel<UserDetailsModel>>(jsonResult);
			userdetailsModel = response.Value;
			return userdetailsModel;

		}

		public string DeleteUserDetails(UserDetailsModel model)
		{
			string userdetailsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "DeleteUserDetails";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			userdetailsModel = response.Value;
			return userdetailsModel;

		}

		public List<UserDetailsModel> GetAllUserDetails(long UserId)
		{
			List<UserDetailsModel> userdetailsModel = new List<UserDetailsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "GetAllUserDetails/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<UserDetailsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UserDetailsModel>>>(jsonResult);
			userdetailsModel = response.Value;
			return userdetailsModel;

		}

		public ResponseModel<string> SaveUserDetail(UserDetailsModel model)
		{
			string userdetailsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "SaveUserDetail";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
