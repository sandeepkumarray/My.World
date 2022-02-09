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
	public class ContenttypesApiService : BaseAPIService, IContenttypesApiService
	{

		public string AddContentTypes(ContentTypesModel model)
		{
						string contenttypesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddContentTypes";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contenttypesModel = response.Value;
						return contenttypesModel;

		}

		public ContentTypesModel GetContentTypes(ContentTypesModel model)
		{
						ContentTypesModel contenttypesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetContentTypes";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<ContentTypesModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentTypesModel>>(jsonResult);
						contenttypesModel = response.Value;
						return contenttypesModel;

		}

		public string DeleteContentTypes(ContentTypesModel model)
		{
						string contenttypesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteContentTypes";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contenttypesModel = response.Value;
						return contenttypesModel;

		}

		public List<ContentTypesModel> GetAllContentTypes(long UserId)
		{
						List<ContentTypesModel> contenttypesModel = new List<ContentTypesModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllContentTypes/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<ContentTypesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContentTypesModel>>>(jsonResult);
						contenttypesModel = response.Value;
						return contenttypesModel;

		}

		public ResponseModel<string> SaveContentType(ContentTypesModel model)
		{
						string contenttypesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveContentType";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
