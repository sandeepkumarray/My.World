using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IScenesService
	{

		ResponseModel<string> AddScenesData(ScenesModel Data);

		ResponseModel<ScenesModel> GetScenesData(ScenesModel Data);

		ResponseModel<string> DeleteScenesData(ScenesModel Data);

		ResponseModel<List<ScenesModel >> GetAllScenesForUserID(long userId);

		ResponseModel<string> SaveScene(ScenesModel Data);

		ResponseModel<string> UpdateScenesData(ScenesModel Data);

	}
}
