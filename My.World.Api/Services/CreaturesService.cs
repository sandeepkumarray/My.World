using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class CreaturesService : ICreaturesService
	{
		public DBContext dbContext;


		public CreaturesService()
		{
		}

		public  CreaturesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddCreaturesData(CreaturesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CreaturesDAL CreaturesDalobj = new CreaturesDAL(dbContext);
                string value = CreaturesDalobj.AddCreaturesData(Data);
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

		public ResponseModel<CreaturesModel> GetCreaturesData(CreaturesModel Data)
		{
			ResponseModel<CreaturesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<CreaturesModel>();
                CreaturesDAL CreaturesDalobj = new CreaturesDAL(dbContext);
                CreaturesModel value = CreaturesDalobj.GetCreaturesData(Data);
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

		public ResponseModel<string> DeleteCreaturesData(CreaturesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CreaturesDAL CreaturesDalobj = new CreaturesDAL(dbContext);
                string value = CreaturesDalobj.DeleteCreaturesData(Data);
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

		public ResponseModel<List<CreaturesModel >> GetAllCreaturesForUserID(long userId)
		{
			ResponseModel<List<CreaturesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<CreaturesModel >>();
                CreaturesDAL CreaturesDalobj = new CreaturesDAL(dbContext);
                List<CreaturesModel> value = CreaturesDalobj.GetAllCreaturesForUserID(userId);
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

		public ResponseModel<string> SaveCreature(CreaturesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CreaturesDAL CreaturesDalobj = new CreaturesDAL(dbContext);
                string value = CreaturesDalobj.SaveData(Data);
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
