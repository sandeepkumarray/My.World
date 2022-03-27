using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IAppConfigApiService
	{

		AppConfigModel GetAppConfig(AppConfigModel model);

		string DeleteAppConfig(AppConfigModel model);

		List<AppConfigModel> GetAllAppConfig();

		ResponseModel<string> SaveAppConfig(AppConfigModel model);

	}
}
