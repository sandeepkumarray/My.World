using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class RacesService : IRacesService
	{
		public DBContext dbContext;


		public RacesService()
		{
		}

		public  RacesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddRacesData(RacesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                RacesDAL RacesDalobj = new RacesDAL(dbContext);
                string value = RacesDalobj.AddRacesData(Data);
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

		public ResponseModel<RacesModel> GetRacesData(RacesModel Data)
		{
			ResponseModel<RacesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<RacesModel>();
                RacesDAL RacesDalobj = new RacesDAL(dbContext);
                RacesModel value = RacesDalobj.GetRacesData(Data);
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

		public ResponseModel<string> DeleteRacesData(RacesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                RacesDAL RacesDalobj = new RacesDAL(dbContext);
                string value = RacesDalobj.DeleteRacesData(Data);
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

		public ResponseModel<List<RacesModel >> GetAllRacesForUserID(long userId)
		{
			ResponseModel<List<RacesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<RacesModel >>();
                RacesDAL RacesDalobj = new RacesDAL(dbContext);
                List<RacesModel> value = RacesDalobj.GetAllRacesForUserID(userId);
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

		public ResponseModel<string> SaveRace(RacesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                RacesDAL RacesDalobj = new RacesDAL(dbContext);
                string value = RacesDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateRacesData(RacesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                RacesDAL RacesDalobj = new RacesDAL(dbContext);
                string value = RacesDalobj.UpdateRacesData(Data);
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
