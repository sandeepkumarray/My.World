using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class UsersService : IUsersService
	{
		public DBContext dbContext { get; set; }

		public  UsersService(DBContext dbContext)
		{
			this.dbContext = dbContext;

		}

        internal ResponseModel<UsersModel> LoginUser(UsersModel Data)
        {
            ResponseModel<UsersModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UsersModel>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                UsersModel value = UsersDalobj.LoginUser(Data);
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

        internal ResponseModel<string> SignupUser(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.SignupUser(Data);
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

        public ResponseModel<string> AddUsersData(UsersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.AddUsersData(Data);
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

		public ResponseModel<string> UpdateUsersData(UsersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersData(Data);
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

        public ResponseModel<UsersModel> GetUsersData(UsersModel Data)
		{
			ResponseModel<UsersModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UsersModel>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                UsersModel value = UsersDalobj.GetUsersData(Data);
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
        public ResponseModel<UsersModel> GetUsersDataByEmail(UsersModel Data)
        {
            ResponseModel<UsersModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UsersModel>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                UsersModel value = UsersDalobj.GetUsersDataByEmail(Data);
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

        public ResponseModel<string> DeleteUsersData(UsersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.DeleteUsersData(Data);
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

		public ResponseModel<List<UsersModel>> GetAllUsersData()
		{
			ResponseModel<List<UsersModel>> return_value = null;
            try
            {
                return_value = new ResponseModel<List<UsersModel>>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                List<UsersModel> value = UsersDalobj.SelectAllUsersData();
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

        public ResponseModel<string> UpdateUsersProfileData(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersProfileData(Data);
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

        public ResponseModel<string> UpdateUsersAccountData(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersAccountData(Data);
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

        public ResponseModel<string> UpdateUsersPasswordData(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersPasswordData(Data);
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

        public ResponseModel<string> UpdateUsersSignInData(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersSignInData(Data);
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

        public ResponseModel<UsersModel> UpdateUsersEmailConfirmData(UsersModel Data)
        {
            ResponseModel<UsersModel> return_value = null;
            try
            {
                return_value = new ResponseModel<UsersModel>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                UsersModel value = UsersDalobj.UpdateUsersEmailConfirmData(Data);
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

        public ResponseModel<string> UpdateUsersSecureCodeData(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersSecureCodeData(Data);
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

        public ResponseModel<string> UpdateUsersPlan(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersPlan(Data);
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

        public ResponseModel<string> UpdateUsersContentTemplate(UsersModel Data)
        {
            ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                string value = UsersDalobj.UpdateUsersContentTemplate(Data);
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

        public ResponseModel<ContentTemplateModel> GetUsersContentTemplate(UsersModel Data)
        {
            ResponseModel<ContentTemplateModel> return_value = null;
            try
            {
                return_value = new ResponseModel<ContentTemplateModel>();
                UsersDal UsersDalobj = new UsersDal(dbContext);
                ContentTemplateModel value = UsersDalobj.GetUserContentTemplate(Data.id);
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
