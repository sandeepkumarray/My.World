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
	public class ContentobjectApiService : BaseAPIService, IContentobjectApiService
	{

		public string AddContentObject(ContentObjectModel model)
		{
						string contentobjectModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddContentObject";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contentobjectModel = response.Value;
						return contentobjectModel;

		}

		public ContentObjectModel GetContentObject(ContentObjectModel model)
		{
						ContentObjectModel contentobjectModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetContentObject";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<ContentObjectModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentObjectModel>>(jsonResult);
						contentobjectModel = response.Value;
						return contentobjectModel;

		}

		public string DeleteContentObject(ContentObjectModel model)
		{
						string contentobjectModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteContentObject";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contentobjectModel = response.Value;
						return contentobjectModel;

		}

		public List<ContentObjectModel> GetAllContentObject(long UserId)
		{
						List<ContentObjectModel> contentobjectModel = new List<ContentObjectModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllContentObject/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<ContentObjectModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContentObjectModel>>>(jsonResult);
						contentobjectModel = response.Value;
						return contentobjectModel;

		}

		public ResponseModel<string> SaveContentObjec(ContentObjectModel model)
		{
						string contentobjectModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveContentObjec";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
