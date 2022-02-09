using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ObjectStorageKeysService : IObjectStorageKeysService
	{
		public DBContext dbContext;


		public ObjectStorageKeysService()
		{
		}

		public  ObjectStorageKeysService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddObjectStorageKeysData(ObjectStorageKeysModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ObjectStorageKeysDAL ObjectStorageKeysDalobj = new ObjectStorageKeysDAL(dbContext);
                string value = ObjectStorageKeysDalobj.AddObjectStorageKeysData(Data);
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

		public ResponseModel<ObjectStorageKeysModel> GetObjectStorageKeysData(ObjectStorageKeysModel Data)
		{
			ResponseModel<ObjectStorageKeysModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ObjectStorageKeysModel>();
                ObjectStorageKeysDAL ObjectStorageKeysDalobj = new ObjectStorageKeysDAL(dbContext);
                ObjectStorageKeysModel value = ObjectStorageKeysDalobj.GetObjectStorageKeysData(Data);
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

		public ResponseModel<string> DeleteObjectStorageKeysData(ObjectStorageKeysModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ObjectStorageKeysDAL ObjectStorageKeysDalobj = new ObjectStorageKeysDAL(dbContext);
                string value = ObjectStorageKeysDalobj.DeleteObjectStorageKeysData(Data);
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

		public ResponseModel<List<ObjectStorageKeysModel >> GetAllObjectStorageKeysForUserID(long userId)
		{
			ResponseModel<List<ObjectStorageKeysModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ObjectStorageKeysModel >>();
                ObjectStorageKeysDAL ObjectStorageKeysDalobj = new ObjectStorageKeysDAL(dbContext);
                List<ObjectStorageKeysModel> value = ObjectStorageKeysDalobj.GetAllObjectStorageKeysForUserID(userId);
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

		public ResponseModel<string> SaveObjectstoragekey(ObjectStorageKeysModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ObjectStorageKeysDAL ObjectStorageKeysDalobj = new ObjectStorageKeysDAL(dbContext);
                string value = ObjectStorageKeysDalobj.SaveData(Data);
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
