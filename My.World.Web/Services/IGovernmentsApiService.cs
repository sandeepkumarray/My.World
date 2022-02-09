using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IGovernmentsApiService
	{

		GovernmentsModel GetGovernments(GovernmentsModel model);

		string DeleteGovernments(GovernmentsModel model);

		List<GovernmentsModel> GetAllGovernments(long UserId);

		ResponseModel<string> SaveGovernment(GovernmentsModel model);

	}
}
