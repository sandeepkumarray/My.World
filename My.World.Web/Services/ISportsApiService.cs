using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface ISportsApiService
	{

		SportsModel GetSports(SportsModel model);

		string DeleteSports(SportsModel model);

		List<SportsModel> GetAllSports(long UserId);

		ResponseModel<string> SaveSport(SportsModel model);

	}
}
