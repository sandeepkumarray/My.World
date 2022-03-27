using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class DeitiesService : IDeitiesService
	{
		public DBContext dbContext;


		public DeitiesService()
		{
		}

		public  DeitiesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddDeitiesData(DeitiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DeitiesDAL DeitiesDalobj = new DeitiesDAL(dbContext);
                string value = DeitiesDalobj.AddDeitiesData(Data);
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

		public ResponseModel<DeitiesModel> GetDeitiesData(DeitiesModel Data)
		{
			ResponseModel<DeitiesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<DeitiesModel>();
                DeitiesDAL DeitiesDalobj = new DeitiesDAL(dbContext);
                DeitiesModel value = DeitiesDalobj.GetDeitiesData(Data);
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

		public ResponseModel<string> DeleteDeitiesData(DeitiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DeitiesDAL DeitiesDalobj = new DeitiesDAL(dbContext);
                string value = DeitiesDalobj.DeleteDeitiesData(Data);
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

		public ResponseModel<List<DeitiesModel >> GetAllDeitiesForUserID(long userId)
		{
			ResponseModel<List<DeitiesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<DeitiesModel >>();
                DeitiesDAL DeitiesDalobj = new DeitiesDAL(dbContext);
                List<DeitiesModel> value = DeitiesDalobj.GetAllDeitiesForUserID(userId);
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

		public ResponseModel<string> SaveDeitie(DeitiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DeitiesDAL DeitiesDalobj = new DeitiesDAL(dbContext);
                string value = DeitiesDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateDeitiesData(DeitiesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                DeitiesDAL DeitiesDalobj = new DeitiesDAL(dbContext);
                string value = DeitiesDalobj.UpdateDeitiesData(Data);
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
