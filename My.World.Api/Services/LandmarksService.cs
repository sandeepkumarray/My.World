using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class LandmarksService : ILandmarksService
	{
		public DBContext dbContext;


		public LandmarksService()
		{
		}

		public  LandmarksService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddLandmarksData(LandmarksModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LandmarksDAL LandmarksDalobj = new LandmarksDAL(dbContext);
                string value = LandmarksDalobj.AddLandmarksData(Data);
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

		public ResponseModel<LandmarksModel> GetLandmarksData(LandmarksModel Data)
		{
			ResponseModel<LandmarksModel> return_value = null;
            try
            {
                return_value = new ResponseModel<LandmarksModel>();
                LandmarksDAL LandmarksDalobj = new LandmarksDAL(dbContext);
                LandmarksModel value = LandmarksDalobj.GetLandmarksData(Data);
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

		public ResponseModel<string> DeleteLandmarksData(LandmarksModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LandmarksDAL LandmarksDalobj = new LandmarksDAL(dbContext);
                string value = LandmarksDalobj.DeleteLandmarksData(Data);
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

		public ResponseModel<List<LandmarksModel >> GetAllLandmarksForUserID(long userId)
		{
			ResponseModel<List<LandmarksModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<LandmarksModel >>();
                LandmarksDAL LandmarksDalobj = new LandmarksDAL(dbContext);
                List<LandmarksModel> value = LandmarksDalobj.GetAllLandmarksForUserID(userId);
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

		public ResponseModel<string> SaveLandmark(LandmarksModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LandmarksDAL LandmarksDalobj = new LandmarksDAL(dbContext);
                string value = LandmarksDalobj.SaveData(Data);
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
