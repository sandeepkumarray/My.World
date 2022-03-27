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
	public class UniversesApiService : BaseAPIService, IUniversesApiService
	{

		public string AddUniverses(UniversesModel model)
		{
			string universesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Universes/AddUniverses";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			universesModel = response.Value;
			return universesModel;

		}

		public UniversesModel GetUniverses(UniversesModel model)
		{
			UniversesModel universesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Universes/GetUniverses";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<UniversesModel> response = JsonConvert.DeserializeObject<ResponseModel<UniversesModel>>(jsonResult);
			universesModel = response.Value;
			return universesModel;

		}

		public string DeleteUniverses(UniversesModel model)
		{
			string universesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Universes/DeleteUniverses";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			universesModel = response.Value;
			return universesModel;

		}

		public List<UniversesModel> GetAllUniverses(long UserId)
		{
			List<UniversesModel> universesModel = new List<UniversesModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Universes/GetAllUniverses/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<UniversesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UniversesModel>>>(jsonResult);
			universesModel = response.Value;
			return universesModel;

		}

		public ResponseModel<string> SaveUniverse(UniversesModel model)
		{
			string universesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Universes/SaveUniverse";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
