using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ITechnologiesApiService
	{

		TechnologiesModel GetTechnologies(TechnologiesModel model);

		string DeleteTechnologies(TechnologiesModel model);

		List<TechnologiesModel> GetAllTechnologies(long UserId);

		ResponseModel<string> SaveTechnologie(TechnologiesModel model);

	}
}
