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
	public class FoodsApiService : BaseAPIService, IFoodsApiService
	{

		public string AddFoods(FoodsModel model)
		{
						string foodsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "AddFoods";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						foodsModel = response.Value;
						return foodsModel;

		}

		public FoodsModel GetFoods(FoodsModel model)
		{
						FoodsModel foodsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetFoods";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<FoodsModel> response = JsonConvert.DeserializeObject<ResponseModel<FoodsModel>>(jsonResult);
						foodsModel = response.Value;
						return foodsModel;

		}

		public string DeleteFoods(FoodsModel model)
		{
						string foodsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "DeleteFoods";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						foodsModel = response.Value;
						return foodsModel;

		}

		public List<FoodsModel> GetAllFoods(long UserId)
		{
						List<FoodsModel> foodsModel = new List<FoodsModel>();
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "GetAllFoods/" + UserId;
						client.ServiceMethod = Method.GET;
						string jsonResult = client.GETAsync();
						ResponseModel<List<FoodsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<FoodsModel>>>(jsonResult);
						foodsModel = response.Value;
						return foodsModel;

		}

		public ResponseModel<string> SaveFood(FoodsModel model)
		{
						string foodsModel = null;
						RestHttpClient client = new RestHttpClient();
						client.Host = MyWorldApiUrl;
						client.ApiUrl = "SaveFood";
						client.ServiceMethod = Method.POST;
						client.RequestBody = model;
						string jsonResult = client.GetResponseAsync();
						ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
						return response;

		}

	}
}
