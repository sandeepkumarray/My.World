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
	public class ScenesApiService : BaseAPIService, IScenesApiService
	{

		public string AddScenes(ScenesModel model)
		{
						string scenesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddScenes";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						scenesModel = response.Value;
						return scenesModel;

		}

		public ScenesModel GetScenes(ScenesModel model)
		{
						ScenesModel scenesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetScenes";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<ScenesModel> response = JsonConvert.DeserializeObject<ResponseModel<ScenesModel>>(jsonResult);
						scenesModel = response.Value;
						return scenesModel;

		}

		public string DeleteScenes(ScenesModel model)
		{
						string scenesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteScenes";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						scenesModel = response.Value;
						return scenesModel;

		}

		public List<ScenesModel> GetAllScenes(long UserId)
		{
						List<ScenesModel> scenesModel = new List<ScenesModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllScenes/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<ScenesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ScenesModel>>>(jsonResult);
						scenesModel = response.Value;
						return scenesModel;

		}

		public ResponseModel<string> SaveScene(ScenesModel model)
		{
						string scenesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveScene";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
