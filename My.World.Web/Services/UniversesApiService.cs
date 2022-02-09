using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace My.World.Web.Services
{
    public class UniversesApiService : BaseAPIService, IUniversesApiService
    {
        public string AddUniverses(UniversesModel model)
        {
            string universesModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "AddUniverses";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            universesModel = response.Value;
            return universesModel;

        }

        public string UpdateUniverses(UniversesModel model)
        {
            string universesModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUniverses";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            universesModel = response.Value;
            return universesModel;

        }

        public UniversesModel GetUniverses(UniversesModel model)
        {
            UniversesModel universesModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetUniverses";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<UniversesModel> response = JsonConvert.DeserializeObject<ResponseModel<UniversesModel>>(jsonResult);
            universesModel = response.Value;
            return universesModel;

        }

        public string DeleteUniverses(UniversesModel model)
        {
            string universesModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "DeleteUniverses";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            universesModel = response.Value;
            return universesModel;

        }

        public List<UniversesModel> GetAllUniverses()
        {
            List<UniversesModel> universesModel = new List<UniversesModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllUniverses";
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<List<UniversesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UniversesModel>>>(jsonResult);
            universesModel = response.Value;
            return universesModel;

        }

        public List<UniversesModel> GetAllUniverses(long userId)
        {
            List<UniversesModel> universesModel = new List<UniversesModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllUniversesForUserID/" + userId;

            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<UniversesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UniversesModel>>>(jsonResult);
            universesModel = response.Value;
            return universesModel;
        }

        public ResponseModel<string> SaveUniverse(UniversesModel model)
        {
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "SaveUniverse";
            client.ServiceMethod = Method.POST;
            model.updated_at = DateTime.Now;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            return response;
        }

    }
}
