using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class LocationsService : ILocationsService
	{
		public DBContext dbContext;


		public LocationsService()
		{
		}

		public  LocationsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddLocationsData(LocationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LocationsDAL LocationsDalobj = new LocationsDAL(dbContext);
                string value = LocationsDalobj.AddLocationsData(Data);
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

		public ResponseModel<LocationsModel> GetLocationsData(LocationsModel Data)
		{
			ResponseModel<LocationsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<LocationsModel>();
                LocationsDAL LocationsDalobj = new LocationsDAL(dbContext);
                LocationsModel value = LocationsDalobj.GetLocationsData(Data);
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

		public ResponseModel<string> DeleteLocationsData(LocationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LocationsDAL LocationsDalobj = new LocationsDAL(dbContext);
                string value = LocationsDalobj.DeleteLocationsData(Data);
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

		public ResponseModel<List<LocationsModel >> GetAllLocationsForUserID(long userId)
		{
			ResponseModel<List<LocationsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<LocationsModel >>();
                LocationsDAL LocationsDalobj = new LocationsDAL(dbContext);
                List<LocationsModel> value = LocationsDalobj.GetAllLocationsForUserID(userId);
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

		public ResponseModel<string> SaveLocation(LocationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LocationsDAL LocationsDalobj = new LocationsDAL(dbContext);
                string value = LocationsDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateLocationsData(LocationsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LocationsDAL LocationsDalobj = new LocationsDAL(dbContext);
                string value = LocationsDalobj.UpdateLocationsData(Data);
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
