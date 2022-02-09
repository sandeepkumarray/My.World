using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ContentObjectAttachmentService : IContentObjectAttachmentService
	{
		public DBContext dbContext;


		public ContentObjectAttachmentService()
		{
		}

		public  ContentObjectAttachmentService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddContentObjectAttachmentData(ContentObjectAttachmentModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentObjectAttachmentDAL ContentObjectAttachmentDalobj = new ContentObjectAttachmentDAL(dbContext);
                string value = ContentObjectAttachmentDalobj.AddContentObjectAttachmentData(Data);
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

		public ResponseModel<ContentObjectAttachmentModel> GetContentObjectAttachmentData(ContentObjectAttachmentModel Data)
		{
			ResponseModel<ContentObjectAttachmentModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ContentObjectAttachmentModel>();
                ContentObjectAttachmentDAL ContentObjectAttachmentDalobj = new ContentObjectAttachmentDAL(dbContext);
                ContentObjectAttachmentModel value = ContentObjectAttachmentDalobj.GetContentObjectAttachmentData(Data);
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

		public ResponseModel<string> DeleteContentObjectAttachmentData(ContentObjectAttachmentModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentObjectAttachmentDAL ContentObjectAttachmentDalobj = new ContentObjectAttachmentDAL(dbContext);
                string value = ContentObjectAttachmentDalobj.DeleteContentObjectAttachmentData(Data);
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

		public ResponseModel<List<ContentObjectAttachmentModel >> GetAllContentObjectAttachmentForUserID(long userId)
		{
			ResponseModel<List<ContentObjectAttachmentModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContentObjectAttachmentModel >>();
                ContentObjectAttachmentDAL ContentObjectAttachmentDalobj = new ContentObjectAttachmentDAL(dbContext);
                List<ContentObjectAttachmentModel> value = ContentObjectAttachmentDalobj.GetAllContentObjectAttachmentForUserID(userId);
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

		public ResponseModel<string> SaveContentobjectattachmen(ContentObjectAttachmentModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentObjectAttachmentDAL ContentObjectAttachmentDalobj = new ContentObjectAttachmentDAL(dbContext);
                string value = ContentObjectAttachmentDalobj.SaveData(Data);
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
