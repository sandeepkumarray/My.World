using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class TechnologiesService : ITechnologiesService
	{
		public DBContext dbContext;


		public TechnologiesService()
		{
		}

		public  TechnologiesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddTechnologiesData(TechnologiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TechnologiesDAL TechnologiesDalobj = new TechnologiesDAL(dbContext);
                string value = TechnologiesDalobj.AddTechnologiesData(Data);
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

		public ResponseModel<TechnologiesModel> GetTechnologiesData(TechnologiesModel Data)
		{
			ResponseModel<TechnologiesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<TechnologiesModel>();
                TechnologiesDAL TechnologiesDalobj = new TechnologiesDAL(dbContext);
                TechnologiesModel value = TechnologiesDalobj.GetTechnologiesData(Data);
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

		public ResponseModel<string> DeleteTechnologiesData(TechnologiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TechnologiesDAL TechnologiesDalobj = new TechnologiesDAL(dbContext);
                string value = TechnologiesDalobj.DeleteTechnologiesData(Data);
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

		public ResponseModel<List<TechnologiesModel >> GetAllTechnologiesForUserID(long userId)
		{
			ResponseModel<List<TechnologiesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<TechnologiesModel >>();
                TechnologiesDAL TechnologiesDalobj = new TechnologiesDAL(dbContext);
                List<TechnologiesModel> value = TechnologiesDalobj.GetAllTechnologiesForUserID(userId);
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

		public ResponseModel<string> SaveTechnologie(TechnologiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TechnologiesDAL TechnologiesDalobj = new TechnologiesDAL(dbContext);
                string value = TechnologiesDalobj.SaveData(Data);
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
