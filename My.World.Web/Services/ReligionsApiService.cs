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
	public class ReligionsApiService : BaseAPIService, IReligionsApiService
	{

		public string AddReligions(ReligionsModel model)
		{
			string religionsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Religions/AddReligions";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			religionsModel = response.Value;
			return religionsModel;

		}

		public ReligionsModel GetReligions(ReligionsModel model)
		{
			ReligionsModel religionsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Religions/GetReligions";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<ReligionsModel> response = JsonConvert.DeserializeObject<ResponseModel<ReligionsModel>>(jsonResult);
			religionsModel = response.Value;
			return religionsModel;

		}

		public string DeleteReligions(ReligionsModel model)
		{
			string religionsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Religions/DeleteReligions";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			religionsModel = response.Value;
			return religionsModel;

		}

		public List<ReligionsModel> GetAllReligions(long UserId)
		{
			List<ReligionsModel> religionsModel = new List<ReligionsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Religions/GetAllReligions/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<ReligionsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ReligionsModel>>>(jsonResult);
			religionsModel = response.Value;
			return religionsModel;

		}

		public ResponseModel<string> SaveReligion(ReligionsModel model)
		{
			string religionsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Religions/SaveReligion";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
