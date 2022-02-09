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
	public class DeitiesApiService : BaseAPIService, IDeitiesApiService
	{

		public string AddDeities(DeitiesModel model)
		{
						string deitiesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddDeities";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						deitiesModel = response.Value;
						return deitiesModel;

		}

		public DeitiesModel GetDeities(DeitiesModel model)
		{
						DeitiesModel deitiesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetDeities";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<DeitiesModel> response = JsonConvert.DeserializeObject<ResponseModel<DeitiesModel>>(jsonResult);
						deitiesModel = response.Value;
						return deitiesModel;

		}

		public string DeleteDeities(DeitiesModel model)
		{
						string deitiesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteDeities";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						deitiesModel = response.Value;
						return deitiesModel;

		}

		public List<DeitiesModel> GetAllDeities(long UserId)
		{
						List<DeitiesModel> deitiesModel = new List<DeitiesModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllDeities/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<DeitiesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<DeitiesModel>>>(jsonResult);
						deitiesModel = response.Value;
						return deitiesModel;

		}

		public ResponseModel<string> SaveDeitie(DeitiesModel model)
		{
						string deitiesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveDeitie";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
