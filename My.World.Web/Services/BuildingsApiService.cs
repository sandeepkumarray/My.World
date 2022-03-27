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
	public class BuildingsApiService : BaseAPIService, IBuildingsApiService
	{

		public string AddBuildings(BuildingsModel model)
		{
			string buildingsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Buildings/AddBuildings";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			buildingsModel = response.Value;
			return buildingsModel;

		}

		public BuildingsModel GetBuildings(BuildingsModel model)
		{
			BuildingsModel buildingsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Buildings/GetBuildings";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<BuildingsModel> response = JsonConvert.DeserializeObject<ResponseModel<BuildingsModel>>(jsonResult);
			buildingsModel = response.Value;
			return buildingsModel;

		}

		public string DeleteBuildings(BuildingsModel model)
		{
			string buildingsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Buildings/DeleteBuildings";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			buildingsModel = response.Value;
			return buildingsModel;

		}

		public List<BuildingsModel> GetAllBuildings(long UserId)
		{
			List<BuildingsModel> buildingsModel = new List<BuildingsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Buildings/GetAllBuildings/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<BuildingsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<BuildingsModel>>>(jsonResult);
			buildingsModel = response.Value;
			return buildingsModel;

		}

		public ResponseModel<string> SaveBuilding(BuildingsModel model)
		{
			string buildingsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Buildings/SaveBuilding";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
