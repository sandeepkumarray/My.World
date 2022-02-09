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
	public class TownsApiService : BaseAPIService, ITownsApiService
	{

		public string AddTowns(TownsModel model)
		{
						string townsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddTowns";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						townsModel = response.Value;
						return townsModel;

		}

		public TownsModel GetTowns(TownsModel model)
		{
						TownsModel townsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetTowns";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<TownsModel> response = JsonConvert.DeserializeObject<ResponseModel<TownsModel>>(jsonResult);
						townsModel = response.Value;
						return townsModel;

		}

		public string DeleteTowns(TownsModel model)
		{
						string townsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteTowns";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						townsModel = response.Value;
						return townsModel;

		}

		public List<TownsModel> GetAllTowns(long UserId)
		{
						List<TownsModel> townsModel = new List<TownsModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllTowns/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<TownsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<TownsModel>>>(jsonResult);
						townsModel = response.Value;
						return townsModel;

		}

		public ResponseModel<string> SaveTown(TownsModel model)
		{
						string townsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveTown";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
