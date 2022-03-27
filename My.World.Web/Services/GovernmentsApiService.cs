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
	public class GovernmentsApiService : BaseAPIService, IGovernmentsApiService
	{

		public string AddGovernments(GovernmentsModel model)
		{
			string governmentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Governments/AddGovernments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			governmentsModel = response.Value;
			return governmentsModel;

		}

		public GovernmentsModel GetGovernments(GovernmentsModel model)
		{
			GovernmentsModel governmentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Governments/GetGovernments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<GovernmentsModel> response = JsonConvert.DeserializeObject<ResponseModel<GovernmentsModel>>(jsonResult);
			governmentsModel = response.Value;
			return governmentsModel;

		}

		public string DeleteGovernments(GovernmentsModel model)
		{
			string governmentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Governments/DeleteGovernments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			governmentsModel = response.Value;
			return governmentsModel;

		}

		public List<GovernmentsModel> GetAllGovernments(long UserId)
		{
			List<GovernmentsModel> governmentsModel = new List<GovernmentsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Governments/GetAllGovernments/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<GovernmentsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<GovernmentsModel>>>(jsonResult);
			governmentsModel = response.Value;
			return governmentsModel;

		}

		public ResponseModel<string> SaveGovernment(GovernmentsModel model)
		{
			string governmentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Governments/SaveGovernment";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
