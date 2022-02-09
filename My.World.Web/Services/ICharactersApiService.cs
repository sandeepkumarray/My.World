using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ICharactersApiService
	{

		CharactersModel GetCharacters(CharactersModel model);

		string DeleteCharacters(CharactersModel model);

		List<CharactersModel> GetAllCharacters(long UserId);

		ResponseModel<string> SaveCharacter(CharactersModel model);

	}
}
