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
	public class UsercontentbucketApiService : BaseAPIService, IUsercontentbucketApiService
	{

		public string AddUserContentBucket(UserContentBucketModel model)
		{
						string usercontentbucketModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddUserContentBucket";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						usercontentbucketModel = response.Value;
						return usercontentbucketModel;

		}

		public UserContentBucketModel GetUserContentBucket(UserContentBucketModel model)
		{
						UserContentBucketModel usercontentbucketModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetUserContentBucket";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<UserContentBucketModel> response = JsonConvert.DeserializeObject<ResponseModel<UserContentBucketModel>>(jsonResult);
						usercontentbucketModel = response.Value;
						return usercontentbucketModel;

		}

		public string DeleteUserContentBucket(UserContentBucketModel model)
		{
						string usercontentbucketModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteUserContentBucket";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						usercontentbucketModel = response.Value;
						return usercontentbucketModel;

		}

		public List<UserContentBucketModel> GetAllUserContentBucket(long UserId)
		{
						List<UserContentBucketModel> usercontentbucketModel = new List<UserContentBucketModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllUserContentBucket/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<UserContentBucketModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UserContentBucketModel>>>(jsonResult);
						usercontentbucketModel = response.Value;
						return usercontentbucketModel;

		}

		public ResponseModel<string> SaveUserContentBucke(UserContentBucketModel model)
		{
						string usercontentbucketModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveUserContentBucke";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
