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
	public class OrganizationsApiService : BaseAPIService, IOrganizationsApiService
	{

		public string AddOrganizations(OrganizationsModel model)
		{
			string organizationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Organizations/AddOrganizations";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			organizationsModel = response.Value;
			return organizationsModel;

		}

		public OrganizationsModel GetOrganizations(OrganizationsModel model)
		{
			OrganizationsModel organizationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Organizations/GetOrganizations";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<OrganizationsModel> response = JsonConvert.DeserializeObject<ResponseModel<OrganizationsModel>>(jsonResult);
			organizationsModel = response.Value;
			return organizationsModel;

		}

		public string DeleteOrganizations(OrganizationsModel model)
		{
			string organizationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Organizations/DeleteOrganizations";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			organizationsModel = response.Value;
			return organizationsModel;

		}

		public List<OrganizationsModel> GetAllOrganizations(long UserId)
		{
			List<OrganizationsModel> organizationsModel = new List<OrganizationsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Organizations/GetAllOrganizations/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<OrganizationsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<OrganizationsModel>>>(jsonResult);
			organizationsModel = response.Value;
			return organizationsModel;

		}

		public ResponseModel<string> SaveOrganization(OrganizationsModel model)
		{
			string organizationsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Organizations/SaveOrganization";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
