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
	public class GroupsApiService : BaseAPIService, IGroupsApiService
	{

		public string AddGroups(GroupsModel model)
		{
			string groupsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Groups/AddGroups";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			groupsModel = response.Value;
			return groupsModel;

		}

		public GroupsModel GetGroups(GroupsModel model)
		{
			GroupsModel groupsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Groups/GetGroups";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<GroupsModel> response = JsonConvert.DeserializeObject<ResponseModel<GroupsModel>>(jsonResult);
			groupsModel = response.Value;
			return groupsModel;

		}

		public string DeleteGroups(GroupsModel model)
		{
			string groupsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Groups/DeleteGroups";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			groupsModel = response.Value;
			return groupsModel;

		}

		public List<GroupsModel> GetAllGroups(long UserId)
		{
			List<GroupsModel> groupsModel = new List<GroupsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Groups/GetAllGroups/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<GroupsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<GroupsModel>>>(jsonResult);
			groupsModel = response.Value;
			return groupsModel;

		}

		public ResponseModel<string> SaveGroup(GroupsModel model)
		{
			string groupsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Groups/SaveGroup";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
