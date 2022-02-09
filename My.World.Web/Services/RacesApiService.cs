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
	public class RacesApiService : BaseAPIService, IRacesApiService
	{

		public string AddRaces(RacesModel model)
		{
						string racesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddRaces";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						racesModel = response.Value;
						return racesModel;

		}

		public RacesModel GetRaces(RacesModel model)
		{
						RacesModel racesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetRaces";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<RacesModel> response = JsonConvert.DeserializeObject<ResponseModel<RacesModel>>(jsonResult);
						racesModel = response.Value;
						return racesModel;

		}

		public string DeleteRaces(RacesModel model)
		{
						string racesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteRaces";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						racesModel = response.Value;
						return racesModel;

		}

		public List<RacesModel> GetAllRaces(long UserId)
		{
						List<RacesModel> racesModel = new List<RacesModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllRaces/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<RacesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<RacesModel>>>(jsonResult);
						racesModel = response.Value;
						return racesModel;

		}

		public ResponseModel<string> SaveRace(RacesModel model)
		{
						string racesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveRace";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
