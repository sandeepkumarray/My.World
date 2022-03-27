using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IPlanetsService
	{

		ResponseModel<string> AddPlanetsData(PlanetsModel Data);

		ResponseModel<PlanetsModel> GetPlanetsData(PlanetsModel Data);

		ResponseModel<string> DeletePlanetsData(PlanetsModel Data);

		ResponseModel<List<PlanetsModel >> GetAllPlanetsForUserID(long userId);

		ResponseModel<string> SavePlanet(PlanetsModel Data);

		ResponseModel<string> UpdatePlanetsData(PlanetsModel Data);

	}
}
