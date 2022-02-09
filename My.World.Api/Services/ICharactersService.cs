using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ICharactersService
	{

		ResponseModel<string> AddCharactersData(CharactersModel Data);

		ResponseModel<CharactersModel> GetCharactersData(CharactersModel Data);

		ResponseModel<string> DeleteCharactersData(CharactersModel Data);

		ResponseModel<List<CharactersModel >> GetAllCharactersForUserID(long userId);

		ResponseModel<string> SaveCharacter(CharactersModel Data);

	}
}
