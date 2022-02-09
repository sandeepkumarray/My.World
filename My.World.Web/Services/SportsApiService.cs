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
	public class SportsApiService : BaseAPIService, ISportsApiService
	{

		public string AddSports(SportsModel model)
		{
						string sportsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddSports";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						sportsModel = response.Value;
						return sportsModel;

		}

		public SportsModel GetSports(SportsModel model)
		{
						SportsModel sportsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetSports";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<SportsModel> response = JsonConvert.DeserializeObject<ResponseModel<SportsModel>>(jsonResult);
						sportsModel = response.Value;
						return sportsModel;

		}

		public string DeleteSports(SportsModel model)
		{
						string sportsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteSports";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						sportsModel = response.Value;
						return sportsModel;

		}

		public List<SportsModel> GetAllSports(long UserId)
		{
						List<SportsModel> sportsModel = new List<SportsModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllSports/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<SportsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<SportsModel>>>(jsonResult);
						sportsModel = response.Value;
						return sportsModel;

		}

		public ResponseModel<string> SaveSport(SportsModel model)
		{
						string sportsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveSport";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
