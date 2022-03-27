using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface ISportsService
	{

		ResponseModel<string> AddSportsData(SportsModel Data);

		ResponseModel<SportsModel> GetSportsData(SportsModel Data);

		ResponseModel<string> DeleteSportsData(SportsModel Data);

		ResponseModel<List<SportsModel >> GetAllSportsForUserID(long userId);

		ResponseModel<string> SaveSport(SportsModel Data);

		ResponseModel<string> UpdateSportsData(SportsModel Data);

	}
}
