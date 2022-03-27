using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class LanguagesService : ILanguagesService
	{
		public DBContext dbContext;


		public LanguagesService()
		{
		}

		public  LanguagesService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddLanguagesData(LanguagesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LanguagesDAL LanguagesDalobj = new LanguagesDAL(dbContext);
                string value = LanguagesDalobj.AddLanguagesData(Data);
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

		public ResponseModel<LanguagesModel> GetLanguagesData(LanguagesModel Data)
		{
			ResponseModel<LanguagesModel> return_value = null;
            try
            {
                return_value = new ResponseModel<LanguagesModel>();
                LanguagesDAL LanguagesDalobj = new LanguagesDAL(dbContext);
                LanguagesModel value = LanguagesDalobj.GetLanguagesData(Data);
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

		public ResponseModel<string> DeleteLanguagesData(LanguagesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LanguagesDAL LanguagesDalobj = new LanguagesDAL(dbContext);
                string value = LanguagesDalobj.DeleteLanguagesData(Data);
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

		public ResponseModel<List<LanguagesModel >> GetAllLanguagesForUserID(long userId)
		{
			ResponseModel<List<LanguagesModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<LanguagesModel >>();
                LanguagesDAL LanguagesDalobj = new LanguagesDAL(dbContext);
                List<LanguagesModel> value = LanguagesDalobj.GetAllLanguagesForUserID(userId);
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

		public ResponseModel<string> SaveLanguage(LanguagesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LanguagesDAL LanguagesDalobj = new LanguagesDAL(dbContext);
                string value = LanguagesDalobj.SaveData(Data);
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

		public ResponseModel<string> UpdateLanguagesData(LanguagesModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                LanguagesDAL LanguagesDalobj = new LanguagesDAL(dbContext);
                string value = LanguagesDalobj.UpdateLanguagesData(Data);
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
