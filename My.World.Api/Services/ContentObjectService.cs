using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ContentObjectService : IContentObjectService
	{
		public DBContext dbContext;


		public ContentObjectService()
		{
		}

		public  ContentObjectService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddContentObjectData(ContentObjectModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentObjectDAL ContentObjectDalobj = new ContentObjectDAL(dbContext);
                string value = ContentObjectDalobj.AddContentObjectData(Data);
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

		public ResponseModel<ContentObjectModel> GetContentObjectData(ContentObjectModel Data)
		{
			ResponseModel<ContentObjectModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ContentObjectModel>();
                ContentObjectDAL ContentObjectDalobj = new ContentObjectDAL(dbContext);
                ContentObjectModel value = ContentObjectDalobj.GetContentObjectData(Data);
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

		public ResponseModel<string> DeleteContentObjectData(ContentObjectModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentObjectDAL ContentObjectDalobj = new ContentObjectDAL(dbContext);
                string value = ContentObjectDalobj.DeleteContentObjectData(Data);
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

		public ResponseModel<List<ContentObjectModel >> GetAllContentObjectForUserID(long userId)
		{
			ResponseModel<List<ContentObjectModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContentObjectModel >>();
                ContentObjectDAL ContentObjectDalobj = new ContentObjectDAL(dbContext);
                List<ContentObjectModel> value = ContentObjectDalobj.GetAllContentObjectForUserID(userId);
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

		public ResponseModel<string> SaveContentobjec(ContentObjectModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentObjectDAL ContentObjectDalobj = new ContentObjectDAL(dbContext);
                string value = ContentObjectDalobj.SaveData(Data);
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

        internal ResponseModel<List<ContentObjectModel>> GetAllContentObjectAttachments(long content_id, string content_type)
        {
            ResponseModel<List<ContentObjectModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContentObjectModel>>();
                ContentObjectDAL ContentObjectDalobj = new ContentObjectDAL(dbContext);
                List<ContentObjectModel> value = ContentObjectDalobj.GetAllContentObjectAttachments(content_id, content_type);
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
