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
	public class ContentobjectattachmentApiService : BaseAPIService, IContentobjectattachmentApiService
	{

		public string AddContentObjectAttachment(ContentObjectAttachmentModel model)
		{
						string contentobjectattachmentModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddContentObjectAttachment";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contentobjectattachmentModel = response.Value;
						return contentobjectattachmentModel;

		}

		public ContentObjectAttachmentModel GetContentObjectAttachment(ContentObjectAttachmentModel model)
		{
						ContentObjectAttachmentModel contentobjectattachmentModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetContentObjectAttachment";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<ContentObjectAttachmentModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentObjectAttachmentModel>>(jsonResult);
						contentobjectattachmentModel = response.Value;
						return contentobjectattachmentModel;

		}

		public string DeleteContentObjectAttachment(ContentObjectAttachmentModel model)
		{
						string contentobjectattachmentModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteContentObjectAttachment";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						contentobjectattachmentModel = response.Value;
						return contentobjectattachmentModel;

		}

		public List<ContentObjectAttachmentModel> GetAllContentObjectAttachment(long UserId)
		{
						List<ContentObjectAttachmentModel> contentobjectattachmentModel = new List<ContentObjectAttachmentModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllContentObjectAttachment/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<ContentObjectAttachmentModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContentObjectAttachmentModel>>>(jsonResult);
						contentobjectattachmentModel = response.Value;
						return contentobjectattachmentModel;

		}

		public ResponseModel<string> SaveContentObjectAttachmen(ContentObjectAttachmentModel model)
		{
						string contentobjectattachmentModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveContentObjectAttachmen";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
