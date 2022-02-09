using My.World.Api.DataAccess;
using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Api.Services
{
    public class DashboardService
    {
        public DBContext dbContext { get; set; }

        public DashboardService(DBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        internal ResponseModel<DashboardModel> GetDashboard(DashboardModel Data)
        {
            ResponseModel<DashboardModel> return_value = null;
            try
            {
                return_value = new ResponseModel<DashboardModel>();
                DashboardDAL DashboardDALObj = new DashboardDAL(dbContext);
                DashboardModel value = DashboardDALObj.GetDashboardData(Data.User_Id);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;
        }

        internal ResponseModel<List<MentionsModel>> GetMentions(long userId)
        {
            ResponseModel<List<MentionsModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<MentionsModel>>();
                DashboardDAL DashboardDALObj = new DashboardDAL(dbContext);
                List<MentionsModel> value = DashboardDALObj.GetMentionsData(userId);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;
        }

        internal ResponseModel<List<DashboardRecentModel>> GetRecentData(long userId)
        {
            ResponseModel<List<DashboardRecentModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<DashboardRecentModel>>();
                DashboardDAL DashboardDALObj = new DashboardDAL(dbContext);
                List<DashboardRecentModel> value = DashboardDALObj.GetRecentData(userId);
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;
        }

        public ResponseModel<List<ContentTypesModel>> GetAllContentTypes()
        {
            ResponseModel<List<ContentTypesModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContentTypesModel>>();
                DashboardDAL DashboardDALObj = new DashboardDAL(dbContext);
                List<ContentTypesModel> value = DashboardDALObj.GetAllContentTypes();
                return_value.Value = value;
                return_value.Message = "Success";
                return_value.HttpStatusCode = "200";
                return_value.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            return return_value;
        }
    }
}
