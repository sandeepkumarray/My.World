using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IGovernmentsService
	{

		ResponseModel<string> AddGovernmentsData(GovernmentsModel Data);

		ResponseModel<GovernmentsModel> GetGovernmentsData(GovernmentsModel Data);

		ResponseModel<string> DeleteGovernmentsData(GovernmentsModel Data);

		ResponseModel<List<GovernmentsModel >> GetAllGovernmentsForUserID(long userId);

		ResponseModel<string> SaveGovernment(GovernmentsModel Data);

	}
}
