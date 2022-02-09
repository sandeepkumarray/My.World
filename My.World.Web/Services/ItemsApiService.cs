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
	public class ItemsApiService : BaseAPIService, IItemsApiService
	{
		public ItemsModel GetItems(ItemsModel model)
		{
			ItemsModel itemsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "GetItems";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<ItemsModel> response = JsonConvert.DeserializeObject<ResponseModel<ItemsModel>>(jsonResult);
			itemsModel = response.Value;
			return itemsModel;

		}

		public string DeleteItems(ItemsModel model)
		{
			string itemsModel = null;
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "DeleteItems";
			client.ServiceMethod = Method.POST;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			itemsModel = response.Value;
			return itemsModel;

		}

		public List<ItemsModel> GetAllItems(long UserId)
		{
			List<ItemsModel> itemsModel = new List<ItemsModel>();
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "GetAllItems/" + UserId;

			client.ServiceMethod = Method.GET;
			string jsonResult = client.GETAsync();
			ResponseModel<List<ItemsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ItemsModel>>>(jsonResult);
			itemsModel = response.Value;
			return itemsModel;

		}
		
		public ResponseModel<string> SaveItem(ItemsModel model)
		{
			RestHttpClient client = new RestHttpClient();
			client.Host = MyWorldApiUrl;
			client.ApiUrl = "SaveItem";
			client.ServiceMethod = Method.POST;
			model.updated_at = DateTime.Now;
			client.RequestBody = model;
			string jsonResult = client.GetResponseAsync();
			ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
			return response;
		}
	}
}
