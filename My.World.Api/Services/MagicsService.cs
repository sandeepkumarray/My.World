using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class MagicsService : IMagicsService
	{
		public DBContext dbContext;


		public MagicsService()
		{
		}

		public  MagicsService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddMagicsData(MagicsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                MagicsDAL MagicsDalobj = new MagicsDAL(dbContext);
                string value = MagicsDalobj.AddMagicsData(Data);
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

		public ResponseModel<MagicsModel> GetMagicsData(MagicsModel Data)
		{
			ResponseModel<MagicsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<MagicsModel>();
                MagicsDAL MagicsDalobj = new MagicsDAL(dbContext);
                MagicsModel value = MagicsDalobj.GetMagicsData(Data);
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

		public ResponseModel<string> DeleteMagicsData(MagicsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                MagicsDAL MagicsDalobj = new MagicsDAL(dbContext);
                string value = MagicsDalobj.DeleteMagicsData(Data);
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

		public ResponseModel<List<MagicsModel >> GetAllMagicsForUserID(long userId)
		{
			ResponseModel<List<MagicsModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<MagicsModel >>();
                MagicsDAL MagicsDalobj = new MagicsDAL(dbContext);
                List<MagicsModel> value = MagicsDalobj.GetAllMagicsForUserID(userId);
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

		public ResponseModel<string> SaveMagic(MagicsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                MagicsDAL MagicsDalobj = new MagicsDAL(dbContext);
                string value = MagicsDalobj.SaveData(Data);
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
