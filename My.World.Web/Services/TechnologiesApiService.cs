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
	public class TechnologiesApiService : BaseAPIService, ITechnologiesApiService
	{

		public string AddTechnologies(TechnologiesModel model)
		{
			string technologiesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Technologies/AddTechnologies";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			technologiesModel = response.Value;
			return technologiesModel;

		}

		public TechnologiesModel GetTechnologies(TechnologiesModel model)
		{
			TechnologiesModel technologiesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Technologies/GetTechnologies";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<TechnologiesModel> response = JsonConvert.DeserializeObject<ResponseModel<TechnologiesModel>>(jsonResult);
			technologiesModel = response.Value;
			return technologiesModel;

		}

		public string DeleteTechnologies(TechnologiesModel model)
		{
			string technologiesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Technologies/DeleteTechnologies";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			technologiesModel = response.Value;
			return technologiesModel;

		}

		public List<TechnologiesModel> GetAllTechnologies(long UserId)
		{
			List<TechnologiesModel> technologiesModel = new List<TechnologiesModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Technologies/GetAllTechnologies/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<TechnologiesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<TechnologiesModel>>>(jsonResult);
			technologiesModel = response.Value;
			return technologiesModel;

		}

		public ResponseModel<string> SaveTechnologie(TechnologiesModel model)
		{
			string technologiesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Technologies/SaveTechnologie";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
