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
    public class AppConfigApiService : BaseAPIService, IAppConfigApiService
    {

        public string AddAppConfig(AppConfigModel model)
        {
            string appconfigModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldContentApiUrl;
            client.ApiUrl = "AddAppConfig";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            appconfigModel = response.Value;
            return appconfigModel;
        }

        public AppConfigModel GetAppConfig(AppConfigModel model)
        {
            AppConfigModel appconfigModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAppConfig";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<AppConfigModel> response = JsonConvert.DeserializeObject<ResponseModel<AppConfigModel>>(jsonResult);
            appconfigModel = response.Value;
            return appconfigModel;
        }

        public string DeleteAppConfig(AppConfigModel model)
        {
            string appconfigModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "DeleteAppConfig";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            appconfigModel = response.Value;
            return appconfigModel;
        }

        public List<AppConfigModel> GetAllAppConfig()
        {
            List<AppConfigModel> appconfigModel = new List<AppConfigModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllAppConfig";
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<AppConfigModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<AppConfigModel>>>(jsonResult);
            appconfigModel = response.Value;
            return appconfigModel;
        }

        public ResponseModel<string> SaveAppConfig(AppConfigModel model)
        {
            string appconfigModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "SaveAppconfig";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            return response;
        }

    }
}
