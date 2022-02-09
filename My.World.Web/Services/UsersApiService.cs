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
    public class UsersApiService : BaseAPIService, IUsersApiService
    {
        public ResponseModel<string> UpdateUsersSecureCode(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersSecureCode";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return response;
        }

        public ResponseModel<UsersModel> UpdateUsersEmailConfirm(UsersModel model)
        {
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersEmailConfirm";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<UsersModel> response = JsonConvert.DeserializeObject<ResponseModel<UsersModel>>(jsonResult);            
            return response;
        }

        public ResponseModel<string> SignupUser(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "SignupUser";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return response;
        }

        public UsersModel LoginUser(UsersModel model)
        {
            UsersModel usersModel = model;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "LoginUser";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<UsersModel> response = JsonConvert.DeserializeObject<ResponseModel<UsersModel>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string AddUsers(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "AddUsers";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string UpdateUsers(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsers";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string UpdateUsersProfile(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersProfile";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string UpdateUsersAccount(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersAccount";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string UpdateUsersPassword(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersPassword";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public UsersModel GetUsers(UsersModel model)
        {
            UsersModel usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetUsers";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<UsersModel> response = JsonConvert.DeserializeObject<ResponseModel<UsersModel>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public UsersModel GetUsersDataByEmail(UsersModel model)
        {
            UsersModel usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetUsersDataByEmail";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<UsersModel> response = JsonConvert.DeserializeObject<ResponseModel<UsersModel>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string DeleteUsers(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "DeleteUsers";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public List<UsersModel> GetAllUsers()
        {
            List<UsersModel> usersModel = new List<UsersModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllUsers";
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<List<UsersModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<UsersModel>>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string UpdateUsersPlan(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersPlan";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public string UpdateUsersContentTemplate(UsersModel model)
        {
            string usersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "UpdateUsersContentTemplate";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            usersModel = response.Value;
            return usersModel;
        }

        public ContentTemplateModel GetUsersContentTemplate(UsersModel model)
        {
            ContentTemplateModel contentTemplateModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetUsersContentTemplate";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<ContentTemplateModel> response = JsonConvert.DeserializeObject<ResponseModel<ContentTemplateModel>>(jsonResult);
            contentTemplateModel = response.Value;
            return contentTemplateModel;
        }

    }
}
