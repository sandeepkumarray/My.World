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
	public class CountriesApiService : BaseAPIService, ICountriesApiService
	{

		public string AddCountries(CountriesModel model)
		{
			string countriesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Countries/AddCountries";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			countriesModel = response.Value;
			return countriesModel;

		}

		public CountriesModel GetCountries(CountriesModel model)
		{
			CountriesModel countriesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Countries/GetCountries";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<CountriesModel> response = JsonConvert.DeserializeObject<ResponseModel<CountriesModel>>(jsonResult);
			countriesModel = response.Value;
			return countriesModel;

		}

		public string DeleteCountries(CountriesModel model)
		{
			string countriesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Countries/DeleteCountries";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			countriesModel = response.Value;
			return countriesModel;

		}

		public List<CountriesModel> GetAllCountries(long UserId)
		{
			List<CountriesModel> countriesModel = new List<CountriesModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Countries/GetAllCountries/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<CountriesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<CountriesModel>>>(jsonResult);
			countriesModel = response.Value;
			return countriesModel;

		}

		public ResponseModel<string> SaveCountrie(CountriesModel model)
		{
			string countriesModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Countries/SaveCountrie";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
