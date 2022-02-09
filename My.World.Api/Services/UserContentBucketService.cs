using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class UserContentBucketService : IUserContentBucketService
	{
		public DBContext dbContext;


		public UserContentBucketService()
		{
		}

		public  UserContentBucketService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddUserContentBucketData(UserContentBucketModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UserContentBucketDAL UserContentBucketDalobj = new UserContentBucketDAL(dbContext);
                string value = UserContentBucketDalobj.AddUserContentBucketData(Data);
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

		public ResponseModel<UserContentBucketModel> GetUserContentBucketData(UserContentBucketModel Data)
		{
			ResponseModel<UserContentBucketModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UserContentBucketModel>();
                UserContentBucketDAL UserContentBucketDalobj = new UserContentBucketDAL(dbContext);
                UserContentBucketModel value = UserContentBucketDalobj.GetUserContentBucketData(Data);
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

		public ResponseModel<string> DeleteUserContentBucketData(UserContentBucketModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UserContentBucketDAL UserContentBucketDalobj = new UserContentBucketDAL(dbContext);
                string value = UserContentBucketDalobj.DeleteUserContentBucketData(Data);
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

		public ResponseModel<List<UserContentBucketModel >> GetAllUserContentBucketForUserID(long userId)
		{
			ResponseModel<List<UserContentBucketModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<UserContentBucketModel >>();
                UserContentBucketDAL UserContentBucketDalobj = new UserContentBucketDAL(dbContext);
                List<UserContentBucketModel> value = UserContentBucketDalobj.GetAllUserContentBucketForUserID(userId);
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

		public ResponseModel<string> SaveUsercontentbucke(UserContentBucketModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UserContentBucketDAL UserContentBucketDalobj = new UserContentBucketDAL(dbContext);
                string value = UserContentBucketDalobj.SaveData(Data);
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
