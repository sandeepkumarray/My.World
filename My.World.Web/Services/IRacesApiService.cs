using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IRacesApiService
	{

		RacesModel GetRaces(RacesModel model);

		string DeleteRaces(RacesModel model);

		List<RacesModel> GetAllRaces(long UserId);

		ResponseModel<string> SaveRace(RacesModel model);

	}
}
