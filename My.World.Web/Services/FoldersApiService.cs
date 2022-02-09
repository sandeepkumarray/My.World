using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace My.World.Web.Services
{
    public class FoldersApiService : BaseAPIService, IFoldersApiService
    {

        public Int64 Addfolders(FoldersModel model)
        {
            long id = 0;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "Addfolders";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            id = Convert.ToInt64(response.Value);
            return id;

        }

        public string Updatefolders(FoldersModel model)
        {
            string FoldersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "Updatefolders";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            FoldersModel = response.Value;
            return FoldersModel;

        }

        public FoldersModel Getfolders(FoldersModel model)
        {
            FoldersModel FoldersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "Getfolders";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<FoldersModel> response = JsonConvert.DeserializeObject<ResponseModel<FoldersModel>>(jsonResult);
            FoldersModel = response.Value;
            return FoldersModel;

        }

        public string Deletefolders(FoldersModel model)
        {
            string FoldersModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "Deletefolders";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            FoldersModel = response.Value;
            return FoldersModel;

        }

        public List<FoldersModel> GetAllfolders(long userId)
        {
            List<FoldersModel> FoldersModel = new List<FoldersModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllfolders/" + userId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<FoldersModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<FoldersModel>>>(jsonResult);
            FoldersModel = response.Value;
            return FoldersModel;

        }
        
        public List<FoldersModel> GetAllChildFolders(long folderId)
        {
            List<FoldersModel> FoldersModel = new List<FoldersModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllChildFolders/" + folderId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<FoldersModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<FoldersModel>>>(jsonResult);
            FoldersModel = response.Value;
            return FoldersModel;
        }

        public List<FoldersModel> GetEligibleParentFolders(long userid, long folderId)
        {
            List<FoldersModel> FoldersModel = new List<FoldersModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetEligibleParentFolders/" + userid + "/" + folderId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<FoldersModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<FoldersModel>>>(jsonResult);
            FoldersModel = response.Value;
            return FoldersModel;
        }
    }
}
