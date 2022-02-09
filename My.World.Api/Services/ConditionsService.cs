using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ConditionsService : IConditionsService
	{
		public DBContext dbContext;


		public ConditionsService()
		{
		}

		public  ConditionsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddConditionsData(ConditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ConditionsDAL ConditionsDalobj = new ConditionsDAL(dbContext);
                string value = ConditionsDalobj.AddConditionsData(Data);
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

		public ResponseModel<ConditionsModel> GetConditionsData(ConditionsModel Data)
		{
			ResponseModel<ConditionsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ConditionsModel>();
                ConditionsDAL ConditionsDalobj = new ConditionsDAL(dbContext);
                ConditionsModel value = ConditionsDalobj.GetConditionsData(Data);
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

		public ResponseModel<string> DeleteConditionsData(ConditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ConditionsDAL ConditionsDalobj = new ConditionsDAL(dbContext);
                string value = ConditionsDalobj.DeleteConditionsData(Data);
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

		public ResponseModel<List<ConditionsModel >> GetAllConditionsForUserID(long userId)
		{
			ResponseModel<List<ConditionsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ConditionsModel >>();
                ConditionsDAL ConditionsDalobj = new ConditionsDAL(dbContext);
                List<ConditionsModel> value = ConditionsDalobj.GetAllConditionsForUserID(userId);
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

		public ResponseModel<string> SaveCondition(ConditionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ConditionsDAL ConditionsDalobj = new ConditionsDAL(dbContext);
                string value = ConditionsDalobj.SaveData(Data);
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
