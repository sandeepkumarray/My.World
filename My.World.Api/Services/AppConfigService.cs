using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class AppConfigService : IAppConfigService
	{
		public DBContext dbContext;


		public AppConfigService()
		{
		}

		public  AppConfigService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddAppConfigData(AppConfigModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                AppConfigDAL AppConfigDalobj = new AppConfigDAL(dbContext);
                string value = AppConfigDalobj.AddAppConfigData(Data);
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

		public ResponseModel<AppConfigModel> GetAppConfigData(AppConfigModel Data)
		{
			ResponseModel<AppConfigModel> return_value = null;
            try
            {
                return_value = new ResponseModel<AppConfigModel>();
                AppConfigDAL AppConfigDalobj = new AppConfigDAL(dbContext);
                AppConfigModel value = AppConfigDalobj.GetAppConfigData(Data);
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

		public ResponseModel<string> DeleteAppConfigData(AppConfigModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                AppConfigDAL AppConfigDalobj = new AppConfigDAL(dbContext);
                string value = AppConfigDalobj.DeleteAppConfigData(Data);
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

		public ResponseModel<List<AppConfigModel >> GetAllAppConfigForUserID()
		{
			ResponseModel<List<AppConfigModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<AppConfigModel >>();
                AppConfigDAL AppConfigDalobj = new AppConfigDAL(dbContext);
                List<AppConfigModel> value = AppConfigDalobj.GetAllAppConfigForUserID();
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

		public ResponseModel<string> SaveAppConfig(AppConfigModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                AppConfigDAL AppConfigDalobj = new AppConfigDAL(dbContext);
                string value = AppConfigDalobj.AddAppConfigData(Data);
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

        public ResponseModel<string> UpdateAppConfig(AppConfigModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                AppConfigDAL AppConfigDalobj = new AppConfigDAL(dbContext);
                string value = AppConfigDalobj.UpdateAppConfigData(Data);
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
