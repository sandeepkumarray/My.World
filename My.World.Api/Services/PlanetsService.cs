using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class PlanetsService : IPlanetsService
	{
		public DBContext dbContext;


		public PlanetsService()
		{
		}

		public  PlanetsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddPlanetsData(PlanetsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                PlanetsDAL PlanetsDalobj = new PlanetsDAL(dbContext);
                string value = PlanetsDalobj.AddPlanetsData(Data);
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

		public ResponseModel<PlanetsModel> GetPlanetsData(PlanetsModel Data)
		{
			ResponseModel<PlanetsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<PlanetsModel>();
                PlanetsDAL PlanetsDalobj = new PlanetsDAL(dbContext);
                PlanetsModel value = PlanetsDalobj.GetPlanetsData(Data);
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

		public ResponseModel<string> DeletePlanetsData(PlanetsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                PlanetsDAL PlanetsDalobj = new PlanetsDAL(dbContext);
                string value = PlanetsDalobj.DeletePlanetsData(Data);
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

		public ResponseModel<List<PlanetsModel >> GetAllPlanetsForUserID(long userId)
		{
			ResponseModel<List<PlanetsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<PlanetsModel >>();
                PlanetsDAL PlanetsDalobj = new PlanetsDAL(dbContext);
                List<PlanetsModel> value = PlanetsDalobj.GetAllPlanetsForUserID(userId);
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

		public ResponseModel<string> SavePlanet(PlanetsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                PlanetsDAL PlanetsDalobj = new PlanetsDAL(dbContext);
                string value = PlanetsDalobj.SaveData(Data);
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
