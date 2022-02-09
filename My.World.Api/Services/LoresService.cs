using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class LoresService : ILoresService
	{
		public DBContext dbContext;


		public LoresService()
		{
		}

		public  LoresService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddLoresData(LoresModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LoresDAL LoresDalobj = new LoresDAL(dbContext);
                string value = LoresDalobj.AddLoresData(Data);
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

		public ResponseModel<LoresModel> GetLoresData(LoresModel Data)
		{
			ResponseModel<LoresModel> return_value = null;
            try
            {
                return_value = new ResponseModel<LoresModel>();
                LoresDAL LoresDalobj = new LoresDAL(dbContext);
                LoresModel value = LoresDalobj.GetLoresData(Data);
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

		public ResponseModel<string> DeleteLoresData(LoresModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LoresDAL LoresDalobj = new LoresDAL(dbContext);
                string value = LoresDalobj.DeleteLoresData(Data);
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

		public ResponseModel<List<LoresModel >> GetAllLoresForUserID(long userId)
		{
			ResponseModel<List<LoresModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<LoresModel >>();
                LoresDAL LoresDalobj = new LoresDAL(dbContext);
                List<LoresModel> value = LoresDalobj.GetAllLoresForUserID(userId);
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

		public ResponseModel<string> SaveLore(LoresModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LoresDAL LoresDalobj = new LoresDAL(dbContext);
                string value = LoresDalobj.SaveData(Data);
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
