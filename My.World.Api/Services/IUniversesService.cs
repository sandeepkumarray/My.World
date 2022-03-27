using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IUniversesService
	{

		ResponseModel<string> AddUniversesData(UniversesModel Data);

		ResponseModel<UniversesModel> GetUniversesData(UniversesModel Data);

		ResponseModel<string> DeleteUniversesData(UniversesModel Data);

		ResponseModel<List<UniversesModel >> GetAllUniversesForUserID(long userId);

		ResponseModel<string> SaveUniverse(UniversesModel Data);

		ResponseModel<string> UpdateUniversesData(UniversesModel Data);

	}
}
