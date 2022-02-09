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
	public class TraditionsApiService : BaseAPIService, ITraditionsApiService
	{

		public string AddTraditions(TraditionsModel model)
		{
						string traditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddTraditions";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						traditionsModel = response.Value;
						return traditionsModel;

		}

		public TraditionsModel GetTraditions(TraditionsModel model)
		{
						TraditionsModel traditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetTraditions";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<TraditionsModel> response = JsonConvert.DeserializeObject<ResponseModel<TraditionsModel>>(jsonResult);
						traditionsModel = response.Value;
						return traditionsModel;

		}

		public string DeleteTraditions(TraditionsModel model)
		{
						string traditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteTraditions";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						traditionsModel = response.Value;
						return traditionsModel;

		}

		public List<TraditionsModel> GetAllTraditions(long UserId)
		{
						List<TraditionsModel> traditionsModel = new List<TraditionsModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllTraditions/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<TraditionsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<TraditionsModel>>>(jsonResult);
						traditionsModel = response.Value;
						return traditionsModel;

		}

		public ResponseModel<string> SaveTradition(TraditionsModel model)
		{
						string traditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveTradition";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
