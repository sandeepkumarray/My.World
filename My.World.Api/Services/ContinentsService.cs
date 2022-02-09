using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ContinentsService : IContinentsService
	{
		public DBContext dbContext;


		public ContinentsService()
		{
		}

		public  ContinentsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddContinentsData(ContinentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContinentsDAL ContinentsDalobj = new ContinentsDAL(dbContext);
                string value = ContinentsDalobj.AddContinentsData(Data);
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

		public ResponseModel<ContinentsModel> GetContinentsData(ContinentsModel Data)
		{
			ResponseModel<ContinentsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ContinentsModel>();
                ContinentsDAL ContinentsDalobj = new ContinentsDAL(dbContext);
                ContinentsModel value = ContinentsDalobj.GetContinentsData(Data);
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

		public ResponseModel<string> DeleteContinentsData(ContinentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContinentsDAL ContinentsDalobj = new ContinentsDAL(dbContext);
                string value = ContinentsDalobj.DeleteContinentsData(Data);
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

		public ResponseModel<List<ContinentsModel >> GetAllContinentsForUserID(long userId)
		{
			ResponseModel<List<ContinentsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContinentsModel >>();
                ContinentsDAL ContinentsDalobj = new ContinentsDAL(dbContext);
                List<ContinentsModel> value = ContinentsDalobj.GetAllContinentsForUserID(userId);
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

		public ResponseModel<string> SaveContinent(ContinentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContinentsDAL ContinentsDalobj = new ContinentsDAL(dbContext);
                string value = ContinentsDalobj.SaveData(Data);
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
