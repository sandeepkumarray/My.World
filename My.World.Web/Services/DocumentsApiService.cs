using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace My.World.Web.Services
{
	public class DocumentsApiService : BaseAPIService, IDocumentsApiService
	{

		public Int64 Adddocuments(DocumentsModel model)
		{
			long id = 0;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "Adddocuments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			id = Convert.ToInt64(response.Value);
			return id;

		}

		public string Updatedocuments(DocumentsModel model)
		{
			string DocumentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "Updatedocuments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			DocumentsModel = response.Value;
			return DocumentsModel;

		}

		public DocumentsModel Getdocuments(DocumentsModel model)
		{
			DocumentsModel DocumentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "Getdocuments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<DocumentsModel> response = JsonConvert.DeserializeObject<ResponseModel<DocumentsModel>>(jsonResult);
			DocumentsModel = response.Value;
			return DocumentsModel;

		}

		public string Deletedocuments(DocumentsModel model)
		{
			string DocumentsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "Deletedocuments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			DocumentsModel = response.Value;
			return DocumentsModel;

		}

		public List<DocumentsModel> GetAlldocuments(long User_Id)
		{
			List<DocumentsModel> DocumentsModel = new List<DocumentsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "GetAlldocuments/" + User_Id;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<DocumentsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<DocumentsModel>>>(jsonResult);
			DocumentsModel = response.Value;
			return DocumentsModel;

		}

		public List<DocumentsModel> GetAllFolderDocuments(long User_Id, long FolderId)
		{
			List<DocumentsModel> DocumentsModel = new List<DocumentsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "GetAllFolderDocuments/" + User_Id + "/" + FolderId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<DocumentsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<DocumentsModel>>>(jsonResult);
			DocumentsModel = response.Value;
			return DocumentsModel;

		}

		public ResponseModel<string> Savedocuments(DocumentsModel model)
		{
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "Savedocuments";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;
		}
	}
}
