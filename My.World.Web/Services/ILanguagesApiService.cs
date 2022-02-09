using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ILanguagesApiService
	{

		LanguagesModel GetLanguages(LanguagesModel model);

		string DeleteLanguages(LanguagesModel model);

		List<LanguagesModel> GetAllLanguages(long UserId);

		ResponseModel<string> SaveLanguage(LanguagesModel model);

	}
}
