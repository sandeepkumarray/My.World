using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IPlanetsApiService
	{

		PlanetsModel GetPlanets(PlanetsModel model);

		string DeletePlanets(PlanetsModel model);

		List<PlanetsModel> GetAllPlanets(long UserId);

		ResponseModel<string> SavePlanet(PlanetsModel model);

	}
}
