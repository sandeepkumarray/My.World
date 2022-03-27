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
	public class CreaturesApiService : BaseAPIService, ICreaturesApiService
	{

		public string AddCreatures(CreaturesModel model)
		{
			string creaturesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Creatures/AddCreatures";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			creaturesModel = response.Value;
			return creaturesModel;

		}

		public CreaturesModel GetCreatures(CreaturesModel model)
		{
			CreaturesModel creaturesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Creatures/GetCreatures";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<CreaturesModel> response = JsonConvert.DeserializeObject<ResponseModel<CreaturesModel>>(jsonResult);
			creaturesModel = response.Value;
			return creaturesModel;

		}

		public string DeleteCreatures(CreaturesModel model)
		{
			string creaturesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Creatures/DeleteCreatures";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			creaturesModel = response.Value;
			return creaturesModel;

		}

		public List<CreaturesModel> GetAllCreatures(long UserId)
		{
			List<CreaturesModel> creaturesModel = new List<CreaturesModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Creatures/GetAllCreatures/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<CreaturesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<CreaturesModel>>>(jsonResult);
			creaturesModel = response.Value;
			return creaturesModel;

		}

		public ResponseModel<string> SaveCreature(CreaturesModel model)
		{
			string creaturesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Creatures/SaveCreature";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
