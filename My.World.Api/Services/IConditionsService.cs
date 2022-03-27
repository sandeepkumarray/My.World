using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IConditionsService
	{

		ResponseModel<string> AddConditionsData(ConditionsModel Data);

		ResponseModel<ConditionsModel> GetConditionsData(ConditionsModel Data);

		ResponseModel<string> DeleteConditionsData(ConditionsModel Data);

		ResponseModel<List<ConditionsModel >> GetAllConditionsForUserID(long userId);

		ResponseModel<string> SaveCondition(ConditionsModel Data);

		ResponseModel<string> UpdateConditionsData(ConditionsModel Data);

	}
}
