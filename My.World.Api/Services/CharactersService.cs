using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using My.World.Api.DataAccess;

namespace My.World.Api.Services
{
	public class CharactersService : ICharactersService
	{
		public DBContext dbContext;


		public CharactersService()
		{
		}

		public  CharactersService(DBContext dbContext)
		{
						this.dbContext = dbContext;

		}

		public ResponseModel<string> AddCharactersData(CharactersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CharactersDAL CharactersDalobj = new CharactersDAL(dbContext);
                string value = CharactersDalobj.AddCharactersData(Data);
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

		public ResponseModel<CharactersModel> GetCharactersData(CharactersModel Data)
		{
			ResponseModel<CharactersModel> return_value = null;
            try
            {
                return_value = new ResponseModel<CharactersModel>();
                CharactersDAL CharactersDalobj = new CharactersDAL(dbContext);
                CharactersModel value = CharactersDalobj.GetCharactersData(Data);
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

		public ResponseModel<string> DeleteCharactersData(CharactersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CharactersDAL CharactersDalobj = new CharactersDAL(dbContext);
                string value = CharactersDalobj.DeleteCharactersData(Data);
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

		public ResponseModel<List<CharactersModel >> GetAllCharactersForUserID(long userId)
		{
			ResponseModel<List<CharactersModel >> return_value = null;
            try
            {
                return_value = new ResponseModel<List<CharactersModel >>();
                CharactersDAL CharactersDalobj = new CharactersDAL(dbContext);
                List<CharactersModel> value = CharactersDalobj.GetAllCharactersForUserID(userId);
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

		public ResponseModel<string> SaveCharacter(CharactersModel Data)
		{
			ResponseModel<string> return_value = null;
            try
            {
                return_value = new ResponseModel<string>();
                CharactersDAL CharactersDalobj = new CharactersDAL(dbContext);
                string value = CharactersDalobj.SaveData(Data);
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
