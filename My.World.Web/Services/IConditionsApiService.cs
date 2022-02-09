using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IConditionsApiService
	{

		ConditionsModel GetConditions(ConditionsModel model);

		string DeleteConditions(ConditionsModel model);

		List<ConditionsModel> GetAllConditions(long UserId);

		ResponseModel<string> SaveCondition(ConditionsModel model);

	}
}
