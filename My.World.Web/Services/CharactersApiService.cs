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
	public class CharactersApiService : BaseAPIService, ICharactersApiService
	{

		public string AddCharacters(CharactersModel model)
		{
						string charactersModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddCharacters";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						charactersModel = response.Value;
						return charactersModel;

		}

		public CharactersModel GetCharacters(CharactersModel model)
		{
						CharactersModel charactersModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetCharacters";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<CharactersModel> response = JsonConvert.DeserializeObject<ResponseModel<CharactersModel>>(jsonResult);
						charactersModel = response.Value;
						return charactersModel;

		}

		public string DeleteCharacters(CharactersModel model)
		{
						string charactersModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteCharacters";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						charactersModel = response.Value;
						return charactersModel;

		}

		public List<CharactersModel> GetAllCharacters(long UserId)
		{
						List<CharactersModel> charactersModel = new List<CharactersModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllCharacters/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<CharactersModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<CharactersModel>>>(jsonResult);
						charactersModel = response.Value;
						return charactersModel;

		}

		public ResponseModel<string> SaveCharacter(CharactersModel model)
		{
						string charactersModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveCharacter";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
