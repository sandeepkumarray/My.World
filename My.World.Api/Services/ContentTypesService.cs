using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class ContentTypesService : IContentTypesService
	{
		public DBContext dbContext;


		public ContentTypesService()
		{
		}

		public  ContentTypesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddContentTypesData(ContentTypesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentTypesDAL ContentTypesDalobj = new ContentTypesDAL(dbContext);
                string value = ContentTypesDalobj.AddContentTypesData(Data);
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

		public ResponseModel<ContentTypesModel> GetContentTypesData(ContentTypesModel Data)
		{
			ResponseModel<ContentTypesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ContentTypesModel>();
                ContentTypesDAL ContentTypesDalobj = new ContentTypesDAL(dbContext);
                ContentTypesModel value = ContentTypesDalobj.GetContentTypesData(Data);
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

		public ResponseModel<string> DeleteContentTypesData(ContentTypesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentTypesDAL ContentTypesDalobj = new ContentTypesDAL(dbContext);
                string value = ContentTypesDalobj.DeleteContentTypesData(Data);
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

		public ResponseModel<List<ContentTypesModel >> GetAllContentTypesForUserID(long userId)
		{
			ResponseModel<List<ContentTypesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<ContentTypesModel >>();
                ContentTypesDAL ContentTypesDalobj = new ContentTypesDAL(dbContext);
                List<ContentTypesModel> value = ContentTypesDalobj.GetAllContentTypesForUserID(userId);
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

		public ResponseModel<string> SaveContenttype(ContentTypesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                ContentTypesDAL ContentTypesDalobj = new ContentTypesDAL(dbContext);
                string value = ContentTypesDalobj.SaveData(Data);
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
