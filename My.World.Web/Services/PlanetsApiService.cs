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
	public class PlanetsApiService : BaseAPIService, IPlanetsApiService
	{

		public string AddPlanets(PlanetsModel model)
		{
			string planetsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Planets/AddPlanets";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			planetsModel = response.Value;
			return planetsModel;

		}

		public PlanetsModel GetPlanets(PlanetsModel model)
		{
			PlanetsModel planetsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Planets/GetPlanets";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<PlanetsModel> response = JsonConvert.DeserializeObject<ResponseModel<PlanetsModel>>(jsonResult);
			planetsModel = response.Value;
			return planetsModel;

		}

		public string DeletePlanets(PlanetsModel model)
		{
			string planetsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Planets/DeletePlanets";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			planetsModel = response.Value;
			return planetsModel;

		}

		public List<PlanetsModel> GetAllPlanets(long UserId)
		{
			List<PlanetsModel> planetsModel = new List<PlanetsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Planets/GetAllPlanets/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<PlanetsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<PlanetsModel>>>(jsonResult);
			planetsModel = response.Value;
			return planetsModel;

		}

		public ResponseModel<string> SavePlanet(PlanetsModel model)
		{
			string planetsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Planets/SavePlanet";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
