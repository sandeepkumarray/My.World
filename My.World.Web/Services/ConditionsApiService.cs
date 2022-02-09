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
	public class ConditionsApiService : BaseAPIService, IConditionsApiService
	{

		public string AddConditions(ConditionsModel model)
		{
						string conditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddConditions";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						conditionsModel = response.Value;
						return conditionsModel;

		}

		public ConditionsModel GetConditions(ConditionsModel model)
		{
						ConditionsModel conditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetConditions";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<ConditionsModel> response = JsonConvert.DeserializeObject<ResponseModel<ConditionsModel>>(jsonResult);
						conditionsModel = response.Value;
						return conditionsModel;

		}

		public string DeleteConditions(ConditionsModel model)
		{
						string conditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteConditions";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						conditionsModel = response.Value;
						return conditionsModel;

		}

		public List<ConditionsModel> GetAllConditions(long UserId)
		{
						List<ConditionsModel> conditionsModel = new List<ConditionsModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllConditions/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<ConditionsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ConditionsModel>>>(jsonResult);
						conditionsModel = response.Value;
						return conditionsModel;

		}

		public ResponseModel<string> SaveCondition(ConditionsModel model)
		{
						string conditionsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveCondition";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
