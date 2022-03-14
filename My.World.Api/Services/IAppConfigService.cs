using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;

namespace My.World.Api.Services
{
	public interface IAppConfigService
	{

		ResponseModel<string> AddAppConfigData(AppConfigModel Data);

		ResponseModel<AppConfigModel> GetAppConfigData(AppConfigModel Data);

		ResponseModel<string> DeleteAppConfigData(AppConfigModel Data);

		ResponseModel<List<AppConfigModel >> GetAllAppConfigForUserID();

		ResponseModel<string> SaveAppConfig(AppConfigModel Data);
		ResponseModel<string> UpdateAppConfig(AppConfigModel Data);

	}
}
