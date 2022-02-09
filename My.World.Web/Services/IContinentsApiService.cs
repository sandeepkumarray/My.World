using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IContinentsApiService
	{

		ContinentsModel GetContinents(ContinentsModel model);

		string DeleteContinents(ContinentsModel model);

		List<ContinentsModel> GetAllContinents(long UserId);

		ResponseModel<string> SaveContinent(ContinentsModel model);

	}
}
