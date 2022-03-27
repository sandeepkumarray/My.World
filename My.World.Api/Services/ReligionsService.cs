using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ReligionsService : IReligionsService
	{
		public DBContext dbContext;


		public ReligionsService()
		{
		}

		public  ReligionsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddReligionsData(ReligionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ReligionsDAL ReligionsDalobj = new ReligionsDAL(dbContext);
                string value = ReligionsDalobj.AddReligionsData(Data);
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

		public ResponseModel<ReligionsModel> GetReligionsData(ReligionsModel Data)
		{
			ResponseModel<ReligionsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ReligionsModel>();
                ReligionsDAL ReligionsDalobj = new ReligionsDAL(dbContext);
                ReligionsModel value = ReligionsDalobj.GetReligionsData(Data);
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

		public ResponseModel<string> DeleteReligionsData(ReligionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ReligionsDAL ReligionsDalobj = new ReligionsDAL(dbContext);
                string value = ReligionsDalobj.DeleteReligionsData(Data);
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

		public ResponseModel<List<ReligionsModel >> GetAllReligionsForUserID(long userId)
		{
			ResponseModel<List<ReligionsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ReligionsModel >>();
                ReligionsDAL ReligionsDalobj = new ReligionsDAL(dbContext);
                List<ReligionsModel> value = ReligionsDalobj.GetAllReligionsForUserID(userId);
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

		public ResponseModel<string> SaveReligion(ReligionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ReligionsDAL ReligionsDalobj = new ReligionsDAL(dbContext);
                string value = ReligionsDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateReligionsData(ReligionsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ReligionsDAL ReligionsDalobj = new ReligionsDAL(dbContext);
                string value = ReligionsDalobj.UpdateReligionsData(Data);
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
