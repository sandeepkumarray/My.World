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
	public class LoresApiService : BaseAPIService, ILoresApiService
	{

		public string AddLores(LoresModel model)
		{
			string loresModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Lores/AddLores";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			loresModel = response.Value;
			return loresModel;

		}

		public LoresModel GetLores(LoresModel model)
		{
			LoresModel loresModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Lores/GetLores";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<LoresModel> response = JsonConvert.DeserializeObject<ResponseModel<LoresModel>>(jsonResult);
			loresModel = response.Value;
			return loresModel;

		}

		public string DeleteLores(LoresModel model)
		{
			string loresModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Lores/DeleteLores";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			loresModel = response.Value;
			return loresModel;

		}

		public List<LoresModel> GetAllLores(long UserId)
		{
			List<LoresModel> loresModel = new List<LoresModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Lores/GetAllLores/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<LoresModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<LoresModel>>>(jsonResult);
			loresModel = response.Value;
			return loresModel;

		}

		public ResponseModel<string> SaveLore(LoresModel model)
		{
			string loresModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Lores/SaveLore";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
