using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class TraditionsService : ITraditionsService
	{
		public DBContext dbContext;


		public TraditionsService()
		{
		}

		public  TraditionsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddTraditionsData(TraditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TraditionsDAL TraditionsDalobj = new TraditionsDAL(dbContext);
                string value = TraditionsDalobj.AddTraditionsData(Data);
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

		public ResponseModel<TraditionsModel> GetTraditionsData(TraditionsModel Data)
		{
			ResponseModel<TraditionsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<TraditionsModel>();
                TraditionsDAL TraditionsDalobj = new TraditionsDAL(dbContext);
                TraditionsModel value = TraditionsDalobj.GetTraditionsData(Data);
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

		public ResponseModel<string> DeleteTraditionsData(TraditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TraditionsDAL TraditionsDalobj = new TraditionsDAL(dbContext);
                string value = TraditionsDalobj.DeleteTraditionsData(Data);
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

		public ResponseModel<List<TraditionsModel >> GetAllTraditionsForUserID(long userId)
		{
			ResponseModel<List<TraditionsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<TraditionsModel >>();
                TraditionsDAL TraditionsDalobj = new TraditionsDAL(dbContext);
                List<TraditionsModel> value = TraditionsDalobj.GetAllTraditionsForUserID(userId);
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

		public ResponseModel<string> SaveTradition(TraditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TraditionsDAL TraditionsDalobj = new TraditionsDAL(dbContext);
                string value = TraditionsDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateTraditionsData(TraditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TraditionsDAL TraditionsDalobj = new TraditionsDAL(dbContext);
                string value = TraditionsDalobj.UpdateTraditionsData(Data);
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
