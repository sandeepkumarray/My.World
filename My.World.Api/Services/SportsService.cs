using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class SportsService : ISportsService
	{
		public DBContext dbContext;


		public SportsService()
		{
		}

		public  SportsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddSportsData(SportsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                SportsDAL SportsDalobj = new SportsDAL(dbContext);
                string value = SportsDalobj.AddSportsData(Data);
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

		public ResponseModel<SportsModel> GetSportsData(SportsModel Data)
		{
			ResponseModel<SportsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<SportsModel>();
                SportsDAL SportsDalobj = new SportsDAL(dbContext);
                SportsModel value = SportsDalobj.GetSportsData(Data);
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

		public ResponseModel<string> DeleteSportsData(SportsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                SportsDAL SportsDalobj = new SportsDAL(dbContext);
                string value = SportsDalobj.DeleteSportsData(Data);
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

		public ResponseModel<List<SportsModel >> GetAllSportsForUserID(long userId)
		{
			ResponseModel<List<SportsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<SportsModel >>();
                SportsDAL SportsDalobj = new SportsDAL(dbContext);
                List<SportsModel> value = SportsDalobj.GetAllSportsForUserID(userId);
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

		public ResponseModel<string> SaveSport(SportsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                SportsDAL SportsDalobj = new SportsDAL(dbContext);
                string value = SportsDalobj.SaveData(Data);
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
