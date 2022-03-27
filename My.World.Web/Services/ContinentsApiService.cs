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
	public class ContinentsApiService : BaseAPIService, IContinentsApiService
	{

		public string AddContinents(ContinentsModel model)
		{
			string continentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Continents/AddContinents";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			continentsModel = response.Value;
			return continentsModel;

		}

		public ContinentsModel GetContinents(ContinentsModel model)
		{
			ContinentsModel continentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Continents/GetContinents";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<ContinentsModel> response = JsonConvert.DeserializeObject<ResponseModel<ContinentsModel>>(jsonResult);
			continentsModel = response.Value;
			return continentsModel;

		}

		public string DeleteContinents(ContinentsModel model)
		{
			string continentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Continents/DeleteContinents";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			continentsModel = response.Value;
			return continentsModel;

		}

		public List<ContinentsModel> GetAllContinents(long UserId)
		{
			List<ContinentsModel> continentsModel = new List<ContinentsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Continents/GetAllContinents/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<ContinentsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContinentsModel>>>(jsonResult);
			continentsModel = response.Value;
			return continentsModel;

		}

		public ResponseModel<string> SaveContinent(ContinentsModel model)
		{
			string continentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Continents/SaveContinent";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
