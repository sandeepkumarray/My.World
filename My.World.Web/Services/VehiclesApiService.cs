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
	public class VehiclesApiService : BaseAPIService, IVehiclesApiService
	{

		public string AddVehicles(VehiclesModel model)
		{
						string vehiclesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddVehicles";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						vehiclesModel = response.Value;
						return vehiclesModel;

		}

		public VehiclesModel GetVehicles(VehiclesModel model)
		{
						VehiclesModel vehiclesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetVehicles";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<VehiclesModel> response = JsonConvert.DeserializeObject<ResponseModel<VehiclesModel>>(jsonResult);
						vehiclesModel = response.Value;
						return vehiclesModel;

		}

		public string DeleteVehicles(VehiclesModel model)
		{
						string vehiclesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteVehicles";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						vehiclesModel = response.Value;
						return vehiclesModel;

		}

		public List<VehiclesModel> GetAllVehicles(long UserId)
		{
						List<VehiclesModel> vehiclesModel = new List<VehiclesModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllVehicles/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<VehiclesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<VehiclesModel>>>(jsonResult);
						vehiclesModel = response.Value;
						return vehiclesModel;

		}

		public ResponseModel<string> SaveVehicle(VehiclesModel model)
		{
						string vehiclesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveVehicle";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
