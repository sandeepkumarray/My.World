using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ScenesService : IScenesService
	{
		public DBContext dbContext;


		public ScenesService()
		{
		}

		public  ScenesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddScenesData(ScenesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ScenesDAL ScenesDalobj = new ScenesDAL(dbContext);
                string value = ScenesDalobj.AddScenesData(Data);
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

		public ResponseModel<ScenesModel> GetScenesData(ScenesModel Data)
		{
			ResponseModel<ScenesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ScenesModel>();
                ScenesDAL ScenesDalobj = new ScenesDAL(dbContext);
                ScenesModel value = ScenesDalobj.GetScenesData(Data);
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

		public ResponseModel<string> DeleteScenesData(ScenesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ScenesDAL ScenesDalobj = new ScenesDAL(dbContext);
                string value = ScenesDalobj.DeleteScenesData(Data);
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

		public ResponseModel<List<ScenesModel >> GetAllScenesForUserID(long userId)
		{
			ResponseModel<List<ScenesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ScenesModel >>();
                ScenesDAL ScenesDalobj = new ScenesDAL(dbContext);
                List<ScenesModel> value = ScenesDalobj.GetAllScenesForUserID(userId);
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

		public ResponseModel<string> SaveScene(ScenesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ScenesDAL ScenesDalobj = new ScenesDAL(dbContext);
                string value = ScenesDalobj.SaveData(Data);
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
