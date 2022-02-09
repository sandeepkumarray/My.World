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
    public class ObjectstoragekeysApiService : BaseAPIService, IObjectstoragekeysApiService
    {

        public string AddObjectStorageKeys(ObjectStorageKeysModel model)
        {
            string objectstoragekeysModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "AddObjectStorageKeys";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            objectstoragekeysModel = response.Value;
            return objectstoragekeysModel;

        }

        public ObjectStorageKeysModel GetObjectStorageKeys(ObjectStorageKeysModel model)
        {
            ObjectStorageKeysModel objectstoragekeysModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetObjectStorageKeys";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<ObjectStorageKeysModel> response = JsonConvert.DeserializeObject<ResponseModel<ObjectStorageKeysModel>>(jsonResult);
            objectstoragekeysModel = response.Value;
            return objectstoragekeysModel;

        }

        public string DeleteObjectStorageKeys(ObjectStorageKeysModel model)
        {
            string objectstoragekeysModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "DeleteObjectStorageKeys";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            objectstoragekeysModel = response.Value;
            return objectstoragekeysModel;

        }

        public List<ObjectStorageKeysModel> GetAllObjectStorageKeys(long UserId)
        {
            List<ObjectStorageKeysModel> objectstoragekeysModel = new List<ObjectStorageKeysModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllObjectStorageKeys/" + UserId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<ObjectStorageKeysModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ObjectStorageKeysModel>>>(jsonResult);
            objectstoragekeysModel = response.Value;
            return objectstoragekeysModel;

        }

        public ResponseModel<string> SaveObjectStorageKey(ObjectStorageKeysModel model)
        {
            string objectstoragekeysModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "SaveObjectStorageKey";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            return response;

        }

    }
}
