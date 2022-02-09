using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IBuildingsService
	{

		ResponseModel<string> AddBuildingsData(BuildingsModel Data);

		ResponseModel<BuildingsModel> GetBuildingsData(BuildingsModel Data);

		ResponseModel<string> DeleteBuildingsData(BuildingsModel Data);

		ResponseModel<List<BuildingsModel >> GetAllBuildingsForUserID(long userId);

		ResponseModel<string> SaveBuilding(BuildingsModel Data);

	}
}
