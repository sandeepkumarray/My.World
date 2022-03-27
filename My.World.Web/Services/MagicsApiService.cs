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
	public class MagicsApiService : BaseAPIService, IMagicsApiService
	{

		public string AddMagics(MagicsModel model)
		{
			string magicsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Magics/AddMagics";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			magicsModel = response.Value;
			return magicsModel;

		}

		public MagicsModel GetMagics(MagicsModel model)
		{
			MagicsModel magicsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Magics/GetMagics";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<MagicsModel> response = JsonConvert.DeserializeObject<ResponseModel<MagicsModel>>(jsonResult);
			magicsModel = response.Value;
			return magicsModel;

		}

		public string DeleteMagics(MagicsModel model)
		{
			string magicsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Magics/DeleteMagics";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			magicsModel = response.Value;
			return magicsModel;

		}

		public List<MagicsModel> GetAllMagics(long UserId)
		{
			List<MagicsModel> magicsModel = new List<MagicsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Magics/GetAllMagics/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<MagicsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<MagicsModel>>>(jsonResult);
			magicsModel = response.Value;
			return magicsModel;

		}

		public ResponseModel<string> SaveMagic(MagicsModel model)
		{
			string magicsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Magics/SaveMagic";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
