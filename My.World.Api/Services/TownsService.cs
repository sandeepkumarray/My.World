using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class TownsService : ITownsService
	{
		public DBContext dbContext;


		public TownsService()
		{
		}

		public  TownsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddTownsData(TownsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TownsDAL TownsDalobj = new TownsDAL(dbContext);
                string value = TownsDalobj.AddTownsData(Data);
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

		public ResponseModel<TownsModel> GetTownsData(TownsModel Data)
		{
			ResponseModel<TownsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<TownsModel>();
                TownsDAL TownsDalobj = new TownsDAL(dbContext);
                TownsModel value = TownsDalobj.GetTownsData(Data);
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

		public ResponseModel<string> DeleteTownsData(TownsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TownsDAL TownsDalobj = new TownsDAL(dbContext);
                string value = TownsDalobj.DeleteTownsData(Data);
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

		public ResponseModel<List<TownsModel >> GetAllTownsForUserID(long userId)
		{
			ResponseModel<List<TownsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<TownsModel >>();
                TownsDAL TownsDalobj = new TownsDAL(dbContext);
                List<TownsModel> value = TownsDalobj.GetAllTownsForUserID(userId);
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

		public ResponseModel<string> SaveTown(TownsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                TownsDAL TownsDalobj = new TownsDAL(dbContext);
                string value = TownsDalobj.SaveData(Data);
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
