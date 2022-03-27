using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ILanguagesService
	{

		ResponseModel<string> AddLanguagesData(LanguagesModel Data);

		ResponseModel<LanguagesModel> GetLanguagesData(LanguagesModel Data);

		ResponseModel<string> DeleteLanguagesData(LanguagesModel Data);

		ResponseModel<List<LanguagesModel >> GetAllLanguagesForUserID(long userId);

		ResponseModel<string> SaveLanguage(LanguagesModel Data);

		ResponseModel<string> UpdateLanguagesData(LanguagesModel Data);

	}
}
