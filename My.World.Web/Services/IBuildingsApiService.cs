using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IBuildingsApiService
	{

		BuildingsModel GetBuildings(BuildingsModel model);

		string DeleteBuildings(BuildingsModel model);

		List<BuildingsModel> GetAllBuildings(long UserId);

		ResponseModel<string> SaveBuilding(BuildingsModel model);

	}
}
