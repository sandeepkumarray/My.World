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
	public class LanguagesApiService : BaseAPIService, ILanguagesApiService
	{

		public string AddLanguages(LanguagesModel model)
		{
						string languagesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddLanguages";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						languagesModel = response.Value;
						return languagesModel;

		}

		public LanguagesModel GetLanguages(LanguagesModel model)
		{
						LanguagesModel languagesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetLanguages";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<LanguagesModel> response = JsonConvert.DeserializeObject<ResponseModel<LanguagesModel>>(jsonResult);
						languagesModel = response.Value;
						return languagesModel;

		}

		public string DeleteLanguages(LanguagesModel model)
		{
						string languagesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteLanguages";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						languagesModel = response.Value;
						return languagesModel;

		}

		public List<LanguagesModel> GetAllLanguages(long UserId)
		{
						List<LanguagesModel> languagesModel = new List<LanguagesModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllLanguages/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<LanguagesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<LanguagesModel>>>(jsonResult);
						languagesModel = response.Value;
						return languagesModel;

		}

		public ResponseModel<string> SaveLanguage(LanguagesModel model)
		{
						string languagesModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveLanguage";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
