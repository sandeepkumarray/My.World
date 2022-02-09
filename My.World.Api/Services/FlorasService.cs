using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class FlorasService : IFlorasService
	{
		public DBContext dbContext;


		public FlorasService()
		{
		}

		public  FlorasService(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

		public ResponseModel<string> AddFlorasData(FlorasModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FlorasDAL FlorasDalobj = new FlorasDAL(dbContext);
                string value = FlorasDalobj.AddFlorasData(Data);
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

		public ResponseModel<FlorasModel> GetFlorasData(FlorasModel Data)
		{
			ResponseModel<FlorasModel> return_value = null;
            try
            {
                return_value = new ResponseModel<FlorasModel>();
                FlorasDAL FlorasDalobj = new FlorasDAL(dbContext);
                FlorasModel value = FlorasDalobj.GetFlorasData(Data);
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

		public ResponseModel<string> DeleteFlorasData(FlorasModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FlorasDAL FlorasDalobj = new FlorasDAL(dbContext);
                string value = FlorasDalobj.DeleteFlorasData(Data);
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

		public ResponseModel<List<FlorasModel >> GetAllFlorasForUserID(long userId)
		{
			ResponseModel<List<FlorasModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<FlorasModel >>();
                FlorasDAL FlorasDalobj = new FlorasDAL(dbContext);
                List<FlorasModel> value = FlorasDalobj.GetAllFlorasForUserID(userId);
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

		public ResponseModel<string> SaveFlora(FlorasModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                FlorasDAL FlorasDalobj = new FlorasDAL(dbContext);
                string value = FlorasDalobj.SaveData(Data);
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
