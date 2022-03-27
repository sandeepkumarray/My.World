using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class UniversesService : IUniversesService
	{
		public DBContext dbContext;


		public UniversesService()
		{
		}

		public  UniversesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddUniversesData(UniversesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDalobj = new UniversesDAL(dbContext);
                string value = UniversesDalobj.AddUniversesData(Data);
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

		public ResponseModel<UniversesModel> GetUniversesData(UniversesModel Data)
		{
			ResponseModel<UniversesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UniversesModel>();
                UniversesDAL UniversesDalobj = new UniversesDAL(dbContext);
                UniversesModel value = UniversesDalobj.GetUniversesData(Data);
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

		public ResponseModel<string> DeleteUniversesData(UniversesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDalobj = new UniversesDAL(dbContext);
                string value = UniversesDalobj.DeleteUniversesData(Data);
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

		public ResponseModel<List<UniversesModel >> GetAllUniversesForUserID(long userId)
		{
			ResponseModel<List<UniversesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<UniversesModel >>();
                UniversesDAL UniversesDalobj = new UniversesDAL(dbContext);
                List<UniversesModel> value = UniversesDalobj.GetAllUniversesForUserID(userId);
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

		public ResponseModel<string> SaveUniverse(UniversesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDalobj = new UniversesDAL(dbContext);
                string value = UniversesDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateUniversesData(UniversesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UniversesDAL UniversesDalobj = new UniversesDAL(dbContext);
                string value = UniversesDalobj.UpdateUniversesData(Data);
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
