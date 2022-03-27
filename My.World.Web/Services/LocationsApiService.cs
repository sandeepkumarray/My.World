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
	public class LocationsApiService : BaseAPIService, ILocationsApiService
	{

		public string AddLocations(LocationsModel model)
		{
			string locationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Locations/AddLocations";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			locationsModel = response.Value;
			return locationsModel;

		}

		public LocationsModel GetLocations(LocationsModel model)
		{
			LocationsModel locationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Locations/GetLocations";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<LocationsModel> response = JsonConvert.DeserializeObject<ResponseModel<LocationsModel>>(jsonResult);
			locationsModel = response.Value;
			return locationsModel;

		}

		public string DeleteLocations(LocationsModel model)
		{
			string locationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Locations/DeleteLocations";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			locationsModel = response.Value;
			return locationsModel;

		}

		public List<LocationsModel> GetAllLocations(long UserId)
		{
			List<LocationsModel> locationsModel = new List<LocationsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Locations/GetAllLocations/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<LocationsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<LocationsModel>>>(jsonResult);
			locationsModel = response.Value;
			return locationsModel;

		}

		public ResponseModel<string> SaveLocation(LocationsModel model)
		{
			string locationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Locations/SaveLocation";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
