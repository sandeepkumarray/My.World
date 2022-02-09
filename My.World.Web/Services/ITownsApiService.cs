using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ITownsApiService
	{

		TownsModel GetTowns(TownsModel model);

		string DeleteTowns(TownsModel model);

		List<TownsModel> GetAllTowns(long UserId);

		ResponseModel<string> SaveTown(TownsModel model);

	}
}
