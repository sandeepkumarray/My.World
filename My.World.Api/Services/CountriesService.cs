using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class CountriesService : ICountriesService
	{
		public DBContext dbContext;


		public CountriesService()
		{
		}

		public  CountriesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddCountriesData(CountriesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CountriesDAL CountriesDalobj = new CountriesDAL(dbContext);
                string value = CountriesDalobj.AddCountriesData(Data);
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

		public ResponseModel<CountriesModel> GetCountriesData(CountriesModel Data)
		{
			ResponseModel<CountriesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<CountriesModel>();
                CountriesDAL CountriesDalobj = new CountriesDAL(dbContext);
                CountriesModel value = CountriesDalobj.GetCountriesData(Data);
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

		public ResponseModel<string> DeleteCountriesData(CountriesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CountriesDAL CountriesDalobj = new CountriesDAL(dbContext);
                string value = CountriesDalobj.DeleteCountriesData(Data);
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

		public ResponseModel<List<CountriesModel >> GetAllCountriesForUserID(long userId)
		{
			ResponseModel<List<CountriesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<CountriesModel >>();
                CountriesDAL CountriesDalobj = new CountriesDAL(dbContext);
                List<CountriesModel> value = CountriesDalobj.GetAllCountriesForUserID(userId);
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

		public ResponseModel<string> SaveCountrie(CountriesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CountriesDAL CountriesDalobj = new CountriesDAL(dbContext);
                string value = CountriesDalobj.SaveData(Data);
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
