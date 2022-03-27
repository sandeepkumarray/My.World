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
	public class ContentPlansApiService : BaseAPIService, IContentPlansApiService
	{

		public string AddContentPlans(ContentPlansModel model)
		{
						string contentplansModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddContentPlans";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contentplansModel = response.Value;
						return contentplansModel;

		}

		public ContentPlansModel GetContentPlans(ContentPlansModel model)
		{
						ContentPlansModel contentplansModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetContentPlans";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<ContentPlansModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentPlansModel>>(jsonResult);
						contentplansModel = response.Value;
						return contentplansModel;

		}

		public string DeleteContentPlans(ContentPlansModel model)
		{
						string contentplansModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteContentPlans";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contentplansModel = response.Value;
						return contentplansModel;

		}

		public List<ContentPlansModel> GetAllContentPlans()
		{
						List<ContentPlansModel> contentplansModel = new List<ContentPlansModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllContentPlans";
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<ContentPlansModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContentPlansModel>>>(jsonResult);
						contentplansModel = response.Value;
						return contentplansModel;

		}

		public ResponseModel<string> SaveContentPlan(ContentPlansModel model)
		{
						string contentplansModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveContentPlan";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
