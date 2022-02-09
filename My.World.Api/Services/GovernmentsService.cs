using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class GovernmentsService : IGovernmentsService
	{
		public DBContext dbContext;


		public GovernmentsService()
		{
		}

		public  GovernmentsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddGovernmentsData(GovernmentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GovernmentsDAL GovernmentsDalobj = new GovernmentsDAL(dbContext);
                string value = GovernmentsDalobj.AddGovernmentsData(Data);
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

		public ResponseModel<GovernmentsModel> GetGovernmentsData(GovernmentsModel Data)
		{
			ResponseModel<GovernmentsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<GovernmentsModel>();
                GovernmentsDAL GovernmentsDalobj = new GovernmentsDAL(dbContext);
                GovernmentsModel value = GovernmentsDalobj.GetGovernmentsData(Data);
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

		public ResponseModel<string> DeleteGovernmentsData(GovernmentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GovernmentsDAL GovernmentsDalobj = new GovernmentsDAL(dbContext);
                string value = GovernmentsDalobj.DeleteGovernmentsData(Data);
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

		public ResponseModel<List<GovernmentsModel >> GetAllGovernmentsForUserID(long userId)
		{
			ResponseModel<List<GovernmentsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<GovernmentsModel >>();
                GovernmentsDAL GovernmentsDalobj = new GovernmentsDAL(dbContext);
                List<GovernmentsModel> value = GovernmentsDalobj.GetAllGovernmentsForUserID(userId);
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

		public ResponseModel<string> SaveGovernment(GovernmentsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                GovernmentsDAL GovernmentsDalobj = new GovernmentsDAL(dbContext);
                string value = GovernmentsDalobj.SaveData(Data);
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
