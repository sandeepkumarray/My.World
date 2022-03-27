using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IRacesService
	{

		ResponseModel<string> AddRacesData(RacesModel Data);

		ResponseModel<RacesModel> GetRacesData(RacesModel Data);

		ResponseModel<string> DeleteRacesData(RacesModel Data);

		ResponseModel<List<RacesModel >> GetAllRacesForUserID(long userId);

		ResponseModel<string> SaveRace(RacesModel Data);

		ResponseModel<string> UpdateRacesData(RacesModel Data);

	}
}
