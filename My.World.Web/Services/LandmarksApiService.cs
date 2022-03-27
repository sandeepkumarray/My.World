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
	public class LandmarksApiService : BaseAPIService, ILandmarksApiService
	{

		public string AddLandmarks(LandmarksModel model)
		{
			string landmarksModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Landmarks/AddLandmarks";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			landmarksModel = response.Value;
			return landmarksModel;

		}

		public LandmarksModel GetLandmarks(LandmarksModel model)
		{
			LandmarksModel landmarksModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Landmarks/GetLandmarks";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<LandmarksModel> response = JsonConvert.DeserializeObject<ResponseModel<LandmarksModel>>(jsonResult);
			landmarksModel = response.Value;
			return landmarksModel;

		}

		public string DeleteLandmarks(LandmarksModel model)
		{
			string landmarksModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Landmarks/DeleteLandmarks";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			landmarksModel = response.Value;
			return landmarksModel;

		}

		public List<LandmarksModel> GetAllLandmarks(long UserId)
		{
			List<LandmarksModel> landmarksModel = new List<LandmarksModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Landmarks/GetAllLandmarks/" + UserId;
			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<LandmarksModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<LandmarksModel>>>(jsonResult);
			landmarksModel = response.Value;
			return landmarksModel;

		}

		public ResponseModel<string> SaveLandmark(LandmarksModel model)
		{
			string landmarksModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldContentApiUrl;
			client.ApiUrl = "Landmarks/SaveLandmark";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;

		}

	}
}
