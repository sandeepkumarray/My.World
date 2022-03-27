using My.World.Api.Models;
using My.World.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public class DashboardApiService : BaseAPIService, IDashboardApiService
    {
        public DashboardModel GetDashboard(DashboardModel model)
        {
            DashboardModel dashboardModel = null;
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetDashboard";
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<DashboardModel> response = JsonConvert.DeserializeObject<ResponseModel<DashboardModel>>(jsonResult);
            dashboardModel = response.Value;
            return dashboardModel;

        }

        public Int64 CreateItem(string controller, UsersModel userAccount)
        {
            ResponseModel<string> return_value = new Api.Models.ResponseModel<string>();
            long id = 0;
            string apiUrl = controller + "/Add" + controller;
            Object model = null;

            switch (controller)
            {
                case "Characters":
                    model = new CharactersModel() { Name = "New Characters", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Buildings":
                    model = new BuildingsModel() { Name = "New Buildings", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Conditions":
                    model = new ConditionsModel() { Name = "New Conditions", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Continents":
                    model = new ContinentsModel() { Local_name = "New Continents", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Countries":
                    model = new CountriesModel() { Name = "New Countries", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Creatures":
                    model = new CreaturesModel() { Name = "New Creatures", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Deities":
                    model = new DeitiesModel() { Name = "New Deities", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Floras":
                    model = new FlorasModel() { Name = "New Floras", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Foods":
                    model = new FoodsModel() { Name = "New Foods", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Governments":
                    model = new GovernmentsModel() { Name = "New Governments", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Groups":
                    model = new GroupsModel() { Name = "New Groups", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Items":
                    model = new ItemsModel() { Name = "New Items", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Jobs":
                    model = new JobsModel() { Name = "New Jobs", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Landmarks":
                    model = new LandmarksModel() { Name = "New Landmarks", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Languages":
                    model = new LanguagesModel() { Name = "New Languages", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Locations":
                    model = new LocationsModel() { Name = "New Locations", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Lores":
                    model = new LoresModel() { Name = "New Lores", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Magics":
                    model = new MagicsModel() { Name = "New Magics", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Planets":
                    model = new PlanetsModel() { Name = "New Planets", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Races":
                    model = new RacesModel() { Name = "New Races", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Religions":
                    model = new ReligionsModel() { Name = "New Religions", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Scenes":
                    model = new ScenesModel() { Name = "New Scenes", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Sports":
                    model = new SportsModel() { Name = "New Sports", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Technologies":
                    model = new TechnologiesModel() { Name = "New Technologies", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Towns":
                    model = new TownsModel() { Name = "New Towns", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Traditions":
                    model = new TraditionsModel() { Name = "New Traditions", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Universes":
                    model = new UniversesModel() { name = "New Universe", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Vehicles":
                    model = new VehiclesModel() { Name = "New Vehicles", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                case "Organizations":
                    model = new OrganizationsModel() { Name = "New Organizations", created_at = DateTime.Now, updated_at = DateTime.Now, user_id = userAccount.id };
                    break;

                default:
                    break;
            }

            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldContentApiUrl;
            client.ApiUrl = apiUrl;
            client.ServiceMethod = Method.POST;
            client.RequestBody = model;
            string jsonResult = client.GetResponseAsync();
            ResponseModel<string> response = JsonConvert.DeserializeObject<ResponseModel<string>>(jsonResult);
            id = Convert.ToInt64(response.Value);
            return id;
        }

        public List<MentionsModel> GetMentionsData(long UserId)
        {
            List<MentionsModel> mentionsList = new List<MentionsModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetMentions/" + UserId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<MentionsModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<MentionsModel>>>(jsonResult);
            mentionsList = response.Value;
            return mentionsList;
        }
        public List<DashboardRecentModel> GetRecentsData(long UserId)
        {
            List<DashboardRecentModel> mentionsList = new List<DashboardRecentModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetRecents/" + UserId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<DashboardRecentModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<DashboardRecentModel>>>(jsonResult);
            mentionsList = response.Value;
            return mentionsList;
        }

        public List<ContentTypesModel> GetAllContentTypes()
        {
            List<ContentTypesModel> contentTypesList = new List<ContentTypesModel>();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetAllContentTypes";
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<List<ContentTypesModel>> response = JsonConvert.DeserializeObject<ResponseModel<List<ContentTypesModel>>>(jsonResult);
            contentTypesList = response.Value;
            return contentTypesList;
        }
        public BaseModel GetContentDetailsFromTypeID(string contentType, string contentId)
        {
            BaseModel contentTypesList = new BaseModel();
            RestHttpClient client = new RestHttpClient();
            client.Host = MyWorldApiUrl;
            client.ApiUrl = "GetContentDetailsFromTypeID/" + contentType +"/" + contentId;
            client.ServiceMethod = Method.GET;
            string jsonResult = client.GETAsync();
            ResponseModel<BaseModel> response = JsonConvert.DeserializeObject<ResponseModel<BaseModel>>(jsonResult);
            contentTypesList = response.Value;
            return contentTypesList;

        }
    }
}
