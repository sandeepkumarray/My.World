using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IContinentsService
	{

		ResponseModel<string> AddContinentsData(ContinentsModel Data);

		ResponseModel<ContinentsModel> GetContinentsData(ContinentsModel Data);

		ResponseModel<string> DeleteContinentsData(ContinentsModel Data);

		ResponseModel<List<ContinentsModel >> GetAllContinentsForUserID(long userId);

		ResponseModel<string> SaveContinent(ContinentsModel Data);

	}
}
