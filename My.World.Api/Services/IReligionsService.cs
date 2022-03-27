using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IReligionsService
	{

		ResponseModel<string> AddReligionsData(ReligionsModel Data);

		ResponseModel<ReligionsModel> GetReligionsData(ReligionsModel Data);

		ResponseModel<string> DeleteReligionsData(ReligionsModel Data);

		ResponseModel<List<ReligionsModel >> GetAllReligionsForUserID(long userId);

		ResponseModel<string> SaveReligion(ReligionsModel Data);

		ResponseModel<string> UpdateReligionsData(ReligionsModel Data);

	}
}
