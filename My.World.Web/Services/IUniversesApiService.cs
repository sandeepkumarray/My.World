using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IUniversesApiService
	{

		UniversesModel GetUniverses(UniversesModel model);

		string DeleteUniverses(UniversesModel model);

		List<UniversesModel> GetAllUniverses(long UserId);

		ResponseModel<string> SaveUniverse(UniversesModel model);

	}
}
