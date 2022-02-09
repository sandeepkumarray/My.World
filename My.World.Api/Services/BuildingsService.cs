using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class BuildingsService : IBuildingsService
	{
		public DBContext dbContext;


		public BuildingsService()
		{
		}

		public  BuildingsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddBuildingsData(BuildingsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                BuildingsDAL BuildingsDalobj = new BuildingsDAL(dbContext);
                string value = BuildingsDalobj.AddBuildingsData(Data);
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

		public ResponseModel<BuildingsModel> GetBuildingsData(BuildingsModel Data)
		{
			ResponseModel<BuildingsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<BuildingsModel>();
                BuildingsDAL BuildingsDalobj = new BuildingsDAL(dbContext);
                BuildingsModel value = BuildingsDalobj.GetBuildingsData(Data);
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

		public ResponseModel<string> DeleteBuildingsData(BuildingsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                BuildingsDAL BuildingsDalobj = new BuildingsDAL(dbContext);
                string value = BuildingsDalobj.DeleteBuildingsData(Data);
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

		public ResponseModel<List<BuildingsModel >> GetAllBuildingsForUserID(long userId)
		{
			ResponseModel<List<BuildingsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<BuildingsModel >>();
                BuildingsDAL BuildingsDalobj = new BuildingsDAL(dbContext);
                List<BuildingsModel> value = BuildingsDalobj.GetAllBuildingsForUserID(userId);
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

		public ResponseModel<string> SaveBuilding(BuildingsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                BuildingsDAL BuildingsDalobj = new BuildingsDAL(dbContext);
                string value = BuildingsDalobj.SaveData(Data);
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
