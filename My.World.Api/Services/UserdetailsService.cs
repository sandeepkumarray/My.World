using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class UserdetailsService : IUserdetailsService
	{
		public DBContext dbContext { get; set; }

		public  UserdetailsService(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

		public ResponseModel<string> AddUserDetailsData(UserDetailsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UserDetailsDal UserDetailsDalobj = new UserDetailsDal(dbContext);
                string value = UserDetailsDalobj.AddUserDetailsData(Data);
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

		public ResponseModel<string> UpdateUserDetailsData(UserDetailsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UserDetailsDal UserDetailsDalobj = new UserDetailsDal(dbContext);
                string value = UserDetailsDalobj.UpdateUserDetailsData(Data);
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

		public ResponseModel<UserDetailsModel> GetUserDetailsData(UserDetailsModel Data)
		{
			ResponseModel<UserDetailsModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UserDetailsModel>();
                UserDetailsDal UserDetailsDalobj = new UserDetailsDal(dbContext);
                UserDetailsModel value = UserDetailsDalobj.GetUserDetailsData(Data);
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

		public ResponseModel<string> DeleteUserDetailsData(UserDetailsModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UserDetailsDal UserDetailsDalobj = new UserDetailsDal(dbContext);
                string value = UserDetailsDalobj.DeleteUserDetailsData(Data);
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

		public ResponseModel<List<UserDetailsModel>> GetAllUserDetailsData()
		{
			ResponseModel<List<UserDetailsModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<UserDetailsModel>>();
                UserDetailsDal UserDetailsDalobj = new UserDetailsDal(dbContext);
                List<UserDetailsModel> value = UserDetailsDalobj.SelectAllUserDetailsData();
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
