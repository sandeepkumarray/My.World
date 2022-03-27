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
	public class FlorasApiService : BaseAPIService, IFlorasApiService
	{

		public string AddFloras(FlorasModel model)
		{
			string florasModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Floras/AddFloras";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			florasModel = response.Value;
			return florasModel;

		}

		public FlorasModel GetFloras(FlorasModel model)
		{
			FlorasModel florasModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Floras/GetFloras";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<FlorasModel> response = JsonConvert.DeserializeObject<ResponseModel<FlorasModel>>(jsonResult);
			florasModel = response.Value;
			return florasModel;

		}

		public string DeleteFloras(FlorasModel model)
		{
			string florasModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Floras/DeleteFloras";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			florasModel = response.Value;
			return florasModel;

		}

		public List<FlorasModel> GetAllFloras(long UserId)
		{
			List<FlorasModel> florasModel = new List<FlorasModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Floras/GetAllFloras/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<FlorasModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<FlorasModel>>>(jsonResult);
			florasModel = response.Value;
			return florasModel;

		}

		public ResponseModel<string> SaveFlora(FlorasModel model)
		{
			string florasModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Floras/SaveFlora";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
